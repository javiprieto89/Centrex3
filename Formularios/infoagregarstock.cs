using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Centrex
{

    public partial class infoagregarstock
    {
        private ItemEntity i = new ItemEntity();
        private int id_proveedor;

        public infoagregarstock()
        {
            InitializeComponent();
        }

        public infoagregarstock(int _id_proveedor)
        {
            InitializeComponent();
            id_proveedor = _id_proveedor;
        }

        private void infoagregarstock_Load(object sender, EventArgs e)
        {
            var proveedoresCargados = generales.Cargar_Combo(
              ref cmb_proveedor,
            entidad: "ProveedorEntity",
       displaymember: "RazonSocial",
           valuemember: "IdProveedor",
         predet: -1,
         soloActivos: true,
         filtros: null,
      orden: OrdenAsc("RazonSocial"));

            if (proveedoresCargados <= 0)
            {
                MessageBox.Show("No hay proveedores activos cargados; no es posible registrar stock.",
              "Centrex",
                MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation);
                closeandupdate(this);
                return;
            }

            if (editaStock)
            {
                psearch_item.Visible = true;
            }

            if (edicion | edicion_item_registro_stock)
            {
                i = mitem.info_item(edita_item_registro_stock.IdItem);
                id = i.IdItem;

                lbl_item.Text = i.Descript;
                txt_fecha.Text = ConversorFechas.GetFecha(edita_item_registro_stock.Fecha, txt_fecha);
                txt_factura.Text = edita_item_registro_stock.Factura;
                cmb_proveedor.SelectedValue = id_proveedor;
                txt_cantidad.Text = edita_item_registro_stock.Cantidad.ToString();
                txt_costo.Text = edita_item_registro_stock.Costo.ToString();
                txt_preciolista.Text = edita_item_registro_stock.PrecioLista.ToString();
                txt_factor.Text = edita_item_registro_stock.Factor.ToString();
                txt_notas.Text = edita_item_registro_stock.Nota;

                if (edicion & !editaStock)
                {
                    lbl_item.Enabled = false;
                    txt_fecha.Enabled = false;
                    txt_factura.Enabled = false;
                    cmb_proveedor.Enabled = false;
                    txt_cantidad.Enabled = false;
                    txt_costo.Enabled = false;
                    txt_preciolista.Enabled = false;
                    txt_factor.Enabled = false;
                    txt_notas.Enabled = false;
                    cmd_ok.Text = "Aceptar";
                }
            }
            else
            {
                i = ConvertToItem(mitem.info_item(id));

                lbl_item.Text = i.Descript;
                txt_fecha.Text = My.MyProject.Forms.add_stock.txt_fecha.Text;
                txt_factura.Text = My.MyProject.Forms.add_stock.txt_factura.Text;

                if (id_proveedor > 0)
                {
                    cmb_proveedor.SelectedValue = id_proveedor;
                }
                else
                {
                    cmb_proveedor.SelectedIndex = -1;
                    cmb_proveedor.Text = "Seleccione un proveedor...";
                }

                txt_costo.Text = i.Costo.ToString();
                txt_preciolista.Text = i.PrecioLista.ToString();
                txt_factor.Text = i.Factor.ToString();
                txt_notas.Text = My.MyProject.Forms.add_stock.txt_notas.Text;

                // Si el item se encuentra inactivo, al agregar stock se activa
                if (i.Activo == false)
                {
                    if (!string.IsNullOrEmpty(txt_notas.Text))
                    {
                        txt_notas.Text = txt_notas.Text + "\r\n" + "ESTE ITEM SE ENCUENTRA INACTIVO, SI AGREGA STOCK SE ACTIVARÁ";
                    }
                    else
                    {
                        txt_notas.Text = "ESTE ITEM SE ENCUENTRA INACTIVO, SI AGREGA STOCK SE ACTIVARÁ";
                    }
                    i.Activo = true;
                    mitem.updateitem(i);
                }
            }

            ActiveControl = txt_cantidad;
        }

        private void cmd_ok_Click(object sender, EventArgs e)
        {
            if (edicion)
            {
                closeandupdate(this);
                return;
            }

            var rs = new RegistroStockEntity();

            if (cmb_proveedor.SelectedValue is null)
            {
                MessageBox.Show("Debe seleccionar un proveedor", "Centrex", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (string.IsNullOrEmpty(txt_cantidad.Text))
            {
                MessageBox.Show("El campo 'Cantidad' no puede estar vació", "Centrex", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            else if (txt_cantidad.Text == "0")
            {
                MessageBox.Show("La cantidad ingresada no puede ser 0", "Centrex", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            else if (string.IsNullOrEmpty(txt_costo.Text))
            {
                MessageBox.Show("El campo 'Costo' no puede estar vació", "Centrex", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            else if (string.IsNullOrEmpty(txt_preciolista.Text))
            {
                MessageBox.Show("El campo 'Precio lista' no puede estar vació", "Centrex", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            else if (string.IsNullOrEmpty(txt_factor.Text))
            {
                MessageBox.Show("El campo 'Factor' no puede estar vació", "Centrex", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            rs.IdItem = id;
            rs.Fecha = DateOnly.Parse(txt_fecha.Text);
            rs.Factura = txt_factura.Text;
            rs.IdProveedor = Convert.ToInt32(cmb_proveedor.SelectedValue);
            rs.Cantidad = Convert.ToInt32(txt_cantidad.Text);
            rs.Costo = Convert.ToDecimal(txt_costo.Text);
            rs.PrecioLista = Convert.ToDecimal(txt_preciolista.Text);
            rs.Factor = Convert.ToDecimal(txt_factor.Text);
            rs.CantidadAnterior = (int)i.Cantidad;
            rs.CostoAnterior = i.Costo;
            rs.PrecioListaAnterior = i.PrecioLista;
            rs.FactorAnterior = (int)i.Factor;
            rs.Nota = txt_notas.Text;

            if (edicion_item_registro_stock)
            {
                rs.IdRegistro = edita_item_registro_stock.IdRegistro;
                stock.UpdateStockTmp(rs);
            }
            else
            {
                stock.AddStockTmp(rs);
            }

            closeandupdate(this);
        }

        private void cmd_exit_Click(object sender, EventArgs e)
        {
            closeandupdate(this);
        }

        private void txt_costo_LostFocus(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txt_costo.Text))
            {
                txt_preciolista.Text = (Convert.ToDouble(txt_costo.Text) * Convert.ToDouble(txt_factor.Text)).ToString();
            }
        }

        private void txt_factor_LostFocus(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txt_costo.Text))
            {
                txt_preciolista.Text = (Convert.ToDouble(txt_costo.Text) * Convert.ToDouble(txt_factor.Text)).ToString();
            }
        }

        private void psearch_item_Click(object sender, EventArgs e)
        {
            var i = new ItemEntity();
            string tablatmp;

            tablatmp = tabla;
            tabla = "items_registros_stock";

            Enabled = false;
            My.MyProject.Forms.search.ShowDialog();
            Enabled = true;

            if (id == 0)
                return;

            i = ConvertToItem(mitem.info_item(id));

            lbl_item.Text = i.Descript;
            tabla = tablatmp;
        }

        private static List<Tuple<string, bool>> OrdenAsc(string columna) =>
     new List<Tuple<string, bool>> { Tuple.Create(columna, true) };
    }
}

