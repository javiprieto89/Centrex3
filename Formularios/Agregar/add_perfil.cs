using System;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace Centrex
{
    public partial class add_perfil
    {
        public add_perfil()
        {
            InitializeComponent();
        }
        private void cmd_ok_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_perfil.Text))
            {
                Interaction.MsgBox("El campo 'Perfil' es obligatorio y está vacio");
            }

            var tmp = new PerfilEntity();

            tmp.Nombre = txt_perfil.Text;
            tmp.Activo = chk_activo.Checked;

            if (VariablesGlobales.edicion == true)
            {
                tmp.IdPerfil = VariablesGlobales.edita_perfil.IdPerfil;
                if (perfiles.updateperfil(tmp) == false)
                {
                    Interaction.MsgBox("Hubo un problema al actualizar el perfil, consulte con su programdor", (MsgBoxStyle)((int)Constants.vbExclamation + (int)Constants.vbOKOnly), "Centrex");
                    closeandupdate(this);
                }
            }
            else
            {
                perfiles.addperfil(tmp);
            }

            if (chk_secuencia.Checked == true)
            {
                txt_perfil.Text = "";
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

        private void add_perfil_FormClosed(object sender, FormClosedEventArgs e)
        {
            closeandupdate(this);
        }

        private void add_perfil_Load(object sender, EventArgs e)
        {
            chk_activo.Checked = true;
            if (VariablesGlobales.edicion == true | VariablesGlobales.borrado == true)
            {
                chk_secuencia.Enabled = false;
                txt_perfil.Text = VariablesGlobales.edita_perfil.Nombre;
                chk_activo.Checked = VariablesGlobales.edita_perfil.Activo;
            }

            if (VariablesGlobales.borrado == true)
            {
                txt_perfil.Enabled = false;
                chk_activo.Enabled = false;
                cmd_ok.Visible = false;
                cmd_exit.Visible = false;
                Show();
                if (Interaction.MsgBox("¿Está seguro que desea borrar este perfil?", (MsgBoxStyle)((int)Constants.vbYesNo + (int)Constants.vbQuestion)) == MsgBoxResult.Yes)
                {
                    if (perfiles.borrarperfil(VariablesGlobales.edita_perfil.IdPerfil) == false)
                    {
                        if (Interaction.MsgBox("Ocurrió un error al realizar el borrado del perfil, ¿desea intectar desactivarlo para que no aparezca en la búsqueda?", (MsgBoxStyle)((int)MsgBoxStyle.Question + (int)MsgBoxStyle.YesNo)) == Constants.vbYes)
                        {
                            // Realizo un borrado lógico
                            if (perfiles.updateperfil(VariablesGlobales.edita_perfil, true) == true)
                            {
                                Interaction.MsgBox("Se ha podido realizar un borrado lógico, pero el perfil no se borró definitivamente." + "\r" + "Esto posiblemente se deba a que el perfil, tiene operaciones realizadas y por lo tanto no podrá borrarse", Constants.vbInformation);
                            }
                            else
                            {
                                Interaction.MsgBox("No se ha podido borrar del perfil, consulte con el programador");
                            }
                        }
                    }
                }
                closeandupdate(this);
            }
        }
    }
}

