using System;
using System.Linq;

namespace Centrex.Funciones
{

    static class cajas
    {
        // ************************************ FUNCIONES DE CAJAS ********************
        public static CajaEntity info_caja(int IdCaja)
        {
            var tmp = new CajaEntity();

            try
            {
                using (CentrexDbContext context = new CentrexDbContext())
                {
                    var cajaEntity = context.CajaEntity.FirstOrDefault(c => c.IdCaja == IdCaja);

                    if (cajaEntity is not null)
                    {
                        return cajaEntity;
                    }
                    else
                    {
                        tmp.Nombre = "error";
                    }
                }

                return tmp;
            }
            catch (Exception ex)
            {
                tmp.Nombre = "error";
                Interaction.MsgBox(ex.Message);
                return tmp;
            }
        }

        public static bool addCaja(CajaEntity c)
        {
            try
            {
                using (CentrexDbContext context = new CentrexDbContext())
                {
                    var cajaEntity = new CajaEntity()
                    {
                        Nombre = c.Nombre,
                        EsCc = c.EsCc,
                        Activo = c.Activo
                    };

                    context.CajaEntity.Add(cajaEntity);
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

        public static bool updateCaja(CajaEntity c, bool borra = false)
        {
            try
            {
                using (CentrexDbContext context = new CentrexDbContext())
                {
                    var cajaEntity = context.CajaEntity.FirstOrDefault(ce => ce.IdCaja == c.IdCaja);

                    if (cajaEntity is not null)
                    {
                        if (borra == true)
                        {
                            cajaEntity.Activo = false;
                        }
                        else
                        {
                            cajaEntity.Nombre = c.Nombre;
                            cajaEntity.EsCc = c.EsCc;
                            cajaEntity.Activo = c.Activo;
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

        public static bool borrarCaja(CajaEntity c)
        {
            try
            {
                using (CentrexDbContext context = new CentrexDbContext())
                {
                    var cajaEntity = context.CajaEntity.FirstOrDefault(ce => ce.IdCaja == c.IdCaja);

                    if (cajaEntity is not null)
                    {
                        context.CajaEntity.Remove(cajaEntity);
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
