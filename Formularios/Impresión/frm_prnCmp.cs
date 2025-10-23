using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using Microsoft.VisualBasic;

namespace Centrex
{

    public partial class frm_prnCmp
    {
        private bool esPresupuesto = false;
        private bool imprimeRemito = true;
        private bool noMostrarRemito = false;
        private PedidoEntity p;
        private ComprobanteEntity c;
        private ClienteEntity cli;
        private bool esMonotributo;
        private int id_tipoComprobante;
        private int idPedido;

        // ==========================
        // CONSTRUCTORES
        // ==========================
        public frm_prnCmp(bool Presupuesto = false, bool Remito = true, bool _noMostrarRemito = false, int pedidoId = 0)
        {
            InitializeComponent();
            esPresupuesto = Presupuesto;
            imprimeRemito = Remito;
            noMostrarRemito = _noMostrarRemito;
            idPedido = pedidoId;
        }

        // ==========================
        // LOAD
        // ==========================
        private void frm_reportes_Load(object sender, EventArgs e)
        {
            if (idPedido == 0)
            {
                Interaction.MsgBox("No se especificó un ID de pedido válido.", Constants.vbCritical);
                return;
            }

            try
            {
                using (var context = new CentrexDbContext())
                {
                    // Cargar pedido con relaciones
                    p = context.Pedidos.Include(x => x.Comprobante).Include(x => x.Cliente).FirstOrDefault(x => x.IdPedido == idPedido);



                    if (p is null)
                    {
                        Interaction.MsgBox("No se encontró el pedido especificado.", Constants.vbCritical);
                        return;
                    }

                    c = p.Comprobante;
                    cli = p.Cliente;
                    esMonotributo = cli.IdClaseFiscal == 7;
                    id_tipoComprobante = c.IdTipoComprobante;

                    // Configurar ReportViewer
                    ReportViewer1.ProcessingMode = ProcessingMode.Local;
                    ReportViewer1.LocalReport.DataSources.Clear();

                    // Seleccionar reporte según tipo de comprobante
                    string fileName = SeleccionarReporte(id_tipoComprobante);
                    ReportViewer1.LocalReport.DisplayName = fileName;

                    // Empresa (ajustá el DbSet según tu modelo)
                    // Si tu DbContext tiene DbSet(Of EmpresaEntity) usá .Companies
                    var empresa = context.Empresas.FirstOrDefault();

                    // Items del pedido
                    var items = context.PedidosItems.Include(i => i.Item).Where(i => i.IdPedido == p.IdPedido).ToList();



                    // Agregar datasets al reporte
                    if (empresa is not null)
                    {
                        ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSetFC_empresa", new List<EmpresaEntity>() { empresa }));
                    }

                    ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSetFC_cabecera", new List<PedidoEntity>() { p }));

                    ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSetFC_detalle", items)); // items ya es List(Of PedidoItemEntity)

                    ReportViewer1.PrinterSettings.Copies = 3;
                    ReportViewer1.RefreshReport();
                }
            }

            catch (Exception ex)
            {
                Interaction.MsgBox("Error al generar el reporte: " + ex.Message, Constants.vbCritical);
            }
        }

        // ==========================
        // SELECCIÓN DE REPORTE
        // ==========================
        private string SeleccionarReporte(int tipo)
        {
            string fileName = "";

            switch (tipo)
            {
                case 99:
                    {
                        ReportViewer1.LocalReport.ReportEmbeddedResource = "Centrex.rpt_presupuesto.rdlc";
                        fileName = "PS ";
                        break;
                    }
                case 1:
                    {
                        if (esMonotributo)
                        {
                            ReportViewer1.LocalReport.ReportEmbeddedResource = "Centrex.rpt_facturaA_Monotributo.rdlc";
                        }
                        else
                        {
                            ReportViewer1.LocalReport.ReportEmbeddedResource = "Centrex.rpt_facturaA.rdlc";
                        }
                        fileName = "FC A ";
                        break;
                    }
                case 6:
                    {
                        ReportViewer1.LocalReport.ReportEmbeddedResource = "Centrex.rpt_facturaB.rdlc";
                        fileName = "FC B ";
                        break;
                    }
                case 3:
                    {
                        ReportViewer1.LocalReport.ReportEmbeddedResource = "Centrex.rpt_notaDeCreditoA.rdlc";
                        fileName = "NC A ";
                        break;
                    }
                case 8:
                    {
                        ReportViewer1.LocalReport.ReportEmbeddedResource = "Centrex.rpt_notaDeCreditoB.rdlc";
                        fileName = "NC B ";
                        break;
                    }
                case 2:
                    {
                        ReportViewer1.LocalReport.ReportEmbeddedResource = "Centrex.rpt_notaDeDebitoA.rdlc";
                        fileName = "ND A ";
                        break;
                    }
                case 7:
                    {
                        ReportViewer1.LocalReport.ReportEmbeddedResource = "Centrex.rpt_notaDeDebitoB.rdlc";
                        fileName = "ND B ";
                        break;
                    }
                case 199:
                    {
                        ReportViewer1.LocalReport.ReportEmbeddedResource = "Centrex.rpt_saleOrderRM.rdlc";
                        fileName = "RM ";
                        break;
                    }
                case 1011:
                    {
                        ReportViewer1.LocalReport.ReportEmbeddedResource = "Centrex.rpt_proformaA.rdlc";
                        fileName = "PF A ";
                        break;
                    }
                case 1012:
                    {
                        ReportViewer1.LocalReport.ReportEmbeddedResource = "Centrex.rpt_proformaB.rdlc";
                        fileName = "PF B ";
                        break;
                    }
                case 201:
                    {
                        ReportViewer1.LocalReport.ReportEmbeddedResource = "Centrex.rpt_facturaA_MiPyme.rdlc";
                        fileName = "FC A MiPyme ";
                        break;
                    }

                default:
                    {
                        ReportViewer1.LocalReport.ReportEmbeddedResource = "Centrex.rpt_facturaB.rdlc";
                        fileName = "FC B ";
                        break;
                    }
            }

            if (tipo == 99)
            {
                fileName += p.idPresupuesto.ToString();
            }
            else
            {
                fileName += p.numeroComprobante.ToString();
            }

            return fileName;
        }

        // ==========================
        // EVENTO AL CERRAR
        // ==========================
        private void frm_reportes_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (new[] { 199, 99, 1011, 1012 }.Contains(id_tipoComprobante) || noMostrarRemito)
                return;

            if (imprimeRemito)
            {
                e.Cancel = true;
                ReportViewer1.RefreshReport();






            }
        }
    }
}
