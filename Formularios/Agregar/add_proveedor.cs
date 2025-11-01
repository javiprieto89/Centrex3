using System;
using System.Collections.Generic;
using System.Windows.Forms;

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
            if (busquedaavanzada)
            {
                {
                    ref var withBlock = ref edita_proveedor;
                    withBlock.RazonSocial = Strings.Trim(txt_razonSocial.Text);
                    withBlock.IdClaseFiscal = (int?)cmb_claseFiscal.SelectedValue;
                    withBlock.IdTipoDocumento = Conversions.ToInteger(cmb_tipoDocumento.SelectedValue);
                    withBlock.Contacto = Strings.Trim(txt_contacto.Text);
                    withBlock.Vendedor = Strings.Trim(txt_vendedor.Text);
                    withBlock.TaxNumber = txt_taxNumber.Text;
                    withBlock.Telefono = txt_telefono.Text;
                    withBlock.Celular = txt_celular.Text;
                    withBlock.Email = txt_email.Text;
                    withBlock.IdPaisFiscal = (int?)cmb_paisFiscal.SelectedValue;
                    withBlock.IdProvinciaFiscal = (int?)cmb_provinciaFiscal.SelectedValue;
                    withBlock.DireccionFiscal = txt_direccionFiscal.Text;
                    withBlock.LocalidadFiscal = txt_localidadFiscal.Text;
                    withBlock.CpFiscal = txt_cpFiscal.Text;
                    withBlock.IdPaisEntrega = (int?)cmb_paisEntrega.SelectedValue;
                    withBlock.IdProvinciaEntrega = (int?)cmb_provinciaEntrega.SelectedValue;
                    withBlock.DireccionEntrega = txt_direccionEntrega.Text;
                    withBlock.LocalidadEntrega = txt_localidadEntrega.Text;
                    withBlock.CpEntrega = txt_cpEntrega.Text;
                    withBlock.EsInscripto = chk_esInscripto.Checked;
                    withBlock.Activo = chk_activo.Checked;
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

            p.RazonSocial = Strings.Trim(txt_razonSocial.Text);
            p.IdClaseFiscal = (int?)cmb_claseFiscal.SelectedValue;
            p.IdTipoDocumento = Conversions.ToInteger(cmb_tipoDocumento.SelectedValue);
            p.Contacto = Strings.Trim(txt_contacto.Text);
            p.Vendedor = Strings.Trim(txt_vendedor.Text);
            p.TaxNumber = txt_taxNumber.Text;
            p.Telefono = txt_telefono.Text;
            p.Celular = txt_celular.Text;
            p.Email = txt_email.Text;
            p.IdPaisFiscal = (int?)cmb_paisFiscal.SelectedValue;
            p.IdProvinciaFiscal = (int?)cmb_provinciaFiscal.SelectedValue;
            p.DireccionFiscal = txt_direccionFiscal.Text;
            p.LocalidadFiscal = txt_localidadFiscal.Text;
            p.CpFiscal = txt_cpFiscal.Text;
            p.IdPaisEntrega = (int?)cmb_paisEntrega.SelectedValue;
            p.IdProvinciaEntrega = (int?)cmb_provinciaEntrega.SelectedValue;
            if (string.IsNullOrEmpty(txt_direccionEntrega.Text))
                p.DireccionEntrega = txt_direccionFiscal.Text;
            else
                p.DireccionEntrega = txt_direccionEntrega.Text;
            if (string.IsNullOrEmpty(txt_localidadEntrega.Text))
                p.LocalidadEntrega = txt_localidadFiscal.Text;
            else
                p.LocalidadEntrega = txt_localidadEntrega.Text;
            if (string.IsNullOrEmpty(txt_cpEntrega.Text))
                p.CpEntrega = txt_cpFiscal.Text;
            else
                p.CpEntrega = txt_cpEntrega.Text;
            p.EsInscripto = chk_esInscripto.Checked;
            p.Activo = chk_activo.Checked;
            p.Notas = txt_notas.Text;

            if (edicion == true)
            {
                p.IdProveedor = edita_proveedor.IdProveedor;
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
                cmb_tipoDocumento.SelectedValue = id_tipoDocumento_default;
                txt_taxNumber.Text = "";
                txt_contacto.Text = "";
                txt_vendedor.Text = "";
                txt_telefono.Text = "";
                txt_celular.Text = "";
                txt_email.Text = "";
                cmb_paisFiscal.SelectedValue = id_pais_default;
                CargarProvinciasFiscales();
                cmb_provinciaFiscal.SelectedValue = id_provincia_default;
                txt_direccionFiscal.Text = "";
                txt_localidadFiscal.Text = "";
                txt_cpFiscal.Text = "";
                cmb_paisEntrega.SelectedValue = id_pais_default;
                CargarProvinciasEntrega();
                cmb_provinciaEntrega.SelectedValue = id_provincia_default;
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
            var ordenPais = OrdenAsc("Pais");
            var ordenProvincia = OrdenAsc("Provincia");
            var ordenClaseFiscal = OrdenAsc("Descript");
            var ordenDocumento = OrdenAsc("Documento");

            // Cargo todos los países de dirección fiscal
            var argPaisFiscal = cmb_paisFiscal;
            generales.Cargar_Combo(
                ref argPaisFiscal,
                entidad: "PaisEntity",
                displaymember: "Pais",
                valuemember: "IdPais",
                predet: 0,
                soloActivos: false,
                filtros: null,
                orden: ordenPais);
            cmb_paisFiscal = argPaisFiscal;

            // Cargo todos los países de dirección de entrega
            var argPaisEntrega = cmb_paisEntrega;
            generales.Cargar_Combo(
                ref argPaisEntrega,
                entidad: "PaisEntity",
                displaymember: "Pais",
                valuemember: "IdPais",
                predet: 0,
                soloActivos: false,
                filtros: null,
                orden: ordenPais);
            cmb_paisEntrega = argPaisEntrega;

            // Provincias iniciales en base al país seleccionado
            CargarProvinciasFiscales(ordenProvincia);
            CargarProvinciasEntrega(ordenProvincia);

            // Cargo todas las clases fiscales
            var argClaseFiscal = cmb_claseFiscal;
            generales.Cargar_Combo(
                ref argClaseFiscal,
                entidad: "SysClaseFiscalEntity",
                displaymember: "Descript",
                valuemember: "IdClaseFiscal",
                predet: -1,
                soloActivos: false,
                filtros: null,
                orden: ordenClaseFiscal);
            cmb_claseFiscal = argClaseFiscal;
            cmb_claseFiscal.SelectedIndex = -1;
            cmb_claseFiscal.Text = "Seleccione una clase fiscal...";

            // Cargo todos los tipos de documentos
            var argTipoDocumento = cmb_tipoDocumento;
            generales.Cargar_Combo(
                ref argTipoDocumento,
                entidad: "TipoDocumentoEntity",
                displaymember: "Documento",
                valuemember: "IdTipoDocumento",
                predet: -1,
                soloActivos: true,
                filtros: null,
                orden: ordenDocumento);
            cmb_tipoDocumento = argTipoDocumento;
            if (cmb_tipoDocumento.Items.Count > 0 && id_tipoDocumento_default != 0)
            {
                try
                {
                    cmb_tipoDocumento.SelectedValue = id_tipoDocumento_default;
                }
                catch
                {
                    cmb_tipoDocumento.SelectedIndex = -1;
                }
            }

            ActiveControl = txt_razonSocial;

            if (busquedaavanzada)
            {
                Text = "Buscar proveedors";
                cmd_ok.Text = "Buscar";
                chk_secuencia.Visible = false;
                chk_activo.Checked = true;
                return;
            }

            chk_activo.Checked = true;
            if (edicion == true | borrado == true)
            {
                chk_secuencia.Enabled = false;
                txt_razonSocial.Text = edita_proveedor.RazonSocial;
                cmb_claseFiscal.SelectedValue = edita_proveedor.IdClaseFiscal;
                cmb_tipoDocumento.SelectedValue = edita_proveedor.IdTipoDocumento;
                txt_taxNumber.Text = edita_proveedor.TaxNumber;
                txt_contacto.Text = edita_proveedor.Contacto;
                txt_vendedor.Text = edita_proveedor.Vendedor;
                txt_telefono.Text = edita_proveedor.Telefono;
                txt_celular.Text = edita_proveedor.Celular;
                txt_email.Text = edita_proveedor.Email;
                cmb_paisFiscal.SelectedValue = edita_proveedor.IdPaisFiscal;
                CargarProvinciasFiscales();
                cmb_provinciaFiscal.SelectedValue = edita_proveedor.IdProvinciaFiscal;
                txt_direccionFiscal.Text = edita_proveedor.DireccionFiscal;
                txt_localidadFiscal.Text = edita_proveedor.LocalidadFiscal;
                txt_cpFiscal.Text = edita_proveedor.CpFiscal;
                cmb_paisEntrega.SelectedValue = edita_proveedor.IdPaisEntrega;
                CargarProvinciasEntrega();
                cmb_provinciaEntrega.SelectedValue = edita_proveedor.IdProvinciaEntrega;
                txt_direccionEntrega.Text = edita_proveedor.DireccionEntrega;
                txt_localidadEntrega.Text = edita_proveedor.LocalidadEntrega;
                txt_cpEntrega.Text = edita_proveedor.CpEntrega;
                chk_esInscripto.Checked = edita_proveedor.EsInscripto;
                chk_activo.Checked = edita_proveedor.Activo;
                txt_notas.Text = edita_proveedor.Notas;
            }

            if (borrado == true)
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
                    if (proveedores.borrarproveedor(edita_proveedor) == false)
                    {
                        if (Interaction.MsgBox("Ocurrió un error al realizar el borrado del proveedor, ¿desea intectar desactivarlo para que no aparezca en la búsqueda?", (MsgBoxStyle)((int)MsgBoxStyle.Question + (int)MsgBoxStyle.YesNo)) == Constants.vbYes)
                        {
                            // Realizo un borrado lógico
                            if (proveedores.updateProveedor(edita_proveedor, true) == true)
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

        private void cmb_paisFiscal_SelectionChangeCommitted(object sender, EventArgs e) =>
            CargarProvinciasFiscales();

        private void cmb_paisEntrega_SelectionChangeCommitted(object sender, EventArgs e) =>
            CargarProvinciasEntrega();

        private void CargarProvinciasFiscales(List<Tuple<string, bool>>? orden = null)
        {
            if (cmb_paisFiscal.SelectedValue is null)
            {
                cmb_provinciaFiscal.DataSource = null;
                cmb_provinciaFiscal.Items.Clear();
                return;
            }

            var filtros = new Dictionary<string, object>
            {
                ["IdPais"] = Convert.ToInt32(cmb_paisFiscal.SelectedValue)
            };

            var argcombo = cmb_provinciaFiscal;
            generales.Cargar_Combo(
                ref argcombo,
                entidad: "ProvinciaEntity",
                displaymember: "Provincia",
                valuemember: "IdProvincia",
                predet: -1,
                soloActivos: false,
                filtros: filtros,
                orden: orden ?? OrdenAsc("Provincia"));
            cmb_provinciaFiscal = argcombo;

            if (cmb_provinciaFiscal.Items.Count > 0 && cmb_provinciaFiscal.SelectedIndex < 0)
            {
                cmb_provinciaFiscal.SelectedIndex = 0;
            }
        }

        private void CargarProvinciasEntrega(List<Tuple<string, bool>>? orden = null)
        {
            if (cmb_paisEntrega.SelectedValue is null)
            {
                cmb_provinciaEntrega.DataSource = null;
                cmb_provinciaEntrega.Items.Clear();
                return;
            }

            var filtros = new Dictionary<string, object>
            {
                ["IdPais"] = Convert.ToInt32(cmb_paisEntrega.SelectedValue)
            };

            var argcombo = cmb_provinciaEntrega;
            generales.Cargar_Combo(
                ref argcombo,
                entidad: "ProvinciaEntity",
                displaymember: "Provincia",
                valuemember: "IdProvincia",
                predet: -1,
                soloActivos: false,
                filtros: filtros,
                orden: orden ?? OrdenAsc("Provincia"));
            cmb_provinciaEntrega = argcombo;

            if (cmb_provinciaEntrega.Items.Count > 0 && cmb_provinciaEntrega.SelectedIndex < 0)
            {
                cmb_provinciaEntrega.SelectedIndex = 0;
            }
        }

        private void CargarPaisesFiscales(List<Tuple<string, bool>>? orden = null)
        {
            var argcombo = cmb_paisFiscal;
            generales.Cargar_Combo(
                ref argcombo,
                entidad: "PaisEntity",
                displaymember: "Pais",
                valuemember: "IdPais",
                predet: -1,
                soloActivos: false,
                orden: orden ?? OrdenAsc("Pais"));
            cmb_paisFiscal = argcombo;

            if (cmb_paisFiscal.Items.Count > 0 && cmb_paisFiscal.SelectedIndex < 0)
            {
                cmb_paisFiscal.SelectedIndex = 0;
            }
        }

        private void CargarPaisesEntrega(List<Tuple<string, bool>>? orden = null)
        {
            var argcombo = cmb_paisEntrega;
            generales.Cargar_Combo(
                ref argcombo,
                entidad: "PaisEntity",
                displaymember: "Pais",
                valuemember: "IdPais",
                predet: -1,
                soloActivos: false,
                orden: orden ?? OrdenAsc("Pais"));
            cmb_paisEntrega = argcombo;

            if (cmb_paisEntrega.Items.Count > 0 && cmb_paisEntrega.SelectedIndex < 0)
            {
                cmb_paisEntrega.SelectedIndex = 0;
            }
        }

        private static List<Tuple<string, bool>> OrdenAsc(string columna) =>
            new List<Tuple<string, bool>> { Tuple.Create(columna, true) };

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
