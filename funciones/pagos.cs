using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Centrex.Models;

namespace Centrex
{

    static class pagos
    {
        // ************************************ FUNCIONES DE PAGOS ***************************
        public static PagoEntity info_pago(string id_pago)
        {
            try
            {
                using (var context = new CentrexDbContext())
                {
                    return context.Pagos.FirstOrDefault(p => p.IdPago == Conversions.ToInteger(id_pago));
                }
            }
            catch (Exception ex)
            {
                Interaction.MsgBox(ex.Message.ToString());
                return null;
            }
        }

        public static int addpago(pago p)
        {
            try
            {
                using (var context = new CentrexDbContext())
                {
                    var pagoEntity = new PagoEntity()
                    {
                        FechaCarga = DateTime.Now,
                        FechaPago = !string.IsNullOrEmpty(p.fecha_pago) ? Conversions.ToDate(p.fecha_pago) : DateTime.Now,
                        IdProveedor = p.IdProveedor,
                        IdCc = p.id_cc,
                        dineroEnCc = (decimal)p.dineroEnCc,
                        efectivo = (decimal)p.efectivo,
                        totalTransferencia = (decimal)p.totalTransferencia,
                        totalCh = (decimal)p.totalch,
                        total = (decimal)p.total,
                        hayCheque = p.hayCheque,
                        hayTransferencia = p.hayTransferencia,
                        activo = true,
                        IdAnulaPago = p.id_anulaPago != -1 ? p.id_anulaPago : default,
                        notas = p.notas ?? ""
                    };

                    context.Pagos.Add(pagoEntity);
                    context.SaveChanges();

                    return pagoEntity.IdPago;
                }
            }
            catch (Exception ex)
            {
                Interaction.MsgBox(ex.Message);
                return -1;
            }
        }

        public static bool Anula_Pago(pago p)
        {
            try
            {
                var ap = new pago();
                var cc = new ccProveedor();

                ap = p;
                ap.dineroEnCc = ap.dineroEnCc * -1;
                ap.efectivo = ap.efectivo * -1;
                ap.totalTransferencia = ap.totalTransferencia * -1;
                ap.totalch = ap.totalch * -1;
                ap.total = ap.total * -1;
                ap.id_anulaPago = p.id_pago;
                ap.notas += Constants.vbCr + "ANULA PAGO: " + ap.id_pago.ToString();

                // Agrego un pago exactamente al revez, referenciando al pago original
                ap.id_pago = addpago(ap);

                if (ap.id_pago > 0)
                {
                    // Borro los cheques asignados al pago para liberarlos y actualizo el saldo
                    borrar_chequePagado(ap.id_pago);
                    cc = ccProveedores.info_ccProveedor(p.id_cc);
                    cc.saldo -= p.total;
                    ccProveedores.updateCCProveedor(cc);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Interaction.MsgBox(ex.Message);
                return false;
            }
        }

        public static bool add_chequePagado(int id_pago, int[] id_cheque)
        {
            try
            {
                using (var context = new CentrexDbContext())
                {
                    foreach (int chequeId in id_cheque)
                    {
                        var pagoCheque = new PagoChequeEntity()
                        {
                            IdPago = id_pago,
                            IdCheque = chequeId
                        };
                        context.PagosCheques.Add(pagoCheque);
                    }

                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Interaction.MsgBox(ex.Message);
                return false;
            }
        }

        public static bool borrar_chequePagado(int id_pago)
        {
            try
            {
                using (var context = new CentrexDbContext())
                {
                    var chequesPagados = context.PagosCheques.Where(pc => pc.IdPago == id_pago).ToList();

                    foreach (var pagoCheque in chequesPagados)
                        context.PagosCheques.Remove((PagoChequeEntity)pagoCheque);

                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Interaction.MsgBox(ex.Message.ToString());
                return false;
            }
        }

        public static bool guardar_pagos_facturas_importes(int id_pago, DataGridView dg)
        {
            try
            {
                using (var context = new CentrexDbContext())
                {
                    if (dg.Rows.Count > 0)
                    {
                        foreach (DataGridViewRow row in dg.Rows)
                        {
                            if (row is not null & !row.IsNewRow)
                            {
                                var facturaImporte = new PagoNFacturaImporteEntity()
                                {
                                    IdPago = id_pago,
                                    fecha = Conversions.ToDate(row.Cells["Fecha"].Value),
                                    nfactura = row.Cells["Factura"].Value.ToString(),
                                    importe = Conversions.ToDecimal(row.Cells["Importe"].Value)
                                };
                                context.PagosNFacturasImportes.Add(facturaImporte);
                            }
                        }

                        context.SaveChanges();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Interaction.MsgBox(ex.Message);
                return false;
            }
        }

        public static bool Pago_Ya_Anulado(string id_pago)
        {
            try
            {
                using (var context = new CentrexDbContext())
                {
                    int idPagoInt = Conversions.ToInteger(id_pago);
                    bool existe = context.Pagos.Any(p => p.IdAnulaPago.HasValue && p.IdAnulaPago.Value == idPagoInt);
                    return existe;
                }
            }
            catch (Exception ex)
            {
                Interaction.MsgBox(ex.Message.ToString());
                return false;
            }
        }
        // ************************************ FUNCIONES DE PAGOS ***************************
    }
}
