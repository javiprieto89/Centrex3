using System;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace Centrex
{
    public partial class BackupDB
    {
        private bool resultado;

        public BackupDB()
        {
            InitializeComponent();
        }
        private void BackupDB_Load(object sender, EventArgs e)
        {
            string anio = DateTime.Now.Year.ToString();
            string mes = DateTime.Now.Month.ToString();
            string dia = DateTime.Now.Day.ToString();
            string hora = DateTime.Now.Hour.ToString();
            string minuto = DateTime.Now.Minute.ToString();
            string segundo = DateTime.Now.Second.ToString();
            string timeStamp = anio + mes + dia + "_" + hora + minuto + segundo;
            string archivoBackup_local;
            Timer1.Enabled = true;
            ProgressBar1.Visible = true;


            archivoBackup_local = VariablesGlobales.archivoBackup + "_" + timeStamp + ".bak";
            resultado = VariablesGlobales.dbBackup(VariablesGlobales.rutaBackup, archivoBackup_local);
            // If pc = "BRUNO" Then
            // resultado = dbBackup(Application.StartupPath + "\SQL\BKP", archivoBackup_local)
            // End If

            var directory = new System.IO.DirectoryInfo(Application.StartupPath + @"\SQL\BKP");

            foreach (System.IO.FileInfo @file in directory.GetFiles())
            {
                if ((DateTime.Now - @file.CreationTime).Days > 10)
                    @file.Delete();
            }
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            if (ProgressBar1.Value == 100)
            {
                Timer1.Enabled = false;
                ProgressBar1.Visible = false;
                if (!resultado)
                {
                    // MsgBox("Se ha realizado correctamente el backup", vbInformation, "Centrex")
                    // Else
                    Interaction.MsgBox("Ha ocurrido un error al realizar un backup de la base de datos" + '\r' + "Consulte con el programador", Constants.vbInformation, "Centrex");
                }
                closeandupdate(this);
            }
            else
            {
                ProgressBar1.Value = ProgressBar1.Value + 5;
            }
        }
    }
}
