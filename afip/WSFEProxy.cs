using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security;
using System.Text;
using System.Xml;
using Microsoft.VisualBasic.CompilerServices;

namespace Centrex.Afip.Proxy
{
    /// <summary>
    /// Cliente manual para el servicio WSFEv1
    /// Reemplaza las Service References con llamadas HTTP directas
    /// </summary>
    public class WSFEClient
    {
        private readonly string _url;
        private readonly int _timeout;

        public WSFEClient(string url, int timeoutSeconds = 60)
        {
            _url = url;
            _timeout = timeoutSeconds * 1000;
        }

        /// <summary>
        /// Método FEDummy - Prueba de conectividad
        /// </summary>
        public FEDummyResponse FEDummy()
        {
            string soapBody = "<FEDummy xmlns=\"http://ar.gov.afip.dif.FEV1/\" />";
            string response = CallSoapMethod("FEDummy", soapBody);
            return ParseFEDummyResponse(response);
        }

        /// <summary>
        /// Método FECompUltimoAutorizado
        /// </summary>
        public FECompUltimoAutorizadoResponse FECompUltimoAutorizado(FEAuthRequest auth, int ptoVta, int cbteTipo)
        {
            string soapBody = CreateFECompUltimoAutorizadoBody(auth, ptoVta, cbteTipo);
            string response = CallSoapMethod("FECompUltimoAutorizado", soapBody);
            return ParseFECompUltimoAutorizadoResponse(response);
        }

        /// <summary>
        /// Método FECompConsultar
        /// </summary>
        public FECompConsultaResponse FECompConsultar(FEAuthRequest auth, FECompConsultaReq req)
        {
            string soapBody = CreateFECompConsultarBody(auth, req);
            string response = CallSoapMethod("FECompConsultar", soapBody);
            return ParseFECompConsultaResponse(response);
        }

        /// <summary>
        /// Método FECAESolicitar
        /// </summary>
        public FECAEResponse FECAESolicitar(FEAuthRequest auth, FECAERequest request)
        {
            string soapBody = CreateFECAESolicitarBody(auth, request);
            string response = CallSoapMethod("FECAESolicitar", soapBody);
            return ParseFECAEResponse(response);
        }

        /// <summary>
        /// Método FEParamGetTiposCbte
        /// </summary>
        public FEParamGetTiposCbteResponse FEParamGetTiposCbte(FEAuthRequest auth)
        {
            string soapBody = CreateFEParamGetTiposCbteBody(auth);
            string response = CallSoapMethod("FEParamGetTiposCbte", soapBody);
            return ParseFEParamGetTiposCbteResponse(response);
        }

        /// <summary>
        /// Método FEParamGetTiposDoc
        /// </summary>
        public FEParamGetTiposDocResponse FEParamGetTiposDoc(FEAuthRequest auth)
        {
            string soapBody = CreateFEParamGetTiposDocBody(auth);
            string response = CallSoapMethod("FEParamGetTiposDoc", soapBody);
            return ParseFEParamGetTiposDocResponse(response);
        }

        /// <summary>
        /// Método FEParamGetTiposIva
        /// </summary>
        public object FEParamGetTiposIva(FEAuthRequest auth)
        {
            string soapBody = CreateFEParamGetTiposIvaBody(auth);
            string response = CallSoapMethod("FEParamGetTiposIva", soapBody);
            return ParseGenericResponse(response, "FEParamGetTiposIvaResponse");
        }

        /// <summary>
        /// Método FEParamGetTiposMonedas
        /// </summary>
        public object FEParamGetTiposMonedas(FEAuthRequest auth)
        {
            string soapBody = CreateFEParamGetTiposMonedasBody(auth);
            string response = CallSoapMethod("FEParamGetTiposMonedas", soapBody);
            return ParseGenericResponse(response, "FEParamGetTiposMonedasResponse");
        }

        /// <summary>
        /// Método FEParamGetTiposConcepto
        /// </summary>
        public object FEParamGetTiposConcepto(FEAuthRequest auth)
        {
            string soapBody = CreateFEParamGetTiposConceptoBody(auth);
            string response = CallSoapMethod("FEParamGetTiposConcepto", soapBody);
            return ParseGenericResponse(response, "FEParamGetTiposConceptoResponse");
        }

