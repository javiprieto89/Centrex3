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
            if (edicion == true || borrado == true)
            {
                chk_secuencia.Enabled = false;
                txt_usuario.Text = edita_usuario.Usuario;
                txt_password.Text = encripta.Desencriptar(edita_usuario.Password);
                txt_nombre.Text = edita_usuario.Nombre;
                cmb_perfil.SelectedValue = 1; // Fijo por el momento
                chk_activo.Checked = edita_usuario.Activo;
            }

            if (borrado == true)
            {
                txt_usuario.Enabled = false;
                txt_password.Enabled = false;
                txt_nombre.Enabled = false;
                chk_activo.Enabled = false;
                cmd_ok.Visible = false;
                cmd_exit.Visible = false;
                Show();
                if (MessageBox.Show("¿Está seguro que desea borrar este usuario?", "Centrex",
                   MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (usuarios.borrarUsuario(edita_usuario) == false)
                    {
                        if (MessageBox.Show("Ocurrió un error al realizar el borrado del usuario, ¿desea intentar desactivarlo para que no aparezca en la búsqueda?",
                         "Centrex", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            // Realizo un borrado lógico
                            if (usuarios.updateUsuario(edita_usuario, true) == true)
                            {
                                MessageBox.Show("Se ha podido realizar un borrado lógico, pero el usuario no se borró definitivamente.\r\nEsto posiblemente se deba a que el usuario tiene operaciones realizadas y por lo tanto no podrá borrarse",
                            "Centrex", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("No se ha podido borrar el usuario, consulte con el programador",
                                    "Centrex", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show("El campo 'Usuario' es obligatorio y está vacio", "Centrex",
                   MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            else if (string.IsNullOrEmpty(txt_password.Text))
            {
                MessageBox.Show("El campo 'Password' es obligatorio y está vacio", "Centrex",
           MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            else if (string.IsNullOrEmpty(txt_nombre.Text))
            {
                MessageBox.Show("El campo 'Nombre' es obligatorio y está vacio", "Centrex",
            MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            else if (cmb_perfil.Text == "Seleccione un perfil...")
            {
                MessageBox.Show("Debe seleccionar un perfil inicial", "Centrex",
                 MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            var u = new UsuarioEntity();
            var perf_user = new UsuarioPerfilEntity();
            var encripta = new EncriptarType();

            u.Usuario = txt_usuario.Text;
            u.Password = encripta.Encriptar(txt_password.Text);
            u.Nombre = txt_nombre.Text;
            u.Activo = chk_activo.Checked;

            if (edicion == true)
            {
                u.IdUsuario = edita_usuario.IdUsuario;
                if (usuarios.updateUsuario(u) == false)
                {
                    MessageBox.Show("Hubo un problema al actualizar el usuario, consulte con su programador",
                                "Centrex", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    closeandupdate(this);
                }
            }
            else
            {
                usuarios.addUsuario(u);
                u = info_usuario(txt_usuario.Text, false);
                if (u.Usuario == "error")
                {
                    MessageBox.Show("Ha ocurrido un error al crear el usuario, consulte con su programador.",
                   "Centrex", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                perf_user.IdPerfil = Convert.ToInt32(cmb_perfil.SelectedValue);
                perf_user.IdUsuario = u.IdUsuario;

                if (!usuarios.add_usuario_perfil(perf_user))
                {
                    MessageBox.Show("Ha ocurrido un error al hacer la asignación del usuario con el perfil.",
                      "Centrex", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }

            if (chk_secuencia.Checked == true)
            {
                txt_usuario.Text = "";
                txt_password.Text = "";
                txt_nombre.Text = "";
                cmb_perfil.Text = "Seleccione un perfil...";
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
            e.KeyChar = '\0';
        }
    }
}

