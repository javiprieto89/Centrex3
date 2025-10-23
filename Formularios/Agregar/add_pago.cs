using System;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace Centrex
{
    public partial class add_pago
    {
        private proveedor p = new proveedor();
        private ccProveedor cc = new ccProveedor();
        private string selColName = "Seleccionado";
        private double total = default;
        private double efectivo = default;
        private double transferenciaBancaria = default;
        private double totalCh = default;
        private int[] chSel;
        private bool noCambiar = false;

        public add_pago()
        {
            InitializeComponent();
        }
        // Private chSelSearch() As Integer

        private void add_pago_Load(object sender, EventArgs e)
        {
            string sqlstr;

            // form = Me ' Comentado para evitar error de compilación

            // Cargo el combo con todos los proveedores
            sqlstr = "SELECT p.id_proveedor AS 'id_proveedor', p.razon_social AS 'razon_social' FROM proveedores AS p WHERE p.activo = '1' ORDER BY p.razon_social ASC";
            var argcombo = cmb_proveedor;
            generales.Cargar_Combo(ref argcombo, sqlstr, VariablesGlobales.basedb, "razon_social", Conversions.ToInteger("id_proveedor"));
            cmb_proveedor = argcombo;
            cmb_proveedor.Text = "Seleccione un proveedor...";
            cmb_cc.Enabled = false;

            resetForm();
            actualizarDataGrid();
            lbl_fecha.Text = generales.Hoy();
            // form = Me ' Comentado para evitar error de compilación
        }

        private void add_pago_FormClosed(object sender, FormClosedEventArgs e)
        {
            closeandupdate(this);
        }

        private void cmb_proveedor_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string sqlstr;

            // 
            // MUESTRA LAS FACTURAS PENDIENTES DE ABONAR POR EL CLIENTE
            // chk_efectivo.Enabled = True
            // chk_transferenciaBancaria.Enabled = True
            // chk_cheque.Enabled = True
            // chklb_facturasPendientes.Enabled = True

            // sqlstr = "SELECT p.id_pedido AS 'id_pedido', CONCAT(c.comprobante, ' - Nº ',  p.numeroComprobante) AS 'numeroComprobante'" &
            // "FROM pedidos AS p " &
            // "INNER JOIN comprobantes AS c ON p.id_comprobante = c.id_comprobante " &
            // "WHERE p.id_cliente = 6 AND c.id_tipoComprobante IN (1, 6, 11, 51) " &
            // "ORDER BY p.numeroComprobante ASC"

            // cargar_checkListBox(chklb_facturasPendientes, cs, sqlstr, "id_pedido", "numeroComprobante")
            // MUESTRA LAS FACTURAS PENDIENTES DE ABONAR POR EL CLIENTE
            // 

            string p = cmb_proveedor.SelectedValue.ToString();

            // sqlstr = "SELECT id_cc, nombre FROM cc_proveedores WHERE activo = 1 AND id_proveedor = '" + p + "' ORDER BY nombre ASC"
            // cargar_combo(cmb_cc, sqlstr, basedb, "nombre", "id_cc")
            // cmb_cc.Enabled = True

            // Cargo los combos con todas las cuentas corrientes del proveedor
            sqlstr = "SELECT id_cc, nombre FROM cc_proveedores WHERE activo = 1 AND id_proveedor = '" + p + "' ORDER BY nombre ASC";
            var argcombo = cmb_cc;
            generales.Cargar_Combo(ref argcombo, sqlstr, VariablesGlobales.basedb, "nombre", Conversions.ToInteger("id_cc"));
            cmb_cc = argcombo;
            cmb_cc.Enabled = true;

            chk_efectivo.Enabled = true;
            chk_transferencia.Enabled = true;
            chk_cheque.Enabled = true;
            dg_view_nFC_importes.Enabled = true;
            txt_notas.Enabled = true;
            // c = info_cliente(cmb_cliente.SelectedValue)
            cmb_cc_SelectionChangeCommitted(null, null);
            // actualizarDataGrid(p)
            actualizarDataGrid();
            resetForm();
        }

        private void chk_efectivo_CheckedChanged(object sender, EventArgs e)
        {
            if (Conversions.ToDouble(txt_efectivo.Text) != 0d)
                efectivo = Convert.ToDouble(txt_efectivo.Text);

            if (chk_efectivo.Checked)
            {
                total += efectivo;
            }
            else
            {
                total -= efectivo;
            }

            txt_efectivo.Enabled = chk_efectivo.Checked;
            lbl_importePago.Text = total.ToString();
        }

        // Private Sub chk_transferenciaBancaria_CheckedChanged(sender As Object, e As EventArgs)
        // If txt_transferenciaBancaria.Text <> 0 Then transferenciaBancaria = Convert.ToDouble(txt_transferenciaBancaria.Text)

        // If chk_transferencia.Checked Then
        // total += transferenciaBancaria
        // Else
        // total -= transferenciaBancaria
        // End If

        // txt_transferenciaBancaria.Enabled = chk_transferencia.Checked
        // End Sub

        private void chk_cheque_CheckedChanged(object sender, EventArgs e)
        {
            bool chk;
            chk = chk_cheque.Checked;

            if (chk)
            {
                total += totalCh;
                lbl_totalCh.Text = Strings.FormatCurrency(totalCh.ToString());
                lbl_importePago.Text = Strings.FormatCurrency(total.ToString());
            }
            else
            {
                total -= totalCh;
                lbl_totalCh.Text = Strings.FormatCurrency("0");
                lbl_importePago.Text = Strings.FormatCurrency(total.ToString());
            }

            dg_viewCH.Enabled = chk;
            cmd_addCheques.Enabled = chk;
            cmd_verCheques.Enabled = chk;
            txt_search.Enabled = chk;
            lbl_borrarbusqueda.Enabled = chk;
        }

        private void cmb_cc_SelectionChangeCommitted(object sender, EventArgs e)
        {
            cc = ccProveedores.info_ccProveedor(Conversions.ToInteger(cmb_cc.SelectedValue));

            lbl_dineroCuenta.Text = "$ " + cc.saldo.ToString();
            if (cc.saldo < 0d)
            {
                lbl_dineroCuenta.ForeColor = Color.Red;
            }
            else
            {
                lbl_dineroCuenta.ForeColor = Color.Green;
            }
        }

        private void cmd_addCheques_Click(object sender, EventArgs e)
        {
            var addCheque = new add_cheque(-1, Conversions.ToInteger(cmb_proveedor.SelectedValue));
            addCheque.ShowDialog();
            // actualizarDataGrid(cmb_proveedor.SelectedValue.ToString)
            actualizarDataGrid();
        }

        private void cmd_verCheques_Click(object sender, EventArgs e)
        {
            var frm = new frmCheques(Conversions.ToInteger(cmb_proveedor.SelectedValue));
            frm.ShowDialog();
        }

        private void cmd_ok_Click(object sender, EventArgs e)
        {
            var pg = new pago();
            var ccp = new ccProveedor();

            if (cmb_proveedor.Text == "Seleccione un proveedor...")
            {
                Interaction.MsgBox("El campo 'Proveedor' es obligatorio y está vacio", Constants.vbOKOnly, Constants.vbExclamation);
                return;
                if (!chk_efectivo.Checked & !chk_transferencia.Checked & !chk_cheque.Checked)
                {
                    Interaction.MsgBox("Debe elegir algún medio de pago", Constants.vbOKOnly, Constants.vbExclamation);
                    return;
                }
            }

            ccp = ccProveedores.info_ccProveedor(Conversions.ToInteger(cmb_cc.SelectedValue));

            // .fecha_pago = lbl_fecha.Text
            pg.fecha_pago = dtp_fechaPago.Value.Date.ToString();
            pg.IdProveedor = Conversions.ToInteger(cmb_proveedor.SelectedValue);
            pg.id_cc = Conversions.ToInteger(cmb_cc.SelectedValue);
            pg.dineroEnCc = ccp.saldo;

            if (chk_efectivo.Checked)
            {
                pg.efectivo = Conversions.ToDouble(txt_efectivo.Text);
            }
            else
            {
                pg.efectivo = 0d;
            }

            // If chk_transferencia.Checked Then
            // .totalTransferencia = txt_transferenciaBancaria.Text
            // Else
            // .totalTransferencia = 0
            // End If

            if (chk_transferencia.Checked)
            {
                pg.hayTransferencia = true;
                pg.totalTransferencia = sumaTransferencias();
            }
            else
            {
                pg.hayTransferencia = false;
                pg.totalTransferencia = 0d;
            }

            if (chk_cheque.Checked)
            {
                pg.hayCheque = true;
                pg.totalch = sumaCheques();
            }
            else
            {
                pg.hayCheque = false;
                pg.totalch = 0d;
            }

            // .hayCheque = chk_cheque.Checked
            // .aplicaFc = txt_aplicaFc.Text
            pg.notas = txt_notas.Text;

            pg.total = pg.total + sumaCheques() + sumaTransferencias();

            pg.id_pago = pagos.addpago(pg);
            if (pg.id_pago != -1)
            {
                if (pg.hayCheque)
                {
                    int count = -1;

                    foreach (DataGridViewRow row in dg_viewCH.Rows)
                    {
                        if (Convert.ToBoolean(row.Cells[selColName].Value))
                            count += 1;
                    }
                    var cheques = new int[count + 1];
                    count = 0;

                    var ch = new cheque();
                    foreach (DataGridViewRow row in dg_viewCH.Rows)
                    {
                        if (Convert.ToBoolean(row.Cells[selColName].Value))
                        {
                            cheques[count] = Conversions.ToInteger(row.Cells["ID"].Value);
                            // Actualizo el estado del cheque
                            ch = Centrex.cheques.info_cheque(cheques[count].ToString());
                            ch.id_estadoch = VariablesGlobales.ID_CH_ENTREGADO;
                            Centrex.cheques.updatech(ch);
                            count += 1;
                        }
                    }

                    pagos.add_chequePagado(pg.id_pago, cheques);
                }

                if (pg.hayTransferencia)
                {
                    if (!guardarTransferencias(pg))
                    {
                        Interaction.MsgBox("Hubo un problema al agregar el cobro.", (MsgBoxStyle)((int)Constants.vbExclamation + (int)Constants.vbOKOnly), "Centrex");
                        closeandupdate(this);
                    }
                }

                if (!pagos.guardar_pagos_facturas_importes(pg.id_pago, dg_view_nFC_importes))
                {
                    Interaction.MsgBox("Hubo un problema al agregar el pago.", (MsgBoxStyle)((int)Constants.vbExclamation + (int)Constants.vbOKOnly), "Centrex");
                    closeandupdate(this);
                }

                // Actualizo el saldo del proveedor
                // Dim p As New proveedor

                ccp.saldo -= pg.total;
                ccProveedores.updateCCProveedor(ccp);

                // Dim frm As New frm_prnReportes("rpt_ordenDePago", "datos_empresa", "SP_pago_cabecera", "SP_detalle_pagos_cheques", "SP_detalle_pagos_transferencias", "DS_Datos_Empresa",
                // "DS_Pago_Cabecera", "DS_Detalle_Pagos_Cheques", "DS_Detalle_Pagos_Transferencias", pg.id_pago, True)
                // frm.ShowDialog()
                var rptOrdenDePago = new frm_prnOrdenDePago(pg.id_pago);
                rptOrdenDePago.ShowDialog();
                closeandupdate(this);
            }
            else
            {
                Interaction.MsgBox("Hubo un problema al agregar el cobro.", Constants.vbExclamation);
                closeandupdate(this);
            }
        }

        private void txt_efectivo_Leave(object sender, EventArgs e)
        {
            total -= efectivo;
            efectivo = Convert.ToDouble(txt_efectivo.Text);
            total += efectivo;

            lbl_importePago.Text = "$ " + Convert.ToString(total);
        }

        // Private Sub txt_transferenciaBancaria_Leave(sender As Object, e As EventArgs)
        // total -= transferenciaBancaria
        // transferenciaBancaria = Convert.ToDouble(txt_transferenciaBancaria.Text)
        // total += transferenciaBancaria

        // lbl_importePago.Text = "$ " + Convert.ToString(total)
        // End Sub

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

        private void cmb_proveedor_KeyPress(object sender, KeyPressEventArgs e)
        {
            // e.KeyChar = ""
        }

        private void cmb_cc_KeyPress(object sender, KeyPressEventArgs e)
        {
            // e.KeyChar = ""
        }

        private void lbl_borrarbusqueda_DoubleClick(object sender, EventArgs e)
        {
            txt_search.Text = "";
            // actualizarDataGrid(cmb_proveedor.SelectedValue.ToString)
            actualizarDataGrid();
        }

        // Private Sub actualizarDataGrid(ByVal p As String)
        private void actualizarDataGrid()
        {
            string sqlstr = "";
            string txtsearch = "";
            int count = 0;

            if (!string.IsNullOrEmpty(txt_search.Text))
            {
                txtsearch = Strings.Replace(txt_search.Text, " ", "%");

                checkCheques();

                sqlstr = "SELECT ch.id_cheque AS 'ID', CASE WHEN ch.id_cliente IS NULL THEN '**** CHEQUE PROPIO ****' ELSE c.razon_social END AS 'Cliente', b.nombre AS 'Banco', ch.nCheque AS 'Nº cheque', ch.importe AS 'Importe', sech.estado AS 'Estado', " + "CASE WHEN ch.id_cuentaBancaria IS NULL THEN 'No' ELSE CONCAT('Si, en:', cbb.nombre, ' - ', cb.nombre) END AS '¿Depositado?', " + "CASE WHEN ch.activo = 1 THEN 'Si' ELSE 'No' END AS '¿Activo?' " + "FROM cheques AS ch " + "LEFT JOIN clientes AS c ON ch.id_cliente = c.id_cliente " + "INNER JOIN bancos AS b ON ch.id_banco = b.id_banco " + "LEFT JOIN cuentas_bancarias AS cb ON ch.id_cuentaBancaria = cb.id_cuentaBancaria " + "LEFT JOIN bancos AS cbb ON cb.id_banco = cbb.id_banco " + "INNER JOIN sysestados_cheques AS sech ON ch.id_estadoch = sech.id_estadoch " + "WHERE ch.activo = 1 AND ch.id_estadoch = 1  " + "AND (ch.id_cheque LIKE '%" + txtsearch + "%' " + "OR b.nombre LIKE '%" + txtsearch + "%' " + "OR ch.nCheque LIKE '%" + txtsearch + "%' " + "OR ch.importe LIKE '%" + txtsearch + "%' " + "OR sech.estado LIKE '%" + txtsearch + "%') " + "ORDER BY ch.nCheque ASC"; // id_estadoch = 1 = En cartera
            }
            else
            {
                checkCheques();

                // MUESTRA LOS CHEQUES EN CARTERA DISPONIBLES PARA USAR PARA PAGAR A UN PROVEEDOR

                sqlstr = "SELECT ch.id_cheque AS 'ID', CASE WHEN ch.id_cliente IS NULL THEN '**** CHEQUE PROPIO ****' ELSE c.razon_social END AS 'Cliente', b.nombre AS 'Banco', ch.nCheque AS 'Nº cheque', ch.importe AS 'Importe', sech.estado AS 'Estado', " + "CASE WHEN ch.id_cuentaBancaria IS NULL THEN 'No' ELSE CONCAT('Si, en:', cbb.nombre, ' - ', cb.nombre) END AS '¿Depositado?', " + "CASE WHEN ch.activo = 1 THEN 'Si' ELSE 'No' END AS '¿Activo?' " + "FROM cheques AS ch " + "LEFT JOIN clientes AS c ON ch.id_cliente = c.id_cliente " + "INNER JOIN bancos AS b ON ch.id_banco = b.id_banco " + "LEFT JOIN cuentas_bancarias AS cb ON ch.id_cuentaBancaria = cb.id_cuentaBancaria " + "LEFT JOIN bancos AS cbb ON cb.id_banco = cbb.id_banco " + "INNER JOIN sysestados_cheques AS sech ON ch.id_estadoch = sech.id_estadoch " + "WHERE ch.activo = 1 AND ch.id_estadoch = 1  " + "ORDER BY ch.nCheque ASC"; // id_estadoch = 1 = En cartera
            }

            if (!string.IsNullOrEmpty(sqlstr) & sqlstr != "error")
            {
                int nRegs = 0;
                int tPaginas = 0;
                var txtnPage = new TextBox();
                var argdataGrid = dg_viewCH;
                generales.cargar_datagrid(ref argdataGrid, sqlstr, VariablesGlobales.basedb, 0, ref nRegs, ref tPaginas, 1, ref txtnPage, "cheques", "cheques");
                dg_viewCH = argdataGrid;
                selCheques();
            }

            // If chSel IsNot Nothing Then
            // For Each idCheque As Integer In chSel
            // totalCh += info_cheque(idCheque.ToString).importe
            // Next
            // End If

            // total += totalCh
            lbl_importePago.Text = "$ " + Convert.ToString(total);
        }

        private void txt_search_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                // actualizarDataGrid(cmb_proveedor.SelectedValue.ToString)
                actualizarDataGrid();
            }
        }

        private void dg_view_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dg_viewCH.IsCurrentCellDirty)
            {
                dg_viewCH.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void dg_view_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            int count = 0;

            if (noCambiar)
                return;

            total -= totalCh;
            totalCh = 0d;

            checkCheques();

            // For Each idCheque As Integer In chSel
            // totalCh += info_cheque(idCheque.ToString).importe
            // Next

            // For Each row As DataGridViewRow In dg_view.Rows
            // If (Convert.ToBoolean(row.Cells(selColName).Value)) Then
            // totalCh += Convert.ToDouble(row.Cells("Importe").Value)
            // End If
            // Next

            if (chSel is not null)
            {
                foreach (int a in chSel)
                    totalCh += (double)Centrex.cheques.info_cheque(a.ToString()).importe;
            }

            total += totalCh;
            lbl_totalCh.Text = "$ " + Convert.ToString(totalCh);
            lbl_importePago.Text = "$ " + Convert.ToString(total);
        }

        private void checkCheques()
        {
            // Reviso el datagridview y si el cheque esta seleccionado lo agrego a chSel (Cheques seleccionados)
            // Si el cheque no está seleccionado lo borro de chSel
            // Si el cheque ya está agregado en chSel, no lo vuelve a agregar
            // Si el cheque no está en chSel no hace nada

            foreach (DataGridViewRow row in dg_viewCH.Rows)
            {
                if (Convert.ToBoolean(row.Cells[selColName].Value))
                {
                    chSel = generales.addValArray(ref chSel, Conversions.ToInteger(row.Cells["ID"].Value));
                }
                else if (chSel is not null)
                {
                    chSel = generales.delValArray(ref chSel, Conversions.ToInteger(row.Cells["ID"].Value));
                }
            }
        }

        private void selCheques()
        {
            int c = 0;
            noCambiar = true;

            foreach (DataGridViewRow row in dg_viewCH.Rows)
            {
                if (generales.searchArray(chSel, Conversions.ToInteger(row.Cells["ID"].Value)))
                {
                    row.Cells[selColName].Value = true;
                }
            }

            noCambiar = false;
        }

        private void resetForm()
        {
            total = 0d;
            efectivo = 0d;
            transferenciaBancaria = 0d;
            totalCh = 0d;
            chSel = null;
            noCambiar = false;

            chk_efectivo.Checked = false;
            chk_transferencia.Checked = false;
            chk_cheque.Checked = false;

            txt_efectivo.Text = efectivo.ToString();
            // txt_transferenciaBancaria.Text = transferenciaBancaria
            lbl_totalTransferencia.Text = Strings.FormatCurrency(transferenciaBancaria);
            txt_search.Text = "";
            lbl_totalCh.Text = "$ " + Convert.ToString(totalCh);
            lbl_importePago.Text = "$ " + Convert.ToString(total);
            cargarDGTransferencias();
        }

        private void pic_searchCCProveedor_Click(object sender, EventArgs e)
        {
            // busqueda
            string tmp;
            proveedor tmpProveedor;

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

        private void chk_transferencia_CheckedChanged(object sender, EventArgs e)
        {
            bool chk;
            chk = chk_transferencia.Checked;

            if (chk)
            {
                total += transferenciaBancaria;
                lbl_totalTransferencia.Text = Strings.FormatCurrency(transferenciaBancaria.ToString());
                lbl_importePago.Text = Strings.FormatCurrency(total.ToString());
            }
            else
            {
                total -= transferenciaBancaria;
                lbl_totalTransferencia.Text = Strings.FormatCurrency("0");
                lbl_importePago.Text = Strings.FormatCurrency(total.ToString());
            }

            txt_searchTransferencia.Enabled = chk;
            dg_viewTransferencia.Enabled = chk;
            cmd_addTransferencia.Enabled = chk;
        }
        private void cargarDGTransferencias()
        {
            string sqlstr;

            sqlstr = "SELECT t.id_tmpTransferencia AS 'ID', b.nombre AS 'Banco', cb.nombre AS 'Cuenta Bancaria', CAST(t.fecha AS VARCHAR(50)) AS 'Fecha', " + "t.total AS 'Total', t.notas AS 'Notas' " + "FROM tmptransferencias AS t " + "INNER JOIN cuentas_bancarias AS cb ON t.id_cuentaBancaria = cb.id_cuentaBancaria " + "INNER JOIN bancos AS b ON cb.id_banco = b.id_banco";

            // Carga el datagrid con los nuevos datos
            int nRegs = 0;
            int tPaginas = 0;
            var txtnPage = new TextBox();
            var argdataGrid = dg_viewTransferencia;
            generales.cargar_datagrid(ref argdataGrid, sqlstr, VariablesGlobales.basedb, 0, ref nRegs, ref tPaginas, 1, ref txtnPage, "tmptransferencias", "tmptransferencias");
            dg_viewTransferencia = argdataGrid;
        }
        private double sumaTransferencias()
        {
            // Obtengo el total de las transferencias
            var suma = default(double);

            foreach (DataGridViewRow row in dg_viewTransferencia.Rows)
                suma = Conversions.ToDouble(suma + row.Cells["Total"].Value);

            return suma;
        }

        private void actualizaTransferencias()
        {
            cargarDGTransferencias();

            total -= transferenciaBancaria;
            transferenciaBancaria = sumaTransferencias();
            total += transferenciaBancaria;

            lbl_totalTransferencia.Text = Strings.FormatCurrency(transferenciaBancaria);
            lbl_importePago.Text = Strings.FormatCurrency(total);
        }

        private void dg_viewTransferencia_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string seleccionado = dg_viewTransferencia.CurrentRow.Cells[0].Value.ToString();
            VariablesGlobales.edita_transferencia = InfoTmpTransferencia(seleccionado);

            if (VariablesGlobales.edita_transferencia.id_transferencia == -1)
            {
                Interaction.MsgBox("Ocurrió un problema al editar la transferencia.", (MsgBoxStyle)((int)Constants.vbExclamation + (int)Constants.vbOKOnly), "Centrex");
                return;
            }

            if (VariablesGlobales.borrado == false)
                VariablesGlobales.edicion = true;

            My.MyProject.Forms.add_transferencia.ShowDialog();

            actualizaTransferencias();

            VariablesGlobales.edicion = false;
        }

        private void cmd_addTransferencia_Click(object sender, EventArgs e)
        {
            // Dim t As New transferencia

            My.MyProject.Forms.add_transferencia.ShowDialog();

            actualizaTransferencias();
        }

        private double sumaCheques()
        {
            // Obtengo el total de los cheques
            var suma = default(double);

            foreach (DataGridViewRow row in dg_viewCH.Rows)
            {
                if (Convert.ToBoolean(row.Cells[selColName].Value))
                {
                    suma = Conversions.ToDouble(suma + row.Cells["Importe"].Value);
                }
            }

            return suma;
        }

        private void dg_view_nFC_importes_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dg_view_nFC_importes.CurrentCell.ColumnIndex == 2) // Numeric column with decimal point
            {
                ((TextBox)e.Control).KeyPress += dg_view_nFC_importes_textbox_keyPress;
            }
        }

        private void dg_view_nFC_importes_textbox_keyPress(object sender, KeyPressEventArgs e)
        {
            // Allows Numeric values, one decimal point and BackSpace key
            TextBox numbers = (TextBox)sender;
            if (Strings.InStr("1234567890.", Conversions.ToString(e.KeyChar)) == 0 & Strings.Asc(e.KeyChar) != 8 | Conversions.ToString(e.KeyChar) == "." & Strings.InStr(numbers.Text, ".") > 0)
            {
                e.KeyChar = '\0';
                e.Handled = true;
            }
        }

        private void dg_viewCH_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            int count = 0;

            if (noCambiar)
                return;

            total -= totalCh;
            totalCh = 0d;

            checkCheques();

            // For Each idCheque As Integer In chSel
            // totalCh += info_cheque(idCheque.ToString).importe
            // Next

            // For Each row As DataGridViewRow In dg_view.Rows
            // If (Convert.ToBoolean(row.Cells(selColName).Value)) Then
            // totalCh += Convert.ToDouble(row.Cells("Importe").Value)
            // End If
            // Next

            if (chSel is not null)
            {
                foreach (int a in chSel)
                    totalCh += (double)Centrex.cheques.info_cheque(a.ToString()).importe;
            }

            total += totalCh;
            lbl_totalCh.Text = Strings.FormatCurrency(totalCh);
            lbl_importePago.Text = Strings.FormatCurrency(total);
        }

        private void dg_viewCH_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string seleccionado = dg_viewTransferencia.CurrentRow.Cells[0].Value.ToString();
            VariablesGlobales.edita_cheque = Centrex.cheques.info_cheque(seleccionado);
            if (VariablesGlobales.borrado == false)
                VariablesGlobales.edicion = true;

            My.MyProject.Forms.add_cheque.ShowDialog();

            actualizarDataGrid();
            VariablesGlobales.edicion = false;
        }


        private void dg_viewCH_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dg_viewCH.IsCurrentCellDirty)
            {
                dg_viewCH.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

    }
}

