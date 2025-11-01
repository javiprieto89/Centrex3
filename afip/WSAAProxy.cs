using System;
using System.Net.Http;
using System.Security;
using System.Text;
using System.Xml;

namespace Centrex.Afip
{
    /// <summary>
    /// Cliente manual para el servicio WSAA (LoginCms)
    /// Reemplaza las Service References con llamadas HTTP directas
    /// </summary>
    public class WSAAClient
    {
        private readonly string _url;
        private readonly int _timeout;

        public WSAAClient(string url, int timeoutSeconds = 60)
        {
            _url = url;
            _timeout = timeoutSeconds * 1000;
        }

        /// <summary>
        /// Llama al método loginCms del servicio WSAA
        /// </summary>
        public string loginCms(string cmsSigned)
        {
            try
            {
                // Crear el SOAP envelope
                string soapEnvelope = CreateLoginCmsSoapEnvelope(cmsSigned);

                using var httpClient = new HttpClient
                {
                    Timeout = TimeSpan.FromMilliseconds(_timeout)
                };

                using var requestMessage = new HttpRequestMessage(HttpMethod.Post, _url)
                {
                    Content = new StringContent(soapEnvelope, Encoding.UTF8, "text/xml")
                };
                requestMessage.Headers.Add("SOAPAction", "http://ar.gov.afip.dif.facturaelectronica/loginCms");

                using var response = httpClient.Send(requestMessage);
                string responseText = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

                if (!response.IsSuccessStatusCode)
                {
                    string details = string.IsNullOrWhiteSpace(responseText)
                        ? response.ReasonPhrase ?? "Respuesta vacía"
                        : responseText;
                    throw new ApplicationException($"Error en llamada WSAA ({(int)response.StatusCode}): {details}");
                }

                return ExtractLoginCmsResponse(responseText);
            }

            catch (HttpRequestException ex)
            {
                throw new ApplicationException("Error en llamada WSAA: " + ex.Message, ex);
            }
        }

        /// <summary>
        /// Crea el SOAP envelope para loginCms
        /// </summary>
        private string CreateLoginCmsSoapEnvelope(string cmsSigned)
        {
            var sb = new StringBuilder();
            sb.AppendLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            sb.AppendLine("<soap:Envelope xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">");
            sb.AppendLine("  <soap:Body>");
            sb.AppendLine("    <loginCms xmlns=\"http://ar.gov.afip.dif.facturaelectronica/\">");
            sb.AppendLine($"      <in0>{SecurityElement.Escape(cmsSigned)}</in0>");
            sb.AppendLine("    </loginCms>");
            sb.AppendLine("  </soap:Body>");
            sb.AppendLine("</soap:Envelope>");
            return sb.ToString();
        }

        /// <summary>
        /// Extrae la respuesta XML del SOAP envelope
        /// </summary>
        private string ExtractLoginCmsResponse(string soapResponse)
        {
            try
            {
                var doc = new XmlDocument();
                doc.LoadXml(soapResponse);

                // Buscar el nodo loginCmsReturn
                var nsmgr = new XmlNamespaceManager(doc.NameTable);
                nsmgr.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/");
                nsmgr.AddNamespace("ns1", "http://ar.gov.afip.dif.facturaelectronica/");

                var node = doc.SelectSingleNode("//ns1:loginCmsReturn", nsmgr);
                if (node is null)
                {
                    // Intentar sin namespace
                    node = doc.SelectSingleNode("//loginCmsReturn");
                }

                if (node is not null)
                {
                    return node.InnerText;
                }
                else
                {
                    throw new ApplicationException("No se encontró loginCmsReturn en la respuesta WSAA");
                }
            }

            catch (Exception ex)
            {
                throw new ApplicationException("Error al parsear respuesta WSAA: " + ex.Message, ex);
            }
        }
    }
}
