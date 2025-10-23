using System;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace Centrex
{
    public partial class add_itemManual
    {
        // Private esEdicion As Boolean

        private int idUsuario;
        private string idUnico;

        public add_itemManual() : base()
        {
            // Esta llamada es exigida por el diseñador.
            InitializeComponent();

            // Agregue cualquier inicialización después de la llamada a InitializeComponent().

        }

        public add_itemManual(item i, int _idUsuario, string _idUnico) : base()
        {
            // Esta llamada es exigida por el diseñador.
            InitializeComponent();

            // Agregue cualquier inicialización después de la llamada a InitializeComponent().
            txt_item.Text = i.descript;
            txt_cantidad.Text = i.cantidad.ToString();
            txt_precio.Text = i.precio_lista.ToString();
            // esEdicion = True

            idUsuario = _idUsuario;
            idUnico = _idUnico;
        }

        public add_itemManual(int _idUsuario, string _idUnico)
        {

            // Esta llamada es exigida por el diseñador.
            InitializeComponent();

            // Agregue cualquier inicialización después de la llamada a InitializeComponent().
            idUsuario = _idUsuario;
            idUnico = _idUnico;
        }

        private void cmd_exit_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void cmd_ok_Click(object sender, EventArgs e)
        {
            var cant = default(decimal);
            decimal precio;

            try
            {
                cant = Conversions.ToDecimal(txt_cantidad.Text);
                precio = Conversions.ToDecimal(txt_precio.Text);
            }
            catch (Exception ex)
            {
            }


            if (string.IsNullOrEmpty(txt_item.Text))
            {
                Interaction.MsgBox("Debe ingresar un item antes de poder continuar", (MsgBoxStyle)((int)Constants.vbCritical + (int)Constants.vbOKOnly), "ComputronSystems");
                return;
            }
            else if (string.IsNullOrEmpty(txt_cantidad.Text))
            {
                Interaction.MsgBox("Debe ingresar la cantidad antes de poder continuar", (MsgBoxStyle)((int)Constants.vbCritical + (int)Constants.vbOKOnly), "ComputronSystems");
                return;
            }
            else if (Math.Truncate(cant) != cant)
            {
                Interaction.MsgBox("La cantidad no puede ser decimal", (MsgBoxStyle)((int)Constants.vbCritical + (int)Constants.vbOKOnly), "ComputronSystems");
                return;
            }
            else if (string.IsNullOrEmpty(txt_precio.Text))
            {
                Interaction.MsgBox("Debe ingresar el precio antes de poder continuar", (MsgBoxStyle)((int)Constants.vbCritical + (int)Constants.vbOKOnly), "ComputronSystems");
                return;
            }

            var i = new ItemEntity();

            i.cantidad = Conversions.ToInteger(txt_cantidad.Text);
            i.descript = txt_item.Text;
            i.precio_lista = Conversions.ToDecimal(txt_precio.Text);
            i.item = "MANUAL";

            Pedidos.AddItemPedidoTmp(i, i.cantidad, i.precio_lista, idUsuario, idUnico, (VariablesGlobales.edita_item?.IdItemTemporal) ?? -1);

            Dispose();
        }

        private void txt_cantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!generales.esNumero(e))
            {
                e.KeyChar = Conversions.ToChar("");
            }
        }

        private void txt_precio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!generales.esNumero(e) & Conversions.ToString(e.KeyChar) != "." & Conversions.ToString(e.KeyChar) != ",")
            {
                e.KeyChar = Conversions.ToChar("");
            }

            if (Strings.Asc(e.KeyChar) == 8)
                return;
        }
    }
}
