using Centrex.Afip;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography.Pkcs;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using ZXing;
using ZXing.Windows.Compatibility;

namespace Centrex
{
    public static class factura_electronica
    {
        // Propiedades públicas para acceso desde WSFEv1
        public static string modo { get; private set; }
        public static string token { get; private set; }
        public static string sign { get; private set; }
        public static string cuit_emisor { get; private set; }
        public static PedidoEntity pAnulado;
        public static ComprobanteEntity cAnulado;
        

        // Propiedades privadas para configuración
        private static string archivo_certificado;
        private static string archivo_licencia;
        private static string password_certificado;
        private static AfipMode afipMode;

        private const string WSAA_HOMO = "https://wsaahomo.afip.gov.ar/ws/services/LoginCms";
        private const string WSAA_PROD = "https://wsaa.afip.gov.ar/ws/services/LoginCms";
        private const string WSFE_HOMO = "https://wswhomo.afip.gov.ar/wsfev1/service.asmx";
        private const string WSFE_PROD = "https://servicios1.afip.gov.ar/wsfev1/service.asmx";
        

        /// <summary>
        /// Función principal de facturación - MANTIENE LA FIRMA ORIGINAL
        /// Devuelve 1 si está OK, 0 si hay error
        /// </summary>
        public static int facturar(PedidoEntity p)
        {
            // SI DEVUELVE 1 ESTÁ TODO OK
            // SI DEVUELVE 0 HUBO UN ERROR

            try
            {
                ClienteEntity cl = new ClienteEntity();
                ComprobanteEntity c = new ComprobanteEntity();
                PedidoEntity pAnulado;
                ComprobanteEntity cAnulado;
                string _fechaAFIP;

                // Obtengo los datos del comprobante
                c = comprobantes.info_comprobante(p.IdComprobante);

                // ============================================
                // COMPROBANTES MANUALES Y PRESUPUESTOS
                // ============================================
                if ((c.EsPresupuesto ?? false) || (c.EsManual ?? false))
                {
                    c.NumeroComprobante += 1;
                    comprobantes.updatecomprobante(c);
                    p.Cerrado = true;
                    p.Activo = false;
                    p.PuntoVenta = c.PuntoVenta;
                    if (c.EsPresupuesto ?? false)
                    {
                        p.IdPresupuesto = c.NumeroComprobante;
                        p.NumeroComprobante = 0;
                    }

                    {
                        p.NumeroComprobante = c.NumeroComprobante;
                        p.IdPresupuesto = 0;
                    }
                    UpdatePedido(p);
                    return 1;
                }

                // ============================================
                // FACTURACIÓN ELECTRÓNICA
                // ============================================

                // Obtengo los datos del cliente
                cl = clientes.info_cliente(p.IdCliente);
                _fechaAFIP = generales.fechaAFIP(); // Fecha de hoy

                // Inicializar configuración AFIP y crear conexión con WSFE
                WSFEv1 wsfe = InicialesFE(c.Testing);

                if (wsfe == null)
                {
                    Interaction.MsgBox("Hubo un problema al inicializar el webservice, puede ser un problema de certificados o de conexión con AFIP",
                                    (MsgBoxStyle)((int)Constants.vbCritical + (int)Constants.vbOKOnly), "Centrex");
                    return 0;
                }


                // Obtener último número de comprobante
                int ultimoNro;
                try
                {
                    ultimoNro = wsfe.FECompUltimoAutorizado(c.PuntoVenta, c.IdTipoComprobante);
                }
                catch (Exception ex)
                {
                    Interaction.MsgBox($"Error al consultar último comprobante: {ex.Message}",
              (MsgBoxStyle)((int)Constants.vbCritical + (int)Constants.vbOKOnly), "Centrex");
                    return 0;
                }

                int nuevoNro = ultimoNro + 1;

                // ============================================
                // ARMAR REQUEST SEGÚN TIPO DE COMPROBANTE
                // ============================================

                var req = new Centrex.Afip.Proxy.FECAERequest();

                // Cabecera
                req.FeCabReq = new Centrex.Afip.Proxy.FECabReq()
                {
                    CantReg = 1,
                    CbteTipo = c.IdTipoComprobante,
                    PtoVta = c.PuntoVenta
                };

                // Detalle básico
                var det = new Centrex.Afip.Proxy.FEDetReq()
                {
                    Concepto = 1,
                    DocTipo = cl.IdTipoDocumento,
                    DocNro = Conversions.ToLong(string.IsNullOrEmpty(cl.TaxNumber) ? "0" : cl.TaxNumber.Replace("-", "")),
                    CbteDesde = nuevoNro,
                    CbteHasta = nuevoNro,
                    CbteFch = _fechaAFIP,
                    ImpTotal = Math.Round(p.Total, 2),
                    ImpTotConc = 0,
                    ImpNeto = Math.Round(p.Subtotal, 2),
                    ImpOpEx = 0,
                    ImpIVA = Math.Round(p.Iva ?? 0, 2),
                    ImpTrib = 0,
                    MonId = "PES",
                    MonCotiz = 1
                };

                // IVA
                int ivaId = (p.Subtotal == p.Total && (p.Iva ?? 0) == 0) ? 3 : 5; // 3=0%, 5=21%
                det.Iva = new Centrex.Afip.Proxy.FEIva[]
                       {
   new Centrex.Afip.Proxy.FEIva()
     {
    Id = ivaId,
   BaseImp = Math.Round(p.Subtotal, 2),
    Importe = Math.Round(p.Iva ?? 0, 2)
         }
                        };

                // ============================================
                // COMPROBANTES MIPYME
                // ============================================
                if (c.EsMiPyMe != false)
                {
                    det.FchVtoPago = _fechaAFIP;

                    var opcionales = new List<Centrex.Afip.Proxy.FEOpcional>();

                    // CBU (campo 2101)
                    if (!string.IsNullOrEmpty(c.CbuEmisor))
                    {
                        opcionales.Add(new Centrex.Afip.Proxy.FEOpcional()
                        {
                            Id = 2101,
                            Valor = c.CbuEmisor
                        });
                    }

                    // Alias CBU (campo 2102)
                    if (!string.IsNullOrEmpty(c.AliasCbuEmisor))
                    {
                        opcionales.Add(new Centrex.Afip.Proxy.FEOpcional()
                        {
                            Id = 2102,
                            Valor = c.AliasCbuEmisor
                        });
                    }

                    // Modo MiPyME (campo 27)
                    if ((c.IdModoMiPyme) > 0)
                    {
                        var modoMiPyme = info_modoMiPyme(c.IdModoMiPyme);
                        if (modoMiPyme != null)
                        {
                            opcionales.Add(new Centrex.Afip.Proxy.FEOpcional()
                            {
                                Id = 27,
                                Valor = modoMiPyme.abreviatura
                            });
                        }
                    }

                    // Campo 22 - Anula MiPyME (solo para NC)
                    if (c.AnulaMiPyMe?.ToUpper() == "S" || c.AnulaMiPyMe?.ToUpper() == "N")
                    {
                        opcionales.Add(new Centrex.Afip.Proxy.FEOpcional()
                        {
                            Id = 22,
                            Valor = c.AnulaMiPyMe.ToUpper()
                        });
                    }

                    det.Opcionales = opcionales.ToArray();

                    // Documentos asociados para NC/ND MiPyME
                    if (EsNotaCreditoODebito(c.IdTipoComprobante) && (p.NumeroPedidoAnulado ?? 0) > 0)
                    {
                        pAnulado = Pedidos.Info_pedido(p.NumeroPedidoAnulado ?? 0);
                        if (pAnulado != null)
                        {
                            cAnulado = comprobantes.info_comprobante(pAnulado.IdComprobante);
                            if (cAnulado != null)
                            {
                                det.CbtesAsoc = new Centrex.Afip.Proxy.FECbtesAsoc[]
                                    {
    new Centrex.Afip.Proxy.FECbtesAsoc()
        {
     Tipo = cAnulado.IdTipoComprobante,
   PtoVta = pAnulado.PuntoVenta ?? 0,
 Nro = p.NumeroComprobanteAnulado ?? 0,
       Cuit = Conversions.ToLong(cuit_emisor_default?.Replace("-", "") ?? "0"),
       CbteFch = _fechaAFIP
   }
                                   };
                            }
                        }
                    }
                }
                else
                {
                    // ============================================
                    // FACTURAS NORMALES - DOCUMENTOS ASOCIADOS PARA NC/ND
                    // ============================================
                    if (EsNotaCreditoODebito(c.IdTipoComprobante) && (p.NumeroPedidoAnulado ?? 0) > 0)
                    {
                        pAnulado = Info_pedido(p.NumeroPedidoAnulado ?? 0);
                        if (pAnulado != null)
                        {
                            cAnulado = comprobantes.info_comprobante(pAnulado.IdComprobante);
                            if (cAnulado != null)
                            {
                                det.CbtesAsoc = new Centrex.Afip.Proxy.FECbtesAsoc[]
                           {
     new Centrex.Afip.Proxy.FECbtesAsoc()
                 {
   Tipo = cAnulado.IdTipoComprobante,
    PtoVta = pAnulado.PuntoVenta ?? 0,
           Nro = p.NumeroComprobanteAnulado ?? 0,
    Cuit = Conversions.ToLong(cuit_emisor_default?.Replace("-", "") ?? "0"),
  CbteFch = _fechaAFIP
      }
                             };
                            }
                        }
                    }
                }

                req.FeDetReq = new Centrex.Afip.Proxy.FEDetReq[] { det };

                // ============================================
                // ENVIAR A AFIP
                // ============================================
                object resp;
                try
                {
                    resp = wsfe.FECAESolicitar(req);
                }
                catch (Exception ex)
                {
                    Interaction.MsgBox($"Error al enviar a AFIP: {ex.Message}",
                   (MsgBoxStyle)((int)Constants.vbCritical + (int)Constants.vbOKOnly), "Centrex");
                    return 0;
                }

                // ============================================
                // PROCESAR RESPUESTA
                // ============================================
                if (resp == null)
                {
                    Interaction.MsgBox("No se recibió respuesta de AFIP",
                       (MsgBoxStyle)((int)Constants.vbCritical + (int)Constants.vbOKOnly), "Centrex");
                    return 0;
                }

                // Verificar errores generales
                if (((dynamic)resp).Errors != null && ((dynamic)resp).Errors.Length > 0)
                {
                    string errMsg = FormatearErroresAFIP(resp);
                    Interaction.MsgBox($"{errorFE_new(wsfe, c, ultimoNro)}\r\n\r\n{errMsg}",
                              (MsgBoxStyle)((int)Constants.vbCritical + (int)Constants.vbOKOnly), "Centrex");
                    return 0;
                }

                // Verificar detalle
                if (((dynamic)resp).FeDetResp == null || ((dynamic)resp).FeDetResp.Length == 0)
                {
                    Interaction.MsgBox("La respuesta no contiene información del comprobante",
                                (MsgBoxStyle)((int)Constants.vbCritical + (int)Constants.vbOKOnly), "Centrex");
                    return 0;
                }

                dynamic detResp = ((dynamic)resp).FeDetResp[0];

                // Verificar resultado
                if (detResp.Resultado != "A")
                {
                    string errMsg = FormatearErroresAFIP(resp);
                    Interaction.MsgBox($"{errorFE_new(wsfe, c, ultimoNro)}\r\n\r\n{errMsg}",
                          (MsgBoxStyle)((int)Constants.vbExclamation + (int)Constants.vbOKOnly), "Centrex");
                    return 0;
                }

                // ============================================
                // ÉXITO - ACTUALIZAR BASE DE DATOS
                // ============================================

                // Actualizar comprobante
                c.NumeroComprobante = nuevoNro;
                comprobantes.updatecomprobante(c);

                // Actualizar pedido
                p.Cae = detResp.CAE;
                p.FechaVencimientoCae = generales.fechaAFIP_fecha(detResp.CAEFchVto);
                p.Cerrado = true;
                p.Activo = false;
                p.PuntoVenta = c.PuntoVenta;
                p.NumeroComprobante = nuevoNro;

                // Generar código de barras
                p.CodigoDeBarras = GenerarCodigoDeBarras(cuit_emisor_default, p.NumeroComprobante.ToString(), p.PuntoVenta.ToString(), p.Cae, detResp.CAEFchVto);

                // Generar y guardar QR
                try
                {
                    string nombreQR = $"{Application.StartupPath}\\QR\\{cl.TaxNumber}_{nuevoNro}_{p.IdPedido}_{_fechaAFIP}.jpg";
                    if (GenerarYGuardarQR_AFIP(c, p, cl, nombreQR, afipMode))
                    {
                        Guardar_QR_DB(nombreQR, p.IdPedido);
                    }
                }
                catch (Exception ex)
                {
                    // QR no crítico - continuar
                    Console.WriteLine($"Error al generar QR: {ex.Message}");
                }

                // Guardar pedido
                UpdatePedido(p);

                return 1;
            }
            catch (Exception ex)
            {
                Interaction.MsgBox($"Error crítico: {ex.Message}\r\n\r\n{ex.StackTrace}",
              (MsgBoxStyle)((int)Constants.vbCritical + (int)Constants.vbOKOnly), "Centrex");
                return 0;
            }
        }

