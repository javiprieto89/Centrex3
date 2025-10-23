using System;
using System.Globalization;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace Centrex
{
    public partial class add_ccCliente
    {
        public add_ccCliente()
        {
            InitializeComponent();
        }
        private void add_ccCliente_Load(object sender, EventArgs e)
        {
            string sqlstr;

            // form = Me ' Comentado para evitar error de compilación
            txt_saldo.Text = "0";
            chk_activo.Checked = true;

            // Cargo el combo con todos los clientes
            sqlstr = "SELECT c.id_cliente AS 'id_cliente', c.razon_social AS 'razon_social' FROM clientes AS c WHERE c.activo = '1' ORDER BY c.razon_social ASC";
            var argcombo = cmb_cliente;
            generales.Cargar_Combo(ref argcombo, sqlstr, VariablesGlobales.basedb, "razon_social", Conversions.ToInteger("id_cliente"));
            cmb_cliente = argcombo;

            // Cargo el combo con todas las monedas
            sqlstr = "SELECT id_moneda, moneda FROM sysMoneda ORDER BY moneda ASC";
            var argcombo1 = cmb_moneda;
            generales.Cargar_Combo(ref argcombo1, sqlstr, VariablesGlobales.basedb, "moneda", Conversions.ToInteger("id_moneda"));
            cmb_moneda = argcombo1;

            if (VariablesGlobales.edicion | VariablesGlobales.borrado)
            {
                cmb_cliente.SelectedValue = VariablesGlobales.edita_ccCliente.IdCliente;
                cmb_moneda.SelectedValue = VariablesGlobales.edita_ccCliente.IdMoneda;
                txt_nombre.Text = VariablesGlobales.edita_ccCliente.Nombre;
                txt_saldo.Text = VariablesGlobales.edita_ccCliente.Saldo.ToString("0.###", CultureInfo.CurrentCulture);
                chk_activo.Checked = VariablesGlobales.edita_ccCliente.Activo;
                cmb_cliente.Enabled = false; // No se puede cambiar el cliente de una cuenta corriente dada de alta
                cmb_moneda.Enabled = false; // No se puede cambiar la moneda de una cuenta corriente dada de alta
                                            // No se puede cambiar el saldo de una cuenta corriente ya dada de alta
                                            // Deberá hacerse a traves de un documento de saldo inicial deudor o acreedor.
                txt_saldo.Enabled = false;

                chk_secuencia.Enabled = false;
            }
            else
            {
                cmb_cliente.Text = "Seleccione un cliente...";
                cmb_moneda.Text = "Seleccione una moneda...";
            }

            if (VariablesGlobales.borrado)
            {
                txt_nombre.Enabled = false;
                chk_activo.Enabled = false;
                cmd_ok.Enabled = false;
                cmd_exit.Enabled = false;

                Show();

                if (Interaction.MsgBox("¿Está seguro que desea borrar esta cuenta corriente?", (MsgBoxStyle)((int)Constants.vbYesNo + (int)Constants.vbQuestion)) == MsgBoxResult.Yes)
                {
                    if (ccClientes.borrarccCliente(VariablesGlobales.edita_ccCliente) == false)
                    {
                        if (Interaction.MsgBox("Ocurrió un error al realizar el borrado de la cuenta corriente, ¿desea intetar desactivarla para que no aparezca en la búsqueda?", (MsgBoxStyle)((int)MsgBoxStyle.Question + (int)MsgBoxStyle.YesNo)) == Constants.vbYes)
                        {
                            // Realizo un borrado lógico
                            if (ccClientes.updateCCCliente(VariablesGlobales.edita_ccCliente, true) == true)
                            {
                                Interaction.MsgBox("Se ha podido realizar un borrado lógico, pero la cuenta corriente no se borró definitivamente." + "\r" + "Esto posiblemente se deba a que la cuenta corriente, tiene operaciones realizadas y por lo tanto no podrá borrarse", Constants.vbInformation);
                            }
                            else
                            {
                                Interaction.MsgBox("No se ha podido borrar la cuenta corriente.");
                            }
                        }
                    }
                    else if (ccClientes.info_ccCliente(VariablesGlobales.edita_ccCliente.IdCc).Nombre != "error")
                    {
                        Interaction.MsgBox("Cada cliente debe tener por lo menos una cuenta corriente, y este cliente tiene solo una, por lo cual no puede ser borrada", (MsgBoxStyle)((int)Constants.vbExclamation + (int)Constants.vbOKOnly), "Centrex");
                    }
                }
                closeandupdate(this);
            }
        }

        private void cmd_ok_Click(object sender, EventArgs e)
        {
            if (cmb_cliente.Text == "Seleccione un cliente...")
            {
                Interaction.MsgBox("El campo 'Cliente' es obligatorio y está vacio", (MsgBoxStyle)((int)Constants.vbExclamation + (int)Constants.vbOKOnly), "Centrex");
                return;
            }
            else if (cmb_moneda.Text == "Seleccione una moneda...")
            {
                Interaction.MsgBox("El campo 'Moneda' es obligatorio y está vacio", (MsgBoxStyle)((int)Constants.vbExclamation + (int)Constants.vbOKOnly), "Centrex");
                return;
            }
            else if (string.IsNullOrEmpty(txt_nombre.Text))
            {
                Interaction.MsgBox("El campo nombre es obligatorio y está vacio.", (MsgBoxStyle)((int)Constants.vbExclamation + (int)Constants.vbOKOnly), "Centrex");
                return;
            }

            var tmp = new ccCliente();

            tmp.IdCliente = Conversions.ToInteger(cmb_cliente.SelectedValue);
            tmp.IdMoneda = Conversions.ToInteger(cmb_moneda.SelectedValue);
            tmp.Nombre = txt_nombre.Text;
            tmp.Saldo = Conversions.ToDecimal(txt_saldo.Text);
            tmp.Activo = chk_activo.Checked;

            if (VariablesGlobales.edicion == true)
            {
                tmp.IdCliente = VariablesGlobales.edita_ccCliente.IdCliente;
                tmp.IdCc = VariablesGlobales.edita_ccCliente.IdCc;
                if (ccClientes.updateCCCliente(tmp) == false)
                {
                    Interaction.MsgBox("Hubo un problema al actualizar la cuenta corriente, consulte con su programdor", (MsgBoxStyle)((int)Constants.vbExclamation + (int)Constants.vbOKOnly), "Centrex");
                    closeandupdate(this);
                }
            }
            else
            {
                ccClientes.addCCCliente(tmp);
            }

            if (chk_secuencia.Checked)
            {
                txt_nombre.Text = "";
                txt_saldo.Text = "0";
                chk_activo.Checked = true;
                ActiveControl = cmb_cliente;
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

        private void pic_searchCliente_Click(object sender, EventArgs e)
        {
            // busqueda
            string tmp;
            tmp = VariablesGlobales.tabla;
            VariablesGlobales.tabla = "clientes";
            Enabled = false;
            My.MyProject.Forms.search.ShowDialog();
            VariablesGlobales.tabla = tmp;

            // Establezco la opción del combo, si es 0 elijo el cliente default
            if (VariablesGlobales.id == 0)
                VariablesGlobales.id = VariablesGlobales.id_cliente_pedido_default;
            var argcmb = cmb_cliente;
            generales.updateform(VariablesGlobales.id.ToString(), ref argcmb);
            cmb_cliente = argcmb;
        }
    }
}

