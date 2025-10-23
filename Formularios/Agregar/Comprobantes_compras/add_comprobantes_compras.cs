using System;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace Centrex
{
    public partial class add_comprobantes_compras
    {
        private int id_comprobante_compra = -1;
        private bool cerrarOk = false;

        public add_comprobantes_compras()
        {
            InitializeComponent();
        }

        private void add_comprobantes_compras_Load(object sender, EventArgs e)
        {
            string sqlstr;

            lbl_fechaCarga.Text = generales.Hoy();


            // form = Me ' Comentado para evitar error de compilación

            // Cargo el combo con todos los proveedores
            sqlstr = "SELECT p.id_proveedor AS 'id_proveedor', p.razon_social AS 'razon_social' FROM proveedores AS p WHERE p.activo = '1' ORDER BY p.razon_social ASC";
            var argcombo = cmb_proveedor;
            generales.Cargar_Combo(ref argcombo, sqlstr, VariablesGlobales.basedb, "razon_social", Conversions.ToInteger("id_proveedor"));
            cmb_proveedor = argcombo;
            cmb_proveedor.Text = "Seleccione un proveedor...";
            cmb_cc.Enabled = false;

            // Cargo el combo con todos los comprobantes
            cmb_tipoComprobante.Enabled = false;
            cmb_tipoComprobante.Text = "Seleccione un comprobante...";

            // Cargo el combo con todas las condiciones de compra
            sqlstr = "SELECT id_condicion_compra, condicion FROM condiciones_compra ORDER BY condicion ASC";
            var argcombo1 = cmb_condicionCompra;
            generales.Cargar_Combo(ref argcombo1, sqlstr, VariablesGlobales.basedb, "condicion", Conversions.ToInteger("id_condicion_compra"));
            cmb_condicionCompra = argcombo1;
            cmb_condicionCompra.SelectedValue = VariablesGlobales.id_condicion_compra_default;
            // cmb_condicionCompra.Text = "Seleccione una condición de compra..."

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
            string sqlstr;
            int p;
            int id_ultima_cc;

            p = Conversions.ToInteger(cmb_proveedor.SelectedValue);

            // Cargo los combos con todas las cuentas corrientes del proveedor
            sqlstr = "SELECT id_cc, nombre FROM cc_proveedores WHERE activo = 1 AND id_proveedor = '" + p.ToString() + "' ORDER BY nombre ASC";
            var argcombo = cmb_cc;
            generales.Cargar_Combo(ref argcombo, sqlstr, VariablesGlobales.basedb, "nombre", Conversions.ToInteger("id_cc"));
            cmb_cc = argcombo;

            // Cargo el combo con todos los comprobantes disponibles para el proveedor
            cmb_tipoComprobante.Enabled = true;
            sqlstr = "DECLARE @id_claseFiscal AS INTEGER " + "SELECT @id_claseFiscal = id_claseFiscal " + "FROM proveedores " + "WHERE id_proveedor = '" + p.ToString() + "' " + "SELECT id_tipoComprobante, comprobante_AFIP " + "FROM tipos_comprobantes " + "WHERE id_claseFiscal LIKE '%' + CAST(@id_claseFiscal AS VARCHAR(5)) + '%' " + "OR id_tipoComprobante IN (1000, 1001)";
            // "WHERE CHARINDEX(@id_claseFiscal, id_claseFiscal) > 0 "
            var argcombo1 = cmb_tipoComprobante;
            generales.Cargar_Combo(ref argcombo1, sqlstr, VariablesGlobales.basedb, "comprobante_AFIP", Conversions.ToInteger("id_tipoComprobante"));
            cmb_tipoComprobante = argcombo1;
            cmb_tipoComprobante.Text = "Seleccione un tipo de comprobante...";


            // Selecciono la última cuenta corriente que se uso del cliente
            id_ultima_cc = comprobantes_compras.Ultima_CC_comprobante_compra_proveedor(p);
            if (id_ultima_cc == -1)
            {
                cmb_cc.Text = "Seleccione una cuenta corriente...";
            }
            else
            {
                cmb_cc.SelectedValue = id_ultima_cc;
                cmb_cc_SelectionChangeCommitted(null, null);
            }
            cmb_cc.Enabled = true;
        }

        private void cmb_cc_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string sqlstr;
            ccProveedor ccp;


            ccp = ccProveedores.info_ccProveedor(Conversions.ToInteger(cmb_cc.SelectedValue));

            // Cargo el combo con todas las monedas disponibles y selecciono la moneda de la cuenta corriente del proveedor seleccionado
            sqlstr = "SELECT id_moneda, moneda FROM sysMoneda ORDER BY moneda ASC";
            var argcombo = cmb_moneda;
            generales.Cargar_Combo(ref argcombo, sqlstr, VariablesGlobales.basedb, "moneda", Conversions.ToInteger("id_moneda"));
            cmb_moneda = argcombo;
            cmb_moneda.SelectedValue = ccp.id_moneda;
            cmb_moneda.Enabled = false;
            if (ccp.id_moneda != VariablesGlobales.ID_PESO)
            {
                txt_tasaCambio.Enabled = true;
            }
            else
            {
                txt_tasaCambio.Text = "0";
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

            // activo = True

            // agregaitem = True
            Enabled = false;

            var frmSearch = new search(true, id_comprobante_compra);
            frmSearch.ShowDialog();
            VariablesGlobales.tabla = tmpTabla;
            VariablesGlobales.activo = tmpActivo;

            // Select Case tbl_comprobantesCompras.SelectedTab.Name
            // Case "productos"

            // Case "impuestos"

            // Case "conceptos"

            // End Select
            // agregaitem = False

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
            string sqlstr;
            var totalItems = default(double);
            var totalImpuestos = default(double);
            var totalConceptos = default(double);
            double totalFactura;

            // Actualizo el datagrid con los productos
            sqlstr = "SELECT CONCAT(cci.id_comprobanteCompraItem, '-', cci.id_item) AS 'ID', i.item AS 'Código', i.descript AS 'Producto', cci.cantidad AS 'Cantidad' " + ", cci.precio AS 'Subtotal', (cci.cantidad * cci.precio) AS 'Total' " + "FROM comprobantes_compras_items AS cci " + "INNER JOIN items AS i ON cci.id_item = i.id_item " + "WHERE cci.id_comprobanteCompra = '" + id_comprobante_compra.ToString() + "' " + "ORDER BY cci.id_comprobanteCompraItem ASC";
            var argdataGrid = dg_viewItems;
            int argnRegs = 0;
            int argtPaginas = 0;
            TextBox argtxtnPage = null;
            generales.cargar_datagrid(ref argdataGrid, sqlstr, VariablesGlobales.basedb, 0, ref argnRegs, ref argtPaginas, 1, ref argtxtnPage, "", "");
            dg_viewItems = argdataGrid;

            // Sumo el precio de todos los productos del datagrid
            foreach (DataGridViewRow row in dg_viewItems.Rows)
                totalItems += Conversions.ToDouble(row.Cells["Total"].Value.ToString());
            txt_totalItems.Text = totalItems.ToString();

            // Actualizo el datagrid con los impuestos
            sqlstr = "SELECT CONCAT(cci.id_comprobanteCompraImpuesto, '-', cci.id_impuesto) AS 'ID', i.nombre AS 'Impuesto', cci.importe AS 'Importe' " + "FROM comprobantes_compras_impuestos AS cci " + "INNER JOIN impuestos AS i ON cci.id_impuesto = i.id_impuesto " + "WHERE cci.id_comprobanteCompra = '" + id_comprobante_compra.ToString() + "' " + "ORDER BY cci.id_comprobanteCompraImpuesto ASC";
            var argdataGrid1 = dg_viewImpuestos;
            int argnRegs1 = 0;
            int argtPaginas1 = 0;
            TextBox argtxtnPage1 = null;
            generales.cargar_datagrid(ref argdataGrid1, sqlstr, VariablesGlobales.basedb, 0, ref argnRegs1, ref argtPaginas1, 1, ref argtxtnPage1, "", "");
            dg_viewImpuestos = argdataGrid1;

            // Sumo el precio de todos los impuestos del datagrid
            foreach (DataGridViewRow row in dg_viewImpuestos.Rows)
                totalImpuestos += Conversions.ToDouble(row.Cells["Importe"].Value.ToString());
            txt_totalImpuestos.Text = totalImpuestos.ToString();

            // Actualizo el datagrid con los conceptos de compra
            sqlstr = "SELECT CONCAT(ccc.id_comprobanteCompraConcepto, '-', ccc.id_concepto_compra) AS 'ID', cc.concepto AS 'Concepto', " + "ccc.subtotal AS 'Subtotal', ccc.iva AS 'I.V.A.', ccc.total AS 'Total' " + "FROM comprobantes_compras_conceptos AS ccc " + "INNER JOIN conceptos_compra AS cc ON ccc.id_concepto_compra = cc.id_concepto_compra " + "WHERE ccc.id_comprobanteCompra = '" + id_comprobante_compra.ToString() + "' " + "ORDER BY ccc.id_comprobanteCompraConcepto ASC";
            var argdataGrid2 = dg_viewConceptos;
            int argnRegs2 = 0;
            int argtPaginas2 = 0;
            TextBox argtxtnPage2 = null;
            generales.cargar_datagrid(ref argdataGrid2, sqlstr, VariablesGlobales.basedb, 0, ref argnRegs2, ref argtPaginas2, 1, ref argtxtnPage2, "", "");
            dg_viewConceptos = argdataGrid2;

            // Sumo el precio de todos los conceptos de compra del datagrid
            foreach (DataGridViewRow row in dg_viewConceptos.Rows)
                totalConceptos += Conversions.ToDouble(row.Cells["Total"].Value.ToString());
            txt_totalConceptos.Text = totalConceptos.ToString();


            totalFactura = totalItems + totalImpuestos + totalConceptos;
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
    }
}

