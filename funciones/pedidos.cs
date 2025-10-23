using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Centrex.Models;

namespace Centrex
{
    // ===============================================
    // Centrex 3.0 - Módulo EF para gestión de Pedidos
    // Migrado a EF Core - Sin SQL
    // ===============================================
    static class Pedidos
    {
        // ===============================================
        // 🔹 Obtener información de un pedido
        // ===============================================
        public static PedidoEntity InfoPedido(string id_pedido = "", bool esCaso = false)
        {
            var tmp = new PedidoEntity();

            try
            {
                using var ctx = new CentrexDbContext();
                PedidoEntity pedido;

                if (string.IsNullOrEmpty(id_pedido))
                    pedido = ctx.PedidoEntity.OrderByDescending(p => p.IdPedido).FirstOrDefault();
                else
                {
                    int id = Convert.ToInt32(id_pedido);
                    pedido = ctx.PedidoEntity.FirstOrDefault(p => p.IdPedido == id);
                }

                if (pedido is null)
                {
                    Interaction.MsgBox("No se encontró el pedido solicitado.", MsgBoxStyle.Exclamation, "Centrex");
                    return tmp;
                }

                // Clonar propiedades principales
                tmp.IdPedido = pedido.IdPedido;
                tmp.Fecha = pedido.Fecha;
                tmp.FechaEdicion = pedido.FechaEdicion;
                tmp.IdCliente = pedido.IdCliente;
                tmp.Markup = pedido.Markup;
                tmp.Subtotal = pedido.Subtotal;
                tmp.Iva = pedido.Iva;
                tmp.Total = pedido.Total;
                tmp.Nota1 = pedido.Nota1;
                tmp.Nota2 = pedido.Nota2;
                tmp.EsPresupuesto = pedido.EsPresupuesto;
                tmp.Activo = pedido.Activo;
                tmp.Cerrado = pedido.Cerrado;
                tmp.IdPresupuesto = pedido.IdPresupuesto;
                tmp.IdComprobante = pedido.IdComprobante;
                tmp.Cae = pedido.Cae;
                tmp.FechaVencimientoCae = pedido.FechaVencimientoCae;
                tmp.PuntoVenta = pedido.PuntoVenta;
                tmp.CodigoDeBarras = pedido.CodigoDeBarras;
                tmp.EsTest = pedido.EsTest;
                tmp.IdCc = pedido.IdCc;
                tmp.FcQr = pedido.FcQr;
                tmp.NumeroComprobanteAnulado = pedido.NumeroComprobanteAnulado;
                tmp.NumeroPedidoAnulado = pedido.NumeroPedidoAnulado;
                tmp.EsDuplicado = pedido.EsDuplicado;
                tmp.IdUsuario = pedido.IdUsuario;

                return tmp;
            }
            catch (Exception ex)
            {
                Interaction.MsgBox($"Error al obtener el pedido: {ex.Message}", MsgBoxStyle.Critical, "Centrex");
                return tmp;
            }
        }

        // ===============================================
        // 🔹 Obtener información de cliente
        // ===============================================
        public static ClienteEntity InfoCliente(int clienteId)
        {
            using var ctx = new CentrexDbContext();
            return ctx.ClienteEntity
                .Include(c => c.IdProvinciaFiscalNavigation)
                .Include(c => c.IdProvinciaEntregaNavigation)
                .FirstOrDefault(c => c.IdCliente == clienteId);
        }

        // ===============================================
        // 🔹 Obtener información de comprobante
        // ===============================================
        public static ComprobanteEntity InfoComprobante(int comprobanteId)
        {
            using var ctx = new CentrexDbContext();
            return ctx.ComprobanteEntity
                .Include(c => c.IdTipoComprobanteNavigation)
                .FirstOrDefault(c => c.IdComprobante == comprobanteId);
        }

        // ===============================================
        // 🔹 Obtener el próximo número de pedido
        // ===============================================
        public static int GetNextPedidoNumber(int tipoComprobanteId, int? puntoVenta)
        {
            using var ctx = new CentrexDbContext();
            var ultimo = ctx.PedidoEntity
                .Where(p => p.IdComprobante == tipoComprobanteId && p.PuntoVenta == puntoVenta)
                .OrderByDescending(p => p.NumeroComprobante)
                .Select(p => p.NumeroComprobante)
                .FirstOrDefault();

            return ultimo.HasValue ? ultimo.Value + 1 : 1;
        }

        // ===============================================
        // 🔹 Agregar nuevo pedido
        // ===============================================
        public static int AddPedido(PedidoEntity pedido)
        {
            using var ctx = new CentrexDbContext();
            pedido.Fecha = DateOnly.FromDateTime(DateTime.Now);
            pedido.FechaEdicion = DateOnly.FromDateTime(DateTime.Now);
            ctx.PedidoEntity.Add(pedido);
            ctx.SaveChanges();
            return pedido.IdPedido;
        }

