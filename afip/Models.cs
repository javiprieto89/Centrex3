using System;

namespace Centrex.Afip
{
    // =========================
    // AUTENTICACIÓN | TICKETS
    // =========================
    public class AfipAuth
    {
        public string Token { get; set; }
        public string Sign { get; set; }
        public long Cuit { get; set; }
    }

    public class AfipTicket
    {
        public string Token { get; set; }
        public string Sign { get; set; }
        public DateTime Expiration { get; set; }
        public bool IsExpired()
        {
            return DateTime.Now >= Expiration;
        }
    }

    // =========================
    // ERRORES/OBSERVACIONES
    // =========================
    public class AfipError
    {
        public int Code { get; set; }
        public string Msg { get; set; }
    }

    public class AfipObservation
    {
        public int Code { get; set; }
        public string Msg { get; set; }
    }

    // =========================
    // RESPUESTAS WSFE GENERALES
    // =========================
    public class FeDetRespItem
    {
        public int Concepto { get; set; }
        public int DocTipo { get; set; }
        public long DocNro { get; set; }
        public long CbteDesde { get; set; }
        public long CbteHasta { get; set; }
        public string CbteFch { get; set; }
        public string Resultado { get; set; }
        public string CAE { get; set; }
        public string CAEFchVto { get; set; }
        public AfipObservation[] Observaciones { get; set; }
    }

    // Renombrado para evitar conflicto con Proxy.FECabResp
    public class AfipCabResp
    {
        public long Cuit { get; set; }
        public int PtoVta { get; set; }
        public int CbteTipo { get; set; }
        public string FchProceso { get; set; }
        public int CantReg { get; set; }
        public string Resultado { get; set; }
        public string Reproceso { get; set; }
    }

    // Renombrado para evitar conflicto con Proxy.FECAEResponse
    public class AfipCAEResponse
    {
        public AfipCabResp FeCabResp { get; set; }
        public FeDetRespItem[] FeDetResp { get; set; }
        public AfipError[] Errors { get; set; }
    }

    public class FECompConsultarResult
    {
        public long CbteDesde { get; set; }
        public long CbteHasta { get; set; }
        public int CbteTipo { get; set; }
        public int Concepto { get; set; }
        public long DocNro { get; set; }
        public int DocTipo { get; set; }
        public int PtoVta { get; set; }
        public string Resultado { get; set; }
        public string CbteFch { get; set; }
        public string CAE { get; set; }
        public string CAEFchVto { get; set; }
        public AfipObservation[] Observaciones { get; set; }
        public AfipError[] Errors { get; set; }
    }
}
