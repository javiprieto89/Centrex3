using System;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace Centrex
{
    public partial class add_permiso
    {
        public add_permiso()
        {
            InitializeComponent();
        }
        private void cmd_ok_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_permiso.Text))
            {
                Interaction.MsgBox("El campo 'Permiso' es obligatorio y está vacio");
            }

            var tmp = new permiso();

            tmp.nombre = txt_permiso.Text;

            if (VariablesGlobales.edicion == true)
            {
                tmp.id_permiso = VariablesGlobales.edita_permiso.id_permiso;
                if (permisos.updatepermiso(tmp) == false)
                {
                    Interaction.MsgBox("Hubo un problema al actualizar el permiso, consulte con su programdor", (MsgBoxStyle)((int)Constants.vbExclamation + (int)Constants.vbOKOnly), "Centrex");
                    closeandupdate(this);
                }
            }
            else
            {
                permisos.addpermiso(tmp);
            }

            if (chk_secuencia.Checked == true)
            {
                txt_permiso.Text = "";
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

        private void add_permiso_FormClosed(object sender, FormClosedEventArgs e)
        {
            closeandupdate(this);
        }

        private void add_permiso_Load(object sender, EventArgs e)
        {
            if (VariablesGlobales.edicion == true | VariablesGlobales.borrado == true)
            {
                chk_secuencia.Enabled = false;
                txt_permiso.Text = VariablesGlobales.edita_permiso.nombre;
            }

            if (VariablesGlobales.borrado == true)
            {
                txt_permiso.Enabled = false;
                cmd_ok.Visible = false;
                cmd_exit.Visible = false;
                Show();
                if (Interaction.MsgBox("¿Está seguro que desea borrar este permiso?", (MsgBoxStyle)((int)Constants.vbYesNo + (int)Constants.vbQuestion)) == MsgBoxResult.Yes)
                {
                    if (permisos.borrarpermiso(VariablesGlobales.edita_permiso) == false)
                    {
                        if (Interaction.MsgBox("Ocurrió un error al realizar el borrado del permiso, ¿desea intectar desactivarlo para que no aparezca en la búsqueda?", (MsgBoxStyle)((int)MsgBoxStyle.Question + (int)MsgBoxStyle.YesNo)) == Constants.vbYes)
                        {
                            // Realizo un borrado lógico
                            if (permisos.updatepermiso(VariablesGlobales.edita_permiso, true) == true)
                            {
                                Interaction.MsgBox("Se ha podido realizar un borrado lógico, pero el permiso no se borró definitivamente." + "\r" + "Esto posiblemente se deba a que el permiso, tiene operaciones realizadas y por lo tanto no podrá borrarse", Constants.vbInformation);
                            }
                            else
                            {
                                Interaction.MsgBox("No se ha podido borrar del permiso, consulte con el programador");
                            }
                        }
                    }
                }
                closeandupdate(this);
            }
        }
    }
}

