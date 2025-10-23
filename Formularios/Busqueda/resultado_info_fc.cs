using System;

namespace Centrex
{
    public partial class resultado_info_fc
    {

        public resultado_info_fc()
        {

            // Esta llamada es exigida por el diseñador.
            InitializeComponent();

            // Agregue cualquier inicialización después de la llamada a InitializeComponent().

        }

        public resultado_info_fc(string _cae, string _subtotal, string _impuestos, string _total, string _cuit)
        {

            // Esta llamada es exigida por el diseñador.
            InitializeComponent();

            // Agregue cualquier inicialización después de la llamada a InitializeComponent().
            var c = new cliente();

            c = clientes.info_cliente(clientes.existecliente(_cuit).ToString());


            txt_cae.Text = _cae;
            txt_subtotal.Text = "$ " + _subtotal;
            txt_impuestos.Text = "$ " + _impuestos;
            txt_total.Text = "$ " + _total;
            txt_CUIT.Text = _cuit;
            txt_cliente.Text = c.razon_social;
        }

        private void resultado_info_fc_Load(object sender, EventArgs e)
        {

        }
    }
}
