using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;

namespace Centrex
{
    public partial class add_comprobantes_compras
    {
        public int id_comprobante_compra = -1;
        public bool cerrarOk = false;

        public add_comprobantes_compras()
        {
            InitializeComponent();
        }

        private void add_comprobantes_compras_Load(object sender, EventArgs e)
        {
            lbl_fechaCarga.Text = generales.Hoy();

            var ordenProveedores = OrdenAsc("RazonSocial");
            var argProveedor = cmb_proveedor;
            generales.Cargar_Combo(
                ref argProveedor,
                entidad: "ProveedorEntity",
                displaymember: "RazonSocial",
                valuemember: "IdProveedor",
                predet: -1,
                soloActivos: true,
                filtros: null,
                orden: ordenProveedores);
            cmb_proveedor = argProveedor;
            cmb_proveedor.SelectedIndex = -1;
            cmb_proveedor.Text = "Seleccione un proveedor...";
            cmb_cc.DataSource = null;
            cmb_cc.Items.Clear();
            cmb_cc.Enabled = false;

            cmb_tipoComprobante.DataSource = null;
            cmb_tipoComprobante.Items.Clear();
            cmb_tipoComprobante.Enabled = false;
            cmb_tipoComprobante.Text = "Seleccione un tipo de comprobante...";

            var ordenCondiciones = OrdenAsc("Condicion");
            var argCondicion = cmb_condicionCompra;
            generales.Cargar_Combo(
                ref argCondicion,
                entidad: "CondicionCompraEntity",
                displaymember: "Condicion",
                valuemember: "IdCondicionCompra",
                predet: -1,
                soloActivos: true,
                filtros: null,
                orden: ordenCondiciones);
            cmb_condicionCompra = argCondicion;

            if (cmb_condicionCompra.Items.Count > 0)
            {
                try
                {
                    cmb_condicionCompra.SelectedValue = VariablesGlobales.id_condicion_compra_default;
                }
                catch
                {
                    cmb_condicionCompra.SelectedIndex = -1;
                }
            }
            else
            {
                cmb_condicionCompra.SelectedIndex = -1;
            }

            txt_tasaCambio.Enabled = false;

            ActiveControl = dtp_fechaComprobanteCompra;

            // cmb_proveedor.SelectedValue = 3
            // cmb_proveedor_SelectionChangeCommitted(Nothing, Nothing)
            // cmb_cc.SelectedValue = 1016
            // cmb_cc_SelectionChangeCommitted(Nothing, Nothing)
            // cmb_condicionCompra.SelectedValue = 1
            // cmb_moneda.SelectedValue = 1
            // txt_puntoVenta.Text = "1"
            // txt_numeroComprobante.Text = "22"
            // txt_CAE.Text = "5454654654"
        }

        private void cmb_proveedor_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cmb_proveedor.SelectedValue is null)
                return;

            int proveedorId = Conversions.ToInteger(cmb_proveedor.SelectedValue);

            var filtrosCc = new Dictionary<string, object>
            {
                ["IdProveedor"] = proveedorId
            };

            var argCc = cmb_cc;
            generales.Cargar_Combo(
                ref argCc,
                entidad: "CcProveedorEntity",
                displaymember: "Nombre",
                valuemember: "IdCc",
                predet: -1,
                soloActivos: true,
                filtros: filtrosCc,
                orden: OrdenAsc("Nombre"));
            cmb_cc = argCc;

            cmb_cc.Enabled = cmb_cc.Items.Count > 0;
            if (!cmb_cc.Enabled)
            {
                cmb_cc.Text = "Seleccione una cuenta corriente...";
            }
            else if (cmb_cc.SelectedIndex < 0)
            {
                cmb_cc.SelectedIndex = 0;
            }

            CargarTiposComprobantesParaProveedor(proveedorId);
            cmb_tipoComprobante.Enabled = true;

