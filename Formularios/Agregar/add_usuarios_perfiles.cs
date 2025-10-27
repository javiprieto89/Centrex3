using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

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
                Interaction.MsgBox($"Error al cargar usuarios: {ex.Message}", (MsgBoxStyle)((int)Constants.vbCritical + (int)Constants.vbOKOnly), "Centrex");
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


            if (VariablesGlobales.edicion == true | VariablesGlobales.borrado == true)
            {
                chk_secuencia.Enabled = false;
                cmb_usuarios.SelectedValue = VariablesGlobales.edita_permiso_perfil.id_perfil;
                cmb_perfiles.SelectedValue = VariablesGlobales.edita_permiso_perfil.id_permiso;
                cmb_usuarios.Enabled = false;
                cmb_perfiles.Enabled = false;
            }

            if (VariablesGlobales.borrado == true)
            {
                cmd_exit.Visible = false;
                Show();
                if (Interaction.MsgBox("¿Está seguro que desea borrar esta relación entre el usuario y el perfil?", (MsgBoxStyle)((int)Constants.vbYesNo + (int)Constants.vbQuestion)) == MsgBoxResult.Yes)
                {
                    // If (borrarTarjeta(VariablesGlobales.edita_tarjeta)) = False Then
                    // MsgBox("No se ha podido borrar la relación, consulte con el programador")
                    // End If
                }
                closeandupdate(this);
            }
            else if (VariablesGlobales.edicion == true)
            {
                Interaction.MsgBox("La relación entre un usuario y un perfil no puede editarse", (MsgBoxStyle)((int)Constants.vbExclamation + (int)Constants.vbOKOnly), "Centrex");
                closeandupdate(this);
            }
        }

        private void cmd_ok_Click(object sender, EventArgs e)
        {
            if (cmb_usuarios.Text == "Seleccione un usuario...")
            {
                Interaction.MsgBox("Debe seleccionar un usuario");
                return;
            }
            else if (cmb_perfiles.Text == "Seleccione un perfil...")
            {
                Interaction.MsgBox("Debe seleccionar un perfil");
                return;
            }

            var tmp = new usuario_perfil();

            tmp.id_usuario = Conversions.ToInteger(cmb_usuarios.SelectedValue);
            tmp.id_perfil = Conversions.ToInteger(cmb_perfiles.SelectedValue);

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
            e.KeyChar = Conversions.ToChar("");
        }

        private void cmb_perfiles_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Conversions.ToChar("");
        }
    }
}

