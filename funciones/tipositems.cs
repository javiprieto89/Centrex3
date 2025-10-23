using System;
using System.Linq;
using System.Xml.Linq;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Centrex.Models;

namespace Centrex
{

    static class tipositems
    {
        // ************************************ FUNCIONES DE TIPOS DE ITEMS ***********************
        public static tipoitem InfoTipoItem(object id_tipo)
        {
            var tmp = new tipoitem();
            try
            {
                using (CentrexDbContext context = GetDbContext())
                {
                    var tipoEntity = context.TiposItems.FirstOrDefault(t => t.IdTipo == Conversions.ToInteger(id_tipo));

                    if (tipoEntity is not null)
                    {
                        tmp.id_tipo = tipoEntity.IdTipo.ToString();
                        tmp.tipo = tipoEntity.tipo;
                        tmp.activo = tipoEntity.activo;
                    }
                    else
                    {
                        tmp.tipo = "error";
                    }
                }
                return tmp;
            }
            catch (Exception ex)
            {
                Interaction.MsgBox(ex.Message.ToString());
                tmp.tipo = "error";
                return tmp;
            }
        }

        public static bool AddTipoItem(tipoitem titem)
        {
            try
            {
                using (CentrexDbContext context = GetDbContext())
                {
                    var tipoEntity = new TipoItemEntity()
                    {
                        tipo = titem.tipo,
                        activo = titem.activo
                    };

                    context.TiposItems.Add(tipoEntity);
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

        public static bool UpdateTipoItem(TipoItemEntity titem, bool borra = false)
        {
            try
            {
                using (CentrexDbContext context = GetDbContext())
                {
                    var tipoEntity = context.TiposItems.FirstOrDefault(t => t.IdTipo == titem.id_tipo);

                    if (tipoEntity is not null)
                    {
                        if (borra == true)
                        {
                            tipoEntity.activo = false;
                        }
                        else
                        {
                            tipoEntity.tipo = titem.tipo;
                            tipoEntity.activo = titem.activo;
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

        public static bool BorrarTipoItem(TipoItemEntity titem)
        {
            try
            {
                using (CentrexDbContext context = GetDbContext())
                {
                    var tipoEntity = context.TiposItems.FirstOrDefault(t => t.IdTipo == titem.id_tipo);

                    if (tipoEntity is not null)
                    {
                        context.TiposItems.Remove(tipoEntity);
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

        // ************************************ FUNCIONES DE TIPOS DE ITEMS ***********************
    }
}
