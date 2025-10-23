using System;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace Centrex
{
    public partial class add_tipoitem
    {
        public add_tipoitem()
        {
            InitializeComponent();
        }
        private void cmd_ok_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_tipoitem.Text))
            {
                Interaction.MsgBox("El campo 'Categoría' es obligatorio y está vacio");
            }

            var tmp = new tipoitem();

            tmp.tipo = txt_tipoitem.Text;
            tmp.activo = chk_activo.Checked;

            if (VariablesGlobales.edicion == true)
            {
                tmp.id_tipo = VariablesGlobales.edita_tipoitem.id_tipo;
                if (tipositems.updatetipoitem(tmp) == false)
                {
                    Interaction.MsgBox("Hubo un problema al actualizar la categoría, consulte con su programdor", Constants.vbExclamation);
                    closeandupdate(this);
                }
            }
            else
            {
                tipositems.AddTipoItem(tmp);
            }

            if (chk_secuencia.Checked == true)
            {
                txt_tipoitem.Text = "";
                chk_activo.Checked = true;
            }
            else
            {
                closeandupdate(this);
            }
        }

        private void cmd_exit_Click(object sender, EventArgs e)
        {
            closeandupdate(this);
        }

        private void add_tipoitem_FormClosed(object sender, FormClosedEventArgs e)
        {
            closeandupdate(this);
        }

        private void add_tipoitem_Load(object sender, EventArgs e)
        {
            chk_activo.Checked = true;
            if (VariablesGlobales.edicion == true | VariablesGlobales.borrado == true)
            {
                chk_secuencia.Enabled = false;
                txt_tipoitem.Text = VariablesGlobales.edita_tipoitem.tipo;
                chk_activo.Checked = VariablesGlobales.edita_tipoitem.activo;
            }

            if (VariablesGlobales.borrado == true)
            {
                txt_tipoitem.Enabled = false;
                chk_activo.Enabled = false;
                cmd_ok.Visible = false;
                cmd_exit.Visible = false;
                Show();
                if (Interaction.MsgBox("¿Está seguro que desea borrar esta categoría?", (MsgBoxStyle)((int)Constants.vbYesNo + (int)Constants.vbQuestion)) == MsgBoxResult.Yes)
                {
                    if (tipositems.BorrarTipoItem(VariablesGlobales.edita_tipoitem) == false)
                    {
                        if (Interaction.MsgBox("Ocurrió un error al realizar el borrado de la categoría, ¿desea intectar desactivarlo para que no aparezca en la búsqueda?", (MsgBoxStyle)((int)MsgBoxStyle.Question + (int)MsgBoxStyle.YesNo)) == Constants.vbYes)
                        {
                            // Realizo un borrado lógico
                            if (tipositems.UpdateTipoItem(VariablesGlobales.edita_tipoitem, true) == true)
                            {
                                Interaction.MsgBox("Se ha podido realizar un borrado lógico, pero la categoría no se borró definitivamente." + "\r" + "Esto posiblemente se deba a que la categoría, tiene operaciones realizadas y por lo tanto no podrá borrarse", Constants.vbInformation);
                            }
                            else
                            {
                                Interaction.MsgBox("No se ha podido borrar la categoría, consulte con el programador");
                            }
                        }
                    }
                }
                closeandupdate(this);
            }
        }
    }
}
