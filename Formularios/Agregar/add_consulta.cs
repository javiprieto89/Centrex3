using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Centrex.Models;

namespace Centrex
{
    public partial class add_consulta : Form
    {
        public add_consulta()
        {
            InitializeComponent();
        }
      
        private void cmd_ok_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_nombre.Text))
            {
                Interaction.MsgBox("El campo 'Nombre de la Consulta' es obligatorio y está vacio");
                return;
            }
            else if (string.IsNullOrEmpty(txt_consulta.Text))
            {
                Interaction.MsgBox("El campo 'Consulta SQL' es obligatorio y está vacio");
                return;
            }

            var tmp = new ConsultaPersonalizadaEntity();

            tmp.Nombre = txt_nombre.Text;
            tmp.Consulta = txt_consulta.Text;
            tmp.Activo = chk_activo.Checked;

            if (VariablesGlobales.edicion == true)
            {
                tmp.IdConsulta = VariablesGlobales.edita_Consulta.IdConsulta;
                if (consultas.updateConsulta(tmp) == false)
                {
                    Interaction.MsgBox("Hubo un problema al actualizar la Consulta, consulte con su programador", Constants.vbExclamation);
                    closeandupdate(this);
                }
            }
            else
            {
                consultas.addConsulta(tmp);
            }

            if (chk_secuencia.Checked == true)
            {
                txt_nombre.Text = "";
                txt_consulta.Text = "";
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

        private void add_Consulta_FormClosed(object sender, FormClosedEventArgs e)
        {
            closeandupdate(this);
        }

        private void add_Consulta_Load(object sender, EventArgs e)
        {
            chk_activo.Checked = true;
            if (VariablesGlobales.edicion == true | VariablesGlobales.borrado == true)
            {
                chk_secuencia.Enabled = false;
                txt_nombre.Text = VariablesGlobales.edita_Consulta.Nombre;
                txt_consulta.Text = VariablesGlobales.edita_Consulta.Consulta;
                chk_activo.Checked = VariablesGlobales.edita_Consulta.Activo;
            }

            if (VariablesGlobales.borrado == true)
            {
                txt_nombre.Enabled = false;
                txt_consulta.Enabled = false;
                chk_activo.Enabled = false;
                cmd_ok.Visible = false;
                cmd_exit.Visible = false;
                Show();
                if (Interaction.MsgBox("¿Está seguro que desea borrar esta Consulta?", (MsgBoxStyle)((int)Constants.vbYesNo + (int)Constants.vbQuestion)) == MsgBoxResult.Yes)
                {
                    if (consultas.borrarConsulta(VariablesGlobales.edita_Consulta) == false)
                    {
                        if (Interaction.MsgBox("Ocurrió un error al realizar el borrado de la Consulta, ¿desea intentar desactivarla para que no aparezca en la búsqueda?", (MsgBoxStyle)((int)MsgBoxStyle.Question + (int)MsgBoxStyle.YesNo)) == Constants.vbYes)
                        {
                            // Realizo un borrado lógico
                            if (consultas.updateConsulta(VariablesGlobales.edita_Consulta, true) == true)
                            {
                                Interaction.MsgBox("Se ha podido realizar un borrado lógico, pero la Consulta no se borró definitivamente." + "\r" + "Esto posiblemente se deba a que la Consulta tiene operaciones realizadas y por lo tanto no podrá borrarse", Constants.vbInformation);
                            }
                            else
                            {
                                Interaction.MsgBox("No se ha podido borrar la Consulta, consulte con el programador");
                            }
                        }
                    }
                }
                closeandupdate(this);
            }
        }
    }
}

