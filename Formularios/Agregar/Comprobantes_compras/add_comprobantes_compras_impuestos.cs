using System;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace Centrex
{
    public partial class add_comprobantes_compras_impuestos
    {
        private int id_comprobanteCompra;
        private int id_impuesto;
        private impuesto i = new impuesto();

        public add_comprobantes_compras_impuestos()
        {

            // Esta llamada es exigida por el diseñador.
            InitializeComponent();

            // Agregue cualquier inicialización después de la llamada a InitializeComponent().

        }

        public add_comprobantes_compras_impuestos(int _id_comprobanteCompra, int _id_impuesto)
        {

            // Esta llamada es exigida por el diseñador.
            InitializeComponent();

            // Agregue cualquier inicialización después de la llamada a InitializeComponent().
            id_comprobanteCompra = _id_comprobanteCompra;
            id_impuesto = _id_impuesto;

        }
        private void add_comprobantes_compras_impuestos_Load(object sender, EventArgs e)
        {
            i = impuestos.info_impuesto(id_impuesto);
            lbl_impuesto.Text = i.nombre;
        }

        private void txt_importe_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!generales.esNumero(e))
            {
                e.KeyChar = Conversions.ToChar("");
            }
            else
            {
                e.KeyChar = Conversions.ToChar(Strings.FormatCurrency(e));
            }
        }

        private void cmd_ok_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_importe.Text))
            {
                Interaction.MsgBox("El campo importe está vació", (MsgBoxStyle)((int)Constants.vbExclamation + (int)Constants.vbOKOnly), "Centrex");
                return;
            }

            if (!comprobantes_compras.add_impuesto_comprobanteCompra(id_comprobanteCompra, i.id_impuesto, Conversions.ToDouble(txt_importe.Text)))
            {
                Interaction.MsgBox("Hubo un error al agregar el impuesto, consulte con el programador.", (MsgBoxStyle)((int)Constants.vbCritical + (int)Constants.vbOKOnly), "Centrex");
                return;
            }

            closeandupdate(this);
        }

        private void cmd_exit_Click(object sender, EventArgs e)
        {
            closeandupdate(this);
        }
    }
}
