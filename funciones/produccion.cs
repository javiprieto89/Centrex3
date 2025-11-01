using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;


namespace Centrex.Funciones
{

    static class producciones
    {
        // ************************************ FUNCIONES DE PRODUCCION MIGRADAS A ENTITY FRAMEWORK ***************************


        /// <summary>
        /// Obtiene información de una producción específica o la más reciente
        /// </summary>
        public static ProduccionEntity info_produccion(int id_produccion = -1)
        {
            try
            {
                using (var context = new CentrexDbContext())
                {
                   var produccionEntity = new ProduccionEntity();

                    if (id_produccion == -1)
                    {
                        // Última producción
                        produccionEntity = context.ProduccionEntity.Include(p => p.IdProveedorNavigation).Include(p => p.IdUsuarioNavigation).OrderByDescending(p => p.IdProduccion).FirstOrDefault();
                    }
                    else
                    {
                         produccionEntity = context.ProduccionEntity.Include(p => p.IdProveedorNavigation).Include(p => p.IdUsuarioNavigation).FirstOrDefault(p => p.IdProduccion == id_produccion);
                    }

                    if (produccionEntity is not null)
                    {
                        return produccionEntity;
                    }
                    else
                    {
                        return null;
                    }
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener información de producción:" + ex.Message, "Centrex", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }


        /// <summary>
        /// Agrega una nueva producción
        /// </summary>
        public static bool addProduccion(ProduccionEntity p)
        {
            try
            {
                using (var context = new CentrexDbContext())
                {
                    var nuevaProduccionEntity = new ProduccionEntity()
                    {
                        IdProveedor = p.IdProveedor > 0 ? p.IdProveedor : 0,
                        FechaCarga = p.FechaCarga != default(DateOnly) ? p.FechaCarga : DateOnly.FromDateTime(DateTime.Now),
                        FechaEnvio = p.FechaEnvio,
                        FechaRecepcion = p.FechaRecepcion,
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
        public static bool updateProduccion(ProduccionEntity p, bool borra = false)
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
                            produccionEntity.IdProveedor = p.IdProveedor;
                            produccionEntity.FechaCarga = p.FechaCarga;

                            if (p.FechaEnvio != null)
                            {
                                produccionEntity.FechaEnvio = p.FechaEnvio;
                                produccionEntity.Enviado = p.Enviado;
                            }

                            // FechaRecepcion
                            if (p.FechaRecepcion != null)
                            {
                                produccionEntity.FechaRecepcion = p.FechaRecepcion;
                                produccionEntity.Recibido = p.Recibido;
                            }

                            // Notas
                            produccionEntity.Notas = p.Notas ?? string.Empty;

                            // Activo
                            produccionEntity.Activo = p.Activo;

                            // IdUsuario
                            produccionEntity.IdUsuario = p.IdUsuario > 0 ? p.IdUsuario : 0;
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
        public static bool borrarProduccion(ProduccionEntity p)
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
        public static bool addItemProducciontmp(ItemEntity i, int cantidad, int id_tmpProduccionEntityItem = -1)
        {
            // Usa usuario_logueado global
            return addItemProducciontmp(i, cantidad, usuario_logueado.IdUsuario, id_tmpProduccionEntityItem);
        }

        /// <summary>
        /// Sobrecarga con id_usuario explícito para compatibilidad interna
        /// </summary>
        public static bool addItemProducciontmp(ItemEntity i, int cantidad, int id_usuario, int id_tmpProduccionEntityItem = -1)
        {
            try
            {
                using (var context = new CentrexDbContext())
                {
                    if (id_tmpProduccionEntityItem == -1 | id_tmpProduccionEntityItem == 0)
                    {
                        // INSERT - Crear nuevo item temporal
                        var nuevoTmpItem = new TmpProduccionItemEntity()
                        {
                            IdItem = i.IdItem,
                            Cantidad = cantidad,
                            IdUsuario = id_usuario
                        };
                        context.TmpProduccionItemEntity.Add(nuevoTmpItem);
                    }
                    else
                    {
                        // UPDATE - Actualizar item temporal existente
                        var tmpItem = context.TmpProduccionItemEntity.FirstOrDefault(t => t.IdTmpProduccionItem == id_tmpProduccionEntityItem);

                        if (tmpItem is not null)
                        {
                            tmpItem.Cantidad = cantidad;
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
        public static bool addItemAsocProducciontmp(int id_tmpProduccionEntityItem, int id_item, int id_item_asoc, int cantidad_item_asoc_enviada, int id_tmpProduccionEntity_asocItem = -1)
        {
            // Usa usuario_logueado global
            return addItemAsocProducciontmpInternal(id_tmpProduccionEntityItem, id_item, id_item_asoc, cantidad_item_asoc_enviada, usuario_logueado.IdUsuario, id_tmpProduccionEntity_asocItem);
        }

        /// <summary>
        /// Sobrecarga con id_usuario explícito para compatibilidad interna
        /// </summary>
        private static bool addItemAsocProducciontmpInternal(int id_tmpProduccionEntityItem, int id_item, int id_item_asoc, int cantidad_item_asoc_enviada, int id_usuario, int id_tmpProduccionEntity_asocItem = -1)
        {
            try
            {
                using (var context = new CentrexDbContext())
                {
                    if (id_tmpProduccionEntity_asocItem == -1 | id_tmpProduccionEntity_asocItem == 0)
                    {
                        // INSERT - Crear nuevo item asociado temporal
                        var nuevoTmpAsocItem = new TmpProduccionAsocItemEntity()
                        {
                            IdItem = id_item,
                            IdItemAsoc = id_item_asoc,
                            CantidadItemAsocEnviada = cantidad_item_asoc_enviada,
                            IdUsuario = id_usuario
                        };
                        context.TmpProduccionAsocItemEntity.Add(nuevoTmpAsocItem);
                    }
                    else
                    {
                        // UPDATE - Actualizar item asociado temporal existente
                        var tmpAsocItem = context.TmpProduccionAsocItemEntity.FirstOrDefault(t => t.IdTmpProduccionItem == id_tmpProduccionEntity_asocItem);

                        if (tmpAsocItem is not null)
                        {
                            tmpAsocItem.CantidadItemAsocEnviada = cantidad_item_asoc_enviada;
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
        public static bool guardarProduccion(int id_produccion = 0)
        {
            // Usa usuario_logueado global
            return guardarProduccion(usuario_logueado.IdUsuario, id_produccion);
        }

        /// <summary>
        /// Sobrecarga con id_usuario explícito para compatibilidad interna
        /// </summary>
        //private static bool guardarProduccionEntity(int id_usuario, int id_produccion = 0)
        //{
        //    var p = new ProduccionEntity();

        //    if (id_produccion == 0)
        //    {
        //        p = info_produccion();
        //        id_produccion = p.IdProduccion;
        //    }

        //    try
        //    {
        //        using (var context = new CentrexDbContext())
        //        {
        //            // Obtener items temporales del usuario actual
        //            var tmpItems = context.TmpProduccionItemEntity.Where(t => t.IdUsuario == id_usuario).ToList();

        //            // 1. Actualizar items existentes
        //            foreach (var tmpItem in tmpItems)
        //            {
        //                var existingItem = context.ProduccionItemEntity.FirstOrDefault(pi => pi.IdProduccionItem == id_produccion && Operators.ConditionalCompareObjectEqual(pi.IdItem, ((dynamic)tmpItem).IdItem, false));

        //                if (existingItem is not null)
        //                {
        //                    existingItem.Cantidad = Conversions.ToInteger(((dynamic)tmpItem).Cantidad);
        //                }
        //            }

        //            // 2. Insertar nuevos items
        //            foreach (var tmpItem in tmpItems)
        //            {
        //                bool exists = context.ProduccionItemEntity.Any(pi => pi.IdProduccion == id_produccion && Operators.ConditionalCompareObjectEqual(pi.IdItem, ((dynamic)tmpItem).IdItem, false));

        //                if (!exists)
        //                {
        //                    var nuevoItem = new ProduccionItemEntity()
        //                    {
        //                        IdProduccion = id_produccion,
        //                        IdItem = Conversions.ToInteger(((dynamic)tmpItem).IdItem),
        //                        Cantidad = Conversions.ToInteger(((dynamic)tmpItem).Cantidad)
        //                    };
        //                    context.ProduccionItemEntity.Add(nuevoItem);
        //                }
        //            }

        //            context.SaveChanges();

        //            // 3. Procesar items asociados
        //            var tmpAsocItems = context.TmpProduccionAsocItemEntity.Where(t => t.IdUsuario == id_usuario).ToList();

        //            // Actualizar items asociados existentes
        //            foreach (var tmpAsocItem in tmpAsocItems)
        //            {
        //                var existingAsocItem = context.ProduccionAsocItemEntity.FirstOrDefault(pa => pa.IdProduccion == id_produccion && Operators.ConditionalCompareObjectEqual(pa.IdItem, ((dynamic)tmpAsocItem).IdItem, false) && Operators.ConditionalCompareObjectEqual(pa.IdItemAsoc, ((dynamic)tmpAsocItem).IdItemAsoc, false));

        //                if (existingAsocItem is not null)
        //                {
        //                    existingAsocItem.CantidadItemAsocEnviada = ((dynamic)tmpAsocItem).Cantidad;
        //                }
        //            }

        //            // Insertar nuevos items asociados
        //            foreach (var tmpAsocItem in tmpAsocItems)
        //            {
        //                bool exists = context.ProduccionAsocItemEntity.Any(pa => pa.IdProduccion == id_produccion && Operators.ConditionalCompareObjectEqual(pa.IdItem, ((dynamic)tmpAsocItem).IdItem, false) && Operators.ConditionalCompareObjectEqual(pa.IdItemAsoc, ((dynamic)tmpAsocItem).IdItemAsoc, false));

        //                if (!exists)
        //                {
        //                    var nuevoAsocItem = new ProduccionAsocItemEntity()
        //                    {
        //                        IdProduccion = id_produccion,
        //                        IdItem = Conversions.ToInteger(((dynamic)tmpAsocItem).IdItem),
        //                        IdItemAsoc = Conversions.ToInteger(((dynamic)tmpAsocItem).IdItemAsoc),
        //                        CantidadItemAsocEnviada = ((dynamic)tmpAsocItem).Cantidad
        //                    };
        //                    context.ProduccionAsocItemEntity.Add(nuevoAsocItem);
        //                }
        //            }

        //            context.SaveChanges();

        //            // 4. Eliminar items que ya no están en temporal
        //            // Obtener IDs de items en temporal
        //            var tmpItemIds = tmpItems.Select(t => t.IdItem).ToList();

        //            // Eliminar items de produccion_items que no están en temporal
        //            var itemsToDelete = context.ProduccionItemEntity.Where(pi => pi.IdProduccion == id_produccion && !tmpItemIds.Contains(pi.IdItem)).ToList();

        //            foreach (var itemToDelete in itemsToDelete)
        //                context.ProduccionItemEntity.Remove(itemToDelete);

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

        private static bool guardarProduccion(int id_usuario, int id_produccion = 0)
        {
            var p = new ProduccionEntity();

            if (id_produccion == 0)
            {
                p = info_produccion();
                id_produccion = p.IdProduccion;
            }

            try
            {
                using (var context = new CentrexDbContext())
                {
                    // Obtener items temporales del usuario actual
                    var tmpItems = context.TmpProduccionItemEntity
                        .Where(t => t.IdUsuario == id_usuario)
                        .ToList();

                    // 1. Actualizar o insertar items
                    foreach (var tmpItem in tmpItems)
                    {
                        var existingItem = context.ProduccionItemEntity
                            .FirstOrDefault(pi => pi.IdProduccion == id_produccion
                                               && pi.IdItem == tmpItem.IdItem);

                        if (existingItem != null)
                        {
                            // Actualizar item existente
                            existingItem.Cantidad = tmpItem.Cantidad;
                        }
                        else
                        {
                            // Insertar nuevo item
                            var nuevoItem = new ProduccionItemEntity
                            {
                                IdProduccion = id_produccion,
                                IdItem = (int)tmpItem.IdItem,
                                Cantidad = tmpItem.Cantidad
                            };
                            context.ProduccionItemEntity.Add(nuevoItem);
                        }
                    }

                    context.SaveChanges();

                    // 2. Procesar items asociados
                    var tmpAsocItems = context.TmpProduccionAsocItemEntity
                        .Where(t => t.IdUsuario == id_usuario)
                        .ToList();

                    foreach (var tmpAsocItem in tmpAsocItems)
                    {
                        var existingAsocItem = context.ProduccionAsocItemEntity
                            .FirstOrDefault(pa => pa.IdProduccion == id_produccion
                                               && pa.IdItem == tmpAsocItem.IdItem
                                               && pa.IdItemAsoc == tmpAsocItem.IdItemAsoc);

                        if (existingAsocItem != null)
                        {
                            // Actualizar item asociado existente
                            existingAsocItem.CantidadItemAsocEnviada = tmpAsocItem.CantidadItemAsocEnviada;
                        }
                        else
                        {
                            // Insertar nuevo item asociado
                            var nuevoAsocItem = new ProduccionAsocItemEntity
                            {
                                IdProduccion = id_produccion,
                                IdItem = tmpAsocItem.IdItem,
                                IdItemAsoc = tmpAsocItem.IdItemAsoc,
                                CantidadItemAsocEnviada = tmpAsocItem.CantidadItemAsocEnviada
                            };
                            context.ProduccionAsocItemEntity.Add(nuevoAsocItem);
                        }
                    }

                    context.SaveChanges();

                    // 3. Eliminar items que ya no están en temporal
                    var tmpItemIds = tmpItems.Select(t => t.IdItem).ToList();

                    var itemsToDelete = context.ProduccionItemEntity
                        .Where(pi => pi.IdProduccion == id_produccion
                                  && !tmpItemIds.Contains(pi.IdItem))
                        .ToList();

                    context.ProduccionItemEntity.RemoveRange(itemsToDelete);

                    // 4. Eliminar items asociados que ya no están en temporal
                    var tmpAsocItemPairs = tmpAsocItems
                        .Select(t => new { t.IdItem, t.IdItemAsoc })
                        .ToList();

                    var asocItemsToDelete = context.ProduccionAsocItemEntity
                        .Where(pa => pa.IdProduccion == id_produccion)
                        .ToList()
                        .Where(pa => !tmpAsocItemPairs.Any(tmp => tmp.IdItem == pa.IdItem
                                                                && tmp.IdItemAsoc == pa.IdItemAsoc))
                        .ToList();

                    context.ProduccionAsocItemEntity.RemoveRange(asocItemsToDelete);

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
            return produccion_a_produccionTmp(id_produccion, usuario_logueado.IdUsuario);
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
        public static double askCantidadCargadaProduccion(int id_item, int id = -1, int id_tmpProduccionEntityItem = -1)
        {
            // Usa usuario_logueado global
            return askCantidadCargadaProduccion(id_item, usuario_logueado.IdUsuario, id, id_tmpProduccionEntityItem);
        }

        /// <summary>
        /// Sobrecarga con id_usuario explícito para compatibilidad interna
        /// </summary>
        public static double askCantidadCargadaProduccion(int id_item, int id_usuario, int id = -1, int id_tmpProduccionEntityItem = -1)
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
                        query = query.Where(t => Operators.ConditionalCompareObjectEqual(t.IdTmpProduccionItem, id_tmpProduccionEntityItem, false));
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
                Interaction.MsgBox("Error al consultar cantidad de producción: " + ex.Message, MsgBoxStyle.Critical, "Centrex");
                return -1;
            }
        }

        /// <summary>
        /// Imprime reporte de producción
        /// NOTA: Las variables globales id y showrpt deben ser manejadas externamente
        /// </summary>
        public static void imprimirProduccion(int id_produccion, bool showrpt)
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
        //public static void borrarTmpProduccion(int id_usuario)
        //{
        //    try
        //    {
        //        using (var context = new CentrexDbContext())
        //        {
        //            // Borrar items asociados temporales
        //            var tmpAsocItems = context.TmpProduccionAsocItemEntity.Where(t => t.IdUsuario == id_usuario).ToList();

        //            foreach (var item in tmpAsocItems)
        //                context.TmpProduccionAsocItemEntity.Remove(item);

        //            // Borrar items temporales
        //            var tmpItems = context.TmpProduccionItemEntity.Where(t => t.IdUsuario == id_usuario).ToList();

        //            foreach (var item in tmpItems)
        //                context.TmpProduccionItemEntity.Remove(item);

        //            context.SaveChanges();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Interaction.MsgBox("Error al borrar tablas temporales de producción: " + ex.Message);
        //    }
        //}



        // ************************************ FUNCIONES DE PRODUCCION MIGRADAS A ENTITY FRAMEWORK ***************************

    }
}
