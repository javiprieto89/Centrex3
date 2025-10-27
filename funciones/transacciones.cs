using System;
using System.Linq;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Centrex.Models;

namespace Centrex
{

    static class transacciones
    {

        // ===========================================
        // FUNCIÓN: InfoTransaccion
        // ===========================================
        public static transaccion InfoTransaccion(string id_transaccion = "")
        {
            var tmp = new transaccion();
            try
            {
                using (var context = new CentrexDbContext())
                {
                    int id = 0;
                    int.TryParse(id_transaccion, out id);

                    var transaccionEntity = context.TransaccionEntity.FirstOrDefault(t => t.IdTransaccion == id);

                    if (transaccionEntity is not null)
                    {
                        tmp.IdTransaccion = transaccionEntity.IdTransaccion.ToString();
                        tmp.Fecha = Conversions.ToString(transaccionEntity.Fecha.ToString("dd/MM/yyyy"));
                        tmp.IdPedido = Conversions.ToInteger(transaccionEntity.IdPedido.HasValue ? transaccionEntity.IdPedido.Value.ToString() : "-1");
                        tmp.IdComprobanteCompra = Conversions.ToInteger(transaccionEntity.IdComprobanteCompra.HasValue ? transaccionEntity.IdComprobanteCompra.Value.ToString() : "-1");
                        tmp.IdCobro = Conversions.ToInteger(transaccionEntity.IdCobro.HasValue ? transaccionEntity.IdCobro.Value.ToString() : "-1");
                        tmp.IdPago = Conversions.ToInteger(transaccionEntity.IdPago.HasValue ? transaccionEntity.IdPago.Value.ToString() : "-1");
                        tmp.IdTipoComprobante = Conversions.ToInteger(transaccionEntity.IdTipoComprobante.HasValue ? transaccionEntity.IdTipoComprobante.Value.ToString() : "-1");
                        tmp.NumeroComprobante = transaccionEntity.NumeroComprobante.HasValue ? transaccionEntity.NumeroComprobante.Value.ToString() : "";
                        tmp.PuntoVenta = Conversions.ToInteger(transaccionEntity.PuntoVenta.HasValue ? transaccionEntity.PuntoVenta.Value.ToString() : "");
                        tmp.Total = Conversions.ToDouble(transaccionEntity.Total.HasValue ? transaccionEntity.Total.Value.ToString("0.000") : "0.000");
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
        public static bool Agregar_Transaccion_Desde_Pedido(transaccion t)
        {
            try
            {
                using (var context = new CentrexDbContext())
                {
                    var nuevaTransaccion = new TransaccionEntity()
                    {
                        Fecha = DateOnly.FromDateTime(DateTime.ParseExact(t.Fecha, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture)),
                        IdPedido = string.IsNullOrEmpty(t.IdPedido.ToString()) ? default : t.IdPedido,
                        IdTipoComprobante = string.IsNullOrEmpty(t.IdTipoComprobante.ToString()) ? default : t.IdTipoComprobante,
                        NumeroComprobante = string.IsNullOrEmpty(t.NumeroComprobante) ? default : Conversions.ToInteger(t.NumeroComprobante),
                        PuntoVenta = string.IsNullOrEmpty(t.PuntoVenta.ToString()) ? default : t.PuntoVenta,
                        Total = string.IsNullOrEmpty(t.Total.ToString()) ? 0m : (decimal)t.Total,
                        IdCc = string.IsNullOrEmpty(t.IdCc.ToString()) ? default : t.IdCc,
                        IdCliente = string.IsNullOrEmpty(t.IdCliente.ToString()) ? default : t.IdCliente,
                        IdCobro = string.IsNullOrEmpty(t.IdCobro.ToString()) ? default : t.IdCobro,
                        IdPago = string.IsNullOrEmpty(t.IdPago.ToString()) ? default : t.IdPago
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
        public static bool Agregar_Transaccion_Desde_Comprobante_Compra(transaccion t)
        {
            try
            {
                using (var context = new CentrexDbContext())
                {
                    var nuevaTransaccion = new TransaccionEntity()
                    {
                        Fecha = DateOnly.FromDateTime(DateTime.ParseExact(t.fecha, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture)),
                        IdComprobanteCompra = string.IsNullOrEmpty(t.id_comprobanteCompra.ToString()) ? default : t.id_comprobanteCompra,
                        IdTipoComprobante = string.IsNullOrEmpty(t.id_tipoComprobante.ToString()) ? default : t.id_tipoComprobante,
                        NumeroComprobante = string.IsNullOrEmpty(t.numeroComprobante) ? default : Conversions.ToInteger(t.numeroComprobante),
                        PuntoVenta = string.IsNullOrEmpty(t.puntoVenta.ToString()) ? default : t.puntoVenta,
                        Total = string.IsNullOrEmpty(t.total.ToString()) ? 0m : (decimal)t.total,
                        IdCc = string.IsNullOrEmpty(t.id_cc.ToString()) ? default : t.id_cc,
                        IdProveedor = string.IsNullOrEmpty(t.IdProveedor.ToString()) ? default : t.IdProveedor,
                        IdCobro = string.IsNullOrEmpty(t.id_cobro.ToString()) ? default : t.id_cobro,
                        IdPago = string.IsNullOrEmpty(t.id_pago.ToString()) ? default : t.id_pago
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
