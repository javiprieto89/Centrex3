using System;
using System.Linq;
using System.Xml.Linq;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Centrex.Models;

namespace Centrex.Funciones
{

    static class impuestos
    {

        // ************************************ FUNCIONES DE IMPUESTOS **********************
        public static ImpuestoEntity info_impuesto(object IdImpuesto)
        {
            var tmp = new ImpuestoEntity();
            try
            {
                using (CentrexDbContext context = new CentrexDbContext())
                {
                    var impuestoEntity = context.ImpuestoEntity.FirstOrDefault(i => i.IdImpuesto == Conversions.ToInteger(IdImpuesto));

                    if (impuestoEntity is not null)
                    {
                        tmp.IdImpuesto = impuestoEntity.IdImpuesto;
                        tmp.Nombre = impuestoEntity.Nombre;
                        tmp.Porcentaje = impuestoEntity.Porcentaje;
                        tmp.EsRetencion = (bool)(impuestoEntity.EsRetencion == null ? false : impuestoEntity.EsRetencion);
                        tmp.EsPercepcion = (bool)(impuestoEntity.EsPercepcion == null ? false : impuestoEntity.EsPercepcion);
                        tmp.Activo = impuestoEntity.Activo;
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
                Interaction.MsgBox(ex.Message.ToString());
                tmp.Nombre = "error";
                return tmp;
            }
        }


        public static int addImpuesto(ImpuestoEntity i)
        {
            try
            {
                using (CentrexDbContext context = new CentrexDbContext())
                {
                    var impuestoEntity = new ImpuestoEntity()
                    {
                        Nombre = i.Nombre,
                        Porcentaje = i.Porcentaje,
                        EsRetencion = i.EsRetencion,
                        EsPercepcion = i.EsPercepcion,
                        Activo = i.Activo
                    };

                    context.ImpuestoEntity.Add(impuestoEntity);
                    context.SaveChanges();

                    // Retornar el ID generado
                    return impuestoEntity.IdImpuesto;
                }
            }
            catch (Exception ex)
            {
                Interaction.MsgBox(ex.Message);
                return -1;
            }
        }

        public static bool updateImpuesto(ImpuestoEntity i, bool borra = false)
        {
            try
            {
                using (CentrexDbContext context = new CentrexDbContext())
                {
                    var impuestoEntity = context.ImpuestoEntity.FirstOrDefault(imp => imp.IdImpuesto == i.IdImpuesto);

                    if (impuestoEntity is not null)
                    {
                        if (borra == true)
                        {
                            impuestoEntity.Activo = false;
                        }
                        else
                        {
                            impuestoEntity.Nombre = i.Nombre;
                            impuestoEntity.Porcentaje = i.Porcentaje;
                            impuestoEntity.EsRetencion = i.EsRetencion;
                            impuestoEntity.EsPercepcion = i.EsPercepcion;
                            impuestoEntity.Activo = i.Activo;
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

        public static bool borrarImpuesto(ImpuestoEntity i)
        {
            try
            {
                using (CentrexDbContext context = new CentrexDbContext())
                {
                    var impuestoEntity = context.ImpuestoEntity.FirstOrDefault(imp => imp.IdImpuesto == i.IdImpuesto);

                    if (impuestoEntity is not null)
                    {
                        context.ImpuestoEntity.Remove(impuestoEntity);
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
        // ************************************ FUNCIONES DE IMPUESTOS **********************
    }
}
