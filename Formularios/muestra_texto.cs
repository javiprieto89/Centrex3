using System;

namespace Centrex
{
    public partial class muestra_texto
    {
        private string str;
        public muestra_texto()
        {

            // Esta llamada es exigida por el diseñador.
            InitializeComponent();

            // Agregue cualquier inicialización después de la llamada a InitializeComponent().

        }

        public muestra_texto(string _str)
        {

            // Esta llamada es exigida por el diseñador.
            InitializeComponent();

            // Agregue cualquier inicialización después de la llamada a InitializeComponent().
            str = _str;
        }

        private void muestra_texto_Load(object sender, EventArgs e)
        {
            txt_msg.Text = str;
        }

        private void cmd_ok_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
