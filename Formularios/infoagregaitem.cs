using System;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace Centrex
{
    public partial class infoagregaitem
    {

        private string seleccionado;
        private bool produccion = false;
        private bool ordenCompra = false;
        private bool comprobanteCompra = false;
        private int id_comprobanteCompra = -1;
        private bool actualiza = true;
        private int idUsuario;
        private string idUnico;
        public int cant;

        public infoagregaitem()
        {

            // Esta llamada es exigida por el diseñador.
            InitializeComponent();

            // Agregue cualquier inicialización después de la llamada a InitializeComponent().

        }

        public infoagregaitem(int _idUsuario, string _idUnico)
        {

            // Esta llamada es exigida por el diseñador.
            InitializeComponent();

            // Agregue cualquier inicialización después de la llamada a InitializeComponent().
            idUsuario = _idUsuario;
            idUnico = _idUnico;
        }

        public infoagregaitem(bool _produccion, bool _ordenCompra, bool _actualiza, int _idUsuario, string _idUnico)
        {

            // Esta llamada es exigida por el diseñador.
            InitializeComponent();

            // Agregue cualquier inicialización después de la llamada a InitializeComponent().
            produccion = _produccion;
            ordenCompra = _ordenCompra;
            actualiza = _actualiza;
            idUsuario = _idUsuario;
            idUnico = _idUnico;
        }

        public infoagregaitem(bool _comprobanteCompra, int _id_comprobanteCompra)
        {

            // Esta llamada es exigida por el diseñador.
            InitializeComponent();

            // Agregue cualquier inicialización después de la llamada a InitializeComponent().
            comprobanteCompra = _comprobanteCompra;
            id_comprobanteCompra = _id_comprobanteCompra;
            // actualiza = _actualiza
        }

        private void infoagregaitem_FormClosed(object sender, FormClosedEventArgs e)
        {
            // closeandupdate(Me)
        }

        private void infoagregaitem_Load(object sender, EventArgs e)
        {

            lbl_item.Text = VariablesGlobales.edita_item.item;
            lbl_desc.Text = VariablesGlobales.edita_item.descript;
            lbl_stock.Text = Conversions.ToString(VariablesGlobales.edita_item.cantidad.Value);

            if (produccion)
                txt_precio.Enabled = false;

            if (VariablesGlobales.agregaitem == true)
            {
                txt_cantidad.Text = 1.ToString();
                txt_precio.Text = VariablesGlobales.edita_item.precio_lista.ToString();
            }
            else if (produccion)
            {
                txt_cantidad.Text = producciones.askCantidadCargadaProduccion(VariablesGlobales.edita_item.id_item, -1, VariablesGlobales.edita_item.IdItemTemporal).ToString();
            }
            else if (ordenCompra)
            {
                txt_cantidad.Text = ordenesCompras.askCantidadCargadaOC(VariablesGlobales.edita_item.id_item, id_tmpOCItem: VariablesGlobales.edita_item.IdItemTemporal).ToString();
                txt_precio.Text = ordenesCompras.askPrecioCargadoOC(VariablesGlobales.edita_item.id_item, id_tmpOCItem: VariablesGlobales.edita_item.IdItemTemporal).ToString();
            }
            else if (comprobanteCompra)
            {
            }

            else
            {
                txt_cantidad.Text = Pedidos.askCantidadCargada(VariablesGlobales.edita_item.id_item, -1, "tmppedidos_items", idUsuario, idUnico).ToString();
                txt_precio.Text = Pedidos.askPreciocargado(VariablesGlobales.edita_item.id_item, -1, "tmppedidos_items", idUsuario, idUnico).ToString();
            }
        }

        private void cmd_ok_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_cantidad.Text))
                txt_cantidad.Text = 0.ToString();
            if (Conversions.ToInteger(txt_cantidad.Text) == -1 | Conversions.ToInteger(txt_cantidad.Text) == 0)
            {
                closeandupdate(this);
                return;
            }

            if (Conversions.ToDouble(txt_cantidad.Text) == 0d)
            {
                Interaction.MsgBox("La cantidad ingresada no puede ser 0, ingrese nuevamente", Constants.vbExclamation, "Centrex");
                return;
            }

            if (!actualiza)
            {
                cant = Conversions.ToInteger(txt_cantidad.Text);
                Dispose();
                return;
            }

            if (!produccion & !ordenCompra & !comprobanteCompra)
            {
                if (VariablesGlobales.edita_item.cantidad - Conversions.ToDouble(txt_cantidad.Text) is { } arg1 && arg1 < 0)
                {
                    Interaction.MsgBox("No hay " + txt_cantidad.Text + " " + VariablesGlobales.edita_item.item.ToString() + " hay solo " + VariablesGlobales.edita_item.cantidad.ToString() + ".", Constants.vbExclamation);
                    // MsgBox("No hay " + txt_cantidad.Text + " " + VariablesGlobales.edita_item.item.ToString + " hay solo " + VariablesGlobales.edita_item.cantidad.ToString + ", ingrese una nueva cantidad o cancele", vbExclamation)
                    // Exit Sub
                }
            }

            if (produccion)
            {
                // Es un pedido de produccion
                if (!VariablesGlobales.agregaitem)
                {
                    addItemProducciontmp(VariablesGlobales.ConvertToItem(VariablesGlobales.edita_item), txt_cantidad.Text, VariablesGlobales.edita_item.IdItemTemporal);
                }
                else
                {
                    producciones.addItemProducciontmp(VariablesGlobales.ConvertToItem(VariablesGlobales.edita_item), Conversions.ToInteger(txt_cantidad.Text));
                }
            }
            else if (ordenCompra)
            {
                // Es una orden de compra
                if (!VariablesGlobales.agregaitem)
                {
                    ordenesCompras.addItemOCtmp(VariablesGlobales.ConvertToItem(VariablesGlobales.edita_item), txt_cantidad.Text, txt_precio.Text, VariablesGlobales.edita_item.IdItemTemporal);
                }
                else
                {
                    ordenesCompras.addItemOCtmp(VariablesGlobales.ConvertToItem(VariablesGlobales.edita_item), Conversions.ToInteger(txt_cantidad.Text), Conversions.ToDouble(txt_precio.Text));
                }
            }
            else if (comprobanteCompra)
            {
                // Es un comprobante de compra
                comprobantes_compras.add_item_comprobanteCompra(id_comprobanteCompra, VariablesGlobales.edita_item.id_item, Conversions.ToInteger(txt_cantidad.Text), Conversions.ToDouble(txt_precio.Text));
            }
            // Pedido normal
            else if (!VariablesGlobales.agregaitem)
            {
                Pedidos.AddItemPedidoTmp(VariablesGlobales.edita_item, Conversions.ToDouble(txt_cantidad.Text), Conversions.ToDouble(txt_precio.Text), idUsuario, idUnico, VariablesGlobales.edita_item.IdItemTemporal);
            }
            else
            {
                Pedidos.AddItemPedidoTmp(VariablesGlobales.edita_item, Conversions.ToDouble(txt_cantidad.Text), Conversions.ToDouble(txt_precio.Text), idUsuario, idUnico);
            }

            Dispose();
        }

        private void cmd_exit_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void txt_cantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Conversions.ToInteger(generales.esNumero(e)) == 0)
            {
                e.KeyChar = Conversions.ToChar("");
            }
        }
    }
}

