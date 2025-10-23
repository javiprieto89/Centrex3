using System;
using System.Linq;
using System.Xml.Linq;
using Microsoft.VisualBasic;
using Centrex.Models;

namespace Centrex
{

    static class cobros_retenciones
    {

        // ************************************
        // FUNCIONES DE RETENCIONES EN COBROS (Entity Framework)
        // ************************************

        public static CobroRetencionEntity info_cobroRetencion(int id_retencion)
        {
            try
            {
                using (var context = new CentrexDbContext())
                {
                    return context.CobrosRetenciones.FirstOrDefault(r => r.IdRetencion == id_retencion);
                }
            }
            catch (Exception ex)
            {
                Interaction.MsgBox(ex.Message);
                return null;
            }
        }

        public static TmpCobroRetencionEntity info_tmpCobroRetencion(int id_retencion)
        {
            try
            {
                using (var context = new CentrexDbContext())
                {
                    return context.TmpCobrosRetenciones.FirstOrDefault(r => r.IdTmpRetencion == id_retencion);
                }
            }
            catch (Exception ex)
            {
                Interaction.MsgBox(ex.Message);
                return null;
            }
        }

        public static bool addCobroRetencion(CobroRetencionEntity cb)
        {
            try
            {
                using (var context = new CentrexDbContext())
                {
                    context.CobrosRetenciones.Add(cb);
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

        public static bool addTmpCobroRetencion(TmpCobroRetencionEntity cb)
        {
            try
            {
                using (var context = new CentrexDbContext())
                {
                    context.TmpCobrosRetenciones.Add(cb);
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

        public static bool guardarCobroRetencion(CobroEntity c)
        {
            try
            {
                using (var context = new CentrexDbContext())
                {
                    // Obtener todos los temporales y migrarlos a definitivos
                    var tmpList = context.TmpCobrosRetenciones.ToList();

                    foreach (var tmp in tmpList)
                    {
                        var nuevo = new CobroRetencionEntity()
                        {
                            IdCobro = c.IdCobro,
                            IdImpuesto = tmp.IdImpuesto,
                            total = tmp.total,
                            notas = tmp.notas
                        };
                        context.CobrosRetenciones.Add(nuevo);
                    }

                    context.SaveChanges();

                    // Borrar temporales
                    context.TmpCobrosRetenciones.RemoveRange(tmpList);
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

        public static bool updateTmpCobroRetencion(TmpCobroRetencionEntity cb)
        {
            try
            {
                using (var context = new CentrexDbContext())
                {
                    var entity = context.TmpCobrosRetenciones.FirstOrDefault(r => r.IdTmpRetencion == cb.IdTmpRetencion);
                    if (entity is null)
                        return false;

                    entity.IdImpuesto = cb.IdImpuesto;
                    entity.total = cb.total;
                    entity.notas = cb.notas;

                    context.Entry(entity).State = EntityState.Modified;
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

        public static bool borrarTmpCobroRetencion(int id_retencion)
        {
            try
            {
                using (var context = new CentrexDbContext())
                {
                    var entity = context.TmpCobrosRetenciones.FirstOrDefault(r => r.IdTmpRetencion == id_retencion);
                    if (entity is null)
                        return false;

                    context.TmpCobrosRetenciones.Remove(entity);
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

        public static bool borrarTmpCobroRetencion(TmpCobroRetencionEntity cb)
        {
            return borrarTmpCobroRetencion(cb.IdTmpRetencion);
        }

        public static bool borrarCobroRetencion(int id_retencion)
        {
            try
            {
                using (var context = new CentrexDbContext())
                {
                    var entity = context.CobrosRetenciones.FirstOrDefault(r => r.IdRetencion == id_retencion);
                    if (entity is null)
                        return false;

                    context.CobrosRetenciones.Remove(entity);
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

        public static bool borrarCobroRetencion(CobroRetencionEntity cb)
        {
            return borrarCobroRetencion(cb.IdRetencion);
        }

    }
}
