using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Centrex.Afip;
using Centrex.Afip.Models;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace Centrex
{


    public partial class frm_pruebas_afip
    {
        public frm_pruebas_afip()
        {
            InitializeComponent();
        }
        private void frm_pruebas_afip_Load(object sender, EventArgs e)
        {
            // Cargar configuración inicial
            CargarConfiguracion();
        }

        private void CargarConfiguracion()
        {
            // Cargar valores por defecto
            txt_cuit.Text = VariablesGlobales.cuit_emisor_default;

            // Cargar opciones de modo
            cmb_mode.Items.Clear();
            cmb_mode.Items.Add("HOMO");
            cmb_mode.Items.Add("PROD");
            cmb_mode.SelectedIndex = 0; // Por defecto HOMO

            // Cargar certificados disponibles
            CargarCertificados();
        }

        private void CargarCertificados()
        {
            cmb_certificados.Items.Clear();
            string certificadosPath = Application.StartupPath + @"\Certificados\";

            if (Directory.Exists(certificadosPath))
            {
                string[] archivos = Directory.GetFiles(certificadosPath, "*.pfx");
                foreach (string archivo in archivos)
                    cmb_certificados.Items.Add(Path.GetFileName(archivo));
            }

            if (cmb_certificados.Items.Count > 0)
            {
                cmb_certificados.SelectedIndex = 0;
            }
        }

        private void btn_probar_conexion_Click(object sender, EventArgs e)
        {
            try
            {
                // Validar datos
                if (string.IsNullOrWhiteSpace(txt_cuit.Text))
                {
                    Interaction.MsgBox("Debe ingresar el CUIT emisor", Constants.vbExclamation, "Centrex");
                    return;
                }

                if (cmb_certificados.SelectedItem is null)
                {
                    Interaction.MsgBox("Debe seleccionar un certificado", Constants.vbExclamation, "Centrex");
                    return;
                }

                if (string.IsNullOrWhiteSpace(txt_password.Text))
                {
                    Interaction.MsgBox("Debe ingresar la contraseña del certificado", Constants.vbExclamation, "Centrex");
                    return;
                }

                // Configurar valores dinámicos
                string certificadoPath = Application.StartupPath + @"\Certificados\" + cmb_certificados.SelectedItem.ToString();
                AfipConfig.DynamicCertPath = certificadoPath;
                AfipConfig.DynamicCertPassword = txt_password.Text;
                AfipConfig.DynamicCuitEmisor = Conversions.ToLong(txt_cuit.Text);

                // Determinar modo
                bool esTest = cmb_mode.SelectedItem.ToString().ToUpper() == "HOMO";

                // Probar conexión
                btn_probar_conexion.Enabled = false;
                txt_resultado.Text = "Probando conexión...";
                Application.DoEvents();
            }

            // Dim resultado As String = ProbarConexionAFIP(esTest)
            // txt_resultado.Text = resultado

            catch (Exception ex)
            {
                txt_resultado.Text = "ERROR: " + ex.Message + Constants.vbCrLf + "Tipo: " + ex.GetType().Name;
            }
            finally
            {
                btn_probar_conexion.Enabled = true;
            }
        }

        private void btn_probar_ultimo_comprobante_Click(object sender, EventArgs e)
        {
            try
            {
                // Validar datos
                if (string.IsNullOrWhiteSpace(txt_punto_venta.Text))
                {
                    Interaction.MsgBox("Debe ingresar el punto de venta", Constants.vbExclamation, "Centrex");
                    return;
                }

                if (string.IsNullOrWhiteSpace(txt_tipo_comprobante.Text))
                {
                    Interaction.MsgBox("Debe ingresar el tipo de comprobante", Constants.vbExclamation, "Centrex");
                    return;
                }

                // Configurar valores dinámicos (usar los mismos del botón anterior)
                if (string.IsNullOrWhiteSpace(AfipConfig.DynamicCertPath))
                {
                    Interaction.MsgBox("Primero debe probar la conexión básica", Constants.vbExclamation, "Centrex");
                    return;
                }

                // Determinar modo
                bool esTest = cmb_mode.SelectedItem.ToString().ToUpper() == "HOMO";

                // Probar último comprobante
                btn_probar_ultimo_comprobante.Enabled = false;
                txt_resultado.Text = "Consultando último comprobante...";
                Application.DoEvents();

                var afipMode = esTest ? AfipMode.HOMO : AfipMode.PROD;
                var wsfe = WSFEv1.CreateWithTa(afipMode);
                var ultimoCmp = wsfe.FECompUltimoAutorizado(Conversions.ToInteger(txt_punto_venta.Text), Conversions.ToInteger(txt_tipo_comprobante.Text));

                txt_resultado.Text = "ÉXITO: Último comprobante autorizado: " + ultimoCmp.ToString() + Constants.vbCrLf + "Punto de venta: " + txt_punto_venta.Text + Constants.vbCrLf + "Tipo de comprobante: " + txt_tipo_comprobante.Text;
            }

            catch (Exception ex)
            {
                txt_resultado.Text = "ERROR: " + ex.Message + Constants.vbCrLf + "Tipo: " + ex.GetType().Name;
            }
            finally
            {
                btn_probar_ultimo_comprobante.Enabled = true;
            }
        }

        private void btn_probar_parametros_Click(object sender, EventArgs e)
        {
            try
            {
                // Configurar valores dinámicos (usar los mismos del botón anterior)
                if (string.IsNullOrWhiteSpace(AfipConfig.DynamicCertPath))
                {
                    Interaction.MsgBox("Primero debe probar la conexión básica", Constants.vbExclamation, "Centrex");
                    return;
                }

                // Determinar modo
                bool esTest = cmb_mode.SelectedItem.ToString().ToUpper() == "HOMO";

                // Probar parámetros
                btn_probar_parametros.Enabled = false;
                txt_resultado.Text = "Consultando parámetros AFIP...";
                Application.DoEvents();

                var afipMode = esTest ? AfipMode.HOMO : AfipMode.PROD;
                var wsfe = WSFEv1.CreateWithTa(afipMode);

                var resultado = new StringBuilder();
                resultado.AppendLine("PARÁMETROS AFIP:");
                resultado.AppendLine("=================");

                // Probar diferentes parámetros
                try
                {
                    var tiposIva = wsfe.FEParamGetTiposIva();
                    resultado.AppendLine("✓ Tipos de IVA: OK");
                }
                catch (Exception ex)
                {
                    resultado.AppendLine("✗ Tipos de IVA: " + ex.Message);
                }

                try
                {
                    var tiposTributos = wsfe.FEParamGetTiposTributos();
                    resultado.AppendLine("✓ Tipos de Tributos: OK");
                }
                catch (Exception ex)
                {
                    resultado.AppendLine("✗ Tipos de Tributos: " + ex.Message);
                }

                try
                {
                    var monedas = wsfe.FEParamGetTiposMonedas();
                    resultado.AppendLine("✓ Monedas: OK");
                }
                catch (Exception ex)
                {
                    resultado.AppendLine("✗ Monedas: " + ex.Message);
                }

                try
                {
                    var ptosVenta = wsfe.FEParamGetPtosVenta();
                    resultado.AppendLine("✓ Puntos de Venta: OK");
                }
                catch (Exception ex)
                {
                    resultado.AppendLine("✗ Puntos de Venta: " + ex.Message);
                }

                try
                {
                    var opcionales = wsfe.FEParamGetTiposOpcional();
                    resultado.AppendLine("✓ Tipos Opcionales: OK");
                }
                catch (Exception ex)
                {
                    resultado.AppendLine("✗ Tipos Opcionales: " + ex.Message);
                }

                txt_resultado.Text = resultado.ToString();
            }

            catch (Exception ex)
            {
                txt_resultado.Text = "ERROR: " + ex.Message + Constants.vbCrLf + "Tipo: " + ex.GetType().Name;
            }
            finally
            {
                btn_probar_parametros.Enabled = true;
            }
        }

        private void btn_limpiar_Click(object sender, EventArgs e)
        {
            txt_resultado.Text = "";
        }

        private void btn_cerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Btn_obtener_puntos_de_venta_Click(object sender, EventArgs e)
        {
            var resultado = new StringBuilder();

            try
            {
                // Validar que ya se haya probado la conexión
                if (string.IsNullOrWhiteSpace(AfipConfig.DynamicCertPath))
                {
                    Interaction.MsgBox("Primero debe probar la conexión básica", Constants.vbExclamation, "Centrex");
                    return;
                }

                // Determinar modo (HOMO o PROD)
                bool esTest = cmb_mode.SelectedItem.ToString().ToUpper() == "HOMO";

                // Mostrar estado
                btn_probar_parametros.Enabled = false;
                txt_resultado.Text = "Consultando puntos de venta AFIP...";
                Application.DoEvents();

                // Crear cliente WSFEv1 con el TA actual
                var afipMode = esTest ? AfipMode.HOMO : AfipMode.PROD;
                var wsfe = WSFEv1.CreateWithTa(afipMode);

                // --- Llamada al servicio ---
                try
                {
                    List<PtoVentaInfo> ptosVenta = wsfe.FEParamGetPtosVenta();

                    if (ptosVenta is null || ptosVenta.Count == 0)
                    {
                        resultado.AppendLine("✗ No se encontraron puntos de venta habilitados.");
                    }
                    else
                    {
                        resultado.AppendLine("✓ Puntos de venta habilitados:");

                        foreach (var pto in ptosVenta)
                            resultado.AppendLine($"  • Nro: {pto.Nro} | Tipo: {pto.EmisionTipo} | Bloqueado: {pto.Bloqueado} | Baja: {pto.FchBaja}");

                        resultado.AppendLine();
                        resultado.AppendLine($"Total: {ptosVenta.Count} puntos de venta encontrados.");
                    }
                }

                catch (Exception ex)
                {
                    resultado.AppendLine("✗ Error al consultar puntos de venta:");
                    resultado.AppendLine(ex.Message);
                }
            }

            catch (Exception ex)
            {
                resultado.AppendLine("✗ Error general:");
                resultado.AppendLine(ex.Message);
            }
            finally
            {
                // Mostrar el resultado en el TextBox
                txt_resultado.Text = resultado.ToString();
                btn_probar_parametros.Enabled = true;
            }
        }


    }
}
