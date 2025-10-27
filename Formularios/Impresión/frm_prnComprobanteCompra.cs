/*
 * FORMULARIO DE IMPRESIÓN DE COMPROBANTES DE COMPRA - COMENTADO TEMPORALMENTE
 * Este formulario maneja la impresión de comprobantes de compra usando ReportViewer
 * Se comenta temporalmente durante la migración de VB.NET a C# y EF Core
 * 
 * TODO: Migrar completamente a EF Core y actualizar lógica de impresión
 */

/*
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.VisualBasic;

namespace Centrex
{

    public partial class frm_prnComprobanteCompra
    {
        private int ID;

        public frm_prnComprobanteCompra(int _id = 0)
        {
            InitializeComponent();
            ID = _id;
        }

        private void frm_prnComprobanteCompra_Load(object sender, EventArgs e)
        {
//            if (ID == 0)
//            {
//                Interaction.MsgBox("No se especific� un ID de comprobante de compra v�lido.", Constants.vbCritical);
//                return;
//            }

//            try
//            {
//                using (var context = new CentrexDbContext())
//                {
//                    // === Cargar comprobante con sus relaciones ===
//                    var comprobante = context.ComprobantesCompras.Include(c => c.Proveedor).Include(c => c.Items).Include(c => c.impuestos).Include(c => c.conceptos).FirstOrDefault(c => c.IdComprobanteCompra == ID);





//                    if (comprobante is null)
//                    {
//                        Interaction.MsgBox("No se encontr� el comprobante de compra.", Constants.vbCritical);
//                        return;
//                    }

//                    // === Datos base ===
//                    var empresa = context.Empresas.FirstOrDefault();
//                    var items = comprobante.Items.ToList();
//                    var impuestos = comprobante.impuestos.ToList();
//                    var conceptos = comprobante.conceptos.ToList();

//#if false
//                    string archivoRpt = "Centrex.rpt_comprobanteCompra.rdlc";
//                    string fileName = $"Comprobante compra {ID}";

//                    rpt_view.ProcessingMode = ProcessingMode.Local;
//                    rpt_view.LocalReport.ReportEmbeddedResource = archivoRpt;
//                    rpt_view.LocalReport.DataSources.Clear();

//                    if (empresa is not null)
//                    {
//                        rpt_view.LocalReport.DataSources.Add(new ReportDataSource("DS_Empresa", new List<EmpresaEntity>() { empresa }));
//                    }

//                    rpt_view.LocalReport.DataSources.Add(new ReportDataSource("DS_ComprobanteCompraCabecera", new List<ComprobanteCompraEntity>() { comprobante }));
//                    rpt_view.LocalReport.DataSources.Add(new ReportDataSource("DS_ComprobanteCompraDetalleItems", items));
//                    rpt_view.LocalReport.DataSources.Add(new ReportDataSource("DS_ComprobanteCompraDetalleImpuestos", impuestos));
//                    rpt_view.LocalReport.DataSources.Add(new ReportDataSource("DS_ComprobanteCompraDetalleConceptos", conceptos));

//                    rpt_view.LocalReport.DisplayName = fileName;
//                    rpt_view.PrinterSettings.Copies = 2;
//                    rpt_view.RefreshReport();
//#else
//                    Interaction.MsgBox("La vista de ReportViewer para comprobantes de compra se encuentra deshabilitada temporalmente.", MsgBoxStyle.Information, "Centrex");
//#endif
//                }
//            }

//            catch (Exception ex)
//            {
//                Interaction.MsgBox("Error al generar el comprobante de compra: " + ex.Message, Constants.vbCritical);
//            }
        }
    }
}
*/
