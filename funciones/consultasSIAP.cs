using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualBasic;
using Centrex.Models;

public class consultaSIAP
{
    public string id_consulta { get; set; } = "";
    public string nombre { get; set; } = "";
    public string consulta { get; set; } = "";
    public bool excel { get; set; }
    public bool txt { get; set; }
    public bool activo { get; set; }
}

public static class consultasSIAP
{
    // 🔹 Diccionario local con todas las consultas lógicas SIAP
    private static readonly Dictionary<int, consultaSIAP> _consultas = new()
    {
        {
            1,
            new consultaSIAP
            {
                id_consulta = "1",
                nombre = "Listado de Ventas",
                consulta = "VENTAS_RESUMEN", // clave lógica
                excel = true,
                txt = false,
                activo = true
            }
        },
        {
            2,
            new consultaSIAP
            {
                id_consulta = "2",
                nombre = "Listado de Compras",
                consulta = "COMPRAS_RESUMEN",
                excel = true,
                txt = false,
                activo = true
            }
        },
        {
            3,
            new consultaSIAP
            {
                id_consulta = "3",
                nombre = "Stock Actual",
                consulta = "STOCK_ACTUAL",
                excel = false,
                txt = true,
                activo = true
            }
        }
    };

    // 🔹 Devuelve los metadatos de la consulta
    public static consultaSIAP info_consultaSIAP(int id_consulta)
    {
        try
        {
            if (_consultas.TryGetValue(id_consulta, out var resultado))
                return resultado;

            Interaction.MsgBox($"No existe una consulta SIAP con ID {id_consulta}.", MsgBoxStyle.Exclamation);
            return new consultaSIAP { nombre = "error" };
        }
        catch (Exception ex)
        {
            Interaction.MsgBox("Error en info_consultaSIAP: " + ex.Message, MsgBoxStyle.Critical);
            return new consultaSIAP { nombre = "error" };
        }
    }

    public static List<consultaSIAP> ObtenerConsultasActivas()
    {
        return _consultas.Values
            .Where(c => c.activo)
            .OrderBy(c => c.nombre)
            .ToList();
    }

    // 🔹 Ejecuta la consulta lógica y devuelve el resultado como texto (para exportar)
    public static string ejecutarConsultaSIAP(string claveConsulta, DateTime? fechaDesde = null, DateTime? fechaHasta = null)
    {
        try
        {
            using var context = new CentrexDbContext();
            DateOnly? fechaDesdeOnly = fechaDesde.HasValue ? DateOnly.FromDateTime(fechaDesde.Value.Date) : null;
            DateOnly? fechaHastaOnly = fechaHasta.HasValue ? DateOnly.FromDateTime(fechaHasta.Value.Date) : null;

            switch (claveConsulta)
            {
                // 🔹 Ventas (usa Comprobantes emitidos — no tienen FechaCarga, así que solo listamos)
                case "VENTAS_RESUMEN":
                    var ventas = context.ComprobanteEntity
                        .Where(c => c.Activo)
                        .Select(c =>
                            $"Comprobante: {c.Comprobante} Nº {c.PuntoVenta:D4}-{c.NumeroComprobante:D8} | " +
                            $"Tipo: {c.IdTipoComprobante} | Activo: {c.Activo}")
                        .ToList();

                    return string.Join(Environment.NewLine, ventas);


                // 🔹 Compras (usa ComprobanteCompraEntity → sí tiene FechaCarga)
                case "COMPRAS_RESUMEN":
                    var compras = context.ComprobanteCompraEntity
                        .Where(c => !fechaDesdeOnly.HasValue || c.FechaCarga >= fechaDesdeOnly.Value)
                        .Where(c => !fechaHastaOnly.HasValue || c.FechaCarga <= fechaHastaOnly.Value)
                        .Select(c =>
                            $"Compra #{c.IdComprobanteCompra} | Fecha: {c.FechaCarga:dd/MM/yyyy} | " +
                            $"Proveedor: {c.IdProveedor} | Total: {c.Total:C}")
                        .ToList();

                    return string.Join(Environment.NewLine, compras);


                // 🔹 Stock (usa ItemEntity)
                case "STOCK_ACTUAL":
                    var stock = context.ItemEntity
                        .Where(i => i.Activo)
                        .Select(i =>
                            $"{i.Item} | Cantidad: {i.Cantidad} | Costo: {i.Costo:C} | Precio Lista: {i.PrecioLista:C}")
                        .ToList();

                    return string.Join(Environment.NewLine, stock);


                default:
                    return "Consulta no implementada.";
            }
        }
        catch (Exception ex)
        {
            Interaction.MsgBox("Error ejecutando consulta SIAP: " + ex.Message, MsgBoxStyle.Critical);
            return "ERROR";
        }
    }

}

