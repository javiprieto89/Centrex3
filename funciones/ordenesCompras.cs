using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Centrex.Models;

namespace Centrex
{

    static class ordenesCompras
    {
        // ************************************ FUNCIONES DE ORDENES DE COMPRA MIGRADAS A ENTITY FRAMEWORK ***************************

        /// <summary>
    /// Obtiene información de una orden de compra usando Entity Framework
    /// </summary>
        public static ordenCompra info_ordenCompra(string id_ordenCompra = "")
        {
            var tmp = new ordenCompra();
            try
            {
                using (var context = new CentrexDbContext())
                {
                    OrdenCompraEntity ordenEntity = null;

                    if (string.IsNullOrEmpty(id_ordenCompra))
                    {
                        // Obtener la última orden de compra
                        ordenEntity = context.OrdenCompraEntity.OrderByDescending(o => o.IdOrdenCompra).FirstOrDefault();

                    }
                    else
                    {
                        // Obtener orden específica
                        int idOrden = Conversions.ToInteger(id_ordenCompra);
                        ordenEntity = context.OrdenCompraEntity.FirstOrDefault(o => o.IdOrdenCompra == idOrden);
                    }

                    if (ordenEntity is not null)
                    {
                        // Calcular subtotal, iva y total desde los items
                        var items = context.OrdenCompraItemEntity.Where(i => i.IdOrdenCompra == ordenEntity.IdOrdenCompra).ToList();


                        decimal subtotal = items.Sum(i => i.Cantidad * i.Precio);
                        decimal iva = 0m;
                        decimal total = subtotal + iva;

                        tmp.id_ordenCompra = Conversions.ToInteger(ordenEntity.IdOrdenCompra.ToString());
                        tmp.IdProveedor = Conversions.ToInteger(ordenEntity.IdProveedor.HasValue ? ordenEntity.IdProveedor.Value.ToString() : "0");
                        tmp.fecha_carga = ordenEntity.fecha.ToString("dd/MM/yyyy");
                        tmp.fecha_comprobante = ordenEntity.fecha.ToString("dd/MM/yyyy");
                        tmp.fecha_recepcion = "0"; // No existe en la entidad
                        tmp.subtotal = Conversions.ToDouble(subtotal.ToString());
                        tmp.iva = Conversions.ToDouble(iva.ToString());
                        tmp.total = Conversions.ToDouble(total.ToString());
                        tmp.recibido = Conversions.ToBoolean("0"); // No existe en la entidad
                        tmp.notas = ordenEntity.observaciones ?? "";
                        tmp.activo = Conversions.ToBoolean(ordenEntity.activo ? "1" : "0");
                    }
                    else
                    {
                        tmp.id_ordenCompra = -1;
                    }

                    return tmp;
                }
            }
            catch (Exception ex)
            {
                Interaction.MsgBox(ex.Message.ToString());
                tmp.id_ordenCompra = -1;
                return tmp;
            }
        }

