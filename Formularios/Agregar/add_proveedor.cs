using System;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace Centrex
{
    public partial class add_proveedor
    {
        public add_proveedor()
        {
            InitializeComponent();
        }
        private void cmd_ok_Click(object sender, EventArgs e)
        {
            if (VariablesGlobales.busquedaavanzada)
            {
                {
                    ref var withBlock = ref VariablesGlobales.edita_proveedor;
                    withBlock.razon_social = Strings.Trim(txt_razonSocial.Text);
                    withBlock.id_claseFiscal = (int?)cmb_claseFiscal.SelectedValue;
                    withBlock.id_tipoDocumento = Conversions.ToInteger(cmb_tipoDocumento.SelectedValue);
                    withBlock.contacto = Strings.Trim(txt_contacto.Text);
                    withBlock.vendedor = Strings.Trim(txt_vendedor.Text);
                    withBlock.taxNumber = txt_taxNumber.Text;
                    withBlock.telefono = txt_telefono.Text;
                    withBlock.celular = txt_celular.Text;
                    withBlock.email = txt_email.Text;
                    withBlock.id_pais_fiscal = cmb_paisFiscal.SelectedValue;
                    withBlock.id_provincia_fiscal = (int?)cmb_provinciaFiscal.SelectedValue;
                    withBlock.direccion_fiscal = txt_direccionFiscal.Text;
                    withBlock.localidad_fiscal = txt_localidadFiscal.Text;
                    withBlock.cp_fiscal = txt_cpFiscal.Text;
                    withBlock.id_pais_entrega = cmb_paisEntrega.SelectedValue;
                    withBlock.id_provincia_entrega = (int?)cmb_provinciaEntrega.SelectedValue;
                    withBlock.direccion_entrega = txt_direccionEntrega.Text;
                    withBlock.localidad_entrega = txt_localidadEntrega.Text;
                    withBlock.cp_entrega = txt_cpEntrega.Text;
                    withBlock.esInscripto = chk_esInscripto.Checked;
                    withBlock.activo = chk_activo.Checked;
                }
                closeandupdate(this);
                return;
            }

            if (string.IsNullOrEmpty(txt_razonSocial.Text))
            {
                Interaction.MsgBox("El campo 'Razon social' es obligatorio y está vacio");
                return;
            }
            else if (cmb_claseFiscal.Text == "Seleccione una clase fiscal...")
            {
                Interaction.MsgBox("Debe seleccionar una clase fiscal", (MsgBoxStyle)((int)Constants.vbExclamation + (int)Constants.vbOKOnly), "Centrex");
                return;
            }
            else if ((Strings.InStr(Strings.LCase(cmb_claseFiscal.Text), "indefinido") == 0 | Strings.InStr(Strings.LCase(cmb_tipoDocumento.Text), "indefinido") == 0 | Strings.InStr(Strings.LCase(cmb_claseFiscal.Text), "c.u.i.t.") > 0) & string.IsNullOrEmpty(txt_taxNumber.Text))
            {
                Interaction.MsgBox("Debe especificar un documento o seleccionar una clase fiscal y un tipo de documento indefinido.", (MsgBoxStyle)((int)Constants.vbExclamation + (int)Constants.vbOKOnly), "Centrex");
                return;
            }

            var p = new ProveedorEntity();

            p.razon_social = Strings.Trim(txt_razonSocial.Text);
            p.id_claseFiscal = (int?)cmb_claseFiscal.SelectedValue;
            p.id_tipoDocumento = Conversions.ToInteger(cmb_tipoDocumento.SelectedValue);
            p.contacto = Strings.Trim(txt_contacto.Text);
            p.vendedor = Strings.Trim(txt_vendedor.Text);
            p.taxNumber = txt_taxNumber.Text;
            p.telefono = txt_telefono.Text;
            p.celular = txt_celular.Text;
            p.email = txt_email.Text;
            p.id_pais_fiscal = cmb_paisFiscal.SelectedValue;
            p.id_provincia_fiscal = (int?)cmb_provinciaFiscal.SelectedValue;
            p.direccion_fiscal = txt_direccionFiscal.Text;
            p.localidad_fiscal = txt_localidadFiscal.Text;
            p.cp_fiscal = txt_cpFiscal.Text;
            p.id_pais_entrega = cmb_paisEntrega.SelectedValue;
            p.id_provincia_entrega = (int?)cmb_provinciaEntrega.SelectedValue;
            if (string.IsNullOrEmpty(txt_direccionEntrega.Text))
                p.direccion_entrega = txt_direccionFiscal.Text;
            else
                p.direccion_entrega = txt_direccionEntrega.Text;
            if (string.IsNullOrEmpty(txt_localidadEntrega.Text))
                p.localidad_entrega = txt_localidadFiscal.Text;
            else
                p.localidad_entrega = txt_localidadEntrega.Text;
            if (string.IsNullOrEmpty(txt_cpEntrega.Text))
                p.cp_entrega = txt_cpFiscal.Text;
            else
                p.cp_entrega = txt_cpEntrega.Text;
            p.esInscripto = chk_esInscripto.Checked;
            p.activo = chk_activo.Checked;
            p.notas = txt_notas.Text;

            if (VariablesGlobales.edicion == true)
            {
                p.IdProveedor = VariablesGlobales.edita_proveedor.IdProveedor;
                if (proveedores.updateProveedor(p) == false)
                {
                    Interaction.MsgBox("Hubo un problema al actualizar el proveedor, consulte con su programdor", Constants.vbExclamation);
                    closeandupdate(this);
                }
            }
            else
            {
                proveedores.addproveedor(p);
            }

            if (chk_secuencia.Checked == true)
            {
                txt_razonSocial.Text = "";
                cmb_claseFiscal.Text = "Seleccione una clase fiscal...";
                cmb_tipoDocumento.SelectedValue = VariablesGlobales.id_tipoDocumento_default;
                txt_taxNumber.Text = "";
                txt_contacto.Text = "";
                txt_vendedor.Text = "";
                txt_telefono.Text = "";
                txt_celular.Text = "";
                txt_email.Text = "";
                cmb_paisFiscal.SelectedValue = VariablesGlobales.id_pais_default;
                cmb_provinciaFiscal.SelectedValue = VariablesGlobales.id_provincia_default;
                txt_direccionFiscal.Text = "";
                txt_localidadFiscal.Text = "";
                txt_cpFiscal.Text = "";
                cmb_paisEntrega.SelectedValue = VariablesGlobales.id_pais_default;
                cmb_provinciaEntrega.SelectedValue = VariablesGlobales.id_provincia_default;
                txt_direccionEntrega.Text = "";
                txt_localidadEntrega.Text = "";
                txt_cpEntrega.Text = "";
                chk_esInscripto.Checked = false;
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

        // Private Sub txt_taxNumber_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_taxNumber
        // 'e.Handled = valida(e.KeyChar, 2)
        // End Sub

        private void add_proveedor_FormClosed(object sender, FormClosedEventArgs e)
        {
            closeandupdate(this);
        }

        private void add_proveedor_Load(object sender, EventArgs e)
        {
            // Cargo todos los paises de dirección fiscal
            var argcombo = cmb_paisFiscal;
            generales.Cargar_Combo(ref argcombo, "SELECT id_pais, pais FROM paises ORDER BY pais ASC", VariablesGlobales.basedb, "pais", Conversions.ToInteger("id_pais"));
            cmb_paisFiscal = argcombo;

            // Cargo todas las provincias de direccion fiscal
            var argcombo1 = cmb_provinciaFiscal;
            generales.Cargar_Combo(ref argcombo1, "SELECT id_provincia, provincia FROM provincias WHERE id_pais = '" + cmb_paisFiscal.SelectedValue.ToString() + "' ORDER BY provincia ASC", VariablesGlobales.basedb, "provincia", Conversions.ToInteger("id_provincia"));
            cmb_provinciaFiscal = argcombo1;

            // Cargo todos los paises de dirección de entrega
            var argcombo2 = cmb_paisEntrega;
            generales.Cargar_Combo(ref argcombo2, "SELECT id_pais, pais FROM paises ORDER BY pais ASC", VariablesGlobales.basedb, "pais", Conversions.ToInteger("id_pais"));
            cmb_paisEntrega = argcombo2;

            // Cargo todas las provincias de direccion de entrega
            var argcombo3 = cmb_provinciaEntrega;
            generales.Cargar_Combo(ref argcombo3, "SELECT id_provincia, provincia FROM provincias WHERE id_pais = '" + cmb_paisEntrega.SelectedValue.ToString() + "' ORDER BY provincia ASC", VariablesGlobales.basedb, "provincia", Conversions.ToInteger("id_provincia"));
            cmb_provinciaEntrega = argcombo3;

            // Cargo todos las clases fiscales
            var argcombo4 = cmb_claseFiscal;
            generales.Cargar_Combo(ref argcombo4, "SELECT id_claseFiscal, descript FROM sys_ClasesFiscales ORDER BY descript ASC", VariablesGlobales.basedb, "descript", Conversions.ToInteger("id_claseFiscal"));
            cmb_claseFiscal = argcombo4;
            cmb_claseFiscal.Text = "Seleccione una clase fiscal...";

            // Cargo todos los tipos de documentos
            var argcombo5 = cmb_tipoDocumento;
            generales.Cargar_Combo(ref argcombo5, "SELECT id_tipoDocumento, documento FROM tipos_documentos WHERE activo = 1 ORDER BY documento ASC", VariablesGlobales.basedb, "documento", Conversions.ToInteger("id_tipoDocumento"));
            cmb_tipoDocumento = argcombo5;
            cmb_tipoDocumento.SelectedValue = VariablesGlobales.id_tipoDocumento_default;

            ActiveControl = txt_razonSocial;

            if (VariablesGlobales.busquedaavanzada)
            {
                Text = "Buscar proveedors";
                cmd_ok.Text = "Buscar";
                chk_secuencia.Visible = false;
                chk_activo.Checked = true;
                return;
            }

            chk_activo.Checked = true;
            if (VariablesGlobales.edicion == true | VariablesGlobales.borrado == true)
            {
                chk_secuencia.Enabled = false;
                txt_razonSocial.Text = VariablesGlobales.edita_proveedor.razon_social;
                cmb_claseFiscal.SelectedValue = VariablesGlobales.edita_proveedor.id_claseFiscal;
                cmb_tipoDocumento.SelectedValue = VariablesGlobales.edita_proveedor.id_tipoDocumento;
                txt_taxNumber.Text = VariablesGlobales.edita_proveedor.taxNumber;
                txt_contacto.Text = VariablesGlobales.edita_proveedor.contacto;
                txt_vendedor.Text = VariablesGlobales.edita_proveedor.vendedor;
                txt_telefono.Text = VariablesGlobales.edita_proveedor.telefono;
                txt_celular.Text = VariablesGlobales.edita_proveedor.celular;
                txt_email.Text = VariablesGlobales.edita_proveedor.email;
                cmb_paisFiscal.SelectedValue = VariablesGlobales.edita_proveedor.id_pais_fiscal;
                cmb_provinciaFiscal.SelectedValue = VariablesGlobales.edita_proveedor.id_provincia_fiscal;
                txt_direccionFiscal.Text = VariablesGlobales.edita_proveedor.direccion_fiscal;
                txt_localidadFiscal.Text = VariablesGlobales.edita_proveedor.localidad_fiscal;
                txt_cpFiscal.Text = VariablesGlobales.edita_proveedor.cp_fiscal;
                cmb_paisEntrega.SelectedValue = VariablesGlobales.edita_proveedor.id_pais_entrega;
                cmb_provinciaEntrega.SelectedValue = VariablesGlobales.edita_proveedor.id_provincia_entrega;
                txt_direccionEntrega.Text = VariablesGlobales.edita_proveedor.direccion_entrega;
                txt_localidadEntrega.Text = VariablesGlobales.edita_proveedor.localidad_entrega;
                txt_cpEntrega.Text = VariablesGlobales.edita_proveedor.cp_entrega;
                chk_esInscripto.Checked = VariablesGlobales.edita_proveedor.esInscripto;
                chk_activo.Checked = VariablesGlobales.edita_proveedor.activo;
                txt_notas.Text = VariablesGlobales.edita_proveedor.notas;
            }

            if (VariablesGlobales.borrado == true)
            {
                txt_razonSocial.Enabled = false;
                cmb_claseFiscal.Enabled = false;
                cmb_tipoDocumento.Enabled = false;
                txt_taxNumber.Enabled = false;
                txt_contacto.Enabled = false;
                txt_telefono.Enabled = false;
                txt_celular.Enabled = false;
                txt_email.Enabled = false;
                cmb_paisFiscal.Enabled = false;
                cmb_provinciaFiscal.Enabled = false;
                txt_direccionFiscal.Enabled = false;
                txt_localidadFiscal.Enabled = false;
                txt_cpFiscal.Enabled = false;
                cmb_paisEntrega.Enabled = false;
                cmb_provinciaEntrega.Enabled = false;
                txt_direccionEntrega.Enabled = false;
                txt_localidadEntrega.Enabled = false;
                txt_cpEntrega.Enabled = false;
                chk_esInscripto.Enabled = false;
                chk_activo.Enabled = false;
                cmd_ok.Visible = false;
                cmd_exit.Visible = false;
                Show();
                if (Interaction.MsgBox("¿Está seguro que desea borrar este proveedor?", (MsgBoxStyle)((int)Constants.vbYesNo + (int)Constants.vbQuestion)) == MsgBoxResult.Yes)
                {
                    if (proveedores.borrarproveedor(VariablesGlobales.edita_proveedor) == false)
                    {
                        if (Interaction.MsgBox("Ocurrió un error al realizar el borrado del proveedor, ¿desea intectar desactivarlo para que no aparezca en la búsqueda?", (MsgBoxStyle)((int)MsgBoxStyle.Question + (int)MsgBoxStyle.YesNo)) == Constants.vbYes)
                        {
                            // Realizo un borrado lógico
                            if (proveedores.updateProveedor(VariablesGlobales.edita_proveedor, true) == true)
                            {
                                Interaction.MsgBox("Se ha podido realizar un borrado lógico, pero el proveedor no se borró definitivamente." + "\r" + "Esto posiblemente se deba a que el proveedor, tiene operaciones realizadas y por lo tanto no podrá borrarse", Constants.vbInformation);
                            }
                            else
                            {
                                Interaction.MsgBox("No se ha podido borrar el proveedor, consulte con el programador");
                            }
                        }
                    }
                }
                closeandupdate(this);
            }
        }

        private void cmb_paisFiscal_SelectionChangeCommitted(object sender, EventArgs e)
        {
            // Cargo todas las provincias de direccion de fiscal
            var argcombo = cmb_provinciaFiscal;
            generales.Cargar_Combo(ref argcombo, "SELECT id_provincia, provincia FROM provincias WHERE id_pais = '" + cmb_paisFiscal.SelectedValue.ToString() + "' ORDER BY provincia ASC", VariablesGlobales.basedb, "provincia", Conversions.ToInteger("id_provincia"));
            cmb_provinciaFiscal = argcombo;
        }

        private void cmb_paisEntrega_SelectionChangeCommitted(object sender, EventArgs e)
        {
            // Cargo todas las provincias de direccion de entrega
            var argcombo = cmb_provinciaEntrega;
            generales.Cargar_Combo(ref argcombo, "SELECT id_provincia, provincia FROM provincias WHERE id_pais = '" + cmb_paisEntrega.SelectedValue.ToString() + "' ORDER BY provincia ASC", VariablesGlobales.basedb, "provincia", Conversions.ToInteger("id_provincia"));
            cmb_provinciaEntrega = argcombo;
        }

        private void txt_taxNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }
    }
}