        /// <summary>
        /// Método FEParamGetPtosVenta
        /// </summary>
        public FEParamGetPtosVentaResponse FEParamGetPtosVenta(FEAuthRequest auth)
        {
            string soapBody = CreateFEParamGetPtosVentaBody(auth);
            string response = CallSoapMethod("FEParamGetPtosVenta", soapBody);
            return ParseFEParamGetPtosVentaResponse(response);
        }

        /// <summary>
        /// Método FEParamGetCotizacion
        /// </summary>
        public object FEParamGetCotizacion(FEAuthRequest auth, string moneda, string fecha)
        {
            string soapBody = CreateFEParamGetCotizacionBody(auth, moneda, fecha);
            string response = CallSoapMethod("FEParamGetCotizacion", soapBody);
            return ParseGenericResponse(response, "FEParamGetCotizacionResponse");
        }

        /// <summary>
        /// Método FEParamGetTiposTributos
        /// </summary>
        public object FEParamGetTiposTributos(FEAuthRequest auth)
        {
            string soapBody = CreateFEParamGetTiposTributosBody(auth);
            string response = CallSoapMethod("FEParamGetTiposTributos", soapBody);
            return ParseGenericResponse(response, "FEParamGetTiposTributosResponse");
        }

        /// <summary>
        /// Método FEParamGetTiposOpcional
        /// </summary>
        public object FEParamGetTiposOpcional(FEAuthRequest auth)
        {
            string soapBody = CreateFEParamGetTiposOpcionalBody(auth);
            string response = CallSoapMethod("FEParamGetTiposOpcional", soapBody);
            return ParseGenericResponse(response, "FEParamGetTiposOpcionalResponse");
        }

        // ============================================
        // MÉTODOS PRIVADOS - LLAMADAS SOAP
        // ============================================

        private string CallSoapMethod(string methodName, string soapBody)
        {
            try
            {
                string soapEnvelope = CreateSoapEnvelope(soapBody);

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(_url);
                request.Method = "POST";
                request.ContentType = "text/xml; charset=utf-8";
                request.Timeout = _timeout;
                request.Headers.Add("SOAPAction", $"http://ar.gov.afip.dif.FEV1/{methodName}");

                byte[] bytes = Encoding.UTF8.GetBytes(soapEnvelope);
                request.ContentLength = bytes.Length;

                using (var stream = request.GetRequestStream())
                {
                    stream.Write(bytes, 0, bytes.Length);
                }

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    using (var responseStream = response.GetResponseStream())
                    {
                        using (var reader = new StreamReader(responseStream))
                        {
                            return reader.ReadToEnd();
                        }
                    }
                }
            }

            catch (WebException ex)
            {
                string errorMessage = "Error en llamada WSFE: ";
                if (ex.Response is not null)
                {
                    using (var responseStream = ex.Response.GetResponseStream())
                    {
                        using (var reader = new StreamReader(responseStream))
                        {
                            errorMessage += reader.ReadToEnd();
                        }
                    }
                }
                else
                {
                    errorMessage += ex.Message;
                }
                throw new ApplicationException(errorMessage, ex);
            }
        }

