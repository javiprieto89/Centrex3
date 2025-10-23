using System;
using System.IO;
using System.Net;
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

                // Crear la petición HTTP
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(_url);
                request.Method = "POST";
                request.ContentType = "text/xml; charset=utf-8";
                request.Timeout = _timeout;
                request.Headers.Add("SOAPAction", "http://ar.gov.afip.dif.facturaelectronica/loginCms");

                // Escribir el SOAP en el body
                byte[] bytes = Encoding.UTF8.GetBytes(soapEnvelope);
                request.ContentLength = bytes.Length;

                using (var stream = request.GetRequestStream())
                {
                    stream.Write(bytes, 0, bytes.Length);
                }

                // Obtener respuesta
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    using (var responseStream = response.GetResponseStream())
                    {
                        using (var reader = new StreamReader(responseStream))
                        {
                            string responseText = reader.ReadToEnd();
                            return ExtractLoginCmsResponse(responseText);
                        }
                    }
                }
            }

            catch (WebException ex)
            {
                string errorMessage = "Error en llamada WSAA: ";
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
