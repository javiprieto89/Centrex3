using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Centrex
{
    public partial class config
    {
        public config()
        {
            InitializeComponent();
        }
        [DllImport("shell32.dll", EntryPoint = "ShellExecuteA")]
        private static extern long ShellExecute(long hwnd, string lpOperation, string lpFile, string lpParameters, string lpDirectory, long nShowCmd);
        private configInit c = new configInit();

        private void tctrl_Selecting(object sender, TabControlCancelEventArgs e)
        {
            var ordenClientes = new List<Tuple<string, bool>> { Tuple.Create("RazonSocial", true) };
            generales.Cargar_Combo(
                ref cmb_clientes,
                entidad: "ClienteEntity",
                displaymember: "RazonSocial",
                valuemember: "IdCliente",
                predet: -1,
                soloActivos: true,
                orden: ordenClientes);
            cmb_clientes.SelectedIndex = -1;
        }

        private void config_Load(object sender, EventArgs e)
        {
            c.leerConfig();

            txtdb.Text = c.nameDB;
            txtserver.Text = c.serverDB;
            txtuser.Text = c.userDB;
            txtpassword.Text = c.passwordDB;
            txt_rutaBackup.Text = c.backupPath;
            txt_nombreBackup.Text = c.backupFile;
            txt_itPerPage.Text = c.itemsPorPagina;

            dtp_fecha_sistema.Value = DateTime.Now;
        }

        private void cmd_ok_Click(object sender, EventArgs e)
        {
            c.nameDB = txtdb.Text;
            c.serverDB = txtserver.Text;
            c.userDB = txtuser.Text;
            c.passwordDB = txtpassword.Text;
            c.backupPath = txt_rutaBackup.Text;
            c.backupFile = txt_nombreBackup.Text;
            c.itemsPorPagina = txt_itPerPage.Text;

            c.guardarConfig();
            Dispose();
        }

        private void cmd_exit_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void cmd_elegirRuta_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog1.ShowDialog();
            txt_rutaBackup.Text = FolderBrowserDialog1.SelectedPath;
        }

        private void cmd_abrirCarpeta_Click(object sender, EventArgs e)
        {
            if (System.IO.Directory.Exists(txt_rutaBackup.Text))
            {
                string arglpOperation = "Open";
                string arglpFile = txt_rutaBackup.Text;
                string arglpParameters = "";
                string arglpDirectory = "";
                config.ShellExecute(0L, arglpOperation, arglpFile, arglpParameters, arglpDirectory, 1L);
                txt_rutaBackup.Text = arglpFile;
            }
            else
            {
                MessageBox.Show(
                    "La ruta ingresada:\n" + txt_rutaBackup.Text + "\nNO existe\n\n" +
                    "Por favor escriba un directorio válido o selecciónelo desde el botón: 'Elegir carpeta'",
                    "Computron",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void cmd_cierre_diario_Click(object sender, EventArgs e)
        {

        }

        private void tBackup_Click(object sender, EventArgs e)
        {

        }
    }
}
