using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.VisualBasic;

namespace Centrex
{
    public partial class add_ccProveedor
    {
        public add_ccProveedor()
        {
            InitializeComponent();
        }
        private static List<Tuple<string, bool>> OrdenAsc(string columna) =>
            new List<Tuple<string, bool>> { Tuple.Create(columna, true) };

        private void add_ccProveedor_Load(object sender, EventArgs e)
        {
            // form = Me ' Comentado para evitar error de compilación
            txt_saldo.Text = "0";
            chk_activo.Checked = true;

            CargarProveedores();
            CargarMonedas();

            if (VariablesGlobales.edicion | VariablesGlobales.borrado)
            {
                cmb_proveedor.SelectedValue = VariablesGlobales.edita_ccProveedor.IdProveedor;
                cmb_moneda.SelectedValue = VariablesGlobales.edita_ccProveedor.IdMoneda;
                txt_nombre.Text = VariablesGlobales.edita_ccProveedor.Nombre;
                txt_saldo.Text = VariablesGlobales.edita_ccProveedor.Saldo.ToString("0.###", CultureInfo.CurrentCulture);
                chk_activo.Checked = VariablesGlobales.edita_ccProveedor.Activo;
                cmb_proveedor.Enabled = false; // No se puede cambiar el proveedor de una cuenta corriente dada de alta
                cmb_moneda.Enabled = false; // No se puede cambiar la moneda de una cuenta corriente ya dada de alta
                                            // No se puede cambiar el saldo de una cuenta corriente ya dada de alta
                                            // Deberá hacerse a traves de un documento de saldo inicial deudor o acreedor.
                txt_saldo.Enabled = false;

                chk_secuencia.Enabled = false;
            }
            else
            {
                cmb_proveedor.SelectedIndex = -1;
                cmb_proveedor.Text = "Seleccione un proveedor...";
                cmb_moneda.SelectedIndex = -1;
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
                    if (ccProveedores.borrarccProveedor(VariablesGlobales.edita_ccProveedor) == false)
                    {
                        if (Interaction.MsgBox("Ocurrió un error al realizar el borrado de la cuenta corriente, ¿desea intetar desactivarla para que no aparezca en la búsqueda?", (MsgBoxStyle)((int)MsgBoxStyle.Question + (int)MsgBoxStyle.YesNo)) == Constants.vbYes)
                        {
                            // Realizo un borrado lógico
                            if (ccProveedores.updateCCProveedor(VariablesGlobales.edita_ccProveedor, true) == true)
                            {
                                Interaction.MsgBox("Se ha podido realizar un borrado lógico, pero la cuenta corriente no se borró definitivamente." + "\r" + "Esto posiblemente se deba a que la cuenta corriente, tiene operaciones realizadas y por lo tanto no podrá borrarse", Constants.vbInformation);
                            }
                            else
                            {
                                Interaction.MsgBox("No se ha podido borrar la cuenta corriente.");
                            }
                        }
                    }
                    else if (ccProveedores.info_ccProveedor(VariablesGlobales.edita_ccProveedor.IdCc).Nombre != "error")
                    {
                        Interaction.MsgBox("Cada proveedor debe tener por lo menos una cuenta corriente, y este proveedor tiene solo una, por lo cual no puede ser borrada", (MsgBoxStyle)((int)Constants.vbExclamation + (int)Constants.vbOKOnly), "Centrex");
                    }
                }
                closeandupdate(this);
            }
        }

        private void cmd_ok_Click(object sender, EventArgs e)
        {
            if (cmb_proveedor.SelectedValue is null)
            {
                Interaction.MsgBox("El campo 'Proveedor' es obligatorio y está vacio", (MsgBoxStyle)((int)Constants.vbExclamation + (int)Constants.vbOKOnly), "Centrex");
                return;
            }
            else if (cmb_moneda.SelectedValue is null)
            {
                Interaction.MsgBox("El campo 'Moneda' es obligatorio y está vacio", (MsgBoxStyle)((int)Constants.vbExclamation + (int)Constants.vbOKOnly), "Centrex");
                return;
            }
            else if (string.IsNullOrEmpty(txt_nombre.Text))
            {
                Interaction.MsgBox("El campo nombre es obligatorio y está vacio.", (MsgBoxStyle)((int)Constants.vbExclamation + (int)Constants.vbOKOnly), "Centrex");
                return;
            }

            var tmp = new CcProveedorEntity();

            tmp.IdProveedor = Convert.ToInt32(cmb_proveedor.SelectedValue);
            tmp.IdMoneda = Convert.ToInt32(cmb_moneda.SelectedValue);
            tmp.Nombre = txt_nombre.Text;
            tmp.Saldo = Convert.ToDecimal(txt_saldo.Text);
            tmp.Activo = chk_activo.Checked;

            if (VariablesGlobales.edicion == true)
            {
                tmp.IdProveedor = VariablesGlobales.edita_ccProveedor.IdProveedor;
                tmp.IdCc = VariablesGlobales.edita_ccProveedor.IdCc;
                if (ccProveedores.updateCCProveedor(tmp) == false)
                {
                    Interaction.MsgBox("Hubo un problema al actualizar la cuenta corriente, consulte con su programdor", (MsgBoxStyle)((int)Constants.vbExclamation + (int)Constants.vbOKOnly), "Centrex");
                    closeandupdate(this);
                }
            }
            else
            {
                ccProveedores.addCCProveedor(tmp);
            }

            if (chk_secuencia.Checked)
            {
                txt_nombre.Text = "";
                txt_saldo.Text = "0";
                chk_activo.Checked = true;
                cmb_proveedor.SelectedIndex = -1;
                cmb_proveedor.Text = "Seleccione un proveedor...";
                cmb_moneda.SelectedIndex = -1;
                cmb_moneda.Text = "Seleccione una moneda...";
                ActiveControl = cmb_proveedor;
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

        private void pic_searchProveedor_Click(object sender, EventArgs e)
        {
            // busqueda
            string tmp;
            tmp = VariablesGlobales.tabla;
            VariablesGlobales.tabla = "proveedores";
            Enabled = false;
            My.MyProject.Forms.search.ShowDialog();
            VariablesGlobales.tabla = tmp;

            // Establezco la opción del combo, si es 0 elijo el proveedor default
            if (VariablesGlobales.id == 0)
                VariablesGlobales.id = VariablesGlobales.id_proveedor_default;
            var argcmb = cmb_proveedor;
            generales.updateform(VariablesGlobales.id.ToString(), ref argcmb);
            cmb_proveedor = argcmb;
        }

        private void CargarProveedores()
        {
            var orden = OrdenAsc("RazonSocial");
            generales.Cargar_Combo(
                ref cmb_proveedor,
                entidad: "ProveedorEntity",
                displaymember: "RazonSocial",
                valuemember: "IdProveedor",
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

