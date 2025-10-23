using System;
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
                    var ccEntity = context.CcClientes.FirstOrDefault(cc => cc.IdCc == id_cc);

                    if (ccEntity is not null)
                    {
                        tmp.id_cc = ccEntity.IdCc.ToString();
                        tmp.id_cliente = ccEntity.IdCliente.ToString();
                        tmp.id_moneda = ccEntity.IdMoneda.ToString();
                        tmp.nombre = ccEntity.nombre;
                        tmp.saldo = Conversions.ToDouble(ccEntity.saldo.ToString());
                        tmp.activo = ccEntity.activo;
                    }
                    else
                    {
                        tmp.nombre = "error";
                    }
                }

                return tmp;
            }
            catch (Exception ex)
            {
                tmp.nombre = "error";
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
                        IdCliente = cc.id_cliente,
                        IdMoneda = cc.id_moneda,
                        nombre = cc.nombre,
                        saldo = (decimal)cc.saldo,
                        activo = cc.activo
                    };

                    context.CcClientes.Add(ccEntity);
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
                    var ccEntity = context.CcClientes.FirstOrDefault(c => c.IdCc == cc.id_cc);

                    if (ccEntity is not null)
                    {
                        if (borra == true)
                        {
                            ccEntity.activo = false;
                        }
                        else
                        {
                            ccEntity.IdCliente = cc.id_cliente;
                            ccEntity.IdMoneda = cc.id_moneda;
                            ccEntity.nombre = cc.nombre;
                            ccEntity.saldo = (decimal)cc.saldo;
                            ccEntity.activo = cc.activo;
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
                    var ccEntity = context.CcClientes.FirstOrDefault(c => c.IdCc == cc.id_cc);

                    if (ccEntity is not null)
                    {
                        context.CcClientes.Remove(ccEntity);
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
                    // Usar stored procedure
                    var resultado = context.Database.SqlQuery<object>("EXEC SP_consulta_CC_Cliente @id_cliente, @id_cc, @fecha_desde, @fecha_hasta", new SqlParameter("@id_cliente", id_cliente), new SqlParameter("@id_cc", id_Cc), new SqlParameter("@fecha_desde", fecha_desde), new SqlParameter("@fecha_hasta", fecha_hasta)).ToList();

                    // Convertir a DataTable
                    if (resultado.Count > 0)
                    {
                        System.Reflection.PropertyInfo[] props = resultado[0].GetType().GetProperties();
                        foreach (var prop in props)
                            datatable.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);

                        foreach (var item in resultado)
                        {
                            var row = datatable.NewRow();
                            foreach (var prop in props)
                                row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                            datatable.Rows.Add(row);
                        }
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
                    var importeTotal = (from trans in context.Transacciones
                                        join tipoComp in context.TiposComprobantes on trans.IdTipoComprobante equals tipoComp.IdTipoComprobante
                                        join comp in context.Comprobantes on tipoComp.IdTipoComprobante equals comp.IdTipoComprobante
                                        where trans.fecha >= fecha_desde && trans.fecha <= fecha_hasta && trans.IdCliente == id_cliente && comp.contabilizar == true
                                        select trans.total).Distinct().Sum();

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
