using System;
using System.Drawing;
using System.Windows.Forms;

namespace Centrex
{
    public partial class ErrorDialog : Form
    {
        public ErrorDialog(string titulo, string mensaje)
        {
            InitializeComponent();

            Text = titulo;
            txtError.Text = mensaje;
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnCopiar_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(txtError.Text);
            MessageBox.Show("Texto copiado al portapapeles.", "Centrex", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
