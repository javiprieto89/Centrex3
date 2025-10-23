using System;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography.Pkcs;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using Centrex.Afip;
using MessagingToolkit.QRCode.Codec;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace Centrex
{

    static class factura_electronica
    {
        private static string modo;
        private static string archivo_certificado;
        private static string password_certificado;
        private static string cuit_emisor;
        private static string token;
        private static string sign;

        private const string WSAA_HOMO = "https://wsaahomo.afip.gov.ar/ws/services/LoginCms";
        private const string WSAA_PROD = "https://wsaa.afip.gov.ar/ws/services/LoginCms";
        private const string WSFE_HOMO = "https://wswhomo.afip.gov.ar/wsfev1/service.asmx";
        private const string WSFE_PROD = "https://servicios1.afip.gov.ar/wsfev1/service.asmx";

        // ============================================================================================
        // 1. CONFIGURACIÓN INICIAL SEGÚN PC Y ENTORNO
        // ============================================================================================
        private static bool InicialesFE(bool esTest)
        {
            string pc = Environment.MachineName.ToUpper();

            if (esTest)
            {
                modo = "Test";
                switch (pc ?? "")
                {
                    case "JARVIS":
                    case "SERVTEC-06":
                        {
                            archivo_certificado = Application.StartupPath + @"\Certificados\JARVIS20171211.pfx";
                            break;
                        }
                    case "BRUNO":
                        {
                            archivo_certificado = Application.StartupPath + @"\Certificados\bruno2023_prueba.pfx";
                            break;
                        }
                    case "SILVIA":
                        {
                            archivo_certificado = Application.StartupPath + @"\Certificados\SILVIA.pfx";
                            break;
                        }

                    default:
                        {
                            Interaction.MsgBox("PC no habilitada para emitir documentos de testing.", Constants.vbCritical);
                            return false;
                        }
                }
            }
            else
            {
                modo = "Fiscal";
                switch (pc ?? "")
                {
                    case "JARVIS":
                        {
                            archivo_certificado = Application.StartupPath + @"\Certificados\JARVISDR.pfx";
                            break;
                        }
                    case "SERVTEC-06":
                        {
                            archivo_certificado = Application.StartupPath + @"\Certificados\JARVIS.pfx";
                            break;
                        }
                    case "BRUNO":
                        {
                            archivo_certificado = Application.StartupPath + @"\Certificados\Bruno.pfx";
                            break;
                        }
                    case "SILVIA":
                        {
                            archivo_certificado = Application.StartupPath + @"\Certificados\SILVIA.pfx";
                            break;
                        }

                    default:
                        {
                            Interaction.MsgBox("PC no habilitada para emitir documentos fiscales.", Constants.vbCritical);
                            return false;
                        }
                }
            }

            if (!File.Exists(archivo_certificado))
            {
                Interaction.MsgBox("No existe el archivo del certificado.", Constants.vbCritical);
                return false;
            }

            cuit_emisor = VariablesGlobales.cuit_emisor_default;
            password_certificado = "Ladeda78";
            return true;
        }

        // ============================================================================================
        // 2. LOGIN CMS - OBTIENE TOKEN Y SIGN DESDE WSAA
        // ============================================================================================
        private static bool GenerarLoginCms(bool esTest)
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

                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
                req.Method = "POST";
                req.ContentType = "text/xml;charset=utf-8";
                using (var s = req.GetRequestStream())
                {
                    byte[] bytes = Encoding.UTF8.GetBytes(soap);
                    s.Write(bytes, 0, bytes.Length);
                }

                string xmlResp;
                using (var reader = new StreamReader(req.GetResponse().GetResponseStream()))
                {
                    xmlResp = reader.ReadToEnd();
                }

                var doc = XDocument.Parse(xmlResp);
                token = doc.Descendants().FirstOrDefault(x => x.Name.LocalName == "token")?.Value;
                sign = doc.Descendants().FirstOrDefault(x => x.Name.LocalName == "sign")?.Value;
                return !(string.IsNullOrEmpty(token) || string.IsNullOrEmpty(sign));
            }
            catch (Exception ex)
            {
                WriteLog("LoginCMS", ex.Message);
                return false;
            }
        }

        // ============================================================================================
        // 3. CONSULTA ÚLTIMO COMPROBANTE
        // ============================================================================================
        public static int ConsultaUltimoComprobante(int puntoVenta, int tipoComprobante, bool esTest)
        {
            if (!InicialesFE(esTest) || !GenerarLoginCms(esTest))
                return 0;

            try
            {
                string url = esTest ? WSFE_HOMO : WSFE_PROD;
                string soap = $"<?xml version=\"1.0\"?><soap:Envelope xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\"><soap:Body><FECompUltimoAutorizado xmlns=\"http://ar.gov.afip.dif.FEV1/\"><Auth><Token>{token}</Token><Sign>{sign}</Sign><Cuit>{cuit_emisor}</Cuit></Auth><PtoVta>{puntoVenta}</PtoVta><CbteTipo>{tipoComprobante}</CbteTipo></FECompUltimoAutorizado></soap:Body></soap:Envelope>";

                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
                req.Method = "POST";
                req.ContentType = "text/xml;charset=utf-8";
                using (var stream = req.GetRequestStream())
                {
                    byte[] data = Encoding.UTF8.GetBytes(soap);
                    stream.Write(data, 0, data.Length);
                }

                string xmlResp;
                using (var reader = new StreamReader(req.GetResponse().GetResponseStream()))
                {
                    xmlResp = reader.ReadToEnd();
                }

                var doc = XDocument.Parse(xmlResp);
                string nro = doc.Descendants().FirstOrDefault(x => x.Name.LocalName == "CbteNro")?.Value;
                return string.IsNullOrEmpty(nro) ? 0 : Conversions.ToInteger(nro);
            }
            catch
            {
                return 0;
            }
        }

        // ============================================================================================
        // 4. EMISIÓN Y REGISTRO DE FACTURA (Completo)
        // ============================================================================================
        public static bool Facturar(PedidoEntity p)
        {
            try
            {
                var c = comprobantes.info_comprobante(p.IdComprobante);
                bool esTest = c.testing;
                if (!InicialesFE(esTest))
                    return false;

                // -------- CASO 1: COMPROBANTE MANUAL O PRESUPUESTO ----------
                if (c.esManual | c.esPresupuesto)
                {
                    using (var ctx = new CentrexDbContext())
                    {
                        c.numeroComprobante += 1;
                        ctx.Entry(c).State = EntityState.Modified;

                        p.numeroComprobante = c.numeroComprobante;
                        p.cerrado = true;
                        p.activo = false;
                        ctx.Entry(p).State = EntityState.Modified;
                        ctx.SaveChanges();

                        // Registrar en cuenta corriente
                        RegistrarMovimientoCC(p);
                    }
                    WriteLog("Facturar", $"Comprobante manual/presupuesto registrado localmente. Nro={p.numeroComprobante}");
                    return true;
                }

                // -------- CASO 2: FACTURA ELECTRÓNICA ----------
                if (!GenerarLoginCms(esTest))
                    return false;

                string url = esTest ? WSFE_HOMO : WSFE_PROD;
                int nuevoNro = ConsultaUltimoComprobante(c.puntoVenta, c.id_tipoComprobante, esTest) + 1;
                string fecha = DateTime.Now.ToString("yyyyMMdd");

                // Request SOAP FECAESolicitar
                decimal subtotal = p.subtotal;
                decimal iva = p.iva ?? 0m;
                decimal total = p.total;
                string subtotalStr = subtotal.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture);
                string ivaStr = iva.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture);
                string totalStr = total.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture);

                string xml =
                    "<?xml version=\"1.0\"?><soap:Envelope xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\"><soap:Body><FECAESolicitar xmlns=\"http://ar.gov.afip.dif.FEV1/\"><Auth><Token>" + token +
                    "</Token><Sign>" + sign + "</Sign><Cuit>" + cuit_emisor + "</Cuit></Auth><FeCAEReq><FeCabReq><CantReg>1</CantReg><PtoVta>" + c.puntoVenta + "</PtoVta><CbteTipo>" + c.id_tipoComprobante +
                    "</CbteTipo></FeCabReq><FeDetReq><FECAEDetRequest><Concepto>1</Concepto><DocTipo>80</DocTipo><DocNro>0</DocNro><CbteDesde>" + nuevoNro + "</CbteDesde><CbteHasta>" + nuevoNro +
                    "</CbteHasta><CbteFch>" + fecha + "</CbteFch><ImpTotal>" + totalStr + "</ImpTotal><ImpNeto>" + subtotalStr + "</ImpNeto><ImpIVA>" + ivaStr +
                    "</ImpIVA><ImpOpEx>0</ImpOpEx><ImpTrib>0</ImpTrib><MonId>PES</MonId><MonCotiz>1</MonCotiz><Iva><AlicIva><Id>5</Id><BaseImp>" + subtotalStr + "</BaseImp><Importe>" + ivaStr +
                    "</Importe></AlicIva></Iva></FECAEDetRequest></FeDetReq></FeCAEReq></FECAESolicitar></soap:Body></soap:Envelope>";

                // Enviar request
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
                req.Method = "POST";
                req.ContentType = "text/xml;charset=utf-8";
                using (var stream = req.GetRequestStream())
                {
                    byte[] data = Encoding.UTF8.GetBytes(xml);
                    stream.Write(data, 0, data.Length);
                }

                string xmlResp;
                using (var reader = new StreamReader(req.GetResponse().GetResponseStream()))
                {
                    xmlResp = reader.ReadToEnd();
                }

                var doc = XDocument.Parse(xmlResp);
                string resultado = doc.Descendants().FirstOrDefault(x => x.Name.LocalName == "Resultado")?.Value;
                string cae = doc.Descendants().FirstOrDefault(x => x.Name.LocalName == "CAE")?.Value;
                string vto = doc.Descendants().FirstOrDefault(x => x.Name.LocalName == "CAEFchVto")?.Value;

                if (resultado != "A")
                {
                    WriteLog("Facturar", $"Rechazado: {xmlResp}");
                    Interaction.MsgBox("Comprobante rechazado por AFIP.", Constants.vbExclamation);
                    return false;
                }

                // Guardar en base
                using (var ctx = new CentrexDbContext())
                {
                    c.numeroComprobante = nuevoNro;
                    p.cae = cae;
                    p.FechaVencimientoCae = vto;
                    p.numeroComprobante = nuevoNro;
                    p.cerrado = true;
                    p.activo = false;

                    ctx.Entry(c).State = EntityState.Modified;
                    ctx.Entry(p).State = EntityState.Modified;
                    ctx.SaveChanges();

                    // Registrar movimiento CC
                    RegistrarMovimientoCC(p);
                }

                // Generar QR
                string rutaQR = CrearQR(p);
                if (!string.IsNullOrEmpty(rutaQR))
                    Guardar_QR_DB(rutaQR, p.id_pedido);

                WriteLog("Facturar", $"Autorizado - CAE: {cae} - Vto: {vto}");
                return true;
            }

            catch (Exception ex)
            {
                WriteLog("Facturar", ex.Message);
                Interaction.MsgBox("Error al facturar: " + ex.Message, Constants.vbCritical);
                return false;
            }
        }

        // ============================================================================================
        // 5. REGISTRO EN CUENTA CORRIENTE (simplificado)
        // ============================================================================================
        private static void RegistrarMovimientoCC(PedidoEntity p)
        {
            try
            {
                using (var ctx = new CentrexDbContext())
                {
                    var cc = new CcClienteEntity()
                    {
                        IdCliente = p.IdCliente,
                        Fecha = DateTime.Now,
                        Importe = p.total,
                        Detalle = "Factura " + p.numeroComprobante,
                        IdPedido = p.IdPedido
                    };
                    ctx.CcClientes.Add(cc);
                    ctx.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                WriteLog("RegistrarMovimientoCC", ex.Message);
            }
        }

        // ============================================================================================
        // 6. GENERAR Y GUARDAR QR
        // ============================================================================================
        public static string CrearQR(PedidoEntity p)
        {
            try
            {
                string json = $"{{\"ver\":1,\"fecha\":\"{DateTime.Now:yyyy-MM-dd}\",\"cuit\":{cuit_emisor},\"ptoVta\":{p.puntoVenta},\"tipoCmp\":1,\"nroCmp\":{p.numeroComprobante},\"importe\":{p.total},\"moneda\":\"PES\",\"ctz\":1,\"tipoDocRec\":80,\"nroDocRec\":0,\"tipoCodAut\":\"E\",\"codAut\":\"{p.cae}\"}}";
                string b64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(json));
                string url = $"https://www.afip.gob.ar/fe/qr/?p={b64}";

                var qr = new QRCodeEncoder();
                var img = qr.Encode(url);
                string nombreArchivo = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"QR_{p.id_pedido}.jpg");
                img.Save(nombreArchivo, ImageFormat.Jpeg);
                return nombreArchivo;
            }
            catch
            {
                return "";
            }
        }

        public static bool Guardar_QR_DB(string archivo_imagen, int id_pedido)
        {
            try
            {
                var img = Image.FromFile(archivo_imagen);
                var ms = new MemoryStream();
                img.Save(ms, ImageFormat.Jpeg);
                byte[] md = ms.ToArray();

                using (var ctx = new CentrexDbContext())
                {
                    var pedido = ctx.Pedidos.FirstOrDefault(x => x.id_pedido == id_pedido);
                    if (pedido is not null)
                    {
                        pedido.fc_qr = md;
                        ctx.SaveChanges();
                        return true;
                    }
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
    /// Consulta un comprobante en AFIP - ADAPTADO COMPLETO (usa c.testing y EF)
    /// </summary>
        public static void Consultar_Comprobante(int pVenta, int tipo_comprobante, string nComprobante)
        {
            try
            {
                // ==============================
                // 1️⃣ OBTENER DATOS DEL COMPROBANTE
                // ==============================
                var c = info_comprobante_porPtoYTipo(pVenta, tipo_comprobante);
                if (c is null)
                {
                    Interaction.MsgBox("No se encontró el comprobante asociado a ese punto de venta y tipo.", (MsgBoxStyle)((int)Constants.vbExclamation + (int)Constants.vbOKOnly), "Centrex");
                    return;
                }

                // ==============================
                // 2️⃣ DETERMINAR ENTORNO Y CONFIGURAR AFIP
                // ==============================
                bool esTest = c.testing;
                if (!InicialesFE(esTest))
                {
                    Interaction.MsgBox("Error al inicializar el webservice, revise los certificados.", (MsgBoxStyle)((int)Constants.vbCritical + (int)Constants.vbOKOnly), "Centrex");
                    return;
                }

                // Seleccionar modo según c.testing
                var afipMode = esTest ? AfipMode.HOMO : AfipMode.PROD;

                try
                {
                    VariablesGlobales.wsfe = WSFEv1.CreateWithTa(token, cuit_emisor);
                }
                catch (Exception ex)
                {
                    Interaction.MsgBox("No se pudo conectar con AFIP (" + afipMode.ToString() + "): " + ex.Message, (MsgBoxStyle)((int)Constants.vbCritical + (int)Constants.vbOKOnly), "Centrex");
                    return;
                }

                // ==============================
                // 3️⃣ CONSULTAR EN WSFE
                // ==============================
                object resultado = null;
                try
                {
                    resultado = ((dynamic)VariablesGlobales.wsfe).FECompConsultar(pVenta, tipo_comprobante, Conversions.ToLong(nComprobante));
                }
                catch (Exception ex)
                {
                    Interaction.MsgBox("Error en la consulta al WSFE: " + ex.Message, (MsgBoxStyle)((int)Constants.vbCritical + (int)Constants.vbOKOnly), "Centrex");
                    return;
                }

                if (resultado is null)
                {
                    Interaction.MsgBox("El comprobante no existe o AFIP no devolvió datos.", (MsgBoxStyle)((int)Constants.vbExclamation + (int)Constants.vbOKOnly), "Centrex");
                    return;
                }

                // ==============================
                // 4️⃣ PROCESAR RESULTADOS
                // ==============================
                string cae = Conversions.ToString(((dynamic)resultado).CAE ?? "");
                decimal impNeto = Conversions.ToDecimal(((dynamic)resultado).ImpNeto ?? 0);
                decimal impIva = Conversions.ToDecimal(((dynamic)resultado).ImpIVA ?? 0);
                decimal impTotal = Conversions.ToDecimal(((dynamic)resultado).ImpTotal ?? 0);
                string docNro = Conversions.ToString(((dynamic)resultado).DocNro ?? "");
                string estado = Conversions.ToString(((dynamic)resultado).Resultado ?? "");

                // ==============================
                // 5️⃣ MOSTRAR FORMULARIO DE RESULTADO
                // ==============================
                var resultadofc = new resultado_info_fc(cae, impNeto.ToString(), impIva.ToString(), impTotal.ToString(), docNro);
                resultadofc.ShowDialog();

                // ==============================
                // 6️⃣ LOG Y VALIDACIONES
                // ==============================
                string logMsg = $"ConsultaComprobante OK → Tipo:{tipo_comprobante}, PV:{pVenta}, N°:{nComprobante}, CAE:{cae}, Total:{impTotal}, Estado:{estado}, Entorno:{afipMode}";
                WriteLog("Consultar_Comprobante", logMsg);
            }

            catch (Exception ex)
            {
                Interaction.MsgBox("Error general al consultar comprobante: " + ex.Message, (MsgBoxStyle)((int)Constants.vbCritical + (int)Constants.vbOKOnly), "Centrex");
                WriteLog("Consultar_Comprobante", "Error crítico: " + ex.ToString());
            }
        }

        private static comprobante info_comprobante_porPtoYTipo(int puntoVenta, int tipoComprobante)
        {
            try
            {
                using (var ctx = new CentrexDbContext())
                {
                    return ctx.ComprobanteEntity.FirstOrDefault(x => x.PuntoVenta == puntoVenta && x.idTipoComprobante == tipoComprobante);
                }
            }
            catch
            {
                return null;
            }
        }



        // ============================================================================================
        // 7. LOG
        // ============================================================================================
        public static void WriteLog(string op, string msg)
        {
            try
            {
                string dir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);
                string ruta = Path.Combine(dir, $"afip_{op}_{DateTime.Now:yyyyMMdd_HHmmss}.log");
                File.AppendAllText(ruta, $"[{DateTime.Now:HH:mm:ss}] {msg}{Environment.NewLine}");
            }
            catch
            {
            }
        }


    }
}
