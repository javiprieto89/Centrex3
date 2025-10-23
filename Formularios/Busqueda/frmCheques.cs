using System;
using System.Windows.Forms;
using Microsoft.VisualBasic.CompilerServices;

namespace Centrex
{
    public partial class frmCheques
    {
        private cliente cl = new cliente();
        private proveedor pr = new proveedor();
        private cobro c = new cobro();
        private bool cliente = false;

        public frmCheques(int idCliente = -1, int idProveedor = -1, int idCobro = -1)
        {

            // Esta llamada es exigida por el diseñador.
            InitializeComponent();
            if (idCliente != -1)
            {
                cl = clientes.info_cliente(idCliente.ToString());
                cliente = true;
            }
            else
            {
                pr = proveedores.info_proveedor(idProveedor.ToString());
            }

            if (idCobro != -1)
            {
                c = cobros.info_cobro(idCobro.ToString());
            }
            // Agregue cualquier inicialización después de la llamada a InitializeComponent().

        }
        private void frmCheques_Load(object sender, EventArgs e)
        {
            string sqlstr;

            sqlstr = "INSERT INTO tmpSelCH (id_cheque) SELECT id_cheque FROM cheques WHERE id_cliente = '" + cl.id_cliente.ToString() + "'";
            ejecutarSQL(sqlstr);

            if (cliente)
            {
                lbl_cheques.Text = "Cheques disponibles del cliente: " + cl.razon_social;
                // sqlstr = "SELECT ch.id_cheque AS 'ID', b.nombre AS 'Banco emisor', ch.nCheque AS 'Nº cheque', ch.importe AS '$$' " &
                // "FROM cheques AS ch " &
                // "INNER JOIN bancos AS b ON ch.id_banco = b.id_banco " &
                // "WHERE ch.id_cliente = '" + cl.id_cliente.ToString + "'"
                sqlstr = "SELECT tmpch.seleccionado AS 'Seleccionado', ch.id_cheque AS 'ID', b.nombre AS 'Banco emisor', ch.nCheque AS 'Nº cheque', ch.importe AS '$$' " + "FROM tmpSelCH AS tmpch " + "INNER JOIN cheques AS ch ON tmpch.id_cheque = ch.id_cheque " + "INNER JOIN bancos AS b ON ch.id_banco = b.id_banco " + "WHERE ch.id_cliente = '" + cl.id_cliente.ToString() + "' ";
                int nRegs = 0;
                int tPaginas = 0;
                var txtnPage = new TextBox();
                var argdataGrid = dg_view;
                generales.cargar_datagrid(ref argdataGrid, sqlstr, VariablesGlobales.basedb, 0, ref nRegs, ref tPaginas, 1, ref txtnPage, "cheques", "cheques");
                dg_view = argdataGrid;

                dg_view.ReadOnly = false;
                dg_view.Columns["ID"].ReadOnly = true;
                dg_view.Columns["Banco emisor"].ReadOnly = true;
                dg_view.Columns["Nº cheque"].ReadOnly = true;
                dg_view.Columns["$$"].ReadOnly = true;
                dg_view.Columns["Seleccionado"].ReadOnly = false;
            }

            else
            {
                lbl_cheques.Text = "Cheques en cartera";
            }
        }

        private void cmd_ok_Click(object sender, EventArgs e)
        {
            string sqlstr;

            if (dg_view.Rows.Count > 0)
            {
                foreach (DataGridViewRow fila in dg_view.Rows)
                {
                    if (fila is not null)
                    {
                        if (Conversions.ToBoolean(fila.Cells["Seleccionado"].Value))
                        {
                            sqlstr = "UPDATE tmpSelCH SET seleccionado = '1' WHERE id_cheque = '" + fila.Cells["ID"].Value.ToString() + "'";
                            ejecutarSQL(sqlstr);
                            // MsgBox("Fila " + fila.Cells("Nº cheque").Value.ToString + "seleccionado")
                        }
                    }
                }
            }
        }
    }
}
