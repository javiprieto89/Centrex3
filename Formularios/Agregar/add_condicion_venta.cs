using System;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Centrex.Models;

namespace Centrex
{
    public partial class add_condicion_venta
    {
        public add_condicion_venta()
        {
            InitializeComponent();
        }

        private void cmd_ok_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_condicion.Text))
            {
                Interaction.MsgBox("El campo 'Condicion' es obligatorio y está vacio");
                return;
            }
            else if (string.IsNullOrEmpty(txt_vencimiento.Text))
            {
                Interaction.MsgBox("El campo 'Vencimiento' es obligatorio y está vacio");
                return;
            }
            else if (string.IsNullOrEmpty(txt_recargo.Text))
            {
                Interaction.MsgBox("El campo 'Recargo' es obligatorio y está vacio");
                return;
            }

            var tmp = new CondicionVentaEntity();

            tmp.Condicion = txt_condicion.Text;
            tmp.Vencimiento = Conversions.ToInteger(txt_vencimiento.Text);
            tmp.Recargo = Conversions.ToDecimal(txt_recargo.Text);
            tmp.Activo = chk_activo.Checked;

            if (VariablesGlobales.edicion == true)
            {
                tmp.IdCondicionVenta = VariablesGlobales.edita_condicion_venta.IdCondicionVenta;
                if (condiciones_venta.updateCondicion_venta(tmp) == false)
                {
                    Interaction.MsgBox("Hubo un problema al actualizar la condición, consulte con su programador", Constants.vbExclamation);
                    closeandupdate(this);
                }
            }
            else
            {
                condiciones_venta.addCondicion_venta(tmp);
            }

            if (chk_secuencia.Checked == true)
            {
                txt_condicion.Text = "";
                txt_vencimiento.Text = "";
                txt_recargo.Text = "";
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

        private void Add_condicion_Load(object sender, EventArgs e)
        {
            chk_activo.Checked = true;
            if (VariablesGlobales.edicion == true | VariablesGlobales.borrado == true)
            {
                chk_secuencia.Enabled = false;
                txt_condicion.Text = VariablesGlobales.edita_condicion_venta.Condicion;
                txt_vencimiento.Text = VariablesGlobales.edita_condicion_venta.Vencimiento.ToString();
                txt_recargo.Text = VariablesGlobales.edita_condicion_venta.Recargo.ToString();
                chk_activo.Checked = VariablesGlobales.edita_condicion_venta.Activo;
            }

            if (VariablesGlobales.borrado == true)
            {
                txt_condicion.Enabled = false;
                txt_vencimiento.Enabled = false;
                txt_recargo.Enabled = false;
                chk_activo.Enabled = false;
                cmd_ok.Visible = false;
                cmd_exit.Visible = false;
                // rb_aplicaVentas.Enabled = False
                // rb_aplicaCompras.Enabled = False
                Show();
                if (Interaction.MsgBox("¿Está seguro que desea borrar esta condición?", (MsgBoxStyle)((int)Constants.vbYesNo + (int)Constants.vbQuestion)) == MsgBoxResult.Yes)
                {
                    if (condiciones_venta.borrarCondicion_venta(VariablesGlobales.edita_condicion_venta) == false)
                    {
                        if (Interaction.MsgBox("Ocurrió un error al realizar el borrado de la condición, ¿desea intentar desactivarla para que no aparezca en la búsqueda?", (MsgBoxStyle)((int)MsgBoxStyle.Question + (int)MsgBoxStyle.YesNo)) == Constants.vbYes)
                        {
                            // Realizo un borrado lógico
                            if (condiciones_venta.updateCondicion_venta(VariablesGlobales.edita_condicion_venta, true) == true)
                            {
                                Interaction.MsgBox("Se ha podido realizar un borrado lógico, pero la condición no se borró definitivamente." + "\r" + "Esto posiblemente se deba a que la condición tiene operaciones realizadas y por lo tanto no podrá borrarse", Constants.vbInformation);
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

