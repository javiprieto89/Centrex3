using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Centrex
{
    public partial class frmCheques
    {
        private ClienteEntity cl = new ClienteEntity();
        private ProveedorEntity pr = new ProveedorEntity();
        private CobroEntity c = new CobroEntity();
        private bool cliente = false;

        public frmCheques(int idCliente = -1, int idProveedor = -1, int idCobro = -1)
        {

            // Esta llamada es exigida por el diseñador.
            InitializeComponent();
            if (idCliente != -1)
            {
                cl = clientes.info_cliente(idCliente);
                cliente = true;
            }
            else
            {
                pr = proveedores.info_proveedor(idProveedor.ToString());
            }

            if (idCobro != -1)
            {
                c = cobros.info_cobro(idCobro.ToString());
            }
            // Agregue cualquier inicialización después de la llamada a InitializeComponent().

        }
        private async void frmCheques_Load(object sender, EventArgs e)
        {
            await InicializarSeleccionChequesAsync();
            await LoadChequesAsync();
        }

        private async void cmd_ok_Click(object sender, EventArgs e)
        {
            if (dg_view.Rows.Count == 0)
                return;

            try
            {
                var ids = dg_view.Rows
                    .Cast<DataGridViewRow>()
                    .Where(fila => fila.Cells["ID"].Value is not null)
                    .Select(fila => Convert.ToInt32(fila.Cells["ID"].Value))
                    .Distinct()
                    .ToList();

                if (ids.Count == 0)
                    return;

                using var ctx = new CentrexDbContext();
                var registros = await ctx.TmpSelChEntity
                    .Where(tmp => ids.Contains(tmp.IdCheque))
                    .ToDictionaryAsync(tmp => tmp.IdCheque);

                foreach (DataGridViewRow fila in dg_view.Rows)
                {
                    if (fila.Cells["ID"].Value is null)
                        continue;

                    var idCheque = Convert.ToInt32(fila.Cells["ID"].Value);
                    var seleccionado = fila.Cells["Seleccionado"].Value != DBNull.Value &&
                                       Convert.ToBoolean(fila.Cells["Seleccionado"].Value);

                    if (registros.TryGetValue(idCheque, out var registro))
                    {
                        registro.Seleccionado = seleccionado;
                    }
                }

                await ctx.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Interaction.MsgBox($"Error al actualizar la selección de cheques: {ex.Message}", MsgBoxStyle.Critical, "Centrex");
            }
        }

        private async Task InicializarSeleccionChequesAsync()
        {
            if (!cliente)
                return;

            using var ctx = new CentrexDbContext();
            var chequeIds = await ctx.ChequeEntity
                .AsNoTracking()
                .Where(ch => ch.IdCliente == cl.IdCliente)
                .Select(ch => ch.IdCheque)
                .ToListAsync();

            if (chequeIds.Count == 0)
                return;

            var existentes = await ctx.TmpSelChEntity
                .Where(tmp => chequeIds.Contains(tmp.IdCheque))
                .Select(tmp => tmp.IdCheque)
                .ToListAsync();

            var nuevos = chequeIds
                .Except(existentes)
                .Select(id => new TmpSelChEntity
                {
                    IdCheque = id,
                    Seleccionado = false
                })
                .ToList();

            if (nuevos.Count > 0)
            {
                await ctx.TmpSelChEntity.AddRangeAsync(nuevos);
                await ctx.SaveChangesAsync();
            }
        }

        private async Task LoadChequesAsync()
        {
            using var ctx = new CentrexDbContext();

            if (cliente)
            {
                lbl_cheques.Text = "Cheques disponibles del cliente: " + cl.RazonSocial;

                var query = ctx.TmpSelChEntity
                    .AsNoTracking()
                    .Include(tmp => tmp.IdChequeNavigation)
                        .ThenInclude(ch => ch.IdBancoNavigation)
                    .Where(tmp => tmp.IdChequeNavigation.IdCliente == cl.IdCliente)
                    .OrderBy(tmp => tmp.IdCheque)
                    .Select(tmp => new
                    {
                        Seleccionado = tmp.Seleccionado,
                        ID = tmp.IdCheque,
                        BancoEmisor = tmp.IdChequeNavigation.IdBancoNavigation.Nombre,
                        NumeroCheque = tmp.IdChequeNavigation.NCheque,
                        Importe = tmp.IdChequeNavigation.Importe
                    });

                var result = new DataGridQueryResult
                {
                    Query = query,
                    ColumnasOcultar = new List<string> { "ID" }
                };

                await LoadDataGridDynamic.LoadDataGridWithEntityAsync(dg_view, result, depuracion: true);

                if (dg_view.Columns.Contains("BancoEmisor"))
                    dg_view.Columns["BancoEmisor"].HeaderText = "Banco emisor";
                if (dg_view.Columns.Contains("NumeroCheque"))
                    dg_view.Columns["NumeroCheque"].HeaderText = "Nº cheque";
                if (dg_view.Columns.Contains("Importe"))
                {
                    dg_view.Columns["Importe"].HeaderText = "$$";
                    dg_view.Columns["Importe"].DefaultCellStyle.Format = "N2";
                }

                dg_view.ReadOnly = false;
                foreach (DataGridViewColumn columna in dg_view.Columns)
                {
                    columna.ReadOnly = columna.Name != "Seleccionado";
                }
            }
            else
            {
                lbl_cheques.Text = "Cheques en cartera";
                dg_view.DataSource = null;
            }
        }
    }
}


