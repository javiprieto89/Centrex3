using System;
using System.Linq;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Centrex.Models;

namespace Centrex
{

    static class precios
    {
        public static bool updatePrecios_items(string id_item, string precio)
        {
            try
            {
                using (CentrexDbContext context = GetDbContext())
                {
                    var itemEntity = context.Items.FirstOrDefault(i => i.IdItem == Conversions.ToInteger(id_item));

                    if (itemEntity is not null)
                    {
                        itemEntity.precio_lista = Conversions.ToDecimal(precio);
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

    }
}
