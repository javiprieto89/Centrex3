using System;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace Centrex
{
    public partial class frm_depositarCH
    {
        private int desde_cartera;
        private int pagina_cartera;
        private int nRegs_cartera;
        private int tPaginas_cartera;
        private ColumnClickEventArgs orderCol_cartera = null;
        // Dim fecha_desde_cartera As Date
        // Dim fecha_hasta_cartera As Date

        private int desde_depositado;
        private int pagina_depositado;
        private int nRegs_depositado;
        private int tPaginas_depositado;
        private ColumnClickEventArgs orderCol_depositado = null;

        public frm_depositarCH()
        {
            InitializeComponent();
        }

        // *******************************
        // * id_estadoch   *   Estado    *
        // *******************************
        // *  1           *   En cartera *
        // *  2           *   Entregado  *
        // *  3           *   Cobrado    *
        // *  4           *   Rechazado  *
        // *******************************

        private void frm_depositarCH_Load(object sender, EventArgs e)
        {
            // fecha_desde_cartera = Today()
            // fecha_hasta_cartera = Date.MaxValue

            var argcombo = cmb_banco;
            generales.Cargar_Combo(ref argcombo, "SELECT id_banco, nombre FROM bancos WHERE activo = '1' ORDER BY nombre ASC", VariablesGlobales.basedb, "nombre", Conversions.ToInteger("id_banco"));
            cmb_banco = argcombo;

            cmb_banco.Text = "Seleccione un banco...";
            cmb_cuentaBancaria.Text = "Seleccione una cuenta bancaria...";
            chk_desdeSiempre_cartera.Checked = true;
            dtp_desde_cartera.Value = dtp_desde_cartera.MinDate;
            chk_hastaSiempre_cartera.Checked = true;
            dtp_hasta_cartera.Value = DateTime.Today.Date;

            cmb_cuentaBancaria.Enabled = false;
            // cmd_depositar.Enabled = False
            // cmd_acreditar.Enabled = False
            // cmd_anular.Enabled = False


            actualizarDatagrid_cartera();

        }

        private void actualizarDatagrid_cartera()
        {
            string sqlstr;
            DateTime fecha_desde;
            DateTime fecha_hasta;

            if (chk_desdeSiempre_cartera.Checked)
            {
                fecha_desde = dtp_desde_cartera.Value;
            }
            else
            {
                fecha_desde = DateTime.MinValue;
            }

            if (chk_hastaSiempre_cartera.Checked)
            {
                fecha_hasta = dtp_hasta_cartera.Value;
            }
            else
            {
                fecha_hasta = DateTime.MaxValue;
            }


            sqlstr = "SELECT ch.id_cheque AS 'ID', CAST(ch.fecha_ingreso AS VARCHAR(50)) AS 'F. Ingreso', CAST(ch.fecha_emision AS VARCHAR(50)) AS 'F. Emisión', " + "c.razon_social AS 'Recibido de', p.razon_social AS 'Entregado a', b.nombre AS 'Banco emisor', " + "cb.nombre AS 'Depositado en', ch.nCheque AS 'Nº cheque', ch.nCheque2 AS '2do nºd/cheque', ch.importe AS 'Monto', sech.estado AS 'Estado', " + "CAST(ch.fecha_cobro AS VARCHAR(50)) AS 'Fecha de cobro', " + "CAST(ch.fecha_salida AS VARCHAR(50)) AS 'Fecha de salida', CAST(ch.fecha_deposito AS VARCHAR(50)) AS 'Fecha de deposito' " + "FROM cheques AS ch " + "LEFT JOIN clientes AS c ON ch.id_cliente = c.id_cliente " + "LEFT JOIN proveedores AS p ON ch.id_proveedor = p.id_proveedor " + "LEFT JOIN bancos AS b ON ch.id_banco = b.id_banco " + "LEFT JOIN cuentas_bancarias AS cb ON ch.id_cuentaBancaria = cb.id_cuentaBancaria " + "LEFT JOIN sysestados_cheques AS sech ON ch.id_estadoch = sech.id_estadoch " + "WHERE ch.activo = '1' " + "AND ch.id_estadoch = '1' " + "AND ch.fecha_cobro BETWEEN '" + fecha_desde.ToString("yyyy/MM/dd") + "' AND '" + fecha_hasta.ToString("yyyy/MM/dd") + "' ";
            if (!string.IsNullOrEmpty(txt_nCH_cartera.Text))
                sqlstr += "AND ch.nCheque = '" + txt_nCH_cartera.Text + "' ";
            if (!string.IsNullOrEmpty(txt_importeCH_cartera.Text))
                sqlstr += "AND ch.importe = '" + txt_importeCH_cartera.Text + "' ";
            sqlstr += "AND ch.id_estadoch = '" + VariablesGlobales.ID_CH_CARTERA.ToString() + "' " + "ORDER BY ch.id_cheque ASC";

            desde_cartera = 0;
            pagina_cartera = 1;


            var argdataGrid = dg_view_chCartera;
            var argtxtnPage = txt_nPage_cartera;
            generales.cargar_datagrid(ref argdataGrid, sqlstr, VariablesGlobales.basedb, desde_cartera, ref nRegs_cartera, ref tPaginas_cartera, pagina_cartera, ref argtxtnPage, "cheques", "cheques");
            dg_view_chCartera = argdataGrid;
            txt_nPage_cartera = argtxtnPage;
            dg_view_chCartera.ClearSelection();
        }

        private void actualizarDatagrid_cartera(string sqlstr, DateTime fecha_desde, DateTime fecha_hasta)
        {
            desde_cartera = 0;
            pagina_cartera = 1;


            var argdataGrid = dg_view_chCartera;
            var argtxtnPage = txt_nPage_cartera;
            generales.cargar_datagrid(ref argdataGrid, sqlstr, VariablesGlobales.basedb, desde_cartera, ref nRegs_cartera, ref tPaginas_cartera, pagina_cartera, ref argtxtnPage, "cheques", "cheques");
            dg_view_chCartera = argdataGrid;
            txt_nPage_cartera = argtxtnPage;
        }

        private void cmd_next_Click_cartera(object sender, EventArgs e)
        {
            if (pagina_cartera == Math.Ceiling(nRegs_cartera / (double)VariablesGlobales.itXPage))
                return;
            desde_cartera += VariablesGlobales.itXPage;
            pagina_cartera += 1;
            actualizarDatagrid_cartera();
        }

        private void cmd_prev_Click_cartera(object sender, EventArgs e)
        {
            if (pagina_cartera == 1)
                return;
            desde_cartera -= VariablesGlobales.itXPage;
            pagina_cartera -= 1;
            actualizarDatagrid_cartera();
        }

        private void cmd_first_Click_cartera(object sender, EventArgs e)
        {
            desde_cartera = 0;
            pagina_cartera = 1;
            actualizarDatagrid_cartera();
        }

        private void cmd_last_Click_cartera(object sender, EventArgs e)
        {
            pagina_cartera = tPaginas_cartera;
            desde_cartera = nRegs_cartera - VariablesGlobales.itXPage;
            actualizarDatagrid_cartera();
        }

        private void cmd_go_Click_cartera(object sender, EventArgs e)
        {
            pagina_cartera = Conversions.ToInteger(txt_nPage_cartera.Text);
            if (pagina_cartera > tPaginas_cartera)
                pagina_cartera = tPaginas_cartera;
            desde_cartera = (pagina_cartera - 1) * VariablesGlobales.itXPage;
            actualizarDatagrid_cartera();
        }

        private void txt_nPage_KeyDown_cartera(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmd_go_Click_cartera(null, null);
            }
        }

        private void txt_nPage_Click_cartera(object sender, EventArgs e)
        {
            txt_nPage_cartera.Text = "";
        }

        private void actualizarDatagrid_depositado()
        {
            string sqlstr;
            DateTime fecha_desde;
            DateTime fecha_hasta;

            if (chk_desdeSiempre_depositado.Checked)
            {
                fecha_desde = dtp_desde_depositado.Value;
            }
            else
            {
                fecha_desde = DateTime.MinValue;
            }

            if (chk_hastaSiempre_depositado.Checked)
            {
                fecha_hasta = dtp_hasta_depositado.Value;
            }
            else
            {
                fecha_hasta = DateTime.MaxValue;
            }

            sqlstr = "SELECT ch.id_cheque AS 'ID', CAST(ch.fecha_ingreso AS VARCHAR(50)) AS 'F. Ingreso', CAST(ch.fecha_emision AS VARCHAR(50)) AS 'F. Emisión', " + "c.razon_social AS 'Recibido de', p.razon_social AS 'Entregado a', b.nombre AS 'Banco emisor', " + "cb.nombre AS 'Depositado en', ch.nCheque AS 'Nº cheque', ch.nCheque2 AS '2do nºd/cheque', ch.importe AS 'Monto', sech.estado AS 'Estado', " + "CAST(ch.fecha_cobro AS VARCHAR(50)) AS 'Fecha de cobro', " + "CAST(ch.fecha_salida AS VARCHAR(50)) AS 'Fecha de salida', CAST(ch.fecha_deposito AS VARCHAR(50)) AS 'Fecha de deposito' " + "FROM cheques AS ch " + "LEFT JOIN clientes AS c ON ch.id_cliente = c.id_cliente " + "LEFT JOIN proveedores AS p ON ch.id_proveedor = p.id_proveedor " + "LEFT JOIN bancos AS b ON ch.id_banco = b.id_banco " + "LEFT JOIN cuentas_bancarias AS cb ON ch.id_cuentaBancaria = cb.id_cuentaBancaria " + "LEFT JOIN sysestados_cheques AS sech ON ch.id_estadoch = sech.id_estadoch " + "WHERE ch.activo = '1' ";
            if (cmb_cuentaBancaria.Text != "Seleccione una cuenta bancaria...")
                sqlstr += " AND ch.id_cuentaBancaria = '" + cmb_cuentaBancaria.SelectedValue.ToString() + "'";
            sqlstr += "AND ch.fecha_cobro BETWEEN '" + fecha_desde.ToString("yyyy/MM/dd") + "' AND '" + fecha_hasta.ToString("yyyy/MM/dd") + "' ";
            if (!string.IsNullOrEmpty(txt_nCH_depositado.Text))
                sqlstr += "AND ch.nCheque = '" + txt_nCH_depositado.Text + "' ";
            if (!string.IsNullOrEmpty(txt_importeCH_depositado.Text))
                sqlstr += "AND ch.importe = '" + txt_importeCH_depositado.Text + "' ";
            sqlstr += "AND ch.id_estadoch = '" + VariablesGlobales.ID_CH_DEPOSITADO.ToString() + "' " + "ORDER BY ch.id_cheque ASC";

            desde_cartera = 0;
            pagina_cartera = 1;

            var argdataGrid = dg_view_chDepositados;
            var argtxtnPage = txt_nPage_depositado;
            generales.cargar_datagrid(ref argdataGrid, sqlstr, VariablesGlobales.basedb, desde_depositado, ref nRegs_depositado, ref tPaginas_depositado, pagina_depositado, ref argtxtnPage);
            dg_view_chDepositados = argdataGrid;
            txt_nPage_depositado = argtxtnPage;
        }

        private void cmd_next_Click_depositado(object sender, EventArgs e)
        {
            if (pagina_depositado == Math.Ceiling(nRegs_depositado / (double)VariablesGlobales.itXPage))
                return;
            desde_depositado += VariablesGlobales.itXPage;
            pagina_depositado += 1;
            actualizarDatagrid_depositado();
        }

        private void cmd_prev_Click_depositado(object sender, EventArgs e)
        {
            if (pagina_depositado == 1)
                return;
            desde_depositado -= VariablesGlobales.itXPage;
            pagina_depositado -= 1;
            actualizarDatagrid_depositado();
        }

        private void cmd_first_Click_depositado(object sender, EventArgs e)
        {
            desde_depositado = 0;
            pagina_depositado = 1;
            actualizarDatagrid_depositado();
        }

        private void cmd_last_Click_depositado(object sender, EventArgs e)
        {
            pagina_depositado = tPaginas_depositado;
            desde_depositado = nRegs_depositado - VariablesGlobales.itXPage;
            actualizarDatagrid_depositado();
        }

        private void cmd_go_Click_depositado(object sender, EventArgs e)
        {
            pagina_depositado = Conversions.ToInteger(txt_nPage_depositado.Text);
            if (pagina_depositado > tPaginas_depositado)
                pagina_depositado = tPaginas_depositado;
            desde_depositado = (pagina_depositado - 1) * VariablesGlobales.itXPage;
            actualizarDatagrid_depositado();
        }

        private void txt_nPage_KeyDown_depositado(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmd_go_Click_depositado(null, null);
            }
        }

        private void txt_nPage_Click_depositado(object sender, EventArgs e)
        {
            txt_nPage_depositado.Text = "";
        }

        private void txt_nCH_cartera_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                actualizarDatagrid_cartera();
            }
        }

        private void txt_importeCH_cartera_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                actualizarDatagrid_cartera();
            }
        }

        private void cmd_depositar_Click(object sender, EventArgs e)
        {
            int c;
            int sel = 0;
            bool hay_error = false;
            var ch = new cheque();

            var loopTo = dg_view_chCartera.Rows.Count - 1;
            for (c = 0; c <= loopTo; c++)
            {
                if (dg_view_chCartera.Rows[c].Selected)
                {
                    sel += 1;
                }
            }

            if (dg_view_chCartera.Rows.Count - 1 == 0)
            {
                Interaction.MsgBox("No hay cheques que pueda depositar", (MsgBoxStyle)((int)Constants.vbExclamation + (int)Constants.vbOKOnly), "Centrex");
                return;
            }
            else if (sel == 0)
            {
                Interaction.MsgBox("No ha seleccionado ningún cheque para depositar", (MsgBoxStyle)((int)Constants.vbExclamation + (int)Constants.vbOKOnly), "Centrex");
                return;
            }
            else if (cmb_banco.Text == "Seleccione un banco...")
            {
                Interaction.MsgBox("No hay seleccionado un banco en el cual depositar el/los cheque(s)", (MsgBoxStyle)((int)Constants.vbExclamation + (int)Constants.vbOKOnly), "Centrex");
                return;
            }
            else if (cmb_cuentaBancaria.Text == "Seleccione una cuenta bancaria...")
            {
                Interaction.MsgBox("No hay seleccionada una cuenta bancaria en la cual depositar el/los cheque(s)", (MsgBoxStyle)((int)Constants.vbExclamation + (int)Constants.vbOKOnly), "Centrex");
                return;
            }

            if (Interaction.MsgBox("¿Confirma depositar el/los cheque(s) seleccionados en la cuenta: " + cmb_cuentaBancaria.Text + " perteneciente al banco: " + cmb_banco.Text + "?", (MsgBoxStyle)((int)Constants.vbYesNo + (int)Constants.vbQuestion), "Centrex") == Constants.vbNo)
            {
                return;
            }

            var loopTo1 = dg_view_chCartera.Rows.Count - 1;
            for (c = 0; c <= loopTo1; c++)
            {
                if (dg_view_chCartera.Rows[c].Selected)
                {
                    ch.id_cheque = Conversions.ToInteger(dg_view_chCartera.Rows[c].Cells[0].Value.ToString());
                    ch.fecha_deposito = generales.Hoy();
                    ch.id_cuentaBancaria = Conversions.ToInteger(cmb_cuentaBancaria.SelectedValue);
                    ch.nCheque = dg_view_chCartera.Rows[c].Cells[7].Value.ToString();
                    if (!cheques.Depositar_cheque(ch))
                    {
                        Interaction.MsgBox("Hubo un problema al depositar el cheque con número: " + ch.nCheque.ToString() + "en la cuenta bancaria: " + cmb_cuentaBancaria.Text + "perteneciente al banco: " + cmb_banco.Text, (MsgBoxStyle)((int)Constants.vbCritical + (int)Constants.vbOKOnly), "Centrex");
                        hay_error = true;
                        // Exit Sub
                    }
                }
            }


            if (!hay_error)
            {
                Interaction.MsgBox("Se han depositado correctamente los cheques.", (MsgBoxStyle)((int)Constants.vbInformation + (int)Constants.vbOKOnly), "Centrex");
            }
            else
            {
                Interaction.MsgBox("Verifique, no todos los cheques se depositaron correctamente, puede ser que no puedan ser depositados.", (MsgBoxStyle)((int)Constants.vbInformation + (int)Constants.vbOKOnly), "Centrex");
            }

            actualizarDatagrid_cartera();
            actualizarDatagrid_depositado();
        }

        private void cmb_banco_SelectionChangeCommitted(object sender, EventArgs e)
        {
            var argcombo = cmb_cuentaBancaria;
            generales.Cargar_Combo(ref argcombo, "SELECT cb.id_cuentaBancaria, CONCAT(b.nombre, ' - ', cb.nombre) AS nombre " + "FROM cuentas_bancarias AS cb " + "INNER JOIN bancos AS b  ON cb.id_banco = b.id_banco " + "WHERE cb.activo = '1' AND cb.id_banco = '" + cmb_banco.SelectedValue.ToString() + "' " + "ORDER BY b.nombre, cb.nombre ASC", VariablesGlobales.basedb, "nombre", Conversions.ToInteger("id_cuentaBancaria"));
            cmb_cuentaBancaria = argcombo;

            cmb_cuentaBancaria.Text = "Seleccione una cuenta bancaria...";
            cmb_cuentaBancaria.Enabled = true;

            actualizarDatagrid_depositado();
        }

        private void chk_desdeSiempre_cartera_CheckedChanged(object sender, EventArgs e)
        {
            dtp_desde_cartera.Enabled = chk_desdeSiempre_cartera.Checked;
            actualizarDatagrid_cartera();
        }

        private void chk_hastaSiempre_cartera_CheckedChanged(object sender, EventArgs e)
        {
            dtp_hasta_cartera.Enabled = chk_hastaSiempre_cartera.Checked;
            actualizarDatagrid_cartera();
        }

        private void chk_desdeSiempre_depositado_CheckedChanged(object sender, EventArgs e)
        {
            dtp_desde_depositado.Enabled = chk_desdeSiempre_depositado.Checked;
        }

        private void chk_hastaSiempre_depositado_CheckedChanged(object sender, EventArgs e)
        {
            dtp_hasta_depositado.Enabled = chk_hastaSiempre_depositado.Checked;
        }
        private void txt_nCH_depositado_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                actualizarDatagrid_depositado();
            }
        }

        private void txt_importeCH_depositado_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                actualizarDatagrid_depositado();
            }
        }

        private void frm_depositarCH_FormClosed(object sender, FormClosedEventArgs e)
        {
            closeandupdate(this);
        }

        private void cmb_cuentaBancaria_Leave(object sender, EventArgs e)
        {
            actualizarDatagrid_depositado();
        }

        private void chk_desdeSiempre_depositado_Leave(object sender, EventArgs e)
        {
            actualizarDatagrid_depositado();
        }

        private void dtp_desde_depositado_Leave(object sender, EventArgs e)
        {
            actualizarDatagrid_depositado();
        }

        private void chk_hastaSiempre_depositado_Leave(object sender, EventArgs e)
        {
            actualizarDatagrid_depositado();
        }

        private void dtp_hasta_depositado_Leave(object sender, EventArgs e)
        {
            actualizarDatagrid_depositado();
        }

        private void txt_nCH_depositado_Leave(object sender, EventArgs e)
        {
            actualizarDatagrid_depositado();
        }

        private void txt_importeCH_depositado_Leave(object sender, EventArgs e)
        {
            actualizarDatagrid_depositado();
        }

        private void dtp_desde_cartera_Leave(object sender, EventArgs e)
        {
            actualizarDatagrid_cartera();
        }

        private void dtp_hasta_cartera_Leave(object sender, EventArgs e)
        {
            actualizarDatagrid_cartera();
        }

        private void txt_nCH_cartera_Leave(object sender, EventArgs e)
        {
            actualizarDatagrid_cartera();
        }

        private void txt_importeCH_cartera_Leave(object sender, EventArgs e)
        {
            actualizarDatagrid_cartera();
        }

        private void cmb_cuentaBancaria_SelectionChangeCommitted(object sender, EventArgs e)
        {
            actualizarDatagrid_depositado();
        }

        private void cmd_anularDeposito_Click(object sender, EventArgs e)
        {
            int c;
            int sel = 0;
            bool hay_error = false;
            var ch = new cheque();

            var loopTo = dg_view_chDepositados.Rows.Count - 1;
            for (c = 0; c <= loopTo; c++)
            {
                if (dg_view_chDepositados.Rows[c].Selected)
                {
                    sel += 1;
                }
            }

            if (dg_view_chDepositados.Rows.Count - 1 == 0)
            {
                Interaction.MsgBox("No hay cheques que pueda anular el deposito.", (MsgBoxStyle)((int)Constants.vbExclamation + (int)Constants.vbOKOnly), "Centrex");
                return;
            }
            else if (sel == 0)
            {
                Interaction.MsgBox("No ha seleccionado ningún cheque para anular el deposito.", (MsgBoxStyle)((int)Constants.vbExclamation + (int)Constants.vbOKOnly), "Centrex");
                return;
            }

            if (Interaction.MsgBox("¿Confirma anular el deposito de el/los cheque(s) seleccionados?", (MsgBoxStyle)((int)Constants.vbYesNo + (int)Constants.vbQuestion), "Centrex") == Constants.vbNo)
            {
                return;
            }

            var loopTo1 = dg_view_chDepositados.Rows.Count - 1;
            for (c = 0; c <= loopTo1; c++)
            {
                if (dg_view_chDepositados.Rows[c].Selected)
                {
                    ch.id_cheque = Conversions.ToInteger(dg_view_chDepositados.Rows[c].Cells[0].Value.ToString());
                    ch.nCheque = dg_view_chDepositados.Rows[c].Cells[7].Value.ToString();
                    if (!cheques.Anular_Deposito_Cheque(ch.id_cheque))
                    {
                        Interaction.MsgBox("Hubo un problema al depositar el cheque con número: " + ch.nCheque.ToString() + "en la cuenta bancaria: " + cmb_cuentaBancaria.Text + "perteneciente al banco: " + cmb_banco.Text, (MsgBoxStyle)((int)Constants.vbCritical + (int)Constants.vbOKOnly), "Centrex");
                        hay_error = true;
                        // Exit Sub
                    }
                }
            }


            if (!hay_error)
            {
                Interaction.MsgBox("Se ha(n) anulado el/los deposito(s) correctamente.", (MsgBoxStyle)((int)Constants.vbInformation + (int)Constants.vbOKOnly), "Centrex");
            }
            else
            {
                Interaction.MsgBox("Verifique, no se pudo anular el deposito de todos los cheques, posiblemente el deposito no pueda ser anulado.", (MsgBoxStyle)((int)Constants.vbInformation + (int)Constants.vbOKOnly), "Centrex");
            }

            actualizarDatagrid_cartera();
            actualizarDatagrid_depositado();
        }

        private void cmd_filtrarCH_cartera_Click(object sender, EventArgs e)
        {

        }
    }
}
