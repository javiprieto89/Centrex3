using System;
using System.Collections.Generic;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace Centrex
{

    public partial class infoagregarstock
    {
        private ItemEntity i = new ItemEntity();
        private int id_proveedor;

        public infoagregarstock()
        {

            // Esta llamada es exigida por el diseñador.
            InitializeComponent();

            // Agregue cualquier inicialización después de la llamada a InitializeComponent().

        }

        public infoagregarstock(int _id_proveedor)
        {

            // Esta llamada es exigida por el diseñador.
            InitializeComponent();

            // Agregue cualquier inicialización después de la llamada a InitializeComponent().
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
                Interaction.MsgBox("No hay proveedores activos cargados; no es posible registrar stock.", MsgBoxStyle.Exclamation, "Centrex");
                closeandupdate(this);
                return;
            }

            if (editaStock)
            {
                psearch_item.Visible = true;
            }

            if (VariablesGlobales.edicion | edicion_item_registro_stock)
            {
                i = mitem.info_item(VariablesGlobales.edita_item_registro_stock.id_item);
                VariablesGlobales.id = i.id_item;

                lbl_item.Text = i.descript;
                txt_fecha.Text = Conversions.ToString(DateTime.Parse(VariablesGlobales.edita_item_registro_stock.fecha));
                txt_factura.Text = VariablesGlobales.edita_item_registro_stock.factura;
                // cmb_proveedor.SelectedValue = CByte(VariablesGlobales.edita_item_registro_stock.id_proveedor)
                cmb_proveedor.SelectedValue = id_proveedor;
                txt_cantidad.Text = VariablesGlobales.edita_item_registro_stock.cantidad;
                txt_costo.Text = VariablesGlobales.edita_item_registro_stock.costo;
                txt_preciolista.Text = VariablesGlobales.edita_item_registro_stock.precio_lista;
                txt_factor.Text = VariablesGlobales.edita_item_registro_stock.factor;
                txt_notas.Text = VariablesGlobales.edita_item_registro_stock.nota;
                if (VariablesGlobales.edicion & !editaStock)
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
                i = VariablesGlobales.ConvertToItem(mitem.info_item(VariablesGlobales.id.ToString()));

                lbl_item.Text = i.descript;
                txt_fecha.Text = My.MyProject.Forms.add_stock.txt_fecha.Text;
                txt_factura.Text = My.MyProject.Forms.add_stock.txt_factura.Text;
                // cmb_proveedor.SelectedValue = CByte(add_stock.cmb_proveedor.SelectedValue)
                if (id_proveedor > 0)
                {
                    cmb_proveedor.SelectedValue = id_proveedor;
                }
                else
                {
                    cmb_proveedor.SelectedIndex = -1;
                    cmb_proveedor.Text = "Seleccione un proveedor...";
                }

                txt_costo.Text = i.costo.ToString();
                txt_preciolista.Text = i.precio_lista.ToString();
                txt_factor.Text = i.factor.ToString();
                txt_notas.Text = My.MyProject.Forms.add_stock.txt_notas.Text;

                // Si el item se encuentra inactivo, al agregar stock se activa
                if (i.activo == false)
                {
                    if (!string.IsNullOrEmpty(txt_notas.Text))
                    {
                        txt_notas.Text = txt_notas.Text + Constants.vbCrLf + "ESTE ITEM SE ENCUENTRA INACTIVO, SI AGREGA STOCK SE ACTIVARÁ";
                    }
                    else
                    {
                        txt_notas.Text = "ESTE ITEM SE ENCUENTRA INACTIVO, SI AGREGA STOCK SE ACTIVARÁ";
                    }
                    i.activo = true;
                    mitem.updateitem(i);
                }

            }
            ActiveControl = txt_cantidad;
        }

        private void cmd_ok_Click(object sender, EventArgs e)
        {
            if (VariablesGlobales.edicion)
            {
                closeandupdate(this);
                return;
            }

            var rs = new registro_stock();

            if (cmb_proveedor.SelectedValue is null)
            {
                Interaction.MsgBox("Debe seleccionar un proveedor", Constants.vbExclamation, "Centrex");
                return;
            }
            if (string.IsNullOrEmpty(txt_cantidad.Text))
            {
                Interaction.MsgBox("El campo 'Cantidad' no puede estar vació", Constants.vbExclamation, "Centrex");
                return;
            }
            else if (txt_cantidad.Text == "0")
            {
                Interaction.MsgBox("La cantidad ingresada no puede ser 0", Constants.vbExclamation, "Centrex");
                return;
            }
            else if (string.IsNullOrEmpty(txt_costo.Text))
            {
                Interaction.MsgBox("El campo 'Costo' no puede estar vació", Constants.vbExclamation, "Centrex");
                return;
            }
            else if (string.IsNullOrEmpty(txt_preciolista.Text))
            {
                Interaction.MsgBox("El campo 'Precio lista' no puede estar vació", Constants.vbExclamation, "Centrex");
                return;
            }
            else if (string.IsNullOrEmpty(txt_factor.Text))
            {
                Interaction.MsgBox("El campo 'Factor' no puede estar vació", Constants.vbExclamation, "Centrex");
                return;
            }

            rs.IdItem = VariablesGlobales.id;
            rs.Fecha = DateOnly.Parse(txt_fecha.Text);
            rs.Factura = txt_factura.Text;
            rs.IdProveedor = Convert.ToInt32(cmb_proveedor.SelectedValue);
            rs.Cantidad = Convert.ToDecimal(txt_cantidad.Text);
            rs.Costo = Convert.ToDecimal(txt_costo.Text);
            rs.PrecioLista = Convert.ToDecimal(txt_preciolista.Text);
            rs.Factor = Convert.ToDecimal(txt_factor.Text);
            rs.CantidadAnterior = i.Cantidad;
            rs.CostoAnterior = i.Costo;
            rs.PrecioListaAnterior = i.PrecioLista;
            rs.FactorAnterior = i.Factor;
            rs.Nota = txt_notas.Text;
            if (edicion_item_registro_stock)
            {
                rs.IdRegistro = VariablesGlobales.edita_item_registro_stock.IdRegistro;
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
            var i = new item();
            string tablatmp;

            tablatmp = VariablesGlobales.tabla;
            VariablesGlobales.tabla = "items_registros_stock";

            Enabled = false;
            My.MyProject.Forms.search.ShowDialog();
            Enabled = true;

            if (VariablesGlobales.id == 0)
                return;
            i = VariablesGlobales.ConvertToItem(mitem.info_item(VariablesGlobales.id));

            lbl_item.Text = i.Descript;
            VariablesGlobales.tabla = tablatmp;
        }

        private static List<Tuple<string, bool>> OrdenAsc(string columna) =>
            new List<Tuple<string, bool>> { Tuple.Create(columna, true) };
    }
}

