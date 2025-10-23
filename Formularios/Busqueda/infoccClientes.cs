using System;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace Centrex
{
    public partial class infoccClientes
    {
        private int desde;
        private int pagina;
        private int nRegs;
        private int tPaginas;

        private DateTime fechaDesde;
        private DateTime fechaHasta;

        private ColumnClickEventArgs orderCol = null;

        public infoccClientes()
        {
            InitializeComponent();
        }

        private void ccClientes_Load(object sender, EventArgs e)
        {
            string sqlstr;

            // form = Me ' Comentado para evitar error de compilación

            // Cargo el combo con todos los clientes
            sqlstr = "SELECT c.id_cliente AS 'id_cliente', c.razon_social AS 'razon_social' FROM clientes AS c WHERE c.activo = '1' ORDER BY c.razon_social ASC";
            var argcombo = cmb_cliente;
            generales.Cargar_Combo(ref argcombo, sqlstr, VariablesGlobales.basedb, "razon_social", Conversions.ToInteger("id_cliente"));
            cmb_cliente = argcombo;
            cmb_cliente.Text = "Seleccione un cliente...";

            pExportXLS.Enabled = false;
        }

        private void ccClientes_FormClosed(object sender, FormClosedEventArgs e)
        {
            closeandupdate(this);
        }

        private void cmd_consultar_Click(object sender, EventArgs e)
        {
            // Dim saldo As saldoCaja
            string saldo;
            string total;

            if (cmb_cliente.Text == "Seleccione un cliente...")
            {
                Interaction.MsgBox("El campo 'Cliente' es obligatorio y está vacio", (MsgBoxStyle)((int)Constants.vbExclamation + (int)Constants.vbOKOnly), "Centrex");
                return;
            }
            else if (cmb_cc.Text == "Seleccione una cuenta corriente...")
            {
                Interaction.MsgBox("Debe elegir una cuenta corriente del cliente seleccionado para poder realizar la consulta.", (MsgBoxStyle)((int)MsgBoxStyle.Exclamation + (int)Constants.vbOKOnly), "Centrex");
                return;
            }

            if (chk_desdeSiempre.Checked)
            {
                fechaDesde = dtp_desde.MinDate;
            }
            else
            {
                fechaDesde = dtp_desde.Value.Date;
            }

            if (chk_hastaSiempre.Checked)
            {
                fechaHasta = dtp_hasta.MaxDate;
            }
            else
            {
                fechaHasta = dtp_hasta.Value.Date;
            }

            desde = 0;
            pagina = 1;

            actualizarDatagrid();

            cmd_first.Enabled = true;
            cmd_prev.Enabled = true;
            cmd_next.Enabled = true;
            cmd_last.Enabled = true;
            txt_nPage.Enabled = true;
            cmd_go.Enabled = true;
            pExportXLS.Enabled = true;

            // cmd_last_Click(Nothing, Nothing) 'Bush quiere que aparezca en la última página

            total = ccClientes.consultaTotalCcCliente(Conversions.ToInteger(cmb_cliente.SelectedValue), fechaDesde, fechaHasta);

            lbl_total.Text = "$ " + total;

            saldo = ccClientes.info_ccCliente(Conversions.ToInteger(cmb_cc.SelectedValue)).saldo.ToString();
            lbl_saldo.Text = "$ " + saldo;

            if (Conversions.ToBoolean(Strings.InStr(saldo, "-")))
            {
                lbl_saldo.ForeColor = Color.Red;
            }
            else
            {
                lbl_saldo.ForeColor = Color.Green;
            }

            lbl_saldo.Visible = true;
            lbl_total.Visible = true;
        }

        private void dg_view_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string seleccionado;
            // Dim p As New pedido
            var frmPrn = new frm_prnCmp(true);
            // If borrado = False Then edicion = True

            // If dg_view.Rows.Count = 0 Then Exit Sub


            // VariablesGlobales.edita_pedido = info_pedido(seleccionado)
            // pedido_a_pedidoTmp(seleccionado)
            // add_pedido.ShowDialog()

            // If borrado = False Then edicion = False
            seleccionado = dg_view.CurrentRow.Cells[0].Value.ToString();

            VariablesGlobales.id = transacciones.InfoTransaccion(seleccionado).id_pedido;
            // id = p.id_pedido

            frmPrn.ShowDialog();
        }

        private void cmb_cliente_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string sqlstr;

            // Cargo el combo con todas las cuentas corrientes del cliente seleccionado
            sqlstr = "SELECT cc.id_cc AS 'id_cc', cc.nombre AS 'nombre' FROM cc_clientes AS cc WHERE cc.id_cliente = '" + cmb_cliente.SelectedValue.ToString() + "' AND cc.activo = '1' ORDER BY cc.nombre ASC";
            var argcombo = cmb_cc;
            generales.Cargar_Combo(ref argcombo, sqlstr, VariablesGlobales.basedb, "nombre", Conversions.ToInteger("id_cc"));
            cmb_cc = argcombo;
            cmb_cc.Text = "Seleccione una cuenta corriente...";

            cmb_cc.Enabled = true;
            ActiveControl = cmb_cc;

            if (cmb_cc.Items.Count == 1)
            {
                cmb_cc.SelectedIndex = 0;
                cmb_cc.Text = ccClientes.info_ccCliente(Conversions.ToInteger(cmb_cc.SelectedValue)).nombre;
            }
        }

        private void psearch_cliente_Click(object sender, EventArgs e)
        {
            // busqueda
            string tmp;
            tmp = VariablesGlobales.tabla;
            VariablesGlobales.tabla = "clientes";
            Enabled = false;
            My.MyProject.Forms.search.ShowDialog();
            VariablesGlobales.tabla = tmp;

            // Establezco la opción del combo, si es 0 elijo el cliente default
            if (VariablesGlobales.id == 0)
                VariablesGlobales.id = VariablesGlobales.id_cliente_pedido_default;
            var argcmb = cmb_cliente;
            generales.updateform(VariablesGlobales.id.ToString(), ref argcmb);
            cmb_cliente = argcmb;
            cmb_cliente_SelectionChangeCommitted(null, null);
        }

        private void psearch_cc_Click(object sender, EventArgs e)
        {
            // busqueda
            string tmp;
            tmp = VariablesGlobales.tabla;
            VariablesGlobales.tabla = "archivoCCClientes";
            Enabled = false;
            My.MyProject.Forms.search.ShowDialog();
            VariablesGlobales.tabla = tmp;


            var argcmb = cmb_cc;
            generales.updateform(VariablesGlobales.id.ToString(), ref argcmb);
            cmb_cc = argcmb;
        }

        private void chk_desdeSiempre_CheckedChanged(object sender, EventArgs e)
        {
            dtp_desde.Enabled = !chk_desdeSiempre.Checked;
        }

        private void chk_hastaSiempre_CheckedChanged(object sender, EventArgs e)
        {
            dtp_hasta.Enabled = !chk_hastaSiempre.Checked;
        }

        private void pExportXLS_Click(object sender, EventArgs e)
        {
            string rutaArchivo;

            {
                var withBlock = SaveFileDialog1;
                withBlock.AddExtension = true;
                withBlock.CheckFileExists = false;
                withBlock.CheckPathExists = true;
                withBlock.Filter = "Excel Worksheets 2007 (*.xlsx)|*.xlsx";
                withBlock.DefaultExt = "xls";
                withBlock.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                withBlock.FileName = cmb_cliente.Text + " - " + cmb_cc.Text;
                withBlock.ShowDialog();
                rutaArchivo = withBlock.FileName;
                if (string.IsNullOrEmpty(rutaArchivo))
                {
                    Interaction.MsgBox("Exportación cancelada.", (MsgBoxStyle)((int)Constants.vbInformation + (int)Constants.vbOKOnly), "Centrex");
                    return;
                }
            }

            var argdataGrid = dgView_paraExportar;
            var argtxtnPage = txt_nPage;
            ccClientes.consultaCcCliente(ref argdataGrid, Conversions.ToInteger(cmb_cliente.SelectedValue), Conversions.ToInteger(cmb_cc.SelectedValue), fechaDesde, fechaHasta, desde, ref nRegs, ref tPaginas, pagina, ref argtxtnPage, Conversions.ToBoolean(1));
            dgView_paraExportar = argdataGrid;
            txt_nPage = argtxtnPage;

            generales.exportarExcel(dgView_paraExportar, rutaArchivo);
            Interaction.MsgBox("Archivo exportado a: " + SaveFileDialog1.FileName, (MsgBoxStyle)((int)Constants.vbInformation + (int)Constants.vbOKOnly), "Centrex");
        }

        private void actualizarDatagrid()
        {
            var argdataGrid = dg_view;
            var argtxtnPage = txt_nPage;
            ccClientes.consultaCcCliente(ref argdataGrid, Conversions.ToInteger(cmb_cliente.SelectedValue), Conversions.ToInteger(cmb_cc.SelectedValue), fechaDesde, fechaHasta, desde, ref nRegs, ref tPaginas, pagina, ref argtxtnPage, Conversions.ToBoolean(0));
            dg_view = argdataGrid;
            txt_nPage = argtxtnPage;
        }

        private void cmd_next_Click(object sender, EventArgs e)
        {
            if (pagina == Math.Ceiling(nRegs / (double)VariablesGlobales.itXPage))
                return;
            desde += VariablesGlobales.itXPage;
            pagina += 1;
            actualizarDatagrid();
        }

        private void cmd_prev_Click(object sender, EventArgs e)
        {
            if (pagina == 1)
                return;
            desde -= VariablesGlobales.itXPage;
            pagina -= 1;
            actualizarDatagrid();
        }

        private void cmd_first_Click(object sender, EventArgs e)
        {
            desde = 0;
            pagina = 1;
            actualizarDatagrid();
        }

        private void cmd_last_Click(object sender, EventArgs e)
        {
            pagina = tPaginas;
            desde = nRegs - VariablesGlobales.itXPage;
            actualizarDatagrid();
        }

        private void cmd_go_Click(object sender, EventArgs e)
        {
            pagina = Conversions.ToInteger(txt_nPage.Text);
            if (pagina > tPaginas)
                pagina = tPaginas;
            desde = (pagina - 1) * VariablesGlobales.itXPage;
            actualizarDatagrid();
        }
    }
}

