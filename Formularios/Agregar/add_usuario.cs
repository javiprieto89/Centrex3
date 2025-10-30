using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Centrex
{
    public partial class add_usuario
    {
        public add_usuario()
        {
            InitializeComponent();
        }
        private void add_usuario_Load(object sender, EventArgs e)
        {
            var encripta = new EncriptarType();
            var up = new UsuarioPerfilEntity();

            var ordenPerfiles = new List<Tuple<string, bool>> { Tuple.Create("Nombre", true) };
            var argcombo = cmb_perfil;
            generales.Cargar_Combo(
                ref argcombo,
                entidad: "PerfilEntity",
                displaymember: "Nombre",
                valuemember: "IdPerfil",
                predet: -1,
                soloActivos: true,
                filtros: null,
                orden: ordenPerfiles);
            cmb_perfil = argcombo;
            cmb_perfil.SelectedIndex = -1;
            cmb_perfil.Text = "Seleccione un perfil...";

            chk_activo.Checked = true;
            if (VariablesGlobales.edicion == true | VariablesGlobales.borrado == true)
            {
                // up = info_perfi
                chk_secuencia.Enabled = false;
                txt_usuario.Text = VariablesGlobales.edita_usuario.Usuario;
                txt_password.Text = encripta.Desencriptar(VariablesGlobales.edita_usuario.Password);
                txt_nombre.Text = VariablesGlobales.edita_usuario.Nombre;
                cmb_perfil.SelectedValue = 1; // Fijo por el momento
                                              // cmb_perfil.Enabled = False
                chk_activo.Checked = VariablesGlobales.edita_usuario.Activo;
            }

            if (VariablesGlobales.borrado == true)
            {
                txt_usuario.Enabled = false;
                txt_password.Enabled = false;
                txt_nombre.Enabled = false;
                chk_activo.Enabled = false;
                cmd_ok.Visible = false;
                cmd_exit.Visible = false;
                Show();
                if (Interaction.MsgBox("¿Está seguro que desea borrar este usuario?", (MsgBoxStyle)((int)Constants.vbYesNo + (int)Constants.vbQuestion)) == MsgBoxResult.Yes)
                {
                    if (usuarios.borrarUsuario(VariablesGlobales.edita_usuario) == false)
                    {
                        if (Interaction.MsgBox("Ocurrió un error al realizar el borrado del usuario, ¿desea intectar desactivarlo para que no aparezca en la búsqueda?", (MsgBoxStyle)((int)MsgBoxStyle.Question + (int)MsgBoxStyle.YesNo)) == Constants.vbYes)
                        {
                            // Realizo un borrado lógico
                            if (usuarios.updateUsuario(VariablesGlobales.edita_usuario, true) == true)
                            {
                                Interaction.MsgBox("Se ha podido realizar un borrado lógico, pero el usuario no se borró definitivamente." + "\r" + "Esto posiblemente se deba a que el usuario, tiene operaciones realizadas y por lo tanto no podrá borrarse", Constants.vbInformation);
                            }
                            else
                            {
                                Interaction.MsgBox("No se ha podido borrar del usuario, consulte con el programador");
                            }
                        }
                    }
                }
                closeandupdate(this);
            }
        }

        private void cmd_ok_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_usuario.Text))
            {
                Interaction.MsgBox("El campo 'Usuario' es obligatorio y está vacio");
                return;
            }
            else if (string.IsNullOrEmpty(txt_password.Text))
            {
                Interaction.MsgBox("El campo 'Password' es obligatorio y está vacio");
                return;
            }
            else if (string.IsNullOrEmpty(txt_nombre.Text))
            {
                Interaction.MsgBox("El campo 'Nombre' es obligatorio y está vacio");
                return;
            }
            else if (cmb_perfil.Text == "Seleccione un perfil...")
            {
                Interaction.MsgBox("Debe seleccionar un perfil inicial");
                return;
            }

            var u = new UsuarioEntity();
            var perf_user = new UsuarioPerfilEntity();
            var encripta = new EncriptarType();

            u.Usuario = txt_usuario.Text;
            u.Password = encripta.Encriptar(txt_password.Text);
            u.Nombre = txt_nombre.Text;
            u.Activo = chk_activo.Checked;

            if (VariablesGlobales.edicion == true)
            {
                u.IdUsuario = VariablesGlobales.edita_usuario.IdUsuario;
                if (usuarios.updateUsuario(u) == false)
                {
                    Interaction.MsgBox("Hubo un problema al actualizar el usuario, consulte con su programdor", Constants.vbExclamation);
                    closeandupdate(this);
                }
            }
            else
            {
                usuarios.addUsuario(u);
                u = info_usuario(txt_usuario.Text, false);
                if (u.Usuario == "error")
                {
                    Interaction.MsgBox("Ha ocurrido un error al crear el usuario, consulte con su programador.", (MsgBoxStyle)((int)Constants.vbExclamation + (int)Constants.vbOKOnly), "Centrex");
                    return;
                }
                perf_user.IdPerfil = Conversions.ToInteger(cmb_perfil.SelectedValue);
                perf_user.IdUsuario = u.IdUsuario;

                if (!usuarios.add_usuario_perfil(perf_user))
                {
                    Interaction.MsgBox("Ha ocurrido un error al hacer la asignación del usuario con el perfil.", (MsgBoxStyle)((int)Constants.vbExclamation + (int)Constants.vbOKOnly), "Centrex");
                    return;
                }
            }

            if (chk_secuencia.Checked == true)
            {
                txt_usuario.Text = "";
                txt_password.Text = "";
                txt_nombre.Text = "";
                cmb_perfil.Text = "Selecione un perfil...";
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

        private void add_usuario_FormClosed(object sender, FormClosedEventArgs e)
        {
            closeandupdate(this);
        }

        private void pic_showPassword_Click(object sender, EventArgs e)
        {
            txt_password.UseSystemPasswordChar = false;
        }

        private void pic_showPassword_MouseLeave(object sender, EventArgs e)
        {
            txt_password.UseSystemPasswordChar = true;
        }

        private void cmb_perfil_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Conversions.ToChar("");
        }
    }
}

