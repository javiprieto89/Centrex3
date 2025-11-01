using System;
using System.Windows.Forms;

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

            var tmp = new ConceptoCompraEntity();

            tmp.Concepto = txt_concepto.Text;
            tmp.Activo = chk_activo.Checked;

            if (edicion == true)
            {
                tmp.IdConceptoCompra = edita_concepto_compra.IdConceptoCompra;
                if (conceptos_compra.updateConcepto_compra(tmp) == false)
                {
                    Interaction.MsgBox("Hubo un problema al actualizar el concepto, consulte con su programador", Constants.vbExclamation);
                    closeandupdate(this);
                }
            }
            else
            {
                conceptos_compra.addConcepto_compra(tmp);
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
            if (edicion == true | borrado == true)
            {
                chk_secuencia.Enabled = false;
                txt_concepto.Text = edita_concepto_compra.Concepto;
                chk_activo.Checked = edita_concepto_compra.Activo;
            }

            if (borrado == true)
            {
                txt_concepto.Enabled = false;
                chk_activo.Enabled = false;
                cmd_ok.Visible = false;
                cmd_exit.Visible = false;
                Show();
                if (Interaction.MsgBox("¿Está seguro que desea borrar este concepto?", (MsgBoxStyle)((int)Constants.vbYesNo + (int)Constants.vbQuestion)) == MsgBoxResult.Yes)
                {
                    if (conceptos_compra.borrarConcepto_compra(edita_concepto_compra) == false)
                    {
                        if (Interaction.MsgBox("Ocurrió un error al realizar el borrado del concepto, ¿desea intentar desactivarlo para que no aparezca en la búsqueda?", (MsgBoxStyle)((int)MsgBoxStyle.Question + (int)MsgBoxStyle.YesNo)) == Constants.vbYes)
                        {
                            // Realizo un borrado lógico
                            if (conceptos_compra.updateConcepto_compra(edita_concepto_compra, true) == true)
                            {
                                Interaction.MsgBox("Se ha podido realizar un borrado lógico, pero el concepto no se borró definitivamente." + "\r" + "Esto posiblemente se deba a que el concepto tiene operaciones realizadas y por lo tanto no podrá borrarse", Constants.vbInformation);
                            }
                            else
                            {
                                Interaction.MsgBox("No se ha podido borrar el concepto, consulte con el programador");
                            }
                        }
                    }
                }
                closeandupdate(this);
            }
        }
    }
}
