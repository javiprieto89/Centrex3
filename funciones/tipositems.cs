using System;
using System.Linq;

namespace Centrex.Funciones
{

    static class tipositems
    {
        // ************************************ FUNCIONES DE TIPOS DE ITEMS ***********************
        public static TipoItemEntity info_tipoitem(object IdTipo)
        {
            var tmp = new TipoItemEntity();
            try
            {
                using (CentrexDbContext context = new CentrexDbContext())
                {
                    var tipoEntity = context.TipoItemEntity.FirstOrDefault(t => t.IdTipo == Conversions.ToInteger(IdTipo));

                    if (tipoEntity is not null)
                    {
                        tmp.IdTipo = tipoEntity.IdTipo;
                        tmp.Tipo = tipoEntity.Tipo;
                        tmp.Activo = tipoEntity.Activo;
                    }
                    else
                    {
                        tmp.Tipo = "error";
                    }
                }
                return tmp;
            }
            catch (Exception ex)
            {
                Interaction.MsgBox(ex.Message.ToString());
                tmp.Tipo = "error";
                return tmp;
            }
        }

        public static bool AddTipoItem(TipoItemEntity titem)
        {
            try
            {
                using (CentrexDbContext context = new CentrexDbContext())
                {
                    var tipoEntity = new TipoItemEntity()
                    {
                        Tipo = titem.Tipo,
                        Activo = titem.Activo
                    };

                    context.TipoItemEntity.Add(tipoEntity);
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

        public static bool updatetipoitem(TipoItemEntity titem, bool borra = false)
        {
            try
            {
                using (CentrexDbContext context = new CentrexDbContext())
                {
                    var tipoEntity = context.TipoItemEntity.FirstOrDefault(t => t.IdTipo == titem.IdTipo);

                    if (tipoEntity is not null)
                    {
                        if (borra == true)
                        {
                            tipoEntity.Activo = false;
                        }
                        else
                        {
                            tipoEntity.Tipo = titem.Tipo;
                            tipoEntity.Activo = titem.Activo;
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
                using (CentrexDbContext context = new CentrexDbContext())
                {
                    var tipoEntity = context.TipoItemEntity.FirstOrDefault(t => t.IdTipo == titem.IdTipo);

                    if (tipoEntity is not null)
                    {
                        context.TipoItemEntity.Remove(tipoEntity);
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
