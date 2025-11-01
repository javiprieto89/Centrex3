using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Centrex
{
    public partial class add_cuentaBancaria
    {
        public add_cuentaBancaria()
        {
            InitializeComponent();
        }
        private static List<Tuple<string, bool>> OrdenAsc(string columna) =>
            new List<Tuple<string, bool>> { Tuple.Create(columna, true) };

        private void add_cuentaBancaria_Load(object sender, EventArgs e)
        {
            // form = Me ' Comentado para evitar error de compilación

            var bancosCargados = CargarBancos();
            if (bancosCargados <= 0)
            {
                Interaction.MsgBox("No hay ningún banco cargado, por lo cual no podrá cargar ninguna cuenta bancaria", (MsgBoxStyle)((int)Constants.vbExclamation + (int)Constants.vbOKOnly), "Centrex");
                closeandupdate(this);
                return;
            }

            CargarMonedas();

            if (edicion | borrado)
            {
                cmb_banco.SelectedValue = edita_cuentaBancaria.IdBanco;
                txt_cuentaBancaria.Text = edita_cuentaBancaria.Nombre;
                cmb_moneda.SelectedValue = edita_cuentaBancaria.IdMoneda;
            }
            else
            {
                cmb_banco.SelectedIndex = -1;
                cmb_banco.Text = "Seleccione un banco...";
                cmb_moneda.SelectedIndex = -1;
                cmb_moneda.Text = "Seleccione una moneda...";
            }

            if (borrado)
            {
                cmb_banco.Enabled = false;
                txt_cuentaBancaria.Enabled = false;
                cmb_moneda.Enabled = false;
                cmd_ok.Enabled = false;
                cmd_exit.Enabled = false;
                Show();
                if (Interaction.MsgBox("¿Está seguro que desea borrar esta cuenta bancaria?", (MsgBoxStyle)((int)Constants.vbYesNo + (int)Constants.vbQuestion)) == MsgBoxResult.Yes)
                {
                    if (cuentas_bancarias.borrarcuenta_Bancaria(edita_cuentaBancaria) == false)
                    {
                        if (Interaction.MsgBox("Ocurrió un error al realizar el borrado de la cuenta, ¿desea intectar desactivarla para que no aparezca en la búsqueda?", (MsgBoxStyle)((int)MsgBoxStyle.Question + (int)MsgBoxStyle.YesNo)) == Constants.vbYes)
                        {
                            // Realizo un borrado lógico
                            if (cuentas_bancarias.updatecuentaBancaria(edita_cuentaBancaria, true) == true)
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
            if (cmb_banco.SelectedValue is null)
            {
                Interaction.MsgBox("Debe seleccionar un banco.", (MsgBoxStyle)((int)Constants.vbExclamation + (int)Constants.vbOKOnly), "Centrex");
                return;
            }
            else if (cmb_moneda.SelectedValue is null)
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

            cb.IdBanco = Convert.ToInt32(cmb_banco.SelectedValue);
            cb.Nombre = txt_cuentaBancaria.Text.Trim();
            cb.IdMoneda = Convert.ToInt32(cmb_moneda.SelectedValue);
            cb.Activo = chk_activo.Checked;

            if (edicion)
            {
                cb.IdCuentaBancaria = edita_cuentaBancaria.IdCuentaBancaria;
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
                cmb_banco.SelectedIndex = -1;
                cmb_banco.Text = "Seleccione un banco...";
                txt_cuentaBancaria.Text = "";
                cmb_moneda.SelectedIndex = -1;
                cmb_moneda.Text = "Seleccione una moneda...";
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
            tmp = tabla;
            tabla = "bancos";
            Enabled = false;
            My.MyProject.Forms.search.ShowDialog();
            tabla = tmp;

            // Establezco la opción del combo
            if (id > 0)
            {
                cmb_banco.SelectedValue = id;
            }
            id = 0;
        }

        private void add_cuentaBancaria_FormClosed(object sender, FormClosedEventArgs e)
        {
            closeandupdate(this);
        }

        private int CargarBancos()
        {
            var orden = OrdenAsc("Nombre");
            return generales.Cargar_Combo(
                ref cmb_banco,
                entidad: "BancoEntity",
                displaymember: "Nombre",
                valuemember: "IdBanco",
                predet: -1,
                soloActivos: true,
                filtros: null,
                orden: orden);
        }

        private void CargarMonedas()
        {
            var orden = OrdenAsc("Moneda");
            generales.Cargar_Combo(
                ref cmb_moneda,
                entidad: "SysMonedaEntity",
                displaymember: "Moneda",
                valuemember: "IdMoneda",
                predet: -1,
                soloActivos: false,
                filtros: null,
                orden: orden);
        }
    }
}
