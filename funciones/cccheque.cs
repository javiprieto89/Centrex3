using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Centrex.Funciones
{
         /// <summary>
        /// Obtiene la información completa de un cheque por su ID.
        /// </summary>
        public static class cccheque
        {
            /// <summary>
            /// Obtiene la información completa de un cheque por su ID.
            /// </summary>
            public static ChequeEntity info_cheque(string id)
            {
                try
                {
                    using var ctx = new CentrexDbContext();
                    int idCh = int.TryParse(id, out var v) ? v : 0;

                    var cheque = ctx.ChequeEntity
                        .Include(c => c.IdClienteNavigation)
                        .Include(c => c.IdProveedorNavigation)
                        .Include(c => c.IdBancoNavigation)
                        .Include(c => c.IdCuentaBancariaNavigation)
                        .Include(c => c.IdEstadochNavigation)
                        .AsNoTracking()
                        .FirstOrDefault(c => c.IdCheque == idCh);

                    return cheque ?? new ChequeEntity();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error en cccheque.info_cheque: {ex.Message}");
                    return new ChequeEntity();
                }
            }
        }
    }

