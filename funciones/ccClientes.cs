using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Centrex.Funciones
{
    static class ccClientes
    {
        // ============================
        // INFO CUENTA CORRIENTE CLIENTE
        // ============================
        public static CcClienteEntity info_ccCliente(int id_cc)
        {
            try
            {
                using var context = new CentrexDbContext();
                var ccEntity = context.CcClienteEntity
                    .AsNoTracking()
                    .FirstOrDefault(cc => cc.IdCc == id_cc);

                return ccEntity ?? new CcClienteEntity { Nombre = "error" };
            }
            catch (Exception ex)
            {
                Interaction.MsgBox(ex.Message, MsgBoxStyle.Critical, "Centrex");
                return new CcClienteEntity { Nombre = "error" };
            }
        }

        // ============================
        // AGREGAR CUENTA CORRIENTE CLIENTE
        // ============================
        public static bool addCCCliente(CcClienteEntity cc)
        {
            try
            {
                using var context = new CentrexDbContext();
                context.CcClienteEntity.Add(cc);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Interaction.MsgBox(ex.Message, MsgBoxStyle.Critical, "Centrex");
                return false;
            }
        }

        // ============================
        // ACTUALIZAR / BORRAR LÓGICAMENTE
        // ============================
        public static bool updateCCCliente(CcClienteEntity cc, bool borra = false)
        {
            try
            {
                using var context = new CentrexDbContext();
                var entity = context.CcClienteEntity.FirstOrDefault(c => c.IdCc == cc.IdCc);

                if (entity == null)
                    return false;

                if (borra)
                {
                    entity.Activo = false;
                }
                else
                {
                    entity.IdCliente = cc.IdCliente;
                    entity.IdMoneda = cc.IdMoneda;
                    entity.Nombre = cc.Nombre;
                    entity.Saldo = cc.Saldo;
                    entity.Activo = cc.Activo;
                }

                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Interaction.MsgBox(ex.Message, MsgBoxStyle.Critical, "Centrex");
                return false;
            }
        }

        // ============================
        // BORRAR DEFINITIVO
        // ============================
        public static bool borrarccCliente(CcClienteEntity cc)
        {
            try
            {
                using var context = new CentrexDbContext();
                var entity = context.CcClienteEntity.FirstOrDefault(c => c.IdCc == cc.IdCc);
                if (entity == null) return false;

                context.CcClienteEntity.Remove(entity);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Interaction.MsgBox(ex.Message, MsgBoxStyle.Critical, "Centrex");
                return false;
            }
        }

        // ============================
        // CONSULTA GENERAL DE CC CLIENTE (usa infraestructura genérica)
        // ============================
        public static void consultaCcCliente(
      ref DataGridView dataGrid,
      int id_cliente,
      int id_Cc,
      DateTime fecha_desde,
      DateTime fecha_hasta,
      int desde,
      ref int nRegs,
      ref int tPaginas,
      int pagina,
      ref TextBox txtnPage,
      bool traerTodo)
        {
            try
            {
                using var ctx = new CentrexDbContext();

                // =======================
                // Tipos de comprobantes (según AFIP)
                // =======================
                var tiposDebito = new int[] { 1, 6, 11, 51, 201, 206, 211, 1006, 2, 7, 12, 52, 202, 207, 212, 1007, 1002, 1003, 1004, 1005, 1010 };
                var tiposCredito = new int[] { 3, 8, 13, 53, 203, 208, 213, 1008, 4, 9, 15, 54, 1009 };

                // =======================
                // Consulta base (reemplaza el WITH del SP)
                // =======================
                var movimientos = ctx.TransaccionEntity
                    .Include(t => t.IdTipoComprobanteNavigation)
                    .Include(t => t.IdClienteNavigation)                    
                    .Where(t =>
                        t.IdCliente == id_cliente &&
                        t.IdCc == id_Cc &&
                        t.Fecha >= DateOnly.FromDateTime(fecha_desde) &&
                        t.Fecha <= DateOnly.FromDateTime(fecha_hasta) &&
                        t.IdTipoComprobanteNavigation.Contabilizar == true)
                    .OrderBy(t => t.IdTransaccion)
                    .Select(t => new
                    {
                        t.IdTransaccion,
                        t.IdCliente,
                        t.IdCc,
                        t.NumeroComprobante,
                        t.PuntoVenta,
                        t.IdTipoComprobante,
                        t.IdTipoComprobanteNavigation.NombreAbreviado,
                        t.Fecha,
                        t.Total,
                        Debito = tiposDebito.Contains(t.IdTipoComprobante.Value) ? t.Total ?? 0 : 0,
                        Credito = tiposCredito.Contains(t.IdTipoComprobante.Value) ? (t.Total ?? 0) * -1 : 0,
                        Signo = tiposDebito.Contains(t.IdTipoComprobante.Value)
                                    ? 1
                                    : tiposCredito.Contains(t.IdTipoComprobante.Value)
                                        ? -1
                                        : 0
                    })
                    .AsEnumerable() // pasamos a memoria para poder calcular saldo incremental
                    .ToList();

                // =======================
                // Calcular saldo acumulado manualmente
                // =======================
                decimal saldo = 0;
                var resultado = movimientos.Select(m =>
                {
                    saldo += (m.Total ?? 0) * m.Signo;
                    return new
                    {
                        ID = m.IdTransaccion,
                        Fecha = m.Fecha,
                        Comprobante = generales.CalculoComprobanteLocal(m.NombreAbreviado, m.PuntoVenta ?? 0, m.NumeroComprobante ?? 0),
                        Débito = m.Debito,
                        Crédito = m.Credito,
                        Saldo = saldo
                    };
                }).ToList();

                // =======================
                // Cargar el DataGrid con la infraestructura unificada
                // =======================
                var result = new DataGridQueryResult
                {
                    EsMaterializada = true,
                    DataMaterializada = resultado,
                    GridName = "ccClientes"
                };

                LoadDataGridDynamic.LoadDataGridWithEntityAsync(dataGrid, result)
                    .GetAwaiter()
                    .GetResult();

                // =======================
                // Calcular paginación
                // =======================
                nRegs = dataGrid.Rows.Count;
                tPaginas = (int)Math.Ceiling(nRegs / (double)VariablesGlobales.itXPage);
                txtnPage.Text = $"{pagina} / {tPaginas}";
            }
            catch (Exception ex)
            {
                Interaction.MsgBox($"Error en consultaCcCliente: {ex.Message}", MsgBoxStyle.Critical, "Centrex");
            }
        }

        // ============================
        // TOTAL CUENTA CORRIENTE
        // ============================
        //
        public static string consultaTotalCcCliente(int id_cliente, DateTime fecha_desde, DateTime fecha_hasta)
        {
            try
            {
                using var ctx = new CentrexDbContext();

                var total = (
                    from t in ctx.TransaccionEntity
                    join tc in ctx.TipoComprobanteEntity on t.IdTipoComprobante equals tc.IdTipoComprobante
                    join c in ctx.ComprobanteEntity on tc.IdTipoComprobante equals c.IdTipoComprobante
                    where
                        t.IdCliente == id_cliente &&
                        t.Fecha >= DateOnly.FromDateTime(fecha_desde) &&
                        t.Fecha <= DateOnly.FromDateTime(fecha_hasta) &&
                        c.Contabilizar == true
                    select t.Total
                ).Sum() ?? 0m;

                return total.ToString("N2");
            }
            catch (Exception ex)
            {
                Interaction.MsgBox($"Error en consultaTotalCcCliente: {ex.Message}", MsgBoxStyle.Critical, "Centrex");
                return "0";
            }
        }


    }
}
