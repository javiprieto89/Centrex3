using System;
using System.Data;
using System.Linq;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Centrex.Models;

namespace Centrex
{

    static class comprobantes_compras
    {
        /// <summary>
    /// Obtiene información de un comprobante de compra por su ID
    /// </summary>
    /// <param name="id_comprobante_compra">ID del comprobante de compra</param>
    /// <returns>Objeto comprobante_compra (clase legacy) con los datos, o Nothing si no existe</returns>
        public static comprobante_compra info_comprobante_compra(string id_comprobante_compra)
        {
            var tmp = new comprobante_compra();

            if (string.IsNullOrEmpty(id_comprobante_compra))
            {
                tmp.numeroComprobante = "-1";
                return tmp;
            }

            try
            {
                using (var context = new CentrexDbContext())
                {
                    int idComprobante = int.Parse(id_comprobante_compra);

                    var entity = context.ComprobantesCompras.FirstOrDefault(c => c.id_comprobanteCompra == idComprobante);

                    if (entity is null)
                    {
                        tmp.numeroComprobante = "-1";
                        return tmp;
                    }

                    // Mapear de Entity a clase legacy
                    tmp.id_comprobanteCompra = entity.id_comprobanteCompra;
                    tmp.fecha_carga = Conversions.ToString(entity.fecha_carga.ToString()[Conversions.ToInteger("dd/MM/yyyy")]);
                    tmp.fecha_comprobante = entity.fecha.ToString("dd/MM/yyyy");
                    tmp.id_tipoComprobante = entity.id_tipoComprobante;
                    tmp.IdProveedor = entity.IdProveedor;
                    tmp.id_cc = entity.id_cc.HasValue ? entity.id_cc.Value : 0;
                    tmp.id_moneda = entity.id_moneda;
                    tmp.puntoVenta = entity.puntoVenta.ToString();
                    tmp.numeroComprobante = entity.numero.ToString();
                    tmp.id_condicion_compra = entity.id_condicion_compra.HasValue ? entity.id_condicion_compra.Value : 0;
                    tmp.subtotal = (double)entity.neto;
                    tmp.impuestos = (double)entity.ImpuestosTotal;
                    tmp.conceptos = 0d; // Este campo no existe en la entity, se mantiene en 0
                    tmp.total = (double)entity.total;
                    tmp.tasaCambio = 0d; // Este campo no existe en la entity, se mantiene en 0
                    tmp.nota = string.IsNullOrEmpty(entity.observaciones) ? string.Empty : entity.observaciones;
                    tmp.cae = string.IsNullOrEmpty(entity.cae) ? string.Empty : entity.cae;
                    tmp.activo = entity.activo;

                    return tmp;
                }
            }
            catch (Exception ex)
            {
                Interaction.MsgBox(ex.Message.ToString());
                tmp.numeroComprobante = "-1";
                return tmp;
            }
        }

