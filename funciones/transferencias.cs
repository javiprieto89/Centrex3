using System;
using System.Linq;
using System.Xml.Linq;
using Microsoft.VisualBasic;
using Centrex.Models;

namespace Centrex
{

    static class transferencias
    {
        // ************************************ FUNCIONES DE TRANSFERENCIAS (VERSIÓN EF PURA) ***************************

        // --------------------------------------------------------
        // Obtiene una transferencia definitiva por ID
        // --------------------------------------------------------
        public static TransferenciaEntity InfoTransferencia(int idTransferencia)
        {
            using (var ctx = new CentrexDbContext())
            {
                return ctx.Transferencias.Include("CuentaBancaria").FirstOrDefault(t => t.IdTransferencia == idTransferencia);
            }
        }

        // --------------------------------------------------------
        // Obtiene una transferencia temporal por ID
        // --------------------------------------------------------
        public static TmpTransferenciaEntity InfoTmpTransferencia(int idTransferencia)
        {
            using (var ctx = new CentrexDbContext())
            {
                return ctx.TmpTransferencias.Include("CuentaBancaria").FirstOrDefault(t => t.id_tmpTransferencia == idTransferencia);
            }
        }

        // --------------------------------------------------------
        // Agrega una nueva transferencia (definitiva)
        // --------------------------------------------------------
        public static bool AddTransferencia(TransferenciaEntity transferencia)
        {
            try
            {
                using (var ctx = new CentrexDbContext())
                {
                    ctx.Transferencias.Add(transferencia);
                    ctx.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Interaction.MsgBox("Error al agregar transferencia: " + ex.Message);
                return false;
            }
        }

        // --------------------------------------------------------
        // Agrega una transferencia temporal
        // --------------------------------------------------------
        public static bool AddTmpTransferencia(TmpTransferenciaEntity tmp)
        {
            try
            {
                using (var ctx = new CentrexDbContext())
                {
                    ctx.TmpTransferencias.Add(tmp);
                    ctx.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Interaction.MsgBox("Error al agregar transferencia temporal: " + ex.Message);
                return false;
            }
        }

        // --------------------------------------------------------
        // Guarda todas las transferencias temporales como definitivas (Cobro)
        // --------------------------------------------------------
        public static bool GuardarTransferenciasCobro(int idCobro)
        {
            try
            {
                using (var ctx = new CentrexDbContext())
                {
                    var tmpList = ctx.TmpTransferencias.ToList();

                    foreach (var t in tmpList)
                        ctx.Transferencias.Add(new TransferenciaEntity()
                        {
                            IdCobro = idCobro,
                            IdCuentaBancaria = t.id_cuentaBancaria,
                            fecha = t.fecha,
                            total = t.total,
                            nComprobante = t.nComprobante,
                            notas = t.notas
                        });

                    ctx.TmpTransferencias.RemoveRange(tmpList);
                    ctx.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Interaction.MsgBox("Error al guardar transferencias (Cobro): " + ex.Message);
                return false;
            }
        }

        // --------------------------------------------------------
        // Guarda todas las transferencias temporales como definitivas (Pago)
        // --------------------------------------------------------
        public static bool GuardarTransferenciasPago(int idPago)
        {
            try
            {
                using (var ctx = new CentrexDbContext())
                {
                    var tmpList = ctx.TmpTransferencias.ToList();

                    foreach (var t in tmpList)
                        ctx.Transferencias.Add(new TransferenciaEntity()
                        {
                            IdPago = idPago,
                            IdCuentaBancaria = t.id_cuentaBancaria,
                            fecha = t.fecha,
                            total = t.total,
                            nComprobante = t.nComprobante,
                            notas = t.notas
                        });

                    ctx.TmpTransferencias.RemoveRange(tmpList);
                    ctx.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Interaction.MsgBox("Error al guardar transferencias (Pago): " + ex.Message);
                return false;
            }
        }

        // --------------------------------------------------------
        // Actualiza una transferencia temporal
        // --------------------------------------------------------
        public static bool UpdateTmpTransferencia(TmpTransferenciaEntity tmp)
        {
            try
            {
                using (var ctx = new CentrexDbContext())
                {
                    var entidad = ctx.TmpTransferencias.FirstOrDefault(t => t.id_tmpTransferencia == tmp.id_tmpTransferencia);
                    if (entidad is null)
                    {
                        Interaction.MsgBox("No se encontró la transferencia temporal a actualizar.", Constants.vbExclamation);
                        return false;
                    }

                    entidad.id_cuentaBancaria = tmp.id_cuentaBancaria;
                    entidad.fecha = tmp.fecha;
                    entidad.total = tmp.total;
                    entidad.nComprobante = tmp.nComprobante;
                    entidad.notas = tmp.notas;

                    ctx.Entry(entidad).State = EntityState.Modified;
                    ctx.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Interaction.MsgBox("Error al actualizar transferencia temporal: " + ex.Message);
                return false;
            }
        }

        // --------------------------------------------------------
        // Elimina una transferencia temporal por entidad
        // --------------------------------------------------------
        public static bool BorrarTmpTransferencia(TmpTransferenciaEntity tmp)
        {
            try
            {
                using (var ctx = new CentrexDbContext())
                {
                    var entidad = ctx.TmpTransferencias.FirstOrDefault(t => t.id_tmpTransferencia == tmp.id_tmpTransferencia);
                    if (entidad is null)
                    {
                        Interaction.MsgBox("No se encontró la transferencia temporal a eliminar.", Constants.vbExclamation);
                        return false;
                    }
                    ctx.TmpTransferencias.Remove(entidad);
                    ctx.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Interaction.MsgBox("Error al borrar transferencia temporal: " + ex.Message);
                return false;
            }
        }

        // --------------------------------------------------------
        // Elimina una transferencia temporal por ID
        // --------------------------------------------------------
        public static bool BorrarTmpTransferencia(int idTransferencia)
        {
            try
            {
                using (var ctx = new CentrexDbContext())
                {
                    var entidad = ctx.TmpTransferencias.FirstOrDefault(t => t.id_tmpTransferencia == idTransferencia);
                    if (entidad is null)
                    {
                        Interaction.MsgBox("No se encontró la transferencia temporal a eliminar.", Constants.vbExclamation);
                        return false;
                    }
                    ctx.TmpTransferencias.Remove(entidad);
                    ctx.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Interaction.MsgBox("Error al borrar transferencia temporal por ID: " + ex.Message);
                return false;
            }
        }

        // --------------------------------------------------------
        // Elimina una transferencia definitiva
        // --------------------------------------------------------
        public static bool BorrarTransferencia(TransferenciaEntity transferencia)
        {
            try
            {
                using (var ctx = new CentrexDbContext())
                {
                    var entidad = ctx.Transferencias.FirstOrDefault(t => t.IdTransferencia == transferencia.IdTransferencia);
                    if (entidad is null)
                    {
                        Interaction.MsgBox("No se encontró la transferencia a eliminar.", Constants.vbExclamation);
                        return false;
                    }
                    ctx.Transferencias.Remove(entidad);
                    ctx.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Interaction.MsgBox("Error al borrar transferencia: " + ex.Message);
                return false;
            }
        }

        // --------------------------------------------------------
        // Elimina todas las transferencias temporales (utilidad)
        // --------------------------------------------------------
        public static bool BorrarTmpTransferenciasTodas()
        {
            try
            {
                using (var ctx = new CentrexDbContext())
                {
                    var todas = ctx.TmpTransferencias.ToList();
                    ctx.TmpTransferencias.RemoveRange(todas);
                    ctx.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Interaction.MsgBox("Error al limpiar tabla temporal de transferencias: " + ex.Message);
                return false;
            }
        }

        // ************************************ FIN DE FUNCIONES DE TRANSFERENCIAS ***************************
    }
}