        private string CreateSoapEnvelope(string body)
        {
            var sb = new StringBuilder();
            sb.AppendLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            sb.AppendLine("<soap:Envelope xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">");
            sb.AppendLine("  <soap:Body>");
            sb.AppendLine(body);
            sb.AppendLine("  </soap:Body>");
            sb.AppendLine("</soap:Envelope>");
            return sb.ToString();
        }

        // ============================================
        // MÉTODOS PRIVADOS - CREAR SOAP BODIES
        // ============================================

        private string CreateAuthBody(FEAuthRequest auth)
        {
            var sb = new StringBuilder();
            sb.AppendLine("    <Auth>");
            sb.AppendLine($"      <Token>{SecurityElement.Escape(auth.Token)}</Token>");
            sb.AppendLine($"      <Sign>{SecurityElement.Escape(auth.Sign)}</Sign>");
            sb.AppendLine($"      <Cuit>{auth.Cuit}</Cuit>");
            sb.AppendLine("    </Auth>");
            return sb.ToString();
        }

        private string CreateFECompUltimoAutorizadoBody(FEAuthRequest auth, int ptoVta, int cbteTipo)
        {
            var sb = new StringBuilder();
            sb.AppendLine("<FECompUltimoAutorizado xmlns=\"http://ar.gov.afip.dif.FEV1/\">");
            sb.Append(CreateAuthBody(auth));
            sb.AppendLine($"  <PtoVta>{ptoVta}</PtoVta>");
            sb.AppendLine($"  <CbteTipo>{cbteTipo}</CbteTipo>");
            sb.AppendLine("</FECompUltimoAutorizado>");
            return sb.ToString();
        }

        private string CreateFECompConsultarBody(FEAuthRequest auth, FECompConsultaReq req)
        {
            var sb = new StringBuilder();
            sb.AppendLine("<FECompConsultar xmlns=\"http://ar.gov.afip.dif.FEV1/\">");
            sb.Append(CreateAuthBody(auth));
            sb.AppendLine("  <FeCompConsReq>");
            sb.AppendLine($"    <CbteTipo>{req.CbteTipo}</CbteTipo>");
            sb.AppendLine($"    <CbteNro>{req.CbteNro}</CbteNro>");
            sb.AppendLine($"    <PtoVta>{req.PtoVta}</PtoVta>");
            sb.AppendLine("  </FeCompConsReq>");
            sb.AppendLine("</FECompConsultar>");
            return sb.ToString();
        }

        private string CreateFECAESolicitarBody(FEAuthRequest auth, FECAERequest request)
        {
            var sb = new StringBuilder();
            sb.AppendLine("<FECAESolicitar xmlns=\"http://ar.gov.afip.dif.FEV1/\">");
            sb.Append(CreateAuthBody(auth));
            sb.AppendLine("  <FeCAEReq>");
            sb.AppendLine("    <FeCabReq>");
            sb.AppendLine($"      <CantReg>{request.FeCabReq.CantReg}</CantReg>");
            sb.AppendLine($"      <PtoVta>{request.FeCabReq.PtoVta}</PtoVta>");
            sb.AppendLine($"      <CbteTipo>{request.FeCabReq.CbteTipo}</CbteTipo>");
            sb.AppendLine("    </FeCabReq>");
            sb.AppendLine("    <FeDetReq>");

            foreach (var det in request.FeDetReq)
            {
                sb.AppendLine("      <FECAEDetRequest>");
                sb.AppendLine($"        <Concepto>{det.Concepto}</Concepto>");
                sb.AppendLine($"        <DocTipo>{det.DocTipo}</DocTipo>");
                sb.AppendLine($"        <DocNro>{det.DocNro}</DocNro>");
                sb.AppendLine($"        <CbteDesde>{det.CbteDesde}</CbteDesde>");
                sb.AppendLine($"        <CbteHasta>{det.CbteHasta}</CbteHasta>");
                sb.AppendLine($"        <CbteFch>{det.CbteFch}</CbteFch>");
                sb.AppendLine($"        <ImpTotal>{det.ImpTotal.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture)}</ImpTotal>");
                sb.AppendLine($"        <ImpTotConc>{det.ImpTotConc.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture)}</ImpTotConc>");
                sb.AppendLine($"        <ImpNeto>{det.ImpNeto.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture)}</ImpNeto>");
                sb.AppendLine($"        <ImpOpEx>{det.ImpOpEx.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture)}</ImpOpEx>");
                sb.AppendLine($"        <ImpTrib>{det.ImpTrib.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture)}</ImpTrib>");
                sb.AppendLine($"        <ImpIVA>{det.ImpIVA.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture)}</ImpIVA>");

                if (!string.IsNullOrEmpty(det.FchServDesde))
                {
                    sb.AppendLine($"        <FchServDesde>{det.FchServDesde}</FchServDesde>");
                }
                if (!string.IsNullOrEmpty(det.FchServHasta))
                {
                    sb.AppendLine($"        <FchServHasta>{det.FchServHasta}</FchServHasta>");
                }
                if (!string.IsNullOrEmpty(det.FchVtoPago))
                {
                    sb.AppendLine($"        <FchVtoPago>{det.FchVtoPago}</FchVtoPago>");
                }

                sb.AppendLine($"        <MonId>{SecurityElement.Escape(det.MonId)}</MonId>");
                sb.AppendLine($"        <MonCotiz>{det.MonCotiz.ToString("0.000000", System.Globalization.CultureInfo.InvariantCulture)}</MonCotiz>");

                if (det.Iva is not null && det.Iva.Length > 0)
                {
                    sb.AppendLine("        <Iva>");
                    foreach (var iva in det.Iva)
                    {
                        sb.AppendLine("          <AlicIva>");
                        sb.AppendLine($"            <Id>{iva.Id}</Id>");
                        sb.AppendLine($"            <BaseImp>{iva.BaseImp.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture)}</BaseImp>");
                        sb.AppendLine($"            <Importe>{iva.Importe.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture)}</Importe>");
                        sb.AppendLine("          </AlicIva>");
                    }
                    sb.AppendLine("        </Iva>");
                }

                // Opcionales (MiPyME)
                if (det.Opcionales is not null && det.Opcionales.Length > 0)
                {
                    sb.AppendLine("        <Opcionales>");
                    foreach (var opc in det.Opcionales)
                    {
                        sb.AppendLine("          <Opcional>");
                        sb.AppendLine($"            <Id>{opc.Id}</Id>");
                        sb.AppendLine($"            <Valor>{SecurityElement.Escape(opc.Valor)}</Valor>");
                        sb.AppendLine("          </Opcional>");
                    }
                    sb.AppendLine("        </Opcionales>");
                }

                // Comprobantes Asociados (NC/ND)
                if (det.CbtesAsoc is not null && det.CbtesAsoc.Length > 0)
                {
                    sb.AppendLine("        <CbtesAsoc>");
                    foreach (var asoc in det.CbtesAsoc)
                    {
                        sb.AppendLine("          <CbteAsoc>");
                        sb.AppendLine($"            <Tipo>{asoc.Tipo}</Tipo>");
                        sb.AppendLine($"            <PtoVta>{asoc.PtoVta}</PtoVta>");
                        sb.AppendLine($"            <Nro>{asoc.Nro}</Nro>");
                        sb.AppendLine($"            <Cuit>{asoc.Cuit}</Cuit>");
                        if (!string.IsNullOrEmpty(asoc.CbteFch))
                        {
                            sb.AppendLine($"            <CbteFch>{asoc.CbteFch}</CbteFch>");
                        }
                        sb.AppendLine("          </CbteAsoc>");
                    }
                    sb.AppendLine("        </CbtesAsoc>");
                }

                sb.AppendLine("      </FECAEDetRequest>");
            }

            sb.AppendLine("    </FeDetReq>");
            sb.AppendLine("  </FeCAEReq>");
            sb.AppendLine("</FECAESolicitar>");
            return sb.ToString();
        }

        private string CreateFEParamGetTiposCbteBody(FEAuthRequest auth)
        {
            var sb = new StringBuilder();
            sb.AppendLine("<FEParamGetTiposCbte xmlns=\"http://ar.gov.afip.dif.FEV1/\">");
            sb.Append(CreateAuthBody(auth));
            sb.AppendLine("</FEParamGetTiposCbte>");
            return sb.ToString();
        }

        private string CreateFEParamGetTiposDocBody(FEAuthRequest auth)
        {
            var sb = new StringBuilder();
            sb.AppendLine("<FEParamGetTiposDoc xmlns=\"http://ar.gov.afip.dif.FEV1/\">");
            sb.Append(CreateAuthBody(auth));
            sb.AppendLine("</FEParamGetTiposDoc>");
            return sb.ToString();
        }

        private string CreateFEParamGetTiposIvaBody(FEAuthRequest auth)
        {
            var sb = new StringBuilder();
            sb.AppendLine("<FEParamGetTiposIva xmlns=\"http://ar.gov.afip.dif.FEV1/\">");
            sb.Append(CreateAuthBody(auth));
            sb.AppendLine("</FEParamGetTiposIva>");
            return sb.ToString();
        }

        private string CreateFEParamGetTiposMonedasBody(FEAuthRequest auth)
        {
            var sb = new StringBuilder();
            sb.AppendLine("<FEParamGetTiposMonedas xmlns=\"http://ar.gov.afip.dif.FEV1/\">");
            sb.Append(CreateAuthBody(auth));
            sb.AppendLine("</FEParamGetTiposMonedas>");
            return sb.ToString();
        }

        private string CreateFEParamGetTiposConceptoBody(FEAuthRequest auth)
        {
            var sb = new StringBuilder();
            sb.AppendLine("<FEParamGetTiposConcepto xmlns=\"http://ar.gov.afip.dif.FEV1/\">");
            sb.Append(CreateAuthBody(auth));
            sb.AppendLine("</FEParamGetTiposConcepto>");
            return sb.ToString();
        }

        private string CreateFEParamGetPtosVentaBody(FEAuthRequest auth)
        {
            var sb = new StringBuilder();
            sb.AppendLine("<FEParamGetPtosVenta xmlns=\"http://ar.gov.afip.dif.FEV1/\">");
            sb.Append(CreateAuthBody(auth));
            sb.AppendLine("</FEParamGetPtosVenta>");
            return sb.ToString();
        }

        private string CreateFEParamGetCotizacionBody(FEAuthRequest auth, string moneda, string fecha)
        {
            var sb = new StringBuilder();
            sb.AppendLine("<FEParamGetCotizacion xmlns=\"http://ar.gov.afip.dif.FEV1/\">");
            sb.Append(CreateAuthBody(auth));
            sb.AppendLine($"  <MonId>{SecurityElement.Escape(moneda)}</MonId>");
            sb.AppendLine($"  <FchCotiz>{fecha}</FchCotiz>");
            sb.AppendLine("</FEParamGetCotizacion>");
            return sb.ToString();
        }

        private string CreateFEParamGetTiposTributosBody(FEAuthRequest auth)
        {
            var sb = new StringBuilder();
            sb.AppendLine("<FEParamGetTiposTributos xmlns=\"http://ar.gov.afip.dif.FEV1/\">");
            sb.Append(CreateAuthBody(auth));
            sb.AppendLine("</FEParamGetTiposTributos>");
            return sb.ToString();
        }

        private string CreateFEParamGetTiposOpcionalBody(FEAuthRequest auth)
        {
            var sb = new StringBuilder();
            sb.AppendLine("<FEParamGetTiposOpcional xmlns=\"http://ar.gov.afip.dif.FEV1/\">");
            sb.Append(CreateAuthBody(auth));
            sb.AppendLine("</FEParamGetTiposOpcional>");
            return sb.ToString();
        }

        // ============================================
        // MÉTODOS PRIVADOS - PARSE RESPONSES
        // ============================================

        private FEDummyResponse ParseFEDummyResponse(string soapResponse)
        {
            var doc = new XmlDocument();
            doc.LoadXml(soapResponse);

            var nsmgr = new XmlNamespaceManager(doc.NameTable);
            nsmgr.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/");
            nsmgr.AddNamespace("ns1", "http://ar.gov.afip.dif.FEV1/");

            var response = new FEDummyResponse();
            response.AppServer = GetNodeValue(doc, "//ns1:AppServer", nsmgr);
            response.AuthServer = GetNodeValue(doc, "//ns1:AuthServer", nsmgr);
            response.DbServer = GetNodeValue(doc, "//ns1:DbServer", nsmgr);

            return response;
        }

        private FECompUltimoAutorizadoResponse ParseFECompUltimoAutorizadoResponse(string soapResponse)
        {
            var doc = new XmlDocument();
            doc.LoadXml(soapResponse);

            var nsmgr = new XmlNamespaceManager(doc.NameTable);
            nsmgr.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/");
            nsmgr.AddNamespace("ns1", "http://ar.gov.afip.dif.FEV1/");

            var response = new FECompUltimoAutorizadoResponse();
            string cbteNroStr = GetNodeValue(doc, "//ns1:CbteNro", nsmgr);
            if (!string.IsNullOrEmpty(cbteNroStr))
            {
                response.CbteNro = Conversions.ToInteger(cbteNroStr);
            }

            response.Errors = ParseErrors(doc, nsmgr);

            return response;
        }

        private FECompConsultaResponse ParseFECompConsultaResponse(string soapResponse)
        {
            var doc = new XmlDocument();
            doc.LoadXml(soapResponse);

            var nsmgr = new XmlNamespaceManager(doc.NameTable);
            nsmgr.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/");
            nsmgr.AddNamespace("ns1", "http://ar.gov.afip.dif.FEV1/");

            var response = new FECompConsultaResponse();
            var resultNode = doc.SelectSingleNode("//ns1:ResultGet", nsmgr);

            if (resultNode is not null)
            {
                response.ResultGet = new FECompConsultaResult();
                response.ResultGet.CodAutorizacion = GetChildNodeValue(resultNode, "CodAutorizacion");
                response.ResultGet.DocNro = Conversions.ToLong(GetChildNodeValue(resultNode, "DocNro", "0"));
                response.ResultGet.ImpTotal = Conversions.ToDecimal(GetChildNodeValue(resultNode, "ImpTotal", "0"));
            }

            return response;
        }

        private FECAEResponse ParseFECAEResponse(string soapResponse)
        {
            var doc = new XmlDocument();
            doc.LoadXml(soapResponse);

            var nsmgr = new XmlNamespaceManager(doc.NameTable);
            nsmgr.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/");
            nsmgr.AddNamespace("ns1", "http://ar.gov.afip.dif.FEV1/");

            var response = new FECAEResponse();

            // Parse FeCabResp
            var cabNode = doc.SelectSingleNode("//ns1:FeCabResp", nsmgr);
            if (cabNode is not null)
            {
                response.FeCabResp = new FECabResp();
                response.FeCabResp.Cuit = Conversions.ToLong(GetChildNodeValue(cabNode, "Cuit", "0"));
                response.FeCabResp.PtoVta = Conversions.ToInteger(GetChildNodeValue(cabNode, "PtoVta", "0"));
                response.FeCabResp.CbteTipo = Conversions.ToInteger(GetChildNodeValue(cabNode, "CbteTipo", "0"));
                response.FeCabResp.FchProceso = GetChildNodeValue(cabNode, "FchProceso");
                response.FeCabResp.CantReg = Conversions.ToInteger(GetChildNodeValue(cabNode, "CantReg", "0"));
                response.FeCabResp.Resultado = GetChildNodeValue(cabNode, "Resultado");
                response.FeCabResp.Reproceso = GetChildNodeValue(cabNode, "Reproceso");
            }

            // Parse FeDetResp
            var detNodes = doc.SelectNodes("//ns1:FECAEDetResponse", nsmgr);
            if (detNodes is not null && detNodes.Count > 0)
            {
                var detList = new List<FEDetResp>();
                foreach (XmlNode detNode in detNodes)
                {
                    var det = new FEDetResp();
                    det.Concepto = Conversions.ToInteger(GetChildNodeValue(detNode, "Concepto", "0"));
                    det.DocTipo = Conversions.ToInteger(GetChildNodeValue(detNode, "DocTipo", "0"));
                    det.DocNro = Conversions.ToLong(GetChildNodeValue(detNode, "DocNro", "0"));
                    det.CbteDesde = Conversions.ToLong(GetChildNodeValue(detNode, "CbteDesde", "0"));
                    det.CbteHasta = Conversions.ToLong(GetChildNodeValue(detNode, "CbteHasta", "0"));
                    det.CbteFch = GetChildNodeValue(detNode, "CbteFch");
                    det.Resultado = GetChildNodeValue(detNode, "Resultado");
                    det.CAE = GetChildNodeValue(detNode, "CAE");
                    det.CAEFchVto = GetChildNodeValue(detNode, "CAEFchVto");
                    detList.Add(det);
                }
                response.FeDetResp = detList.ToArray();
            }

            response.Errors = ParseErrors(doc, nsmgr);

            return response;
        }

        private FEParamGetTiposCbteResponse ParseFEParamGetTiposCbteResponse(string soapResponse)
        {
            return new FEParamGetTiposCbteResponse() { RawXml = soapResponse };
        }

        private FEParamGetTiposDocResponse ParseFEParamGetTiposDocResponse(string soapResponse)
        {
            return new FEParamGetTiposDocResponse() { RawXml = soapResponse };
        }

        private FEParamGetPtosVentaResponse ParseFEParamGetPtosVentaResponse(string soapResponse)
        {
            try
            {
                var doc = new XmlDocument();
                doc.LoadXml(soapResponse);

                // Crear namespace manager
                var nsmgr = new XmlNamespaceManager(doc.NameTable);
                nsmgr.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/");
                nsmgr.AddNamespace("ns", "http://ar.gov.afip.dif.FEV1/");

                // Buscar el nodo de respuesta usando namespaces
                var resultNode = doc.SelectSingleNode("//ns:FEParamGetPtosVentaResponse/ns:FEParamGetPtosVentaResult", nsmgr);

                if (resultNode is null)
                {
                    // Intentar sin namespace
                    resultNode = doc.SelectSingleNode("//FEParamGetPtosVentaResult");
                }

                var response = new FEParamGetPtosVentaResponse();

                if (resultNode is not null)
                {
                    // Buscar ResultGet
                    var resultGetNode = resultNode.SelectSingleNode("ns:ResultGet", nsmgr);
                    if (resultGetNode is null)
                    {
                        resultGetNode = resultNode.SelectSingleNode("ResultGet");
                    }

                    if (resultGetNode is not null)
                    {
                        response.ResultGet = new FEPtosVentaResult();

                        // Buscar todos los PtoVenta
                        var ptosVentaNodes = resultGetNode.SelectNodes("ns:PtoVenta", nsmgr);
                        if (ptosVentaNodes is null || ptosVentaNodes.Count == 0)
                        {
                            ptosVentaNodes = resultGetNode.SelectNodes("PtoVenta");
                        }

                        if (ptosVentaNodes is not null && ptosVentaNodes.Count > 0)
                        {
                            var ptosList = new List<FEPtoVenta>();

                            foreach (XmlNode ptoNode in ptosVentaNodes)
                            {
                                var pto = new FEPtoVenta()
                                {
                                    Nro = int.Parse(GetChildNodeValue(ptoNode, "Nro", "0")),
                                    EmisionTipo = GetChildNodeValue(ptoNode, "EmisionTipo"),
                                    Bloqueado = GetChildNodeValue(ptoNode, "Bloqueado", "N"),
                                    FchBaja = GetChildNodeValue(ptoNode, "FchBaja")
                                };
                                ptosList.Add(pto);
                            }

                            response.ResultGet.PtoVenta = ptosList.ToArray();
                        }
                    }
                }

                return response;
            }

            catch (Exception ex)
            {
                Console.WriteLine($"Error en ParseFEParamGetPtosVentaResponse: {ex.Message}");
                throw;
            }
        }

        private object ParseGenericResponse(string soapResponse, string responseType)
        {
            // Implementación genérica para respuestas menos críticas
            var doc = new XmlDocument();
            doc.LoadXml(soapResponse);
            return doc;
        }

        private FEErr[] ParseErrors(XmlDocument doc, XmlNamespaceManager nsmgr)
        {
            var errorNodes = doc.SelectNodes("//ns1:Err", nsmgr);
            if (errorNodes is not null && errorNodes.Count > 0)
            {
                var errorList = new List<FEErr>();
                foreach (XmlNode errNode in errorNodes)
                {
                    var err = new FEErr();
                    err.Code = Conversions.ToInteger(GetChildNodeValue(errNode, "Code", "0"));
                    err.Msg = GetChildNodeValue(errNode, "Msg");
                    errorList.Add(err);
                }
                return errorList.ToArray();
            }
            return null;
        }

        private string GetNodeValue(XmlDocument doc, string xpath, XmlNamespaceManager nsmgr, string defaultValue = "")
        {
            var node = doc.SelectSingleNode(xpath, nsmgr);
            if (node is not null)
            {
                return node.InnerText;
            }
            return defaultValue;
        }

        private string GetChildNodeValue(XmlNode parentNode, string childName, string defaultValue = "")
        {
            // Buscar el nodo sin usar XPath con prefijos
            foreach (XmlNode child in parentNode.ChildNodes)
            {
                if ((child.LocalName ?? "") == (childName ?? ""))
                {
                    return child.InnerText;
                }
            }
            return defaultValue;
        }

    }

