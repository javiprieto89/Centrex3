using System;
using System.Linq;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Centrex.Models;

namespace Centrex.Funciones
{

    static class transacciones
    {

        // ===========================================
        // FUNCIÓN: InfoTransaccion
        // ===========================================
        public static TransaccionEntity InfoTransaccion(string id_transaccion = "")
        {
            var tmp = new TransaccionEntity();
            try
            {
                using (var context = new CentrexDbContext())
                {
                    int id = 0;
                    int.TryParse(id_transaccion, out id);

                    var transaccionEntity = context.TransaccionEntity.FirstOrDefault(t => t.IdTransaccion == id);

                    if (transaccionEntity is not null)
                    {
                        tmp.IdTransaccion = transaccionEntity.IdTransaccion;
                        tmp.Fecha = transaccionEntity.Fecha;
                        tmp.IdPedido = Conversions.ToInteger(transaccionEntity.IdPedido.HasValue ? transaccionEntity.IdPedido.Value.ToString() : "-1");
                        tmp.IdComprobanteCompra = Conversions.ToInteger(transaccionEntity.IdComprobanteCompra.HasValue ? transaccionEntity.IdComprobanteCompra.Value.ToString() : "-1");
                        tmp.IdCobro = Conversions.ToInteger(transaccionEntity.IdCobro.HasValue ? transaccionEntity.IdCobro.Value.ToString() : "-1");
                        tmp.IdPago = Conversions.ToInteger(transaccionEntity.IdPago.HasValue ? transaccionEntity.IdPago.Value.ToString() : "-1");
                        tmp.IdTipoComprobante = Conversions.ToInteger(transaccionEntity.IdTipoComprobante.HasValue ? transaccionEntity.IdTipoComprobante.Value.ToString() : "-1");
                        tmp.NumeroComprobante = transaccionEntity.NumeroComprobante;
                        tmp.PuntoVenta = Conversions.ToInteger(transaccionEntity.PuntoVenta.HasValue ? transaccionEntity.PuntoVenta.Value.ToString() : "");
                        tmp.Total = transaccionEntity.Total;
                        tmp.IdCc = Conversions.ToInteger(transaccionEntity.IdCc.HasValue ? transaccionEntity.IdCc.Value.ToString() : "-1");
                        tmp.IdCliente = Conversions.ToInteger(transaccionEntity.IdCliente.HasValue ? transaccionEntity.IdCliente.Value.ToString() : "-1");
                        tmp.IdProveedor = Conversions.ToInteger(transaccionEntity.IdProveedor.HasValue ? transaccionEntity.IdProveedor.Value.ToString() : "-1");
                    }
                }

                return tmp;
            }
            catch (Exception ex)
            {
                Interaction.MsgBox("Error en InfoTransaccion: " + ex.Message);
                return tmp;
            }
        }

        // ===========================================
        // FUNCIÓN: Agregar_Transaccion_Desde_Pedido
        // ===========================================
        public static bool Agregar_Transaccion_Desde_Pedido(TransaccionEntity t)
        {
            try
            {
                using (var context = new CentrexDbContext())
                {
                    var nuevaTransaccion = new TransaccionEntity()
                    {
                        Fecha = t.Fecha,
                        IdPedido = t.IdPedido,
                        IdTipoComprobante = t.IdTipoComprobante,
                        NumeroComprobante = t.NumeroComprobante,
                        PuntoVenta = t.PuntoVenta,
                        Total = t.Total,
                        IdCc = t.IdCc,
                        IdCliente = t.IdCliente,
                        IdCobro = t.IdCobro,
                        IdPago = t.IdPago
                    };

                    context.TransaccionEntity.Add(nuevaTransaccion);
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Interaction.MsgBox("Error al agregar transacción desde pedido: " + ex.Message);
                return false;
            }
        }

        // ===========================================
        // FUNCIÓN: Agregar_Transaccion_Desde_Comprobante_Compra
        // ===========================================
        public static bool Agregar_Transaccion_Desde_Comprobante_Compra(TransaccionEntity t)
        {
            try
            {
                using (var context = new CentrexDbContext())
                {
                    var nuevaTransaccion = new TransaccionEntity()
                    {
                        Fecha = t.Fecha,
                        IdComprobanteCompra = t.IdComprobanteCompra,
                        IdTipoComprobante = t.IdTipoComprobante,
                        NumeroComprobante = t.NumeroComprobante,
                        PuntoVenta = t.PuntoVenta,
                        Total = t.Total,
                        IdCc = t.IdCc,
                        IdProveedor = t.IdProveedor,
                        IdCobro = t.IdCobro,
                        IdPago = t.IdPago
                    };

                    context.TransaccionEntity.Add(nuevaTransaccion);
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Interaction.MsgBox("Error al agregar transacción desde comprobante de compra: " + ex.Message);
                return false;
            }
        }

    }
}
