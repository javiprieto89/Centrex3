using System;
using System.Windows.Forms;

namespace Centrex
{
    public partial class login
    {
        private bool iniciar = false;

        public login()
        {
            InitializeComponent();
        }

        private void cmd_start_Click(object sender, EventArgs e)
        {
            UsuarioEntity u;

            u = info_usuario(txt_usuario.Text, true);
            if (u.Usuario == "error")
            {
                MessageBox.Show("El nombre de usuario: " + txt_usuario.Text + " NO EXISTE",
                       "Centrex",
             MessageBoxButtons.OK,
         MessageBoxIcon.Error);
                return;
            }

            u = usuarios.info_login(txt_usuario.Text, txt_password.Text);
            if (u.Usuario == "error")
            {
                MessageBox.Show("La contraseña ingresada para el usuario: " + txt_usuario.Text + " NO ES CORRECTA.",
                       "Centrex",
           MessageBoxButtons.OK,
                 MessageBoxIcon.Error);
                return;
            }
            else if (!u.Activo)
            {
                MessageBox.Show("El usuario: " + txt_usuario.Text + " esta deshabilitado para el inicio de sesión",
       "Centrex",
             MessageBoxButtons.OK,
     MessageBoxIcon.Error);
                return;
            }

            usuario_logueado = u;
            iniciar = true;
            closeandupdate(this);
        }

        private void cmd_exit_Click(object sender, EventArgs e)
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

        private void login_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!iniciar)
            {
                Environment.Exit(0);
            }
        }

        private void login_Load(object sender, EventArgs e)
        {

        }
    }
}
