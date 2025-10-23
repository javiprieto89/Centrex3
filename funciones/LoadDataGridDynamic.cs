using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;

namespace Centrex
{
    public static class LoadDataGridDynamic
    {
        /// <summary>
        /// Carga un DataGridView dinámicamente desde un DataGridQueryResult de forma asíncrona
        /// </summary>
        public static async Task LoadDataGridWithEntityAsync(
            DataGridView grid,
            DataGridQueryResult queryResult,
            bool depuracion = false)
        {
            try
            {
                if (queryResult == null || queryResult.Query == null)
                {
                    MessageBox.Show("No hay datos para mostrar.", "Centrex", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Ejecutar consulta y materializar datos
                object data;

                if (queryResult.EsMaterializada && queryResult.DataMaterializada != null)
                {
                    // Ya está materializada
                    data = queryResult.DataMaterializada;
                }
                else
                {
                    // Materializar la query de forma asíncrona
                    var queryable = queryResult.Query;
                    var listMethod = typeof(EntityFrameworkQueryableExtensions)
                        .GetMethod("ToListAsync")
                        .MakeGenericMethod(queryable.ElementType);

                    var task = (Task)listMethod.Invoke(null, new object[] { queryable, default(System.Threading.CancellationToken) });
                    await task.ConfigureAwait(false);

                    var resultProperty = task.GetType().GetProperty("Result");
                    data = resultProperty.GetValue(task);
                }

                // Convertir a DataTable para el grid
                var dt = ConvertToDataTable(data);

                // Aplicar formato de depositado si es necesario (para cheques)
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

                // Asignar al grid
                grid.DataSource = dt;

                // Configurar apariencia y comportamiento del grid
                ConfigurarDataGrid(
                    grid,
                    queryResult.ColumnasOcultar,
                    queryResult.ColoresColumnas,
                    queryResult.TieneCheckbox,
                    queryResult.NombreColumnCheckbox,
                    queryResult.PosicionColumnCheckbox,
                    depuracion);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error cargando grilla: {ex.Message}\n\n{ex.StackTrace}", "Centrex", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Convierte una lista de objetos anónimos a DataTable
        /// </summary>
        private static DataTable ConvertToDataTable(object data)
        {
            var dt = new DataTable();

            if (data == null)
                return dt;

            var enumerable = data as System.Collections.IEnumerable;
            if (enumerable == null)
                return dt;

            var enumerator = enumerable.GetEnumerator();
            if (!enumerator.MoveNext())
                return dt; // Lista vacía

            var firstItem = enumerator.Current;
            var properties = firstItem.GetType().GetProperties();

            // Crear columnas
            foreach (var prop in properties)
            {
                var columnType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                dt.Columns.Add(prop.Name, columnType);
            }

            // Agregar primera fila
            var firstRow = dt.NewRow();
            foreach (var prop in properties)
            {
                var value = prop.GetValue(firstItem);
                firstRow[prop.Name] = value ?? DBNull.Value;
            }
            dt.Rows.Add(firstRow);

            // Agregar resto de filas
            while (enumerator.MoveNext())
            {
                var item = enumerator.Current;
                var row = dt.NewRow();
                foreach (var prop in properties)
                {
                    var value = prop.GetValue(item);
                    row[prop.Name] = value ?? DBNull.Value;
                }
                dt.Rows.Add(row);
            }

            return dt;
        }

        /// <summary>
        /// Configura la apariencia y funcionalidad visual de un DataGridView.
        /// Permite personalizar columnas, colores, visibilidad y agregar checkbox de selección.
        /// </summary>
        /// <param name="dataGrid">El control DataGridView a configurar.</param>
        /// <param name="colsHide">Lista opcional de nombres de columnas a ocultar.</param>
        /// <param name="coloresColumnas">Diccionario (columna → color) para personalizar columnas específicas.</param>
        /// <param name="check">Si es True, agrega una columna checkbox editable.</param>
        /// <param name="colCheckName">Nombre de la columna checkbox.</param>
        /// <param name="colCheckPos">Posición de la columna checkbox.</param>
        /// <param name="depuracion">Si está en modo depuración, evita ocultar la primera columna.</param>
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
                // 🎨 Estilo general
                dataGrid.RowsDefaultCellStyle.BackColor = Color.White;
                dataGrid.AlternatingRowsDefaultCellStyle.BackColor = Color.AliceBlue;
                dataGrid.EnableHeadersVisualStyles = false;
                dataGrid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);
                dataGrid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
                dataGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGrid.MultiSelect = false;
                dataGrid.RowHeadersVisible = false;

                // 🔢 Mantener orden de columnas
                int i = 0;
                foreach (DataGridViewColumn columna in dataGrid.Columns)
                {
                    dataGrid.Columns[columna.Name].DisplayIndex = i;
                    i++;
                }

                // 👁️ Ocultar columnas por nombre
                if (colsHide != null)
                {
                    foreach (var col in colsHide)
                    {
                        if (dataGrid.Columns.Contains(col))
                        {
                            dataGrid.Columns[col].Visible = false;
                        }
                    }
                }

                // 🟡 Aplicar color personalizado por columna
                if (coloresColumnas != null)
                {
                    foreach (var par in coloresColumnas)
                    {
                        if (dataGrid.Columns.Contains(par.Key))
                        {
                            dataGrid.Columns[par.Key].DefaultCellStyle.BackColor = par.Value;
                        }
                    }
                }

                // 🧩 Agregar columna checkbox (si corresponde)
                if (check)
                {
                    var column = new DataGridViewCheckBoxColumn
                    {
                        Name = colCheckName,
                        ReadOnly = false
                    };
                    dataGrid.ReadOnly = false;
                    dataGrid.Columns.Add(column);
                    dataGrid.Columns[colCheckName].DisplayIndex = colCheckPos;
                    dataGrid.Columns[colCheckName].ReadOnly = false;

                    // Solo permitir edición en la columna de checkbox
                    foreach (DataGridViewColumn col in dataGrid.Columns)
                    {
                        if (!col.Name.Equals(colCheckName))
                        {
                            col.ReadOnly = true;
                        }
                    }
                }

                // 👀 Reglas específicas por formulario
                if (dataGrid.Name != "dg_view_resultados" && !depuracion)
                {
                    if (dataGrid.Columns.Count > 0)
                        dataGrid.Columns[0].Visible = false;
                }

                if (dataGrid.Name == "dg_viewPedido")
                {
                    if (dataGrid.Columns.Count > 1)
                        dataGrid.Columns[1].Visible = false;
                }

                // 🔄 Ajuste visual rápido
                dataGrid.Height += 1;
                dataGrid.Height -= 1;

                // 📐 Ajuste de ancho automático
                if (dataGrid.Rows.Count > 0)
                {
                    dataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                }
                else
                {
                    dataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                }

                dataGrid.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al configurar DataGrid: {ex.Message}", "Centrex", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Sobrecarga del método para mantener compatibilidad con código existente que usa IQueryable directamente
        /// </summary>
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
