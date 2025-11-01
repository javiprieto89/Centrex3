using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Centrex
{
    public partial class add_usuarios_perfiles
    {
        public add_usuarios_perfiles()
        {
            InitializeComponent();
        }

        private void add_usuarios_perfiles_Load(object sender, EventArgs e)
        {
            // Cargo todos los usuarios con descripción combinada
            try
            {
                using var ctx = new CentrexDbContext();
                var usuarios = ctx.UsuarioEntity
        .AsNoTracking()
     .OrderBy(u => u.Usuario)
            .Select(u => new
            {
                IdUsuario = u.IdUsuario,
                Descripcion = u.Usuario + " - " + u.Nombre
            })
       .ToList();

                cmb_usuarios.DataSource = usuarios;
                cmb_usuarios.DisplayMember = "Descripcion";
                cmb_usuarios.ValueMember = "IdUsuario";
                cmb_usuarios.SelectedIndex = -1;
                cmb_usuarios.Text = "Seleccione un usuario...";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar usuarios: {ex.Message}", "Centrex", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmb_usuarios.DataSource = null;
                cmb_usuarios.Items.Clear();
            }

            // Cargo todos los perfiles
            var ordenPerfiles = new List<Tuple<string, bool>> { Tuple.Create("Nombre", true) };
            var argcombo1 = cmb_perfiles;
            generales.Cargar_Combo(
               ref argcombo1,
               entidad: "PerfilEntity",
                  displaymember: "Nombre",
          valuemember: "IdPerfil",
               predet: -1,
              soloActivos: true,
               filtros: null,
         orden: ordenPerfiles);
            cmb_perfiles = argcombo1;
            cmb_perfiles.SelectedIndex = -1;
            cmb_perfiles.Text = "Seleccione un perfil...";


            if (edicion == true || borrado == true)
            {
                chk_secuencia.Enabled = false;
                cmb_usuarios.SelectedValue = edita_permiso_perfil.IdPefil;
                cmb_perfiles.SelectedValue = edita_permiso_perfil.IdPermiso;
                cmb_usuarios.Enabled = false;
                cmb_perfiles.Enabled = false;
            }

            if (borrado == true)
            {
                cmd_exit.Visible = false;
                Show();
                if (MessageBox.Show("¿Está seguro que desea borrar esta relación entre el usuario y el perfil?", "Centrex", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    // Aquí iría el código de borrado si fuera necesario
                }
                closeandupdate(this);
            }
            else if (edicion == true)
            {
                MessageBox.Show("La relación entre un usuario y un perfil no puede editarse", "Centrex", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                closeandupdate(this);
            }
        }

        private void cmd_ok_Click(object sender, EventArgs e)
        {
            if (cmb_usuarios.Text == "Seleccione un usuario...")
            {
                MessageBox.Show("Debe seleccionar un usuario", "Centrex", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            else if (cmb_perfiles.Text == "Seleccione un perfil...")
            {
                MessageBox.Show("Debe seleccionar un perfil", "Centrex", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            var tmp = new UsuarioPerfilEntity();

            tmp.IdUsuario = Convert.ToInt32(cmb_usuarios.SelectedValue);
            tmp.IdPerfil = Convert.ToInt32(cmb_perfiles.SelectedValue);

            usuarios.add_usuario_perfil(tmp);

            if (chk_secuencia.Checked == true)
            {
                cmb_usuarios.SelectedIndex = -1;
                cmb_usuarios.Text = "Seleccione un usuario...";
                cmb_perfiles.SelectedIndex = -1;
                cmb_perfiles.Text = "Seleccione un perfil...";
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

        private void add_usuarios_perfiles_FormClosed(object sender, FormClosedEventArgs e)
        {
            closeandupdate(this);
        }

        private void cmb_permisos_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = '\0';
        }

        private void cmb_perfiles_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = '\0';
        }
    }
}

