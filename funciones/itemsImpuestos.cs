using System;
using System.Linq;
using System.Xml.Linq;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Centrex.Models;

namespace Centrex
{

    static class itemsImpuestos
    {
        // ************************************ FUNCIONES DE RELACION ITEMS E IMPUESTOS ********************
        public static itemImpuesto info_itemImpuesto(string id_item, string id_impuesto)
        {
            var tmp = new itemImpuesto();
            try
            {
                using (CentrexDbContext context = GetDbContext())
                {
                    var itemImpuestoEntity = context.ItemsImpuestos.FirstOrDefault(ii => ii.IdItem == Conversions.ToInteger(id_item) && ii.IdImpuesto == Conversions.ToInteger(id_impuesto));

                    if (itemImpuestoEntity is not null)
                    {
                        tmp.id_item = itemImpuestoEntity.IdItem.ToString();
                        tmp.id_impuesto = itemImpuestoEntity.IdImpuesto.ToString();
                        tmp.activo = itemImpuestoEntity.activo;
                    }
                }

                return tmp;
            }
            catch (Exception ex)
            {
                Interaction.MsgBox(ex.Message.ToString());
                return tmp;
            }
        }

        public static bool additemImpuesto(itemImpuesto ii)
        {
            try
            {
                using (CentrexDbContext context = GetDbContext())
                {
                    var itemImpuestoEntity = new ItemImpuestoEntity()
                    {
                        IdItem = ii.id_item,
                        IdImpuesto = ii.id_impuesto,
                        activo = ii.activo
                    };

                    context.ItemsImpuestos.Add(itemImpuestoEntity);
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

        public static bool updateitemImpuesto(itemImpuesto iiViejo, itemImpuesto iiNuevo = null, bool borra = false)
        {
            try
            {
                using (CentrexDbContext context = GetDbContext())
                {
                    var itemImpuestoEntity = context.ItemsImpuestos.FirstOrDefault(ii => ii.IdItem == iiViejo.id_item && ii.IdImpuesto == iiViejo.id_impuesto);

                    if (itemImpuestoEntity is not null)
                    {
                        if (borra == true)
                        {
                            itemImpuestoEntity.activo = false;
                        }
                        else if (iiNuevo is not null)
                        {
                            // Actualizar la clave primaria compuesta requiere eliminar y crear
                            context.ItemsImpuestos.Remove(itemImpuestoEntity);
                            context.SaveChanges();

                            var nuevoItemImpuesto = new ItemImpuestoEntity()
                            {
                                IdItem = iiNuevo.id_item,
                                IdImpuesto = iiNuevo.id_impuesto,
                                activo = iiNuevo.activo
                            };
                            context.ItemsImpuestos.Add(nuevoItemImpuesto);
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

        public static bool borraritemImpuesto(itemImpuesto ii)
        {
            try
            {
                using (CentrexDbContext context = GetDbContext())
                {
                    var itemImpuestoEntity = context.ItemsImpuestos.FirstOrDefault(iie => iie.IdItem == ii.id_item && iie.IdImpuesto == ii.id_impuesto);

                    if (itemImpuestoEntity is not null)
                    {
                        context.ItemsImpuestos.Remove(itemImpuestoEntity);
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
        // ************************************ FUNCIONES DE RELACION ITEMS E IMPUESTOS ********************
    }
}