        private static string GenerarCodigoDeBarras(string cuit_emisor, string numeroComprobante, string puntoVenta, string cae, string fechaVencimiento_cae)
        {
            long impares = 0;
            long pares = 0;
            long impares3;
            long total;
            int digitoVerificador;
            string codigoDeBarras;

            string cod = cuit_emisor + numeroComprobante + puntoVenta + cae + fechaVencimiento_cae;

            // Analizo la cadena de caracteres:
            // Tengo que sumar todos los caracteres impares y los pares
            for (int i = 1; i <= cod.Length; i++)
            {
                if (i % 2 == 0)
                {
                    // Si el valor de i es par entra por acá
                    pares += long.Parse(cod.Substring(i - 1, 1));
                }
                else
                {
                    // Si el valor de i es impar entra por acá
                    impares += long.Parse(cod.Substring(i - 1, 1));
                }
            }

            impares3 = 3 * impares;
            total = pares + impares3;
            digitoVerificador = 10 - (int)(total % 10);

            // Si el dígito verificador es 10, debe ser 0
            if (digitoVerificador == 10)
                digitoVerificador = 0;

            codigoDeBarras = cod + digitoVerificador.ToString();

            return codigoDeBarras;
        }

        /// <summary>
        /// Inicializa configuración AFIP - MANTIENE LA FIRMA ORIGINAL
        /// </summary>
        //private static bool inicialesFE(bool esTest)
        //{
        //    try
        //    {
        //        string pc = pc;

