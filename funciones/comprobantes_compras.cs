using System;
using System.Data;
using System.Linq;

namespace Centrex.Funciones
{

    static class comprobantes_compras
    {
        /// <summary>
        /// Obtiene información de un comprobante de compra por su ID
        /// </summary>
        /// <param name="IdComprobanteCompra">ID del comprobante de compra</param>
        /// <returns>Objeto comprobante_compra (clase legacy) con los datos, o Nothing si no existe</returns>
        public static ComprobanteCompraEntity info_comprobante_compra(int IdComprobanteCompra)
        {

            var tmp = new ComprobanteCompraEntity();
            try
            {
                if (IdComprobanteCompra != 0 && IdComprobanteCompra != -1)
                {
                    using (var context = new CentrexDbContext())
                    {

                        var entity = context.ComprobanteCompraEntity.FirstOrDefault(c => c.IdComprobanteCompra == IdComprobanteCompra);

                        if (entity is not null)
                        {
                            return entity;

                            //// Mapear de Entity a clase legacy
                            //tmp.id_comprobanteCompra = entity.id_comprobanteCompra;
                            //tmp.fecha_carga = Conversions.ToString(entity.fecha_carga.ToString()[Conversions.ToInteger("dd/MM/yyyy")]);
                            //tmp.fecha_comprobante = entity.fecha.ToString("dd/MM/yyyy");
                            //tmp.IdTipoComprobante = entity.IdTipoComprobante;
                            //tmp.IdProveedor = entity.IdProveedor;
                            //tmp.IdCc = entity.IdCc.HasValue ? entity.IdCc.Value : 0;
                            //tmp.IdMoneda = entity.IdMoneda;
                            //tmp.PuntoVenta = entity.PuntoVenta.ToString();
                            //tmp.numeroComprobante = entity.Numero.ToString();
                            //tmp.IdCondicionCompra = entity.IdCondicionCompra.HasValue ? entity.IdCondicionCompra.Value : 0;
                            //tmp.subtotal = (double)entity.Neto;
                            //tmp.impuestos = (double)entity.ImpuestosTotal;
                            //tmp.conceptos = 0d; // Este campo no existe en la entity, se mantiene en 0
                            //tmp.Total = (double)entity.Total;
                            //tmp.tasaCambio = 0d; // Este campo no existe en la entity, se mantiene en 0
                            //tmp.nota = string.IsNullOrEmpty(entity.observaciones) ? string.Empty : entity.observaciones;
                            //tmp.cae = string.IsNullOrEmpty(entity.cae) ? string.Empty : entity.cae;
                            //tmp.activo = entity.activo;

                            //return tmp;
                        }
                        else
                        {
                            tmp.IdComprobanteCompra = -1;
                            return entity;
                        }
                    }
                }
                else
                {
                    tmp.IdComprobanteCompra = -1;
                    return tmp;
                }
            }
            catch (Exception ex)
            {
                Interaction.MsgBox(ex.Message.ToString());
                tmp.IdComprobanteCompra = -1;
                return tmp;
            }
        }

        /// <summary>
        /// Agrega un nuevo comprobante de compra
        /// </summary>
        /// <param name="cc">Clase legacy comprobante_compra con los datos</param>
        /// <returns>ID del comprobante creado, o -1 si falla</returns>
        public static int add_comprobante_compra(ComprobanteCompraEntity cc)
        {
            try
            {
                using (var context = new CentrexDbContext())
                {
                    var nuevoComprobante = new ComprobanteCompraEntity()
                    {
                        IdProveedor = cc.IdProveedor,
                        IdCc = (int)(cc.IdCc > 0 ? cc.IdCc : default(int?)),
                        IdCondicionCompra = (int)(cc.IdCondicionCompra > 0 ? cc.IdCondicionCompra : default(int?)),
                        IdTipoComprobante = cc.IdTipoComprobante,
                        IdMoneda = cc.IdMoneda,
                        PuntoVenta = cc.PuntoVenta,
                        NumeroComprobante = cc.NumeroComprobante,
                        FechaComprobante = cc.FechaComprobante,
                        Cae = string.IsNullOrEmpty(cc.Cae) ? string.Empty : cc.Cae,
                        Total = (decimal)cc.Total,
                        Subtotal = (decimal)cc.Subtotal,
                        Nota = string.IsNullOrEmpty(cc.Nota) ? string.Empty : cc.Nota,
                        FechaCarga = DateOnly.FromDateTime(DateTime.Now),
                        Activo = true
                    };

                    context.ComprobanteCompraEntity.Add(nuevoComprobante);
                    context.SaveChanges();

                    return nuevoComprobante.IdComprobanteCompra;
                }
            }
            catch (Exception ex)
            {
                Interaction.MsgBox(ex.Message);
                return -1;
            }
        }

