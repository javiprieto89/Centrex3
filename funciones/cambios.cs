using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Centrex.Funciones
{

    static class cambios
    {
        public static bool haycambios()
        {
            bool tmp;
            try
            {
                using (CentrexDbContext context = new CentrexDbContext())
                {
                    tmp = context.CambioEntity.Any(c => c.Activo == true);
                    return tmp;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                tmp = false;
                return tmp;
            }
        }

        public static bool archivarcambios()
        {
            try
            {
                using (CentrexDbContext context = new CentrexDbContext())
                {
                    var cambiosActivos = context.CambioEntity.Where(c => c.Activo == true).ToList();

                    foreach (var cambio in cambiosActivos)
                        cambio.Activo = false;

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
