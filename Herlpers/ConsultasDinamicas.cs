using System;
using System.Collections.Generic;

namespace Centrex.Helpers
{
    // 🔹 Clase que modela los filtros dinámicos
    public class FiltroConsulta
    {
        public string Entidad { get; set; } = "";
        public string CampoFecha { get; set; } = "";
        public DateTime? FechaDesde { get; set; } = null;
        public DateTime? FechaHasta { get; set; } = null;
        public Dictionary<string, object> FiltrosExtras { get; set; } = new();
    }

    // 🔹 Clase estática para construir filtros y ejecutar consultas dinámicas
    public static class ConsultasDinamicas
    {
        /// <summary>
        /// Crea un objeto de filtros de consulta, equivalente a la antigua "cabecera"
        /// </summary>
        public static FiltroConsulta CrearCabecera(
            string entidad,
            string campoFecha,
            DateTime? fecha_desde,
            DateTime? fecha_hasta,
            Dictionary<string, object>? filtrosExtras = null)
        {
            return new FiltroConsulta
            {
                Entidad = entidad,
                CampoFecha = campoFecha,
                FechaDesde = fecha_desde,
                FechaHasta = fecha_hasta,
                FiltrosExtras = filtrosExtras ?? new()
            };
        }
    }
}

