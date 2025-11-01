using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

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
            if (busquedaavanzada)
            {
                {
                    var withBlock = edita_cliente;
                    withBlock.RazonSocial = txt_razonSocial.Text.Trim();
                    withBlock.NombreFantasia = txt_nombreFantasia.Text.Trim();
                    withBlock.IdClaseFiscal = ToNullableInt(cmb_claseFiscal.SelectedValue);
                    if (cmb_tipoDocumento.SelectedValue is not null)
                    {
                        withBlock.IdTipoDocumento = Convert.ToInt32(cmb_tipoDocumento.SelectedValue);
                    }
                    withBlock.Contacto = txt_contacto.Text.Trim();
                    withBlock.TaxNumber = txt_taxNumber.Text;
                    withBlock.Telefono = txt_telefono.Text;
                    withBlock.Celular = txt_celular.Text;
                    withBlock.Email = txt_email.Text;
                    withBlock.IdPaisFiscal = ToNullableInt(cmb_paisFiscal.SelectedValue);
                    withBlock.IdProvinciaFiscal = ToNullableInt(cmb_provinciaFiscal.SelectedValue);
                    withBlock.DireccionFiscal = txt_direccionFiscal.Text;
                    withBlock.LocalidadFiscal = txt_localidadFiscal.Text;
                    withBlock.CpFiscal = txt_cpFiscal.Text;
                    withBlock.IdPaisEntrega = ToNullableInt(cmb_paisEntrega.SelectedValue);
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
                MessageBox.Show("El campo 'Razon social' es obligatorio y está vacio", "Centrex", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            var cl = new ClienteEntity();

            cl.RazonSocial = txt_razonSocial.Text.Trim();
            cl.NombreFantasia = txt_nombreFantasia.Text.Trim();
            cl.IdClaseFiscal = ToNullableInt(cmb_claseFiscal.SelectedValue);
            cl.IdTipoDocumento = Convert.ToInt32(cmb_tipoDocumento.SelectedValue);
            cl.Contacto = txt_contacto.Text.Trim();
            cl.TaxNumber = txt_taxNumber.Text;
            cl.Telefono = txt_telefono.Text;
            cl.Celular = txt_celular.Text;
            cl.Email = txt_email.Text;
            cl.IdPaisFiscal = ToNullableInt(cmb_paisFiscal.SelectedValue);
            cl.IdPaisEntrega = ToNullableInt(cmb_paisEntrega.SelectedValue);
            cl.IdProvinciaFiscal = ToNullableInt(cmb_provinciaFiscal.SelectedValue);
            cl.IdProvinciaEntrega = ToNullableInt(cmb_provinciaEntrega.SelectedValue);
            cl.DireccionFiscal = txt_direccionFiscal.Text;
            cl.LocalidadFiscal = txt_localidadFiscal.Text;
            cl.CpFiscal = txt_cpFiscal.Text;

            if (string.IsNullOrEmpty(txt_direccionEntrega.Text))
            {
                cl.IdPaisEntrega = cl.IdPaisFiscal;
                cl.IdProvinciaEntrega = cl.IdProvinciaFiscal;
                cl.DireccionEntrega = txt_direccionFiscal.Text;
                cl.LocalidadEntrega = txt_localidadFiscal.Text;
                cl.CpEntrega = txt_cpFiscal.Text;
            }
            else
            {
                cl.IdPaisFiscal = ToNullableInt(cmb_paisFiscal.SelectedValue);
                cl.IdProvinciaEntrega = ToNullableInt(cmb_provinciaEntrega.SelectedValue);
                cl.DireccionEntrega = txt_direccionEntrega.Text;
                cl.LocalidadEntrega = txt_localidadEntrega.Text;
                cl.CpEntrega = txt_cpEntrega.Text;
            }

            cl.EsInscripto = chk_esInscripto.Checked;
            cl.Activo = chk_activo.Checked;
            cl.Notas = txt_notas.Text;

            if (edicion == true)
            {
                cl.IdCliente = edita_cliente.IdCliente;
                if (clientes.updatecliente(cl) == false)
                {
                    MessageBox.Show("Hubo un problema al actualizar el cliente, consulte con su programador", "Centrex", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
                cmb_tipoDocumento.SelectedValue = id_tipoDocumento_default;
                txt_taxNumber.Text = "";
                txt_contacto.Text = "";
                txt_telefono.Text = "";
                txt_celular.Text = "";
                txt_email.Text = "";
                cmb_paisFiscal.SelectedValue = id_pais_default;
                CargarProvinciasPorPais(ref cmb_provinciaFiscal, cmb_paisFiscal.SelectedValue);
                if (id_provincia_default > 0 && cmb_provinciaFiscal.Items.Count > 0)
                {
                    cmb_provinciaFiscal.SelectedValue = id_provincia_default;
                }
                else
                {
                    cmb_provinciaFiscal.SelectedIndex = -1;
                }
                txt_direccionFiscal.Text = "";
                txt_localidadFiscal.Text = "";
                txt_cpFiscal.Text = "";
                cmb_paisEntrega.SelectedValue = id_pais_default;
                CargarProvinciasPorPais(ref cmb_provinciaEntrega, cmb_paisEntrega.SelectedValue);
                if (id_provincia_default > 0 && cmb_provinciaEntrega.Items.Count > 0)
                {
                    cmb_provinciaEntrega.SelectedValue = id_provincia_default;
                }
                else
                {
                    cmb_provinciaEntrega.SelectedIndex = -1;
                }
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

        private void add_cliente_FormClosed(object sender, FormClosedEventArgs e)
        {
            closeandupdate(this);
        }

        private void add_cliente_Load(object sender, EventArgs e)
        {
            var ordenPais = OrdenAscendente("Pais");
            var ordenProvincia = OrdenAscendente("Provincia");
            var ordenClaseFiscal = OrdenAscendente("Descript");
            var ordenDocumento = OrdenAscendente("Documento");

            generales.Cargar_Combo(
           ref cmb_paisFiscal,
      entidad: "PaisEntity",
          displaymember: "Pais",
         valuemember: "IdPais",
           predet: -1,
         soloActivos: false,
               orden: ordenPais);

            if (id_pais_default > 0 && cmb_paisFiscal.Items.Count > 0)
            {
                cmb_paisFiscal.SelectedValue = id_pais_default;
            }

            CargarProvinciasPorPais(ref cmb_provinciaFiscal, cmb_paisFiscal.SelectedValue, ordenProvincia);
            if (id_provincia_default > 0 && cmb_provinciaFiscal.Items.Count > 0)
            {
                cmb_provinciaFiscal.SelectedValue = id_provincia_default;
            }

            generales.Cargar_Combo(
               ref cmb_paisEntrega,
             entidad: "PaisEntity",
              displaymember: "Pais",
                     valuemember: "IdPais",
                        predet: -1,
                     soloActivos: false,
           orden: ordenPais);

            if (id_pais_default > 0 && cmb_paisEntrega.Items.Count > 0)
            {
                cmb_paisEntrega.SelectedValue = id_pais_default;
            }
            else
            {
                cmb_paisEntrega.SelectedIndex = -1;
            }

            CargarProvinciasPorPais(ref cmb_provinciaEntrega, cmb_paisEntrega.SelectedValue, ordenProvincia);
            if (cmb_paisEntrega.SelectedIndex == -1)
            {
                cmb_provinciaEntrega.DataSource = null;
                cmb_provinciaEntrega.Items.Clear();
                cmb_provinciaEntrega.SelectedIndex = -1;
            }
            else if (id_provincia_default > 0 && cmb_provinciaEntrega.Items.Count > 0)
            {
                cmb_provinciaEntrega.SelectedValue = id_provincia_default;
            }

            generales.Cargar_Combo(
       ref cmb_claseFiscal,
                entidad: "SysClaseFiscalEntity",
   displaymember: "Descript",
    valuemember: "IdClaseFiscal",
                predet: -1,
    soloActivos: false,
  orden: ordenClaseFiscal);
            cmb_claseFiscal.Text = "Seleccione una clase fiscal...";

            generales.Cargar_Combo(
             ref cmb_tipoDocumento,
          entidad: "TipoDocumentoEntity",
             displaymember: "Documento",
               valuemember: "IdTipoDocumento",
             predet: -1,
              soloActivos: true,
            orden: ordenDocumento);
            if (id_tipoDocumento_default > 0)
            {
                cmb_tipoDocumento.SelectedValue = id_tipoDocumento_default;
            }

            cmb_tipoDocumento_SelectionChangeCommitted(null, null);

            ActiveControl = txt_taxNumber;

            if (busquedaavanzada)
            {
                Text = "Buscar clientes";
                cmd_ok.Text = "Buscar";
                chk_secuencia.Visible = false;
                chk_activo.Checked = true;
                return;
            }

            chk_activo.Checked = true;
            if (edicion == true || borrado == true)
            {
                chk_secuencia.Enabled = false;
                txt_razonSocial.Text = edita_cliente.RazonSocial;
                txt_nombreFantasia.Text = edita_cliente.NombreFantasia;
                if (edita_cliente.IdClaseFiscal is int claseFiscalId)
                {
                    cmb_claseFiscal.SelectedValue = claseFiscalId;
                }
                else
                {
                    cmb_claseFiscal.SelectedIndex = -1;
                }

                cmb_tipoDocumento.SelectedValue = edita_cliente.IdTipoDocumento;
                txt_taxNumber.Text = edita_cliente.TaxNumber;
                txt_contacto.Text = edita_cliente.Contacto;
                txt_telefono.Text = edita_cliente.Telefono;
                txt_celular.Text = edita_cliente.Celular;
                txt_email.Text = edita_cliente.Email;

                SeleccionarPaisYProvincia(cmb_paisFiscal, ref cmb_provinciaFiscal, edita_cliente.IdProvinciaFiscal);
                txt_direccionFiscal.Text = edita_cliente.DireccionFiscal;
                txt_localidadFiscal.Text = edita_cliente.LocalidadFiscal;
                txt_cpFiscal.Text = edita_cliente.CpFiscal;

                SeleccionarPaisYProvincia(cmb_paisEntrega, ref cmb_provinciaEntrega, edita_cliente.IdProvinciaEntrega);
                txt_direccionEntrega.Text = edita_cliente.DireccionEntrega;
                txt_localidadEntrega.Text = edita_cliente.LocalidadEntrega;
                txt_cpEntrega.Text = edita_cliente.CpEntrega;

                chk_esInscripto.Checked = edita_cliente.EsInscripto;
                chk_activo.Checked = edita_cliente.Activo;
                txt_notas.Text = edita_cliente.Notas;
            }

            if (borrado == true)
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
                if (MessageBox.Show("¿Está seguro que desea borrar este cliente?", "Centrex", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (clientes.borrarcliente(edita_cliente) == false)
                    {
                        if (MessageBox.Show("Ocurrió un error al realizar el borrado del cliente, ¿desea intentar desactivarlo para que no aparezca en la búsqueda?", "Centrex", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            // Realizo un borrado lógico
                            if (clientes.updatecliente(edita_cliente, true) == true)
                            {
                                MessageBox.Show("Se ha podido realizar un borrado lógico, pero el cliente no se borró definitivamente.\r\nEsto posiblemente se deba a que el cliente tiene operaciones realizadas y por lo tanto no podrá borrarse", "Centrex", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("No se ha podido borrar el cliente, consulte con el programador", "Centrex", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
                closeandupdate(this);
            }
        }

        private void cmb_paisFiscal_SelectionChangeCommitted(object sender, EventArgs e)
        {
            var ordenProvincia = OrdenAscendente("Provincia");
            CargarProvinciasPorPais(ref cmb_provinciaFiscal, cmb_paisFiscal.SelectedValue, ordenProvincia);
        }

        private void cmb_paisEntrega_SelectionChangeCommitted(object sender, EventArgs e)
        {
            var ordenProvincia = OrdenAscendente("Provincia");
            CargarProvinciasPorPais(ref cmb_provinciaEntrega, cmb_paisEntrega.SelectedValue, ordenProvincia);
        }

        private void txt_taxNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar))
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
            if (cmb_tipoDocumento.SelectedValue != null && Convert.ToInt32(cmb_tipoDocumento.SelectedValue) == 80)
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
        }

        private void cmb_paisFiscal_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void cmb_provinciaFiscal_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void cmb_paisEntrega_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void cmb_provinciaEntrega_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void txt_taxNumber_LostFocus(object sender, EventArgs e)
        {
            ClienteEntity cl;
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
                cl = clientes.info_cliente(nCliente);
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
                edita_cliente = cl;
                edicion = true;
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
                return Convert.ToInt32(value);
            }
            catch
            {
                return null;
            }
        }

        private static List<Tuple<string, bool>> OrdenAscendente(string columna)
        {
            return new List<Tuple<string, bool>> { Tuple.Create(columna, true) };
        }

        private void CargarProvinciasPorPais(ref ComboBox comboProvincia, object? idPaisValue, List<Tuple<string, bool>>? orden = null)
        {
            if (idPaisValue is null || idPaisValue == DBNull.Value || !int.TryParse(idPaisValue.ToString(), out var idPais))
            {
                comboProvincia.DataSource = null;
                comboProvincia.Items.Clear();
                comboProvincia.SelectedIndex = -1;
                return;
            }

            orden ??= OrdenAscendente("Provincia");

            var filtros = new Dictionary<string, object>
            {
                ["IdPais"] = idPais
            };

            generales.Cargar_Combo(
     ref comboProvincia,
                entidad: "ProvinciaEntity",
         displaymember: "Provincia",
         valuemember: "IdProvincia",
        predet: -1,
                soloActivos: false,
                filtros: filtros,
             orden: orden);
        }

        private void SeleccionarPaisYProvincia(ComboBox cmbPais, ref ComboBox cmbProvincia, int? idProvincia)
        {
            if (idProvincia is null)
            {
                cmbPais.SelectedIndex = -1;
                cmbProvincia.DataSource = null;
                cmbProvincia.Items.Clear();
                cmbProvincia.SelectedIndex = -1;
                return;
            }

            using var context = new CentrexDbContext();
            var provincia = context.ProvinciaEntity.AsNoTracking().FirstOrDefault(p => p.IdProvincia == idProvincia.Value);
            if (provincia is null)
            {
                cmbProvincia.DataSource = null;
                cmbProvincia.Items.Clear();
                cmbProvincia.SelectedIndex = -1;
                return;
            }

            cmbPais.SelectedValue = provincia.IdPais;
            var ordenProvincia = OrdenAscendente("Provincia");
            CargarProvinciasPorPais(ref cmbProvincia, provincia.IdPais, ordenProvincia);
            cmbProvincia.SelectedValue = provincia.IdProvincia;
        }
    }
}
