using System;
using System.Globalization;
using System.Windows.Forms;

namespace Centrex
{
    public partial class infoagregaitem
    {

        private bool produccion = false;
        private bool ordenCompra = false;
        private bool comprobanteCompra = false;
        private int id_comprobanteCompra = -1;
        private bool actualiza = true;
        private int idUsuario;
        private Guid idUnico;
        public int cant;

        public infoagregaitem()
        {
            InitializeComponent();
        }

        public infoagregaitem(int _idUsuario, Guid _idUnico)
        {
            InitializeComponent();
            idUsuario = _idUsuario;
            idUnico = _idUnico;
        }

        public infoagregaitem(bool _produccion, bool _ordenCompra, bool _actualiza, int _idUsuario, Guid _idUnico)
        {
            InitializeComponent();
            produccion = _produccion;
            ordenCompra = _ordenCompra;
            actualiza = _actualiza;
            idUsuario = _idUsuario;
            idUnico = _idUnico;
        }

        public infoagregaitem(bool _comprobanteCompra, int _id_comprobanteCompra)
        {
            InitializeComponent();
            comprobanteCompra = _comprobanteCompra;
            id_comprobanteCompra = _id_comprobanteCompra;
        }

        private void infoagregaitem_FormClosed(object sender, FormClosedEventArgs e)
        {
        }

        private void infoagregaitem_Load(object sender, EventArgs e)
        {
            lbl_item.Text = edita_item.Item;
            lbl_desc.Text = edita_item.Descript;
            lbl_stock.Text = edita_item.Cantidad.Value.ToString();

            if (produccion)
                txt_precio.Enabled = false;

            if (agregaitem == true)
            {
                txt_cantidad.Text = 1.ToString();
                txt_precio.Text = edita_item.PrecioLista.ToString();
            }
            else if (produccion)
            {
                txt_cantidad.Text = producciones.askCantidadCargadaProduccion(edita_item.IdItem, -1, edita_item.IdItemTemporal).ToString();
            }
            else if (ordenCompra)
            {
                txt_cantidad.Text = ordenesCompras.askCantidadCargadaOC(edita_item.IdItem, id_tmpOCItem: edita_item.IdItemTemporal).ToString();
                txt_precio.Text = ordenesCompras.askPrecioCargadoOC(edita_item.IdItem, id_tmpOCItem: edita_item.IdItemTemporal).ToString();
            }
            else if (comprobanteCompra)
            {
            }
            else
            {
                txt_cantidad.Text = Pedidos.askCantidadCargada(edita_item.IdItem, -1, "tmppedidos_items", idUsuario, idUnico).ToString();
                txt_precio.Text = Pedidos.askPreciocargado(edita_item.IdItem, -1, "tmppedidos_items", idUsuario, idUnico).ToString();
            }
        }

        private void cmd_ok_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_cantidad.Text))
                txt_cantidad.Text = 0.ToString();

            int cantidadInt = int.Parse(txt_cantidad.Text);
            if (cantidadInt == -1 || cantidadInt == 0)
            {
                closeandupdate(this);
                return;
            }

            if (double.Parse(txt_cantidad.Text) == 0d)
            {
                MessageBox.Show("La cantidad ingresada no puede ser 0, ingrese nuevamente", "Centrex", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (!actualiza)
            {
                cant = int.Parse(txt_cantidad.Text);
                Dispose();
                return;
            }

            if (!produccion && !ordenCompra && !comprobanteCompra)
            {
                double cantidadDisponible = edita_item.Cantidad.Value - double.Parse(txt_cantidad.Text);
                if (cantidadDisponible < 0)
                {
                    MessageBox.Show("No hay " + txt_cantidad.Text + " " + edita_item.Item.ToString() + " hay solo " + edita_item.Cantidad.ToString() + ".", "Centrex", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }

            if (produccion)
            {
                // Es un pedido de produccion
                if (!agregaitem)
                {
                    addItemProducciontmp(ConvertToItem(edita_item), int.Parse(txt_cantidad.Text), edita_item.IdItemTemporal);
                }
                else
                {
                    producciones.addItemProducciontmp(ConvertToItem(edita_item), int.Parse(txt_cantidad.Text));
                }
            }
            else if (ordenCompra)
            {
                // Es una orden de compra
                if (!agregaitem)
                {
                    ordenesCompras.addItemOCtmp(ConvertToItem(edita_item), int.Parse(txt_cantidad.Text), decimal.Parse(txt_precio.Text, CultureInfo.CurrentCulture), edita_item.IdItemTemporal);
                }
                else
                {
                    ordenesCompras.addItemOCtmp(ConvertToItem(edita_item), int.Parse(txt_cantidad.Text), decimal.Parse(txt_precio.Text));
                }
            }
            else if (comprobanteCompra)
            {
                // Es un comprobante de compra
                comprobantes_compras.add_item_comprobanteCompra(id_comprobanteCompra, edita_item.IdItem, int.Parse(txt_cantidad.Text), decimal.Parse(txt_precio.Text));
            }
            // Pedido normal
            else if (!agregaitem)
            {
                Pedidos.AddItemPedidoTmp(edita_item, int.Parse(txt_cantidad.Text), decimal.Parse(txt_precio.Text), idUsuario, idUnico, edita_item.IdItemTemporal);
            }
            else
            {
                Pedidos.AddItemPedidoTmp(edita_item, int.Parse(txt_cantidad.Text), decimal.Parse(txt_precio.Text), idUsuario, idUnico);
            }

            Dispose();
        }

        private void cmd_exit_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void txt_cantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!generales.esNumero(e))
            {
                e.KeyChar = '\0';
            }
        }
    }
}