        // ===============================================
        // 🔹 Actualizar pedido existente
        // ===============================================
        public static bool UpdatePedido(PedidoEntity pedido)
        {
            using var ctx = new CentrexDbContext();
            var original = ctx.PedidoEntity.Find(pedido.IdPedido);
            if (original is null)
                return false;

            ctx.Entry(original).CurrentValues.SetValues(pedido);
            original.FechaEdicion = DateOnly.FromDateTime(DateTime.Now);
            ctx.SaveChanges();
            return true;
        }

        // ===============================================
        // 🔹 Obtener ítems temporales por usuario
        // ===============================================
        public static List<TmpPedidoItemEntity> ObtenerTmpItemsPorUsuario(int usuarioId)
        {
            using var ctx = new CentrexDbContext();
            return ctx.TmpPedidoItemEntity
                .Include(t => t.IdPedidoItemNavigation)
                .Where(t => t.IdUsuario == usuarioId && t.Activo == true)
                .ToList();
        }

        // ===============================================
        // 🔹 Eliminar ítems temporales del usuario
        // ===============================================
        public static void EliminarTmpItemsPorUsuario(int usuarioId)
        {
            using var ctx = new CentrexDbContext();
            var items = ctx.TmpPedidoItemEntity.Where(t => t.IdUsuario == usuarioId).ToList();
            if (items.Any())
            {
                ctx.TmpPedidoItemEntity.RemoveRange(items);
                ctx.SaveChanges();
            }
        }

        // ===============================================
        // 🔹 Guardar pedido definitivo
        // ===============================================
        public static bool GuardarPedido(PedidoEntity pedido, int tmpUsuarioId)
        {
            using var ctx = new CentrexDbContext();
            using var trans = ctx.Database.BeginTransaction();

            try
            {
                pedido.Fecha = DateOnly.FromDateTime(DateTime.Now);
                pedido.FechaEdicion = DateOnly.FromDateTime(DateTime.Now);
                ctx.PedidoEntity.Add(pedido);
                ctx.SaveChanges();

                var tmpItems = ctx.TmpPedidoItemEntity
                    .Where(t => t.IdUsuario == tmpUsuarioId)
                    .ToList();

                foreach (var tmp in tmpItems)
                {
                    var nuevoItem = new PedidoItemEntity
                    {
                        IdPedido = pedido.IdPedido,
                        IdItem = tmp.IdItem,
                        Cantidad = tmp.Cantidad,
                        Precio = tmp.Precio,
                        Descript = tmp.Descript ?? ""
                    };
                    ctx.PedidoItemEntity.Add(nuevoItem);
                }

                ctx.TmpPedidoItemEntity.RemoveRange(tmpItems);
                ctx.SaveChanges();
                trans.Commit();
                return true;
            }
            catch (Exception ex)
            {
                trans.Rollback();
                MessageBox.Show($"Error al guardar pedido: {ex.Message}", "Centrex", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        // ===============================================
        // 🔹 Agregar o actualizar ítem temporal
        // ===============================================
        public static bool AddItemPedidoTmp(ItemEntity i, double cantidad, double precio, int idUsuario, Guid idUnico, int idTmpPedidoItem = -1)
        {
            try
            {
                using var ctx = new CentrexDbContext();
                double precioCalculado = precio;

                if (i.EsDescuento)
                {
                    add_pedido frm = Application.OpenForms["add_pedido"] as add_pedido;
                    double subtotal = 0d;

                    if (frm is not null)
                        double.TryParse(frm.txt_subTotal.Text, out subtotal);

                    precioCalculado = Math.Round(subtotal * (double)(i.Factor ?? 0), 2) * -1;
                }

                var tmpItem = idTmpPedidoItem > 0
                    ? ctx.TmpPedidoItemEntity.FirstOrDefault(p => p.IdTmpPedidoItem == idTmpPedidoItem)
                    : null;

                if (tmpItem is null)
                {
                    tmpItem = new TmpPedidoItemEntity
                    {
                        IdItem = i.IdItem,
                        Cantidad = (int)Math.Round(cantidad),
                        Precio = (decimal)precioCalculado,
                        Descript = i.Descript,
                        IdUsuario = idUsuario,
                        IdUnico = idUnico,
                        Activo = true
                    };
                    ctx.TmpPedidoItemEntity.Add(tmpItem);
                }
                else
                {
                    tmpItem.Cantidad = (int)Math.Round(cantidad);
                    tmpItem.Precio = (decimal)precioCalculado;
                    tmpItem.Descript = i.Descript;
                }

                ctx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Interaction.MsgBox($"Error al guardar ítem temporal: {ex.Message}", MsgBoxStyle.Critical, "Centrex");
                return false;
            }
        }
    }
}
