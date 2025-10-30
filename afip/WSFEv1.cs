using Centrex.Afip.Models;
using Centrex.Afip.Proxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Xml;

namespace Centrex.Afip
{
    /// <summary>
    /// Servicio WSFEv1 para AFIP - ADAPTADO COMPLETAMENTE
    /// </summary>
    public class WSFEv1 : IDisposable
    {
        private readonly FEAuthRequest _auth;
        private WSFEClient _client;
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

        public object FEParamGetTiposIva()
        {
            try
            {
                var auth = CreateAuthRequest();
                return _client.FEParamGetTiposIva(auth);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error en FEParamGetTiposIva: " + ex.Message, ex);
            }
        }

           
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

        /// <summary>
        /// Obtiene puntos de venta habilitados
        /// </summary>
        private List<PtoVentaInfo> ParsePtosVentaXml(string xmlString)
        {
            var resultList = new List<PtoVentaInfo>();
            try
            {
                var xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(xmlString);

                var nsmgr = new XmlNamespaceManager(xmlDoc.NameTable);
                nsmgr.AddNamespace("afip", "http://ar.gov.afip.dif.FEV1/");

                var nodes = xmlDoc.SelectNodes("//afip:PtoVenta", nsmgr);
                foreach (XmlNode node in nodes)
                {
                    var info = new PtoVentaInfo
                    {
                        PuntoVenta = int.Parse(node.SelectSingleNode("afip:Nro", nsmgr)?.InnerText ?? "0"),
                        EmisionTipo = node.SelectSingleNode("afip:EmisionTipo", nsmgr)?.InnerText,
                        Bloqueado = node.SelectSingleNode("afip:Bloqueado", nsmgr)?.InnerText,
                        FchBaja = node.SelectSingleNode("afip:FchBaja", nsmgr)?.InnerText
                    };
                    resultList.Add(info);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error parseando XML: {ex.Message}");
            }
            return resultList;
        }

        public object FEParamGetTiposMonedas()
        {
            try
            {
                var auth = CreateAuthRequest();
                return _client.FEParamGetTiposMonedas(auth);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error en FEParamGetTiposMonedas: " + ex.Message, ex);
            }
        }

        public object FEParamGetCotizacion(string moneda, string fecha = null)
        {
            try
            {
                var auth = CreateAuthRequest();
                fecha ??= DateTime.Now.ToString("yyyyMMdd");
                return _client.FEParamGetCotizacion(auth, moneda, fecha);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error en FEParamGetCotizacion: " + ex.Message, ex);
            }
        }

        public object FEParamGetTiposTributos()
        {
            try
            {
                var auth = CreateAuthRequest();
                return _client.FEParamGetTiposTributos(auth);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error en FEParamGetTiposTributos: " + ex.Message, ex);
            }
        }

        public object FEParamGetTiposOpcional()
        {
            try
            {
                var auth = CreateAuthRequest();
                return _client.FEParamGetTiposOpcional(auth);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error en FEParamGetTiposOpcional: " + ex.Message, ex);
            }
        }

        private FEAuthRequest CreateAuthRequest()
        {
            return new FEAuthRequest
            {
                Token = _auth.Token,
                Sign = _auth.Sign,
                Cuit = _auth.Cuit
            };
        }

        private FECAERequest ConvertToFECAERequest(object obj)
        {
            try
            {
                var request = new FECAERequest();
                var feCabReq = GetPropertyValue(obj, "FeCabReq");
                if (feCabReq != null)
                {
                    request.FeCabReq = new FECabReq
                    {
                        CantReg = Convert.ToInt32(GetPropertyValue(feCabReq, "CantReg")),
                        PtoVta = Convert.ToInt32(GetPropertyValue(feCabReq, "PtoVta")),
                        CbteTipo = Convert.ToInt32(GetPropertyValue(feCabReq, "CbteTipo"))
                    };
                }

                var feDetReqArray = GetPropertyValue(obj, "FeDetReq") as Array;
                if (feDetReqArray != null)
                {
                    var detList = new List<FEDetReq>();
                    foreach (var det in feDetReqArray)
                    {
                        var detReq = new FEDetReq
                        {
                            Concepto = Convert.ToInt32(GetPropertyValue(det, "Concepto")),
                            DocTipo = Convert.ToInt32(GetPropertyValue(det, "DocTipo")),
                            DocNro = Convert.ToInt64(GetPropertyValue(det, "DocNro")),
                            CbteDesde = Convert.ToInt64(GetPropertyValue(det, "CbteDesde")),
                            CbteHasta = Convert.ToInt64(GetPropertyValue(det, "CbteHasta")),
                            CbteFch = Convert.ToString(GetPropertyValue(det, "CbteFch")),
                            ImpTotal = Convert.ToDecimal(GetPropertyValue(det, "ImpTotal")),
                            ImpTotConc = Convert.ToDecimal(GetPropertyValue(det, "ImpTotConc")),
                            ImpNeto = Convert.ToDecimal(GetPropertyValue(det, "ImpNeto")),
                            ImpOpEx = Convert.ToDecimal(GetPropertyValue(det, "ImpOpEx")),
                            ImpTrib = Convert.ToDecimal(GetPropertyValue(det, "ImpTrib")),
                            ImpIVA = Convert.ToDecimal(GetPropertyValue(det, "ImpIVA")),
                            FchServDesde = CStrSafe(GetPropertyValue(det, "FchServDesde")),
                            FchServHasta = CStrSafe(GetPropertyValue(det, "FchServHasta")),
                            FchVtoPago = CStrSafe(GetPropertyValue(det, "FchVtoPago")),
                            MonId = Convert.ToString(GetPropertyValue(det, "MonId")),
                            MonCotiz = Convert.ToDecimal(GetPropertyValue(det, "MonCotiz"))
                        };

                        var ivaArray = GetPropertyValue(det, "Iva") as Array;
                        if (ivaArray != null)
                        {
                            var ivaList = new List<FEIva>();
                            foreach (var iva in ivaArray)
                            {
                                ivaList.Add(new FEIva
                                {
                                    Id = Convert.ToInt32(GetPropertyValue(iva, "Id")),
                                    BaseImp = Convert.ToDecimal(GetPropertyValue(iva, "BaseImp")),
                                    Importe = Convert.ToDecimal(GetPropertyValue(iva, "Importe"))
                                });
                            }
                            detReq.Iva = ivaList.ToArray();
                        }

                        detList.Add(detReq);
                    }
                    request.FeDetReq = detList.ToArray();
                }

                return request;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al convertir FECAERequest: " + ex.Message, ex);
            }
        }

        private object ConvertFECAEResponseToObject(FECAEResponse response)
        {
            var cabResp = new
            {
                response.FeCabResp.Cuit,
                response.FeCabResp.PtoVta,
                response.FeCabResp.CbteTipo,
                response.FeCabResp.FchProceso,
                response.FeCabResp.CantReg,
                response.FeCabResp.Resultado,
                response.FeCabResp.Reproceso
            };

            var detRespList = new List<object>();
            if (response.FeDetResp != null)
            {
                foreach (var det in response.FeDetResp)
                {
                    var obsList = new List<object>();
                    if (det.Observaciones != null)
                    {
                        foreach (var obs in det.Observaciones)
                        {
                            obsList.Add(new { obs.Code, obs.Msg });
                        }
                    }

                    detRespList.Add(new
                    {
                        det.Concepto,
                        det.DocTipo,
                        det.DocNro,
                        det.CbteDesde,
                        det.CbteHasta,
                        det.CbteFch,
                        det.Resultado,
                        det.CAE,
                        det.CAEFchVto,
                        Observaciones = obsList.ToArray()
                    });
                }
            }

            var errList = new List<object>();
            if (response.Errors != null)
            {
                foreach (var errorItem in response.Errors)
                {
                    errList.Add(new { errorItem.Code, errorItem.Msg });
                }
            }

            return new
            {
                FeCabResp = cabResp,
                FeDetResp = detRespList.ToArray(),
                Errors = errList.ToArray()
            };
        }

        private object GetPropertyValue(object obj, string propertyName)
        {
            if (obj == null) return null;
            var propInfo = obj.GetType().GetProperty(propertyName);
            return propInfo?.GetValue(obj);
        }

        private string CStrSafe(object obj) => obj == null ? "" : Convert.ToString(obj);

        public void Dispose()
        {
            _client = null;
        }

        private static void MostrarErrorDetallado(string titulo, string mensaje, string detalles)
        {
            try
            {
                Console.WriteLine("=== MostrarErrorDetallado ===");
                Console.WriteLine("Título: " + titulo);
                Console.WriteLine("Mensaje: " + mensaje);
                Console.WriteLine("Detalles: " + detalles);

                var frmError = new frm_error_detalle();
                frmError.MostrarError(titulo, mensaje, detalles);
                frmError.ShowDialog();

                Console.WriteLine("=== Formulario de error mostrado correctamente ===");
            }
            catch (Exception ex)
            {
                Console.WriteLine("=== Error al mostrar formulario de errores ===");
                Console.WriteLine("Error: " + ex.Message);
                Console.WriteLine("Stack Trace: " + ex.StackTrace);

                MessageBox.Show($"{mensaje}\n\n=== DETALLES TÉCNICOS ===\n{detalles}", titulo,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
