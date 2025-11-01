/*
 * FORMULARIO DE IMPRESIÓN DE ORDEN DE PAGO - COMENTADO TEMPORALMENTE
 * Este formulario maneja la impresión de órdenes de pago usando ReportViewer
 * Se comenta temporalmente durante la migración de VB.NET a C# y EF Core
 * 
 * TODO: Migrar completamente a EF Core y actualizar lógica de impresión
 */

/*
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing.Printing;
//using Microsoft.Reporting.WinForms;
using Microsoft.VisualBasic;

namespace Centrex
{

    public class frm_prnOrdenDePago
    {
        //// Generales
        //private int ID;

        //private SqlCommand comando = new SqlCommand();

        public frm_prnOrdenDePago()
        {

            // Llamada necesaria para el diseñador.
            //InitializeComponent();
            //base.Load += frm_prnReciboPago_Load;

            // Agregue cualquier inicialización después de la llamada a InitializeComponent().

        }

        //public frm_prnOrdenDePago(int _id)
        //{

        //    // Esta llamada es exigida por el diseñador.
        //    InitializeComponent();

        //    // Agregue cualquier inicialización después de la llamada a InitializeComponent().
        //    ID = _id;
        //    base.Load += frm_prnReciboPago_Load;
        //}


        private void frm_prnReciboPago_Load(object sender, EventArgs e)
        {
            //    string archivoRpt;
            //    string sqlstr;
            //    string filename;

            //    archivoRpt = "rpt_ordenDePago";

            //    filename = "OP " + ID.ToString();
            //    archivoRpt = "Centrex." + archivoRpt + ".rdlc";

            //    rpt_view.ProcessingMode = ProcessingMode.Local;
            //    rpt_view.LocalReport.DataSources.Clear();


            //    var DataSetFC_empresa = new DataSet("DS_Datos_Empresa");
            //    var DS_Pago_Cabecera = new DataSet("DS_Pago_Cabecera");
            //    var DS_Detalle_Pago_Cheques = new DataSet("DS_Detalle_Pago_Cheques");
            //    var DS_Detalle_Pago_Transferencias = new DataSet("DS_Detalle_Pago_Transferencias");
            //    var DS_Detalle_Pago_FC_Importes = new DataSet("DS_Detalle_Pago_FC_Importes");

            //    var DT_empresa = new DataTable();
            //    var DT_Pago_Cabecera = new DataTable();
            //    var DT_Detalle_Pago_Cheques = new DataTable();
            //    var DT_Detalle_Pago_Transferencias = new DataTable();
            //    var DT_Detalle_Pago_FC_Importes = new DataTable();

            //    var da = new SqlDataAdapter();

            //    try
            //    {
            //        abrirdb(serversql, basedb, usuariodb, passdb);

            //        comando.CommandType = CommandType.Text;
            //        comando.Connection = (SqlConnection)CN;

            //        sqlstr = "EXEC	[dbo].[datos_empresa]";
            //        comando.CommandText = sqlstr;
            //        da.SelectCommand = comando;
            //        da.Fill(DataSetFC_empresa, "Tabla");

            //        sqlstr = "EXEC	[dbo].[SP_pago_cabecera]	@id = " + ID.ToString();
            //        comando.CommandText = sqlstr;
            //        da.SelectCommand = comando;
            //        da.Fill(DS_Pago_Cabecera, "Tabla");

            //        sqlstr = "EXEC [dbo].[SP_detalle_pagos_cheques]	@id = " + ID.ToString();
            //        comando.CommandText = sqlstr;
            //        da.SelectCommand = comando;
            //        da.Fill(DS_Detalle_Pago_Cheques, "Tabla");

            //        sqlstr = "EXEC [dbo].[SP_detalle_pagos_transferencias]	@id = " + ID.ToString();
            //        comando.CommandText = sqlstr;
            //        da.SelectCommand = comando;
            //        da.Fill(DS_Detalle_Pago_Transferencias, "Tabla");

            //        sqlstr = "EXEC [dbo].[SP_detalle_pagos_fc_importes]	@id = " + ID.ToString();
            //        comando.CommandText = sqlstr;
            //        da.SelectCommand = comando;
            //        da.Fill(DS_Detalle_Pago_FC_Importes, "Tabla");

            //        DT_empresa = DataSetFC_empresa.Tables[0];
            //        DT_Pago_Cabecera = DS_Pago_Cabecera.Tables[0];
            //        DT_Detalle_Pago_Cheques = DS_Detalle_Pago_Cheques.Tables[0];
            //        DT_Detalle_Pago_Transferencias = DS_Detalle_Pago_Transferencias.Tables[0];
            //        DT_Detalle_Pago_FC_Importes = DS_Detalle_Pago_FC_Importes.Tables[0];
            //    }

            //    catch (Exception ex)
            //    {
            //        Interaction.MsgBox(ex.Message.ToString());
            //        return;
            //    }
            //    finally
            //    {
            //        cerrardb();
            //    }

            //    {
            //        var withBlock = rpt_view;
            //        withBlock.LocalReport.DataSources.Add(new ReportDataSource("DS_Datos_Empresa", DT_empresa));
            //        withBlock.LocalReport.DataSources.Add(new ReportDataSource("DS_Pago_Cabecera", DT_Pago_Cabecera));
            //        withBlock.LocalReport.DataSources.Add(new ReportDataSource("DS_Detalle_Pago_Cheques", DT_Detalle_Pago_Cheques));
            //        withBlock.LocalReport.DataSources.Add(new ReportDataSource("DS_Detalle_Pago_Transferencias", DT_Detalle_Pago_Transferencias));
            //        withBlock.LocalReport.DataSources.Add(new ReportDataSource("DS_Detalle_Pago_FC_Importes", DT_Detalle_Pago_FC_Importes));
            //    }

            //    {
            //        var withBlock1 = rpt_view;
            //        withBlock1.PrinterSettings.Copies = 2;
            //        withBlock1.LocalReport.ReportEmbeddedResource = archivoRpt;

            //        withBlock1.LocalReport.DisplayName = filename;

            //        withBlock1.PrinterSettings.PrintRange = PrintRange.SomePages;
            //        withBlock1.PrinterSettings.FromPage = 1;
            //        withBlock1.PrinterSettings.ToPage = 1;

            //        withBlock1.RefreshReport();
            //    }
            //}
        }
    }
}
*/
