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
                        produccionEntity = context.Produccion.Include(p => p.Proveedor).Include(p => p.Usuario).OrderByDescending(p => p.IdProduccion).FirstOrDefault();



                    }
                    else
                    {
                        int idProd;
                        if (int.TryParse(id_produccion, out idProd))
                        {
                            produccionEntity = context.Produccion.Include(p => p.Proveedor).Include(p => p.Usuario).FirstOrDefault(p => p.IdProduccion == idProd);


                        }
                    }

                    if (produccionEntity is not null)
                    {
                        tmp.id_produccion = produccionEntity.IdProduccion.ToString();
                        tmp.IdProveedor = produccionEntity.IdProveedor.ToString();
                        tmp.fecha_carga = produccionEntity.FechaCarga.ToString("dd/MM/yyyy");
                        tmp.fecha_envio = produccionEntity.FechaEnvio.HasValue ? produccionEntity.FechaEnvio.Value.ToString("dd/MM/yyyy") : "";
                        tmp.fecha_recepcion = produccionEntity.FechaRecepcion.HasValue ? produccionEntity.FechaRecepcion.Value.ToString("dd/MM/yyyy") : "";

                        // ✅ Cambiado: Boolean simple
                        tmp.enviado = Conversions.ToBoolean(produccionEntity.enviado == true ? "1" : "0");
                        tmp.recibido = Conversions.ToBoolean(produccionEntity.recibido == true ? "1" : "0");

                        tmp.notas = produccionEntity.notas ?? "";
                        tmp.activo = Conversions.ToBoolean(produccionEntity.activo ? "1" : "0");
                        tmp.id_usuario = produccionEntity.IdUsuario.ToString();
                    }
                    else
                    {
                        tmp.id_produccion = Conversions.ToInteger("-1");
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
        public static bool addProduccion(produccion p)
        {
            try
            {
                using (var context = new CentrexDbContext())
                {
                    var nuevaProduccion = new ProduccionEntity()
                    {
                        IdProveedor = !string.IsNullOrEmpty(p.IdProveedor.ToString()) ? p.IdProveedor : default(int?),
                        FechaCarga = !string.IsNullOrEmpty(p.fecha_carga) ? DateTime.Parse(p.fecha_carga) : default(DateTime?),
                        FechaEnvio = p.fecha_envio is not null && !string.IsNullOrEmpty(p.fecha_envio) ? DateTime.Parse(p.fecha_envio) : default(DateTime?),
                        FechaRecepcion = p.fecha_recepcion is not null && !string.IsNullOrEmpty(p.fecha_recepcion) ? DateTime.Parse(p.fecha_recepcion) : default(DateTime?),
                        enviado = p.enviado == Conversions.ToBoolean("1") ? true : false,
                        recibido = p.recibido == Conversions.ToBoolean("1") ? true : false,
                        notas = p.notas ?? "",
                        activo = true,
                        IdUsuario = !string.IsNullOrEmpty(p.id_usuario.ToString()) ? p.id_usuario : default(int?)
                    };

                    context.Produccion.Add(nuevaProduccion);
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
        public static bool updateProduccion(produccion p, bool borra = false)
        {
            try
            {
                using (var context = new CentrexDbContext())
                {
                    int idProd = int.Parse(p.id_produccion.ToString());
                    var produccionEntity = context.Produccion.FirstOrDefault(prod => prod.IdProduccion == idProd);

                    if (produccionEntity is not null)
                    {
                        if (borra)
                        {
                            // Solo marcar como inactivo
                            produccionEntity.activo = false;
                        }
                        else
                        {
                            // Actualizar todos los campos
                            produccionEntity.IdProveedor = !string.IsNullOrEmpty(p.IdProveedor.ToString()) ? p.IdProveedor : default(int?);
                            produccionEntity.FechaCarga = !string.IsNullOrEmpty(p.fecha_carga) ? DateTime.Parse(p.fecha_carga) : default(DateTime?);

                            if (p.fecha_envio is not null && !string.IsNullOrEmpty(p.fecha_envio))
                            {
                                produccionEntity.FechaEnvio = DateTime.Parse(p.fecha_envio);
                                produccionEntity.enviado = p.enviado == Conversions.ToBoolean("1") ? true : false;
                            }

                            if (p.fecha_recepcion is not null && !string.IsNullOrEmpty(p.fecha_recepcion))
                            {
                                produccionEntity.FechaRecepcion = DateTime.Parse(p.fecha_recepcion);
                                produccionEntity.recibido = p.recibido == Conversions.ToBoolean("1") ? true : false;
                            }

                            produccionEntity.notas = p.notas ?? "";
                            produccionEntity.activo = p.activo == Conversions.ToBoolean("1") ? true : false;
                            produccionEntity.IdUsuario = !string.IsNullOrEmpty(p.id_usuario.ToString()) ? p.id_usuario : default(int?);
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
        public static bool borrarProduccion(produccion p)
        {
            try
            {
                using (var context = new CentrexDbContext())
                {
                    int idProd = int.Parse(p.id_produccion.ToString());
                    var produccionEntity = context.Produccion.FirstOrDefault(prod => prod.IdProduccion == idProd);

                    if (produccionEntity is not null)
                    {
                        context.Produccion.Remove(produccionEntity);
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
        public static bool addItemProducciontmp(item i, int cantidad, int id_tmpProduccionItem = -1)
        {
            // Usa usuario_logueado global
            return addItemProducciontmp(i, cantidad, VariablesGlobales.usuario_logueado.IdUsuario, id_tmpProduccionItem);
        }

        /// <summary>
    /// Sobrecarga con id_usuario explícito para compatibilidad interna
    /// </summary>
        public static bool addItemProducciontmp(item i, int cantidad, int id_usuario, int id_tmpProduccionItem = -1)
        {
            try
            {
                using (var context = new CentrexDbContext())
                {
                    if (id_tmpProduccionItem == -1 | id_tmpProduccionItem == 0)
                    {
                        // INSERT - Crear nuevo item temporal
                        var nuevoTmpItem = new TmpProduccionItemEntity()
                        {
                            IdItem = i.id_item,
                            cantidad = cantidad,
                            IdUsuario = id_usuario
                        };
                        context.TmpProduccionItems.Add(nuevoTmpItem);
                    }
                    else
                    {
                        // UPDATE - Actualizar item temporal existente
                        var tmpItem = context.TmpProduccionItems.FirstOrDefault(t => t.IdTmpProduccionItem == id_tmpProduccionItem);

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
    /// Reemplaza SP_insertAsocItemsProduccionTMP y SP_updateAsocItemsProduccionTMP
    /// </summary>
        public static bool addItemAsocProducciontmp(int id_tmpProduccionItem, int id_item, int id_item_asoc, int cantidad_item_asoc_enviada, int id_tmpProduccion_asocItem = -1)
        {
            // Usa usuario_logueado global
            return addItemAsocProducciontmpInternal(id_tmpProduccionItem, id_item, id_item_asoc, cantidad_item_asoc_enviada, VariablesGlobales.usuario_logueado.IdUsuario, id_tmpProduccion_asocItem);
        }

        /// <summary>
    /// Sobrecarga con id_usuario explícito para compatibilidad interna
    /// </summary>
        private static bool addItemAsocProducciontmpInternal(int id_tmpProduccionItem, int id_item, int id_item_asoc, int cantidad_item_asoc_enviada, int id_usuario, int id_tmpProduccion_asocItem = -1)
        {
            try
            {
                using (var context = new CentrexDbContext())
                {
                    if (id_tmpProduccion_asocItem == -1 | id_tmpProduccion_asocItem == 0)
                    {
                        // INSERT - Crear nuevo item asociado temporal
                        var nuevoTmpAsocItem = new TmpProduccionAsocItemEntity()
                        {
                            IdItem = id_item,
                            IdItemAsoc = id_item_asoc,
                            Cantidad = cantidad_item_asoc_enviada,
                            IdUsuario = id_usuario
                        };
                        context.TmpProduccionAsocItems.Add(nuevoTmpAsocItem);
                    }
                    else
                    {
                        // UPDATE - Actualizar item asociado temporal existente
                        var tmpAsocItem = context.TmpProduccionAsocItems.FirstOrDefault(t => t.IdTmpProduccionAsocItem == id_tmpProduccion_asocItem);

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
        public static bool guardarProduccion(int id_produccion = 0)
        {
            // Usa usuario_logueado global
            return guardarProduccion(VariablesGlobales.usuario_logueado.IdUsuario, id_produccion);
        }

        /// <summary>
    /// Sobrecarga con id_usuario explícito para compatibilidad interna
    /// </summary>
        private static bool guardarProduccion(int id_usuario, int id_produccion = 0)
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
                    var tmpItems = context.TmpProduccionItems.Where(t => t.IdUsuario == id_usuario).ToList();

                    // 1. Actualizar items existentes
                    foreach (var tmpItem in tmpItems)
                    {
                        var existingItem = context.ProduccionItems.FirstOrDefault(pi => pi.id_produccion == id_produccion && Operators.ConditionalCompareObjectEqual(pi.id_item, ((dynamic)tmpItem).IdItem, false));

                        if (existingItem is not null)
                        {
                            existingItem.cantidad = Conversions.ToInteger(((dynamic)tmpItem).Cantidad);
                        }
                    }

                    // 2. Insertar nuevos items
                    foreach (var tmpItem in tmpItems)
                    {
                        bool exists = context.ProduccionItems.Any(pi => pi.id_produccion == id_produccion && Operators.ConditionalCompareObjectEqual(pi.id_item, ((dynamic)tmpItem).IdItem, false));

                        if (!exists)
                        {
                            var nuevoItem = new ProduccionItemEntity()
                            {
                                id_produccion = id_produccion,
                                id_item = Conversions.ToInteger(((dynamic)tmpItem).IdItem),
                                cantidad = Conversions.ToInteger(((dynamic)tmpItem).Cantidad)
                            };
                            context.ProduccionItems.Add(nuevoItem);
                        }
                    }

                    context.SaveChanges();

                    // 3. Procesar items asociados
                    var tmpAsocItems = context.TmpProduccionAsocItems.Where(t => t.IdUsuario == id_usuario).ToList();

                    // Actualizar items asociados existentes
                    foreach (var tmpAsocItem in tmpAsocItems)
                    {
                        var existingAsocItem = context.ProduccionAsocItems.FirstOrDefault(pa => pa.id_produccion == id_produccion && Operators.ConditionalCompareObjectEqual(pa.id_item, ((dynamic)tmpAsocItem).IdItem, false) && Operators.ConditionalCompareObjectEqual(pa.id_item_asoc, ((dynamic)tmpAsocItem).IdItemAsoc, false));

                        if (existingAsocItem is not null)
                        {
                            existingAsocItem.cantidad = ((dynamic)tmpAsocItem).Cantidad;
                        }
                    }

                    // Insertar nuevos items asociados
                    foreach (var tmpAsocItem in tmpAsocItems)
                    {
                        bool exists = context.ProduccionAsocItems.Any(pa => pa.id_produccion == id_produccion && Operators.ConditionalCompareObjectEqual(pa.id_item, ((dynamic)tmpAsocItem).IdItem, false) && Operators.ConditionalCompareObjectEqual(pa.id_item_asoc, ((dynamic)tmpAsocItem).IdItemAsoc, false));

                        if (!exists)
                        {
                            var nuevoAsocItem = new ProduccionAsocItemEntity()
                            {
                                id_produccion = id_produccion,
                                id_item = Conversions.ToInteger(((dynamic)tmpAsocItem).IdItem),
                                id_item_asoc = Conversions.ToInteger(((dynamic)tmpAsocItem).IdItemAsoc),
                                cantidad = ((dynamic)tmpAsocItem).Cantidad
                            };
                            context.ProduccionAsocItems.Add(nuevoAsocItem);
                        }
                    }

                    context.SaveChanges();

                    // 4. Eliminar items que ya no están en temporal
                    // Obtener IDs de items en temporal
                    var tmpItemIds = tmpItems.Select(t => t.IdItem).ToList();

                    // Eliminar items de produccion_items que no están en temporal
                    var itemsToDelete = context.ProduccionItems.Where(pi => pi.id_produccion == id_produccion && !tmpItemIds.Contains(pi.id_item)).ToList();

                    foreach (var itemToDelete in itemsToDelete)
                        context.ProduccionItems.Remove(itemToDelete);

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
                    var itemsProduccion = context.ProduccionItems.Where(pi => pi.id_produccion == id_produccion).ToList();

                    // Copiar a tabla temporal
                    foreach (var item in itemsProduccion)
                    {
                        var tmpItem = new TmpProduccionItemEntity()
                        {
                            IdItem = item.id_item,
                            cantidad = item.cantidad,
                            IdUsuario = id_usuario
                        };
                        context.TmpProduccionItems.Add(tmpItem);
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
        public static double askCantidadCargadaProduccion(int id_item, int id = -1, int id_tmpProduccionItem = -1)
        {
            // Usa usuario_logueado global
            return askCantidadCargadaProduccion(id_item, VariablesGlobales.usuario_logueado.IdUsuario, id, id_tmpProduccionItem);
        }

        /// <summary>
    /// Sobrecarga con id_usuario explícito para compatibilidad interna
    /// </summary>
        private static double askCantidadCargadaProduccion(int id_item, int id_usuario, int id = -1, int id_tmpProduccionItem = -1)
        {
            try
            {
                using (var context = new CentrexDbContext())
                {
                    var query = context.TmpProduccionItems.Where(t => t.IdItem == id_item && t.IdUsuario == id_usuario);

                    if (id != -1)
                    {
                        // Filtro adicional si se necesita (aunque TmpProduccionItems no tiene id_produccion)
                        // Este filtro podría no ser necesario en la versión temporal
                    }

                    if (id_tmpProduccionItem != -1)
                    {
                        query = query.Where(t => Operators.ConditionalCompareObjectEqual(t.IdTmpProduccionItem, id_tmpProduccionItem, false));
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
        public static void borrarTmpProduccion(int id_usuario)
        {
            try
            {
                using (var context = new CentrexDbContext())
                {
                    // Borrar items asociados temporales
                    var tmpAsocItems = context.TmpProduccionAsocItems.Where(t => t.IdUsuario == id_usuario).ToList();

                    foreach (var item in tmpAsocItems)
                        context.TmpProduccionAsocItems.Remove((TmpProduccionAsocItemEntity)item);

                    // Borrar items temporales
                    var tmpItems = context.TmpProduccionItems.Where(t => t.IdUsuario == id_usuario).ToList();

                    foreach (var item in tmpItems)
                        context.TmpProduccionItems.Remove((TmpProduccionItemEntity)item);

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
