using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Centrex
{
    public partial class add_pedido : Form
    {
        private bool emitir = false;
        private double totalOriginal;
        private double subTotalOriginal;
        public ComprobanteEntity comprobanteSeleccionado = new();
        private double markupOriginal = -1;
        private int nPedido = -1;
        private bool historico;
        private int numeroPedido_anulado = -1;
        private int idUsuario;
        private Guid idUnico;
        private ColumnClickEventArgs? orderCol = null;
        private bool suspendEventos = false;

        private const string GRID_COL_ID = "ID";
        private const string GRID_COL_PRODUCTO = "Producto";
        private const string GRID_COL_CANT = "Cant.";
        private const string GRID_COL_PRECIO = "Precio";
        private const string GRID_COL_SUBTOTAL = "Subtotal";

        public add_pedido()
        {
            InitializeComponent();
            HookExtraEvents();
        }

        public add_pedido(Guid _idUnico) : this()
        {
            idUnico = _idUnico;
        }

        private void HookExtraEvents()
        {
            FormClosed += add_pedido_FormClosed;
            KeyDown += add_pedido_KeyDown;
            dg_items.DoubleClick += dg_items_DoubleClick;
            dg_items.MouseDown += dg_items_MouseDown;
            EditarToolStripMenuItem.Click += EditarToolStripMenuItem_Click;
            RecargarPrecioToolStripMenuItem.Click += RecargarPrecioToolStripMenuItem_Click;
            txt_markup.KeyPress += txt_markup_KeyPress;
            txt_markup.KeyDown += txt_markup_KeyDown;
            txt_impuestos.TextChanged += txt_impuestos_TextChanged;
            cmb_cliente.SelectionChangeCommitted += cmb_cliente_SelectionChangeCommitted;
            cmb_cliente.Leave += cmb_cliente_Leave;
            cmb_comprobante.SelectionChangeCommitted += cmb_comprobante_SelectionChangeCommitted;
            cmb_cc.SelectionChangeCommitted += cmb_cc_SelectionChangeCommitted;
            cmb_seleccionarComprobanteAnulación.Click += cmb_seleccionarComprobanteAnulación_Click;
            cmd_recargaprecios.Click += cmd_recargaprecios_Click;
            cmd_addItemManual.Click += cmd_addItemManual_Click;
            cmd_add_descuento.Click += cmd_add_descuento_Click;
            cmd_emitir.Click += cmd_emitir_Click;
            txt_markup.LostFocus += txt_markup_LostFocus;
            chk_presupuesto.CheckedChanged += chk_presupuesto_CheckedChanged;
        }

        private void add_pedido_Load(object sender, EventArgs e)
        {
            try
            {
                SuspendLayout();

                if (idUnico == Guid.Empty)
                {
                    idUnico = Generar_ID_Unico();
                }

                idUsuario = usuario_logueado?.IdUsuario ?? 0;

                historico = activo;
                activo = true;

                InicializarCombosClientes();
                InicializarCombosComprobantes();

                cmb_cliente.Text = "Seleccione un cliente...";
                cmb_comprobante.Text = "Seleccione un comprobante...";
                cmb_cc.Text = "Seleccione una cuenta corriente...";

                cmb_cliente.SelectedIndex = -1;
                ClienteEntity? cliDefault = null;

                if (clientes.estaClienteDefault(id_cliente_pedido_default))
                {
                    cmb_cliente.SelectedValue = id_cliente_pedido_default;
                    cliDefault = clientes.info_cliente(id_cliente_pedido_default);
                }
                else if (cmb_cliente.Items.Count > 0)
                {
                    cmb_cliente.SelectedIndex = 0;
                    if (cmb_cliente.SelectedValue != null)
                    {
                        cliDefault = clientes.info_cliente(Conversions.ToInteger(cmb_cliente.SelectedValue));
                    }
                }

                if (cliDefault != null)
                {
                    checkCustNoTaxNumber(cliDefault);
                }

                cmb_cliente_SelectionChangeCommitted(null, null);
                cmb_comprobante_SelectionChangeCommitted(null, null);

                lbl_date.Text = generales.Hoy();
                txt_total.Text = "0,00";
                txt_subTotal.Text = "0,00";
                txt_impuestos.Text = "0,00";
                txt_totalO.Text = txt_total.Text;
                txt_totalDescuentos.Text = "0,00";

                LoadGridItems().GetAwaiter().GetResult();
                txt_markup_LostFocus(null, null);

                cmd_ok.Enabled = false;
                cmd_emitir.Enabled = false;

                if (edicion || borrado || terminarpedido)
                {
                    CargarPedidoExistente();
                }
                else
                {
                    MeActiveControl();
                }

                if (txt_markup.Text != "0")
                {
                    cmd_add_descuento.Enabled = false;
                }

                if (borrado)
                {
                    ManejarFormularioBorrado();
                }

                ResumeLayout();
            }
            catch (Exception ex)
            {
                Interaction.MsgBox("Error al cargar el formulario: " + ex.Message, MsgBoxStyle.Critical, "Centrex");
            }
        }

        private void MeActiveControl()
        {
            ActiveControl = cmb_cliente;
        }

        private void InicializarCombosClientes()
        {
            var orden = new List<Tuple<string, bool>>
            {
                Tuple.Create("RazonSocial", true)
            };

            generales.Cargar_Combo(
                ref cmb_cliente,
                entidad: "ClienteEntity",
                displaymember: "RazonSocial",
                valuemember: "IdCliente",
                predet: -1,
                soloActivos: true,
                orden: orden
            );
        }

        private void InicializarCombosComprobantes()
        {
            generales.Cargar_Combo(
                ref cmb_comprobante,
                entidad: "ComprobanteEntity",
                displaymember: "Comprobante",
                valuemember: "IdComprobante",
                predet: id_comprobante_default,
                soloActivos: true
            );

            comprobanteSeleccionado = comprobantes.info_comprobante(id_comprobante_default);
            if (comprobanteSeleccionado != null && comprobanteSeleccionado.IdComprobante != 0)
            {
                cmb_comprobante.SelectedValue = comprobanteSeleccionado.IdComprobante;
            }
        }

        private async Task LoadGridItems()
        {
            using var ctx = new CentrexDbContext();

            var query = from t in ctx.TmpPedidoItemEntity
                        where t.IdUsuario == idUsuario && t.IdUnico == idUnico && t.Activo
                        join i in ctx.ItemEntity on t.IdItem equals i.IdItem into it
                        from i in it.DefaultIfEmpty()
                        orderby t.IdTmpPedidoItem
                        select new
                        {
                            ID = t.IdItem.HasValue ? $"{t.IdTmpPedidoItem}-{t.IdItem.Value}" : $"{t.IdTmpPedidoItem}-0",
                            id_pedidoItem = t.IdPedidoItem,
                            Producto = i != null ? i.Descript : t.Descript,
                            Cant = t.Cantidad,
                            Precio = t.Precio,
                            Subtotal = Math.Round((decimal)(t.Cantidad * t.Precio), 2)
                        };

            var data = await query.ToListAsync();

            var result = new DataGridQueryResult
            {
                EsMaterializada = true,
                DataMaterializada = data
            };

            await LoadDataGridWithEntityAsync(dg_items, result);

            if (!dg_items.Columns.Contains(GRID_COL_ID))
            {
                return;
            }

            dg_items.Columns[GRID_COL_ID].Visible = false;
            if (dg_items.Columns.Contains("id_pedidoItem"))
            {
                dg_items.Columns["id_pedidoItem"].Visible = false;
            }
        }

        private void CargarPedidoExistente()
        {
            if (edita_pedido == null)
            {
                Interaction.MsgBox("No se encontró el pedido a editar.", MsgBoxStyle.Exclamation, "Centrex");
                return;
            }

            lbl_date.Text = edita_pedido.Fecha.ToDateTime(TimeOnly.MinValue).ToString("dd/MM/yyyy");

            comprobanteSeleccionado = comprobantes.info_comprobante(edita_pedido.IdComprobante);
            cmb_comprobante.SelectedValue = edita_pedido.IdComprobante;
            cmb_comprobante.Text = comprobanteSeleccionado.Comprobante;

            cmb_cliente.SelectedValue = edita_pedido.IdCliente;
            cmb_cliente_SelectionChangeCommitted(null, null);

            txt_markup.Text = (edita_pedido.Markup ?? 0).ToString("N2");
            txt_subTotal.Text = edita_pedido.Subtotal.ToString("N2");
            txt_impuestos.Text = (edita_pedido.Iva ?? 0).ToString("N2");
            txt_total.Text = edita_pedido.Total.ToString("N2");
            txt_totalO.Text = txt_total.Text;
            txt_nota1.Text = edita_pedido.Nota1;
            txt_nota2.Text = edita_pedido.Nota2;
            txt_comprobanteAsociado.Text = edita_pedido.NumeroComprobanteAnulado.HasValue
                ? edita_pedido.NumeroComprobanteAnulado.Value.ToString()
                : string.Empty;
            chk_presupuesto.Checked = edita_pedido.EsPresupuesto ?? false;
            chk_secuencia.Enabled = false;
            subTotalOriginal = double.TryParse(txt_subTotal.Text, out var st) ? st : 0d;
            chk_esTest.Checked = edita_pedido.EsTest ?? false;

            markupOriginal = double.TryParse(txt_markup.Text, out var mk) ? mk : 0d;
            lbl_order.Text = edita_pedido.IdPedido.ToString();
            lbl_pedido.Visible = true;
            lbl_order.Visible = true;

            if ((edita_pedido.NumeroComprobanteAnulado ?? 0) != 0)
            {
                MostrarPanelComprobanteAsociado(true);
                txt_comprobanteAsociado.Text = edita_pedido.NumeroComprobanteAnulado?.ToString();
                numeroPedido_anulado = edita_pedido.NumeroPedidoAnulado ?? -1;
                cmb_cliente.Enabled = false;
                cmb_cc.Enabled = false;
                cmb_comprobante.Enabled = false;
            }

            if (!(edita_pedido.Activo ?? true) || borrado)
            {
                cmd_recargaprecios.Enabled = false;
                cmd_emitir.Enabled = false;
                cmb_comprobante.Enabled = false;
                cmb_cliente.Enabled = false;
                cmb_cc.Enabled = false;
                pic_searchComprobante.Enabled = false;
                pic_searchCliente.Enabled = false;
                dg_items.Enabled = false;
                txt_markup.Enabled = false;
                txt_subTotal.Enabled = false;
                txt_impuestos.Enabled = false;
                txt_total.Enabled = false;
                txt_nota1.Enabled = false;
                txt_nota2.Enabled = false;
                cmd_add_item.Enabled = false;
                cmd_addItemManual.Enabled = false;
                cmd_add_descuento.Enabled = false;
                cmd_ok.Enabled = false;
                chk_presupuesto.Enabled = false;
                chk_esTest.Enabled = false;
            }

            if (borrado)
            {
                cmd_exit.Enabled = false;
                Show();
                if (Interaction.MsgBox("¿Está seguro que desea borrar este pedido?", MsgBoxStyle.YesNo | MsgBoxStyle.Question, "Centrex") == MsgBoxResult.Yes)
                {
                    if (!BorrarPedido(edita_pedido))
                    {
                        if (Interaction.MsgBox("Ocurrió un error al borrar el pedido. ¿Desea desactivarlo para que no aparezca en la búsqueda?", MsgBoxStyle.Question | MsgBoxStyle.YesNo, "Centrex") == MsgBoxResult.Yes)
                        {
                            if (UpdatePedido(edita_pedido, true))
                            {
                                Interaction.MsgBox("Se realizó un borrado lógico. El pedido no se borró definitivamente.", MsgBoxStyle.Information, "Centrex");
                            }
                            else
                            {
                                Interaction.MsgBox("No se ha podido borrar el pedido.", MsgBoxStyle.Exclamation, "Centrex");
                            }
                        }
                    }
                }
                closeandupdate(this);
            }
        }

        private void MostrarPanelComprobanteAsociado(bool mostrar)
        {
            lbl_avisoAFIP_NC_ND.Visible = mostrar;
            cmb_seleccionarComprobanteAnulación.Visible = mostrar;
            lbl_comprobante.Visible = mostrar;
            lbl_comprobanteAsociado.Visible = mostrar;
            txt_comprobanteAsociado.Visible = mostrar;
        }

        private void ManejarFormularioBorrado()
        {
            cmd_exit.Enabled = false;
        }

        private void add_pedido_FormClosed(object? sender, FormClosedEventArgs e)
        {
            id = 0;
            edita_pedido = new PedidoEntity();
            BorrarTablaPedidosTemporales(idUsuario, idUnico);
            generales.activaitems("pedidos_items");
            edicion = false;
            activo = historico;
            closeandupdate(this);
        }

        private void add_pedido_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Insert)
            {
                cmd_add_item_Click(null, null);
            }
        }

        private void cmd_add_item_Click(object? sender, EventArgs e)
        {
            if (dg_items.Rows.Count >= (comprobanteSeleccionado?.MaxItems ?? 999))
            {
                Interaction.MsgBox("No se pueden agregar más ítems por comprobante.", MsgBoxStyle.Exclamation, "Centrex");
                return;
            }

            var tmpTabla = tabla;
            var tmpActivo = activo;

            tabla = "items_sinDescuento";
            activo = true;

            agregaitem = true;
            Enabled = false;

            var frm = new search(idUsuario, idUnico);
            frm.ShowDialog();

            tabla = tmpTabla;
            activo = tmpActivo;
            agregaitem = false;
            Enabled = true;

            LoadGridItems().GetAwaiter().GetResult();
            txt_markup_LostFocus(null, null);
        }

        private void cmd_add_descuento_Click(object sender, EventArgs e)
        {
            if (dg_items.Rows.Count >= (comprobanteSeleccionado?.MaxItems ?? 999))
            {
                Interaction.MsgBox("No se pueden agregar más ítems por comprobante.", MsgBoxStyle.Exclamation, "Centrex");
                return;
            }

            var tmpTabla = tabla;
            var tmpActivo = activo;

            tabla = "itemsDescuentos";
            activo = true;

            agregaitem = true;
            Enabled = false;

            var frm = new search(idUsuario, idUnico, "itemsDescuentos");
            frm.ShowDialog();

            tabla = tmpTabla;
            activo = tmpActivo;
            agregaitem = false;
            Enabled = true;

            LoadGridItems().GetAwaiter().GetResult();
            txt_markup_LostFocus(null, null);
        }

        private void cmd_addItemManual_Click(object? sender, EventArgs e)
        {
            agregaitem = true;
            var frm = new add_itemManual(idUsuario, idUnico);
            frm.ShowDialog();
            agregaitem = false;

            LoadGridItems().GetAwaiter().GetResult();
            txt_markup_LostFocus(null, null);
        }

        private void add_pedido_FormClosing(object? sender, FormClosingEventArgs e)
        {
        }

        private void cmd_exit_Click(object sender, EventArgs e)
        {
            closeandupdate(this);
        }

        private void cmd_ok_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmb_cliente.SelectedValue == null || string.Equals(cmb_cliente.Text, "Seleccione un cliente...", StringComparison.OrdinalIgnoreCase))
                {
                    Interaction.MsgBox("El campo 'Cliente' es obligatorio y está vacío", MsgBoxStyle.Exclamation, "Centrex");
                    return;
                }
                if (cmb_cc.SelectedValue == null || string.Equals(cmb_cc.Text, "Seleccione una cuenta corriente...", StringComparison.OrdinalIgnoreCase))
                {
                    Interaction.MsgBox("El campo 'Cuenta corriente' es obligatorio y está vacío", MsgBoxStyle.Exclamation, "Centrex");
                    return;
                }
                if (cmb_comprobante.SelectedValue == null || string.Equals(cmb_comprobante.Text, "Seleccione un comprobante...", StringComparison.OrdinalIgnoreCase))
                {
                    Interaction.MsgBox("El campo 'Comprobante' es obligatorio y está vacío", MsgBoxStyle.Exclamation, "Centrex");
                    return;
                }
                if (dg_items.Rows.Count == 0)
                {
                    Interaction.MsgBox("No hay ítems cargados", MsgBoxStyle.Exclamation, "Centrex");
                    return;
                }
                if (!string.IsNullOrWhiteSpace(txt_comprobanteAsociado.Text) && numeroPedido_anulado != -1)
                {
                    var pedidoAnulado = Pedidos.info_pedido(numeroPedido_anulado);
                    if (pedidoAnulado != null && double.TryParse(txt_total.Text, out var totalActual) && totalActual > (double)pedidoAnulado.Total)
                    {
                        Interaction.MsgBox("El monto del comprobante asociado no puede ser superior al comprobante original.", MsgBoxStyle.Exclamation, "Centrex");
                        return;
                    }
                }

                var pedido = edicion ? Pedidos.info_pedido(edita_pedido.IdPedido) : new PedidoEntity();
                if (pedido == null)
                {
                    pedido = new PedidoEntity();
                }

                pedido.IdComprobante = (int)cmb_comprobante.SelectedValue;
                comprobanteSeleccionado = comprobantes.info_comprobante(pedido.IdComprobante);
                pedido.IdCliente = (int)cmb_cliente.SelectedValue;
                pedido.Fecha = DateOnly.FromDateTime(DateTime.Now);
                decimal.TryParse(txt_markup.Text, NumberStyles.Any, CultureInfo.CurrentCulture, out var markupDec);
                pedido.Markup = markupDec;
                decimal.TryParse(txt_subTotal.Text, NumberStyles.Any, CultureInfo.CurrentCulture, out var subtotalDec);
                pedido.Subtotal = subtotalDec;
                decimal.TryParse(txt_impuestos.Text, NumberStyles.Any, CultureInfo.CurrentCulture, out var ivaDec);
                pedido.Iva = ivaDec;
                decimal.TryParse(txt_total.Text, NumberStyles.Any, CultureInfo.CurrentCulture, out var totalDec);
                pedido.Total = totalDec;
                pedido.Nota1 = txt_nota1.Text;
                pedido.Nota2 = txt_nota2.Text;
                pedido.Activo = true;
                pedido.EsPresupuesto = chk_presupuesto.Checked;
                if (comprobanteSeleccionado.EsPresupuesto ?? false)
                {
                    pedido.EsPresupuesto = true;
                }
                pedido.IdPresupuesto = 0;
                pedido.EsTest = chk_esTest.Checked;
                pedido.IdCc = (int)cmb_cc.SelectedValue;
                pedido.IdUsuario = idUsuario;

                if (!string.IsNullOrWhiteSpace(txt_comprobanteAsociado.Text))
                {
                    pedido.NumeroComprobanteAnulado = int.TryParse(txt_comprobanteAsociado.Text, out var nca) ? nca : null;
                    pedido.NumeroPedidoAnulado = numeroPedido_anulado;
                }

                if (pedido.Markup.HasValue && pedido.Markup.Value != 0)
                {
                    double totalPedido = (double)Pedidos.CalculoTotalPuro(dg_items);
                    double descuento;
                    var markup = (double)pedido.Markup.Value;
                    if ((comprobanteSeleccionado.EsPresupuesto ?? false) && !chk_presupuesto.Checked)
                    {
                        descuento = Math.Round(100 - ((totalPedido / (1 + (markup / 100.0)) * 100) / totalPedido), 4);
                    }
                    else
                    {
                        descuento = Math.Round(100 - (((totalPedido / (1 + (markup / 100.0)) / 1.21) * 100) / totalPedido), 4);
                    }

                    double costo = Math.Round(((totalPedido * descuento) / 100) * -1, 2);
                    var itemDescuento = info_itemDesc($"Descuento: {descuento}%") ?? new ItemEntity { Descript = "error" };

                    if (itemDescuento.Descript == "error")
                    {
                        itemDescuento.Activo = true;
                        itemDescuento.Cantidad = 1;
                        itemDescuento.Costo = descuento;
                        itemDescuento.PrecioLista = descuento;
                        itemDescuento.Descript = $"Descuento: {descuento}%";
                        itemDescuento.EsDescuento = false;
                        itemDescuento.EsMarkup = true;
                        itemDescuento.Factor = 1;
                        itemDescuento.IdProveedor = 2;
                        itemDescuento.IdTipo = 4;
                        itemDescuento.Item = "MARKUP";
                        itemDescuento.PrecioLista = 0;
                        itemDescuento.IdMarca = 2;
                        additem(itemDescuento);
                        itemDescuento.IdItem = infoItem_lastItem().IdItem;
                    }

                    int idTmpMarkup = Pedidos.ExisteDescuentoMarkupTmp(itemDescuento.IdItem);
                    if (idTmpMarkup == -1)
                    {
                        Pedidos.AddItemPedidoTmp(itemDescuento, 1, (decimal)costo, idUsuario, idUnico, -1);
                    }
                    else
                    {
                        Pedidos.AddItemPedidoTmp(itemDescuento, 1, (decimal)costo, idUsuario, idUnico, idTmpMarkup);
                    }
                }

                bool esEdicion = edicion && edita_pedido?.IdPedido > 0;
                PedidoEntity ultimoPedido = null;

                if (esEdicion)
                {
                    pedido.IdPedido = edita_pedido.IdPedido;
                    pedido.FechaEdicion = DateOnly.FromDateTime(DateTime.Now);
                    if (!Pedidos.UpdatePedido(pedido))
                    {
                        Interaction.MsgBox("Hubo un problema al actualizar el pedido.", MsgBoxStyle.Exclamation, "Centrex");
                        closeandupdate(this);
                        return;
                    }

                    Pedidos.GuardarPedido(idUsuario, idUnico, pedido.IdPedido);
                    Pedidos.BorrarTablaPedidosTemporales(idUsuario, idUnico);
                    ultimoPedido = pedido;
                }
                else
                {
                    if (Pedidos.AddPedido(pedido))
                    {
                        var ultimoId = Pedidos.Info_Ultimo_Pedido_Por_Usuario(idUsuario);
                        if (ultimoId == -1)
                        {
                            Interaction.MsgBox("Hubo un problema al obtener el pedido recién agregado.", MsgBoxStyle.Exclamation, "Centrex");
                            return;
                        }

                        ultimoPedido = Pedidos.info_pedido(ultimoId);
                        if (ultimoPedido == null)
                        {
                            Interaction.MsgBox("Hubo un problema al obtener el pedido recién agregado.", MsgBoxStyle.Exclamation, "Centrex");
                            return;
                        }

                        if (!Pedidos.GuardarPedido(idUsuario, idUnico, ultimoPedido.IdPedido))
                        {
                            Interaction.MsgBox("Hubo un problema al agregar el pedido.", MsgBoxStyle.Exclamation, "Centrex");
                            Pedidos.BorrarTablaPedidosTemporales(idUsuario, idUnico);
                            closeandupdate(this);
                            return;
                        }
                        else
                        {
                            Pedidos.BorrarTablaPedidosTemporales(idUsuario, idUnico);
                        }
                    }
                    else
                    {
                        Interaction.MsgBox("Hubo un problema al agregar el pedido.", MsgBoxStyle.Exclamation, "Centrex");
                        Pedidos.BorrarTablaPedidosTemporales(idUsuario, idUnico);
                        closeandupdate(this);
                        return;
                    }
                    edicion = true;
                    edita_pedido = ultimoPedido;
                }

                if (ultimoPedido == null)
                {
                    ultimoPedido = pedido;
                }

                Pedidos.pedido_a_pedidoTmp(ultimoPedido.IdPedido, idUsuario, idUnico);

                if (emitir)
                {
                    bool requiereFactura = !chk_presupuesto.Checked && !(comprobanteSeleccionado?.EsPresupuesto ?? false);

                    if (requiereFactura)
                    {
                        if (factura_electronica.facturar(ultimoPedido) == 1)
                        {
                            if (comprobanteSeleccionado.Contabilizar ?? false)
                            {
                                var ccC = ccClientes.info_ccCliente((int)cmb_cc.SelectedValue);
                                if (ccC == null || ccC.Nombre == "error")
                                {
                                    Interaction.MsgBox("Error al impactar el saldo en la cuenta corriente del cliente.", MsgBoxStyle.Critical, "Centrex");
                                }
                                else
                                {
                                    var tc = new TipoComprobante(comprobanteSeleccionado.IdTipoComprobante);
                                    if (tc.signoCliente == "+")
                                    {
                                        ccC.Saldo += ultimoPedido.Total;
                                    }
                                    else
                                    {
                                        ccC.Saldo -= ultimoPedido.Total;
                                    }
                                    ccClientes.updateCCCliente(ccC);

                                    var t = new TransaccionEntity
                                    {
                                        Fecha = ultimoPedido.Fecha,
                                        IdPedido = ultimoPedido.IdPedido,
                                        IdTipoComprobante = comprobanteSeleccionado.IdTipoComprobante,
                                        NumeroComprobante = ultimoPedido.EsPresupuesto ?? false
                                            ? ultimoPedido.IdPresupuesto
                                            : ultimoPedido.NumeroComprobante,
                                        PuntoVenta = ultimoPedido.PuntoVenta,
                                        Total = ultimoPedido.Total,
                                        IdCc = ultimoPedido.IdCc,
                                        IdCliente = ultimoPedido.IdCliente
                                    };

                                    if (!transacciones.Agregar_Transaccion_Desde_Pedido(t))
                                    {
                                        Interaction.MsgBox("Ha ocurrido un error al agregar la transacción en el cliente.", MsgBoxStyle.Exclamation, "Centrex");
                                        return;
                                    }
                                }
                            }

                            if (comprobanteSeleccionado.IdTipoComprobante != 1000 && comprobanteSeleccionado.IdTipoComprobante != 1001)
                            {
                                factura_electronica.imprimirFactura(ultimoPedido.IdPedido, 0, chk_remitos.Checked);
                            }
                            else
                            {
                                Interaction.MsgBox($"Se generó un comprobante: \"{comprobanteSeleccionado.Comprobante}\" por un valor de: $ {ultimoPedido.Total}", MsgBoxStyle.Information, "Centrex");
                            }
                            emitir = false;
                        }
                        else
                        {
                            Interaction.MsgBox("Ha ocurrido un error al facturar el pedido.", MsgBoxStyle.Exclamation, "Centrex");
                            return;
                        }
                    }
                    else
                    {
                        Pedidos.cerrarPedido(ultimoPedido, chk_presupuesto.Checked, chk_remitos.Checked);
                        emitir = false;
                    }
                }

                if (chk_secuencia.Checked)
                {
                    cmb_comprobante.SelectedValue = id_comprobante_default;
                    cmb_cliente.SelectedValue = id_cliente_pedido_default;
                    lbl_date.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txt_subTotal.Text = "0,00";
                    txt_impuestos.Text = "0,00";
                    txt_total.Text = "0,00";
                    txt_totalO.Text = "0,00";
                    txt_totalDescuentos.Text = "0,00";
                    txt_markup.Text = "0";
                    chk_presupuesto.Checked = false;
                    generales.borrartbl("tmppedidos_items");
                    cmb_cliente_SelectionChangeCommitted(null, null);
                    LoadGridItems().GetAwaiter().GetResult();
                    txt_markup_LostFocus(null, null);
                }
                else
                {
                    closeandupdate(this);
                }
            }
            catch (Exception ex)
            {
                Interaction.MsgBox("Error al guardar el pedido: " + ex.Message, MsgBoxStyle.Critical, "Centrex");
            }
        }

        private void cmd_emitir_Click(object sender, EventArgs e)
        {
            emitir = true;
            cmd_ok_Click(null, null);
            emitir = false;
        }

        private void dg_items_DoubleClick(object? sender, EventArgs e)
        {
            if (dg_items.CurrentRow == null)
                return;

            var idTmpValue = dg_items.CurrentRow.Cells[GRID_COL_ID]?.Value?.ToString();
            if (string.IsNullOrWhiteSpace(idTmpValue))
                return;

            var partes = idTmpValue.Split('-');
            if (!int.TryParse(partes[0], out var idTmp))
                return;

            using var ctx = new CentrexDbContext();
            var tmp = ctx.TmpPedidoItemEntity.FirstOrDefault(t => t.IdTmpPedidoItem == idTmp);
            if (tmp == null)
                return;

            ItemEntity item = null;
            if (tmp.IdItem.HasValue)
            {
                item = ctx.ItemEntity.FirstOrDefault(i => i.IdItem == tmp.IdItem.Value);
                if (item == null)
                    return;
            }

            if (item != null && (item.EsDescuento || item.EsMarkup))
                return;

            edita_item = item ?? new ItemEntity
            {
                Descript = tmp.Descript,
                IdTmpPedidoItem = tmp.IdTmpPedidoItem,
                PrecioLista = tmp.Precio,
                Cantidad = tmp.Cantidad
            };

            edita_item.IdTmpPedidoItem = tmp.IdTmpPedidoItem;

            var infoItemAgregado = new infoagregaitem(idUsuario, idUnico);
            infoItemAgregado.ShowDialog();

            LoadGridItems().GetAwaiter().GetResult();
            txt_markup_LostFocus(null, null);
        }

        private void EditarToolStripMenuItem_Click(object? sender, EventArgs e)
        {
            dg_items_DoubleClick(null, null);
        }

        private void BorrarToolStripMenuItem_Click(object? sender, EventArgs e)
        {
            if (dg_items.Rows.Count == 0)
                return;

            var idTmpValue = dg_items.CurrentRow?.Cells[GRID_COL_ID]?.Value?.ToString();
            if (string.IsNullOrWhiteSpace(idTmpValue))
                return;

            var partes = idTmpValue.Split('-');
            if (!int.TryParse(partes[0], out var idTmp))
                return;

            Pedidos.BorrarItemCargado(idTmp);

            LoadGridItems().GetAwaiter().GetResult();
            txt_markup_LostFocus(null, null);
        }

        private void RecargarPrecioToolStripMenuItem_Click(object? sender, EventArgs e)
        {
            if (dg_items.Rows.Count == 0)
                return;

            var idTmpValue = dg_items.CurrentRow?.Cells[GRID_COL_ID]?.Value?.ToString();
            if (string.IsNullOrWhiteSpace(idTmpValue))
                return;

            var partes = idTmpValue.Split('-');
            if (!int.TryParse(partes[0], out var idTmp))
                return;

            using var ctx = new CentrexDbContext();
            var tmp = ctx.TmpPedidoItemEntity.FirstOrDefault(t => t.IdTmpPedidoItem == idTmp);
            if (tmp != null && tmp.IdItem.HasValue)
            {
                Pedidos.RecargaPrecios(-1, tmp.IdItem.Value, "TmpPedidoItemEntity");
            }

            LoadGridItems().GetAwaiter().GetResult();
            txt_markup_LostFocus(null, null);
        }

        private void dg_items_MouseDown(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var hit = dg_items.HitTest(e.X, e.Y);
                if (hit.Type == DataGridViewHitTestType.Cell)
                {
                    dg_items.CurrentCell = dg_items.Rows[hit.RowIndex].Cells[hit.ColumnIndex];
                }
                ContextMenuStrip1.Visible = true;
            }
        }

        private void txt_markup_LostFocus(object? sender, EventArgs e)
        {
            if (comprobanteSeleccionado == null)
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(txt_markup.Text))
            {
                txt_markup.Text = "0";
            }

            cmd_add_descuento.Enabled = txt_markup.Text == "0";

            Pedidos.UpdatePrecios(dg_items, chk_presupuesto, txt_subTotal, txt_impuestos, txt_total, txt_totalO, txt_markup, txt_totalDescuentos, comprobanteSeleccionado, idUsuario, idUnico);

            subTotalOriginal = double.TryParse(txt_subTotal.Text, out var subtotal) ? subtotal : 0d;

            if (markupOriginal != -1 && !string.Equals(markupOriginal.ToString(CultureInfo.InvariantCulture), txt_markup.Text, StringComparison.OrdinalIgnoreCase) && edicion && edita_pedido != null)
            {
                markupOriginal = double.TryParse(txt_markup.Text, out var mk) ? mk : 0d;
                var idItemMarkup = Pedidos.IdItemMarkupPedido(edita_pedido.IdPedido);
                if (idItemMarkup != -1)
                {
                    Pedidos.BorrarItemCargado(idItemMarkup, true);
                }

                Pedidos.UpdatePrecios(dg_items, chk_presupuesto, txt_subTotal, txt_impuestos, txt_total, txt_totalO, txt_markup, txt_totalDescuentos, comprobanteSeleccionado, idUsuario, idUnico);
                txt_markup_LostFocus(null, null);
                return;
            }

            if (!double.TryParse(txt_markup.Text, NumberStyles.Any, CultureInfo.CurrentCulture, out var markup))
            {
                markup = 0;
            }

            if (markup != 0)
            {
                txt_subTotal.Text = Math.Round(subTotalOriginal / (1 + (markup / 100.0) / 1.21), 2).ToString("N2");
            }
            else
            {
                txt_subTotal.Text = Math.Round(subTotalOriginal, 2).ToString("N2");
            }

            markupOriginal = markup;

            var total = (double)Pedidos.CalculoTotalPuro(dg_items);
            if (markup != 0 && dg_items.Rows.Count > 0 && !string.IsNullOrWhiteSpace(txt_total.Text) && txt_total.Text != "0")
            {
                double descuento;
                if ((comprobanteSeleccionado.EsPresupuesto ?? false) && !chk_presupuesto.Checked)
                {
                    descuento = Math.Round(100 - ((total / (1 + (markup / 100.0)) * 100) / total), 4);
                }
                else
                {
                    descuento = Math.Round(100 - (((total / (1 + (markup / 100.0)) / 1.21) * 100) / total), 4);
                }

                txt_totalDescuentos.Text = Math.Round((descuento * total) / 100, 2).ToString("N2");
                var toolTipText = "El descuento es del: " + descuento.ToString("N2") + "%";
                tt_descuento.SetToolTip(txt_totalDescuentos, toolTipText);
                tt_descuento.Active = true;
            }
            else
            {
                txt_totalDescuentos.Text = "0";
                tt_descuento.Active = false;
            }

            if ((comprobanteSeleccionado.IdTipoComprobante == 99 || comprobanteSeleccionado.IdTipoComprobante == 0) && !chk_presupuesto.Checked && (markup == 0))
            {
                lbl_noMarkupNoPresupuesto.Visible = true;
            }
            else
            {
                lbl_noMarkupNoPresupuesto.Visible = false;
            }

            updateAndCheck();
        }

        private void chk_presupuesto_CheckedChanged(object? sender, EventArgs e)
        {
            if (!chk_presupuesto.Checked)
            {
                txt_impuestos.Enabled = true;
                Pedidos.UpdatePrecios(dg_items, chk_presupuesto, txt_subTotal, txt_impuestos, txt_total, txt_totalO, txt_markup, txt_totalDescuentos, comprobanteSeleccionado, idUsuario, idUnico);
                cmb_comprobante.SelectedValue = comprobanteSeleccionado.IdComprobante;
            }
            else
            {
                txt_impuestos.Text = "0";
                Pedidos.UpdatePrecios(dg_items, chk_presupuesto, txt_subTotal, txt_impuestos, txt_total, txt_totalO, txt_markup, txt_totalDescuentos, comprobanteSeleccionado, idUsuario, idUnico);
                txt_impuestos.Enabled = false;
                cmb_comprobante.SelectedValue = comprobantePresupuesto_default;
            }
            txt_markup_LostFocus(null, null);
        }

        private void updateAndCheck()
        {
            subTotalOriginal = double.TryParse(txt_subTotal.Text, out var st) ? st : 0d;
            bool hasItems = dg_items.Rows.Count > 0;
            cmd_ok.Enabled = hasItems;
            cmd_emitir.Enabled = hasItems;
        }

        private void txt_markup_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_markup_LostFocus(null, null);
            }
        }

        private void txt_markup_KeyPress(object? sender, KeyPressEventArgs e)
        {
            if (!generales.esNumero(e))
            {
                e.KeyChar = '\0';
            }
        }

        private void txt_impuestos_TextChanged(object? sender, EventArgs e)
        {
            if (comprobanteSeleccionado == null)
            {
                return;
            }

            double subtotal = 0;
            double iva = 0;
            double.TryParse(txt_subTotal.Text, NumberStyles.Any, CultureInfo.CurrentCulture, out subtotal);
            double.TryParse(txt_impuestos.Text, NumberStyles.Any, CultureInfo.CurrentCulture, out iva);

            if (comprobanteSeleccionado.IdTipoComprobante != 6)
            {
                txt_total.Text = (subtotal + iva).ToString("N2");
            }
        }

        private void cmb_cliente_SelectionChangeCommitted(object? sender, EventArgs? e)
        {
            if (suspendEventos)
                return;

            if (cmb_cliente.SelectedValue == null)
                return;

            var cliente = clientes.info_cliente(Conversions.ToInteger(cmb_cliente.SelectedValue));
            if (cliente == null || cliente.RazonSocial == "error")
            {
                Interaction.MsgBox("No se pudo obtener el cliente seleccionado.", MsgBoxStyle.Exclamation, "Centrex");
                return;
            }

            checkCustNoTaxNumber(cliente);

            string condicion = (cliente.IdTipoDocumento == 80 && (cliente.EsInscripto ?? false)) ? "IN" : "NOT IN";

            var tiposRestrictivos = new[] { 1, 2, 3, 4, 5, 63, 34, 39, 60, 201, 202, 203, 1011 };
            var tiposSiempre = new[] { 0, 99, 199, 1000, 10001 };

            using (var ctx = new CentrexDbContext())
            {
                var query = ctx.ComprobanteEntity.Where(c => c.Activo);

                if (string.Equals(condicion, "IN", StringComparison.OrdinalIgnoreCase))
                {
                    query = query.Where(c => tiposRestrictivos.Contains(c.IdTipoComprobante) || tiposSiempre.Contains(c.IdTipoComprobante));
                }
                else
                {
                    query = query.Where(c => !tiposRestrictivos.Contains(c.IdTipoComprobante) || tiposSiempre.Contains(c.IdTipoComprobante));
                }

                var comprobantesFiltrados = query.OrderBy(c => c.Comprobante).ToList();
                cmb_comprobante.DataSource = comprobantesFiltrados;
                cmb_comprobante.DisplayMember = "Comprobante";
                cmb_comprobante.ValueMember = "IdComprobante";
            }

            if (estaComprobanteDefault(condicion, comprobanteSeleccionado.IdComprobante))
            {
                cmb_comprobante.SelectedValue = comprobanteSeleccionado.IdComprobante;
            }
            else if (estaComprobanteDefault(condicion, id_comprobante_default))
            {
                cmb_comprobante.SelectedValue = id_comprobante_default;
            }
            else if (cmb_comprobante.Items.Count > 0)
            {
                cmb_comprobante.SelectedIndex = 0;
            }

            cmb_comprobante_SelectionChangeCommitted(null, null);

            var filtros = new Dictionary<string, object>
            {
                ["IdCliente"] = cliente.IdCliente
            };
            var orden = new List<Tuple<string, bool>> { Tuple.Create("Nombre", true) };
            generales.Cargar_Combo(ref cmb_cc, "CcClienteEntity", "Nombre", "IdCc", predet: -1, soloActivos: true, filtros: filtros, orden: orden);

            if (cmb_cc.Items.Count == 0)
            {
                Interaction.MsgBox("Este cliente no tiene ninguna cuenta corriente creada.", MsgBoxStyle.Exclamation, "Centrex");
                cmd_emitir.Enabled = false;
                cmd_ok.Enabled = false;
            }
            else
            {
                cmd_emitir.Enabled = true;
                cmd_ok.Enabled = true;
            }

            var idUltimaCc = Pedidos.Ultima_CC_Pedido_Cliente(cliente.IdCliente);
            if (idUltimaCc == -1)
            {
                cmb_cc.Text = "Seleccione una cuenta corriente...";
            }
            else
            {
                cmb_cc.SelectedValue = idUltimaCc;
            }

            cmb_cc.Enabled = true;
            SendKeys.Send("{TAB}");
            txt_markup_LostFocus(null, null);
        }

        private void cmb_cliente_Leave(object? sender, EventArgs e)
        {
            cmb_cliente_SelectionChangeCommitted(null, null);
        }

        private void cmb_comprobante_SelectionChangeCommitted(object? sender, EventArgs? e)
        {
            if (cmb_comprobante.SelectedValue == null)
                return;

            comprobanteSeleccionado = comprobantes.info_comprobante(Conversions.ToInteger(cmb_comprobante.SelectedValue));

            if (comprobanteSeleccionado == null)
                return;

            if (comprobanteSeleccionado.EsPresupuesto ?? false)
            {
                chk_remitos.Checked = false;
                chk_remitos.Enabled = false;
                chk_presupuesto.Checked = true;
                chk_presupuesto.Enabled = true;
            }
            else
            {
                switch (comprobanteSeleccionado.IdTipoComprobante)
                {
                    case 2:
                    case 3:
                    case 7:
                    case 8:
                    case 12:
                    case 13:
                    case 52:
                    case 53:
                    case 202:
                    case 203:
                    case 207:
                    case 208:
                    case 212:
                    case 213:
                        MostrarPanelComprobanteAsociado(true);
                        dg_items.Enabled = false;
                        ContextMenuStrip1.Enabled = false;
                        cmd_add_item.Enabled = false;
                        cmd_add_descuento.Enabled = false;
                        cmd_addItemManual.Enabled = false;
                        cmd_emitir.Enabled = false;
                        break;
                    default:
                        MostrarPanelComprobanteAsociado(false);
                        dg_items.Enabled = true;
                        ContextMenuStrip1.Enabled = true;
                        cmd_add_item.Enabled = true;
                        cmd_add_descuento.Enabled = true;
                        cmd_addItemManual.Enabled = true;
                        cmd_emitir.Enabled = true;
                        break;
                }

                chk_presupuesto.Checked = false;
                chk_presupuesto.Enabled = false;

                var cliente = clientes.info_cliente(Conversions.ToInteger(cmb_cliente.SelectedValue));
                if (cliente != null)
                {
                    checkCustNoTaxNumber(cliente);
                }
            }
            txt_markup_LostFocus(null, null);
        }

        private void cmb_cc_SelectionChangeCommitted(object? sender, EventArgs e)
        {
            ActiveControl = cmb_comprobante;
        }

        private void cmb_seleccionarComprobanteAnulación_Click(object? sender, EventArgs e)
        {
            if (dg_items.Rows.Count > 0)
            {
                if (Interaction.MsgBox("Ya tiene items cargados en el pedido, si continua, los perderá. ¿Desea continuar?", MsgBoxStyle.Question | MsgBoxStyle.YesNo, "Centrex") == MsgBoxResult.No)
                {
                    return;
                }
            }

            Pedidos.BorrarTablaPedidosTemporales(usuario_logueado.IdUsuario, idUnico);

            var cliente = clientes.info_cliente(Conversions.ToInteger(cmb_cliente.SelectedValue));
            var cmp = comprobantes.info_comprobante(Conversions.ToInteger(cmb_comprobante.SelectedValue));

            var tmpTabla = tabla;
            tabla = "anulaComprobanteAFIP";

            var buscaComprobante = new search(cliente, cmp);
            buscaComprobante.ShowDialog();

            tabla = tmpTabla;

            if (id == 0)
            {
                return;
            }

            numeroPedido_anulado = id;
            Pedidos.pedido_a_pedidoTmp(numeroPedido_anulado, usuario_logueado.IdUsuario, idUnico);
            txt_comprobanteAsociado.Text = Pedidos.info_pedido(numeroPedido_anulado)?.NumeroComprobante?.ToString();

            edicion = true;
            LoadGridItems().GetAwaiter().GetResult();
            txt_markup_LostFocus(null, null);

            cmb_cliente.Enabled = false;
            cmb_cc.Enabled = false;
            cmb_comprobante.Enabled = false;

            dg_items.Enabled = true;
            ContextMenuStrip1.Enabled = true;
            cmd_add_item.Enabled = true;
            cmd_add_descuento.Enabled = true;
            cmd_addItemManual.Enabled = true;
            edicion = false;
        }

        private void cmd_recargaprecios_Click(object? sender, EventArgs e)
        {
            RecargarPrecioToolStripMenuItem_Click(sender, e);
        }

        private void checkCustNoTaxNumber(ClienteEntity c)
        {
            switch (comprobanteSeleccionado.IdTipoComprobante)
            {
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                case 7:
                case 8:
                case 9:
                case 10:
                case 11:
                case 12:
                case 13:
                case 15:
                case 34:
                case 35:
                case 39:
                case 40:
                case 49:
                case 51:
                case 52:
                case 53:
                case 54:
                case 60:
                case 61:
                case 63:
                case 64:
                    if (string.IsNullOrWhiteSpace(c.TaxNumber))
                    {
                        cmd_emitir.Enabled = false;
                        lbl_noTaxNumber.Visible = true;
                    }
                    else
                    {
                        cmd_emitir.Enabled = true;
                        lbl_noTaxNumber.Visible = false;
                    }
                    break;
            }
        }
    }
}
