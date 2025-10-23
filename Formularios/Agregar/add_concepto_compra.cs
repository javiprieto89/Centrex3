using System;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace Centrex
{
    public partial class add_concepto_compra
    {
        public add_concepto_compra()
        {
            InitializeComponent();
        }
        private void cmd_ok_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_concepto.Text))
            {
                Interaction.MsgBox("El campo 'concepto' es obligatorio y está vacio");
                return;
            }

            var tmp = new concepto_compra();

            tmp.concepto = txt_concepto.Text;
            tmp.activo = chk_activo.Checked;

            if (VariablesGlobales.edicion == true)
            {
                tmp.id_concepto_compra = VariablesGlobales.edita_concepto_compra.id_concepto_compra;
                if (conceptos_compra.updateConcepto_compra(tmp) == false)
                {
                    Interaction.MsgBox("Hubo un problema al actualizar la concepto, consulte con su programdor", Constants.vbExclamation);
                    closeandupdate(this);
                }
            }
            else
            {
                conceptos_compra.addconcepto_compra(tmp);
            }

            if (chk_secuencia.Checked == true)
            {
                txt_concepto.Text = "";
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

        private void Add_marcai_FormClosed(object sender, FormClosedEventArgs e)
        {
            closeandupdate(this);
        }

        private void Add_concepto_Load(object sender, EventArgs e)
        {

            chk_activo.Checked = true;
            if (VariablesGlobales.edicion == true | VariablesGlobales.borrado == true)
            {
                chk_secuencia.Enabled = false;
                txt_concepto.Text = VariablesGlobales.edita_concepto_compra.concepto;
                chk_activo.Checked = VariablesGlobales.edita_concepto_compra.activo;
            }

            if (VariablesGlobales.borrado == true)
            {
                txt_concepto.Enabled = false;
                chk_activo.Enabled = false;
                cmd_ok.Visible = false;
                cmd_exit.Visible = false;
                Show();
                if (Interaction.MsgBox("¿Está seguro que desea borrar esta condición?", (MsgBoxStyle)((int)Constants.vbYesNo + (int)Constants.vbQuestion)) == MsgBoxResult.Yes)
                {
                    if (conceptos_compra.borrarconcepto_compra(VariablesGlobales.edita_concepto_compra) == false)
                    {
                        if (Interaction.MsgBox("Ocurrió un error al realizar el borrado de la condición, ¿desea intectar desactivarlo para que no aparezca en la búsqueda?", (MsgBoxStyle)((int)MsgBoxStyle.Question + (int)MsgBoxStyle.YesNo)) == Constants.vbYes)
                        {
                            // Realizo un borrado lógico
                            if (conceptos_compra.updateconcepto_compra(VariablesGlobales.edita_concepto_compra, true) == true)
                            {
                                Interaction.MsgBox("Se ha podido realizar un borrado lógico, pero la condición no se borró definitivamente." + "\r" + "Esto posiblemente se deba a que la condición, tiene operaciones realizadas y por lo tanto no podrá borrarse", Constants.vbInformation);
                            }
                            else
                            {
                                Interaction.MsgBox("No se ha podido borrar la condición, consulte con el programador");
                            }
                        }
                    }
                }
                closeandupdate(this);
            }
        }
    }
}
