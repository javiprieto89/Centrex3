using System;
using System.Linq;

namespace Centrex.Funciones
{    public static class ajustes_stock
    {
        // ************************************ FUNCIONES DE RELACION ITEMS E IMPUESTOS ********************
        public static AjusteStockEntity info_ajuste_stock(int _as)
        {
            var tmp = new AjusteStockEntity();

            try
            {
                using (var context = new CentrexDbContext())
                {
                    
                    if (_as != 0 && _as != -1)
                    {
                        var AjusteStockEntity = context.AjusteStockEntity.FirstOrDefault(i => i.IdAjusteStock == _as);
                        tmp.IdAjusteStock = AjusteStockEntity.IdAjusteStock;
                        tmp.Fecha = AjusteStockEntity.Fecha;
                        tmp.IdItem = AjusteStockEntity.IdItem;
                        tmp.Cantidad = AjusteStockEntity.Cantidad;
                        tmp.Notas = AjusteStockEntity.Notas;
                    }
                }

                return tmp;
            }
            catch (Exception ex)
            {
                Interaction.MsgBox(ex.Message.ToString());
                return tmp;
            }
        }

        public static bool add_ajusteStock(AjusteStockEntity _as)
        {
            try
            {
                using (var context = new CentrexDbContext())
                {
                    var ajusteEntity = new AjusteStockEntity()
                    {
                        Fecha = _as.Fecha,
                        IdItem = _as.IdItem,
                        Cantidad = _as.Cantidad,
                        Notas = _as.Notas
                    };

                    context.AjusteStockEntity.Add(ajusteEntity);
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
        // ************************************ FUNCIONES DE RELACION ITEMS E IMPUESTOS ********************
    }
}