    // ============================================
    // CLASES DE DATOS PARA WSFE
    // ============================================

    public class FEAuthRequest
    {
        public string Token { get; set; }
        public string Sign { get; set; }
        public long Cuit { get; set; }
    }

    public class FECompConsultaReq
    {
        public int CbteTipo { get; set; }
        public int CbteNro { get; set; }
        public int PtoVta { get; set; }
    }

    public class FECAERequest
    {
        public FECabReq FeCabReq { get; set; }
        public FEDetReq[] FeDetReq { get; set; }
    }

    public class FECabReq
    {
        public int CantReg { get; set; }
        public int PtoVta { get; set; }
        public int CbteTipo { get; set; }
    }

    public class FEDetReq
    {
        public int Concepto { get; set; }
        public int DocTipo { get; set; }
        public long DocNro { get; set; }
        public long CbteDesde { get; set; }
        public long CbteHasta { get; set; }
        public string CbteFch { get; set; }
        public decimal ImpTotal { get; set; }
        public decimal ImpTotConc { get; set; }
        public decimal ImpNeto { get; set; }
        public decimal ImpOpEx { get; set; }
        public decimal ImpTrib { get; set; }
        public decimal ImpIVA { get; set; }
        public string FchServDesde { get; set; }
        public string FchServHasta { get; set; }
        public string FchVtoPago { get; set; }
        public string MonId { get; set; }
        public decimal MonCotiz { get; set; }
        public FEIva[] Iva { get; set; }
        public FEOpcional[] Opcionales { get; set; }
        public FECbtesAsoc[] CbtesAsoc { get; set; }
    }

