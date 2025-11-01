using System;
using System.Windows.Forms;

namespace Centrex.Funciones
{
    public static class ConversorFechas
    {
        /// <summary>
        /// Convierte una fecha a string (uso simple, un solo parámetro)
        /// Ejemplo: lbl_fechaCarga.Text = GetFecha(miVariable.FechaCarga);
        /// </summary>
        public static string GetFecha(object origen, string formato = "dd/MM/yyyy")
        {
            DateOnly fechaOrigen = ExtraerDateOnly(origen);
            return fechaOrigen == DateOnly.MinValue ? "" : fechaOrigen.ToString(formato);
        }

        /// <summary>
        /// Convierte fechas de forma inteligente detectando automáticamente el tipo de destino.
        /// Ejemplos:
        /// - GetFecha(dateTimePicker, textBox) -> convierte a string y lo asigna al textBox
        /// - GetFecha(textBox, dateTimePicker) -> convierte a DateTime y lo asigna al dateTimePicker
        /// - GetFecha(dateOnly, typeof(string)) -> retorna string
        /// - GetFecha(dateTimePicker, typeof(DateOnly)) -> retorna DateOnly
        /// </summary>
        public static dynamic GetFecha(object origen, object destino, string formato = "dd/MM/yyyy")
        {
            // Extraer el DateOnly del origen
            DateOnly fechaOrigen = ExtraerDateOnly(origen);

            // Detectar qué tipo de destino es y actuar en consecuencia
            switch (destino)
            {
                // ============================================
                // CONTROLES - Asignar directamente
                // ============================================
                case TextBox txt:
                    txt.Text = fechaOrigen == DateOnly.MinValue ? "" : fechaOrigen.ToString(formato);
                    return txt.Text;

                case Label lbl:
                    lbl.Text = fechaOrigen == DateOnly.MinValue ? "" : fechaOrigen.ToString(formato);
                    return lbl.Text;

                case DateTimePicker dtp:
                    dtp.Value = fechaOrigen == DateOnly.MinValue ? DateTime.Now : fechaOrigen.ToDateTime(TimeOnly.MinValue);
                    return dtp.Value;

                case MonthCalendar mc:
                    mc.SelectionStart = fechaOrigen == DateOnly.MinValue ? DateTime.Now : fechaOrigen.ToDateTime(TimeOnly.MinValue);
                    return mc.SelectionStart;

                // ============================================
                // TIPOS - Retornar el valor convertido
                // ============================================
                case Type tipo:
                    return ConvertirATipo(fechaOrigen, tipo, formato);

                default:
                    throw new ArgumentException(
                        $"Tipo de destino no soportado: {destino?.GetType().Name}. " +
                        $"Usa controles (TextBox, Label, DateTimePicker, MonthCalendar) o tipos (typeof(string), typeof(DateTime), typeof(DateOnly))");
            }
        }

        /// <summary>
        /// Convierte a un tipo específico cuando se usa typeof()
        /// </summary>
        private static dynamic ConvertirATipo(DateOnly fecha, Type tipo, string formato)
        {
            if (tipo == typeof(string))
                return fecha == DateOnly.MinValue ? "" : fecha.ToString(formato);

            if (tipo == typeof(DateTime))
                return fecha == DateOnly.MinValue ? DateTime.Now : fecha.ToDateTime(TimeOnly.MinValue);

            if (tipo == typeof(DateTime?))
                return fecha == DateOnly.MinValue ? (DateTime?)null : fecha.ToDateTime(TimeOnly.MinValue);

            if (tipo == typeof(DateOnly))
                return fecha;

            if (tipo == typeof(DateOnly?))
                return fecha == DateOnly.MinValue ? (DateOnly?)null : fecha;

            throw new ArgumentException($"Tipo no soportado: {tipo.Name}");
        }

        /// <summary>
        /// Extrae un DateOnly de cualquier origen válido
        /// </summary>
        private static DateOnly ExtraerDateOnly(object origen)
        {
            return origen switch
            {
                null => DateOnly.MinValue,
                DateOnly dateOnly => dateOnly,
                DateTime dateTime => DateOnly.FromDateTime(dateTime),
                DateTimePicker dtp => DateOnly.FromDateTime(dtp.Value),
                MonthCalendar mc => DateOnly.FromDateTime(mc.SelectionStart),
                TextBox txt => ParsearFecha(txt.Text),
                Label lbl => ParsearFecha(lbl.Text),
                string str => ParsearFecha(str),
                _ => throw new ArgumentException($"Tipo de origen no soportado: {origen.GetType().Name}")
            };
        }

        /// <summary>
        /// Intenta parsear un string a DateOnly con múltiples formatos
        /// </summary>
        private static DateOnly ParsearFecha(string valor)
        {
            if (string.IsNullOrWhiteSpace(valor))
                return DateOnly.MinValue;

            if (DateOnly.TryParse(valor, out var dateOnly))
                return dateOnly;

            if (DateTime.TryParse(valor, out var dateTime))
                return DateOnly.FromDateTime(dateTime);

            string[] formatos = new[]
            {
            "dd/MM/yyyy", "dd-MM-yyyy", "yyyy-MM-dd",
            "yyyy/MM/dd", "dd/MM/yy", "dd-MM-yy",
            "d/M/yyyy", "d-M-yyyy"
        };

            foreach (var formato in formatos)
            {
                if (DateTime.TryParseExact(valor, formato,
                    System.Globalization.CultureInfo.InvariantCulture,
                    System.Globalization.DateTimeStyles.None,
                    out dateTime))
                {
                    return DateOnly.FromDateTime(dateTime);
                }
            }

            return DateOnly.MinValue;
        }
    }
}