        /// <summary>
    /// Agrega una nueva orden de compra usando Entity Framework
    /// </summary>
        public static bool addOrdenCompra(ordenCompra oc)
        {
            try
            {
                using (var context = new CentrexDbContext())
                {
                    var nuevaOrden = new OrdenCompraEntity();

                    // Mapear campos disponibles
                    if (!string.IsNullOrEmpty(oc.IdProveedor.ToString()) && oc.IdProveedor != Conversions.ToDouble("0"))
                    {
                        nuevaOrden.IdProveedor = oc.IdProveedor;
                    }

                    // Usar fecha_comprobante como fecha principal
                    if (!string.IsNullOrEmpty(oc.fecha_comprobante))
                    {
                        nuevaOrden.fecha = DateTime.ParseExact(oc.fecha_comprobante, "dd/MM/yyyy", null);
                    }
                    else if (!string.IsNullOrEmpty(oc.fecha_carga))
                    {
                        nuevaOrden.fecha = DateTime.ParseExact(oc.fecha_carga, "dd/MM/yyyy", null);
                    }
                    else
                    {
                        nuevaOrden.fecha = DateTime.Now;
                    }

                    nuevaOrden.observaciones = oc.notas ?? "";
                    nuevaOrden.activo = true;

                    context.OrdenCompraEntity.Add(nuevaOrden);
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

        /// <summary>
    /// Actualiza o borra (lógicamente) una orden de compra usando Entity Framework
    /// </summary>
        public static bool updateOrdenCompra(ordenCompra oc, bool borra = false)
        {
            try
            {
                using (var context = new CentrexDbContext())
                {
                    int idOrden = oc.id_ordenCompra;
                    var ordenEntity = context.OrdenCompraEntity.FirstOrDefault(o => o.id_ordenCompra == idOrden);

                    if (ordenEntity is null)
                    {
                        Interaction.MsgBox("Orden de compra no encontrada");
                        return false;
                    }

                    if (borra)
                    {
                        // Baja lógica
                        ordenEntity.activo = false;
                    }
                    else
                    {
                        // Actualización
                        if (!string.IsNullOrEmpty(oc.IdProveedor.ToString()) && oc.IdProveedor != Conversions.ToDouble("0"))
                        {
                            ordenEntity.IdProveedor = oc.IdProveedor;
                        }

                        if (!string.IsNullOrEmpty(oc.fecha_comprobante))
                        {
                            ordenEntity.fecha = DateTime.ParseExact(oc.fecha_comprobante, "dd/MM/yyyy", null);
                        }

                        ordenEntity.observaciones = oc.notas ?? "";

                        if (!string.IsNullOrEmpty(Conversions.ToString(oc.activo)))
                        {
                            ordenEntity.activo = oc.activo == Conversions.ToBoolean("1");
                        }
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

        /// <summary>
    /// Borra físicamente una orden de compra y sus items usando Entity Framework
    /// </summary>
        public static bool borrarOrdenCompra(ordenCompra oc)
        {
            try
            {
                using (var context = new CentrexDbContext())
                {
                    int idOrden = oc.id_ordenCompra;

                    // Borrar items temporales
                    var tmpItemEntity = context.TmpOcItemEntity.AsQueryable().Where(t => t.IdOrdenCompra == idOrden).ToList();

                    foreach (var item in tmpItemEntity)
                        context.TmpOcItemEntity.Remove((TmpOCItemEntity)item);

                    // Borrar items de la orden
                    var items = context.OrdenCompraItemEntity.Where(i => i.IdOrdenCompra == idOrden).ToList();

                    foreach (var item in items)
                        context.OrdenCompraItemEntity.Remove((OrdenCompraItemEntity)item);

                    // Borrar orden
                    var ordenEntity = context.OrdenCompraEntity.FirstOrDefault(o => o.IdOrdenCompra == idOrden);
                    if (ordenEntity is not null)
                    {
                        context.OrdenCompraEntity.Remove(ordenEntity);
                    }

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

        /// <summary>
    /// Agrega o actualiza un item en la tabla temporal usando Entity Framework
    /// </summary>
        public static bool addItemOCtmp(item i, int cantidad, double precio, int id_tmpOCItem = -1)
        {
            try
            {
                using (var context = new CentrexDbContext())
                {
                    if (id_tmpOCItem == -1 | id_tmpOCItem == 0)
                    {
                        // Insertar nuevo item temporal
                        var nuevoTmpItem = new TmpOrdenCompraItemEntity();
                        nuevoTmpItem.IdItem = i.id_item;
                        nuevoTmpItem.Cantidad = cantidad;
                        nuevoTmpItem.Precio = precio;
                        nuevoTmpItem.Activo = true;
                        nuevoTmpItem.Descript = i.descript;
                        nuevoTmpItem.CantidadRecibida = 0;

                        context.TmpOcItemEntity.Add(nuevoTmpItem);
                    }
                    else
                    {
                        // Actualizar item temporal existente
                        var tmpItem = context.TmpOcItemEntity.AsQueryable().FirstOrDefault(t => t.IdTmpOcItem == id_tmpOCItem);
                        if (tmpItem is not null)
                        {
                            tmpItem.IdItem = i.id_item;
                            tmpItem.cantidad = cantidad;
                            tmpItem.precio = (decimal)precio;
                            tmpItem.descript = i.descript;
                        }
                        else
                        {
                            return false;
                        }
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

        /// <summary>
    /// Guarda los items temporales en la orden de compra definitiva usando Entity Framework
    /// </summary>
        public static bool guardarOrdenCompra(int id_ordenCompra = 0)
        {
            try
            {
                using (var context = new CentrexDbContext())
                {
                    // Obtener el último pedido si no se especifica
                    if (id_ordenCompra == 0)
                    {
                        var oc = info_ordenCompra();
                        id_ordenCompra = oc.id_ordenCompra;
                    }

                    // Obtener items temporales activos
                    var tmpItemEntity = context.TmpOcItemEntity.AsQueryable().Where(t => t.activo == true).ToList();


                    foreach (var tmpItem in tmpItemEntity)
                    {
                        bool existe = false;

                        // Si tiene IdOcItem, buscar y actualizar
                        if (tmpItem.IdOcItem.HasValue && tmpItem.IdOcItem.Value > 0)
                        {
                            var itemExistente = context.OrdenCompraItemEntity.FirstOrDefault(oi => oi.IdOrdenCompraItem == tmpItem.IdOcItem.Value && oi.IdOrdenCompra == id_ordenCompra);

                            if (itemExistente is not null)
                            {
                                itemExistente.IdItem = tmpItem.IdItem.HasValue ? tmpItem.IdItem.Value : 0;
                                itemExistente.cantidad = tmpItem.cantidad;
                                itemExistente.precio = tmpItem.precio;
                                itemExistente.Observaciones = tmpItem.descript;
                                existe = true;
                            }
                        }

                        // Si no existe, crear nuevo
                        if (!existe)
                        {
                            var nuevoItem = new OrdenCompraItemEntity();
                            nuevoItem.IdOrdenCompra = id_ordenCompra;
                            nuevoItem.IdItem = tmpItem.IdItem.HasValue ? tmpItem.IdItem.Value : 0;
                            nuevoItem.cantidad = tmpItem.cantidad;
                            nuevoItem.precio = tmpItem.precio;
                            nuevoItem.Observaciones = tmpItem.descript;

                            context.OrdenCompraItemEntity.Add(nuevoItem);
                        }
                    }

                    context.SaveChanges();

                    // Borrar items que están en la BD pero marcados como inactivos en temporal
                    var tmpItemEntityInactivos = context.TmpOcItemEntity.AsQueryable().Where(t => (t.activo is { } arg1 ? arg1 == false : (bool?)null) && t.IdOrdenCompra == id_ordenCompra).ToList();


                    foreach (var tmpInactivo in tmpItemEntityInactivos)
                    {
                        if (Conversions.ToBoolean(((dynamic)tmpInactivo).id_ocItem.HasValue))
                        {
                            var itemABorrar = context.OrdenCompraItemEntity.FirstOrDefault(oi => Operators.ConditionalCompareObjectEqual(oi.id_ordenCompraItem, tmpInactivo.id_ocItem.Value, false));

                            if (itemABorrar is not null)
                            {
                                context.OrdenCompraItemEntity.Remove(itemABorrar);
                            }
                        }
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

        /// <summary>
    /// Copia items de una orden de compra a la tabla temporal usando Entity Framework
    /// </summary>
        public static bool oc_a_ocTmp(int id_ordenCompra)
        {
            try
            {
                using (var context = new CentrexDbContext())
                {
                    var items = context.OrdenCompraItemEntity.Where(i => i.id_ordenCompra == id_ordenCompra).ToList();


                    foreach (var item in items)
                    {
                        var tmpItem = new TmpOCItemEntity();
                        tmpItem.id_ocItem = item.id_ordenCompraItem;
                        tmpItem.id_ordenCompra = item.id_ordenCompra;
                        tmpItem.id_item = item.id_item;
                        tmpItem.cantidad = item.cantidad;
                        tmpItem.precio = item.precio;
                        tmpItem.activo = true;
                        tmpItem.descript = item.observaciones;
                        tmpItem.cantidad_recibida = 0; // No existe en la entidad OrdenCompraItem

                        context.TmpOcItemEntity.Add(tmpItem);
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

        /// <summary>
    /// Obtiene la cantidad cargada de un item en la tabla temporal usando Entity Framework
    /// </summary>
        public static double askCantidadCargadaOC(int id_item, int id = -1, int id_tmpOCItem = -1)
        {
            try
            {
                using (var context = new CentrexDbContext())
                {
                    var query = context.TmpOcItemEntity.AsQueryable();

                    // Aplicar filtros
                    query = query.Where(t => t.IdItem == id_item);

                    if (id != -1)
                    {
                        query = query.Where(t => t.IdOrdenCompra == id);
                    }

                    if (id_tmpOCItem != -1)
                    {
                        query = query.Where(t => t.IdTmpOcitem == id_tmpOCItem);
                    }

                    var tmpItem = query.FirstOrDefault();

                    if (tmpItem is not null)
                    {
                        return tmpItem.cantidad;
                    }
                    else
                    {
                        return -1;
                    }
                }
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        /// <summary>
    /// Obtiene el precio cargado de un item en la tabla temporal usando Entity Framework
    /// </summary>
        public static double askPrecioCargadoOC(int id_item, int id = -1, int id_tmpOCItem = -1)
        {
            try
            {
                using (var context = new CentrexDbContext())
                {
                    var query = context.TmpOcItemEntity.AsQueryable();

                    // Aplicar filtros
                    query = query.Where(t => t.IdItem == id_item);

                    if (id != -1)
                    {
                        query = query.Where(t => t.IdOrdenCompra == id);
                    }

                    if (id_tmpOCItem != -1)
                    {
                        query = query.Where(t => t.IdTmpOcitem == id_tmpOCItem);
                    }

                    var tmpItem = query.FirstOrDefault();

                    if (tmpItem is not null)
                    {
                        return (double)tmpItem.Precio;
                    }
                    else
                    {
                        return -1;
                    }
                }
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        /// <summary>
    /// Función de compatibilidad para imprimir producción
    /// </summary>
        public static void imprimirProduccion(int id_produccion)
        {
            // If showrpt Then
            // id = id_produccion
            // id = 0
            // End If
        }

        /// <summary>
    /// Actualiza precios y totales de la orden de compra en el DataGrid usando Entity Framework
    /// </summary>
        public static bool updatePreciosOC(DataGridView datagrid, TextBox txt_subTotal, TextBox txt_impuestos, TextBox txt_total, TextBox txt_totalOriginal)
        {
            try
            {
                using (var context = new CentrexDbContext())
                {
                    // Obtener items temporales activos
                    var tmpItemEntity = context.TmpOcItemEntity.AsQueryable().Where(t => t.activo == true).OrderBy(t => t.IdTmpOcItem).ToList();



                    double subtotal = 0d;
                    double totalImpuestoEntity = 0d;
                    double total = 0d;

                    // Calcular subtotal
                    foreach (var tmpItem in tmpItemEntity)
                    {
                        decimal cantidad = Conversions.ToDecimal(((dynamic)tmpItem).Cantidad);
                        decimal precio = Conversions.ToDecimal(((dynamic)tmpItem).Precio);
                        subtotal += (double)(cantidad * precio);
                    }

                    txt_totalOriginal.Text = subtotal.ToString();

                    // Calcular impuestos
                    foreach (var tmpItem in tmpItemEntity)
                    {
                        decimal precio = Conversions.ToDecimal(((dynamic)tmpItem).Precio);
                        decimal cantidad = Conversions.ToDecimal(((dynamic)tmpItem).cantidad);
                        double impuestosItem = ordenesCompras.calculaImpuestoEntityItem(((dynamic)tmpItem).id_tmpOCItem.ToString(), Conversions.ToBoolean(((dynamic)tmpItem).id_item.HasValue) ? ((dynamic)tmpItem).id_item.Value.ToString() : "0");
                        double totalImpuestoItem = impuestosItem * (double)(precio * cantidad) / 100d;
                        totalImpuestoEntity += totalImpuestoItem;
                    }

                    total = subtotal + totalImpuestoEntity;

                    txt_subTotal.Text = Math.Round(subtotal, 2).ToString();
                    txt_impuestos.Text = Math.Round(totalImpuestoEntity, 2).ToString();
                    txt_total.Text = Math.Round(total, 2).ToString();

                    // Cargar DataGrid
                    var dt = new DataTable();
                    dt.Columns.Add("ID", typeof(string));
                    dt.Columns.Add("id_OCItem", typeof(string));
                    dt.Columns.Add("Producto", typeof(string));
                    dt.Columns.Add("Cant.", typeof(decimal));
                    dt.Columns.Add("Precio", typeof(decimal));
                    dt.Columns.Add("Subtotal", typeof(decimal));

                    foreach (var tmpItem in tmpItemEntity)
                    {
                        var row = dt.NewRow();
                        row["ID"] = ((dynamic)tmpItem).id_tmpOCItem.ToString() + "-" + (Conversions.ToBoolean(((dynamic)tmpItem).id_item.HasValue) ? ((dynamic)tmpItem).id_item.Value.ToString() : "0");
                        row["id_OCItem"] = Conversions.ToBoolean(((dynamic)tmpItem).id_ocItem.HasValue) ? ((dynamic)tmpItem).id_ocItem.Value.ToString() : "";

                        // Obtener descripción del item o usar la descripción temporal
                        string descripcion = Conversions.ToString(((dynamic)tmpItem).descript);
                        if (Conversions.ToBoolean(((dynamic)tmpItem).id_item.HasValue && Operators.ConditionalCompareObjectGreater(((dynamic)tmpItem).id_item.Value, 0, false)))
                        {
                            var itemEntity = context.ItemEntity.FirstOrDefault(i => Operators.ConditionalCompareObjectEqual(i.IdItem, tmpItem.IdItem.Value, false));
                            if (itemEntity is not null)
                            {
                                descripcion = itemEntity.Descript;
                            }
                        }

                        row["Producto"] = descripcion;
                        row["Cant."] = ((dynamic)tmpItem).Cantidad;
                        row["Precio"] = ((dynamic)tmpItem).Precio;
                        row["Subtotal"] = Operators.MultiplyObject(((dynamic)tmpItem).Cantidad, ((dynamic)tmpItem).Precio);
                        dt.Rows.Add(row);
                    }

                    datagrid.DataSource = dt;

                    return true;
                }
            }
            catch (Exception ex)
            {
                Interaction.MsgBox(ex.Message);
                return false;
            }
        }

        /// <summary>
    /// Calcula los impuestos totales de un item usando Entity Framework
    /// </summary>
        public static double calculaImpuestoEntityItem(string id_tmpOCItem, string id_item)
        {
            try
            {
                using (var context = new CentrexDbContext())
                {
                    int idItemInt = Conversions.ToInteger(id_item);
                    int idTmpInt = Conversions.ToInteger(id_tmpOCItem);

                    // Verificar que el item temporal existe
                    var tmpItem = context.TmpOcItemEntity.AsQueryable().FirstOrDefault(t => t.IdTmpOcitem == idTmpInt && t.IdItem == idItemInt && (t.Activo is { } arg2 ? arg2 == true : (bool?)null));

                    if (tmpItem is null)
                    {
                        return 0d;
                    }

                    // Obtener impuestos del item
                    var impuestos = context.ItemImpuestoEntity.Where(ii => ii.IdItem == idItemInt && ii.Activo == true).Join(context.ImpuestoEntity, ii => ii.IdImpuesto, i => i.IdImpuesto, (ii, i) => new { i.Porcentaje }).ToList();



                    double totalImpuestoEntity = 0d;

                    foreach (var imp in impuestos)
                        totalImpuestoEntity = Conversions.ToDouble(totalImpuestoEntity + ((dynamic)imp).Porcentaje);

                    return totalImpuestoEntity;
                }
            }
            catch (Exception ex)
            {
                Interaction.MsgBox(ex.Message);
                return -1;
            }
        }

        /// <summary>
    /// Borra o marca como inactivo un item cargado en la orden de compra temporal usando Entity Framework
    /// </summary>
        public static void borraritemCargadoOC(int id_tmpOCItem_seleccionado = -1)
        {
            try
            {
                using (var context = new CentrexDbContext())
                {
                    if (id_tmpOCItem_seleccionado == -1)
                    {
                        // Borrar todos los items inactivos
                        var itemsInactivos = context.TmpOcItemEntity.AsQueryable().Where(t => t.Activo == false).ToList();


                        foreach (var item in itemsInactivos)
                            context.TmpOcItemEntity.Remove(item);
                    }
                    else
                    {
                        // Marcar como inactivo un item específico
                        var tmpItem = context.TmpOcItemEntity.AsQueryable().FirstOrDefault(t => t.IdTmpOcitem == id_tmpOCItem_seleccionado);

                        if (tmpItem is not null)
                        {
                            tmpItem.Activo = false;
                        }
                    }

                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Interaction.MsgBox(ex.Message.ToString());
            }
        }

        // ************************************ FUNCIONES DE ORDENES DE COMPRA ***************************
    }
}
