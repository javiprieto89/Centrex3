using System;
using System.Linq;
using System.Windows.Forms;

namespace Centrex.Funciones
{

    static class itemsImpuestos
    {
        // ************************************ FUNCIONES DE RELACION ITEMS E IMPUESTOS ********************
        public static ItemImpuestoEntity info_itemImpuesto(int IdItem, int IdImpuesto)
        {
                     try
            {
                using (CentrexDbContext context = new CentrexDbContext())
                {
                    var itemImpuestoEntity = context.ItemImpuestoEntity.FirstOrDefault(ii => ii.IdItem == IdItem && ii.IdImpuesto == IdImpuesto);
                    
                    if (itemImpuestoEntity is not null)
                    {
                        return itemImpuestoEntity;
                    }
                    else
                    {
                        return null;
                    }

                }            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener los impuestos del item:" + ex.Message, "Centrex", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        public static bool additemImpuesto(ItemImpuestoEntity ii)
        {
            try
            {
                using (CentrexDbContext context = new CentrexDbContext())
                {
                    var itemImpuestoEntity = new ItemImpuestoEntity()
                    {
                        IdItem = ii.IdItem,
                        IdImpuesto = ii.IdImpuesto,
                        Activo = ii.Activo
                    };

                    context.ItemImpuestoEntity.Add(itemImpuestoEntity);
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

        public static bool updateitemImpuesto(ItemImpuestoEntity iiViejo, ItemImpuestoEntity iiNuevo = null, bool borra = false)
        {
            try
            {
                using (CentrexDbContext context = new CentrexDbContext())
                {
                    var itemImpuestoEntity = context.ItemImpuestoEntity.FirstOrDefault(ii => ii.IdItem == iiViejo.IdItem && ii.IdImpuesto == iiViejo.IdImpuesto);

                    if (itemImpuestoEntity is not null)
                    {
                        if (borra == true)
                        {
                            itemImpuestoEntity.Activo = false;
                        }
                        else if (iiNuevo is not null)
                        {
                            // Actualizar la clave primaria compuesta requiere eliminar y crear
                            context.ItemImpuestoEntity.Remove(itemImpuestoEntity);
                            context.SaveChanges();

                            var nuevoItemImpuesto = new ItemImpuestoEntity()
                            {
                                IdItem = iiNuevo.IdItem,
                                IdImpuesto = iiNuevo.IdImpuesto,
                                Activo = iiNuevo.Activo
                            };
                            context.ItemImpuestoEntity.Add(nuevoItemImpuesto);
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

        public static bool borraritemImpuesto(ItemImpuestoEntity ii)
        {
            try
            {
                using (CentrexDbContext context = new CentrexDbContext())
                {
                    var itemImpuestoEntity = context.ItemImpuestoEntity.FirstOrDefault(iie => iie.IdItem == ii.IdItem && iie.IdImpuesto == ii.IdImpuesto);

                    if (itemImpuestoEntity is not null)
                    {
                        context.ItemImpuestoEntity.Remove(itemImpuestoEntity);
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