        //        if (esTest)
        //        {
        //            modo = "HOMO";
        //            switch (pc?.ToUpper())
        //            {
        //                case "JARVIS":
        //                case "SERVTEC-06":
        //                    archivo_certificado = Application.StartupPath + "\\Certificados\\JARVIS20171211.pfx";
        //                    break;
        //                case "BRUNO":
        //                    archivo_certificado = Application.StartupPath + "\\Certificados\\bruno2023_prueba.pfx";
        //                    break;
        //                case "SILVIA":
        //                    archivo_certificado = Application.StartupPath + "\\Certificados\\SILVIA.pfx";
        //                    break;
        //                default:
        //                    Interaction.MsgBox("PC no habilitada para emitir documentos de testing.",
        //                     (MsgBoxStyle)((int)Constants.vbCritical + (int)Constants.vbOKOnly), "Centrex");
        //                    return false;
        //            }
        //        }
        //        else
        //        {
        //            modo = "PROD";
        //            switch (pc?.ToUpper())
        //            {
        //                case "JARVIS":
        //                    archivo_certificado = Application.StartupPath + "\\Certificados\\BRUNO.pfx";
        //                    break;
        //                case "SERVTEC-06":
        //                    archivo_certificado = Application.StartupPath + "\\Certificados\\JARVIS.pfx";
        //                    break;
        //                case "BRUNO":
        //                    archivo_certificado = Application.StartupPath + "\\Certificados\\Bruno.pfx";
        //                    break;
        //                case "SILVIA":
        //                    archivo_certificado = Application.StartupPath + "\\Certificados\\SILVIA.pfx";
        //                    break;
        //                default:
        //                    Interaction.MsgBox("PC no habilitada para emitir documentos fiscales.",
        //                         (MsgBoxStyle)((int)Constants.vbCritical + (int)Constants.vbOKOnly), "Centrex");
        //                    return false;
        //            }
        //        }

