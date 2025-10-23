using System;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace Centrex
{
    public partial class edita_precios
    {
        public edita_precios()
        {
            InitializeComponent();
        }
        private void edita_precios_Load(object sender, EventArgs e)
        {
            string sqlstr;
            sqlstr = "SELECT i.id_item AS 'ID', i.item AS 'Código', i.descript AS 'Producto', i.precio_lista AS 'Precio' " + "FROM items AS i " + "WHERE i.esDescuento = '0' AND i.esMarkup = '0' AND i.activo = '1'";

            var argdataGrid = dg_view;
            int argnRegs = 0;
            int argtPaginas = 0;
            TextBox argtxtnPage = null;
            generales.cargar_datagrid(ref argdataGrid, sqlstr, VariablesGlobales.basedb, 0, ref argnRegs, ref argtPaginas, 1, ref argtxtnPage, "", "");
            dg_view = argdataGrid;
            dg_view.Columns[0].ReadOnly = true;
            dg_view.Columns[1].ReadOnly = true;
            dg_view.Columns[2].ReadOnly = true;
        }

        private void cmd_ok_Click(object sender, EventArgs e)
        {
            if (Interaction.MsgBox("¿Está seguro de que desea aplicar la actualización de precios?", (MsgBoxStyle)((int)Constants.vbYesNo + (int)Constants.vbQuestion), "Centrex") == MsgBoxResult.Yes)
            {
                int i;

                var loopTo = dg_view.Rows.Count - 1;
                for (i = 0; i <= loopTo; i++)
                {
                    if (!precios.updatePrecios_items(dg_view.Rows[i].Cells[0].Value.ToString(), dg_view.Rows[i].Cells[3].Value.ToString()))
                    {
                        Interaction.MsgBox("Ha ocurrido un error al actualizar los precios", (MsgBoxStyle)((int)Constants.vbOKOnly + (int)Constants.vbExclamation), "Centrex");
                        return;
                    }
                }
                Interaction.MsgBox("Todos los precios se han actualizado correctamente", (MsgBoxStyle)((int)Constants.vbOKOnly + (int)Constants.vbInformation), "Centrex");
            }
            else
            {
                Interaction.MsgBox("Todos los cambios realizados se descartarán", (MsgBoxStyle)((int)Constants.vbOKOnly + (int)Constants.vbInformation), "Centrex");
            }
            closeandupdate(this);
        }

        private void cmd_exit_Click(object sender, EventArgs e)
        {
            closeandupdate(this);
        }
    }
}

