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
                return ctx.TransferenciaEntity.Include(t => t.IdCuentaBancariaNavigation).FirstOrDefault(t => t.IdTransferencia == idTransferencia);
            }
        }

        // --------------------------------------------------------
        // Obtiene una transferencia temporal por ID
        // --------------------------------------------------------
        public static TmpTransferenciaEntity InfoTmpTransferencia(int idTransferencia)
        {
            using (var ctx = new CentrexDbContext())
            {
                return ctx.TmpTransferenciaEntity.Include(t => t.IdCuentaBancariaNavigation).FirstOrDefault(t => t.IdTmpTransferencia == idTransferencia);
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
                    ctx.TransferenciaEntity.Add(transferencia);
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
                    ctx.TmpTransferenciaEntity.Add(tmp);
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
        public static bool GuardarTransferenciaEntityCobro(int idCobro)
        {
            try
            {
                using (var ctx = new CentrexDbContext())
                {
                    var tmpList = ctx.TmpTransferenciaEntity.ToList();

                    foreach (var t in tmpList)
                        ctx.TransferenciaEntity.Add(new TransferenciaEntity()
                        {
                            IdCobro = idCobro,
                            IdCuentaBancaria = t.IdCuentaBancaria,
                            Fecha = t.Fecha,
                            Total = t.Total,
                            NComprobante = t.NComprobante,
                            Notas = t.Notas
                        });

                    ctx.TmpTransferenciaEntity.RemoveRange(tmpList);
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
        public static bool GuardarTransferenciaEntityPago(int idPago)
        {
            try
            {
                using (var ctx = new CentrexDbContext())
                {
                    var tmpList = ctx.TmpTransferenciaEntity.ToList();

                    foreach (var t in tmpList)
 ctx.TransferenciaEntity.Add(new TransferenciaEntity()
      {
     IdPago = idPago,
 IdCuentaBancaria = t.IdCuentaBancaria,
    Fecha = t.Fecha,
          Total = t.Total,
  NComprobante = t.NComprobante,
  Notas = t.Notas
     });

                    ctx.TmpTransferenciaEntity.RemoveRange(tmpList);
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
                    var entidad = ctx.TmpTransferenciaEntity.FirstOrDefault(t => t.IdTmpTransferencia == tmp.IdTmpTransferencia);
                    if (entidad is null)
                    {
                        Interaction.MsgBox("No se encontró la transferencia temporal a actualizar.", Constants.vbExclamation);
                        return false;
                    }

                    entidad.IdCuentaBancaria = tmp.IdCuentaBancaria;
                    entidad.Fecha = tmp.Fecha;
                    entidad.Total = tmp.Total;
                    entidad.NComprobante = tmp.NComprobante;
                    entidad.Notas = tmp.Notas;

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
                    var entidad = ctx.TmpTransferenciaEntity.FirstOrDefault(t => t.IdTmpTransferencia == tmp.IdTmpTransferencia);
                    if (entidad is null)
                    {
                        Interaction.MsgBox("No se encontró la transferencia temporal a eliminar.", Constants.vbExclamation);
                        return false;
                    }
                    ctx.TmpTransferenciaEntity.Remove(entidad);
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
                    var entidad = ctx.TmpTransferenciaEntity.FirstOrDefault(t => t.IdTmpTransferencia == idTransferencia);
                    if (entidad is null)
                    {
                        Interaction.MsgBox("No se encontró la transferencia temporal a eliminar.", Constants.vbExclamation);
                        return false;
                    }
                    ctx.TmpTransferenciaEntity.Remove(entidad);
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
                    var entidad = ctx.TransferenciaEntity.FirstOrDefault(t => t.IdTransferencia == transferencia.IdTransferencia);
                    if (entidad is null)
                    {
                        Interaction.MsgBox("No se encontró la transferencia a eliminar.", Constants.vbExclamation);
                        return false;
                    }
                    ctx.TransferenciaEntity.Remove(entidad);
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
        public static bool BorrarTmpTransferenciaEntityTodas()
        {
            try
            {
                using (var ctx = new CentrexDbContext())
                {
                    var todas = ctx.TmpTransferenciaEntity.ToList();
                    ctx.TmpTransferenciaEntity.RemoveRange(todas);
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
