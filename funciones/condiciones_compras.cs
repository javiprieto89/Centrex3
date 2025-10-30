using System;
using System.Linq;

namespace Centrex.Funciones
{

    static class condiciones_compra
    {

        // ************************************ FUNCIONES DE CONDICIONES DE COMPRA **********************
        public static CondicionCompraEntity info_condicion_compra(string id_condicion)
        {
            var tmp = new CondicionCompraEntity();

            try
            {
                using (CentrexDbContext context = new CentrexDbContext())
                {
                    
                    var condicionEntity = context.CondicionCompraEntity.FirstOrDefault(c => c.IdCondicionCompra == Conversions.ToInteger(id_condicion));

                    if (condicionEntity is not null)
                    {
                        tmp.IdCondicionCompra = condicionEntity.IdCondicionCompra;
                        tmp.Condicion = condicionEntity.Condicion;
                        tmp.Vencimiento = condicionEntity.Vencimiento;
                        tmp.Recargo = Conversions.ToDecimal(condicionEntity.Recargo.ToString());
                        tmp.Activo = condicionEntity.Activo;
                    }
                    else
                    {
                        tmp.Condicion = "error";
                    }
                }

                return tmp;
            }
            catch (Exception ex)
            {
                tmp.Condicion = "error";
                return tmp;
            }
        }

        public static bool addCondicion_compra(CondicionCompraEntity Condicion)
        {
            try
            {
                using (CentrexDbContext context = new CentrexDbContext())
                {
                    var condicionEntity = new CondicionCompraEntity()
                    {
                        Condicion = Condicion.Condicion,
                        Vencimiento = Condicion.Vencimiento,
                        Recargo = (decimal)Condicion.Recargo,
                        Activo = Condicion.Activo
                    };

                    context.CondicionCompraEntity.Add(condicionEntity);
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

        public static bool updateCondicion_compra(CondicionCompraEntity Condicion, bool borra = false)
        {
            try
            {
                using (CentrexDbContext context = new CentrexDbContext())
                {
                    var condicionEntity = context.CondicionCompraEntity.FirstOrDefault(c => c.IdCondicionCompra == Condicion.IdCondicionCompra);

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

        public static bool borrarCondicion_compra(CondicionCompraEntity Condicion)
        {
            try
            {
                using (CentrexDbContext context = new CentrexDbContext())
                {
                    var condicionEntity = context.CondicionCompraEntity.FirstOrDefault(c => c.IdCondicionCompra == Condicion.IdCondicionCompra);

                    if (condicionEntity is not null)
                    {
                        context.CondicionCompraEntity.Remove(condicionEntity);
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
        // ************************************ FUNCIONES DE CONDICIONES COMPRA **********************
    }
}
