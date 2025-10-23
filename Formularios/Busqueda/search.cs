using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Centrex.Models;

namespace Centrex
{
    public partial class search : Form
    {
        private string _tabla;
        private DataGridQueryResult _queryResult;

        public search(string tabla = "", bool filtroActivo = true, bool esCaso = false)
        {
            InitializeComponent();
            _tabla = tabla;
        }

        // ===============================================
        // 🔹 Carga inicial del formulario
        // ===============================================
        private async void search_Load(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                // Determinar tabla a mostrar
                if (string.IsNullOrEmpty(_tabla))
                    _tabla = VariablesGlobales.tabla ?? "clientes";

                lbl_titulo.Text = "Buscando en: " + _tabla.ToUpper();

                await CargarDataGridAsync();
            }
            catch (Exception ex)
            {
                Interaction.MsgBox($"Error al cargar formulario de búsqueda: {ex.Message}", MsgBoxStyle.Critical, "Centrex");
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        // ===============================================
        // 🔹 Cargar la grilla dinámicamente
        // ===============================================
        private async Task CargarDataGridAsync(string filtroTexto = "")
        {
            try
            {
                using var ctx = new CentrexDbContext();

                // Crear la consulta dinámica con la fábrica
                _queryResult = DataGridQueryFactory.GetQueryForTable(ctx, _tabla);

                // Aplicar filtro textual si existe
                if (!string.IsNullOrEmpty(filtroTexto) && _queryResult.Query != null)
                {
                    var tipo = _queryResult.Query.ElementType;
                    var propiedades = tipo.GetProperties();

                    // Aplica un filtro textual básico (contains)
                    _queryResult.Query = _queryResult.Query.Cast<object>()
                        .Where(x => propiedades.Any(p =>
                        {
                            var val = p.GetValue(x);
                            return val != null && val.ToString().Contains(filtroTexto, StringComparison.OrdinalIgnoreCase);
                        }))
                        .AsQueryable();
                }

                // Cargar grilla con los resultados
                await LoadDataGridDynamic.LoadDataGridWithEntityAsync(dg_view_resultados, _queryResult);
            }
            catch (Exception ex)
            {
                Interaction.MsgBox($"Error cargando datos: {ex.Message}", MsgBoxStyle.Critical, "Centrex");
            }
        }

        // ===============================================
        // 🔹 Botón Buscar / Enter
        // ===============================================
        private async void txt_busqueda_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                await CargarDataGridAsync(txt_busqueda.Text);
            }
        }

        private async void cmd_buscar_Click(object sender, EventArgs e)
        {
            await CargarDataGridAsync(txt_busqueda.Text);
        }

        // ===============================================
        // 🔹 Selección de fila (doble clic o Enter)
        // ===============================================
        private void dg_view_resultados_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
                DevolverSeleccion();
        }

        private void dg_view_resultados_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                DevolverSeleccion();
            }
        }

        // ===============================================
        // 🔹 Devolver ID seleccionado al formulario que llamó
        // ===============================================
        private void DevolverSeleccion()
        {
            try
            {
                if (dg_view_resultados.CurrentRow == null)
                    return;

                var id = dg_view_resultados.CurrentRow.Cells[0].Value?.ToString();
                if (string.IsNullOrEmpty(id))
                    return;

                VariablesGlobales.id = Convert.ToInt32(id);

                // Buscar formulario llamador
                var callerForm = Application.OpenForms.Cast<Form>()
                    .FirstOrDefault(f => f is add_pedido || f is add_banco);

                if (callerForm is not null)
                {
                    callerForm.Tag = VariablesGlobales.id;
                    callerForm.BringToFront();
                }

                this.Close();
            }
            catch (Exception ex)
            {
                Interaction.MsgBox($"Error al devolver selección: {ex.Message}", MsgBoxStyle.Critical, "Centrex");
            }
        }

        // ===============================================
        // 🔹 Botón Salir
        // ===============================================
        private void cmd_salir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
