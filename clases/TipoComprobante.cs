// clases/TipoComprobante.cs
using System;
using System.Linq;
using Centrex.Models;
using Microsoft.VisualBasic;

namespace Centrex
{
    public class TipoComprobante
    {
        public int id_tipoComprobante { get; private set; }
        public string comprobante_AFIP { get; private set; }
        public string id_claseFiscal { get; private set; }
        public char signoProveedor { get; private set; }
        public char signoCliente { get; private set; }
        public bool discriminaIVA { get; private set; }
        public bool esRemito { get; private set; }

        public TipoComprobante()
        {
            Inicializar();
        }

        public TipoComprobante(int idTipoComprobante)
        {
            Inicializar();
            InfoTipoComprobante(idTipoComprobante);
        }

        private void Inicializar()
        {
            id_tipoComprobante = -1;
            comprobante_AFIP = string.Empty;
            id_claseFiscal = string.Empty;
            signoProveedor = '\0';
            signoCliente = '\0';
            discriminaIVA = false;
            esRemito = false;
        }

        private void InfoTipoComprobante(int idTipoComprobante)
        {
            try
            {
                using var ctx = new CentrexDbContext();
                var entity = ctx.TipoComprobanteEntity.FirstOrDefault(tc => tc.IdTipoComprobante == idTipoComprobante);

                if (entity == null)
                {
                    return;
                }

                id_tipoComprobante = entity.IdTipoComprobante;
                comprobante_AFIP = entity.ComprobanteAfip ?? string.Empty;
                id_claseFiscal = entity.IdClaseFiscal ?? string.Empty;
                signoProveedor = ObtenerCaracter(entity.SignoProveedor);
                signoCliente = ObtenerCaracter(entity.SignoCliente);
                discriminaIVA = entity.DiscriminaIva ?? false;
                esRemito = entity.EsRemito ?? false;
            }
            catch (Exception ex)
            {
                Interaction.MsgBox($"Error al obtener el tipo de comprobante: {ex.Message}", MsgBoxStyle.Critical, "Centrex");
                id_tipoComprobante = -1;
            }
        }

        private static char ObtenerCaracter(string? valor)
        {
            return !string.IsNullOrEmpty(valor) ? valor[0] : '\0';
        }
    }
}
