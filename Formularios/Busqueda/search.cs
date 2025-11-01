using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Centrex
{
    public partial class search : Form
    {
        // ================== CAMPOS INTERNOS ==================
        private string _tabla = "";
        private DataGridQueryResult _queryResult;
        private int _idBanco = -1;
        private bool _historicoActivo = true;
        private bool _addItem = true;
        private bool _produccion = false;
        private bool _ordenCompra = false;
        private bool _comprobanteCompra = false;
        private int _idComprobanteCompra = -1;
        private int _idUsuario = -1;
        private Guid _idUnico = Guid.Empty;

        private int _pagina = 1;
        private int _totalRegistros = 0;
        private int _totalPaginas = 0;
        private const int _itemsPorPagina = 50;

        private ClienteEntity _cli;
        private ComprobanteEntity _cmp;


        public int SelectedIndex { get; private set; } = -1;

        // ================== CONSTRUCTORES ==================

        public search() : this(string.Empty) { }

        public search(string tabla)
        {
            InitializeComponent();
            _tabla = tabla;
        }


        // Producción / Orden de compra
        public search(bool produccion, bool ordenCompra, bool addItem) : this(string.Empty)
        {
            _produccion = produccion;
            _ordenCompra = ordenCompra;
            _addItem = addItem;
        }

        // Comprobante de compra + ID
        public search(bool comprobanteCompra, int idComprobanteCompra) : this(string.Empty)
        {
            _comprobanteCompra = comprobanteCompra;
            _idComprobanteCompra = idComprobanteCompra;
            _addItem = false;
        }

        // Filtro por banco
        public search(int idBanco) : this(string.Empty)
        {
            _idBanco = idBanco;
            _tabla = "cuentas_bancarias";
        }

        // Cliente + comprobante (usado en anulaciones)
        public search(ClienteEntity cliente, ComprobanteEntity comprobante) : this(string.Empty)
        {
            _cli = cliente;
            _cmp = comprobante;
        }

        // Usuario + sesión única
        public search(int idUsuario, Guid idUnico) : this(string.Empty)
        {
            _idUsuario = idUsuario;
            _idUnico = idUnico;

            _addItem = false;
            _historicoActivo = true;
        }

        // Búsqueda ligada a usuario + sesión + tabla
        public search(int idUsuario, Guid idUnico, string tabla) : this(tabla)
        {
            _idUsuario = idUsuario;
            _idUnico = idUnico;
            _addItem = true;
            _historicoActivo = true;
        }

        // ================== LOAD EVENT ==================

        private async void search_Load(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                SelectedIndex = -1;
                id = 0;

                if (string.IsNullOrEmpty(_tabla))
                    _tabla = tabla ?? "clientes";

                lblbusqueda.Text = $"Búsqueda ({_tabla})";

                await CargarPaginaAsync();
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

        // ================== CARGA Y FILTRO ==================

        private async Task CargarPaginaAsync(string filtro = "")
        {
            try
            {
                // =======================================
                // CASO ESPECIAL: ANULA COMPROBANTE AFIP
                // =======================================
                if (_tabla == "anulaComprobanteAFIP")
                {
                    using var ctx = new CentrexDbContext();

                    var idCliente = _cli?.IdCliente ?? 0;
                    var tipo = _cmp?.IdTipoComprobante ?? 0;

                    List<int> tiposPermitidos = new();

                    // Si eligió Notas de crédito → mostrar facturas y notas de débito
                    if (new[] { 3, 8, 13, 53, 201, 203, 206, 208, 211, 213 }.Contains(tipo))
                        tiposPermitidos = new() { 1, 2, 6, 7, 11, 12, 51, 52, 201, 202, 206, 207, 211, 212 };
                    // Si eligió Notas de débito → mostrar notas de crédito y facturas
                    else if (new[] { 2, 7, 12, 52, 202, 207, 212 }.Contains(tipo))
                        tiposPermitidos = new() { 3, 8, 13, 53, 201, 203, 206, 208, 211, 213, 1, 6, 11, 51, 201, 206, 211 };

                    var queryAFIP = ctx.PedidoEntity
    .Include(p => p.IdClienteNavigation)
    .Include(p => p.IdComprobanteNavigation)
        .ThenInclude(cp => cp.IdTipoComprobanteNavigation)
    .Where(p =>
        p.IdCliente == idCliente &&
        tiposPermitidos.Contains(p.IdComprobanteNavigation.IdTipoComprobante) &&
        !string.IsNullOrEmpty(p.Cae))
    .OrderByDescending(p => p.FechaEdicion)
    .ThenByDescending(p => p.IdPedido)
    .Select(p => new
    {
        ID = p.IdPedido,
        Fecha = p.FechaEdicion,
        RazonSocial = p.IdClienteNavigation.RazonSocial,
        Comprobante = p.IdComprobanteNavigation.Comprobante,
        NumeroComprobante = p.IdComprobanteNavigation.IdTipoComprobante == 99
            ? p.IdPresupuesto
            : p.NumeroComprobante,
        Total = p.Total,
        Activo = p.Activo
    });


                    // --- Paginación y carga ---
                    var total = await EntityFrameworkQueryableExtensions.CountAsync(queryAFIP);
                    _totalRegistros = total;
                    _totalPaginas = (int)Math.Ceiling((double)total / _itemsPorPagina);
                    var skip = (_pagina - 1) * _itemsPorPagina;

                    var pageData = await EntityFrameworkQueryableExtensions
                        .ToListAsync(queryAFIP.Skip(skip).Take(_itemsPorPagina));

                    _queryResult = new DataGridQueryResult
                    {
                        Query = queryAFIP,
                        EsMaterializada = true,
                        DataMaterializada = pageData,
                        ColumnasOcultar = new List<string> { "ID" }
                    };

                    await LoadDataGridDynamic.LoadDataGridWithEntityAsync(dg_view, _queryResult);
                    lbl_totalRegistros.Text = $"Total: {_totalRegistros}";
                    txt_nPage.Text = $"Página {_pagina} de {_totalPaginas}";

                    return;
                }

                // =======================================
                // RESTO DE TABLAS NORMALES
                // =======================================

                using var ctxNormal = new CentrexDbContext();
                _queryResult = DataGridQueryFactory.GetQueryForTable(ctxNormal, _tabla, _historicoActivo, _idBanco);

                if (_queryResult.Query == null)
                {
                    Interaction.MsgBox("No hay datos para mostrar.", MsgBoxStyle.Information, "Centrex");
                    return;
                }

                var query = _queryResult.Query;

                // --- CountAsync genérico ---
                var countMethod = typeof(EntityFrameworkQueryableExtensions)
                    .GetMethods()
                    .First(m => m.Name == "CountAsync" && m.GetParameters().Length == 2)
                    .MakeGenericMethod(query.ElementType);

                var countTask = (Task)countMethod.Invoke(null, new object[] { query, default(System.Threading.CancellationToken) });
                await countTask.ConfigureAwait(false);
                var countResult = (int)countTask.GetType().GetProperty("Result").GetValue(countTask);

                _totalRegistros = countResult;
                _totalPaginas = (int)Math.Ceiling((double)_totalRegistros / _itemsPorPagina);

                var skipCount = (_pagina - 1) * _itemsPorPagina;

                // --- ToListAsync genérico con paginación ---
                var skipMethod = typeof(Queryable).GetMethods()
                    .First(m => m.Name == "Skip" && m.GetParameters().Length == 2)
                    .MakeGenericMethod(query.ElementType);

                var takeMethod = typeof(Queryable).GetMethods()
                    .First(m => m.Name == "Take" && m.GetParameters().Length == 2)
                    .MakeGenericMethod(query.ElementType);

                var skippedQuery = skipMethod.Invoke(null, new object[] { query, skipCount });
                var takenQuery = takeMethod.Invoke(null, new object[] { skippedQuery, _itemsPorPagina });

                var toListMethod = typeof(EntityFrameworkQueryableExtensions)
                    .GetMethods()
                    .First(m => m.Name == "ToListAsync" && m.GetParameters().Length == 2)
                    .MakeGenericMethod(query.ElementType);

                var listTask = (Task)toListMethod.Invoke(null, new object[] { takenQuery, default(System.Threading.CancellationToken) });
                await listTask.ConfigureAwait(false);
                var pageList = listTask.GetType().GetProperty("Result").GetValue(listTask);

                _queryResult.EsMaterializada = true;
                _queryResult.DataMaterializada = pageList;

                await LoadDataGridDynamic.LoadDataGridWithEntityAsync(dg_view, _queryResult, depuracion: false);
                lbl_totalRegistros.Text = $"Total: {_totalRegistros}";
                txt_nPage.Text = $"Página {_pagina} de {_totalPaginas}";
            }
            catch (Exception ex)
            {
                Interaction.MsgBox($"Error al cargar datos: {ex.Message}", MsgBoxStyle.Critical, "Centrex");
            }
        }

        // ================== EVENTOS DE BÚSQUEDA ==================

        private async void txt_search_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                e.Handled = true;
                _pagina = 1;
                var txtsearch = txt_search.Text.Replace(" ", "%");

                if (_tabla == "anulaComprobanteAFIP")
                {
                    using var ctx = new CentrexDbContext();

                    var idCliente = _cli?.IdCliente ?? 0;
                    var tipo = _cmp?.IdTipoComprobante ?? 0;

                    List<int> tiposPermitidos = new();

                    if (new[] { 3, 8, 13, 53, 201, 203, 206, 208, 211, 213 }.Contains(tipo))
                        tiposPermitidos = new() { 1, 2, 6, 7, 11, 12, 51, 52, 201, 202, 206, 207, 211, 212 };
                    else if (new[] { 2, 7, 12, 52, 202, 207, 212 }.Contains(tipo))
                        tiposPermitidos = new() { 3, 8, 13, 53, 201, 203, 206, 208, 211, 213, 1, 6, 11, 51, 201, 206, 211 };

                    var queryAFIP = ctx.PedidoEntity
                        .Include(p => p.IdClienteNavigation)
                        .Include(p => p.IdComprobanteNavigation)
                        .ThenInclude(cp => cp.IdTipoComprobanteNavigation)
                        .Where(p =>
                            p.IdCliente == idCliente &&
                            tiposPermitidos.Contains(p.IdComprobanteNavigation.IdTipoComprobante) &&
                            !string.IsNullOrEmpty(p.Cae) &&
                            (p.IdPedido.ToString().Contains(txtsearch) ||
                             p.IdClienteNavigation.RazonSocial.Contains(txtsearch) ||
                             p.NumeroComprobante.ToString().Contains(txtsearch) ||
                             p.IdPresupuesto.ToString().Contains(txtsearch)))
                        .OrderByDescending(p => p.FechaEdicion)
                        .ThenByDescending(p => p.IdPedido)
                        .Select(p => new
                        {
                            ID = p.IdPedido,
                            Fecha = p.FechaEdicion,
                            RazonSocial = p.IdClienteNavigation.RazonSocial,
                            Comprobante = p.IdComprobanteNavigation.Comprobante,
                            NumeroComprobante = p.IdComprobanteNavigation.IdTipoComprobante == 99
                                ? p.IdPresupuesto
                                : p.NumeroComprobante,
                            Total = p.Total,
                            Activo = p.Activo
                        });

                    _queryResult = new DataGridQueryResult
                    {
                        Query = queryAFIP,
                        EsMaterializada = false
                    };

                    await LoadDataGridDynamic.LoadDataGridWithEntityAsync(dg_view, _queryResult);
                    return;
                }

                await CargarPaginaAsync(txtsearch);
            }
        }


        private async void cmd_go_Click(object sender, EventArgs e)
        {
            _pagina = 1;
            await CargarPaginaAsync(txt_search.Text);
        }

        private async void lbl_borrarbusqueda_DoubleClick(object sender, EventArgs e)
        {
            txt_search.Text = "";
            _pagina = 1;
            await CargarPaginaAsync();
        }

        // ================== EVENTOS DE PAGINACIÓN ==================

        private async void cmd_next_Click(object sender, EventArgs e)
        {
            if (_pagina < _totalPaginas)
            {
                _pagina++;
                await CargarPaginaAsync(txt_search.Text);
            }
        }

        private async void cmd_prev_Click(object sender, EventArgs e)
        {
            if (_pagina > 1)
            {
                _pagina--;
                await CargarPaginaAsync(txt_search.Text);
            }
        }

        private async void cmd_first_Click(object sender, EventArgs e)
        {
            _pagina = 1;
            await CargarPaginaAsync(txt_search.Text);
        }

        private async void cmd_last_Click(object sender, EventArgs e)
        {
            _pagina = _totalPaginas;
            await CargarPaginaAsync(txt_search.Text);
        }

        private async void cmd_goPage_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txt_nPage.Text, out int nuevaPagina))
            {
                if (nuevaPagina >= 1 && nuevaPagina <= _totalPaginas)
                {
                    _pagina = nuevaPagina;
                    await CargarPaginaAsync(txt_search.Text);
                }
                else
                {
                    Interaction.MsgBox("Número de página fuera de rango.", MsgBoxStyle.Information, "Centrex");
                }
            }
        }

        // ================== EVENTOS GENERALES ==================

        private void cmd_ok_Click(object sender, EventArgs e) => SeleccionarActual();

        private void search_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                if (Application.OpenForms.Count > 0)
                {
                    var mainForm = Application.OpenForms.Cast<Form>().FirstOrDefault(f => f != this);
                    if (mainForm != null)
                        mainForm.Enabled = true;
                }

                generales.closeandupdate(this);
            }
            catch (Exception ex)
            {
                Interaction.MsgBox($"Error al cerrar el buscador: {ex.Message}", MsgBoxStyle.Critical, "Centrex");
            }
        }

        private void search_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
                {
                    txt_search.SelectAll();
                    txt_search.Focus();
                }
            }
            catch (Exception ex)
            {
                Interaction.MsgBox($"Error en búsqueda rápida: {ex.Message}", MsgBoxStyle.Critical, "Centrex");
            }
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

        // ================== LÓGICA DE SELECCIÓN ==================

        private void SeleccionarActual()
        {
            try
            {
                if (dg_view.CurrentRow == null)
                    return;

                var valor = dg_view.CurrentRow.Cells[0].Value?.ToString();
                if (string.IsNullOrEmpty(valor) || !int.TryParse(valor, out var id))
                    return;

                id= SelectedIndex;                

                // Lógica similar a VB: depende de la tabla y el contexto
                if (_tabla.StartsWith("items"))
                {
                    // Lógica de agregar item (simplificada; puede vincular a infoagregaitem)
                    Interaction.MsgBox($"Seleccionado item ID: {id}", MsgBoxStyle.Information, "Centrex");
                }
                else if (_tabla == "pedidos")
                {
                    Interaction.MsgBox($"Seleccionado pedido #{id}", MsgBoxStyle.Information, "Centrex");
                }
                else if (_tabla == "cuentas_bancarias")
                {
                    Interaction.MsgBox($"Seleccionada cuenta ID: {id}", MsgBoxStyle.Information, "Centrex");
                }

                Close();
            }
            catch (Exception ex)
            {
                Interaction.MsgBox($"Error al devolver selección: {ex.Message}", MsgBoxStyle.Critical, "Centrex");
            }
        }

        private void cmd_salir_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
