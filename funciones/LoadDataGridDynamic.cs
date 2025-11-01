using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.Versioning;
using Microsoft.EntityFrameworkCore;

namespace Centrex.Funciones
{
    [SupportedOSPlatform("windows6.1")]
    public static class LoadDataGridDynamic
    {
        // Clase auxiliar para guardar configuración y aplicarla al finalizar el DataBinding
        private class GridConfig
        {
            public List<string> ColumnasOcultar { get; set; } = new();
            public Dictionary<string, Color> ColoresColumnas { get; set; } = new();
            public bool TieneCheckbox { get; set; }
            public string NombreColumnCheckbox { get; set; } = "Seleccionar";
            public int PosicionColumnCheckbox { get; set; }
            public bool Depuracion { get; set; }
        }

        public static async Task LoadDataGridWithEntityAsync(
            DataGridView grid,
            DataGridQueryResult queryResult,
            bool depuracion = false)
        {
            try
            {
                if (queryResult?.Query is null)
                {
                    MessageBox.Show("No hay datos para mostrar.", "Centrex", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                object data;

                if (queryResult.EsMaterializada && queryResult.DataMaterializada is not null)
                {
                    data = queryResult.DataMaterializada;
                }
                else
                {
                    var queryable = queryResult.Query;
                    var listMethod = typeof(Microsoft.EntityFrameworkCore.EntityFrameworkQueryableExtensions)
                        .GetMethod("ToListAsync")
                        ?.MakeGenericMethod(queryable.ElementType)
                        ?? throw new InvalidOperationException("No se pudo materializar la consulta");

                    var task = (Task)listMethod.Invoke(null, new object[] { queryable, default(System.Threading.CancellationToken) });
                    await task.ConfigureAwait(false);
                    var resultProperty = task.GetType().GetProperty("Result");
                    data = resultProperty?.GetValue(task);
                }

                var dt = ConvertToDataTable(data);

                // Agregar columna "Depositado" si corresponde (caso cheques)
                if (dt.Columns.Contains("CuentaBancariaNombre") && dt.Columns.Contains("BancoCuentaNombre"))
                {
                    dt.Columns.Add("Depositado", typeof(string));
                    foreach (DataRow row in dt.Rows)
                    {
                        var cuentaNombre = row["CuentaBancariaNombre"]?.ToString();
                        var bancoNombre = row["BancoCuentaNombre"]?.ToString();
                        row["Depositado"] = string.IsNullOrEmpty(cuentaNombre) ? "No" : $"Si, en: {cuentaNombre} - {bancoNombre}";
                    }
                }

                // Limpiar y cargar DataGrid
                grid.DataSource = null;
                grid.Columns.Clear();
                grid.Rows.Clear();
                grid.Refresh();

                grid.SuspendLayout();
                grid.DataSource = dt;
                grid.ResumeLayout();

                // Guardar configuración para aplicar al finalizar el DataBinding
                grid.Tag = new GridConfig
                {
                    ColumnasOcultar = queryResult.ColumnasOcultar,
                    ColoresColumnas = queryResult.ColoresColumnas,
                    TieneCheckbox = queryResult.TieneCheckbox,
                    NombreColumnCheckbox = queryResult.NombreColumnCheckbox,
                    PosicionColumnCheckbox = queryResult.PosicionColumnCheckbox,
                    Depuracion = depuracion
                };

                // Suscribirse al evento solo una vez
                grid.DataBindingComplete -= grid_DataBindingComplete;
                grid.DataBindingComplete += grid_DataBindingComplete;
            }
            catch (Exception ex)
            {
                ErrorLogger.Log(ex, "LoadDataGridWithEntityAsync");
            }
        }

        private static DataTable ConvertToDataTable(object data)
        {
            var dt = new DataTable();

            if (data is not System.Collections.IEnumerable enumerable)
                return dt;

            var enumerator = enumerable.GetEnumerator();
            if (!enumerator.MoveNext())
                return dt;

            var firstItem = enumerator.Current;
            var properties = firstItem.GetType().GetProperties();

            foreach (var prop in properties)
            {
                var columnType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                dt.Columns.Add(prop.Name, columnType);
            }

            var firstRow = dt.NewRow();
            foreach (var prop in properties)
            {
                firstRow[prop.Name] = prop.GetValue(firstItem) ?? DBNull.Value;
            }
            dt.Rows.Add(firstRow);

            while (enumerator.MoveNext())
            {
                var item = enumerator.Current;
                var row = dt.NewRow();
                foreach (var prop in properties)
                {
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                }
                dt.Rows.Add(row);
            }

            return dt;
        }

        private static void grid_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            var grid = (DataGridView)sender;

            try
            {
                grid.ClearSelection();
                grid.CurrentCell = null;

                // Aplicar configuración almacenada
                if (grid.Tag is GridConfig cfg)
                {
                    ConfigurarDataGrid(
                        grid,
                        cfg.ColumnasOcultar,
                        cfg.ColoresColumnas,
                        cfg.TieneCheckbox,
                        cfg.NombreColumnCheckbox,
                        cfg.PosicionColumnCheckbox,
                        cfg.Depuracion
                    );
                }

                AjustarColumnas(grid);
            }
            catch (Exception ex)
            {
                ErrorLogger.Log(ex, "DataBindingComplete");
            }
        }

        public static void ConfigurarDataGrid(
            DataGridView dataGrid,
            List<string> colsHide = null,
            Dictionary<string, Color> coloresColumnas = null,
            bool check = false,
            string colCheckName = "Seleccionar",
            int colCheckPos = 0,
            bool depuracion = false)
        {
            try
            {
                // 🎨 Estilo base
                dataGrid.RowsDefaultCellStyle.BackColor = Color.White;
                dataGrid.AlternatingRowsDefaultCellStyle.BackColor = Color.AliceBlue;
                //dataGrid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);
                //dataGrid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
                dataGrid.ColumnHeadersDefaultCellStyle.SelectionBackColor = dataGrid.ColumnHeadersDefaultCellStyle.BackColor;
                dataGrid.ColumnHeadersDefaultCellStyle.SelectionForeColor = dataGrid.ColumnHeadersDefaultCellStyle.ForeColor;
                //dataGrid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);
                //dataGrid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
                dataGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGrid.MultiSelect = false;
                dataGrid.RowHeadersVisible = false;
                dataGrid.EnableHeadersVisualStyles = false;

                // Limpia selección para evitar resaltado de columna activa
                dataGrid.CurrentCellChanged += (s, e) =>
                {
                    if (dataGrid.CurrentCell != null)
                        dataGrid.ClearSelection();
                };

                // Ocultar columnas
                if (colsHide is not null)
                {
                    foreach (var col in colsHide)
                    {
                        if (dataGrid.Columns.Contains(col))
                            dataGrid.Columns[col].Visible = false;
                    }
                }

                // Aplicar colores personalizados
                if (coloresColumnas is not null)
                {
                    foreach (var par in coloresColumnas)
                    {
                        if (dataGrid.Columns.Contains(par.Key))
                            dataGrid.Columns[par.Key].DefaultCellStyle.BackColor = par.Value;
                    }
                }

                // Agregar columna CheckBox si corresponde
                if (check)
                {
                    if (!dataGrid.Columns.Contains(colCheckName))
                    {
                        var column = new DataGridViewCheckBoxColumn
                        {
                            Name = colCheckName,
                            ReadOnly = false,
                            HeaderText = string.Empty,
                            ThreeState = false
                        };
                        column.DefaultCellStyle.NullValue = false;
                        dataGrid.ReadOnly = false;
                        dataGrid.Columns.Insert(Math.Min(colCheckPos, dataGrid.Columns.Count), column);
                    }

                    var checkColumn = dataGrid.Columns[colCheckName];
                    checkColumn.DisplayIndex = colCheckPos;
                    checkColumn.ReadOnly = false;

                    foreach (DataGridViewColumn col in dataGrid.Columns)
                    {
                        if (!col.Name.Equals(colCheckName, StringComparison.OrdinalIgnoreCase))
                            col.ReadOnly = true;
                    }
                }

                // Reglas por formulario
                if (dataGrid.Name != "dg_view_resultados" && !depuracion)
                {
                    if (dataGrid.Columns.Count > 0)
                        dataGrid.Columns[0].Visible = false;
                }

                if (dataGrid.Name == "dg_viewPedido" && dataGrid.Columns.Count > 1)
                {
                    dataGrid.Columns[1].Visible = false;
                }

                // Ajuste visual rápido
                dataGrid.Height += 1;
                dataGrid.Height -= 1;

                // Ajuste automático de ancho
                dataGrid.AutoSizeColumnsMode = dataGrid.Rows.Count > 0
                    ? DataGridViewAutoSizeColumnsMode.AllCells
                    : DataGridViewAutoSizeColumnsMode.None;

                dataGrid.Refresh();
            }
            catch (Exception ex)
            {
                ErrorLogger.Log(ex, "ConfigurarDataGrid");
            }
        }

        private static void AjustarColumnas(DataGridView grid)
        {
            if (grid is null)
                return;

            try
            {
                grid.AutoSizeColumnsMode = grid.Rows.Count > 0
                    ? DataGridViewAutoSizeColumnsMode.AllCells
                    : DataGridViewAutoSizeColumnsMode.None;
            }
            catch
            {
                grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            }
        }

        public static async Task LoadDataGridWithEntityAsync<T>(DataGridView grid, IQueryable<T> query)
        {
            var queryResult = new DataGridQueryResult
            {
                Query = query
            };

            await LoadDataGridWithEntityAsync(grid, queryResult);
        }
    }
}
