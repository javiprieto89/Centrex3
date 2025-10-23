using System;
using System.Linq;
using System.Xml.Linq;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Centrex.Models;

namespace Centrex
{

    static class cajas
    {
        // ************************************ FUNCIONES DE CAJAS ********************
        public static caja info_caja(string id_caja)
        {
            var tmp = new caja();

            try
            {
                using (CentrexDbContext context = GetDbContext())
                {
                    var cajaEntity = context.Cajas.FirstOrDefault(c => c.IdCaja == Conversions.ToInteger(id_caja));

                    if (cajaEntity is not null)
                    {
                        tmp.id_caja = cajaEntity.IdCaja.ToString();
                        tmp.nombre = cajaEntity.nombre;
                        tmp.esCC = cajaEntity.esCC;
                        tmp.activo = cajaEntity.activo;
                    }
                    else
                    {
                        tmp.nombre = "error";
                    }
                }

                return tmp;
            }
            catch (Exception ex)
            {
                tmp.nombre = "error";
                return tmp;
            }
        }

        public static bool addCaja(caja c)
        {
            try
            {
                using (CentrexDbContext context = GetDbContext())
                {
                    var cajaEntity = new CajaEntity()
                    {
                        nombre = c.nombre,
                        esCC = c.esCC,
                        activo = c.activo
                    };

                    context.Cajas.Add(cajaEntity);
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

        public static bool updateCaja(caja c, bool borra = false)
        {
            try
            {
                using (CentrexDbContext context = GetDbContext())
                {
                    var cajaEntity = context.Cajas.FirstOrDefault(ce => ce.IdCaja == c.id_caja);

                    if (cajaEntity is not null)
                    {
                        if (borra == true)
                        {
                            cajaEntity.activo = false;
                        }
                        else
                        {
                            cajaEntity.nombre = c.nombre;
                            cajaEntity.esCC = c.esCC;
                            cajaEntity.activo = c.activo;
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

        public static bool borrarCaja(caja c)
        {
            try
            {
                using (CentrexDbContext context = GetDbContext())
                {
                    var cajaEntity = context.Cajas.FirstOrDefault(ce => ce.IdCaja == c.id_caja);

                    if (cajaEntity is not null)
                    {
                        context.Cajas.Remove(cajaEntity);
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


        // ************************************ FUNCIONES DE CAJAS ********************
    }
}
