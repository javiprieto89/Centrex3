using System.Collections;
// Imports System
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Centrex
{
    // Imports System.Collections

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
                VariablesGlobales.basedb = value;
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
                VariablesGlobales.serversql = value;
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
                VariablesGlobales.usuariodb = value;
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
                VariablesGlobales.passdb = value;
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
                VariablesGlobales.rutaBackup = value;
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
                VariablesGlobales.archivoBackup = value;
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
                VariablesGlobales.sepDecimal = value;
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
                itPerPage = Conversions.ToInteger(value);
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

            str = new UTF8Encoding(true).GetBytes("basedb = " + db + Constants.vbCrLf + "serversql = " + srvSQL + Constants.vbCrLf + "usuariodb = " + strEncrypt.Encriptar(usrDB) + Constants.vbCrLf + "passdb = " + strEncrypt.Encriptar(pswDB) + Constants.vbCrLf + "rutaBackup = " + bckupDir + Constants.vbCrLf + "nombreBackup = " + bckupFile + Constants.vbCrLf + "sepDecimal = " + sepdec + Constants.vbCrLf + "itPerPage = " + itPerPage + Constants.vbCrLf + "modificacionesDB = " + modDB);

            fs.Write(str, 0, str.Length);
            fs.Close();
        }

        public void leerConfig()
        {
            var strDesc = new EncriptarType();
            var objReader = new StreamReader(path);
            string str = "";
            var arrSTR = new ArrayList();
            int c = 0;

            do
            {
                c = c + 1;
                str = objReader.ReadLine();
                if (c == 1)
                {
                    VariablesGlobales.basedb = generales.obtieneValorConfig(str);
                    db = VariablesGlobales.basedb;
                }
                else if (c == 2)
                {
                    VariablesGlobales.serversql = generales.obtieneValorConfig(str);
                    srvSQL = VariablesGlobales.serversql;
                }
                else if (c == 3)
                {
                    VariablesGlobales.usuariodb = strDesc.Desencriptar(generales.obtieneValorConfig(str));
                    usrDB = VariablesGlobales.usuariodb;
                }
                else if (c == 4)
                {
                    VariablesGlobales.passdb = strDesc.Desencriptar(generales.obtieneValorConfig(str));
                    pswDB = VariablesGlobales.passdb;
                }
                else if (c == 5)
                {
                    VariablesGlobales.rutaBackup = generales.obtieneValorConfig(str);
                    bckupDir = VariablesGlobales.rutaBackup;
                }
                else if (c == 6)
                {
                    VariablesGlobales.archivoBackup = generales.obtieneValorConfig(str);
                    bckupFile = VariablesGlobales.archivoBackup;
                }
                else if (c == 7)
                {
                    VariablesGlobales.sepDecimal = generales.obtieneValorConfig(str);
                    sepdec = VariablesGlobales.sepDecimal;
                }
                else if (c == 8)
                {
                    VariablesGlobales.itXPage = Conversions.ToInteger(generales.obtieneValorConfig(str));
                    itPerPage = VariablesGlobales.itXPage;
                }
                else if (c == 9)
                {
                    VariablesGlobales.modificacionesDB = Conversions.ToBoolean(generales.obtieneValorConfig(str));
                    modDB = VariablesGlobales.modificacionesDB;
                }
            }
            while (!(str is null));
            objReader.Close();
        }
    }
}
