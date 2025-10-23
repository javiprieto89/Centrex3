using System;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace Centrex
{
    public partial class add_marcai
    {
        public add_marcai()
        {
            InitializeComponent();
        }
        private void cmd_ok_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_marca.Text))
            {
                Interaction.MsgBox("El campo 'Marca' es obligatorio y está vacio", (MsgBoxStyle)((int)Constants.vbExclamation + (int)Constants.vbOKOnly), "Centrex");
                return;
            }

            var tmp = new MarcaEntity();

            tmp.Marca = txt_marca.Text;
            tmp.Activo = chk_activo.Checked;

            if (VariablesGlobales.edicion == true)
            {
                tmp.IdMarca = VariablesGlobales.edita_marcai.IdMarca;
                if (marcasitems.updatemarcai(tmp) == false)
                {
                    Interaction.MsgBox("Hubo un problema al actualizar la marca, consulte con su programdor", Constants.vbExclamation);
                    closeandupdate(this);
                }
            }
            else
            {
                marcasitems.addmarcai(tmp);
            }

            if (chk_secuencia.Checked == true)
            {
                txt_marca.Text = "";
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

        private void add_marcai_FormClosed(object sender, FormClosedEventArgs e)
        {
            closeandupdate(this);
        }

        private void add_marcai_Load(object sender, EventArgs e)
        {
            chk_activo.Checked = true;
            if (VariablesGlobales.edicion == true | VariablesGlobales.borrado == true)
            {
                chk_secuencia.Enabled = false;
                txt_marca.Text = VariablesGlobales.edita_marcai.marca;
                chk_activo.Checked = VariablesGlobales.edita_marcai.activo;
            }

            if (VariablesGlobales.borrado == true)
            {
                txt_marca.Enabled = false;
                chk_activo.Enabled = false;
                cmd_ok.Visible = false;
                cmd_exit.Visible = false;
                Show();
                if (Interaction.MsgBox("¿Está seguro que desea borrar esta marca de auto?", (MsgBoxStyle)((int)Constants.vbYesNo + (int)Constants.vbQuestion)) == MsgBoxResult.Yes)
                {
                    if (marcasitems.borrarmarcai(VariablesGlobales.edita_marcai) == false)
                    {
                        if (Interaction.MsgBox("Ocurrió un error al realizar el borrado de la marca, ¿desea intectar desactivarlo para que no aparezca en la búsqueda?", (MsgBoxStyle)((int)MsgBoxStyle.Question + (int)MsgBoxStyle.YesNo)) == Constants.vbYes)
                        {
                            // Realizo un borrado lógico
                            if (marcasitems.updatemarcai(VariablesGlobales.edita_marcai, true) == true)
                            {
                                Interaction.MsgBox("Se ha podido realizar un borrado lógico, pero la marca no se borró definitivamente." + "\r" + "Esto posiblemente se deba a que el cliente, tiene operaciones realizadas y por lo tanto no podrá borrarse", Constants.vbInformation);
                            }
                            else
                            {
                                Interaction.MsgBox("No se ha podido borrar el cliente, consulte con el programador");
                            }
                        }
                    }
                }
                closeandupdate(this);
            }
        }
    }
}
