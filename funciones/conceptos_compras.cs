using System;
using System.Linq;

namespace Centrex.Funciones
{

    static class conceptos_compra
    {

        // ************************************ FUNCIONES DE CONCEPTOS DE COMPRA **********************
        public static ConceptoCompraEntity info_concepto_compra(string id_concepto)
        {
            var tmp = new ConceptoCompraEntity();

            try
            {
                using (CentrexDbContext context = new CentrexDbContext())
                {
                    var conceptoEntity = context.ConceptoCompraEntity.FirstOrDefault(c => c.IdConceptoCompra == Conversions.ToInteger(id_concepto));

                    if (conceptoEntity is not null)
                    {
                        tmp.IdConceptoCompra = conceptoEntity.IdConceptoCompra;
                        tmp.Concepto = conceptoEntity.Concepto;
                        tmp.Activo = conceptoEntity.Activo;
                    }
                    else
                    {
                        tmp.Concepto = "error";
                    }
                }

                return tmp;
            }
            catch (Exception ex)
            {
                tmp.Concepto = "error";
                return tmp;
            }
        }

        public static bool addConcepto_compra(ConceptoCompraEntity Concepto)
        {
            try
            {
                using (CentrexDbContext context = new CentrexDbContext())
                {
                    var conceptoEntity = new ConceptoCompraEntity()
                    {
                        Concepto = Concepto.Concepto,
                        Activo = Concepto.Activo
                    };

                    context.ConceptoCompraEntity.Add(conceptoEntity);
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

        public static bool updateConcepto_compra(ConceptoCompraEntity Concepto, bool borra = false)
        {
            try
            {
                using (CentrexDbContext context = new CentrexDbContext())
                {
                    var conceptoEntity = context.ConceptoCompraEntity.FirstOrDefault(c => c.IdConceptoCompra == Concepto.IdConceptoCompra);

                    if (conceptoEntity is not null)
                    {
                        if (borra == true)
                        {
                            conceptoEntity.Activo = false;
                        }
                        else
                        {
                            conceptoEntity.Concepto = Concepto.Concepto;
                            conceptoEntity.Activo = Concepto.Activo;
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

        public static bool borrarConcepto_compra(ConceptoCompraEntity Concepto)
        {
            try
            {
                using (CentrexDbContext context = new ())
                {
                    var conceptoEntity = context.ConceptoCompraEntity.FirstOrDefault(c => c.IdConceptoCompra == Concepto.IdConceptoCompra);

                    if (conceptoEntity is not null)
                    {


                        context.ConceptoCompraEntity.Remove(conceptoEntity);
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
