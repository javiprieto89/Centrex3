using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Centrex.Models;

namespace Centrex
{

    static class ccClientes
    {

        // ************************************ FUNCIONES DE CUENTAS CORRIENTES DE CLIENTES **********************
        public static ccCliente info_ccCliente(int id_cc)
        {
            var tmp = new ccCliente();

            try
            {
                using (CentrexDbContext context = GetDbContext())
                {
                    var ccEntity = context.CcClienteEntity.FirstOrDefault(cc => cc.IdCc == id_cc);

                    if (ccEntity is not null)
                    {
                        tmp.IdCc = ccEntity.IdCc;
                        tmp.IdCliente = ccEntity.IdCliente;
                        tmp.IdMoneda = ccEntity.IdMoneda;
                        tmp.Nombre = ccEntity.Nombre;
                        tmp.Saldo = ccEntity.Saldo;
                        tmp.Activo = ccEntity.Activo;
                    }
                    else
                    {
                        tmp.Nombre = "error";
                    }
                }

                return tmp;
            }
            catch (Exception ex)
            {
                tmp.Nombre = "error";
                return tmp;
            }
        }

        public static bool addCCCliente(ccCliente cc)
        {
            try
            {
                using (CentrexDbContext context = GetDbContext())
                {
                    var ccEntity = new CcClienteEntity()
                    {
                        IdCliente = cc.IdCliente,
                        IdMoneda = cc.IdMoneda,
                        Nombre = cc.Nombre,
                        Saldo = cc.Saldo,
                        Activo = cc.Activo
                    };

                    context.CcClienteEntity.Add(ccEntity);
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Interaction.MsgBox(ex.Message);
                return false;
            }
        }

        public static bool updateCCCliente(ccCliente cc, bool borra = false)
        {
            try
            {
                using (CentrexDbContext context = GetDbContext())
                {
                    var ccEntity = context.CcClienteEntity.FirstOrDefault(c => c.IdCc == cc.IdCc);

                    if (ccEntity is not null)
                    {
                        if (borra == true)
                        {
                            ccEntity.Activo = false;
                        }
                        else
                        {
                             ccEntity.IdCliente = cc.IdCliente;
ccEntity.IdMoneda = cc.IdMoneda;
                             ccEntity.Nombre = cc.Nombre;
                            ccEntity.Saldo = cc.Saldo;
                            ccEntity.Activo = cc.Activo;
                        }

                        context.SaveChanges();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Interaction.MsgBox(ex.Message);
                return false;
            }
        }

        public static bool borrarccCliente(ccCliente cc)
        {
            try
            {
                using (CentrexDbContext context = GetDbContext())
                {
                    var ccEntity = context.CcClienteEntity.FirstOrDefault(c => c.IdCc == cc.IdCc);

                    if (ccEntity is not null)
                    {
                        context.CcClienteEntity.Remove(ccEntity);
                        context.SaveChanges();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Interaction.MsgBox(ex.Message.ToString());
                return false;
            }
        }

        public static void consultaCcCliente(ref DataGridView dataGrid, int id_cliente, int id_Cc, DateTime fecha_desde, DateTime fecha_hasta, int desde, ref int nRegs, ref int tPaginas, int pagina, ref TextBox txtnPage, bool traerTodo)
        {

            var datatable = new DataTable();
            DataGridViewColumn oldSortColumn = null;
            var oldSortDir = default(ListSortDirection);

            oldSortColumn = dataGrid.SortedColumn;
            if (dataGrid.SortedColumn is not null)
            {
                if (dataGrid.SortOrder == (int)System.Data.SqlClient.SortOrder.Ascending)
                {
                    oldSortDir = ListSortDirection.Ascending;
                }
                else
                {
                    oldSortDir = ListSortDirection.Descending;
                }
            }

            dataGrid.Columns.Clear();

            try
            {
                using (CentrexDbContext context = GetDbContext())
                {
                    var resultado = context.Procedures
                        .SP_consulta_CC_ClienteAsync(
                            id_cliente,
                            id_Cc,
                            fecha_desde.ToString("yyyy-MM-dd"),
                            fecha_hasta.ToString("yyyy-MM-dd"))
                        .GetAwaiter()
                        .GetResult() ?? new List<SP_consulta_CC_ClienteResult>();

                    if (resultado.Count > 0)
                    {
                        datatable = combos_ef.ToDataTable(resultado);
                    }
                    else
                    {
                        datatable.Rows.Clear();
                    }

                    if (!traerTodo)
                    {
                        nRegs = datatable.Rows.Count;
                        tPaginas = (int)Math.Round(Math.Ceiling(nRegs / (double)VariablesGlobales.itXPage));
                        txtnPage.Text = pagina + " / " + tPaginas;

                        // Paginar manualmente
                        var pagedTable = new DataTable();
                        pagedTable = datatable.Clone();
                        int endIndex = Math.Min(desde + VariablesGlobales.itXPage - 1, datatable.Rows.Count - 1);
                        for (int idx = desde, loopTo = endIndex; idx <= loopTo; idx++)
                        {
                            if (idx < datatable.Rows.Count)
                            {
                                pagedTable.ImportRow(datatable.Rows[idx]);
                            }
                        }
                        datatable = pagedTable;
                    }

                    dataGrid.DataSource = datatable;
                    dataGrid.RowsDefaultCellStyle.BackColor = Color.White;
                    dataGrid.AlternatingRowsDefaultCellStyle.BackColor = Color.AliceBlue;

                    int i = 0;
                    foreach (DataGridViewColumn columna in dataGrid.Columns)
                    {
                        dataGrid.Columns[columna.Name.ToString()].DisplayIndex = i;
                        i = i + 1;
                    }

                    dataGrid.Height = dataGrid.Height + 1;
                    dataGrid.Height = dataGrid.Height - 1;

                    if (dataGrid.Rows.Count > 0)
                    {
                        dataGrid.AutoSizeColumnsMode = (DataGridViewAutoSizeColumnsMode)DataGridViewAutoSizeColumnMode.AllCells;
                    }
                    else
                    {
                        dataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                    }

                    if (oldSortColumn is not null)
                    {
                        dataGrid.Sort(dataGrid.Columns[oldSortColumn.Name], oldSortDir);
                    }

                    dataGrid.Refresh();
                }
            }
            catch (Exception ex)
            {
                Interaction.MsgBox(ex.Message.ToString());
            }
        }

        public static string consultaTotalCcCliente(int id_cliente, DateTime fecha_desde, DateTime fecha_hasta)
        {
            try
            {
                using (CentrexDbContext context = GetDbContext())
                {
                    var importeTotal = (from trans in context.TransaccionEntity
  join tipoComp in context.TipoComprobanteEntity on trans.IdTipoComprobante equals tipoComp.IdTipoComprobante
     join comp in context.ComprobanteEntity on tipoComp.IdTipoComprobante equals comp.IdTipoComprobante
     where trans.Fecha >= DateOnly.FromDateTime(fecha_desde) && trans.Fecha <= DateOnly.FromDateTime(fecha_hasta) && trans.IdCliente == id_cliente && comp.Contabilizar == true
       select trans.Total).Distinct().Sum();

                    // Evitar NullReference (Sum puede devolver Nothing)
                    if (importeTotal is null)
                    {
                        return "0";
                    }
                    else
                    {
                        return importeTotal.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                Interaction.MsgBox(ex.Message.ToString());
                return "";
            }
        }
        // ************************************ FUNCIONES DE CUENTAS CORRIENTES DE CLIENTES **********************
    }
}