    public class FEIva
    {
        public int Id { get; set; }
        public decimal BaseImp { get; set; }
        public decimal Importe { get; set; }
    }

    // Respuestas
    public class FEDummyResponse
    {
        public string AppServer { get; set; }
        public string AuthServer { get; set; }
        public string DbServer { get; set; }
    }

    public class FECompUltimoAutorizadoResponse
    {
        public int CbteNro { get; set; }
        public FEErr[] Errors { get; set; }
    }

    public class FECompConsultaResponse
    {
        public FECompConsultaResult ResultGet { get; set; }
    }

    public class FECompConsultaResult
    {
        public string CodAutorizacion { get; set; }
        public long DocNro { get; set; }
        public decimal ImpTotal { get; set; }
        public FEIva[] Iva { get; set; }
    }

    public class FECAEResponse
    {
        public FECabResp FeCabResp { get; set; }
        public FEDetResp[] FeDetResp { get; set; }
        public FEErr[] Errors { get; set; }
    }

    public class FECabResp
    {
        public long Cuit { get; set; }
        public int PtoVta { get; set; }
        public int CbteTipo { get; set; }
        public string FchProceso { get; set; }
        public int CantReg { get; set; }
        public string Resultado { get; set; }
        public string Reproceso { get; set; }
    }