        /// <summary>
    /// Agrega un nuevo comprobante de compra
    /// </summary>
    /// <param name="cc">Clase legacy comprobante_compra con los datos</param>
    /// <returns>ID del comprobante creado, o -1 si falla</returns>
        public static int add_comprobante_compra(comprobante_compra cc)
        {
            try
            {
                using (var context = new CentrexDbContext())
                {
                    var nuevoComprobante = new ComprobanteCompraEntity()
                    {
                        IdProveedor = cc.IdProveedor,
                        id_cc = (int)(cc.id_cc > 0 ? cc.id_cc : default(int?)),
                        id_condicion_compra = (int)(cc.id_condicion_compra > 0 ? cc.id_condicion_compra : default(int?)),
                        id_tipoComprobante = cc.id_tipoComprobante,
                        id_moneda = cc.id_moneda,
                        puntoVenta = int.Parse(cc.puntoVenta).ToString(),
                        numero = int.Parse(cc.numeroComprobante),
                        fecha = DateTime.Parse(cc.fecha_comprobante),
                        fecha_vencimiento = default,
                        cae = string.IsNullOrEmpty(cc.cae) ? string.Empty : cc.cae,
                        vencimiento_cae = default,
                        total = (decimal)cc.total,
                        neto = (decimal)cc.subtotal,
                        exento = 0m,
                        tributos = 0m,
                        impuestos = default, // Se manejará por separado
                        descuento = 0m,
                        percepciones = 0m,
                        observaciones = string.IsNullOrEmpty(cc.nota) ? string.Empty : cc.nota,
                        fecha_carga = DateTime.Now,
                        activo = true
                    };

                    context.ComprobantesCompras.Add(nuevoComprobante);
                    context.SaveChanges();

                    return nuevoComprobante.id_comprobanteCompra;
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
        public static bool update_comprobante_compra(comprobante_compra cc)
        {
            try
            {
                using (var context = new CentrexDbContext())
                {
                    var comprobante = context.ComprobantesCompras.FirstOrDefault(c => c.id_comprobanteCompra == cc.id_comprobanteCompra);

                    if (comprobante is null)
                    {
                        Interaction.MsgBox("Comprobante no encontrado");
                        return false;
                    }

                    // Actualizar campos
                    comprobante.IdProveedor = cc.IdProveedor;
                    comprobante.id_cc = (int)(cc.id_cc > 0 ? cc.id_cc : default(int?));
                    comprobante.id_condicion_compra = (int)(cc.id_condicion_compra > 0 ? cc.id_condicion_compra : default(int?));
                    comprobante.id_tipoComprobante = cc.id_tipoComprobante;
                    comprobante.id_moneda = cc.id_moneda;
                    comprobante.puntoVenta = int.Parse(cc.puntoVenta).ToString();
                    comprobante.numero = int.Parse(cc.numeroComprobante);
                    comprobante.fecha = DateTime.Parse(cc.fecha_comprobante);
                    comprobante.cae = string.IsNullOrEmpty(cc.cae) ? string.Empty : cc.cae;
                    comprobante.total = (decimal)cc.total;
                    comprobante.neto = (decimal)cc.subtotal;
                    comprobante.ImpuestosTotal = (decimal)cc.impuestos;
                    comprobante.observaciones = string.IsNullOrEmpty(cc.nota) ? string.Empty : cc.nota;

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
        public static bool cerrar_comprobante_compra(comprobante_compra cc)
        {
            try
            {
                using (var context = new CentrexDbContext())
                {
                    var comprobante = context.ComprobantesCompras.FirstOrDefault(c => c.id_comprobanteCompra == cc.id_comprobanteCompra);

                    if (comprobante is null)
                    {
                        Interaction.MsgBox("Comprobante no encontrado");
                        return false;
                    }

                    // Actualizar totales y cerrar
                    comprobante.neto = (decimal)cc.subtotal;
                    comprobante.ImpuestosTotal = (decimal)cc.impuestos;
                    comprobante.total = (decimal)cc.total;
                    comprobante.observaciones = string.IsNullOrEmpty(cc.nota) ? string.Empty : cc.nota;
                    comprobante.activo = false; // Cerrar el comprobante

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
        public static bool add_item_comprobanteCompra(int id_comprobanteCompra, int id_item, int cantidad, double precio)
        {
            try
            {
                using (var context = new CentrexDbContext())
                {
                    var nuevoItem = new ComprobanteCompraItemEntity()
                    {
                        id_comprobanteCompra = id_comprobanteCompra,
                        id_item = id_item,
                        cantidad = (int)Math.Round((decimal)cantidad),
                        precio = (decimal)precio,
                        bonificacion = 0m,
                        subtotal = (decimal)(cantidad * precio),
                        observaciones = string.Empty
                    };

                    context.ComprobantesComprasItems.Add(nuevoItem);
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
        public static bool add_impuesto_comprobanteCompra(int id_comprobanteCompra, int id_impuesto, double importe)
        {
            try
            {
                using (var context = new CentrexDbContext())
                {
                    var nuevoImpuesto = new ComprobanteCompraImpuestoEntity()
                    {
                        id_comprobanteCompra = id_comprobanteCompra,
                        id_impuesto = id_impuesto,
                        baseImponible = 0m,
                        alicuota = 0m,
                        importe = (decimal)importe
                    };

                    context.ComprobantesComprasImpuestos.Add(nuevoImpuesto);
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
    /// <param name="total">Total del concepto</param>
    /// <returns>True si tuvo éxito, False si falló</returns>
        public static bool add_concepto_comprobanteCompra(int id_comprobanteCompra, int id_concepto_compra, double subtotal, double iva, double total)
        {
            try
            {
                using (var context = new CentrexDbContext())
                {
                    // Nota: La entity tiene descripcion e importe, pero la firma legacy recibe subtotal, iva, total
                    // Guardamos el total en el campo importe
                    var nuevoConcepto = new ComprobanteCompraConceptoEntity()
                    {
                        id_comprobanteCompra = id_comprobanteCompra,
                        id_concepto_compra = id_concepto_compra,
                        descripcion = string.Empty,
                        importe = (decimal)total
                    };

                    context.ComprobantesComprasConceptos.Add(nuevoConcepto);
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
    /// Si no se pasa id_comprobante_compra, sale sin hacer nada (los activos ya no se borran automáticamente)
    /// Si se pasa un id_comprobante_compra, borra ese comprobante y todas sus asociaciones
    /// </summary>
    /// <param name="id_comprobante_compra">ID del comprobante a borrar, o -1 para no hacer nada</param>
        public static void borrar_comprobantes_compras_activos(int id_comprobante_compra = -1)
        {
            if (id_comprobante_compra == -1)
            {
                // No hacer nada - el código original comentaba que los activos se borran por trigger
                return;
            }

            try
            {
                using (var context = new CentrexDbContext())
                {
                    // Borrar conceptos asociados
                    var conceptos = context.ComprobantesComprasConceptos.Where(c => c.id_comprobanteCompra == id_comprobante_compra).ToList();
                    context.ComprobantesComprasConceptos.RemoveRange(conceptos);

                    // Borrar impuestos asociados
                    var impuestos = context.ComprobantesComprasImpuestos.Where(i => i.id_comprobanteCompra == id_comprobante_compra).ToList();
                    context.ComprobantesComprasImpuestos.RemoveRange(impuestos);

                    // Borrar items asociados
                    var items = context.ComprobantesComprasItems.Where(i => i.id_comprobanteCompra == id_comprobante_compra).ToList();
                    context.ComprobantesComprasItems.RemoveRange(items);

                    // Borrar el comprobante principal
                    var comprobante = context.ComprobantesCompras.FirstOrDefault(c => c.id_comprobanteCompra == id_comprobante_compra);

                    if (comprobante is not null)
                    {
                        context.ComprobantesCompras.Remove(comprobante);
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
            int id_cc = -1;

            try
            {
                using (var context = new CentrexDbContext())
                {
                    var ultimoComprobante = context.ComprobantesCompras.Where(c => c.IdProveedor == id_proveedor && c.id_cc.HasValue).OrderByDescending(c => c.id_comprobanteCompra).Select(c => c.id_cc).FirstOrDefault();




                    if (ultimoComprobante.HasValue)
                    {
                        id_cc = ultimoComprobante.Value;
                    }

                    return id_cc;
                }
            }
            catch (Exception ex)
            {
                return id_cc;
            }
        }
    }
}
