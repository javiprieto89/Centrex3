using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

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

        public infoccClientes()
        {
            InitializeComponent();
        }

        private void ccClientes_Load(object sender, EventArgs e)
        {
            // Cargo el combo con todos los clientes
            var ordenClientes = new List<Tuple<string, bool>> { Tuple.Create("RazonSocial", true) };
            generales.Cargar_Combo(
                ref cmb_cliente,
                entidad: "ClienteEntity",
                displaymember: "RazonSocial",
                valuemember: "IdCliente",
                predet: -1,
                soloActivos: true,
                orden: ordenClientes);
            cmb_cliente.SelectedIndex = -1;
            cmb_cliente.Text = "Seleccione un cliente...";

            pExportXLS.Enabled = false;
        }

        private void ccClientes_FormClosed(object sender, FormClosedEventArgs e)
        {
            closeandupdate(this);
        }

        private void cmd_consultar_Click(object sender, EventArgs e)
        {
            string saldo;
            string total;

            if (cmb_cliente.Text == "Seleccione un cliente...")
            {
                MessageBox.Show("El campo 'Cliente' es obligatorio y está vacio", "Centrex", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            else if (cmb_cc.Text == "Seleccione una cuenta corriente...")
            {
                MessageBox.Show("Debe elegir una cuenta corriente del cliente seleccionado para poder realizar la consulta.", "Centrex", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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

            ActualizarDatagrid();

            cmd_first.Enabled = true;
            cmd_prev.Enabled = true;
            cmd_next.Enabled = true;
            cmd_last.Enabled = true;
            txt_nPage.Enabled = true;
            cmd_go.Enabled = true;
            pExportXLS.Enabled = true;

            total = ccClientes.consultaTotalCcCliente(Convert.ToInt32(cmb_cliente.SelectedValue), fechaDesde, fechaHasta);

            lbl_total.Text = "$ " + total;

            var ccInfo = ccClientes.info_ccCliente(Convert.ToInt32(cmb_cc.SelectedValue));
            saldo = ccInfo.Saldo.ToString();
            lbl_saldo.Text = "$ " + saldo;

            if (saldo.Contains("-"))
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
            //string seleccionado;
            //var frmPrn = new frm_prnCmp(true);

            //seleccionado = dg_view.CurrentRow.Cells[0].Value.ToString();

            //id = transacciones.InfoTransaccion(seleccionado).IdPedido;

            //frmPrn.ShowDialog();
        }

        private void cmb_cliente_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cmb_cliente.SelectedValue is null)
            {
                cmb_cc.DataSource = null;
                cmb_cc.Items.Clear();
                cmb_cc.Text = "Seleccione una cuenta corriente...";
                cmb_cc.Enabled = false;
                return;
            }

            var filtros = new Dictionary<string, object>
            {
                ["IdCliente"] = Convert.ToInt32(cmb_cliente.SelectedValue)
            };
            var ordenCc = new List<Tuple<string, bool>> { Tuple.Create("Nombre", true) };
            generales.Cargar_Combo(
                ref cmb_cc,
                entidad: "CcClienteEntity",
                displaymember: "Nombre",
                valuemember: "IdCc",
                predet: -1,
                soloActivos: true,
                filtros: filtros,
                orden: ordenCc);
            cmb_cc.Text = "Seleccione una cuenta corriente...";
            cmb_cc.SelectedIndex = -1;

            cmb_cc.Enabled = cmb_cc.Items.Count > 0;
            ActiveControl = cmb_cc;

            if (cmb_cc.Items.Count == 1)
            {
                cmb_cc.SelectedIndex = 0;
            }
        }

        private void psearch_cliente_Click(object sender, EventArgs e)
        {
            // busqueda
            string tmp;
            tmp = tabla;
            tabla = "clientes";
            Enabled = false;
            My.MyProject.Forms.search.ShowDialog();
            tabla = tmp;

            // Establezco la opción del combo, si es 0 elijo el cliente default
            if (id == 0)
                id = id_cliente_pedido_default;

            generales.updateform(id.ToString(), ref cmb_cliente);
            cmb_cliente_SelectionChangeCommitted(null, null);
        }

        private void psearch_cc_Click(object sender, EventArgs e)
        {
            // busqueda
            string tmp;
            tmp = tabla;
            tabla = "archivoCCClientes";
            Enabled = false;
            My.MyProject.Forms.search.ShowDialog();
            tabla = tmp;

            generales.updateform(id.ToString(), ref cmb_cc);
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
                    MessageBox.Show("Exportación cancelada.", "Centrex", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }

            ccClientes.consultaCcCliente(ref dgView_paraExportar, Convert.ToInt32(cmb_cliente.SelectedValue), Convert.ToInt32(cmb_cc.SelectedValue), fechaDesde, fechaHasta, desde, ref nRegs, ref tPaginas, pagina, ref txt_nPage, true);

            generales.exportarExcel(dgView_paraExportar, rutaArchivo);
            MessageBox.Show("Archivo exportado a: " + SaveFileDialog1.FileName, "Centrex", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ActualizarDatagrid()
        {
            ccClientes.consultaCcCliente(ref dg_view, Convert.ToInt32(cmb_cliente.SelectedValue), Convert.ToInt32(cmb_cc.SelectedValue), fechaDesde, fechaHasta, desde, ref nRegs, ref tPaginas, pagina, ref txt_nPage, false);
        }

        private void cmd_next_Click(object sender, EventArgs e)
        {
            if (pagina == Math.Ceiling(nRegs / (double)itXPage))
                return;
            desde += itXPage;
            pagina += 1;
            ActualizarDatagrid();
        }

        private void cmd_prev_Click(object sender, EventArgs e)
        {
            if (pagina == 1)
                return;
            desde -= itXPage;
            pagina -= 1;
            ActualizarDatagrid();
        }

        private void cmd_first_Click(object sender, EventArgs e)
        {
            desde = 0;
            pagina = 1;
            ActualizarDatagrid();
        }

        private void cmd_last_Click(object sender, EventArgs e)
        {
            pagina = tPaginas;
            desde = nRegs - itXPage;
            ActualizarDatagrid();
        }

        private void cmd_go_Click(object sender, EventArgs e)
        {
            pagina = int.Parse(txt_nPage.Text);
            if (pagina > tPaginas)
                pagina = tPaginas;
            desde = (pagina - 1) * itXPage;
            ActualizarDatagrid();
        }
    }
}

