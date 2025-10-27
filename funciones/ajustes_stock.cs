using System;
using System.Linq;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Centrex.Models;

namespace Centrex
{

    static class ajustes_stock
    {
        // ************************************ FUNCIONES DE RELACION ITEMS E IMPUESTOS ********************
        public static ajusteStock info_ajuste_stock(string id_ajusteStock)
        {
            var tmp = new ajusteStock();

            try
            {
                using (CentrexDbContext context = GetDbContext())
                {
                    var ajusteEntity = context.AjusteStockEntity.FirstOrDefault(a => a.IdAjusteStock == Conversions.ToInteger(id_ajusteStock));

                    if (ajusteEntity is not null)
                    {
                        tmp.IdAjusteStock = ajusteEntity.IdAjusteStock  .ToString();
                        tmp.Fecha = Conversions.ToString(ajusteEntity.fecha.ToString()[Conversions.ToInteger("dd/MM/yyyy")]);
                        tmp.IdItem = ajusteEntity.IdItem.ToString();
                        tmp.cantidad = Conversions.ToInteger(ajusteEntity.cantidad.ToString());
                        tmp.notas = ajusteEntity.notas;
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

        public static bool add_ajusteStock(ajusteStock _as)
        {
            try
            {
                using (CentrexDbContext context = GetDbContext())
                {
                    var ajusteEntity = new AjusteStockEntity()
                    {
                        fecha = DateTime.ParseExact(_as.fecha, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture),
                        IdItem = _as.id_item,
                        cantidad = (int)Math.Round((decimal)_as.cantidad),
                        notas = _as.notas
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