        /// <summary>
        /// Actualiza un comprobante de compra existente
        /// </summary>
        /// <param name="cc">Clase legacy comprobante_compra con los datos</param>
        /// <returns>True si tuvo éxito, False si falló</returns>
        public static bool update_comprobante_compra(ComprobanteCompraEntity cc)
        {
            try
            {
                using (var context = new CentrexDbContext())
                {
                    var comprobante = context.ComprobanteCompraEntity.FirstOrDefault(c => c.IdComprobanteCompra == cc.IdComprobanteCompra);

                    if (comprobante is null)
                    {
                        Interaction.MsgBox("Comprobante no encontrado");
                        return false;
                    }

                    // Actualizar campos
                    comprobante.IdProveedor = cc.IdProveedor;
                    comprobante.IdCc = (int)(cc.IdCc > 0 ? cc.IdCc : default(int?));
                    comprobante.IdCondicionCompra = (int)(cc.IdCondicionCompra > 0 ? cc.IdCondicionCompra : default(int?));
                    comprobante.IdTipoComprobante = cc.IdTipoComprobante;
                    comprobante.IdMoneda = cc.IdMoneda;
                    comprobante.PuntoVenta = cc.PuntoVenta;
                    comprobante.NumeroComprobante = cc.NumeroComprobante;
                    comprobante.FechaComprobante = cc.FechaComprobante;
                    comprobante.Cae = string.IsNullOrEmpty(cc.Cae) ? string.Empty : cc.Cae;
                    comprobante.Total = cc.Total;
                    comprobante.Subtotal = cc.Subtotal;
                    comprobante.Impuestos = (decimal)cc.Impuestos;
                    comprobante.Nota = string.IsNullOrEmpty(cc.Nota) ? string.Empty : cc.Nota;

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
        /// Cierra un comprobante de compra actualizando totales y marcándolo como inactivo
        /// </summary>
        /// <param name="cc">Clase legacy comprobante_compra con los datos finales</param>
        /// <returns>True si tuvo éxito, False si falló</returns>
        public static bool cerrar_comprobante_compra(ComprobanteCompraEntity cc)
        {
            try
            {
                using (var context = new CentrexDbContext())
                {
                    var comprobante = context.ComprobanteCompraEntity.FirstOrDefault(c => c.IdComprobanteCompra == cc.IdComprobanteCompra);

                    if (comprobante is null)
                    {
                        Interaction.MsgBox("Comprobante no encontrado");
                        return false;
                    }

                    // Actualizar totales y cerrar
                    comprobante.Subtotal = cc.Subtotal;
                    comprobante.Impuestos = cc.Impuestos;
                    comprobante.Total = (decimal)cc.Total;
                    comprobante.Nota = string.IsNullOrEmpty(cc.Nota) ? string.Empty : cc.Nota;
                    comprobante.Activo = false; // Cerrar el comprobante

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
        /// Agrega un item a un comprobante de compra
        /// </summary>
        /// <param name="id_comprobanteCompra">ID del comprobante de compra</param>
        /// <param name="id_item">ID del item</param>
        /// <param name="cantidad">Cantidad del item</param>
        /// <param name="precio">Precio unitario</param>
        /// <returns>True si tuvo éxito, False si falló</returns>
        public static bool add_item_comprobanteCompra(int id_comprobanteCompra, int id_item, int cantidad, decimal precio)
        {
            try
            {
                using (var context = new CentrexDbContext())
                {
                    var nuevoItem = new ComprobanteCompraItemEntity()
                    {
                        IdComprobanteCompra = id_comprobanteCompra,
                        IdItem = id_item,
                        Cantidad = cantidad,
                        Precio = precio,
                    };

                    context.ComprobanteCompraItemEntity.Add(nuevoItem);
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
        /// Agrega un impuesto a un comprobante de compra
        /// </summary>
        /// <param name="id_comprobanteCompra">ID del comprobante de compra</param>
        /// <param name="id_impuesto">ID del impuesto</param>
        /// <param name="importe">Importe del impuesto</param>
        /// <returns>True si tuvo éxito, False si falló</returns>
        public static bool add_impuesto_comprobanteCompra(int id_comprobanteCompra, int id_impuesto, decimal importe)
        {
            try
            {
                using (var context = new CentrexDbContext())
                {
                    var nuevoImpuesto = new ComprobanteCompraImpuestoEntity()
                    {
                        IdComprobanteCompra = id_comprobanteCompra,
                        IdImpuesto = id_impuesto,
                        Importe = importe
                    };

                    context.ComprobanteCompraImpuestoEntity.Add(nuevoImpuesto);
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
        /// Agrega un concepto a un comprobante de compra
        /// </summary>
        /// <param name="id_comprobanteCompra">ID del comprobante de compra</param>
        /// <param name="id_concepto_compra">ID del concepto de compra</param>
        /// <param name="subtotal">Subtotal del concepto</param>
        /// <param name="iva">IVA del concepto</param>
        /// <param name="Total">Total del concepto</param>
        /// <returns>True si tuvo éxito, False si falló</returns>
        public static bool add_concepto_comprobanteCompra(int id_comprobanteCompra, int id_concepto_compra, decimal subtotal, decimal iva, decimal total)
        {
            try
            {
                using (var context = new CentrexDbContext())
                {
                    // Nota: La entity tiene descripcion e importe, pero la firma legacy recibe subtotal, iva, Total
                    // Guardamos el Total en el campo importe
                    var nuevoConcepto = new ComprobanteCompraConceptoEntity()
                    {
                        IdComprobanteCompra = id_comprobanteCompra,
                        IdConceptoCompra = id_concepto_compra,
                        Subtotal = subtotal,
                        Iva = iva,
                        Total = total
                    };

                    context.ComprobanteCompraConceptoEntity.Add(nuevoConcepto);
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
        /// Borra comprobantes de compra y sus asociaciones (items, impuestos, conceptos)
        /// Si no se pasa IdComprobanteCompra, sale sin hacer nada (los activos ya no se borran automáticamente)
        /// Si se pasa un IdComprobanteCompra, borra ese comprobante y todas sus asociaciones
        /// </summary>
        /// <param name="IdComprobanteCompra">ID del comprobante a borrar, o -1 para no hacer nada</param>
        public static void borrar_comprobantes_compras_activos(int IdComprobanteCompra = -1)
        {
            if (IdComprobanteCompra == -1)
            {
                // No hacer nada - el código original comentaba que los activos se borran por trigger
                return;
            }

            try
            {
                using (var context = new CentrexDbContext())
                {
                    // Borrar conceptos asociados
                    var conceptos = context.ComprobanteCompraConceptoEntity.Where(c => c.IdComprobanteCompra == IdComprobanteCompra).ToList();
                    context.ComprobanteCompraConceptoEntity.RemoveRange(conceptos);

                    // Borrar impuestos asociados
                    var impuestos = context.ComprobanteCompraImpuestoEntity.Where(i => i.IdComprobanteCompra == IdComprobanteCompra).ToList();
                    context.ComprobanteCompraImpuestoEntity.RemoveRange(impuestos);

                    // Borrar items asociados
                    var items = context.ComprobanteCompraItemEntity.Where(i => i.IdComprobanteCompra == IdComprobanteCompra).ToList();
                    context.ComprobanteCompraItemEntity.RemoveRange(items);

                    // Borrar el comprobante principal
                    var comprobante = context.ComprobanteCompraEntity.FirstOrDefault(c => c.IdComprobanteCompra == IdComprobanteCompra);

                    if (comprobante is not null)
                    {
                        context.ComprobanteCompraEntity.Remove(comprobante);
                    }

                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Interaction.MsgBox(ex.Message);
            }
        }

        /// <summary>
        /// Obtiene el último ID de cuenta corriente usado para un proveedor
        /// </summary>
        /// <param name="id_proveedor">ID del proveedor</param>
        /// <returns>ID de la cuenta corriente, o -1 si no se encuentra</returns>
        public static int Ultima_CC_comprobante_compra_proveedor(int id_proveedor)
        {
            int IdCc = -1;

            try
            {
                using (var context = new CentrexDbContext())
                {
                    var ultimoComprobante = context.ComprobanteCompraEntity
                       .Where(c => c.IdProveedor == id_proveedor && c.IdCc > 0)
                       .OrderByDescending(c => c.IdComprobanteCompra)
                       .Select(c => c.IdCc)
                       .FirstOrDefault();

                    if (ultimoComprobante > 0)
                    {
                        IdCc = ultimoComprobante;
                    }

                    return IdCc;
                }
            }
            catch (Exception ex)
            {
                Interaction.MsgBox(ex.Message);
                return IdCc;
            }
        }
    }
}
