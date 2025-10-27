using System;
using System.Data;
using System.Linq;
using System.Xml.Linq;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;


namespace Centrex
{

    static class producciones
    {
        // ************************************ FUNCIONES DE PRODUCCION MIGRADAS A ENTITY FRAMEWORK ***************************

  
        /// <summary>
    /// Obtiene información de una producción específica o la más reciente
    /// </summary>
        public static produccion info_produccion(string id_produccion = "")
        {
            var tmp = new produccion();
            try
            {
                using (var context = new CentrexDbContext())
                {
                    ProduccionEntity produccionEntity = null;

                    if (string.IsNullOrWhiteSpace(id_produccion))
                    {
                        // Última producción
                        produccionEntity = context.ProduccionEntity.Include(p => p.IdProveedorNavigation).Include(p => p.IdUsuarioNavigation).OrderByDescending(p => p.IdProduccion).FirstOrDefault();



                    }
                    else
                    {
                        int idProd;
                        if (int.TryParse(id_produccion, out idProd))
                        {
                            produccionEntity = context.ProduccionEntity.Include(p => p.IdProveedorNavigation).Include(p => p.IdUsuarioNavigation).FirstOrDefault(p => p.IdProduccion == idProd);


                        }
                    }

                    if (produccionEntity is not null)
                    {
                        tmp.IdProduccion = produccionEntity.IdProduccion.ToString();
                        tmp.IdProveedor = produccionEntity.IdProveedor.ToString();
                        tmp.FechaCarga = produccionEntity.FechaCarga.ToString("dd/MM/yyyy");
                        tmp.FechaEnvio = produccionEntity.FechaEnvio.HasValue ? produccionEntity.FechaEnvio.Value.ToString("dd/MM/yyyy") : "";
                        tmp.FechaRecepcion = produccionEntity.FechaRecepcion.HasValue ? produccionEntity.FechaRecepcion.Value.ToString("dd/MM/yyyy") : "";

                        // ✅ Cambiado: Boolean simple
                        tmp.Enviado = Conversions.ToBoolean(produccionEntity.Enviado == true ? "1" : "0");
                        tmp.Recibido = Conversions.ToBoolean(produccionEntity.Recibido == true ? "1" : "0");

                        tmp.Notas = produccionEntity.Notas ?? "";
                        tmp.Activo = Conversions.ToBoolean(produccionEntity.Activo ? "1" : "0");
                        tmp.IdUsuario = produccionEntity.IdUsuario.ToString();
                    }
                    else
                    {
                        tmp.IdProduccion = Conversions.ToInteger("-1");
                    }
                }

                return tmp;
            }

            catch (Exception ex)
            {
                Interaction.MsgBox("Error al obtener información de producción: " + ex.Message, MsgBoxStyle.Critical);
                tmp.id_produccion = Conversions.ToInteger("-1");
                return tmp;
            }
        }


