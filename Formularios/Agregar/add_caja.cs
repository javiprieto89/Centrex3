using System;
using System.Windows.Forms;

namespace Centrex
{
    public partial class add_caja
    {
        public add_caja()
        {
            InitializeComponent();
        }
        private void cmd_ok_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_caja.Text))
            {
                Interaction.MsgBox("El campo 'Caja' es obligatorio y está vacio");
            }

            var tmp = new CajaEntity();

            tmp.Nombre = txt_caja.Text;
            tmp.EsCc = chk_cc.Checked;
            tmp.Activo = chk_activo.Checked;

            if (edicion == true)
            {
                tmp.IdCaja = edita_caja.IdCaja;
                if (cajas.updateCaja(tmp) == false)
                {
                    Interaction.MsgBox("Hubo un problema al actualizar la caja, consulte con su programdor", Constants.vbExclamation);
                    closeandupdate(this);
                }
            }
            else
            {
                cajas.addCaja(tmp);
            }

            if (chk_secuencia.Checked == true)
            {
                txt_caja.Text = "";
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

        private void Add_caja_FormClosed(object sender, FormClosedEventArgs e)
        {
            closeandupdate(this);
        }

        private void Add_caja_Load(object sender, EventArgs e)
        {
            chk_activo.Checked = true;
            if (edicion == true | borrado == true)
            {
                chk_secuencia.Enabled = false;
                txt_caja.Text = edita_caja.Nombre;
                chk_cc.Checked = edita_caja.EsCc;
                chk_activo.Checked = edita_caja.Activo;
            }

            if (borrado == true)
            {
                txt_caja.Enabled = false;
                chk_activo.Enabled = false;
                cmd_ok.Visible = false;
                cmd_exit.Visible = false;
                Show();
                if (Interaction.MsgBox("¿Está seguro que desea borrar esta caja?", (MsgBoxStyle)((int)Constants.vbYesNo + (int)Constants.vbQuestion)) == MsgBoxResult.Yes)
                {
                    if (cajas.borrarCaja(edita_caja) == false)
                    {
                        if (Interaction.MsgBox("Ocurrió un error al realizar el borrado de la caja, ¿desea intectar desactivarlo para que no aparezca en la búsqueda?", (MsgBoxStyle)((int)MsgBoxStyle.Question + (int)MsgBoxStyle.YesNo)) == Constants.vbYes)
                        {
                            // Realizo un borrado lógico
                            if (cajas.updateCaja(edita_caja, true) == true)
                            {
                                Interaction.MsgBox("Se ha podido realizar un borrado lógico, pero la caja no se borró definitivamente." + "\r" + "Esto posiblemente se deba a que la caja, tiene operaciones realizadas y por lo tanto no podrá borrarse", Constants.vbInformation);
                            }
                            else
                            {
                                Interaction.MsgBox("No se ha podido borrar la caja, consulte con el programador");
                            }
                        }
                    }
                }
                closeandupdate(this);
            }
        }
    }
}


