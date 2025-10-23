using System;
using System.Data;
using System.Linq;
using Microsoft.VisualBasic;
using Centrex.Models;

namespace Centrex
{

    static class cambios
    {
        public static bool haycambios()
        {
            bool tmp;
            try
            {
                using (CentrexDbContext context = GetDbContext())
                {
                    tmp = context.Cambios.Any(c => c.activo == true);
                    return tmp;
                }
            }
            catch (Exception ex)
            {
                tmp = false;
                return tmp;
            }
        }

        public static bool archivarcambios()
        {
            try
            {
                using (CentrexDbContext context = GetDbContext())
                {
                    var cambiosActivos = context.Cambios.Where(c => c.activo == true).ToList();

                    foreach (var cambio in cambiosActivos)
                        cambio.activo = false;

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
    }
}