            int idUltimaCc = comprobantes_compras.Ultima_CC_comprobante_compra_proveedor(proveedorId);
            if (idUltimaCc == -1)
            {
                cmb_cc.SelectedIndex = -1;
                cmb_cc.Text = "Seleccione una cuenta corriente...";
            }
            else
            {
                try
                {
                    cmb_cc.SelectedValue = idUltimaCc;
                    cmb_cc_SelectionChangeCommitted(null, null);
                }
                catch
                {
                    cmb_cc.SelectedIndex = -1;
                }
            }
        }

        private void cmb_cc_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cmb_cc.SelectedValue is null)
                return;

            var ccp = ccProveedores.info_ccProveedor(Conversions.ToInteger(cmb_cc.SelectedValue));

            var ordenMoneda = OrdenAsc("Moneda");
            var argMoneda = cmb_moneda;
            generales.Cargar_Combo(
                ref argMoneda,
                entidad: "SysMonedaEntity",
                displaymember: "Moneda",
                valuemember: "IdMoneda",
                predet: -1,
                soloActivos: false,
                filtros: null,
                orden: ordenMoneda);
            cmb_moneda = argMoneda;

            try
            {
                cmb_moneda.SelectedValue = ccp.IdMoneda;
            }
            catch
            {
                cmb_moneda.SelectedIndex = -1;
            }

            cmb_moneda.Enabled = false;
            if (ccp.IdMoneda != VariablesGlobales.ID_PESO)
            {
                txt_tasaCambio.Enabled = true;
            }
            else
            {
                txt_tasaCambio.Enabled = false;
                txt_tasaCambio.Text = "0";
            }
        }

        private void CargarTiposComprobantesParaProveedor(int idProveedor)
        {
            cmb_tipoComprobante.DataSource = null;
            cmb_tipoComprobante.Items.Clear();

            try
            {
                using var ctx = new CentrexDbContext();
                int? claseFiscal = ctx.ProveedorEntity
                    .AsNoTracking()
                    .Where(p => p.IdProveedor == idProveedor)
                    .Select(p => p.IdClaseFiscal)
                    .FirstOrDefault();

                var tipos = ctx.TipoComprobanteEntity
                    .AsNoTracking()
                    .Where(tc =>
                        (claseFiscal.HasValue && tc.IdClaseFiscal != null && tc.IdClaseFiscal.Contains(claseFiscal.Value.ToString())) ||
                        tc.IdTipoComprobante == 1000 ||
                        tc.IdTipoComprobante == 1001)
                    .OrderBy(tc => tc.ComprobanteAfip)
                    .Select(tc => new
                    {
                        tc.IdTipoComprobante,
                        tc.ComprobanteAfip
                    })
                    .ToList();

                cmb_tipoComprobante.DataSource = tipos;
                cmb_tipoComprobante.DisplayMember = "ComprobanteAfip";
                cmb_tipoComprobante.ValueMember = "IdTipoComprobante";
                cmb_tipoComprobante.SelectedIndex = -1;
                cmb_tipoComprobante.Text = "Seleccione un tipo de comprobante...";
                cmb_tipoComprobante.Enabled = cmb_tipoComprobante.Items.Count > 0;
            }
            catch (Exception ex)
            {
                cmb_tipoComprobante.DataSource = null;
                cmb_tipoComprobante.Items.Clear();
                cmb_tipoComprobante.Text = "Seleccione un tipo de comprobante...";
                cmb_tipoComprobante.Enabled = false;
                Interaction.MsgBox($"Error al cargar tipos de comprobante: {ex.Message}", (MsgBoxStyle)((int)Constants.vbExclamation + (int)Constants.vbOKOnly), "Centrex");
            }
        }

        private void pic_proveedorProveedor_Click(object sender, EventArgs e)
        {
            // busqueda
            string tmp;
            tmp = VariablesGlobales.tabla;
            VariablesGlobales.tabla = "proveedores";
            Enabled = false;
            My.MyProject.Forms.search.ShowDialog();
            VariablesGlobales.tabla = tmp;

            // Establezco la opción del combo, si es 0 elijo el cliente default
            if (VariablesGlobales.id == 0)
                VariablesGlobales.id = VariablesGlobales.id_cliente_pedido_default;
            var argcmb = cmb_proveedor;
            generales.updateform(VariablesGlobales.id.ToString(), ref argcmb);
            cmb_proveedor = argcmb;
        }

        private void pic_searchCCProveedor_Click(object sender, EventArgs e)
        {
            // busqueda
            string tmp;
            ProveedorEntity tmpProveedor;

            if (Conversions.ToBoolean(Operators.OrObject(cmb_proveedor.Text == "Seleccione un proveedor...", Operators.ConditionalCompareObjectEqual(cmb_proveedor.SelectedValue, 0, false))))
                return;

            tmp = VariablesGlobales.tabla;
            tmpProveedor = VariablesGlobales.edita_proveedor;
            // VariablesGlobales.edita_cliente = info_cliente(cmb_proveedor.SelectedValue)
            VariablesGlobales.edita_proveedor = proveedores.info_proveedor(Conversions.ToString(cmb_proveedor.SelectedValue));
            VariablesGlobales.tabla = "cc_proveedores";
            Enabled = false;

            My.MyProject.Forms.search.ShowDialog();
            VariablesGlobales.tabla = tmp;
            VariablesGlobales.edita_proveedor = tmpProveedor;

            // Establezco la opción del combo, si es 0 elijo el cliente default
            if (VariablesGlobales.id == 0)
                VariablesGlobales.id = VariablesGlobales.id_cliente_pedido_default;
            var argcmb = cmb_cc;
            generales.updateform(VariablesGlobales.id.ToString(), ref argcmb);
            cmb_cc = argcmb;
        }

        private void cmd_agregar_Click(object sender, EventArgs e)
        {
            string tmpTabla;
            bool tmpActivo;

            tmpTabla = VariablesGlobales.tabla;
            tmpActivo = VariablesGlobales.activo;

            switch (tbl_comprobantesCompras.SelectedTab.Name ?? "")
            {
                case "productos":
                    {
                        VariablesGlobales.tabla = "items_sinDescuento";
                        VariablesGlobales.activo = true;
                        break;
                    }
                case "impuestos":
                    {
                        VariablesGlobales.tabla = "impuestos";
                        break;
                    }
                case "conceptos":
                    {
                        VariablesGlobales.tabla = "conceptos_compra";
                        break;
                    }
            }

            Enabled = false;

            var frmSearch = new search(true, id_comprobante_compra);
            frmSearch.ShowDialog();
            VariablesGlobales.tabla = tmpTabla;
            VariablesGlobales.activo = tmpActivo;
            Enabled = true;

            int seleccionado = VariablesGlobales.id;
            if (seleccionado > 0)
            {
                switch (tbl_comprobantesCompras.SelectedTab.Name ?? string.Empty)
                {
                    case "productos":
                        {
                            VariablesGlobales.edita_item = mitem.info_item(seleccionado.ToString());
                            VariablesGlobales.agregaitem = true;
                            var agregaItemFrm = new infoagregaitem(true, id_comprobante_compra);
                            agregaItemFrm.ShowDialog();
                            VariablesGlobales.agregaitem = false;
                            VariablesGlobales.edita_item = new item();
                            break;
                        }
                    case "impuestos":
                        {
                            var frmImpuestos = new add_comprobantes_compras_impuestos(id_comprobante_compra, seleccionado);
                            frmImpuestos.ShowDialog();
                            break;
                        }
                    case "conceptos":
                        {
                            var frmConceptos = new add_comprobantes_compras_conceptos(id_comprobante_compra, seleccionado);
                            frmConceptos.ShowDialog();
                            break;
                        }
                }
            }

            VariablesGlobales.agregaitem = false;
            VariablesGlobales.id = 0;
            update_form();
        }

        private void cmd_confirmar_Click(object sender, EventArgs e)
        {
            var cc = new comprobante_compra();
            int id_cc;

            if (cmb_proveedor.Text == "Seleccione un proveedor...")
            {
                Interaction.MsgBox("Debe seleccionar un proveedor.", (MsgBoxStyle)((int)Constants.vbExclamation + (int)Constants.vbOKOnly), "Centrex");
                return;
            }
            else if (cmb_cc.Text == "Seleccione una cuenta corriente...")
            {
                Interaction.MsgBox("Debe seleccionar una cuenta corriente.", (MsgBoxStyle)((int)Constants.vbExclamation + (int)Constants.vbOKOnly), "Centrex");
                return;
            }
            else if (cmb_condicionCompra.Text == "Seleccione una condición de compra...")
            {
                Interaction.MsgBox("Debe seleccionar una condición de compra.", (MsgBoxStyle)((int)Constants.vbExclamation + (int)Constants.vbOKOnly), "Centrex");
                return;
            }
            else if (Conversions.ToBoolean(Operators.AndObject(Operators.ConditionalCompareObjectNotEqual(cmb_moneda.SelectedValue, VariablesGlobales.ID_PESO, false), string.IsNullOrEmpty(txt_tasaCambio.Text))))
            {
                Interaction.MsgBox("La moneda seleccionada es extranjera, por lo cual escribir la tasa de cambio", (MsgBoxStyle)((int)Constants.vbExclamation + (int)Constants.vbOKOnly), "Centrex");
                return;
            }
            else if (cmb_tipoComprobante.Text == "Seleccione un tipo de comprobante...")
            {
                Interaction.MsgBox("Debe seleccionar un comprobante.", (MsgBoxStyle)((int)Constants.vbExclamation + (int)Constants.vbOKOnly), "Centrex");
                return;
            }

            cc.fecha_comprobante = dtp_fechaComprobanteCompra.Value.Date.ToShortDateString();
            cc.IdProveedor = Conversions.ToInteger(cmb_proveedor.SelectedValue);
            cc.id_cc = Conversions.ToInteger(cmb_cc.SelectedValue);
            cc.id_condicion_compra = Conversions.ToInteger(cmb_condicionCompra.SelectedValue);
            cc.id_tipoComprobante = Conversions.ToInteger(cmb_tipoComprobante.SelectedValue);
            cc.id_moneda = Conversions.ToInteger(cmb_moneda.SelectedValue);
            cc.tasaCambio = Conversions.ToDouble(txt_tasaCambio.Text);
            cc.puntoVenta = txt_puntoVenta.Text;
            cc.numeroComprobante = txt_numeroComprobante.Text;
            cc.cae = txt_CAE.Text;

            if (id_comprobante_compra == -1)
            {
                id_cc = comprobantes_compras.add_comprobante_compra(cc);
                if (id_cc == -1)
                {
                    Interaction.MsgBox("Hubo un error al dar de alta el comprobante, consulte con el programador.", (MsgBoxStyle)((int)Constants.vbCritical + (int)Constants.vbOKOnly), "Centrex");
                    return;
                }

                id_comprobante_compra = id_cc;
            }
            else
            {
                cc.id_comprobanteCompra = id_comprobante_compra;
                if (!comprobantes_compras.update_comprobante_compra(cc))
                {
                    Interaction.MsgBox("Ocurrió un error al actualizar el comprobante de compra, consulte con el programador.", (MsgBoxStyle)((int)Constants.vbCritical + (int)Constants.vbOKOnly), "Centrex");
                    return;
                }
            }

            dtp_fechaComprobanteCompra.Enabled = false;
            cmb_proveedor.Enabled = false;
            cmb_cc.Enabled = false;
            cmb_condicionCompra.Enabled = false;
            cmb_moneda.Enabled = false;
            txt_tasaCambio.Enabled = false;
            cmb_tipoComprobante.Enabled = false;
            txt_puntoVenta.Enabled = false;
            txt_numeroComprobante.Enabled = false;
            txt_CAE.Enabled = false;
            pic_searchProveedor.Enabled = false;
            pic_searchCondicionCompra.Enabled = false;
            pic_searchCCProveedor.Enabled = false;

            tbl_comprobantesCompras.Enabled = true;
            cmd_confirmar.Enabled = false;
            cmd_editar.Enabled = true;
            cmd_agregar.Enabled = true;
            cmd_ok.Enabled = true;

            update_form();
        }

        private void cmd_exit_Click(object sender, EventArgs e)
        {
            preguntar_antesDe_salir();
        }

        private void preguntar_antesDe_salir()
        {
            if (Interaction.MsgBox("Si sale sin guardar los cambios perderá todo lo guardado." + Constants.vbCr + "¿Desea salir?", (MsgBoxStyle)((int)Constants.vbQuestion + (int)Constants.vbYesNo), "Centrex") == Constants.vbYes)
            {
                if (id_comprobante_compra == -1)
                {
                    closeandupdate(this);
                    return;
                }
                // Borrar los comprobantes de compras que no esten activos y todos los productos, impuestos y conceptos asociados a esa orden
                comprobantes_compras.borrar_comprobantes_compras_activos(id_comprobante_compra);
                closeandupdate(this);
                return;
            }
        }


        public void update_form()
        {
            if (id_comprobante_compra == -1)
            {
                dg_viewItems.DataSource = null;
                dg_viewImpuestos.DataSource = null;
                dg_viewConceptos.DataSource = null;
                txt_totalItems.Text = "0";
                txt_totalImpuestos.Text = "0";
                txt_totalConceptos.Text = "0";
                txt_totalFactura.Text = "0";
                return;
            }

            decimal totalItems = 0m;
            decimal totalImpuestos = 0m;
            decimal totalConceptos = 0m;

            using var ctx = new CentrexDbContext();

            var items = ctx.ComprobanteCompraItemEntity
                .AsNoTracking()
                .Include(i => i.IdItemNavigation)
                .Where(i => i.IdComprobanteCompra == id_comprobante_compra)
                .OrderBy(i => i.IdComprobanteCompraItem)
                .Select(i => new
                {
                    ID = $"{i.IdComprobanteCompraItem}-{i.IdItem}",
                    Codigo = i.IdItemNavigation.Item,
                    Producto = i.IdItemNavigation.Descript ?? string.Empty,
                    Cantidad = i.Cantidad,
                    Subtotal = i.Precio,
                    Total = i.Cantidad * i.Precio
                })
                .ToList();

            dg_viewItems.DataSource = items;
            totalItems = items.Sum(row => row.Total);
            txt_totalItems.Text = totalItems.ToString();
            if (dg_viewItems.Columns.Contains("Codigo"))
                dg_viewItems.Columns["Codigo"].HeaderText = "Código";
            if (dg_viewItems.Columns.Contains("Subtotal"))
                dg_viewItems.Columns["Subtotal"].HeaderText = "Subtotal";

            var impuestos = ctx.ComprobanteCompraImpuestoEntity
                .AsNoTracking()
                .Include(i => i.IdImpuestoNavigation)
                .Where(i => i.IdComprobanteCompra == id_comprobante_compra)
                .OrderBy(i => i.IdComprobanteCompraImpuesto)
                .Select(i => new
                {
                    ID = $"{i.IdComprobanteCompraImpuesto}-{i.IdImpuesto}",
                    Impuesto = i.IdImpuestoNavigation.Nombre,
                    Importe = i.Importe
                })
                .ToList();

            dg_viewImpuestos.DataSource = impuestos;
            totalImpuestos = impuestos.Sum(row => row.Importe);
            txt_totalImpuestos.Text = totalImpuestos.ToString();

            var conceptos = ctx.ComprobanteCompraConceptoEntity
                .AsNoTracking()
                .Include(c => c.IdConceptoCompraNavigation)
                .Where(c => c.IdComprobanteCompra == id_comprobante_compra)
                .OrderBy(c => c.IdComprobanteCompraConcepto)
                .Select(c => new
                {
                    ID = $"{c.IdComprobanteCompraConcepto}-{c.IdConceptoCompra}",
                    Concepto = c.IdConceptoCompraNavigation.Concepto,
                    Subtotal = c.Subtotal,
                    IVA = c.Iva,
                    Total = c.Total
                })
                .ToList();

            dg_viewConceptos.DataSource = conceptos;
            totalConceptos = conceptos.Sum(row => row.Total);
            txt_totalConceptos.Text = totalConceptos.ToString();
            if (dg_viewConceptos.Columns.Contains("IVA"))
                dg_viewConceptos.Columns["IVA"].HeaderText = "I.V.A.";

            decimal totalFactura = totalItems + totalImpuestos + totalConceptos;
            txt_totalFactura.Text = totalFactura.ToString();
        }

        private void cmd_editar_Click(object sender, EventArgs e)
        {
            dtp_fechaComprobanteCompra.Enabled = true;
            cmb_proveedor.Enabled = true;
            cmb_cc.Enabled = true;
            cmb_condicionCompra.Enabled = true;
            cmb_moneda.Enabled = true;
            txt_tasaCambio.Enabled = true;
            cmb_tipoComprobante.Enabled = true;
            txt_puntoVenta.Enabled = true;
            txt_numeroComprobante.Enabled = true;
            txt_CAE.Enabled = true;
            pic_searchProveedor.Enabled = true;
            pic_searchCondicionCompra.Enabled = true;
            pic_searchCCProveedor.Enabled = true;

            tbl_comprobantesCompras.Enabled = false;
            cmd_confirmar.Enabled = true;
            cmd_editar.Enabled = false;
            cmd_agregar.Enabled = false;
            cmd_ok.Enabled = false;
        }

        private void add_comprobantes_compras_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!cerrarOk)
            {
                // cmd_exit_Click(Nothing, Nothing)
                // closeandupdate(Me)
                preguntar_antesDe_salir();
            }
        }

        private void cmd_ok_Click(object sender, EventArgs e)
        {
            var cc = new comprobante_compra();
            var ccp = new ccProveedor();
            var tc = new TipoComprobante(Conversions.ToInteger(cmb_tipoComprobante.SelectedValue));

            if (txt_totalFactura.Text == "0" | string.IsNullOrEmpty(txt_totalFactura.Text))
            {
                Interaction.MsgBox("Carge algún producto, impuesto o concepto para poder guardar.", (MsgBoxStyle)((int)Constants.vbExclamation + (int)Constants.vbOKOnly), "Centrex");
                return;
            }

            ccp = ccProveedores.info_ccProveedor(Conversions.ToInteger(cmb_cc.SelectedValue));
            cc = comprobantes_compras.info_comprobante_compra(id_comprobante_compra.ToString());
            cc.id_comprobanteCompra = id_comprobante_compra;
            cc.subtotal = Conversions.ToDouble(txt_totalItems.Text);
            cc.impuestos = Conversions.ToDouble(txt_totalImpuestos.Text);
            cc.conceptos = Conversions.ToDouble(txt_totalConceptos.Text);
            cc.total = Conversions.ToDouble(txt_totalFactura.Text);
            cc.nota = txt_notas.Text;

            if (!comprobantes_compras.cerrar_comprobante_compra(cc))
            {
                Interaction.MsgBox("Hubo un problema al guardar el comprobante de compra, consulte con el programador.", (MsgBoxStyle)((int)Constants.vbCritical + (int)Constants.vbOKOnly), "Centrex");
                closeandupdate(this);
                return;
            }
            else
            {
                if (Conversions.ToString(tc.signoProveedor) == "+")
                {
                    ccp.saldo += cc.total;
                }
                else
                {
                    ccp.saldo += cc.total;
                }
                ccProveedores.updateCCProveedor(ccp);
                // Dim frm As New frm_prnReportes("rpt_ordenDePago", "datos_empresa", "SP_pago_cabecera", "SP_detalle_pagos_cheques", "SP_detalle_pagos_transferencias", "DS_Datos_Empresa",
                // "DS_Pago_Cabecera", "DS_Detalle_Pagos_Cheques", "DS_Detalle_Pagos_Transferencias", id_comprobante_compra, True)
                // frm.ShowDialog()
                // Se agrega la operación a la tabla transacciones
                var t = new transaccion();
                t.id_comprobanteCompra = id_comprobante_compra;
                t.fecha = cc.fecha_comprobante;
                // t.id_tipoComprobante = info_comprobante(p.id_comprobante).id_tipoComprobante
                t.id_tipoComprobante = cc.id_tipoComprobante;
                t.numeroComprobante = cc.numeroComprobante;
                t.puntoVenta = Conversions.ToInteger(cc.puntoVenta);
                t.total = cc.total;
                t.id_cc = cc.id_cc;
                t.IdProveedor = cc.IdProveedor;
                if (!transacciones.Agregar_Transaccion_Desde_Comprobante_Compra(t))
                {
                    Interaction.MsgBox("Ha ocurrido un error al agregar la transaccion en el proveedor, verifique el mismo o vuelva a intentarlo más tarde.", Constants.vbExclamation, "Centrex");
                    return;
                }
                var rptOrdenDePago = new frm_prnOrdenDePago(id_comprobante_compra);
                rptOrdenDePago.Show();
            }

            cerrarOk = true;
            closeandupdate(this);
        }

        private void dg_viewItems_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string seleccionado;

            seleccionado = dg_viewItems.CurrentRow.Cells[0].Value.ToString();
            seleccionado = Strings.Right(seleccionado, Strings.Len(seleccionado) - Strings.InStr(seleccionado, "-"));
        }

        private void pic_searchCondicionCompra_Click(object sender, EventArgs e)
        {
            // busqueda
            string tmp;
            tmp = VariablesGlobales.tabla;
            VariablesGlobales.tabla = "BY";
            Enabled = false;
            My.MyProject.Forms.search.ShowDialog();
            VariablesGlobales.tabla = tmp;

            // Establezco la opción del combo, si es 0 elijo el cliente default
            if (VariablesGlobales.id == 0)
                VariablesGlobales.id = VariablesGlobales.id_cliente_pedido_default;
            var argcmb = cmb_proveedor;
            generales.updateform(VariablesGlobales.id.ToString(), ref argcmb);
            cmb_proveedor = argcmb;
        }

        private void pic_searchTipoComprobante_Click(object sender, EventArgs e)
        {
            // busqueda
            string tmp;
            tmp = VariablesGlobales.tabla;
            VariablesGlobales.tabla = "tipos_Comprobantes";
            Enabled = false;
            My.MyProject.Forms.search.ShowDialog();
            VariablesGlobales.tabla = tmp;

            // Establezco la opción del combo, si es 0 elijo el cliente default
            if (VariablesGlobales.id == 0)
                VariablesGlobales.id = VariablesGlobales.id_cliente_pedido_default;
            var argcmb = cmb_proveedor;
            generales.updateform(VariablesGlobales.id.ToString(), ref argcmb);
            cmb_proveedor = argcmb;
        }

        private static List<Tuple<string, bool>> OrdenAsc(string columna) =>
            new List<Tuple<string, bool>> { Tuple.Create(columna, true) };
    }
}
