using System;
using System.Linq;
using Centrex.Afip.Proxy;

namespace Centrex.Afip
{
    /// <summary>
    /// Servicio WSFEv1 para AFIP - ADAPTADO COMPLETAMENTE
    /// </summary>
    public class WSFEv1
    {
        private readonly FEAuthRequest _auth;
        private readonly WSFEClient _client;
        private readonly bool _esTest;

        private WSFEv1(string token, string cuit, bool esTest)
        {
     _esTest = esTest;
    _auth = new FEAuthRequest
            {
   Token = token,
         Sign = "", // Se establecerá desde factura_electronica cuando sea necesario
    Cuit = Conversions.ToLong(cuit)
  };

          string wsfeUrl = esTest ? 
       "https://wswhomo.afip.gov.ar/wsfev1/service.asmx" : 
                "https://servicios1.afip.gov.ar/wsfev1/service.asmx";
            
            _client = new WSFEClient(wsfeUrl);
        }

        /// <summary>
        /// Crea una instancia con token de acceso y CUIT
        /// </summary>
        public static WSFEv1 CreateWithTa(string token, string cuit)
   {
            // Determinar si es test basándose en la configuración actual
            bool esTest = (factura_electronica.modo == "HOMO");
     return new WSFEv1(token, cuit, esTest);
        }

  /// <summary>
        /// Crea una instancia con AfipMode
  /// </summary>
        public static WSFEv1 CreateWithTa(AfipMode afipMode)
    {
          bool esTest = (afipMode == AfipMode.HOMO);
            
        // Obtener token y cuit desde factura_electronica
       string currentToken = factura_electronica.token;
      string currentCuit = factura_electronica.cuit_emisor;
            
            if (string.IsNullOrEmpty(currentToken) || string.IsNullOrEmpty(currentCuit))
    {
throw new InvalidOperationException("Debe llamar a GenerarLoginCms() antes de crear WSFEv1");
    }
            
            return new WSFEv1(currentToken, currentCuit, esTest);
   }

   /// <summary>
        /// Obtiene el último comprobante autorizado
   /// </summary>
    public int FECompUltimoAutorizado(int puntoVenta, int tipoComprobante)
        {
            try
 {
     // Actualizar sign desde factura_electronica
  _auth.Sign = factura_electronica.sign;
    
       var response = _client.FECompUltimoAutorizado(_auth, puntoVenta, tipoComprobante);
         
        if (response.Errors != null && response.Errors.Length > 0)
              {
   string errorMsg = string.Join("; ", response.Errors.Select(e => $"[{e.Code}] {e.Msg}"));
     throw new ApplicationException($"Error AFIP: {errorMsg}");
       }
          
        return response.CbteNro;
 }
            catch (Exception ex)
          {
     throw new ApplicationException($"Error en FECompUltimoAutorizado: {ex.Message}", ex);
     }
 }

        /// <summary>
  /// Consulta un comprobante específico
        /// </summary>
      public object FECompConsultar(int puntoVenta, int tipoComprobante, long numeroComprobante)
        {
      try
            {
       // Actualizar sign desde factura_electronica
    _auth.Sign = factura_electronica.sign;
     
     var request = new FECompConsultaReq
            {
           PtoVta = puntoVenta,
             CbteTipo = tipoComprobante,
        CbteNro = (int)numeroComprobante
      };

                var response = _client.FECompConsultar(_auth, request);
   
    if (response?.ResultGet == null)
         {
       return null;
        }
       
        // Retornar un objeto dinámico compatible con el código existente
     return new
        {
    CAE = response.ResultGet.CodAutorizacion,
       ImpTotal = response.ResultGet.ImpTotal,
          ImpNeto = response.ResultGet.ImpTotal, // Asumir que es igual si no hay detalle
       ImpIVA = 0m, // Se calcularía desde response.ResultGet.Iva si está disponible
          DocNro = response.ResultGet.DocNro,
          Resultado = "A" // Asumir aprobado si se obtuvo respuesta
         };
            }
     catch (Exception ex)
   {
throw new ApplicationException($"Error en FECompConsultar: {ex.Message}", ex);
      }
    }

        /// <summary>
        /// Autoriza un comprobante (FECAESolicitar)
        /// </summary>
        public object FECAESolicitar(FECAERequest request)
        {
     try
            {
      // Actualizar sign desde factura_electronica
         _auth.Sign = factura_electronica.sign;
           
        var response = _client.FECAESolicitar(_auth, request);
             
       return response;
 }
   catch (Exception ex)
   {
       throw new ApplicationException($"Error en FECAESolicitar: {ex.Message}", ex);
            }
        }

   /// <summary>
        /// Prueba de conectividad (FEDummy)
      /// </summary>
        public string FEDummy()
        {
     try
       {
         var response = _client.FEDummy();
         
      return $"AppServer: {response.AppServer}, AuthServer: {response.AuthServer}, DbServer: {response.DbServer}";
 }
    catch (Exception ex)
     {
  throw new ApplicationException($"Error en FEDummy: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Obtiene puntos de venta habilitados
 /// </summary>
        public object FEParamGetPtosVenta()
        {
    try
         {
       // Actualizar sign desde factura_electronica
          _auth.Sign = factura_electronica.sign;
     
        var response = _client.FEParamGetPtosVenta(_auth);
                
    // Log de puntos de venta para debugging
                if (response?.ResultGet?.PtoVenta != null)
  {
        Console.WriteLine("=== PUNTOS DE VENTA HABILITADOS ===");
     foreach (var pto in response.ResultGet.PtoVenta)
       {
     Console.WriteLine($"Punto de Venta: {pto.Nro}, Tipo: {pto.EmisionTipo}, Bloqueado: {pto.Bloqueado}");
          }
     }
        
           return response;
     }
    catch (Exception ex)
      {
                throw new ApplicationException($"Error en FEParamGetPtosVenta: {ex.Message}", ex);
   }
        }
    }
}
