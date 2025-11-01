using System;
using System.Collections;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Centrex
{
    public class configInit
    {
        private string path;
        private string db;
        private string srvSQL;
        private string usrDB;
        private string pswDB;
        private string bckupDir;
        private string bckupFile;
        private string sepdec;
        private int itPerPage;
        private bool modDB;

        public configInit()
        {
            path = Application.StartupPath + @"\initConfig.jav";

            // Si el archivo no existe, crear uno por defecto
            if (!File.Exists(path))
            {
                CrearConfiguracionPorDefecto();
            }
        }

        public string nameDB
        {
            get
            {
                return db;
            }
            set
            {
                db = value;
                basedb = value;
            }
        }

        public string serverDB
        {
            get
            {
                return srvSQL.ToString();
            }
            set
            {
                srvSQL = value;
                serversql = value;
            }
        }

        public string userDB
        {
            get
            {
                return usrDB;
            }
            set
            {
                usrDB = value;
                usuariodb = value;
            }
        }

        public string passwordDB
        {
            get
            {
                return pswDB;
            }
            set
            {
                pswDB = value;
                passdb = value;
            }
        }

        public string backupPath
        {
            get
            {
                return bckupDir;
            }
            set
            {
                bckupDir = value;
                rutaBackup = value;
            }
        }

        public string backupFile
        {
            get
            {
                return bckupFile;
            }
            set
            {
                bckupFile = value;
                archivoBackup = value;
            }
        }

        public string sep_Decimal
        {
            get
            {
                return sepdec;
            }
            set
            {
                sepdec = value;
                sepDecimal = value;
            }
        }

        public string itemsPorPagina
        {
            get
            {
                return itPerPage.ToString();
            }
            set
            {
                itPerPage = int.Parse(value);
            }
        }

        public bool modsDB
        {
            get
            {
                return modDB;
            }
            set
            {
                modDB = value;
            }
        }

        public bool ModDB1
        {
            get
            {
                return modDB;
            }
            set
            {
                modDB = value;
            }
        }

        public bool ModDB2
        {
            get
            {
                return modDB;
            }
            set
            {
                modDB = value;
            }
        }

        public void guardarConfig()
        {
            var strEncrypt = new EncriptarType();
            var fs = File.Create(path);
            byte[] str;

            str = new UTF8Encoding(true).GetBytes("basedb = " + db + Environment.NewLine + "serversql = " + srvSQL + Environment.NewLine + "usuariodb = " + strEncrypt.Encriptar(usrDB) + Environment.NewLine + "passdb = " + strEncrypt.Encriptar(pswDB) + Environment.NewLine + "rutaBackup = " + bckupDir + Environment.NewLine + "nombreBackup = " + bckupFile + Environment.NewLine + "sepDecimal = " + sepdec + Environment.NewLine + "itPerPage = " + itPerPage + Environment.NewLine + "modificacionesDB = " + modDB);

            fs.Write(str, 0, str.Length);
            fs.Close();
        }

        private void CrearConfiguracionPorDefecto()
        {
            try
            {
                // Valores por defecto
                db = "dbCentrex";
                srvSQL = "127.0.0.1";
                usrDB = "sa";
                pswDB = "";
                bckupDir = Application.StartupPath + @"\Backups";
                bckupFile = "backup_" + DateTime.Now.ToString("yyyyMMdd") + ".bak";
                sepdec = ",";
                itPerPage = 20;
                modDB = false;

                // Crear directorio de backups si no existe
                if (!Directory.Exists(bckupDir))
                {
                    Directory.CreateDirectory(bckupDir);
                }

                // Guardar configuración
                guardarConfig();

                MessageBox.Show(
                    "Se ha creado un archivo de configuración por defecto.\n\n" +
                   "Por favor, verifique la configuración en:\n" +
                   "Configuración > Configuración del sistema",
               "Configuración Inicial",
                MessageBoxButtons.OK,
                         MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
            "Error al crear configuración por defecto: " + ex.Message,
                     "Error",
            MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
        }

        public void leerConfig()
        {
            // Verificar nuevamente si existe el archivo
            if (!File.Exists(path))
            {
                CrearConfiguracionPorDefecto();
                return;
            }

            var strDesc = new EncriptarType();
            StreamReader objReader = null;

            try
            {
                objReader = new StreamReader(path);
                string str = "";
                var arrSTR = new ArrayList();
                int c = 0;

                do
                {
                    c = c + 1;
                    str = objReader.ReadLine();
                    if (c == 1)
                    {
                        basedb = generales.obtieneValorConfig(str);
                        db = basedb;
                    }
                    else if (c == 2)
                    {
                        serversql = generales.obtieneValorConfig(str);
                        srvSQL = serversql;
                    }
                    else if (c == 3)
                    {
                        usuariodb = strDesc.Desencriptar(generales.obtieneValorConfig(str));
                        usrDB = usuariodb;
                    }
                    else if (c == 4)
                    {
                        passdb = strDesc.Desencriptar(generales.obtieneValorConfig(str));
                        pswDB = passdb;
                    }
                    else if (c == 5)
                    {
                        rutaBackup = generales.obtieneValorConfig(str);
                        bckupDir = rutaBackup;
                    }
                    else if (c == 6)
                    {
                        archivoBackup = generales.obtieneValorConfig(str);
                        bckupFile = archivoBackup;
                    }
                    else if (c == 7)
                    {
                        sepDecimal = generales.obtieneValorConfig(str);
                        sepdec = sepDecimal;
                    }
                    else if (c == 8)
                    {
                        string valorItPerPage = generales.obtieneValorConfig(str);
                        itXPage = int.Parse(valorItPerPage);
                        itPerPage = itXPage;
                    }
                    else if (c == 9)
                    {
                        string valorModDB = generales.obtieneValorConfig(str);
                        modificacionesDB = bool.Parse(valorModDB);
                        modDB = modificacionesDB;
                    }
                }
                while (!(str is null));
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                 "Error al leer configuración: " + ex.Message + "\n\n" +
                "Se creará una configuración por defecto.",
                  "Error de Configuración",
                  MessageBoxButtons.OK,
             MessageBoxIcon.Warning);

                CrearConfiguracionPorDefecto();
            }
            finally
            {
                if (objReader != null)
                {
                    objReader.Close();
                }
            }
        }
    }
}
