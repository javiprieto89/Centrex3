using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Centrex.Funciones;

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
            {
                ref var withBlock = ref c;
                withBlock.leerConfig();

                txtdb.Text = withBlock.nameDB;
                txtserver.Text = withBlock.serverDB;
                txtuser.Text = withBlock.userDB;
                txtpassword.Text = withBlock.passwordDB;
                txt_rutaBackup.Text = withBlock.backupPath;
                txt_nombreBackup.Text = withBlock.backupFile;
                txt_itPerPage.Text = withBlock.itemsPorPagina;
            }

            dtp_fecha_sistema.Value = DateTime.Now;
        }

        private void cmd_ok_Click(object sender, EventArgs e)
        {
            {
                ref var withBlock = ref c;
                withBlock.nameDB = txtdb.Text;
                withBlock.serverDB = txtserver.Text;
                withBlock.userDB = txtuser.Text;
                withBlock.passwordDB = txtpassword.Text;
                withBlock.backupPath = txt_rutaBackup.Text;
                withBlock.backupFile = txt_nombreBackup.Text;
                withBlock.itemsPorPagina = txt_itPerPage.Text;
            }

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
                txt_rutaBackup.Text = arglpFile; // Para Abrir Carpetas
            }
            else
            {
                Interaction.MsgBox("La ruta ingresada: " + Constants.vbCrLf + txt_rutaBackup.Text + "NO existe" + Constants.vbCrLf + "Por favor escriba un directorio válido o seleccioneló desde el botón: 'Elegir carpeta'", (MsgBoxStyle)((int)Constants.vbCritical + (int)Constants.vbOKOnly), "Computron");
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
