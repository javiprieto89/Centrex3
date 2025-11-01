using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Centrex.Funciones
{

    static class ordenesCompras
    {
        // ************************************ FUNCIONES DE ORDENES DE COMPRA MIGRADAS A ENTITY FRAMEWORK ***************************

        /// <summary>
        /// Obtiene información de una orden de compra usando Entity Framework
        /// </summary>
        public static OrdenCompraEntity info_ordenCompra(string IdOrdenCompra = "")
        {
            var tmp = new OrdenCompraEntity();
            try
            {
                using (var context = new CentrexDbContext())
                {
                    OrdenCompraEntity ordenEntity = null;

                    if (string.IsNullOrEmpty(IdOrdenCompra))
                    {
                        // Obtener la última orden de compra
                        ordenEntity = context.OrdenCompraEntity.OrderByDescending(o => o.IdOrdenCompra).FirstOrDefault();

                    }
                    else
                    {
                        // Obtener orden específica
                        int idOrden = Conversions.ToInteger(IdOrdenCompra);
                        ordenEntity = context.OrdenCompraEntity.FirstOrDefault(o => o.IdOrdenCompra == idOrden);
                    }

                    if (ordenEntity is not null)
                    {
                        // Calcular Subtotal, Iva y Total desde los items
                        var items = context.OrdenCompraItemEntity.Where(i => i.IdOrdenCompra == ordenEntity.IdOrdenCompra).ToList();


                        decimal Subtotal = items.Sum(i => i.Cantidad * i.Precio);
                        decimal Iva = 0m;
                        decimal Total = Subtotal + Iva;

                        tmp.IdOrdenCompra = ordenEntity.IdOrdenCompra;
                        tmp.IdProveedor = ordenEntity.IdProveedor;
                        tmp.FechaCarga = ordenEntity.FechaCarga;
                        tmp.FechaComprobante = ordenEntity.FechaComprobante;
                        tmp.FechaRecepcion = ordenEntity.FechaRecepcion;
                        tmp.Subtotal = ordenEntity.Subtotal;
                        tmp.Iva = ordenEntity.Iva;
                        tmp.Total = ordenEntity.Total;
                        tmp.Recibido = ordenEntity.Recibido;
                        tmp.Notas = ordenEntity.Notas ?? "";
                        tmp.Activo = ordenEntity.Activo;
                    }
                    else
                    {
                        tmp.IdOrdenCompra = -1;
                    }

                    return tmp;
                }
            }
            catch (Exception ex)
            {
                Interaction.MsgBox(ex.Message.ToString());
                tmp.IdOrdenCompra = -1;
                return tmp;
            }
        }

        /// <summary>
        /// Agrega una nueva orden de compra usando Entity Framework
        /// </summary>
        public static bool addOrdenCompra(OrdenCompraEntity oc)
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

                    // Usar FechaComprobante como fecha principal
                    if (oc.FechaComprobante != DateOnly.MinValue)
                    {
                        nuevaOrden.FechaComprobante = oc.FechaComprobante;
                    }
                    else if (oc.FechaCarga != DateOnly.MinValue)
                    {
                        nuevaOrden.FechaComprobante = oc.FechaCarga;
                    }
                    else
                    {
                        nuevaOrden.FechaComprobante = DateOnly.FromDateTime(DateTime.Now);
                    }

                    nuevaOrden.Notas = oc.Notas ?? string.Empty;
                    nuevaOrden.Activo = true;

                    nuevaOrden.Notas = oc.Notas ?? string.Empty;
                    nuevaOrden.Activo = true;

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
        public static bool updateOrdenCompra(OrdenCompraEntity oc, bool borra = false)
        {
            try
            {
                using (var context = new CentrexDbContext())
                {
                    int idOrden = oc.IdOrdenCompra;
                    var ordenEntity = context.OrdenCompraEntity.FirstOrDefault(o => o.IdOrdenCompra == idOrden);

                    if (ordenEntity is null)
                    {
                        Interaction.MsgBox("Orden de compra no encontrada");
                        return false;
                    }

                    if (borra)
                    {
                        // Baja lógica
                        ordenEntity.Activo = false;
                    }
                    else
                    {
                        // Actualización
                        if (!string.IsNullOrEmpty(oc.IdProveedor.ToString()) && oc.IdProveedor != Conversions.ToDouble("0"))
                        {
                            ordenEntity.IdProveedor = oc.IdProveedor;
                        }


                        ordenEntity.FechaComprobante = oc.FechaComprobante;


                        ordenEntity.Notas = oc.Notas ?? "";

                        if (!string.IsNullOrEmpty(Conversions.ToString(oc.Activo)))
                        {
                            ordenEntity.Activo = oc.Activo == Conversions.ToBoolean("1");
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
        public static bool borrarOrdenCompra(OrdenCompraEntity oc)
        {
            try
            {
                using (var context = new CentrexDbContext())
                {
                    int idOrden = oc.IdOrdenCompra;

                    // Borrar items temporales
                    var tmpItemEntity = context.TmpOcItemEntity.AsQueryable().Where(t => t.IdOrdenCompra == idOrden).ToList();

                    foreach (var item in tmpItemEntity)
                        context.TmpOcItemEntity.Remove((TmpOcItemEntity)item);

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
        public static bool addItemOCtmp(ItemEntity i, int Cantidad, decimal Precio, int id_tmpOCItem = -1)
        {
            try
            {
                using (var context = new CentrexDbContext())
                {
                    if (id_tmpOCItem == -1 | id_tmpOCItem == 0)
                    {
                        // Insertar nuevo item temporal
                        var nuevoTmpItem = new TmpOcItemEntity();
                        nuevoTmpItem.IdItem = i.IdItem;
                        nuevoTmpItem.Cantidad = Cantidad;
                        nuevoTmpItem.Precio = Precio;
                        nuevoTmpItem.Activo = true;
                        nuevoTmpItem.Descript = i.Descript;
                        nuevoTmpItem.CantidadRecibida = 0;

                        context.TmpOcItemEntity.Add(nuevoTmpItem);
                    }
                    else
                    {
                        // Actualizar item temporal existente
                        var tmpItem = context.TmpOcItemEntity.AsQueryable().FirstOrDefault(t => t.IdTmpOcitem == id_tmpOCItem);
                        if (tmpItem is not null)
                        {
                            tmpItem.IdItem = i.IdItem;
                            tmpItem.Cantidad = Cantidad;
                            tmpItem.Precio = (decimal)Precio;
                            tmpItem.Descript = i.Descript;
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
        //public static bool guardarOrdenCompra(int IdOrdenCompra = 0)
        //{
        //    try
        //    {
        //        using (var context = new CentrexDbContext())
        //        {
        //            // Obtener el último pedido si no se especifica
        //            if (IdOrdenCompra == 0)
        //            {
        //                var oc = info_ordenCompra();
        //                IdOrdenCompra = oc.IdOrdenCompra;
        //            }

        //            // Obtener items temporales activos
        //            var tmpItemEntity = context.TmpOcItemEntity.AsQueryable().Where(t => t.Activo == true).ToList();


        //            foreach (var tmpItem in tmpItemEntity)
        //            {
        //                bool existe = false;

        //                // Si tiene IdOcItem, buscar y actualizar
        //                if (tmpItem.IdOcItem.HasValue && tmpItem.IdOcItem.Value > 0)
        //                {
        //                    var itemExistente = context.OrdenCompraItemEntity.FirstOrDefault(oi => oi.IdOcItem == tmpItem.IdOcItem.Value && oi.IdOrdenCompra == IdOrdenCompra);

        //                    if (itemExistente is not null)
        //                    {
        //                        itemExistente.IdItem = tmpItem.IdItem.HasValue ? tmpItem.IdItem.Value : 0;
        //                        itemExistente.Cantidad = tmpItem.Cantidad;
        //                        itemExistente.Precio = tmpItem.Precio;
        //                        itemExistente.Descript = tmpItem.Descript;
        //                        existe = true;
        //                    }
        //                }

        //                // Si no existe, crear nuevo
        //                if (!existe)
        //                {
        //                    var nuevoItem = new OrdenCompraItemEntity();
        //                    nuevoItem.IdOrdenCompra = IdOrdenCompra;
        //                    nuevoItem.IdItem = tmpItem.IdItem.HasValue ? tmpItem.IdItem.Value : 0;
        //                    nuevoItem.Cantidad = tmpItem.Cantidad;
        //                    nuevoItem.Precio = tmpItem.Precio;
        //                    nuevoItem.Descript = tmpItem.Descript;

        //                    context.OrdenCompraItemEntity.Add(nuevoItem);
        //                }
        //            }

        //            context.SaveChanges();

        //            // Borrar items que están en la BD pero marcados como inactivos en temporal
        //            var tmpItemEntityInactivos = context.TmpOcItemEntity.AsQueryable().Where(t => (t.Activo is { } arg1 ? arg1 == false : (bool?)null) && t.IdOrdenCompra == IdOrdenCompra).ToList();


        //            foreach (var TmpOcItemEntity in tmpItemEntityInactivos)
        //            {
        //                if (Conversions.ToBoolean(((dynamic)tmpInactivo).id_ocItem.HasValue))
        //                {
        //                    var itemABorrar = context.OrdenCompraItemEntity.FirstOrDefault(oi => Operators.ConditionalCompareObjectEqual(oi.IdItemNavigation, tmpInactivo.id_ocItem.Value, false));

        //                    if (itemABorrar is not null)
        //                    {
        //                        context.OrdenCompraItemEntity.Remove(itemABorrar);
        //                    }
        //                }
        //            }

        //            context.SaveChanges();
        //            return true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Interaction.MsgBox(ex.Message);
        //        return false;
        //    }
        //}

        public static bool guardarOrdenCompra(int IdOrdenCompra = 0)
        {
            try
            {
                using (var context = new CentrexDbContext())
                {
                    // Obtener el último pedido si no se especifica
                    if (IdOrdenCompra == 0)
                    {
                        var oc = info_ordenCompra();
                        IdOrdenCompra = oc.IdOrdenCompra;
                    }

                    // ===================================================
                    // PASO 1: Guardar/actualizar items ACTIVOS
                    // ===================================================
                    var tmpItemActivos = context.TmpOcItemEntity
                        .Where(t => t.Activo == true)
                        .ToList();

                    foreach (var tmpItem in tmpItemActivos)
                    {
                        bool existe = false;

                        // Si tiene IdOcItem, buscar y actualizar
                        if (tmpItem.IdOcItem.HasValue && tmpItem.IdOcItem.Value > 0)
                        {
                            var itemExistente = context.OrdenCompraItemEntity
                                .FirstOrDefault(oi => oi.IdOcItem == tmpItem.IdOcItem.Value
                                                   && oi.IdOrdenCompra == IdOrdenCompra);

                            if (itemExistente != null)
                            {
                                itemExistente.IdItem = tmpItem.IdItem ?? 0;
                                itemExistente.Cantidad = tmpItem.Cantidad;
                                itemExistente.Precio = tmpItem.Precio;
                                itemExistente.Descript = tmpItem.Descript;
                                existe = true;
                            }
                        }

                        // Si no existe, crear nuevo
                        if (!existe)
                        {
                            var nuevoItem = new OrdenCompraItemEntity
                            {
                                IdOrdenCompra = IdOrdenCompra,
                                IdItem = tmpItem.IdItem ?? 0,
                                Cantidad = tmpItem.Cantidad,
                                Precio = tmpItem.Precio,
                                Descript = tmpItem.Descript
                            };
                            context.OrdenCompraItemEntity.Add(nuevoItem);
                        }
                    }

                    context.SaveChanges();

                    // ===================================================
                    // PASO 2: Borrar items INACTIVOS de la BD
                    // ===================================================
                    var tmpItemInactivos = context.TmpOcItemEntity
                        .Where(t => t.Activo == false && t.IdOrdenCompra == IdOrdenCompra)
                        .ToList();

                    foreach (var tmpInactivo in tmpItemInactivos)
                    {
                        // Si el item temporal tiene un ID de item de BD, borrarlo
                        if (tmpInactivo.IdOcItem.HasValue && tmpInactivo.IdOcItem.Value > 0)
                        {
                            var itemABorrar = context.OrdenCompraItemEntity
                                .FirstOrDefault(oi => oi.IdOcItem == tmpInactivo.IdOcItem.Value);

                            if (itemABorrar != null)
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
        public static bool oc_a_ocTmp(int IdOrdenCompra)
        {
            try
            {
                using (var context = new CentrexDbContext())
                {
                    var items = context.OrdenCompraItemEntity.Where(i => i.IdOrdenCompra == IdOrdenCompra).ToList();


                    foreach (var item in items)
                    {
                        var tmpItem = new TmpOcItemEntity();
                        tmpItem.IdOcItem = item.IdOcItem;
                        tmpItem.IdOrdenCompra = item.IdOrdenCompra;
                        tmpItem.IdItem = item.IdItem;
                        tmpItem.Cantidad = item.Cantidad;
                        tmpItem.Precio = item.Precio;
                        tmpItem.Activo = true;
                        tmpItem.Descript = item.Descript;
                        tmpItem.CantidadRecibida = item.CantidadRecibida;

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
        /// Obtiene la Cantidad cargada de un item en la tabla temporal usando Entity Framework
        /// </summary>
        public static double askCantidadCargadaOC(int IdItem, int id = -1, int id_tmpOCItem = -1)
        {
            try
            {
                using (var context = new CentrexDbContext())
                {
                    var query = context.TmpOcItemEntity.AsQueryable();

                    // Aplicar filtros
                    query = query.Where(t => t.IdItem == IdItem);

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
                        return tmpItem.Cantidad;
                    }
                    else
                    {
                        return -1;
                    }
                }
            }
            catch (Exception ex)
            {
                Interaction.MsgBox("Error al consultar orden de compra: " + ex.Message, MsgBoxStyle.Critical, "Centrex");
                return -1;
            }
        }

        /// <summary>
        /// Obtiene el Precio cargado de un item en la tabla temporal usando Entity Framework
        /// </summary>
        public static double askPrecioCargadoOC(int IdItem, int id = -1, int id_tmpOCItem = -1)
        {
            try
            {
                using (var context = new CentrexDbContext())
                {
                    var query = context.TmpOcItemEntity.AsQueryable();

                    // Aplicar filtros
                    query = query.Where(t => t.IdItem == IdItem);

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
                Interaction.MsgBox("Error al consultar orden de compra: " + ex.Message, MsgBoxStyle.Critical, "Centrex");
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
                    var tmpItemEntity = context.TmpOcItemEntity.AsQueryable().Where(t => t.Activo == true).OrderBy(t => t.IdTmpOcitem).ToList();



                    double Subtotal = 0d;
                    double totalImpuestoEntity = 0d;
                    double Total = 0d;

                    // Calcular Subtotal
                    foreach (var tmpItem in tmpItemEntity)
                    {
                        decimal Cantidad = Conversions.ToDecimal(((dynamic)tmpItem).Cantidad);
                        decimal Precio = Conversions.ToDecimal(((dynamic)tmpItem).Precio);
                        Subtotal += (double)(Cantidad * Precio);
                    }

                    txt_totalOriginal.Text = Subtotal.ToString();

                    // Calcular impuestos
                    foreach (var tmpItem in tmpItemEntity)
                    {
                        decimal Precio = Conversions.ToDecimal(((dynamic)tmpItem).Precio);
                        decimal Cantidad = Conversions.ToDecimal(((dynamic)tmpItem).Cantidad);
                        double impuestosItem = ordenesCompras.calculaImpuestoEntityItem(((dynamic)tmpItem).id_tmpOCItem.ToString(), Conversions.ToBoolean(((dynamic)tmpItem).IdItem.HasValue) ? ((dynamic)tmpItem).IdItem.Value.ToString() : "0");
                        double totalImpuestoItem = impuestosItem * (double)(Precio * Cantidad) / 100d;
                        totalImpuestoEntity += totalImpuestoItem;
                    }

                    Total = Subtotal + totalImpuestoEntity;

                    txt_subTotal.Text = Math.Round(Subtotal, 2).ToString();
                    txt_impuestos.Text = Math.Round(totalImpuestoEntity, 2).ToString();
                    txt_total.Text = Math.Round(Total, 2).ToString();

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
                        row["ID"] = ((dynamic)tmpItem).id_tmpOCItem.ToString() + "-" + (Conversions.ToBoolean(((dynamic)tmpItem).IdItem.HasValue) ? ((dynamic)tmpItem).IdItem.Value.ToString() : "0");
                        row["id_OCItem"] = Conversions.ToBoolean(((dynamic)tmpItem).id_ocItem.HasValue) ? ((dynamic)tmpItem).id_ocItem.Value.ToString() : "";

                        // Obtener descripción del item o usar la descripción temporal
                        string descripcion = Conversions.ToString(((dynamic)tmpItem).Descript);
                        if (Conversions.ToBoolean(((dynamic)tmpItem).IdItem.HasValue && Operators.ConditionalCompareObjectGreater(((dynamic)tmpItem).IdItem.Value, 0, false)))
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
        public static double calculaImpuestoEntityItem(string id_tmpOCItem, string IdItem)
        {
            try
            {
                using (var context = new CentrexDbContext())
                {
                    int idItemInt = Conversions.ToInteger(IdItem);
                    int idTmpInt = Conversions.ToInteger(id_tmpOCItem);

                    // Verificar que el item temporal existe
                    var tmpItem = context.TmpOcItemEntity
                    .AsQueryable()
                    .FirstOrDefault(t =>
                        t.IdTmpOcitem == idTmpInt
                        && t.IdItem == idItemInt
                        && (t.Activo.HasValue && t.Activo.Value));


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

