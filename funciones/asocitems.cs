using System;
using System.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Centrex.Models;

namespace Centrex.Funciones
{
    static class asocitems
    {
        // ************************************ FUNCIONES DE ITEMS ASOCIADOS ***************************

        public static AsocItemEntity info_asocItem(int IdItem, int id_asocItem)
        {
            var tmp = new AsocItemEntity();
            try
            {
                using (var ctx = new CentrexDbContext())
                {
                    var ent = ctx.AsocItemEntity
                        .AsNoTracking()
                        .FirstOrDefault(a => a.IdItem == IdItem
                                          && a.IdItemAsoc == id_asocItem);

                    if (ent is not null)
                    {
                        return ent;
                    }
                    else 
                    {
                        tmp.IdItem = -1;   
                        return tmp;
                    }
                }
            }
            catch (Exception ex)
            {
                Interaction.MsgBox(ex.Message);
                tmp.IdItem = -1;
                return tmp;
            }
        }

        public static bool addAsocItem(AsocItemEntity it)
        {
            try
            {
                using (var ctx = new CentrexDbContext())
                {
                    ctx.AsocItemEntity.Add(new AsocItemEntity
                    {
                        IdItem = it.IdItem,
                        IdItemAsoc = it.IdItemAsoc,
                        Cantidad = it.Cantidad
                    });
                    ctx.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Interaction.MsgBox(ex.Message);
                return false;
            }
        }

        public static bool updateAsocItem(AsocItemEntity it, bool borra = false)
        {
            try
            {
                using (var ctx = new CentrexDbContext())
                {
                    var ent = ctx.AsocItemEntity
                        .FirstOrDefault(a => a.IdItem == it.IdItem && a.IdItemAsoc == it.IdItemAsoc);

                    if (ent is null)
                        return false;

                    ent.Cantidad = it.Cantidad;
                    ctx.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Interaction.MsgBox(ex.Message);
                return false;
            }
        }

        public static bool borrarAsocItem(AsocItemEntity it)
        {
            try
            {
                using (var ctx = new CentrexDbContext())
                {
                    var ent = ctx.AsocItemEntity
                        .FirstOrDefault(a => a.IdItem == it.IdItem && a.IdItemAsoc == it.IdItemAsoc);

                    if (ent is null)
                        return false;

                    ctx.AsocItemEntity.Remove(ent);
                    ctx.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Interaction.MsgBox(ex.Message);
                return false;
            }
        }

        public static bool Tiene_Items_Asociados(int IdItem)
        {
            try
            {
                using (var ctx = new CentrexDbContext())
                {
                    return ctx.AsocItemEntity
                        .AsNoTracking()
                        .Any(a => a.IdItem == IdItem);
                }
            }
            catch (Exception ex)
            {
                Interaction.MsgBox(ex.Message);
                return false;
            }
        }

        public static DataGridQueryResult Traer_Cantidades_Enviadas_Items_Asociados_Default_Produccion(string IdItem, int id_produccion = -1)
        {
            try
            {
                using (var ctx = new CentrexDbContext())
                {
                    var result = new DataGridQueryResult();
                    result.EsMaterializada = true; // la query se ejecuta y el resultado se guarda en DataMaterializada

                    if (id_produccion == -1)
                    {
                        var data = (from ai in ctx.AsocItemEntity
                                    join tpi in ctx.TmpProduccionAsocItemEntity on ai.IdItem equals tpi.IdItem
                                    join i in ctx.ItemEntity on ai.IdItem equals i.IdItem
                                    join ii in ctx.ItemEntity on ai.IdItemAsoc equals ii.IdItem
                                    where ai.IdItem == Conversions.ToInteger(IdItem)
                                    select new
                                    {
                                        Producto = i.Descript,
                                        Cantidad = tpi.CantidadItemAsocEnviada,
                                        Producto_asociado = ii.Descript,
                                        Cantidad_enviada = ai.Cantidad * tpi.CantidadItemAsocEnviada,
                                        IdTmpProduccionItem = tpi.IdTmpProduccionItem,
                                        ai.IdItem,
                                        ai.IdItemAsoc
                                    }).ToList();

                        result.DataMaterializada = data;
                    }
                    else
                    {
                        var data = (from pai in ctx.ProduccionAsocItemEntity
                                    join pi in ctx.ProduccionItemEntity on pai.IdItem equals pi.IdItem
                                    join i in ctx.ItemEntity on pai.IdItem equals i.IdItem
                                    join ii in ctx.ItemEntity on pai.IdItemAsoc equals ii.IdItem
                                    where pai.IdItem == Conversions.ToInteger(IdItem)
                                          && pai.IdProduccion == id_produccion
                                    select new
                                    {
                                        Producto = i.Descript,
                                        Cantidad = pi.Cantidad,
                                        Producto_asociado = ii.Descript,
                                        Cantidad_enviada = pai.CantidadItemAsocEnviada,
                                        pai.IdItem,
                                        pai.IdItemAsoc
                                    }).Distinct().ToList();

                        result.DataMaterializada = data;
                    }

                    return result;
                }
            }
            catch (Exception ex)
            {
                Interaction.MsgBox(ex.Message);
                // devuelve objeto vacío compatible
                return new DataGridQueryResult
                {
                    EsMaterializada = true,
                    DataMaterializada = new object[0]
                };
            }
        }
    }
}
