using MessagingToolkit.QRCode.Codec;
using Newtonsoft.Json.Linq;
using System;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Windows.Forms;

namespace Centrex
{

    public partial class frm_mercadopago_qr
    {
        private Image qrCodeImage;
        private string paymentLink;
        private string preferenceId;
        private string accessToken = "APP_USR-1861910115308653-101722-495e69898d2624ff1c403ff05258b364-2932397376";
        private Timer checkTimer;
        private string referencia;
        private static readonly HttpClient httpClient = new HttpClient();

        public frm_mercadopago_qr()
        {
            InitializeComponent();
            Text = "Pago con MercadoPago";
            Size = new Size(400, 500);
            StartPosition = FormStartPosition.CenterParent;
        }

        private void frm_mercadopago_qr_Load(object sender, EventArgs e)
        {
            try
            {
                lbl_titulo.Text = "Escanear QR para Pagar";
                lbl_titulo.Font = new Font("Arial", 14f, FontStyle.Bold);
                lbl_titulo.TextAlign = ContentAlignment.MiddleCenter;

                lbl_instrucciones.Text = "1. Abre la app de MercadoPago\r\n2. Toca 'Escanear código'\r\n3. Apunta la cámara al QR\r\n4. Confirma el pago";
                lbl_instrucciones.TextAlign = ContentAlignment.MiddleLeft;

                btn_generar_qr.Text = "Generar Nuevo QR";
                btn_copiar_link.Text = "Copiar Link de Pago";
                btn_cerrar.Text = "Cerrar";

                // Generar QR inicial
                GenerarQR();
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error inicializando formulario: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GenerarQR()
        {
            try
            {
                decimal monto = ObtenerMontoPago();
                string descripcion = "Pago Centrex - " + DateTime.Now.ToString("dd/MM/yyyy HH:mm");
                referencia = "CENTREX-" + DateTime.Now.ToString("yyyyMMddHHmmss");

                // Crear preferencia real
                preferenceId = CrearPreferenciaPago(monto, descripcion, referencia);

                if (string.IsNullOrEmpty(preferenceId))
                {
                    throw new Exception("No se pudo crear la preferencia de pago.");
                }

                paymentLink = "https://www.mercadopago.com.ar/checkout/v1/redirect?pref_id=" + preferenceId;

                // Generar QR real
                qrCodeImage = GenerarCodigoQR(paymentLink);

                // Mostrar QR
                pic_qr.Image = qrCodeImage;
                pic_qr.SizeMode = PictureBoxSizeMode.Zoom;

                lbl_monto.Text = "Monto: $" + monto.ToString("N2");
                lbl_referencia.Text = "Referencia: " + referencia;

                // Iniciar verificación de pago cada 10 segundos
                if (checkTimer is not null)
                {
                    checkTimer.Stop();
                }
                checkTimer = new Timer();
                checkTimer.Interval = 10000;
                checkTimer.Tick += VerificarEstadoPago;
                checkTimer.Start();
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error generando QR: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private decimal ObtenerMontoPago()
        {
            return 10m; // Monto de ejemplo; podés reemplazarlo por el valor real de la licencia
        }

        private string CrearPreferenciaPago(decimal monto, string descripcion, string referencia)
        {
            try
            {
                // JSON de preferencia
                string json = "{" + "\"items\":[{\"title\":\"" + descripcion + "\",\"quantity\":1,\"unit_price\":" + monto + "}]," + "\"external_reference\":\"" + referencia + "\",\"back_urls\":{\"success\":\"https://www.mercadopago.com\",\"failure\":\"https://www.mercadopago.com\"},\"auto_return\":\"approved\"" + "}";

                using var requestMessage = new HttpRequestMessage(HttpMethod.Post, "https://api.mercadopago.com/checkout/preferences")
                {
                    Content = new StringContent(json, Encoding.UTF8, "application/json")
                };
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                using var response = httpClient.Send(requestMessage);
                string result = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

                if (!response.IsSuccessStatusCode)
                {
                    throw new InvalidOperationException($"MercadoPago respondió {(int)response.StatusCode}: {response.ReasonPhrase}\n{result}");
                }

                var j = JObject.Parse(result);
                return j["id"]?.ToString(); // preference_id real
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error creando preferencia: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        private Image GenerarCodigoQR(string texto)
        {
            try
            {
                var qrEncoder = new QRCodeEncoder();
                qrEncoder.QRCodeScale = 6;
                var qrImage = qrEncoder.Encode(texto);
                return qrImage;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error generando QR: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        // Private Sub VerificarEstadoPago(sender As Object, e As EventArgs)
        // Try
        // If String.IsNullOrEmpty(preferenceId) Then Exit Sub

        // ' Buscar pagos asociados a esta preferencia
        // Dim url As String = "https://api.mercadopago.com/v1/payments/search?sort=date_created&criteria=desc&q=" & preferenceId
        // Dim client As New WebClient()
        // client.Headers.Add("Authorization", "Bearer " & accessToken)
        // Dim response As String = client.DownloadString(url)
        // Dim j As JObject = JObject.Parse(response)
        // Dim results = j("results")

        // If results.HasValues Then
        // Dim status As String = results(0)("status").ToString()
        // Dim paymentId As String = results(0)("id").ToString()

        // Select Case status
        // Case "approved"
        // checkTimer.Stop()
        // MessageBox.Show($"✅ Pago aprobado (ID: {paymentId})", "Licencia renovada", MessageBoxButtons.OK, MessageBoxIcon.Information)
        // Case "pending"
        // lbl_instrucciones.Text = "⏳ Pago pendiente, esperando acreditación..."
        // Case "rejected"
        // checkTimer.Stop()
        // MessageBox.Show("❌ Pago rechazado", "Error en pago", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        // End Select
        // End If

        // Catch ex As Exception
        // ' Ignorar errores de conexión para no interrumpir el timer
        // End Try
        // End Sub

        private void VerificarEstadoPago(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(preferenceId))
                    return;

                string url = "https://api.mercadopago.com/v1/payments/search?external_reference=" + referencia;

                using var requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                using var response = httpClient.Send(requestMessage);
                string responseBody = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                if (!response.IsSuccessStatusCode || string.IsNullOrWhiteSpace(responseBody))
                {
                    return;
                }

                var j = JObject.Parse(responseBody);
                var results = j["results"];

                if (results.HasValues)
                {
                    string status = results[0]["status"].ToString();
                    string statusDetail = results[0]["status_detail"].ToString();
                    string paymentId = results[0]["id"].ToString();
                    string paymentDate = results[0]["date_created"].ToString();

                    // === LOG LOCAL ===
                    string logLine = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} | {status.ToUpper()} | ID={paymentId} | Pref={preferenceId} | {statusDetail}";
                    File.AppendAllText("MP_Transactions.log", logLine + Environment.NewLine);

                    // === REACCIONES ===
                    switch (status ?? "")
                    {
                        case "approved":
                            {
                                checkTimer.Stop();
                                MessageBox.Show($"✅ Pago aprobado (ID: {paymentId})", "Licencia renovada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                break;
                            }

                        // TODO: acá podés activar la licencia, por ejemplo:
                        // ActualizarLicencia(True)

                        case "pending":
                            {
                                lbl_instrucciones.Text = "⏳ Pago pendiente, esperando acreditación...";
                                break;
                            }
                        case "rejected":
                            {
                                checkTimer.Stop();
                                MessageBox.Show("❌ Pago rechazado", "Error en pago", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }

                            // TODO: desactivar licencia o marcar error si querés
                            // ActualizarLicencia(False)
                    }
                }
                else
                {
                    // No hay resultados aún (todavía no pagó)
                }
            }

            catch (Exception ex)
            {
                File.AppendAllText("MP_Transactions.log", $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} | ERROR | {ex.Message}" + Environment.NewLine);
            }
        }


        private void btn_generar_qr_Click(object sender, EventArgs e)
        {
            GenerarQR();
        }

        private void btn_copiar_link_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(paymentLink))
                {
                    Clipboard.SetText(paymentLink);
                    MessageBox.Show("Link de pago copiado al portapapeles", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("No hay link de pago disponible", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error copiando link: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_cerrar_Click(object sender, EventArgs e)
        {
            if (checkTimer is not null)
                checkTimer.Stop();
            Close();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            if (qrCodeImage is not null)
                qrCodeImage.Dispose();
            base.OnFormClosed(e);
        }
    }
}
