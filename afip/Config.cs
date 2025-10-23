using System;

namespace Centrex.Afip
{
    public enum AfipMode
    {
        HOMO,
        PROD
    }

    /// <summary>
    /// Configuración centralizada de AFIP
    /// Ahora depende completamente de InicialesFE de factura_electronica.vb
    /// </summary>
    public sealed class AfipConfig
    {
        private AfipConfig()
        {
        }

        // ============================================
        // ENDPOINTS DE SERVICIOS WEB
        // ============================================
        public const string WSAA_HOMO_URL = "https://wsaahomo.afip.gov.ar/ws/services/LoginCms?wsdl";
        public const string WSAA_PROD_URL = "https://wsaa.afip.gov.ar/ws/services/LoginCms?wsdl";
        public const string WSFE_HOMO_URL = "https://wswhomo.afip.gov.ar/wsfev1/service.asmx";
        public const string WSFE_PROD_URL = "https://servicios1.afip.gov.ar/wsfev1/service.asmx";

        // ============================================
        // CONFIGURACIÓN DINÁMICA (Establecida por InicialesFE)
        // ============================================
        public static string DynamicCertPath { get; set; } = null;
        public static string DynamicCertPassword { get; set; } = null;
        public static long? DynamicCuitEmisor { get; set; } = default;

        // ============================================
        // MODO OPERACIÓN
        // ============================================
        private static AfipMode _mode = AfipMode.HOMO;
        public static AfipMode Mode
        {
            get
            {
                return _mode;
            }
            set
            {
                _mode = value;
            }
        }

        // ============================================
        // OBTENER URLs SEGÚN MODO
        // ============================================
        public static string GetWsaaUrl(AfipMode mode)
        {
            return mode == AfipMode.HOMO ? WSAA_HOMO_URL : WSAA_PROD_URL;
        }

        public static string GetWsfeUrl(AfipMode mode)
        {
            return mode == AfipMode.HOMO ? WSFE_HOMO_URL : WSFE_PROD_URL;
        }

        // ============================================
        // CERTIFICADO (desde InicialesFE)
        // ============================================
        public static string GetCertPath(AfipMode mode)
        {
            if (string.IsNullOrWhiteSpace(DynamicCertPath))
            {
                throw new InvalidOperationException("Debe llamar a InicialesFE() antes de usar los servicios AFIP");
            }
            return DynamicCertPath;
        }

        public static string GetCertPassword(AfipMode mode)
        {
            if (string.IsNullOrWhiteSpace(DynamicCertPassword))
            {
                throw new InvalidOperationException("Debe llamar a InicialesFE() antes de usar los servicios AFIP");
            }
            return DynamicCertPassword;
        }

        public static string CertPath
        {
            get
            {
                return GetCertPath(Mode);
            }
        }

        public static string CertPassword
        {
            get
            {
                return GetCertPassword(Mode);
            }
        }

        // ============================================
        // CUIT EMISOR (desde InicialesFE)
        // ============================================
        public static long GetCuitEmisor()
        {
            if (!DynamicCuitEmisor.HasValue || DynamicCuitEmisor.Value <= 0L)
            {
                throw new InvalidOperationException("Debe llamar a InicialesFE() antes de usar los servicios AFIP");
            }
            return DynamicCuitEmisor.Value;
        }

        // ============================================
        // TIMEOUT
        // ============================================
        public static int TimeoutSeconds
        {
            get
            {
                return 60;
            }
        }

        // ============================================
        // UTILIDAD
        // ============================================
        public static void ClearDynamicConfig()
        {
            DynamicCertPath = null;
            DynamicCertPassword = null;
            DynamicCuitEmisor = default;
        }

        public static bool IsConfigured()
        {
            return !string.IsNullOrWhiteSpace(DynamicCertPath) && !string.IsNullOrWhiteSpace(DynamicCertPassword) && DynamicCuitEmisor.HasValue && DynamicCuitEmisor.Value > 0L;
        }

        public static string GetConfigSummary(AfipMode mode)
        {
            try
            {
                var summary = new System.Text.StringBuilder();
                summary.AppendLine("=== CONFIGURACIÓN AFIP ===");
                summary.AppendLine($"Modo: {mode}");
                summary.AppendLine($"WSAA URL: {GetWsaaUrl(mode)}");
                summary.AppendLine($"WSFE URL: {GetWsfeUrl(mode)}");

                if (IsConfigured())
                {
                    summary.AppendLine($"Certificado: {DynamicCertPath}");
                    summary.AppendLine($"Certificado existe: {System.IO.File.Exists(DynamicCertPath)}");
                    summary.AppendLine($"CUIT Emisor: {DynamicCuitEmisor.Value}");
                    summary.AppendLine($"Password configurado: Sí");
                    summary.AppendLine("✓ Configuración establecida por InicialesFE()");
                }
                else
                {
                    summary.AppendLine("⚠️ NO CONFIGURADO - Debe llamar a InicialesFE() primero");
                }
                return summary.ToString();
            }
            catch (Exception ex)
            {
                return "Error al obtener configuración: " + ex.Message;
            }
        }

        public static (bool isValid, string errorMessage) ValidateConfig(AfipMode mode)
        {
            try
            {
                if (!IsConfigured())
                {
                    return (false, "No se ha llamado a InicialesFE(). Configure certificado, password y CUIT primero.");
                }
                if (!System.IO.File.Exists(DynamicCertPath))
                {
                    return (false, $"El certificado no existe en: {DynamicCertPath}");
                }
                if (DynamicCuitEmisor.Value <= 0L)
                {
                    return (false, "El CUIT emisor es inválido");
                }
                return (true, "Configuración válida");
            }
            catch (Exception ex)
            {
                return (false, $"Error al validar configuración: {ex.Message}");
            }
        }
    }
}
