using System;
using System.Linq;
using System.Xml.Linq;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Centrex.Models;

namespace Centrex
{

    static class condiciones_compra
    {

        // ************************************ FUNCIONES DE CONDICIONES DE COMPRA **********************
        public static condicion_compra info_condicion_compra(string id_condicion)
        {
            var tmp = new condicion_compra();

            try
            {
                using (CentrexDbContext context = GetDbContext())
                {
                    var condicionEntity = context.CondicionesCompra.FirstOrDefault(c => c.IdCondicionCompra == Conversions.ToInteger(id_condicion));

                    if (condicionEntity is not null)
                    {
                        tmp.id_condicion_compra = condicionEntity.IdCondicionCompra.ToString();
                        tmp.condicion = condicionEntity.condicion;
                        tmp.vencimiento = Conversions.ToInteger(condicionEntity.vencimiento.ToString());
                        tmp.recargo = Conversions.ToDouble(condicionEntity.recargo.ToString());
                        tmp.activo = condicionEntity.activo;
                    }
                    else
                    {
                        tmp.condicion = "error";
                    }
                }

                return tmp;
            }
            catch (Exception ex)
            {
                tmp.condicion = "error";
                return tmp;
            }
        }

        public static bool addCondicion_compra(condicion_compra condicion)
        {
            try
            {
                using (CentrexDbContext context = GetDbContext())
                {
                    var condicionEntity = new CondicionCompraEntity()
                    {
                        condicion = condicion.condicion,
                        vencimiento = condicion.vencimiento,
                        recargo = (decimal)condicion.recargo,
                        activo = condicion.activo
                    };

                    context.CondicionesCompra.Add(condicionEntity);
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

        public static bool updateCondicion_compra(condicion_compra condicion, bool borra = false)
        {
            try
            {
                using (CentrexDbContext context = GetDbContext())
                {
                    var condicionEntity = context.CondicionesCompra.FirstOrDefault(c => c.IdCondicionCompra == condicion.id_condicion_compra);

                    if (condicionEntity is not null)
                    {
                        if (borra == true)
                        {
                            condicionEntity.activo = false;
                        }
                        else
                        {
                            condicionEntity.condicion = condicion.condicion;
                            condicionEntity.vencimiento = condicion.vencimiento;
                            condicionEntity.recargo = (decimal)condicion.recargo;
                            condicionEntity.activo = condicion.activo;
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

        public static bool borrarCondicion_compra(condicion_compra condicion)
        {
            try
            {
                using (CentrexDbContext context = GetDbContext())
                {
                    var condicionEntity = context.CondicionesCompra.FirstOrDefault(c => c.IdCondicionCompra == condicion.id_condicion_compra);

                    if (condicionEntity is not null)
                    {
                        context.CondicionesCompra.Remove(condicionEntity);
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
