using System;
using System.Data;
using System.Linq;
using System.Xml.Linq;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Centrex.Models;

namespace Centrex
{

    static class asocitems
    {
        // ************************************ FUNCIONES DE ITEMS ASOCIADOS ***************************
        public static asocItem info_asocItem(string id_item, string id_asocItem)
        {
            var tmp = new asocItem();
            try
            {
                using (var ctx = new CentrexDbContext())
                {
                    var ent = ctx.AsocItems.AsNoTracking().FirstOrDefault(a => a.IdItem == Conversions.ToInteger(id_item) && a.IdItemAsoc == Conversions.ToInteger(id_asocItem));
                    if (ent is null)
                    {
                        tmp.id_item = -1;
                        return tmp;
                    }
                    tmp.id_item = ent.IdItem;
                    tmp.id_item_asoc = ent.IdItemAsoc;
                    tmp.cantidad = ent.cantidad;
                    return tmp;
                }
            }
            catch (Exception ex)
            {
                Interaction.MsgBox(ex.Message.ToString());
                tmp.id_item = -1;
                return tmp;
            }
        }

        public static bool addAsocItem(asocItem it)
        {
            try
            {
                using (var ctx = new CentrexDbContext())
                {
                    var ent = new AsocItemEntity()
                    {
                        IdItem = it.id_item,
                        IdItemAsoc = it.id_item_asoc,
                        cantidad = it.cantidad
                    };
                    ctx.AsocItems.Add(ent);
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

        public static bool updateAsocItem(asocItem it, bool borra = false)
        {
            try
            {
                using (var ctx = new CentrexDbContext())
                {
                    var ent = ctx.AsocItems.FirstOrDefault(a => a.IdItem == it.id_item && a.IdItemAsoc == it.id_item_asoc);
                    if (ent is null)
                        return false;
                    ent.cantidad = it.cantidad;
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

        public static bool borrarAsocItem(asocItem it)
        {
            try
            {
                using (var ctx = new CentrexDbContext())
                {
                    var ent = ctx.AsocItems.FirstOrDefault(a => a.IdItem == it.id_item && a.IdItemAsoc == it.id_item_asoc);
                    if (ent is null)
                        return false;
                    ctx.AsocItems.Remove(ent);
                    ctx.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Interaction.MsgBox(ex.Message.ToString());
                return false;
            }
        }

        public static bool Tiene_Items_Asociados(string id_item)
        {
            try
            {
                using (var ctx = new CentrexDbContext())
                {
                    return ctx.AsocItems.AsNoTracking().Any(a => a.IdItem == Conversions.ToInteger(id_item));
                }
            }
            catch (Exception ex)
            {
                Interaction.MsgBox(ex.Message.ToString());
                return false;
            }
        }

        public static DataTable Traer_Cantidades_Enviadas_Items_Asociados_Default_Produccion(string id_item, int id_produccion = -1)
        {
            string sqlstr;

            // sqlstr = "SELECT i.item AS 'Producto', tpi.cantidad AS 'Cantidad', ii.descript AS 'Producto asociado', ai.cantidad * tpi.cantidad  AS 'Cantidad enviada', " &
            // "tpi.id_tmpProduccionItem AS 'id_tmpProduccionItem', ai.id_item AS 'id_item', ai.id_item_asoc AS 'id_item_asoc' " &
            // "FROM asocItems AS ai " &
            // "INNER JOIN tmpproduccion_items AS tpi ON ai.id_item = tpi.id_item " &
            // "INNER JOIN items AS i ON ai.id_item = i.id_item " &
            // "INNER JOIN items AS ii ON ai.id_item_asoc = ii.id_item " &
            // "WHERE ai.id_item = '" + id_item + "'"


            if (id_produccion == -1)
            {
                sqlstr = "SELECT i.item AS 'Producto', tpi.cantidad AS 'Cantidad', ii.descript AS 'Producto asociado', ai.cantidad * tpi.cantidad  AS 'Cantidad enviada', " + "tpi.id_tmpProduccionItem AS 'id_tmpProduccionItem', ai.id_item AS 'id_item', ai.id_item_asoc AS 'id_item_asoc' " + "FROM asocItems AS ai " + "INNER JOIN tmpproduccion_items AS tpi ON ai.id_item = tpi.id_item " + "INNER JOIN items AS i ON ai.id_item = i.id_item " + "INNER JOIN items AS ii ON ai.id_item_asoc = ii.id_item " + "WHERE ai.id_item = '" + id_item + "'";
            }
            else
            {
                sqlstr = "SELECT DISTINCT i.item AS 'Producto', pi.cantidad AS 'Cantidad', ii.descript AS 'Producto asociado', pai.cantidad_item_asoc_enviada  AS 'Cantidad enviada', " + "pai.id_item AS 'id_item', pai.id_item_asoc AS 'id_item_asoc' " + "FROM produccion_asocItems AS pai " + "INNER JOIN produccion_items AS pi ON pai.id_item = pi.id_item " + "INNER JOIN items AS i ON pai.id_item = i.id_item " + "INNER JOIN items AS ii ON pai.id_item_asoc = ii.id_item " + "WHERE pai.id_item = '" + id_item + "' AND pai.id_produccion = '" + id_produccion.ToString() + "'";
            }
            try
            {
                using (var ctx = new CentrexDbContext())
                {
                    if (id_produccion == -1)
                    {
                        var query = from ai in ctx.AsocItems.AsNoTracking()
                                    join tpi in ctx.TmpProduccionItems.AsNoTracking() on ai.IdItem equals tpi.IdItem
                                    join i in ctx.Items.AsNoTracking() on ai.IdItem equals i.IdItem
                                    join ii in ctx.Items.AsNoTracking() on ai.IdItemAsoc equals ii.IdItem
                                    where ai.IdItem == Conversions.ToInteger(id_item)
                                    select new
                                    {
                                        Producto = i.item,
                                        Cantidad = tpi.cantidad,
                                        Producto_asociado = ii.descript,
                                        Cantidad_enviada = ai.cantidad * tpi.cantidad,
                                        id_tmpProduccionItem = tpi.IdTmpProduccionItem,
                                        id_item = ai.IdItem,
                                        id_item_asoc = ai.IdItemAsoc
                                    };
                        return ToDataTable(query);
                    }
                    else
                    {
                        var query2 = (from pai in ctx.ProduccionAsocItems.AsNoTracking()
                                      join pi in ctx.ProduccionItems.AsNoTracking() on pai.id_item equals pi.id_item
                                      join i in ctx.Items.AsNoTracking() on pai.id_item equals i.IdItem
                                      join ii in ctx.Items.AsNoTracking() on pai.id_item_asoc equals ii.IdItem
                                      where pai.id_item == Conversions.ToInteger(id_item) && pai.id_produccion == id_produccion
                                      select new
                                      {
                                          Producto = i.item,
                                          Cantidad = pi.cantidad,
                                          Producto_asociado = ii.descript,
                                          Cantidad_enviada = pai.cantidad,
                                          pai.id_item,
                                          pai.id_item_asoc
                                      }).Distinct();
                        return ToDataTable(query2);
                    }
                }
            }
            catch (Exception ex)
            {
                Interaction.MsgBox(ex.Message.ToString());
                return null;
            }
        }
    }
}
