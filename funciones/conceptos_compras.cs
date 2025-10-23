using System;
using System.Linq;
using System.Xml.Linq;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Centrex.Models;

namespace Centrex
{

    static class conceptos_compra
    {

        // ************************************ FUNCIONES DE CONCEPTOS DE COMPRA **********************
        public static concepto_compra info_concepto_compra(string id_concepto)
        {
            var tmp = new concepto_compra();

            try
            {
                using (CentrexDbContext context = GetDbContext())
                {
                    var conceptoEntity = context.ConceptosCompra.FirstOrDefault(c => c.IdConcepto == Conversions.ToInteger(id_concepto));

                    if (conceptoEntity is not null)
                    {
                        tmp.id_concepto_compra = conceptoEntity.IdConcepto.ToString();
                        tmp.concepto = conceptoEntity.concepto;
                        tmp.activo = conceptoEntity.activo;
                    }
                    else
                    {
                        tmp.concepto = "error";
                    }
                }

                return tmp;
            }
            catch (Exception ex)
            {
                tmp.concepto = "error";
                return tmp;
            }
        }

        public static bool addConcepto_compra(concepto_compra concepto)
        {
            try
            {
                using (CentrexDbContext context = GetDbContext())
                {
                    var conceptoEntity = new ConceptoCompraEntity()
                    {
                        concepto = concepto.concepto,
                        activo = concepto.activo
                    };

                    context.ConceptosCompra.Add(conceptoEntity);
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

        public static bool updateConcepto_compra(concepto_compra concepto, bool borra = false)
        {
            try
            {
                using (CentrexDbContext context = GetDbContext())
                {
                    var conceptoEntity = context.ConceptosCompra.FirstOrDefault(c => c.IdConcepto == concepto.id_concepto_compra);

                    if (conceptoEntity is not null)
                    {
                        if (borra == true)
                        {
                            conceptoEntity.activo = false;
                        }
                        else
                        {
                            conceptoEntity.concepto = concepto.concepto;
                            conceptoEntity.activo = concepto.activo;
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

        public static bool borrarConcepto_compra(concepto_compra concepto)
        {
            try
            {
                using (CentrexDbContext context = GetDbContext())
                {
                    var conceptoEntity = context.ConceptosCompra.FirstOrDefault(c => c.IdConcepto == concepto.id_concepto_compra);

                    if (conceptoEntity is not null)
                    {
                        context.ConceptosCompra.Remove(conceptoEntity);
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
        // ************************************ FUNCIONES DE CONCEPTOS COMPRA **********************
    }
}
