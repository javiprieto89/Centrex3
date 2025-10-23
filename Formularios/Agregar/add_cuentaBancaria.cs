using System;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace Centrex
{
    public partial class add_cuentaBancaria
    {
        public add_cuentaBancaria()
        {
            InitializeComponent();
        }
        private void add_cuentaBancaria_Load(object sender, EventArgs e)
        {
            // form = Me ' Comentado para evitar error de compilación

            // Cargo todos los bancos
            int localCargar_Combo() { var argcombo = cmb_banco; var ret = generales.Cargar_Combo(ref argcombo, "SELECT IdBanco, nombre FROM bancos ORDER BY nombre ASC", VariablesGlobales.basedb, "nombre", Conversions.ToInteger("IdBanco")); cmb_banco = argcombo; return ret; }

            if (localCargar_Combo() == -1)
            {
                Interaction.MsgBox("No hay ningún banco cargado, por lo cual no podrá cargar ninguna cuenta bancaria", (MsgBoxStyle)((int)Constants.vbExclamation + (int)Constants.vbOKOnly), "Centrex");
                closeandupdate(this);
                return;
            }

            // Cargo todas las monedas
            var argcombo = cmb_moneda;
            generales.Cargar_Combo(ref argcombo, "SELECT IdMoneda, moneda FROM sysMoneda ORDER BY IdMoneda ASC", VariablesGlobales.basedb, "moneda", Conversions.ToInteger("IdMoneda"));
            cmb_moneda = argcombo;

            if (VariablesGlobales.edicion | VariablesGlobales.borrado)
            {
                cmb_banco.SelectedValue = VariablesGlobales.edita_cuentaBancaria.IdBanco;
                txt_cuentaBancaria.Text = VariablesGlobales.edita_cuentaBancaria.nombre;
                cmb_moneda.SelectedValue = VariablesGlobales.edita_cuentaBancaria.IdMoneda;
            }
            else
            {
                cmb_banco.Text = "Seleccione un banco...";
                cmb_moneda.Text = "Seleccione una moneda...";
            }

            if (VariablesGlobales.borrado)
            {
                cmb_banco.Enabled = false;
                txt_cuentaBancaria.Enabled = false;
                cmb_moneda.Enabled = false;
                cmd_ok.Enabled = false;
                cmd_exit.Enabled = false;
                Show();
                if (Interaction.MsgBox("¿Está seguro que desea borrar esta cuenta bancaria?", (MsgBoxStyle)((int)Constants.vbYesNo + (int)Constants.vbQuestion)) == MsgBoxResult.Yes)
                {
                    if (cuentas_bancarias.borrarcuenta_Bancaria(VariablesGlobales.edita_cuentaBancaria) == false)
                    {
                        if (Interaction.MsgBox("Ocurrió un error al realizar el borrado de la cuenta, ¿desea intectar desactivarla para que no aparezca en la búsqueda?", (MsgBoxStyle)((int)MsgBoxStyle.Question + (int)MsgBoxStyle.YesNo)) == Constants.vbYes)
                        {
                            // Realizo un borrado lógico
                            if (cuentas_bancarias.updatecuentaBancaria(VariablesGlobales.edita_cuentaBancaria, true) == true)
                            {
                                Interaction.MsgBox("Se ha podido realizar un borrado lógico, pero la cuenta no se borró definitivamente." + "\r" + "Esto posiblemente se deba a que la cuenta, tiene operaciones realizadas y por lo tanto no podrá borrarse", Constants.vbInformation);
                            }
                            else
                            {
                                Interaction.MsgBox("No se ha podido borrar la cuenta, consulte con el programador");
                            }
                        }
                    }
                }
                closeandupdate(this);
            }
        }

        private void cmd_exit_Click(object sender, EventArgs e)
        {
            closeandupdate(this);
        }

        private void cmd_ok_Click(object sender, EventArgs e)
        {
            if (cmb_banco.Text == "Seleccione un banco...")
            {
                Interaction.MsgBox("Debe seleccionar un banco.", (MsgBoxStyle)((int)Constants.vbExclamation + (int)Constants.vbOKOnly), "Centrex");
                return;
            }
            else if (cmb_moneda.Text == "Seleccione una moneda...")
            {
                Interaction.MsgBox("Debe seleccionar una moneda", (MsgBoxStyle)((int)Constants.vbExclamation + (int)Constants.vbOKOnly), "Centrex");
                return;
            }
            else if (string.IsNullOrEmpty(txt_cuentaBancaria.Text))
            {
                Interaction.MsgBox("El campo 'Descripción' es obligatorio y está vacio", (MsgBoxStyle)((int)Constants.vbExclamation + (int)Constants.vbOKOnly), "Centrex");
                return;
            }

            var cb = new CuentaBancariaEntity();

            cb.IdBanco = cmb_banco.SelectedValue;
            cb.nombre = txt_cuentaBancaria.Text;
            cb.IdMoneda = cmb_moneda.SelectedValue;
            cb.activo = chk_activo.Checked;

            if (VariablesGlobales.edicion)
            {
                cb.IdCuentaBancaria = VariablesGlobales.edita_cuentaBancaria.IdCuentaBancaria;
                if (!cuentas_bancarias.updatecuentaBancaria(cb))
                {
                    Interaction.MsgBox("Hubo un problema al actualizar la cuenta bancaria, consulte con su programdor", (MsgBoxStyle)((int)Constants.vbExclamation + (int)Constants.vbOKOnly), "Centrex");
                    closeandupdate(this);
                }
            }
            else
            {
                cuentas_bancarias.addcuentaBancaria(cb);
            }

            if (chk_secuencia.Checked)
            {
                cmb_banco.SelectedValue = 1;
                txt_cuentaBancaria.Text = "";
                cmb_moneda.SelectedValue = 1;
                chk_activo.Checked = true;
            }
            else
            {
                closeandupdate(this);
            }
        }

        private void cmb_banco_KeyPress(object sender, KeyPressEventArgs e)
        {
            // e.KeyChar = ""
        }

        private void cmb_moneda_KeyPress(object sender, KeyPressEventArgs e)
        {
            // e.KeyChar = ""
        }

        private void psearch_banco_Click(object sender, EventArgs e)
        {
            string tmp;
            tmp = VariablesGlobales.tabla;
            VariablesGlobales.tabla = "bancos";
            Enabled = false;
            My.MyProject.Forms.search.ShowDialog();
            VariablesGlobales.tabla = tmp;

            // Establezco la opción del combo
            cmb_banco.SelectedValue = VariablesGlobales.id;
            // cmb_cat.SelectedIndex = id
            VariablesGlobales.id = 0;
        }

        private void add_cuentaBancaria_FormClosed(object sender, FormClosedEventArgs e)
        {
            closeandupdate(this);
        }
    }
}
