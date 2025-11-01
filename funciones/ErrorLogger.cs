using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace Centrex.Funciones
{
    public static class ErrorLogger
    {
        public static void Log(Exception ex, string origen = "")
        {
            try
            {
                var trace = new StackTrace(ex, true);
                var frame = trace.GetFrame(0);
                var file = frame?.GetFileName() ?? "(archivo desconocido)";
                var line = frame?.GetFileLineNumber() ?? 0;
                var method = ex.TargetSite?.Name ?? "(método desconocido)";

                string msg =
                    $"🧨 Error: {ex.Message}\r\n" +
                    $"📄 Archivo: {System.IO.Path.GetFileName(file)}\r\n" +
                    $"🔢 Línea: {line}\r\n" +
                    $"🔧 Método: {method}\r\n" +
                    (!string.IsNullOrEmpty(origen) ? $"📍 Origen: {origen}\r\n" : "") +
                    $"🧩 StackTrace:\r\n{ex.StackTrace}";

                // Mostrar ventana de error con texto copiable
                ErrorDialog dlg = new ErrorDialog("Centrex - Error", msg);
                dlg.ShowDialog();

                // Guardar log diario
                string logDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs");
                if (!Directory.Exists(logDir)) Directory.CreateDirectory(logDir);
                string logPath = Path.Combine(logDir, $"log_{DateTime.Now:yyyyMMdd}.txt");
                File.AppendAllText(logPath, $"[{DateTime.Now:HH:mm:ss}] {msg}\r\n\r\n");
            }
            catch (Exception inner)
            {
                MessageBox.Show($"Error registrando excepción: {inner.Message}", "Centrex");
            }
        }
    }
}
