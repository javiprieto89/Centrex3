using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.Reporting.WinForms;
using Microsoft.VisualBasic;

namespace Centrex
{

    public partial class frm_prnReciboCobro
    {
        private int ID;

        public frm_prnReciboCobro(int _id = 0)
        {
            InitializeComponent();
            ID = _id;
        }

        private void frm_prnReciboCobro_Load(object sender, EventArgs e)
        {
            if (ID == 0)
            {
                Interaction.MsgBox("No se especificó un ID de cobro válido.", Constants.vbCritical);
                return;
            }

            try
            {
                using (var context = new CentrexDbContext())
                {
                    var cobro = context.Cobros.Include(c => c.Cliente).Include(c => c.Cheques).Include(c => c.Transferencias).Include(c => c.Retenciones).Include(c => c.Facturas).FirstOrDefault(c => c.IdCobro == ID);






                    if (cobro is null)
                    {
                        Interaction.MsgBox("No se encontró el cobro.", Constants.vbCritical);
                        return;
                    }

                    var empresa = context.Empresas.FirstOrDefault();

                    string reporte = cobro.EsNoOficial ? "Centrex.rpt_reciboCobro_noOficial.rdlc" : "Centrex.rpt_reciboCobro.rdlc";
                    rpt_view.ProcessingMode = ProcessingMode.Local;
                    rpt_view.LocalReport.ReportEmbeddedResource = reporte;
                    rpt_view.LocalReport.DataSources.Clear();

                    rpt_view.LocalReport.DataSources.Add(new ReportDataSource("DataSetFC_empresa", new List<EmpresaEntity>() { empresa }));
                    rpt_view.LocalReport.DataSources.Add(new ReportDataSource("DS_Cobro_Cabecera", new List<CobroEntity>() { cobro }));
                    rpt_view.LocalReport.DataSources.Add(new ReportDataSource("DS_Detalle_Cobro_Cheques", cobro.Cheques.ToList()));
                    rpt_view.LocalReport.DataSources.Add(new ReportDataSource("DS_Detalle_Cobro_Transferencias", cobro.Transferencias.ToList()));
                    rpt_view.LocalReport.DataSources.Add(new ReportDataSource("DS_Detalle_Cobro_Retenciones", cobro.Retenciones.ToList()));
                    rpt_view.LocalReport.DataSources.Add(new ReportDataSource("DS_Detalle_Cobro_FC_Importes", cobro.Facturas.ToList()));

                    rpt_view.LocalReport.DisplayName = $"RC {ID}";
                    rpt_view.PrinterSettings.Copies = 2;
                    rpt_view.RefreshReport();
                }
            }

            catch (Exception ex)
            {
                Interaction.MsgBox("Error al generar el recibo de cobro: " + ex.Message, Constants.vbCritical);
            }
        }
    }
}
