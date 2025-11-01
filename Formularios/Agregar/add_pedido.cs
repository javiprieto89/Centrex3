using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Centrex
{
    public partial class add_pedido : Form
    {
        private ComprobanteEntity comprobanteSeleccionado;
        private decimal markupOriginal = -1;
        private int idUsuario;
        private Guid idUnico;
        private bool historico;

        public add_pedido()
        {
            InitializeComponent();
        }

        public add_pedido(Guid _idUnico)
        {
            InitializeComponent();
            idUnico = _idUnico;
        }

        private void add_pedido_Load(object sender, EventArgs e)
        {
            if (idUnico == Guid.Empty)
                idUnico = Guid.NewGuid();

            idUsuario = usuario_logueado.IdUsuario;
            historico = activo;
            activo = true;

            // ==== Cargar combos ====
            generales.Cargar_Combo(
                ref cmb_cliente,
                entidad: "ClienteEntity",
                displaymember: "RazonSocial",
                valuemember: "IdCliente",
                predet: -1,
                soloActivos: true
            );

            generales.Cargar_Combo(
                ref cmb_comprobante,
                entidad: "ComprobanteEntity",
                displaymember: "Comprobante",
                valuemember: "IdComprobante",
                predet: id_comprobante_default,
                soloActivos: true
            );

            cmb_cliente.SelectedIndex = -1;
            cmb_cliente.Text = "Seleccione un cliente...";
            cmb_comprobante.SelectedValue = id_comprobante_default;
            comprobanteSeleccionado = comprobantes.InfoComprobante(id_comprobante_default);

            lbl_date.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txt_total.Text = "0,00";
            txt_subTotal.Text = "0,00";
            txt_impuestos.Text = "0,00";
            txt_totalO.Text = txt_total.Text;

            // Inicializar grilla vacía
            LoadGridItems().GetAwaiter().GetResult();

            cmd_ok.Enabled = false;
        }

        private async Task LoadGridItems()
        {
            var query = from t in new CentrexDbContext().TmpPedidoItemEntity
                        where t.IdUsuario == idUsuario && t.IdUnico == idUnico && t.Activo == true
                        join i in new CentrexDbContext().ItemEntity on t.IdItem equals i.IdItem into it
                        from i in it.DefaultIfEmpty()
                        select new
                        {
                            ID = t.IdTmpPedidoItem,
                            Producto = i != null ? i.Descript : t.Descript,
                            Cantidad = t.Cantidad,
                            Precio = t.Precio,
                            Subtotal = t.Cantidad * t.Precio
                        };

            var data = query.ToList();

            var result = new DataGridQueryResult
            {
                EsMaterializada = true,
                DataMaterializada = data
            };

            await LoadDataGridDynamic.LoadDataGridWithEntityAsync(dg_items, result);
        }

        // ============================================================================================
        // === Botones principales ===================================================================
        // ============================================================================================

        private void cmd_add_item_Click(object sender, EventArgs e)
        {
            if (dg_items.Rows.Count >= (comprobanteSeleccionado?.MaxItems ?? 999))
            {
                Interaction.MsgBox("No se pueden agregar más ítems por comprobante.", MsgBoxStyle.Exclamation, "Centrex");
                return;
            }

            var tmpActivo = activo;
            activo = true;
            agregaitem = true;
            Enabled = false;

            var frm = new search(idUsuario, idUnico);
            frm.ShowDialog();

            activo = tmpActivo;
            agregaitem = false;
            Enabled = true;

            Pedidos.UpdatePrecios(dg_items, chk_presupuesto, txt_subTotal, txt_impuestos, txt_total, txt_totalO, txt_markup, txt_totalDescuentos, comprobanteSeleccionado, idUsuario, idUnico);
            updateAndCheck();
        }

        private void cmd_add_descuento_Click(object sender, EventArgs e)
        {
            if (dg_items.Rows.Count >= (comprobanteSeleccionado?.MaxItems ?? 999))
            {
                Interaction.MsgBox("No se pueden agregar más ítems por comprobante.", MsgBoxStyle.Exclamation, "Centrex");
                return;
            }

            var tmpActivo = activo;
            activo = true;
            agregaitem = true;
            Enabled = false;

            var frm = new search(idUsuario, idUnico, "itemsDescuentos");
            frm.ShowDialog();

            activo = tmpActivo;
            agregaitem = false;
            Enabled = true;

            Pedidos.UpdatePrecios(dg_items, chk_presupuesto, txt_subTotal, txt_impuestos, txt_total, txt_totalO, txt_markup, txt_totalDescuentos, comprobanteSeleccionado, idUsuario, idUnico);
            txt_markup_LostFocus(null, null);
        }

        private void cmd_addItemManual_Click(object sender, EventArgs e)
        {
            agregaitem = true;
            var addItemManual = new add_itemManual(idUsuario, idUnico);
            addItemManual.ShowDialog();
            agregaitem = false;

            Pedidos.UpdatePrecios(dg_items, chk_presupuesto, txt_subTotal, txt_impuestos, txt_total, txt_totalO, txt_markup, txt_totalDescuentos, comprobanteSeleccionado, idUsuario, idUnico);
            updateAndCheck();
        }

        private void cmd_exit_Click(object sender, EventArgs e)
        {
            closeandupdate(this);
        }

        private void cmd_ok_Click(object sender, EventArgs e)
        {
            // Guardar o actualizar pedido
            using (var ctx = new CentrexDbContext())
            {
                if (cmb_cliente.SelectedValue == null || cmb_comprobante.SelectedValue == null)
                {
                    Interaction.MsgBox("Debe seleccionar cliente y comprobante.", MsgBoxStyle.Exclamation, "Centrex");
                    return;
                }

                var pedido = new PedidoEntity
                {
                    IdCliente = (int)cmb_cliente.SelectedValue,
                    IdComprobante = (int)cmb_comprobante.SelectedValue,
                    Fecha = DateOnly.FromDateTime(DateTime.Now),
                    Subtotal = decimal.Parse(txt_subTotal.Text),
                    Iva = decimal.Parse(txt_impuestos.Text),
                    Total = decimal.Parse(txt_total.Text),
                    Markup = decimal.Parse(txt_markup.Text),
                    Activo = true
                };

                ctx.PedidoEntity.Add(pedido);
                ctx.SaveChanges();

                Pedidos.GuardarPedido(idUsuario, idUnico, pedido.IdPedido);
                Pedidos.BorrarTablaPedidosTemporales(idUsuario, idUnico);

                Interaction.MsgBox("Pedido guardado correctamente.", MsgBoxStyle.Information, "Centrex");
                closeandupdate(this);
            }
        }

        private void cmd_emitir_Click(object sender, EventArgs e)
        {
            cmd_ok_Click(sender, e);
        }

        // ============================================================================================
        // === Eventos de grilla ======================================================================
        // ============================================================================================

        private void dg_items_DoubleClick(object sender, EventArgs e)
        {
            if (dg_items.CurrentRow == null) return;

            var idTmp = Convert.ToInt32(dg_items.CurrentRow.Cells["ID"].Value);
            using (var ctx = new CentrexDbContext())
            {
                var tmp = ctx.TmpPedidoItemEntity.FirstOrDefault(t => t.IdTmpPedidoItem == idTmp);
                if (tmp == null) return;

                var item = ctx.ItemEntity.FirstOrDefault(i => i.IdItem == tmp.IdItem);
                if (item == null) return;

                if (item.EsDescuento || item.EsMarkup) return;

                edita_item = item;
                edita_item.IdTmpPedidoItem = tmp.IdTmpPedidoItem;

                var infoItemAgregado = new infoagregaitem(idUsuario, idUnico);
                infoItemAgregado.ShowDialog();
            }

            Pedidos.UpdatePrecios(dg_items, chk_presupuesto, txt_subTotal, txt_impuestos, txt_total, txt_totalO, txt_markup, txt_totalDescuentos, comprobanteSeleccionado, idUsuario, idUnico);
            txt_markup_LostFocus(null, null);
        }

        private void EditarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dg_items_DoubleClick(null, null);
        }

        private void BorrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dg_items.Rows.Count == 0) return;

            var idTmp = Convert.ToInt32(dg_items.CurrentRow.Cells["ID"].Value);
            Pedidos.BorrarItemCargado(idTmp);

            Pedidos.UpdatePrecios(dg_items, chk_presupuesto, txt_subTotal, txt_impuestos, txt_total, txt_totalO, txt_markup, txt_totalDescuentos, comprobanteSeleccionado, idUsuario, idUnico);
            txt_markup_LostFocus(null, null);
        }

        private void RecargarPrecioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dg_items.Rows.Count == 0) return;

            var idTmp = Convert.ToInt32(dg_items.CurrentRow.Cells["ID"].Value);
            using (var ctx = new CentrexDbContext())
            {
                var tmp = ctx.TmpPedidoItemEntity.FirstOrDefault(t => t.IdTmpPedidoItem == idTmp);
                if (tmp != null)
                    Pedidos.RecargaPrecios(-1, (tmp.IdItem ?? 0), "TmpPedidoItemEntity");
            }

            Pedidos.UpdatePrecios(dg_items, chk_presupuesto, txt_subTotal, txt_impuestos, txt_total, txt_totalO, txt_markup, txt_totalDescuentos, comprobanteSeleccionado, idUsuario, idUnico);
            txt_markup_LostFocus(null, null);
        }

        private void dg_items_MouseDown(object sender, MouseEventArgs e)
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

        // ============================================================================================
        // === Lógica de Markup / Presupuesto =========================================================
        // ============================================================================================

        private void txt_markup_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_markup.Text))
                txt_markup.Text = "0";

            cmd_add_descuento.Enabled = txt_markup.Text == "0";

            Pedidos.UpdatePrecios(dg_items, chk_presupuesto, txt_subTotal, txt_impuestos, txt_total, txt_totalO, txt_markup, txt_totalDescuentos, comprobanteSeleccionado, idUsuario, idUnico);

            decimal markup = decimal.Parse(txt_markup.Text);
            markupOriginal = markup;

            decimal total = Pedidos.CalculoTotalPuro(dg_items);
            decimal descuento = Pedidos.CalcularDescuento(total, markup);

            txt_totalDescuentos.Text = descuento.ToString("N2");
        }

        private void chk_presupuesto_CheckedChanged(object sender, EventArgs e)
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
            txt_markup_LostFocus(null, null);

            bool hasItems = dg_items.Rows.Count > 0;
            cmd_ok.Enabled = hasItems;
        }
    }
}

