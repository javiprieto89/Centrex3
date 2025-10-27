using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Centrex.Models;
using Microsoft.VisualBasic;

namespace Centrex
{
    public partial class search : Form
    {
        private string _tabla;
        private DataGridQueryResult _queryResult;
        private int _idBanco = -1;
        private bool _historicoActivo = true;
        private bool _addItem = true;
        private bool _comprobanteCompra;
        private int _idComprobanteCompra;

        public int SelectedIndex { get; private set; } = -1;

        public search() : this(string.Empty)
        {
        }

        public search(string tabla)
        {
            InitializeComponent();
            _tabla = tabla;
        }

        // Compatibilidad con constructores anteriores (producción / orden de compra)
        public search(bool produccion, bool ordenCompra, bool addItem) : this(string.Empty)
        {
            _addItem = addItem;
        }

        // Compatibilidad para búsquedas asociadas a comprobantes de compra
        public search(bool comprobanteCompra, int idComprobanteCompra) : this(string.Empty)
        {
            _comprobanteCompra = comprobanteCompra;
            _idComprobanteCompra = idComprobanteCompra;
            _addItem = false;
        }

        // Compatibilidad para búsquedas filtradas por banco
        public search(int idBanco) : this(string.Empty)
        {
            _idBanco = idBanco;
            if (string.IsNullOrEmpty(_tabla))
                _tabla = "cuentas_bancarias";
        }

        private async void search_Load(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                SelectedIndex = -1;
                VariablesGlobales.id = 0;

                if (string.IsNullOrEmpty(_tabla))
                    _tabla = VariablesGlobales.tabla ?? "clientes";

                lblbusqueda.Text = $"Búsqueda ({_tabla})";

                await CargarDataGridAsync();
            }
            catch (Exception ex)
            {
                Interaction.MsgBox($"Error al cargar el buscador: {ex.Message}", MsgBoxStyle.Critical, "Centrex");
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private async Task CargarDataGridAsync(string filtroTexto = "")
        {
            try
            {
                using var ctx = new CentrexDbContext();

                _queryResult = DataGridQueryFactory.GetQueryForTable(ctx, _tabla, _historicoActivo, _idBanco);

                if (!string.IsNullOrWhiteSpace(filtroTexto) && _queryResult.Query != null)
                {
                    var elementos = _queryResult.Query.Cast<object>().ToList();
                    var propiedades = elementos.FirstOrDefault()?.GetType().GetProperties();

                    if (propiedades != null)
                    {
                        var filtrado = elementos
                            .Where(x => propiedades.Any(p =>
                            {
                                var valor = p.GetValue(x);
                                return valor != null && valor.ToString().IndexOf(filtroTexto, StringComparison.OrdinalIgnoreCase) >= 0;
                            }))
                            .ToList();

                        _queryResult.EsMaterializada = true;
                        _queryResult.DataMaterializada = filtrado;
                    }
                }

                await LoadDataGridDynamic.LoadDataGridWithEntityAsync(dg_view, _queryResult, depuracion: true);
                lbl_totalRegistros.Text = $"Total: {dg_view.Rows.Count}";
            }
            catch (Exception ex)
            {
                Interaction.MsgBox($"Error al cargar los datos: {ex.Message}", MsgBoxStyle.Critical, "Centrex");
            }
        }

        private async void txt_search_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                e.Handled = true;
                await CargarDataGridAsync(txt_search.Text);
            }
        }

        private async void cmd_go_Click(object sender, EventArgs e)
        {
            await CargarDataGridAsync(txt_search.Text);
        }

        private async void lbl_borrarbusqueda_DoubleClick(object sender, EventArgs e)
        {
            txt_search.Text = string.Empty;
            await CargarDataGridAsync();
        }

        private void cmd_ok_Click(object sender, EventArgs e)
        {
            SeleccionarActual();
        }

        private void dg_view_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
                SeleccionarActual();
        }

        private void dg_view_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SeleccionarActual();
            }
        }

        private void SeleccionarActual()
        {
            try
            {
                if (dg_view.CurrentRow == null)
                    return;

                var valor = dg_view.CurrentRow.Cells[0].Value?.ToString();
                if (string.IsNullOrEmpty(valor) || !int.TryParse(valor, out var id))
                    return;

                SelectedIndex = id;
                VariablesGlobales.id = id;

                Close();
            }
            catch (Exception ex)
            {
                Interaction.MsgBox($"Error al devolver la selección: {ex.Message}", MsgBoxStyle.Critical, "Centrex");
            }
        }

        private void cmd_prev_Click(object sender, EventArgs e)
        {
            Interaction.MsgBox("La paginación aún no está implementada en la nueva versión del buscador.", MsgBoxStyle.Information, "Centrex");
        }

        private void cmd_next_Click(object sender, EventArgs e)
        {
            Interaction.MsgBox("La paginación aún no está implementada en la nueva versión del buscador.", MsgBoxStyle.Information, "Centrex");
        }

        private void cmd_first_Click(object sender, EventArgs e)
        {
            Interaction.MsgBox("La paginación aún no está implementada en la nueva versión del buscador.", MsgBoxStyle.Information, "Centrex");
        }

        private void cmd_last_Click(object sender, EventArgs e)
        {
            Interaction.MsgBox("La paginación aún no está implementada en la nueva versión del buscador.", MsgBoxStyle.Information, "Centrex");
        }

        private void cmd_salir_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