        /// <summary>
    /// Agrega una nueva producción
    /// </summary>
        public static bool addProduccionEntity(produccion p)
        {
            try
            {
                using (var context = new CentrexDbContext())
                {
                    var nuevaProduccionEntity = new ProduccionEntity()
                    {
                        IdProveedor = !string.IsNullOrEmpty(p.IdProveedor.ToString()) ? p.IdProveedor : default(int),
                        FechaCarga = !string.IsNullOrEmpty(p.FechaCarga) ? DateOnly.FromDateTime(DateTime.Parse(p.FechaCarga)) : default(DateOnly),
                        FechaEnvio = p.FechaEnvio is not null && !string.IsNullOrEmpty(p.FechaEnvio) ? DateOnly.FromDateTime(DateTime.Parse(p.FechaEnvio)) : default(DateOnly?),
                        FechaRecepcion = p.FechaRecepcion is not null && !string.IsNullOrEmpty(p.FechaRecepcion) ? DateOnly.FromDateTime(DateTime.Parse(p.FechaRecepcion)) : default(DateOnly?),
                        Enviado = p.Enviado == Conversions.ToBoolean("1") ? true : false,
                        Recibido = p.Recibido == Conversions.ToBoolean("1") ? true : false,
                        Notas = p.Notas ?? "",
                        Activo = true,
                        IdUsuario = !string.IsNullOrEmpty(p.IdUsuario.ToString()) ? p.IdUsuario : default(int)
                    };

                    context.ProduccionEntity.Add(nuevaProduccionEntity);
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
    /// Actualiza una producción existente
    /// </summary>
        public static bool updateProduccionEntity(produccion p, bool borra = false)
        {
            try
            {
                using (var context = new CentrexDbContext())
                {
                    int idProd = int.Parse(p.IdProduccion.ToString());
                    var produccionEntity = context.ProduccionEntity.FirstOrDefault(prod => prod.IdProduccion == idProd);

                    if (produccionEntity is not null)
                    {
                        if (borra)
                        {
                            // Solo marcar como inactivo
                            produccionEntity.Activo = false;
                        }
                        else
                        {
                            // Actualizar todos los campos
                            produccionEntity.IdProveedor = !string.IsNullOrEmpty(p.IdProveedor.ToString()) ? p.IdProveedor : default(int);
                            produccionEntity.FechaCarga = !string.IsNullOrEmpty(p.FechaCarga) ? DateOnly.FromDateTime(DateTime.Parse(p.FechaCarga)) : default(DateOnly);

                            if (p.FechaEnvio is not null && !string.IsNullOrEmpty(p.FechaEnvio))
                            {
                                produccionEntity.FechaEnvio = DateOnly.FromDateTime(DateTime.Parse(p.FechaEnvio));
                                produccionEntity.Enviado = p.Enviado == Conversions.ToBoolean("1") ? true : false;
                            }

                            if (p.FechaRecepcion is not null && !string.IsNullOrEmpty(p.FechaRecepcion))
                            {
                                produccionEntity.FechaRecepcion = DateOnly.FromDateTime(DateTime.Parse(p.FechaRecepcion));
                                produccionEntity.Recibido = p.Recibido == Conversions.ToBoolean("1") ? true : false;
                            }

                            produccionEntity.Notas = p.Notas ?? "";
                            produccionEntity.Activo = p.Activo == Conversions.ToBoolean("1") ? true : false;
                            produccionEntity.IdUsuario = !string.IsNullOrEmpty(p.IdUsuario.ToString()) ? p.IdUsuario : default(int);
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

        /// <summary>
    /// Elimina físicamente una producción
    /// </summary>
        public static bool borrarProduccionEntity(produccion p)
        {
            try
            {
                using (var context = new CentrexDbContext())
                {
                    int idProd = int.Parse(p.IdProduccion.ToString());
                    var produccionEntity = context.ProduccionEntity.FirstOrDefault(prod => prod.IdProduccion == idProd);

                    if (produccionEntity is not null)
                    {
                        context.ProduccionEntity.Remove(produccionEntity);
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
                Interaction.MsgBox(ex.Message.ToString());
                return false;
            }
        }


        /// <summary>
    /// Agrega o actualiza un item en la tabla temporal de producción usando Entity Framework
    /// </summary>
        public static bool addItemProduccionEntitytmp(item i, int cantidad, int id_tmpProduccionEntityItem = -1)
        {
            // Usa usuario_logueado global
            return addItemProduccionEntitytmp(i, cantidad, VariablesGlobales.usuario_logueado.IdUsuario, id_tmpProduccionEntityItem);
        }

        /// <summary>
    /// Sobrecarga con id_usuario explícito para compatibilidad interna
    /// </summary>
        public static bool addItemProduccionEntitytmp(item i, int cantidad, int id_usuario, int id_tmpProduccionEntityItem = -1)
        {
            try
            {
                using (var context = new CentrexDbContext())
                {
                    if (id_tmpProduccionEntityItem == -1 | id_tmpProduccionEntityItem == 0)
                    {
                        // INSERT - Crear nuevo item temporal
                        var nuevoTmpItem = new TmpProduccionEntityItemEntity()
                        {
                            IdItem = i.id_item,
                            cantidad = cantidad,
                            IdUsuario = id_usuario
                        };
                        context.TmpProduccionEntityItems.Add(nuevoTmpItem);
                    }
                    else
                    {
                        // UPDATE - Actualizar item temporal existente
                        var tmpItem = context.TmpProduccionEntityItems.FirstOrDefault(t => t.IdTmpProduccionEntityItem == id_tmpProduccionEntityItem);

                        if (tmpItem is not null)
                        {
                            tmpItem.cantidad = cantidad;
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
    /// Agrega o actualiza un item asociado en la tabla temporal de producción
    /// Reemplaza SP_insertAsocItemsProduccionEntityTMP y SP_updateAsocItemsProduccionEntityTMP
    /// </summary>
        public static bool addItemAsocProduccionEntitytmp(int id_tmpProduccionEntityItem, int id_item, int id_item_asoc, int cantidad_item_asoc_enviada, int id_tmpProduccionEntity_asocItem = -1)
        {
            // Usa usuario_logueado global
            return addItemAsocProduccionEntitytmpInternal(id_tmpProduccionEntityItem, id_item, id_item_asoc, cantidad_item_asoc_enviada, VariablesGlobales.usuario_logueado.IdUsuario, id_tmpProduccionEntity_asocItem);
        }

        /// <summary>
    /// Sobrecarga con id_usuario explícito para compatibilidad interna
    /// </summary>
        private static bool addItemAsocProduccionEntitytmpInternal(int id_tmpProduccionEntityItem, int id_item, int id_item_asoc, int cantidad_item_asoc_enviada, int id_usuario, int id_tmpProduccionEntity_asocItem = -1)
        {
            try
            {
                using (var context = new CentrexDbContext())
                {
                    if (id_tmpProduccionEntity_asocItem == -1 | id_tmpProduccionEntity_asocItem == 0)
                    {
                        // INSERT - Crear nuevo item asociado temporal
                        var nuevoTmpAsocItem = new TmpProduccionEntityAsocItemEntity()
                        {
                            IdItem = id_item,
                            IdItemAsoc = id_item_asoc,
                            Cantidad = cantidad_item_asoc_enviada,
                            IdUsuario = id_usuario
                        };
                        context.TmpProduccionEntityAsocItems.Add(nuevoTmpAsocItem);
                    }
                    else
                    {
                        // UPDATE - Actualizar item asociado temporal existente
                        var tmpAsocItem = context.TmpProduccionEntityAsocItems.FirstOrDefault(t => t.IdTmpProduccionEntityAsocItem == id_tmpProduccionEntity_asocItem);

                        if (tmpAsocItem is not null)
                        {
                            tmpAsocItem.Cantidad = cantidad_item_asoc_enviada;
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
    /// Guarda los items temporales en las tablas definitivas de producción
    /// Maneja tanto produccion_items como produccion_asocItems
    /// </summary>
        public static bool guardarProduccionEntity(int id_produccion = 0)
        {
            // Usa usuario_logueado global
            return guardarProduccionEntity(VariablesGlobales.usuario_logueado.IdUsuario, id_produccion);
        }

        /// <summary>
    /// Sobrecarga con id_usuario explícito para compatibilidad interna
    /// </summary>
        private static bool guardarProduccionEntity(int id_usuario, int id_produccion = 0)
        {
            var p = new produccion();

            if (id_produccion == 0)
            {
                p = info_produccion();
                id_produccion = p.id_produccion;
            }

            try
            {
                using (var context = new CentrexDbContext())
                {
                    // Obtener items temporales del usuario actual
                    var tmpItems = context.TmpProduccionItemEntity.Where(t => t.IdUsuario == id_usuario).ToList();

                    // 1. Actualizar items existentes
                    foreach (var tmpItem in tmpItems)
                    {
                        var existingItem = context.ProduccionItemEntity.FirstOrDefault(pi => pi.IdProduccion == id_produccion && Operators.ConditionalCompareObjectEqual(pi.IdItem, ((dynamic)tmpItem).IdItem, false));

                        if (existingItem is not null)
                        {
                            existingItem.Cantidad = Conversions.ToInteger(((dynamic)tmpItem).Cantidad);
                        }
                    }

                    // 2. Insertar nuevos items
                    foreach (var tmpItem in tmpItems)
                    {
                        bool exists = context.ProduccionItemEntity.Any(pi => pi.IdProduccion == id_produccion && Operators.ConditionalCompareObjectEqual(pi.IdItem, ((dynamic)tmpItem).IdItem, false));

                        if (!exists)
                        {
                            var nuevoItem = new ProduccionItemEntity()
                            {
                                IdProduccion = id_produccion,
                                IdItem = Conversions.ToInteger(((dynamic)tmpItem).IdItem),
                                Cantidad = Conversions.ToInteger(((dynamic)tmpItem).Cantidad)
                            };
                            context.ProduccionItemEntity.Add(nuevoItem);
                        }
                    }

                    context.SaveChanges();

                    // 3. Procesar items asociados
                    var tmpAsocItems = context.TmpProduccionAsocItemEntity.Where(t => t.IdUsuario == id_usuario).ToList();

                    // Actualizar items asociados existentes
                    foreach (var tmpAsocItem in tmpAsocItems)
                    {
                        var existingAsocItem = context.ProduccionAsocItemEntity.FirstOrDefault(pa => pa.IdProduccion == id_produccion && Operators.ConditionalCompareObjectEqual(pa.IdItem, ((dynamic)tmpAsocItem).IdItem, false) && Operators.ConditionalCompareObjectEqual(pa.IdItemAsoc, ((dynamic)tmpAsocItem).IdItemAsoc, false));

                        if (existingAsocItem is not null)
                        {
                            existingAsocItem.Cantidad = ((dynamic)tmpAsocItem).Cantidad;
                        }
                    }

                    // Insertar nuevos items asociados
                    foreach (var tmpAsocItem in tmpAsocItems)
                    {
                        bool exists = context.ProduccionAsocItemEntity.Any(pa => pa.IdProduccion == id_produccion && Operators.ConditionalCompareObjectEqual(pa.IdItem, ((dynamic)tmpAsocItem).IdItem, false) && Operators.ConditionalCompareObjectEqual(pa.IdItemAsoc, ((dynamic)tmpAsocItem).IdItemAsoc, false));

                        if (!exists)
                        {
                            var nuevoAsocItem = new ProduccionAsocItemEntity()
                            {
                                IdProduccion = id_produccion,
                                IdItem = Conversions.ToInteger(((dynamic)tmpAsocItem).IdItem),
                                IdItemAsoc = Conversions.ToInteger(((dynamic)tmpAsocItem).IdItemAsoc),
                                Cantidad = ((dynamic)tmpAsocItem).Cantidad
                            };
                            context.ProduccionAsocItemEntity.Add(nuevoAsocItem);
                        }
                    }

                    context.SaveChanges();

                    // 4. Eliminar items que ya no están en temporal
                    // Obtener IDs de items en temporal
                    var tmpItemIds = tmpItems.Select(t => t.IdItem).ToList();

                    // Eliminar items de produccion_items que no están en temporal
                    var itemsToDelete = context.ProduccionItemEntity.Where(pi => pi.IdProduccion == id_produccion && !tmpItemIds.Contains(pi.IdItem)).ToList();

                    foreach (var itemToDelete in itemsToDelete)
                        context.ProduccionItemEntity.Remove(itemToDelete);

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
    /// Copia items de producción a la tabla temporal para edición
    /// </summary>
        public static bool produccion_a_produccionTmp(int id_produccion)
        {
            // Usa usuario_logueado global
            return produccion_a_produccionTmp(id_produccion, VariablesGlobales.usuario_logueado.IdUsuario);
        }

        /// <summary>
    /// Sobrecarga con id_usuario explícito para compatibilidad interna
    /// </summary>
        private static bool produccion_a_produccionTmp(int id_produccion, int id_usuario)
        {
            try
            {
                using (var context = new CentrexDbContext())
                {
                    // Obtener items de la producción
                    var itemsProduccionEntity = context.ProduccionItemEntity.Where(pi => pi.IdProduccion == id_produccion).ToList();

                    // Copiar a tabla temporal
                    foreach (var item in itemsProduccionEntity)
                    {
                        var tmpItem = new TmpProduccionItemEntity()
                        {
                            IdItem = item.IdItem,
                            Cantidad = item.Cantidad,
                            IdUsuario = id_usuario
                        };
                        context.TmpProduccionItemEntity.Add(tmpItem);
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
    /// Obtiene la cantidad cargada de un item en producción temporal
    /// </summary>
        public static double askCantidadCargadaProduccionEntity(int id_item, int id = -1, int id_tmpProduccionEntityItem = -1)
        {
            // Usa usuario_logueado global
            return askCantidadCargadaProduccionEntity(id_item, VariablesGlobales.usuario_logueado.IdUsuario, id, id_tmpProduccionEntityItem);
        }

        /// <summary>
    /// Sobrecarga con id_usuario explícito para compatibilidad interna
    /// </summary>
        private static double askCantidadCargadaProduccionEntity(int id_item, int id_usuario, int id = -1, int id_tmpProduccionEntityItem = -1)
        {
            try
            {
                using (var context = new CentrexDbContext())
                {
                    var query = context.TmpProduccionItemEntity.Where(t => t.IdItem == id_item && t.IdUsuario == id_usuario);

                    if (id != -1)
                    {
                        // Filtro adicional si se necesita (aunque TmpProduccionEntityItems no tiene id_produccion)
                        // Este filtro podría no ser necesario en la versión temporal
                    }

                    if (id_tmpProduccionEntityItem != -1)
                    {
                        query = query.Where(t => Operators.ConditionalCompareObjectEqual(t.IdTmpProduccionEntityItem, id_tmpProduccionEntityItem, false));
                    }

                    var tmpItem = query.FirstOrDefault();

                    if (tmpItem is not null)
                    {
                        return (double)tmpItem.Cantidad;
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
    /// Imprime reporte de producción
    /// NOTA: Las variables globales id y showrpt deben ser manejadas externamente
    /// </summary>
        public static void imprimirProduccionEntity(int id_produccion, bool showrpt)
        {
            if (showrpt)
            {
                // La variable global 'id' debe ser asignada externamente antes de llamar al reporte
                // id = id_produccion
                // Aquí iría el código para mostrar el reporte
                // Dim frm As New frm_reportes(esPresupuesto, imprimeRemito)
                // frm.ShowDialog()
            }
        }

        /// <summary>
    /// Borra las tablas temporales de producción del usuario actual
    /// </summary>
        public static void borrarTmpProduccionEntity(int id_usuario)
        {
            try
            {
                using (var context = new CentrexDbContext())
                {
                    // Borrar items asociados temporales
                    var tmpAsocItems = context.TmpProduccionAsocItemEntity.Where(t => t.IdUsuario == id_usuario).ToList();

                    foreach (var item in tmpAsocItems)
                        context.TmpProduccionAsocItemEntity.Remove(item);

                    // Borrar items temporales
                    var tmpItems = context.TmpProduccionItemEntity.Where(t => t.IdUsuario == id_usuario).ToList();

                    foreach (var item in tmpItems)
                        context.TmpProduccionItemEntity.Remove(item);

                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Interaction.MsgBox("Error al borrar tablas temporales de producción: " + ex.Message);
            }
        }



        // ************************************ FUNCIONES DE PRODUCCION MIGRADAS A ENTITY FRAMEWORK ***************************

    }
}
