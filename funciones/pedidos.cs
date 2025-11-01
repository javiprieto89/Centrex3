using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace Centrex.Funciones
{
    public static class Pedidos
    {
        // ============================================================================================
        // 1️⃣ Helpers generales
        // ============================================================================================
        private static decimal ToDecimalSafe(object value)
        {
            if (value == null || value == DBNull.Value) return 0m;
            return Convert.ToDecimal(value, CultureInfo.InvariantCulture);
        }

        private static int ToIntSafe(object value)
        {
            if (value == null || value == DBNull.Value) return 0;
            return Convert.ToInt32(value, CultureInfo.InvariantCulture);
        }

        private static decimal Redondear(decimal valor, int decimales = 2)
            => Math.Round(valor, decimales, MidpointRounding.AwayFromZero);

        // ============================================================================================
        // 2️⃣ Funciones de cálculo
        // ============================================================================================

        public static decimal CalcularSubtotal(IEnumerable<TmpPedidoItemEntity> items)
        {
            if (items == null) return 0m;
            return items.Sum(i =>
                Convert.ToDecimal(i.Cantidad) * i.Precio
            );
        }

        public static decimal CalcularIVA(decimal subtotal, decimal porcentajeIVA)
        {
            return Redondear(subtotal * (porcentajeIVA / 100m));
        }

        public static decimal CalcularDescuento(decimal subtotal, decimal markup)
        {
            if (markup <= 0) return 0m;
            var descuento = subtotal - (subtotal / (1 + (markup / 100m)));
            return Redondear(descuento);
        }

        public static decimal CalcularTotal(decimal subtotal, decimal iva, decimal descuento)
        {
            return Redondear(subtotal + iva - descuento);
        }

        public static decimal CalculoTotalPuro(DataGridView grid)
        {
            decimal total = 0m;
            foreach (DataGridViewRow row in grid.Rows)
            {
                if (row.Cells["Subtotal"]?.Value != null)
                    total += ToDecimalSafe(row.Cells["Subtotal"].Value);
            }
            return Redondear(total);
        }

        // ============================================================================================
        // 3️⃣ Cálculo de impuestos (versión EF del código VB)
        // ============================================================================================

        public static decimal CalcularImpuestosItem(int idTmpPedidoItem, int idItem, ComprobanteEntity comprobanteSeleccionado, bool esPresupuesto)
        {
            try
            {
                using (var ctx = new CentrexDbContext())
                {
                    // Traer los impuestos activos asociados al ítem
                    var impuestos = (from ti in ctx.TmpPedidoItemEntity
                                     join it in ctx.ItemEntity on ti.IdItem equals it.IdItem
                                     join ii in ctx.ItemImpuestoEntity on it.IdItem equals ii.IdItem
                                     join im in ctx.ImpuestoEntity on ii.IdImpuesto equals im.IdImpuesto
                                     where ti.IdTmpPedidoItem == idTmpPedidoItem
                                           && ti.IdItem == idItem
                                           && ti.Activo == true
                                           && ii.Activo == true
                                           && !it.EsDescuento
                                           && !it.EsMarkup
                                     select im).ToList();

                    decimal totalImpuestos = 0m;

                    foreach (var imp in impuestos)
                    {
                        bool esIVA = imp.Nombre != null && imp.Nombre.ToLower().Contains("iva");

                        // Si es presupuesto, excluir IVA
                        if (esPresupuesto && esIVA)
                            continue;

                        // Caso general (igual que el Select Case del VB)
                        if (comprobanteSeleccionado.IdTipoComprobante == 99)
                        {
                            if (esPresupuesto && esIVA)
                                continue;
                            else
                                totalImpuestos += Convert.ToDecimal(imp.Porcentaje);
                        }
                        else
                        {
                            totalImpuestos += Convert.ToDecimal(imp.Porcentaje);
                        }
                    }

                    return Redondear(totalImpuestos, 2);
                }
            }
            catch (Exception ex)
            {
                Interaction.MsgBox("Error al calcular impuestos: " + ex.Message, MsgBoxStyle.Exclamation, "Centrex");
                return -1m;
            }
        }

        // ============================================================================================
        // 4️⃣ Actualización de totales (updatePrecios y descuentos)
        // ============================================================================================

        public static void UpdatePrecios(
            DataGridView grid,
            CheckBox chkPresupuesto,
            TextBox txtSubTotal,
            TextBox txtIVA,
            TextBox txtTotal,
            TextBox txtTotalO,
            TextBox txtMarkup,
            TextBox txtTotalDescuentos,
            ComprobanteEntity comprobante,
            int idUsuario,
            Guid idUnico)
        {
            try
            {
                using (var ctx = new CentrexDbContext())
                {
                    var tmpItems = ctx.TmpPedidoItemEntity
                        .Where(t => t.IdUsuario == idUsuario && t.IdUnico == idUnico && t.Activo == true)
                        .Include(t => t.IdPedidoItemNavigation)
                        .ToList();

                    decimal subtotal = CalcularSubtotal(tmpItems);
                    decimal ivaTotal = 0m;

                    foreach (var tmp in tmpItems)
                    {
                        var porcImpuesto = CalcularImpuestosItem(tmp.IdTmpPedidoItem, tmp.IdItem == null ? 0 : (int)tmp.IdItem, comprobante, chkPresupuesto.Checked);
                        ivaTotal += Redondear(
                            (tmp.Precio *
                             Convert.ToDecimal(tmp.Cantidad)) *
                            (porcImpuesto / 100m)
                        );
                    }

                    decimal descuento = CalcularDescuento(subtotal, ToDecimalSafe(txtMarkup.Text));
                    decimal total = CalcularTotal(subtotal, ivaTotal, descuento);

                    txtSubTotal.Text = subtotal.ToString("N2");
                    txtIVA.Text = ivaTotal.ToString("N2");
                    txtTotal.Text = total.ToString("N2");
                    txtTotalO.Text = total.ToString("N2");

                    UpdateDescuentos(grid, txtTotalDescuentos, txtMarkup);
                }
            }
            catch (Exception ex)
            {
                Interaction.MsgBox("Error al actualizar precios: " + ex.Message, MsgBoxStyle.Exclamation, "Centrex");
            }
        }

        public static void UpdateDescuentos(DataGridView grid, TextBox txtTotalDescuentos, TextBox txtMarkup)
        {
            try
            {
                decimal total = CalculoTotalPuro(grid);
                decimal markup = ToDecimalSafe(txtMarkup.Text);
                decimal descuento = CalcularDescuento(total, markup);
                txtTotalDescuentos.Text = descuento.ToString("N2");
            }
            catch (Exception ex)
            {
                Interaction.MsgBox("Error al actualizar descuentos: " + ex.Message, MsgBoxStyle.Exclamation, "Centrex");
            }
        }

        // ============================================================================================
        // 5️⃣ Funciones de gestión de ítems (borrar, recargar, descuentos)
        // ============================================================================================

        public static bool BorrarItemCargado(int idTmpPedidoItem, bool esMarkup = false)
        {
            try
            {
                using (var ctx = new CentrexDbContext())
                {
                    var item = ctx.TmpPedidoItemEntity.FirstOrDefault(i => i.IdTmpPedidoItem == idTmpPedidoItem);
                    if (item == null) return false;

                    if (esMarkup)
                        ctx.TmpPedidoItemEntity.Remove(item);
                    else
                        item.Activo = false;

                    ctx.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Interaction.MsgBox("Error al borrar item: " + ex.Message, MsgBoxStyle.Exclamation, "Centrex");
                return false;
            }
        }

        public static int ExisteDescuentoMarkupTmp(int idItem)
        {
            using (var ctx = new CentrexDbContext())
            {
                var tmp = ctx.TmpPedidoItemEntity.FirstOrDefault(i => i.IdItem == idItem && i.Activo == true);
                return tmp?.IdTmpPedidoItem ?? -1;
            }
        }

        public static int IdItemMarkupPedido(int idPedido)
        {
            using (var ctx = new CentrexDbContext())
            {
                var item = ctx.PedidoItemEntity
                    .Include(p => p.IdItemNavigation)
                    .FirstOrDefault(p => p.IdPedido == idPedido && p.IdItemNavigation.EsMarkup);
                return item?.IdItem ?? -1;
            }
        }

        public static void RecargaPrecios(int idPedido, int idItem, string tabla = "TmpPedidoItemEntity")
        {
            using (var ctx = new CentrexDbContext())
            {
                if (tabla == "TmpPedidoItemEntity")
                {
                    var tmp = ctx.TmpPedidoItemEntity.FirstOrDefault(t => t.IdItem == idItem);
                    if (tmp != null)
                    {
                        tmp.Precio = ctx.ItemEntity.Where(i => i.IdItem == idItem).Select(i => i.PrecioLista).FirstOrDefault();
                        ctx.SaveChanges();
                    }
                }
                else
                {
                    var pedidoItem = ctx.PedidoItemEntity.FirstOrDefault(p => p.IdPedido == idPedido && p.IdItem == idItem);
                    if (pedidoItem != null)
                    {
                        pedidoItem.Precio = ctx.ItemEntity.Where(i => i.IdItem == idItem).Select(i => i.PrecioLista).FirstOrDefault();
                        ctx.SaveChanges();
                    }
                }
            }
        }

        // ============================================================================================
        // 6️⃣ Funciones de tablas temporales y duplicación
        // ============================================================================================

        public static void BorrarTablaPedidosTemporales(int idUsuario, Guid idUnico)
        {
            using (var ctx = new CentrexDbContext())
            {
                var items = ctx.TmpPedidoItemEntity
                    .Where(t => t.IdUsuario == idUsuario && t.IdUnico == idUnico)
                    .ToList();
                ctx.TmpPedidoItemEntity.RemoveRange(items);
                ctx.SaveChanges();
            }
        }

        public static bool GuardarPedido(int idUsuario, Guid idUnico, int idPedido)
        {
            using (var ctx = new CentrexDbContext())
            {
                var tmpItems = ctx.TmpPedidoItemEntity
                    .Where(t => t.IdUsuario == idUsuario && t.IdUnico == idUnico && t.Activo == true)
                    .ToList();

                foreach (var tmp in tmpItems)
                {
                    var nuevo = new PedidoItemEntity
                    {
                        IdPedido = idPedido,
                        IdItem = tmp.IdItem,
                        Cantidad = tmp.Cantidad,
                        Precio = tmp.Precio,
                        Activo = true
                    };
                    ctx.PedidoItemEntity.Add(nuevo);
                }

                ctx.SaveChanges();
                return true;
            }
        }

        public static void pedido_a_pedidoTmp(int idPedido, int idUsuario, Guid idUnico)
        {
            using (var ctx = new CentrexDbContext())
            {
                var items = ctx.PedidoItemEntity
                    .Where(p => p.IdPedido == idPedido)
                    .ToList();

                foreach (var i in items)
                {
                    var tmp = new TmpPedidoItemEntity
                    {
                        IdUsuario = idUsuario,
                        IdUnico = idUnico,
                        IdItem = i.IdItem,
                        Cantidad = i.Cantidad,
                        Precio = i.Precio,
                        Activo = true
                    };
                    ctx.TmpPedidoItemEntity.Add(tmp);
                }

                ctx.SaveChanges();
            }
        }

        public static void DuplicarPedido(int idPedidoOriginal)
        {
            using (var ctx = new CentrexDbContext())
            {
                var pedido = ctx.PedidoEntity.FirstOrDefault(p => p.IdPedido == idPedidoOriginal);
                if (pedido == null) return;

                var nuevo = new PedidoEntity
                {
                    Fecha = DateOnly.FromDateTime(DateTime.Now),
                    IdCliente = pedido.IdCliente,
                    IdComprobante = pedido.IdComprobante,
                    Subtotal = pedido.Subtotal,
                    Iva = pedido.Iva ?? 0,
                    Total = pedido.Total,
                    Markup = pedido.Markup ?? 0,
                    Activo = true
                };

                ctx.PedidoEntity.Add(nuevo);
                ctx.SaveChanges();

                var items = ctx.PedidoItemEntity.Where(i => i.IdPedido == idPedidoOriginal).ToList();
                foreach (var i in items)
                {
                    ctx.PedidoItemEntity.Add(new PedidoItemEntity
                    {
                        IdPedido = nuevo.IdPedido,
                        IdItem = i.IdItem,
                        Cantidad = i.Cantidad,
                        Precio = i.Precio,
                        Activo = true
                    });
                }

                ctx.SaveChanges();
            }
        }

        // ============================================================================================
        // 7️⃣ Inserción / Actualización de ítems temporales (versión EF de SP_insertItemsTMP y SP_updateItemsTMP)
        // ============================================================================================

        public static bool AddItemPedidoTmp(
            ItemEntity item,
            int cantidad,
            decimal precio,
            int idUsuario,
            Guid idUnico,
            int idTmpPedidoItem = -1)
        {
            try
            {
                using (var ctx = new CentrexDbContext())
                {
                    // Si el ítem es descuento, recalcular el precio negativo sobre subtotal actual
                    if (item.EsDescuento)
                    {
                        var frm = Application.OpenForms["add_pedido"] as Form;
                        if (frm != null)
                        {
                            var txtSubTotal = frm.Controls.Find("txt_subTotal", true).FirstOrDefault() as TextBox;
                            if (txtSubTotal != null && decimal.TryParse(txtSubTotal.Text, out var subtotal))
                            {
                                precio = Math.Round(subtotal * precio, 2) * -1;
                            }
                        }
                    }

                    // Buscar si es inserción o actualización
                    TmpPedidoItemEntity registro;

                    if (idTmpPedidoItem <= 0)
                    {
                        // Nuevo registro temporal
                        registro = new TmpPedidoItemEntity
                        {
                            IdTmpPedidoItem = idTmpPedidoItem,
                            IdItem = item.IdItem,
                            Descript = item.Descript ?? string.Empty,
                            Cantidad = cantidad,
                            Precio = precio,
                            IdUsuario = idUsuario,
                            IdUnico = idUnico,
                            Activo = true
                        };

                        // Si el ítem es manual (como en SP_insertItemsTMP)
                        if ((item.Item ?? string.Empty).ToUpper() == "MANUAL")
                        {
                            registro.IdItem = null;
                        }

                        ctx.TmpPedidoItemEntity.Add(registro);
                    }
                    else
                    {
                        // Actualizar existente
                        registro = ctx.TmpPedidoItemEntity.FirstOrDefault(t => t.IdTmpPedidoItem == idTmpPedidoItem);

                        if (registro == null)
                        {
                            Interaction.MsgBox("No se encontró el registro temporal a actualizar.", MsgBoxStyle.Exclamation, "Centrex");
                            return false;
                        }

                        if ((item.Item ?? string.Empty).ToUpper() == "MANUAL")
                        {
                            registro.Descript = item.Descript ?? string.Empty;
                            registro.IdItem = null;
                        }
                        else
                        {
                            registro.IdItem = item.IdItem;
                            registro.Descript = item.Descript ?? string.Empty;
                        }

                        registro.Cantidad = cantidad;
                        registro.Precio = precio;
                        registro.Activo = true;
                        ctx.TmpPedidoItemEntity.Update(registro);
                    }

                    ctx.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Interaction.MsgBox("Error al agregar ítem temporal: " + ex.Message, MsgBoxStyle.Exclamation, "Centrex");
                return false;
            }
        }

        /// <summary>
        /// Inserta un nuevo registro de pedido (solo encabezado, sin ítems).
        /// </summary>
        public static bool AddPedido(PedidoEntity p)
        {
            try
            {
                using (var ctx = new CentrexDbContext())
                {
                    var nuevoPedido = new PedidoEntity
                    {
                        Fecha = p.Fecha,
                        FechaEdicion = p.FechaEdicion == default ? DateOnly.FromDateTime(DateTime.Now) : p.FechaEdicion,
                        IdCliente = p.IdCliente,
                        Markup = p.Markup,
                        Subtotal = p.Subtotal,
                        Iva = p.Iva,
                        Total = p.Total,
                        Nota1 = p.Nota1,
                        Nota2 = p.Nota2,
                        EsPresupuesto = p.EsPresupuesto,
                        Activo = p.Activo,
                        Cerrado = p.Cerrado,
                        IdPresupuesto = p.IdPresupuesto == 0 ? null : p.IdPresupuesto,
                        IdComprobante = p.IdComprobante,
                        Cae = p.Cae,
                        FechaVencimientoCae = p.FechaVencimientoCae,
                        PuntoVenta = p.PuntoVenta,
                        NumeroComprobante = p.NumeroComprobante == 0 ? null : p.NumeroComprobante,
                        CodigoDeBarras = p.CodigoDeBarras,
                        EsTest = p.EsTest,
                        IdCc = p.IdCc,
                        NumeroComprobanteAnulado = p.NumeroComprobanteAnulado,
                        NumeroPedidoAnulado = p.NumeroPedidoAnulado,
                        IdUsuario = p.IdUsuario
                    };

                    ctx.PedidoEntity.Add(nuevoPedido);
                    ctx.SaveChanges();

                    // 🔹 Guardamos el nuevo ID generado
                    p.IdPedido = nuevoPedido.IdPedido;

                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al insertar pedido: {ex.Message}", "Centrex", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static double askCantidadCargada(int idItem, int idPedido, string tableName, int idUsuario, Guid idUnico)
        {
            try
            {
                using var ctx = new CentrexDbContext();

                if (tableName == "pedidos_detalle")
                {
                    var cantidad = ctx.PedidoItemEntity
                        .AsNoTracking()
                        .Where(x => x.IdItem == idItem && x.IdPedido == idPedido)
                        .Select(x => x.Cantidad)
                        .FirstOrDefault();

                    return cantidad == 0 ? -1 : (double)cantidad;
                }
                else
                {
                    var cantidad = ctx.TmpPedidoItemEntity
                        .AsNoTracking()
                        .Where(x => x.IdItem == idItem && x.IdUsuario == idUsuario && x.IdUnico == idUnico)
                        .Select(x => x.Cantidad)
                        .FirstOrDefault();

                    return cantidad == 0 ? -1 : (double)cantidad;
                }
            }
            catch
            {
                return -1;
            }
        }

        public static double askPreciocargado(int idItem, int idPedido, string tableName, int idUsuario, Guid idUnico)
        {
            try
            {
                using var ctx = new CentrexDbContext();

                if (tableName == "pedidos_detalle")
                {
                    var precio = ctx.PedidoItemEntity
                        .AsNoTracking()
                        .Where(x => x.IdItem == idItem && x.IdPedido == idPedido)
                        .Select(x => x.Precio)
                        .FirstOrDefault();

                    return precio == 0 ? -1 : (double)precio;
                }
                else
                {
                    var precio = ctx.TmpPedidoItemEntity
                        .AsNoTracking()
                        .Where(x => x.IdItem == idItem && x.IdUsuario == idUsuario && x.IdUnico == idUnico)
                        .Select(x => x.Precio)
                        .FirstOrDefault();

                    return precio == 0 ? -1 : (double)precio;
                }
            }
            catch
            {
                return -1;
            }
        }


        public static PedidoEntity info_pedido(int idPedido)
        {
            try
            {
                using (var ctx = new CentrexDbContext())
                {
                    PedidoEntity pedido;

                    if (idPedido != 0)
                    {
                        // 🔹 Si no se pasa ID → último pedido cargado
                        pedido = ctx.PedidoEntity
                            .OrderByDescending(p => p.IdPedido)
                            .FirstOrDefault();
                    }
                    else
                    {
                        // 🔹 Si se pasa ID → traer ese pedido
                        int id = Convert.ToInt32(idPedido);
                        pedido = ctx.PedidoEntity
                            .Include(p => p.IdClienteNavigation)
                            .Include(p => p.IdComprobanteNavigation)
                            .FirstOrDefault(p => p.IdPedido == id);
                    }

                    if (pedido == null)
                    {
                        Interaction.MsgBox("No se encontró el pedido solicitado.", MsgBoxStyle.Exclamation, "Centrex");
                        return null;
                    }

                    // 🔹 Asegurar que todas las propiedades opcionales tengan valores por defecto
                    pedido.IdPresupuesto ??= 0;
                    pedido.Iva ??= 0;
                    pedido.Markup ??= 0;
                    pedido.PuntoVenta ??= 0;
                    pedido.NumeroComprobante ??= 0;
                    pedido.EsDuplicado ??= false;

                    return pedido;
                }
            }
            catch (Exception ex)
            {
                Interaction.MsgBox($"Error al obtener información del pedido: {ex.Message}", MsgBoxStyle.Critical, "Centrex");
                return null;
            }
        }


        // <summary>
        /// Actualiza los datos principales del pedido (encabezado). 
        /// No modifica ítems ni transacciones.
        /// </summary>
        public static bool UpdatePedido(PedidoEntity p, bool borra = false)
        {
            try
            {
                using (var ctx = new CentrexDbContext())
                {
                    var pedido = ctx.PedidoEntity.FirstOrDefault(x => x.IdPedido == p.IdPedido);
                    if (pedido == null)
                    {
                        MessageBox.Show("No se encontró el pedido a actualizar.", "Centrex", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }

                    if (borra)
                    {
                        pedido.Activo = false;
                    }
                    else
                    {
                        pedido.Fecha = p.Fecha;
                        pedido.FechaEdicion = p.FechaEdicion;
                        pedido.IdCliente = p.IdCliente;
                        pedido.Markup = p.Markup;
                        pedido.Subtotal = p.Subtotal;
                        pedido.Iva = p.Iva;
                        pedido.Total = p.Total;
                        pedido.Nota1 = p.Nota1;
                        pedido.Nota2 = p.Nota2;
                        pedido.EsPresupuesto = p.EsPresupuesto;
                        pedido.Activo = p.Activo;
                        pedido.Cerrado = p.Cerrado;
                        pedido.IdPresupuesto = p.IdPresupuesto == 0 ? null : p.IdPresupuesto;
                        pedido.IdComprobante = p.IdComprobante;
                        pedido.Cae = p.Cae;
                        pedido.FechaVencimientoCae = p.FechaVencimientoCae;
                        pedido.PuntoVenta = p.PuntoVenta;
                        pedido.NumeroComprobante = p.NumeroComprobante;
                        pedido.CodigoDeBarras = p.CodigoDeBarras;
                        pedido.EsTest = p.EsTest;
                        pedido.IdCc = p.IdCc;
                        pedido.NumeroComprobanteAnulado = p.NumeroComprobanteAnulado;
                        pedido.NumeroPedidoAnulado = p.NumeroPedidoAnulado;
                        pedido.IdUsuario = p.IdUsuario;
                    }

                    ctx.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al actualizar pedido: {ex.Message}", "Centrex", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

    }
}

