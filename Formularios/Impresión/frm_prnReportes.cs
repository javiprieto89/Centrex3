using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing.Printing;
using Microsoft.Reporting.WinForms;
using Microsoft.VisualBasic;

namespace Centrex
{

    public partial class frm_prnReportes
    {
        // Generales
        private string archivoRpt;
        private int rptID;
        private string spEmpresa;
        private string spCabecera;
        private string spDetalle;

        private string dsEmpresa;
        private string dsCabecera;
        private string dsDetalle;
        // Generales

        // Para cobros
        private bool esCobro;
        // Para pagos
        private bool esPago;

        private string spCheque;
        private string spTransferencia;
        private string spRetencion;

        private string dsCheque;
        private string dsTransferencia;
        private string dsRetencion;
        // Para cobros

        private SqlCommand comando = new SqlCommand();

        public frm_prnReportes()
        {
            InitializeComponent();
        }

        public frm_prnReportes(string _archivoRpt, string _spEmpresa, string _spCabecera, string _spDetalle, string _dsEmpresa, string _dsCabecera, string _dsDetalle, int _id)
        {

            InitializeComponent();

            archivoRpt = _archivoRpt;
            spEmpresa = _spEmpresa;
            spCabecera = _spCabecera;
            spDetalle = _spDetalle;
            dsEmpresa = _dsEmpresa;
            dsCabecera = _dsCabecera;
            dsDetalle = _dsDetalle;
            rptID = _id;
        }

        public frm_prnReportes(string _archivoRpt, string _spEmpresa, string _spCabecera, string _spCheque, string _spTransferencia, string _spRetencion, string _dsEmpresa, string _dsCabecera, string _dsCheque, string _dsTransferencia, string _dsRetencion, int _id, bool _esCobro)
        {

            InitializeComponent();

            archivoRpt = _archivoRpt;
            spEmpresa = _spEmpresa;
            spCabecera = _spCabecera;
            spCheque = _spCheque;
            spTransferencia = _spTransferencia;
            spRetencion = _spRetencion;
            dsEmpresa = _dsEmpresa;
            dsCabecera = _dsCabecera;
            dsCheque = _dsCheque;
            dsTransferencia = _dsTransferencia;
            dsRetencion = _dsRetencion;
            rptID = _id;
            esCobro = _esCobro;
        }

        public frm_prnReportes(string _archivoRpt, string _spEmpresa, string _spCabecera, string _spCheque, string _spTransferencia, string _dsEmpresa, string _dsCabecera, string _dsCheque, string _dsTransferencia, int _id, bool _esPago)
        {

            InitializeComponent();

            archivoRpt = _archivoRpt;
            spEmpresa = _spEmpresa;
            spCabecera = _spCabecera;
            spCheque = _spCheque;
            spTransferencia = _spTransferencia;
            dsEmpresa = _dsEmpresa;
            dsCabecera = _dsCabecera;
            dsCheque = _dsCheque;
            dsTransferencia = _dsTransferencia;
            rptID = _id;
            esPago = _esPago;
        }

        private void frm_prnReportes_Load(object sender, EventArgs e)
        {
            string guardarComo;
            string sqlstr;

            guardarComo = archivoRpt + " " + rptID.ToString();
            archivoRpt = "Centrex." + archivoRpt + ".rdlc";

            rpt_view.ProcessingMode = ProcessingMode.Local;
            rpt_view.LocalReport.DataSources.Clear();

            var da = new SqlDataAdapter();

            if (esCobro)
            {
                var DS_Empresa = new DataSet(dsEmpresa);
                var DS_Cabecera = new DataSet(dsCabecera);
                var DS_Cheque = new DataSet(dsCheque);
                var DS_Transferencia = new DataSet(dsTransferencia);
                var DS_Retencion = new DataSet(dsRetencion);

                DataTable DT_Empresa = new DataTable(), DT_Cabecera = new DataTable(), DT_Cheque = new DataTable(), DT_Transferencia = new DataTable(), DT_Retencion = new DataTable();

                try
                {
                    comando.CommandType = CommandType.Text;
                    comando.Connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["CentrexConnection"].ConnectionString);

                    sqlstr = "EXEC [dbo].[" + spEmpresa + "]";
                    comando.CommandText = sqlstr;
                    da.SelectCommand = comando;
                    da.Fill(DS_Empresa, "Tabla");

                    sqlstr = "EXEC [dbo].[" + spCabecera + "] @id = " + rptID.ToString();
                    comando.CommandText = sqlstr;
                    da.Fill(DS_Cabecera, "Tabla");

                    sqlstr = "EXEC [dbo].[" + spCheque + "] @id = " + rptID.ToString();
                    comando.CommandText = sqlstr;
                    da.Fill(DS_Cheque, "Tabla");

                    sqlstr = "EXEC [dbo].[" + spTransferencia + "] @id = " + rptID.ToString();
                    comando.CommandText = sqlstr;
                    da.Fill(DS_Transferencia, "Tabla");

                    sqlstr = "EXEC [dbo].[" + spRetencion + "] @id = " + rptID.ToString();
                    comando.CommandText = sqlstr;
                    da.Fill(DS_Retencion, "Tabla");

                    DT_Empresa = DS_Empresa.Tables[0];
                    DT_Cabecera = DS_Cabecera.Tables[0];
                    DT_Cheque = DS_Cheque.Tables[0];
                    DT_Transferencia = DS_Transferencia.Tables[0];
                    DT_Retencion = DS_Retencion.Tables[0];
                }

                catch (Exception ex)
                {
                    Interaction.MsgBox("Error al generar reporte: " + ex.Message, Constants.vbCritical);
                    return;
                }

                {
                    var withBlock = rpt_view;
                    withBlock.LocalReport.DataSources.Add(new ReportDataSource(dsEmpresa, DT_Empresa));
                    withBlock.LocalReport.DataSources.Add(new ReportDataSource(dsCabecera, DT_Cabecera));
                    withBlock.LocalReport.DataSources.Add(new ReportDataSource(dsCheque, DT_Cheque));
                    withBlock.LocalReport.DataSources.Add(new ReportDataSource(dsTransferencia, DT_Transferencia));
                    withBlock.LocalReport.DataSources.Add(new ReportDataSource(dsRetencion, DT_Retencion));
                }
            }