    public class FEDetResp
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
        public FEObs[] Observaciones { get; set; }
    }

    public class FEObs
    {
        public int Code { get; set; }
        public string Msg { get; set; }
    }

    public class FEErr
    {
        public int Code { get; set; }
        public string Msg { get; set; }
    }

    public class FEParamGetTiposCbteResponse
    {
        public string RawXml { get; set; }
    }

    public class FEParamGetTiposDocResponse
    {
        public string RawXml { get; set; }
    }

    public class FEParamGetPtosVentaResponse
    {
        public FEPtosVentaResult ResultGet { get; set; }
    }

    public class FEPtosVentaResult
    {
        public FEPtoVenta[] PtoVenta { get; set; }
    }

    public class FEPtoVenta
    {
        public int Nro { get; set; }
        public string EmisionTipo { get; set; }
        public string Bloqueado { get; set; }
        public string FchBaja { get; set; }
    }

    /// <summary>
    /// Clase para opcionales MiPyME (CBU, Alias, Modo, etc.)
    /// </summary>
    public class FEOpcional
    {
        public int Id { get; set; }
        public string Valor { get; set; }
    }

    /// <summary>
    /// Clase para comprobantes asociados (NC/ND)
    /// </summary>
    public class FECbtesAsoc
    {
        public int Tipo { get; set; }
        public int PtoVta { get; set; }
        public long Nro { get; set; }
        public long Cuit { get; set; }
        public string CbteFch { get; set; }
    }

}
