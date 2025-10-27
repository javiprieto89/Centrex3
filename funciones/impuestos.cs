using System;
using System.Linq;
using System.Xml.Linq;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Centrex.Models;

namespace Centrex
{

    static class impuestos
    {

        // ************************************ FUNCIONES DE IMPUESTOS **********************
        public static impuesto info_impuesto(object id_impuesto)
        {
            var tmp = new impuesto();
            try
            {
                using (CentrexDbContext context = GetDbContext())
                {
                    var impuestoEntity = context.ImpuestoEntity.FirstOrDefault(i => i.IdImpuesto == Conversions.ToInteger(id_impuesto));

                    if (impuestoEntity is not null)
                    {
                        tmp.id_impuesto = impuestoEntity.IdImpuesto.ToString();
                        tmp.nombre = impuestoEntity.Nombre;
                        tmp.porcentaje = Conversions.ToDouble(impuestoEntity.Porcentaje == null ? "0" : impuestoEntity.Porcentaje.ToString());
                        tmp.esRetencion = (bool)(impuestoEntity.EsRetencion == null ? false : impuestoEntity.EsRetencion);
                        tmp.esPercepcion = (bool)(impuestoEntity.EsPercepcion == null ? false : impuestoEntity.EsPercepcion);
                        tmp.activo = impuestoEntity.Activo;
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
                Interaction.MsgBox(ex.Message.ToString());
                tmp.nombre = "error";
                return tmp;
            }
        }


        public static int addImpuesto(impuesto i)
        {
            try
            {
                using (CentrexDbContext context = GetDbContext())
                {
                    var impuestoEntity = new ImpuestoEntity()
                    {
                        Nombre = i.nombre,
                        Porcentaje = i.porcentaje != Conversions.ToDouble("") ? (decimal)i.porcentaje : 0m,
                        EsRetencion = i.esRetencion,
                        EsPercepcion = i.esPercepcion,
                        Activo = i.activo
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

        public static bool updateImpuesto(impuesto i, bool borra = false)
        {
            try
            {
                using (CentrexDbContext context = GetDbContext())
                {
                    var impuestoEntity = context.ImpuestoEntity.FirstOrDefault(imp => imp.IdImpuesto == i.id_impuesto);

                    if (impuestoEntity is not null)
                    {
                        if (borra == true)
                        {
                            impuestoEntity.Activo = false;
                        }
                        else
                        {
                            impuestoEntity.Nombre = i.nombre;
                            impuestoEntity.Porcentaje = i.porcentaje != Conversions.ToDouble("") ? (decimal)i.porcentaje : 0m;
                            impuestoEntity.EsRetencion = i.esRetencion;
                            impuestoEntity.EsPercepcion = i.esPercepcion;
                            impuestoEntity.Activo = i.activo;
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

        public static bool borrarImpuesto(impuesto i)
        {
            try
            {
                using (CentrexDbContext context = GetDbContext())
                {
                    var impuestoEntity = context.ImpuestoEntity.FirstOrDefault(imp => imp.IdImpuesto == i.id_impuesto);

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
