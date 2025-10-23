using System;
using System.Linq;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace Centrex
{
    public partial class add_cliente
    {
        private int nCliente;

        public add_cliente()
        {
            InitializeComponent();
        }
        private void cmd_ok_Click(object sender, EventArgs e)
        {
            if (VariablesGlobales.busquedaavanzada)
            {
                {
                    var withBlock = VariablesGlobales.edita_cliente;
                    withBlock.RazonSocial = Strings.Trim(txt_razonSocial.Text);
                    withBlock.NombreFantasia = Strings.Trim(txt_nombreFantasia.Text);
                    withBlock.IdClaseFiscal = ToNullableInt(cmb_claseFiscal.SelectedValue);
                    if (cmb_tipoDocumento.SelectedValue is not null)
                    {
                        withBlock.IdTipoDocumento = Conversions.ToInteger(cmb_tipoDocumento.SelectedValue);
                    }
                    withBlock.Contacto = Strings.Trim(txt_contacto.Text);
                    withBlock.TaxNumber = txt_taxNumber.Text;
                    withBlock.Telefono = txt_telefono.Text;
                    withBlock.Celular = txt_celular.Text;
                    withBlock.Email = txt_email.Text;
                    withBlock.IdProvinciaFiscal = ToNullableInt(cmb_provinciaFiscal.SelectedValue);
                    withBlock.DireccionFiscal = txt_direccionFiscal.Text;
                    withBlock.LocalidadFiscal = txt_localidadFiscal.Text;
                    withBlock.CpFiscal = txt_cpFiscal.Text;
                    withBlock.IdProvinciaEntrega = ToNullableInt(cmb_provinciaEntrega.SelectedValue);
                    withBlock.DireccionEntrega = txt_direccionEntrega.Text;
                    withBlock.LocalidadEntrega = txt_localidadEntrega.Text;
                    withBlock.CpEntrega = txt_cpEntrega.Text;
                    withBlock.EsInscripto = chk_esInscripto.Checked;
                    withBlock.Activo = chk_activo.Checked;
                    withBlock.Notas = txt_notas.Text;
                }
                closeandupdate(this);
                return;
            }

            if (string.IsNullOrEmpty(txt_razonSocial.Text))
            {
                Interaction.MsgBox("El campo 'Razon social' es obligatorio y está vacio");
                return;
            }

            var cl = new cliente();

            cl.RazonSocial = Strings.Trim(txt_razonSocial.Text);
            cl.NombreFantasia = Strings.Trim(txt_nombreFantasia.Text);
            cl.IdClaseFiscal = ToNullableInt(cmb_claseFiscal.SelectedValue);
            cl.IdTipoDocumento = Conversions.ToInteger(cmb_tipoDocumento.SelectedValue);
            cl.Contacto = Strings.Trim(txt_contacto.Text);
            cl.TaxNumber = txt_taxNumber.Text;
            cl.Telefono = txt_telefono.Text;
            cl.Celular = txt_celular.Text;
            cl.Email = txt_email.Text;
            cl.IdProvinciaFiscal = ToNullableInt(cmb_provinciaFiscal.SelectedValue);
            cl.DireccionFiscal = txt_direccionFiscal.Text;
            cl.LocalidadFiscal = txt_localidadFiscal.Text;
            cl.CpFiscal = txt_cpFiscal.Text;
            // If cmb_paisEntrega.Text = "" Then .id_pais_entrega = cmb_paisFiscal.SelectedValue Else .id_pais_entrega = cmb_paisEntrega.SelectedValue
            // If cmb_provinciaEntrega.Text = "" Then .id_provincia_entrega = cmb_provinciaFiscal.SelectedValue Else .id_provincia_entrega = cmb_provinciaEntrega.SelectedValue
            if (string.IsNullOrEmpty(txt_direccionEntrega.Text))
            {
                cl.IdProvinciaEntrega = cl.IdProvinciaFiscal;
                cl.DireccionEntrega = txt_direccionFiscal.Text;
                cl.LocalidadEntrega = txt_localidadFiscal.Text;
                cl.CpEntrega = txt_cpFiscal.Text;
            }
            else
            {
                cl.IdProvinciaEntrega = ToNullableInt(cmb_provinciaEntrega.SelectedValue);
                cl.DireccionEntrega = txt_direccionEntrega.Text;
                cl.LocalidadEntrega = txt_localidadEntrega.Text;
                cl.CpEntrega = txt_cpEntrega.Text;
            }
            // If txt_localidadEntrega.Text = "" Then .localidad_entrega = txt_localidadFiscal.Text Else .localidad_entrega = txt_localidadEntrega.Text
            // If txt_cpEntrega.Text = "" Then .cp_entrega = txt_cpFiscal.Text Else .cp_entrega = txt_cpEntrega.Text
            cl.EsInscripto = chk_esInscripto.Checked;
            cl.Activo = chk_activo.Checked;
            cl.Notas = txt_notas.Text;

            if (VariablesGlobales.edicion == true)
            {
                cl.IdCliente = VariablesGlobales.edita_cliente.IdCliente;
                if (clientes.updatecliente(cl) == false)
                {
                    Interaction.MsgBox("Hubo un problema al actualizar el cliente, consulte con su programdor", (MsgBoxStyle)((int)Constants.vbExclamation + (int)Constants.vbOKOnly), "Centrex");
                    closeandupdate(this);
                }
            }
            else
            {
                clientes.addcliente(cl);
            }

            if (chk_secuencia.Checked == true)
            {
                txt_razonSocial.Text = "";
                txt_nombreFantasia.Text = "";
                cmb_claseFiscal.Text = "Seleccione una clase fiscal....";
                cmb_tipoDocumento.SelectedValue = VariablesGlobales.id_tipoDocumento_default;
                txt_taxNumber.Text = "";
                txt_contacto.Text = "";
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

        private void add_cliente_FormClosed(object sender, FormClosedEventArgs e)
        {
            closeandupdate(this);
        }

        private void add_cliente_Load(object sender, EventArgs e)
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
            cmb_paisEntrega.Text = "";

            // Cargo todas las provincias de direccion de entrega
            var argcombo3 = cmb_provinciaEntrega;
            generales.Cargar_Combo(ref argcombo3, "SELECT id_provincia, provincia FROM provincias WHERE id_pais = '" + cmb_paisEntrega.SelectedValue.ToString() + "' ORDER BY provincia ASC", VariablesGlobales.basedb, "provincia", Conversions.ToInteger("id_provincia"));
            cmb_provinciaEntrega = argcombo3;
            cmb_provinciaEntrega.Text = "";

            // Cargo todos las clases fiscales
            var argcombo4 = cmb_claseFiscal;
            generales.Cargar_Combo(ref argcombo4, "SELECT id_claseFiscal, descript FROM sys_ClasesFiscales ORDER BY descript ASC", VariablesGlobales.basedb, "descript", Conversions.ToInteger("id_claseFiscal"));
            cmb_claseFiscal = argcombo4;
            cmb_claseFiscal.Text = "Seleccione una clase fiscal...";

            // Cargo todos los tipos de documentos
            var argcombo5 = cmb_tipoDocumento;
            generales.Cargar_Combo(ref argcombo5, "SELECT id_tipoDocumento, documento FROM tipos_documentos ORDER BY documento ASC", VariablesGlobales.basedb, "documento", Conversions.ToInteger("id_tipoDocumento"));
            cmb_tipoDocumento = argcombo5;
            cmb_tipoDocumento.SelectedValue = VariablesGlobales.id_tipoDocumento_default;
            cmb_tipoDocumento_SelectionChangeCommitted(null, null);

            // Me.ActiveControl = Me.txt_razonSocial
            ActiveControl = txt_taxNumber;

            if (VariablesGlobales.busquedaavanzada)
            {
                Text = "Buscar clientes";
                cmd_ok.Text = "Buscar";
                chk_secuencia.Visible = false;
                chk_activo.Checked = true;
                return;
            }

            chk_activo.Checked = true;
            if (VariablesGlobales.edicion == true | VariablesGlobales.borrado == true)
            {
                chk_secuencia.Enabled = false;
                txt_razonSocial.Text = VariablesGlobales.edita_cliente.RazonSocial;
                txt_nombreFantasia.Text = VariablesGlobales.edita_cliente.NombreFantasia;
                if (VariablesGlobales.edita_cliente.IdClaseFiscal is int claseFiscalId)
                {
                    cmb_claseFiscal.SelectedValue = claseFiscalId;
                }
                else
                {
                    cmb_claseFiscal.SelectedIndex = -1;
                }

                cmb_tipoDocumento.SelectedValue = VariablesGlobales.edita_cliente.IdTipoDocumento;
                txt_taxNumber.Text = VariablesGlobales.edita_cliente.TaxNumber;
                txt_contacto.Text = VariablesGlobales.edita_cliente.Contacto;
                txt_telefono.Text = VariablesGlobales.edita_cliente.Telefono;
                txt_celular.Text = VariablesGlobales.edita_cliente.Celular;
                txt_email.Text = VariablesGlobales.edita_cliente.Email;

                SeleccionarPaisYProvincia(cmb_paisFiscal, ref cmb_provinciaFiscal, VariablesGlobales.edita_cliente.IdProvinciaFiscal);
                txt_direccionFiscal.Text = VariablesGlobales.edita_cliente.DireccionFiscal;
                txt_localidadFiscal.Text = VariablesGlobales.edita_cliente.LocalidadFiscal;
                txt_cpFiscal.Text = VariablesGlobales.edita_cliente.CpFiscal;

                SeleccionarPaisYProvincia(cmb_paisEntrega, ref cmb_provinciaEntrega, VariablesGlobales.edita_cliente.IdProvinciaEntrega);
                txt_direccionEntrega.Text = VariablesGlobales.edita_cliente.DireccionEntrega;
                txt_localidadEntrega.Text = VariablesGlobales.edita_cliente.LocalidadEntrega;
                txt_cpEntrega.Text = VariablesGlobales.edita_cliente.CpEntrega;

                chk_esInscripto.Checked = VariablesGlobales.edita_cliente.EsInscripto;
                chk_activo.Checked = VariablesGlobales.edita_cliente.Activo;
                txt_notas.Text = VariablesGlobales.edita_cliente.Notas;
            }

            if (VariablesGlobales.borrado == true)
            {
                txt_razonSocial.Enabled = false;
                txt_nombreFantasia.Enabled = false;
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
                if (Interaction.MsgBox("¿Está seguro que desea borrar este cliente?", (MsgBoxStyle)((int)Constants.vbYesNo + (int)Constants.vbQuestion)) == MsgBoxResult.Yes)
                {
                    if (clientes.borrarcliente(VariablesGlobales.edita_cliente) == false)
                    {
                        if (Interaction.MsgBox("Ocurrió un error al realizar el borrado del cliente, ¿desea intectar desactivarlo para que no aparezca en la búsqueda?", (MsgBoxStyle)((int)MsgBoxStyle.Question + (int)MsgBoxStyle.YesNo)) == Constants.vbYes)
                        {
                            // Realizo un borrado lógico
                            if (clientes.updatecliente(VariablesGlobales.edita_cliente, true) == true)
                            {
                                Interaction.MsgBox("Se ha podido realizar un borrado lógico, pero el cliente no se borró definitivamente." + "\r" + "Esto posiblemente se deba a que el cliente, tiene operaciones realizadas y por lo tanto no podrá borrarse", Constants.vbInformation);
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

        private void cmb_tipoDocumento_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (Conversions.ToBoolean(Operators.ConditionalCompareObjectEqual(cmb_tipoDocumento.SelectedValue, 80, false)))
            {
                chk_esInscripto.Checked = true;
            }
            else
            {
                chk_esInscripto.Checked = false;
            }
        }

        private void txt_notas_TextChanged(object sender, EventArgs e)
        {

        }

        private void cmb_tipoDocumento_KeyPress(object sender, KeyPressEventArgs e)
        {
            // e.KeyChar = ""
        }

        private void cmb_paisFiscal_KeyPress(object sender, KeyPressEventArgs e)
        {
            // e.KeyChar = ""
        }

        private void cmb_provinciaFiscal_KeyPress(object sender, KeyPressEventArgs e)
        {
            // e.KeyChar = ""
        }

        private void cmb_paisEntrega_KeyPress(object sender, KeyPressEventArgs e)
        {
            // e.KeyChar = ""
        }

        private void cmb_provinciaEntrega_KeyPress(object sender, KeyPressEventArgs e)
        {
            // e.KeyChar = ""
        }

        private void txt_taxNumber_LostFocus(object sender, EventArgs e)
        {
            cliente cl;
            if (!string.IsNullOrEmpty(txt_taxNumber.Text))
            {
                nCliente = clientes.existecliente(txt_taxNumber.Text);
            }
            else
            {
                return;
            }

            if (nCliente != -1)
            {
                cl = clientes.info_cliente(nCliente.ToString());
                txt_taxNumber.Text = cl.TaxNumber;
                txt_razonSocial.Text = cl.RazonSocial;
                txt_nombreFantasia.Text = cl.NombreFantasia;
                cmb_tipoDocumento.SelectedValue = cl.IdTipoDocumento;
                if (cl.IdClaseFiscal is int claseFiscalId)
                {
                    cmb_claseFiscal.SelectedValue = claseFiscalId;
                }
                else
                {
                    cmb_claseFiscal.SelectedIndex = -1;
                }

                txt_contacto.Text = cl.Contacto;
                txt_telefono.Text = cl.Telefono;
                txt_celular.Text = cl.Celular;
                txt_email.Text = cl.Email;
                chk_activo.Checked = cl.Activo;
                chk_esInscripto.Checked = cl.EsInscripto;

                SeleccionarPaisYProvincia(cmb_paisFiscal, ref cmb_provinciaFiscal, cl.IdProvinciaFiscal);
                txt_direccionFiscal.Text = cl.DireccionFiscal;
                txt_localidadFiscal.Text = cl.LocalidadFiscal;
                txt_cpFiscal.Text = cl.CpFiscal;

                SeleccionarPaisYProvincia(cmb_paisEntrega, ref cmb_provinciaEntrega, cl.IdProvinciaEntrega);
                txt_direccionEntrega.Text = cl.DireccionEntrega;
                txt_localidadEntrega.Text = cl.LocalidadEntrega;
                txt_cpEntrega.Text = cl.CpEntrega;

                txt_notas.Text = cl.Notas;
                cmb_tipoDocumento_SelectionChangeCommitted(null, null);
                VariablesGlobales.edita_cliente = cl;
                VariablesGlobales.edicion = true;
            }
        }

        private static int? ToNullableInt(object? value)
        {
            if (value is null || value is DBNull)
            {
                return null;
            }

            try
            {
                return Conversions.ToInteger(value);
            }
            catch
            {
                return null;
            }
        }

        private void SeleccionarPaisYProvincia(ComboBox cmbPais, ref ComboBox cmbProvincia, int? idProvincia)
        {
            if (idProvincia is null)
            {
                cmbPais.SelectedIndex = -1;
                cmbProvincia.SelectedIndex = -1;
                return;
            }

            using var context = GetDbContext();
            var provincia = context.Provincias.AsNoTracking().FirstOrDefault(p => p.IdProvincia == idProvincia.Value);
            if (provincia is null)
            {
                cmbProvincia.SelectedIndex = -1;
                return;
            }

            cmbPais.SelectedValue = provincia.IdPais;

            generales.Cargar_Combo(ref cmbProvincia, "SELECT id_provincia, provincia FROM provincias WHERE id_pais = '" + provincia.IdPais + "' ORDER BY provincia ASC", VariablesGlobales.basedb, "provincia", Conversions.ToInteger("id_provincia"));
            cmbProvincia.SelectedValue = provincia.IdProvincia;
        }
    }
}