        //        if (!File.Exists(archivo_certificado))
        //        {
        //            Interaction.MsgBox("No existe el archivo del certificado, no podrá continuar.",
        //       (MsgBoxStyle)((int)Constants.vbCritical + (int)Constants.vbOKOnly), "Centrex");
        //            return false;
        //        }

        //        cuit_emisor = cuit_emisor_default;
        //        password_certificado = "Ladeda78";

        //        // Configurar AfipConfig con las propiedades dinámicas
        //        var afipMode = esTest ? AfipMode.HOMO : AfipMode.PROD;
        //        AfipConfig.DynamicCertPath = archivo_certificado;
        //        AfipConfig.DynamicCertPassword = password_certificado;
        //        AfipConfig.DynamicCuitEmisor = Conversions.ToLong(cuit_emisor);
        //        AfipConfig.Mode = afipMode;

        //        // Validar configuración
        //        var validation = AfipConfig.ValidateConfig(afipMode);
        //        if (!validation.isValid)
        //        {
        //            Interaction.MsgBox($"Error en la configuración: {validation.errorMessage}",
        //                  (MsgBoxStyle)((int)Constants.vbCritical + (int)Constants.vbOKOnly), "Centrex");
        //            return false;
        //        }

        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        Interaction.MsgBox($"Error al inicializar: {ex.Message}",
        //                 (MsgBoxStyle)((int)Constants.vbCritical + (int)Constants.vbOKOnly), "Centrex");
        //        return false;
        //    }
        //}

        ///// <summary>
        ///// Genera código de barras según especificaciones AFIP - MANTIENE LA FIRMA ORIGINAL
        ///// </summary>
        //private static string generarCodigoDeBarras(string cuit, long nroComprobante, int ptoVenta, string cae, string vtoCAE)
        //{
        //    try
        //    {
        //        string cuitStr = cuit.Replace("-", "").PadLeft(11, '0');
        //        string tipoComp = "006"; // Ajustar según tipo
        //        string ptoVtaStr = ptoVenta.ToString().PadLeft(5, '0');
        //        string caeStr = cae.PadLeft(14, '0');
        //        string vtoStr = vtoCAE.Replace("/", "").PadLeft(8, '0');

        //        return cuitStr + tipoComp + ptoVtaStr + caeStr + vtoStr;
        //    }
        //    catch (Exception ex)
        //    {
        //        return "";
        //    }
        //}
        private static WSFEv1 InicialesFE(bool esTest)
        {
            try
            {
                string archivo_certificado = "";
                string password_certificado = "Ladeda78";
                string cuit_emisor = cuit_emisor_default;
                AfipMode afipMode = esTest ? AfipMode.HOMO : AfipMode.PROD;

                // ==========================
                // Selección de certificado
                // ==========================
                switch (pc)
                {
                    case "JARVIS":
                        archivo_certificado = Application.StartupPath + (esTest ? "\\Certificados\\JARVIS20171211.pfx" : "\\Certificados\\BRUNO.pfx");
                        break;
                    case "SERVTEC-06":
                        archivo_certificado = Application.StartupPath + (esTest ? "\\Certificados\\JARVIS20171211.pfx" : "\\Certificados\\JARVIS.pfx");
                        break;
                    case "BRUNO":
                        archivo_certificado = Application.StartupPath + (esTest ? "\\Certificados\\bruno2023_prueba.pfx" : "\\Certificados\\Bruno.pfx");
                        break;
                    case "SILVIA":
                        archivo_certificado = Application.StartupPath + "\\Certificados\\SILVIA.pfx";
                        break;
                    default:
                        Interaction.MsgBox(
                            esTest
                                ? "PC no habilitada para emitir documentos de testing."
                                : "PC no habilitada para emitir documentos fiscales.",
                            (MsgBoxStyle)((int)Constants.vbCritical + (int)Constants.vbOKOnly),
                            "Centrex");
                        return null;
                }

                // ==========================
                // Verificar existencia del archivo
                // ==========================
                if (!File.Exists(archivo_certificado))
                {
                    Interaction.MsgBox("No existe el archivo del certificado, no podrá continuar.",
                        (MsgBoxStyle)((int)Constants.vbCritical + (int)Constants.vbOKOnly),
                        "Centrex");
                    return null;
                }

                // ==========================
                // Configurar AFIP
                // ==========================
                AfipConfig.DynamicCertPath = archivo_certificado;
                AfipConfig.DynamicCertPassword = password_certificado;
                AfipConfig.DynamicCuitEmisor = Conversions.ToLong(cuit_emisor);
                AfipConfig.Mode = afipMode;

                var validation = AfipConfig.ValidateConfig(afipMode);
                if (!validation.isValid)
                {
                    Interaction.MsgBox($"Error en la configuración: {validation.errorMessage}",
                        (MsgBoxStyle)((int)Constants.vbCritical + (int)Constants.vbOKOnly),
                        "Centrex");
                    return null;
                }

                // ==========================
                // Crear conexión WSFEv1
                // ==========================
                try
                {
                    var wsfe = WSFEv1.CreateWithTa(afipMode);
                    return wsfe;
                }
                catch (Exception ex)
                {
                    Interaction.MsgBox($"Error al conectar con AFIP: {ex.Message}\r\n\r\nPROBLEMA DE AFIP",
                        (MsgBoxStyle)((int)Constants.vbCritical + (int)Constants.vbOKOnly),
                        "Centrex");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Interaction.MsgBox($"Error al inicializar AFIP: {ex.Message}",
                    (MsgBoxStyle)((int)Constants.vbCritical + (int)Constants.vbOKOnly),
                    "Centrex");
                return null;
            }
        }

        /// <summary>
        /// Genera y guarda el código QR según especificaciones AFIP
        /// </summary>
        private static bool GenerarYGuardarQR_AFIP(ComprobanteEntity c, PedidoEntity p, ClienteEntity cl, string rutaArchivo, AfipMode modo)
        {
            try
            {
                // Crear carpeta si no existe
                string carpetaQR = Path.GetDirectoryName(rutaArchivo);
                if (!Directory.Exists(carpetaQR))
                {
                    Directory.CreateDirectory(carpetaQR);
                }

                // Construir JSON para el QR según especificaciones AFIP
                var datosQR = new StringBuilder();
                datosQR.Append("{");
                datosQR.AppendFormat("\"ver\":1,");
                datosQR.AppendFormat("\"fecha\":\"{0}\",", DateTime.Now.ToString("yyyy-MM-dd"));
                datosQR.AppendFormat("\"cuit\":{0},", AfipConfig.GetCuitEmisor());
                datosQR.AppendFormat("\"ptoVta\":{0},", p.PuntoVenta);
                datosQR.AppendFormat("\"tipoCmp\":{0},", c.IdTipoComprobante);
                datosQR.AppendFormat("\"nroCmp\":{0},", p.NumeroComprobante);
                datosQR.AppendFormat("\"importe\":{0},", p.Total.ToString("0.00").Replace(",", "."));
                datosQR.AppendFormat("\"moneda\":\"{0}\",", "PES");
                datosQR.AppendFormat("\"ctz\":1,");
                datosQR.AppendFormat("\"tipoDocRec\":{0},", cl.IdTipoDocumento);
                datosQR.AppendFormat("\"nroDocRec\":{0},",
             Conversions.ToLong(string.IsNullOrEmpty(cl.TaxNumber) ? "0" : cl.TaxNumber.Replace("-", "")));
                datosQR.AppendFormat("\"tipoCodAut\":\"{0}\",", "E");
                datosQR.AppendFormat("\"codAut\":{0}", Conversions.ToLong(p.Cae));
                datosQR.Append("}");

                // Codificar en Base64
                string datosQRBase64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(datosQR.ToString()));

                // Construir URL
                string urlQR = "https://www.afip.gob.ar/fe/qr/?p=" + datosQRBase64;

                // Generar imagen QR usando ZXing
                var writer = new BarcodeWriter<Bitmap>
                {
                    Format = BarcodeFormat.QR_CODE,
                    Options = new ZXing.QrCode.QrCodeEncodingOptions
                    {
                        Height = 300,
                        Width = 300,
                        Margin = 1,
                        ErrorCorrection = ZXing.QrCode.Internal.ErrorCorrectionLevel.M
                    },
                    Renderer = new BitmapRenderer()
                };

                var bitmapQR = writer.Write(urlQR);
                bitmapQR.Save(rutaArchivo, ImageFormat.Jpeg);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al generar QR: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Guarda imagen QR en la base de datos - MANTIENE LA FIRMA ORIGINAL
        /// </summary>
        public static int Guardar_QR_DB(string archivo_imagen, int id_pedido)
        {
            try
            {
                if (!File.Exists(archivo_imagen))
                {
                    return 0;
                }

                var img = Image.FromFile(archivo_imagen);
                var ms = new MemoryStream();
                img.Save(ms, ImageFormat.Jpeg);
                byte[] md = ms.GetBuffer();

                using (var context = new CentrexDbContext())
                {
                    var pedido = context.PedidoEntity.FirstOrDefault(p => p.IdPedido == id_pedido);
                    if (pedido != null)
                    {
                        pedido.FcQr = md;
                        context.SaveChanges();
                        return 0;
                    }
                }

                return -1;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al guardar QR en DB: {ex.Message}");
                return -1;
            }
        }

        /// <summary>
        /// Formatea errores de AFIP - NUEVA FUNCIÓN
        /// </summary>
        private static string FormatearErroresAFIP(object resp)
        {
            try
            {
                var sb = new StringBuilder();

                if (((dynamic)resp).Errors != null && ((dynamic)resp).Errors.Length > 0)
                {
                    sb.AppendLine("Errores:");
                    foreach (var error in ((dynamic)resp).Errors)
                    {
                        sb.AppendLine($"  [{error.Code}] {error.Msg}");
                    }
                }

                if (((dynamic)resp).FeDetResp != null && ((dynamic)resp).FeDetResp.Length > 0)
                {
                    var detResp = ((dynamic)resp).FeDetResp[0];
                    if (detResp.Observaciones != null && detResp.Observaciones.Length > 0)
                    {
                        if (sb.Length > 0) sb.AppendLine();
                        sb.AppendLine("Observaciones:");
                        foreach (var obs in detResp.Observaciones)
                        {
                            sb.AppendLine($"  [{obs.Code}] {obs.Msg}");
                        }
                    }
                }

                return sb.Length == 0 ? "Error desconocido" : sb.ToString();
            }
            catch
            {
                return "Error al procesar mensaje";
            }
        }

        /// <summary>
        /// Mensaje de error compatible con el formato original - NUEVA FUNCIÓN
        /// </summary>
        private static string errorFE_new(WSFEv1 wsfe, ComprobanteEntity c, int ultimoNro)
        {
            string errorStr = "";
            errorStr = $"El número del último comprobante autorizado para: {c.Comprobante} es: {ultimoNro}";
            return errorStr;
        }

        /// <summary>
        /// Verifica si un tipo de comprobante es NC o ND - NUEVA FUNCIÓN
        /// </summary>
        private static bool EsNotaCreditoODebito(int tipoComprobante)
        {
            int[] notasCredito = { 2, 3, 7, 8, 12, 13, 52, 53, 202, 203, 207, 208, 212, 213 };
            int[] notasDebito = { 4, 5, 9, 10, 14, 15, 54, 55, 204, 205, 209, 210, 214, 215 };
            return notasCredito.Contains(tipoComprobante) || notasDebito.Contains(tipoComprobante);
        }

        /// <summary>
        /// Imprime factura - MANTIENE LA FIRMA ORIGINAL
        /// </summary>
        public static void imprimirFactura(int id_pedido, bool esPresupuesto, bool imprimeRemito)
        {
            if (showrpt)
            {
                id = id_pedido;
                var frm = new frm_prnCmp(esPresupuesto, imprimeRemito);
                frm.ShowDialog();
                id = 0;
            }
        }

        /// <summary>
        /// Función helper para obtener información de modo MiPyME
        /// </summary>
        private static dynamic info_modoMiPyme(int id_modoMiPyMe)
        {
            // Esta función debe ser implementada según la lógica de negocio
            // Por ahora retorna null
            return null;
        }

        // ============================================================================================
        // MÉTODOS HELPER PARA COMPATIBILIDAD CON EL CÓDIGO EXISTENTE
        // ============================================================================================

        /// <summary>
        /// Genera login CMS - MÉTODO PÚBLICO
        /// </summary>
        public static bool GenerarLoginCms(bool esTest)
        {
            try
            {
                string traXml = $"<loginTicketRequest version='1.0'><header><uniqueId>{DateTime.Now.Ticks}</uniqueId><generationTime>{DateTime.Now.AddMinutes(-10):s}</generationTime><expirationTime>{DateTime.Now.AddMinutes(10d):s}</expirationTime></header><service>wsfe</service></loginTicketRequest>";
                File.WriteAllText("TRA.xml", traXml, Encoding.UTF8);

                var cert = new X509Certificate2(archivo_certificado, password_certificado);
                byte[] contenido = File.ReadAllBytes("TRA.xml");
                var contenidoDatos = new ContentInfo(contenido);
                var cmsFirmado = new SignedCms(contenidoDatos);
                cmsFirmado.ComputeSignature(new CmsSigner(cert));
                string loginCms = Convert.ToBase64String(cmsFirmado.Encode());

                string url = esTest ? WSAA_HOMO : WSAA_PROD;
                string soap = $"<?xml version=\"1.0\" encoding=\"UTF-8\"?><soap:Envelope xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:wsaa=\"http://wsaa.view.sua.dvadac.desein.afip.gov.ar/\"><soap:Body><wsaa:loginCms><wsaa:in0>{loginCms}</wsaa:in0></wsaa:loginCms></soap:Body></soap:Envelope>";

                using (var client = new HttpClient())
                {
                    var content = new StringContent(soap, Encoding.UTF8, "text/xml");
                    var response = client.PostAsync(url, content).Result;
                    string xmlResp = response.Content.ReadAsStringAsync().Result;

                    var doc = XDocument.Parse(xmlResp);
                    token = doc.Descendants().FirstOrDefault(x => x.Name.LocalName == "token")?.Value;
                    sign = doc.Descendants().FirstOrDefault(x => x.Name.LocalName == "sign")?.Value;

                    bool loginExitoso = !(string.IsNullOrEmpty(token) || string.IsNullOrEmpty(sign));

                    //WriteLog("GenerarLoginCms", $"Login CMS: {(loginExitoso ? "EXITOSO" : "FALLIDO")} - Modo: {(esTest ? "HOMO" : "PROD")}");

                    return loginExitoso;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"LoginCMS Error: {ex.Message}");
                //WriteLog("GenerarLoginCms", $"Error: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Crea conexión WSFE - MÉTODO HELPER PARA COMPATIBILIDAD
        /// </summary>
        public static object CrearConexionWSFE(string modo)
        {
            try
            {
                bool esTest = modo == "HOMO";
                if (!GenerarLoginCms(esTest))
                {
                    throw new Exception("Error al generar login CMS");
                }

                if (modo == "HOMO" || modo == "PROD")
                {
                    wsfe = WSFEv1.CreateWithTa(token, cuit_emisor);
                    return wsfe;
                }
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"CrearConexionWSFE Error: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Consulta un comprobante específico - HELPER PARA COMPATIBILIDAD
        /// </summary>
        public static object Consultar_Comprobante(int puntoVenta, int tipoComprobante, int numeroComprobante)
        {
            try
            {
                wsfe = WSFEv1.CreateWithTa(afipMode);
                if (wsfe != null)
                {
                    var resultado = ((dynamic)wsfe).FECompConsultar(puntoVenta, tipoComprobante, numeroComprobante);
                    Console.WriteLine($"ConsultarComprobante: Consulta exitosa: PV={puntoVenta}, Tipo={tipoComprobante}, Nro={numeroComprobante}");
                    return resultado;
                }
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ConsultarComprobante Error: {ex.Message}");
                throw;
            }
        }

        // ============================================================================================
        // CONSULTA ÚLTIMO COMPROBANTE (Existente - mantener)
        // ============================================================================================
        public static int ConsultaUltimoComprobante(int puntoVenta, int tipoComprobante, int numeroComprobante, bool esTest)
        {

            try
            {
                wsfe = WSFEv1.CreateWithTa(afipMode);
                if (wsfe != null)
                    try
                    {
                        string url = esTest ? WSFE_HOMO : WSFE_PROD;
                        string soap = $"<?xml version=\"1.0\"?><soap:Envelope xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\"><soap:Body><FECompUltimoAutorizado xmlns=\"http://ar.gov.afip.dif.FEV1/\"><Auth><Token>{token}</Token><Sign>{sign}</Sign><Cuit>{cuit_emisor}</Cuit></Auth><PtoVta>{puntoVenta}</PtoVta><CbteTipo>{tipoComprobante}</CbteTipo></FECompUltimoAutorizado></soap:Body></soap:Envelope>";

                        using (var client = new HttpClient())
                        {
                            var content = new StringContent(soap, Encoding.UTF8, "text/xml");
                            var response = client.PostAsync(url, content).Result;
                            string xmlResp = response.Content.ReadAsStringAsync().Result;

                            var doc = XDocument.Parse(xmlResp);
                            string nro = doc.Descendants().FirstOrDefault(x => x.Name.LocalName == "CbteNro")?.Value;
                            return string.IsNullOrEmpty(nro) ? 0 : Conversions.ToInteger(nro);
                        }
                    }
                    catch
                    {
                        return 0;
                    }
                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ConsultarComprobante Error: {ex.Message}");
                throw;
            }     
        }
    }
}
