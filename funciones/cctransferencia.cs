using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Centrex.Funciones
{
    public static class cctransferencia
    {
        /// <summary>
        /// Obtiene la transferencia temporal (tmp_transferencia) por su ID.
        /// </summary>
        public static TmpTransferenciaEntity InfoTmpTransferencia(string id)
        {
            try
            {
                using var ctx = new CentrexDbContext();
                int idT = int.TryParse(id, out var v) ? v : 0;

                var transf = ctx.TmpTransferenciaEntity
                    .Include(t => t.IdCuentaBancariaNavigation)
                        .ThenInclude(cb => cb.IdBancoNavigation)
                    .AsNoTracking()
                    .FirstOrDefault(t => t.IdTmpTransferencia == idT);

                return transf ?? new TmpTransferenciaEntity();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en cctransferencia.InfoTmpTransferencia: {ex.Message}");
                return new TmpTransferenciaEntity();
            }
        }
    }
}
