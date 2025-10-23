using System;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace Centrex
{
    public partial class frm_error_detalle
    {
        public frm_error_detalle()
        {
            InitializeComponent();
        }
        private void frm_error_detalle_Load(object sender, EventArgs e)
        {
            // Centrar el formulario
            CenterToParent();
        }

        public void MostrarError(string titulo, string mensaje, string detalles)
        {
            Text = titulo;
            txt_mensaje.Text = mensaje;
            txt_detalles.Text = detalles;

            // Seleccionar todo el texto para facilitar la copia
            txt_detalles.SelectAll();
            txt_detalles.Focus();
        }

        private void btn_copiar_Click(object sender, EventArgs e)
        {
            try
            {
                string textoCompleto = "TÍTULO: " + Text + Constants.vbCrLf + Constants.vbCrLf + "MENSAJE: " + txt_mensaje.Text + Constants.vbCrLf + Constants.vbCrLf + "DETALLES: " + txt_detalles.Text;

                Clipboard.SetText(textoCompleto);
                MessageBox.Show("Error copiado al portapapeles", "Centrex", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al copiar: " + ex.Message, "Centrex", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_cerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txt_detalles_KeyDown(object sender, KeyEventArgs e)
        {
            // Ctrl+A para seleccionar todo
            if (e.Control && e.KeyCode == Keys.A)
            {
                txt_detalles.SelectAll();
                e.Handled = true;
            }
        }
    }
}
