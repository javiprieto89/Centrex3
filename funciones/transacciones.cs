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

                    var transaccionEntity = context.Transacciones.FirstOrDefault(t => t.IdTransaccion == id);

                    if (transaccionEntity is not null)
                    {
                        tmp.id_transaccion = transaccionEntity.IdTransaccion.ToString();
                        tmp.fecha = Conversions.ToString(transaccionEntity.fecha.ToString()[Conversions.ToInteger("dd/MM/yyyy")]);
                        tmp.id_pedido = Conversions.ToInteger(transaccionEntity.IdPedido.HasValue ? transaccionEntity.IdPedido.Value.ToString() : "-1");
                        tmp.id_comprobanteCompra = Conversions.ToInteger(transaccionEntity.IdComprobanteCompra.HasValue ? transaccionEntity.IdComprobanteCompra.Value.ToString() : "-1");
                        tmp.id_cobro = Conversions.ToInteger(transaccionEntity.IdCobro.HasValue ? transaccionEntity.IdCobro.Value.ToString() : "-1");
                        tmp.id_pago = Conversions.ToInteger(transaccionEntity.IdPago.HasValue ? transaccionEntity.IdPago.Value.ToString() : "-1");
                        tmp.id_tipoComprobante = Conversions.ToInteger(transaccionEntity.IdTipoComprobante.HasValue ? transaccionEntity.IdTipoComprobante.Value.ToString() : "-1");
                        tmp.numeroComprobante = transaccionEntity.numeroComprobante.HasValue ? transaccionEntity.numeroComprobante.Value.ToString() : "";
                        tmp.puntoVenta = Conversions.ToInteger(transaccionEntity.puntoVenta.HasValue ? transaccionEntity.puntoVenta.Value.ToString() : "");
                        tmp.total = Conversions.ToDouble(transaccionEntity.total.HasValue ? transaccionEntity.total.Value.ToString("0.000") : "0.000");
                        tmp.id_cc = Conversions.ToInteger(transaccionEntity.IdCc.HasValue ? transaccionEntity.IdCc.Value.ToString() : "-1");
                        tmp.id_cliente = Conversions.ToInteger(transaccionEntity.IdCliente.HasValue ? transaccionEntity.IdCliente.Value.ToString() : "-1");
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
                        fecha = DateTime.ParseExact(t.fecha, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture),
                        IdPedido = string.IsNullOrEmpty(t.id_pedido.ToString()) ? default : t.id_pedido,
                        IdTipoComprobante = string.IsNullOrEmpty(t.id_tipoComprobante.ToString()) ? default : t.id_tipoComprobante,
                        numeroComprobante = string.IsNullOrEmpty(t.numeroComprobante) ? default : Conversions.ToInteger(t.numeroComprobante),
                        puntoVenta = string.IsNullOrEmpty(t.puntoVenta.ToString()) ? default : t.puntoVenta,
                        total = string.IsNullOrEmpty(t.total.ToString()) ? 0m : (decimal)t.total,
                        IdCc = string.IsNullOrEmpty(t.id_cc.ToString()) ? default : t.id_cc,
                        IdCliente = string.IsNullOrEmpty(t.id_cliente.ToString()) ? default : t.id_cliente,
                        IdCobro = string.IsNullOrEmpty(t.id_cobro.ToString()) ? default : t.id_cobro,
                        IdPago = string.IsNullOrEmpty(t.id_pago.ToString()) ? default : t.id_pago
                    };

                    context.Transacciones.Add(nuevaTransaccion);
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
                        fecha = DateTime.ParseExact(t.fecha, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture),
                        IdComprobanteCompra = string.IsNullOrEmpty(t.id_comprobanteCompra.ToString()) ? default : t.id_comprobanteCompra,
                        IdTipoComprobante = string.IsNullOrEmpty(t.id_tipoComprobante.ToString()) ? default : t.id_tipoComprobante,
                        numeroComprobante = string.IsNullOrEmpty(t.numeroComprobante) ? default : Conversions.ToInteger(t.numeroComprobante),
                        puntoVenta = string.IsNullOrEmpty(t.puntoVenta.ToString()) ? default : t.puntoVenta,
                        total = string.IsNullOrEmpty(t.total.ToString()) ? 0m : (decimal)t.total,
                        IdCc = string.IsNullOrEmpty(t.id_cc.ToString()) ? default : t.id_cc,
                        IdProveedor = string.IsNullOrEmpty(t.IdProveedor.ToString()) ? default : t.IdProveedor,
                        IdCobro = string.IsNullOrEmpty(t.id_cobro.ToString()) ? default : t.id_cobro,
                        IdPago = string.IsNullOrEmpty(t.id_pago.ToString()) ? default : t.id_pago
                    };

                    context.Transacciones.Add(nuevaTransaccion);
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
