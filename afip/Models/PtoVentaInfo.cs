
namespace Centrex.Afip.Models
{
    /// <summary>
    /// Información de punto de venta AFIP
    /// </summary>
    public class PtoVentaInfo
    {
        public int PuntoVenta { get; set; }
        public string EmisionTipo { get; set; }
        public string Bloqueado { get; set; }
        public string FchBaja { get; set; }


        public PtoVentaInfo()
        {
            PuntoVenta = 0;
            EmisionTipo = "";
            Bloqueado = "";
            FchBaja = "";
        }

        public PtoVentaInfo(int puntoVenta, string emisionTipo, string bloqueado, string fchbBaja) : this()
        {
            PuntoVenta = puntoVenta;
            EmisionTipo = emisionTipo;
            Bloqueado = bloqueado;
            FchBaja = fchbBaja;
        }
    }
}
