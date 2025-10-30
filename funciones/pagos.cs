using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Centrex.Models;

namespace Centrex.Funciones
{

    static class pagos
    {
        // ************************************ FUNCIONES DE PAGOS ***************************
        public static PagoEntity info_pago(string IdPago)
        {
            try
            {
                using (var context = new CentrexDbContext())
                {
                    return context.PagoEntity.FirstOrDefault(p => p.IdPago == Conversions.ToInteger(IdPago));
                }
            }
            catch (Exception ex)
            {
                Interaction.MsgBox(ex.Message.ToString());
                return null;
            }
        }

        public static int addpago(PagoEntity p)
        {
            try
            {
                using (var context = new CentrexDbContext())
                {
                    var pagoEntity = new PagoEntity()
                    {
                        FechaCarga = DateOnly.FromDateTime(DateTime.Now),
                        FechaPago = p.FechaPago,
                        IdProveedor = p.IdProveedor,
                        IdCc = p.IdCc,
                        DineroEnCc = (decimal)p.DineroEnCc,
                        Efectivo = (decimal)p.Efectivo,
                        TotalTransferencia = (decimal)p.TotalTransferencia,
                        TotalCh = (decimal)p.TotalCh,
                        Total = (decimal)p.Total,
                        HayCheque = p.HayCheque,
                        HayTransferencia = p.HayTransferencia,
                        Activo = true,
                        IdAnulaPago = p.IdAnulaPago != -1 ? p.IdAnulaPago : default,
                        Notas = p.Notas ?? ""
                    };

                    context.PagoEntity.Add(pagoEntity);
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

        public static bool Anula_Pago(PagoEntity p)
        {
            try
            {
                var ap = new PagoEntity();
                var cc = new CcProveedorEntity();

                ap = p;
                ap.DineroEnCc = ap.DineroEnCc * -1;
                ap.Efectivo = ap.Efectivo * -1;
                ap.TotalTransferencia = ap.TotalTransferencia * -1;
                ap.TotalCh = ap.TotalCh * -1;
                ap.Total = ap.Total * -1;
                ap.IdAnulaPago = p.IdPago;
                ap.Notas += Constants.vbCr + "ANULA PAGO: " + ap.IdPago.ToString();

                // Agrego un pago exactamente al revez, referenciando al pago original
                ap.IdPago = addpago(ap);

                if (ap.IdPago > 0)
                {
                    // Borro los cheques asignados al pago para liberarlos y actualizo el Saldo
                    borrar_chequePagado(ap.IdPago);
                    cc = ccProveedores.info_ccProveedor(p.IdCc);
                    cc.Saldo -= p.Total;
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

        public static bool add_chequePagado(int IdPago, int[] id_cheque)
        {
            try
            {
                using (var context = new CentrexDbContext())
                {
                    foreach (int chequeId in id_cheque)
                    {
                        var pagoCheque = new PagoChequeEntity()
                        {
                            IdPago = IdPago,
                            IdCheque = chequeId
                        };
                        context.PagoChequeEntity.Add(pagoCheque);
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

        public static bool borrar_chequePagado(int IdPago)
        {
            try
            {
                using (var context = new CentrexDbContext())
                {
                    var chequesPagados = context.PagoChequeEntity.Where(pc => pc.IdPago == IdPago).ToList();

                    foreach (var pagoCheque in chequesPagados)
                        context.PagoChequeEntity.Remove((PagoChequeEntity)pagoCheque);

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

        public static bool guardar_pagos_facturas_importes(int IdPago, DataGridView dg)
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
                                var facturaImporte = new PagoNfacturaImporteEntity()
                                {
                                    IdPago = IdPago,
                                    Fecha = row.Cells["Fecha"].Value is DateTime fecha
                                        ? DateOnly.FromDateTime(fecha)
                                        : DateOnly.FromDateTime(DateTime.Now),
                                    Nfactura = row.Cells["Factura"].Value.ToString(),
                                    Importe = Conversions.ToDecimal(row.Cells["Importe"].Value)
                                };
                                context.PagoNfacturaImporteEntity.Add(facturaImporte);
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

        public static bool Pago_Ya_Anulado(string IdPago)
        {
            try
            {
                using (var context = new CentrexDbContext())
                {
                    int idPagoInt = Conversions.ToInteger(IdPago);
                    bool existe = context.PagoEntity.Any(p => p.IdAnulaPago.HasValue && p.IdAnulaPago.Value == idPagoInt);
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
