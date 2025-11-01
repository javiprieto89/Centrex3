using System;
using System.Windows.Forms;

namespace Centrex
{
    public partial class add_comprobantes_compras_conceptos
    {
        private int id_concepto;
        private int id_comprobanteCompra;
        private ConceptoCompraEntity c = new ConceptoCompraEntity();
        public add_comprobantes_compras_conceptos()
        {

            // Esta llamada es exigida por el diseñador.
            InitializeComponent();

            // Agregue cualquier inicialización después de la llamada a InitializeComponent().

        }

        public add_comprobantes_compras_conceptos(int _id_comprobanteCompra, int _id_concepto)
        {

            // Esta llamada es exigida por el diseñador.
            InitializeComponent();

            // Agregue cualquier inicialización después de la llamada a InitializeComponent().
            id_comprobanteCompra = _id_comprobanteCompra;
            id_concepto = _id_concepto;
        }
        private void add_comprobantes_compras_conceptos_Load(object sender, EventArgs e)
        {
            c = info_concepto_compra(id_concepto);

            lbl_concepto.Text = c.Concepto;
        }

        private void txt_subtotal_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txt_iva_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txt_total_KeyPress(object sender, KeyPressEventArgs e)
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
            if (string.IsNullOrEmpty(txt_subtotal.Text))
            {
                Interaction.MsgBox("El campo subtotal está vacio.", (MsgBoxStyle)((int)Constants.vbExclamation + (int)Constants.vbOKOnly), "Centrex");
                return;
            }
            else if (string.IsNullOrEmpty(txt_iva.Text))
            {
                Interaction.MsgBox("El cambo I.V.A. está vacio.", (MsgBoxStyle)((int)Constants.vbExclamation + (int)Constants.vbOKOnly), "Centrex");
                return;
            }
            else if (string.IsNullOrEmpty(txt_total.Text))
            {
                Interaction.MsgBox("El campo total está vacio.", (MsgBoxStyle)((int)Constants.vbExclamation + (int)Constants.vbOKOnly), "Centrex");
                return;
            }

            if (!comprobantes_compras.add_concepto_comprobanteCompra(id_comprobanteCompra, c.IdConceptoCompra, Conversions.ToDecimal(txt_subtotal.Text), Conversions.ToDecimal(txt_iva.Text), Conversions.ToDecimal(txt_total.Text)))
            {
                Interaction.MsgBox("Hubo un problema al cargar el concepto en la base de datos, consulte con el programador.", (MsgBoxStyle)((int)Constants.vbCritical + (int)Constants.vbOKOnly), "Centrex");
                closeandupdate(this);
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
