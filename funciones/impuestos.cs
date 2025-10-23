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
                    var impuestoEntity = context.Impuestos.FirstOrDefault(i => i.IdImpuesto == Conversions.ToInteger(id_impuesto));

                    if (impuestoEntity is not null)
                    {
                        tmp.id_impuesto = impuestoEntity.IdImpuesto.ToString();
                        tmp.nombre = impuestoEntity.nombre;
                        tmp.porcentaje = Conversions.ToDouble(impuestoEntity.porcentaje == null ? "0" : impuestoEntity.porcentaje.ToString());
                        tmp.esRetencion = (bool)(impuestoEntity.esRetencion == null ? false : impuestoEntity.esRetencion);
                        tmp.esPercepcion = (bool)(impuestoEntity.esPercepcion == null ? false : impuestoEntity.esPercepcion);
                        tmp.activo = impuestoEntity.activo;
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
                        nombre = i.nombre,
                        porcentaje = i.porcentaje != Conversions.ToDouble("") ? (decimal)i.porcentaje : 0m,
                        esRetencion = i.esRetencion,
                        esPercepcion = i.esPercepcion,
                        activo = i.activo
                    };

                    context.Impuestos.Add(impuestoEntity);
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
                    var impuestoEntity = context.Impuestos.FirstOrDefault(imp => imp.IdImpuesto == i.id_impuesto);

                    if (impuestoEntity is not null)
                    {
                        if (borra == true)
                        {
                            impuestoEntity.activo = false;
                        }
                        else
                        {
                            impuestoEntity.nombre = i.nombre;
                            impuestoEntity.porcentaje = i.porcentaje != Conversions.ToDouble("") ? (decimal)i.porcentaje : 0m;
                            impuestoEntity.esRetencion = i.esRetencion;
                            impuestoEntity.esPercepcion = i.esPercepcion;
                            impuestoEntity.activo = i.activo;
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
                    var impuestoEntity = context.Impuestos.FirstOrDefault(imp => imp.IdImpuesto == i.id_impuesto);

                    if (impuestoEntity is not null)
                    {
                        context.Impuestos.Remove(impuestoEntity);
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
