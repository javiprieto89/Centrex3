using System;
using System.Linq;
using System.Windows.Forms;

namespace Centrex.Funciones
{

    static class condiciones_venta
    {

        // ************************************ FUNCIONES DE CONDICIONES DE VENTA **********************
        public static CondicionVentaEntity info_condicion_venta(int id_condicion)
        {
            try
            {
                using (CentrexDbContext context = new CentrexDbContext())
                {
                    var condicionEntity = context.CondicionVentaEntity.FirstOrDefault(c => c.IdCondicionVenta == id_condicion);             

                    if (condicionEntity is not null)
                    {
                        return condicionEntity;
                    }
                    else
                    {
                        return null;
                    }
                }                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener condición de venta: " + ex.Message, "Centrex", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        public static bool addCondicion_venta(CondicionVentaEntity Condicion)
        {
            try
            {
                using (CentrexDbContext context = new CentrexDbContext())
                {
                    var condicionEntity = new CondicionVentaEntity()
                    {
                        Condicion = Condicion.Condicion,
                        Vencimiento = Condicion.Vencimiento,
                        Recargo = (decimal)Condicion.Recargo,
                        Activo = Condicion.Activo
                    };

                    context.CondicionVentaEntity.Add(condicionEntity);
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

        public static bool updateCondicion_venta(CondicionVentaEntity Condicion, bool borra = false)
        {
            try
            {
                using (CentrexDbContext context = new CentrexDbContext())
                {
                    var condicionEntity = context.CondicionVentaEntity.FirstOrDefault(c => c.IdCondicionVenta == Condicion.IdCondicionVenta);

                    if (condicionEntity is not null)
                    {
                        if (borra == true)
                        {
                            condicionEntity.Activo = false;
                        }
                        else
                        {
                            condicionEntity.Condicion = Condicion.Condicion;
                            condicionEntity.Vencimiento = Condicion.Vencimiento;
                            condicionEntity.Recargo = (decimal)Condicion.Recargo;
                            condicionEntity.Activo = Condicion.Activo;
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

        public static bool borrarCondicion_venta(CondicionVentaEntity Condicion)
        {
            try
            {
                using (CentrexDbContext context = new CentrexDbContext())
                {
                    var condicionEntity = context.CondicionVentaEntity.FirstOrDefault(c => c.IdCondicionVenta == Condicion.IdCondicionVenta);

                    if (condicionEntity is not null)
                    {
                        context.CondicionVentaEntity.Remove(condicionEntity);
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
        // ************************************ FUNCIONES DE CONDICIONES VENTA **********************
    }
}

