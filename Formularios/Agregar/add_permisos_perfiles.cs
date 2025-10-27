using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace Centrex
{
    public partial class add_permisos_perfiles
    {
        public add_permisos_perfiles()
        {
            InitializeComponent();
        }
        private void add_permisos_perfiles_Load(object sender, EventArgs e)
        {
            var ordenNombre = new List<Tuple<string, bool>> { Tuple.Create("Nombre", true) };

            // Cargo todos los permisos
            var argPermisos = cmb_permisos;
            generales.Cargar_Combo(
                ref argPermisos,
                entidad: "PermisoEntity",
                displaymember: "Nombre",
                valuemember: "IdPermiso",
                predet: -1,
                soloActivos: false,
                filtros: null,
                orden: ordenNombre);
            cmb_permisos = argPermisos;
            cmb_permisos.SelectedIndex = -1;
            cmb_permisos.Text = "Seleccione un permiso...";

            // Cargo todos los perfiles
            var argPerfiles = cmb_perfiles;
            generales.Cargar_Combo(
                ref argPerfiles,
                entidad: "PerfilEntity",
                displaymember: "Nombre",
                valuemember: "IdPerfil",
                predet: -1,
                soloActivos: true,
                filtros: null,
                orden: ordenNombre);
            cmb_perfiles = argPerfiles;
            cmb_perfiles.SelectedIndex = -1;
            cmb_perfiles.Text = "Seleccione un perfil...";


            if (VariablesGlobales.edicion == true | VariablesGlobales.borrado == true)
            {
                chk_secuencia.Enabled = false;
                cmb_permisos.SelectedValue = VariablesGlobales.edita_permiso_perfil.id_permiso;
                cmb_perfiles.SelectedValue = VariablesGlobales.edita_permiso_perfil.id_perfil;
                cmb_permisos.Enabled = false;
                cmb_perfiles.Enabled = false;
            }

            if (VariablesGlobales.borrado == true)
            {
                cmd_exit.Visible = false;
                Show();
                if (Interaction.MsgBox("¿Está seguro que desea borrar esta relación entre el perfil y el permiso?", (MsgBoxStyle)((int)Constants.vbYesNo + (int)Constants.vbQuestion)) == MsgBoxResult.Yes)
                {
                    // If (borrarTarjeta(VariablesGlobales.edita_tarjeta)) = False Then
                    // MsgBox("No se ha podido borrar la relación, consulte con el programador")
                    // End If
                }
                closeandupdate(this);
            }
            else if (VariablesGlobales.edicion == true)
            {
                Interaction.MsgBox("La relación entre un permiso y un perfil no puede editarse", (MsgBoxStyle)((int)Constants.vbExclamation + (int)Constants.vbOKOnly), "Centrex");
                closeandupdate(this);
            }
        }

        private void cmd_ok_Click(object sender, EventArgs e)
        {
            if (cmb_permisos.Text == "Seleccione un permiso...")
            {
                Interaction.MsgBox("Debe seleccionar un permiso");
                return;
            }
            else if (cmb_perfiles.Text == "Seleccione un perfil...")
            {
                Interaction.MsgBox("Debe seleccionar un perfil");
                return;
            }

            var tmp = new perfil_permiso();


            tmp.id_permiso = Conversions.ToInteger(cmb_permisos.SelectedValue);
            tmp.id_perfil = Conversions.ToInteger(cmb_perfiles.SelectedValue);

            usuarios.add_permiso_perfil(tmp);

            if (chk_secuencia.Checked == true)
            {
                cmb_perfiles.Text = "Selecione un permiso...";
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

        private void add_permisos_perfiles_FormClosed(object sender, FormClosedEventArgs e)
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