            else if (esPago)
            {
                var DS_Empresa = new DataSet(dsEmpresa);
                var DS_Cabecera = new DataSet(dsCabecera);
                var DS_Cheque = new DataSet(dsCheque);
                var DS_Transferencia = new DataSet(dsTransferencia);

                DataTable DT_Empresa = new DataTable(), DT_Cabecera = new DataTable(), DT_Cheque = new DataTable(), DT_Transferencia = new DataTable();

                try
                {
                    comando.CommandType = CommandType.Text;
                    comando.Connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["CentrexConnection"].ConnectionString);

                    sqlstr = "EXEC [dbo].[" + spEmpresa + "]";
                    comando.CommandText = sqlstr;
                    da.Fill(DS_Empresa, "Tabla");

                    sqlstr = "EXEC [dbo].[" + spCabecera + "] @id = " + rptID.ToString();
                    comando.CommandText = sqlstr;
                    da.Fill(DS_Cabecera, "Tabla");

                    sqlstr = "EXEC [dbo].[" + spCheque + "] @id = " + rptID.ToString();
                    comando.CommandText = sqlstr;
                    da.Fill(DS_Cheque, "Tabla");

                    sqlstr = "EXEC [dbo].[" + spTransferencia + "] @id = " + rptID.ToString();
                    comando.CommandText = sqlstr;
                    da.Fill(DS_Transferencia, "Tabla");

                    DT_Empresa = DS_Empresa.Tables[0];
                    DT_Cabecera = DS_Cabecera.Tables[0];
                    DT_Cheque = DS_Cheque.Tables[0];
                    DT_Transferencia = DS_Transferencia.Tables[0];
                }

                catch (Exception ex)
                {
                    Interaction.MsgBox("Error al generar reporte: " + ex.Message, Constants.vbCritical);
                    return;
                }

                {
                    var withBlock2 = rpt_view;
                    withBlock2.LocalReport.DataSources.Add(new ReportDataSource(dsEmpresa, DT_Empresa));
                    withBlock2.LocalReport.DataSources.Add(new ReportDataSource(dsCabecera, DT_Cabecera));
                    withBlock2.LocalReport.DataSources.Add(new ReportDataSource(dsCheque, DT_Cheque));
                    withBlock2.LocalReport.DataSources.Add(new ReportDataSource(dsTransferencia, DT_Transferencia));
                }
            }

            else
            {
                var DS_Empresa = new DataSet(dsEmpresa);
                var DS_Cabecera = new DataSet(dsCabecera);
                var DS_Detalle = new DataSet(dsDetalle);

                DataTable DT_Empresa = new DataTable(), DT_Cabecera = new DataTable(), DT_Detalle = new DataTable();

                try
                {
                    comando.CommandType = CommandType.Text;
                    comando.Connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["CentrexConnection"].ConnectionString);

                    sqlstr = "EXEC [dbo].[" + spEmpresa + "]";
                    comando.CommandText = sqlstr;
                    da.Fill(DS_Empresa, "Tabla");

                    sqlstr = "EXEC [dbo].[" + spCabecera + "] @id = " + rptID.ToString();
                    comando.CommandText = sqlstr;
                    da.Fill(DS_Cabecera, "Tabla");

                    sqlstr = "EXEC [dbo].[" + spDetalle + "] @id = " + rptID.ToString();
                    comando.CommandText = sqlstr;
                    da.Fill(DS_Detalle, "Tabla");

                    DT_Empresa = DS_Empresa.Tables[0];
                    DT_Cabecera = DS_Cabecera.Tables[0];
                    DT_Detalle = DS_Detalle.Tables[0];
                }

                catch (Exception ex)
                {
                    Interaction.MsgBox("Error al generar reporte: " + ex.Message, Constants.vbCritical);
                    return;
                }

                {
                    var withBlock1 = rpt_view;
                    withBlock1.LocalReport.DataSources.Add(new ReportDataSource(dsEmpresa, DT_Empresa));
                    withBlock1.LocalReport.DataSources.Add(new ReportDataSource(dsCabecera, DT_Cabecera));
                    withBlock1.LocalReport.DataSources.Add(new ReportDataSource(dsDetalle, DT_Detalle));
                }
            }

            {
                var withBlock3 = rpt_view;
                withBlock3.PrinterSettings.Copies = 2;
                withBlock3.LocalReport.ReportEmbeddedResource = archivoRpt;
                withBlock3.LocalReport.DisplayName = guardarComo;
                withBlock3.PrinterSettings.PrintRange = PrintRange.SomePages;
                withBlock3.PrinterSettings.FromPage = 1;
                withBlock3.PrinterSettings.ToPage = 1;
                withBlock3.RefreshReport();
            }
        }
    }
}
