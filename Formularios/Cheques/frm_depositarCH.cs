using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Centrex
{
    public partial class frm_depositarCH
    {
        private int desde_cartera;
        private int pagina_cartera;
        private int nRegs_cartera;
        private int tPaginas_cartera;
        // Dim fecha_desde_cartera As Date
        // Dim fecha_hasta_cartera As Date

        private int desde_depositado;
        private int pagina_depositado;
        private int nRegs_depositado;
        private int tPaginas_depositado;

        public frm_depositarCH()
        {
            InitializeComponent();
        }

        // *******************************
        // * id_estadoch   *   Estado    *
        // *******************************
        // *  1           *   En cartera *
        // *  2           *   Entregado  *
        // *  3           *   Cobrado    *
        // *  4           *   Rechazado  *
        // *******************************

        private async void frm_depositarCH_Load(object sender, EventArgs e)
        {
            var ordenBancos = new List<Tuple<string, bool>> { Tuple.Create("Nombre", true) };
            generales.Cargar_Combo(
                ref cmb_banco,
                entidad: "BancoEntity",
                displaymember: "Nombre",
                valuemember: "IdBanco",
                predet: -1,
                soloActivos: true,
                filtros: null,
                orden: ordenBancos);

            cmb_banco.SelectedIndex = -1;

            cmb_cuentaBancaria.DataSource = null;
            cmb_cuentaBancaria.Items.Clear();
            cmb_cuentaBancaria.Enabled = false;
            cmb_cuentaBancaria.SelectedIndex = -1;

            chk_desdeSiempre_cartera.Checked = true;
            dtp_desde_cartera.Value = dtp_desde_cartera.MinDate;
            chk_hastaSiempre_cartera.Checked = true;
            dtp_hasta_cartera.Value = DateTime.Today.Date;

            chk_desdeSiempre_depositado.Checked = true;
            dtp_desde_depositado.Value = dtp_desde_depositado.MinDate;
            chk_hastaSiempre_depositado.Checked = true;
            dtp_hasta_depositado.Value = DateTime.Today.Date;

            pagina_cartera = 1;
            desde_cartera = 0;
            pagina_depositado = 1;
            desde_depositado = 0;

            await LoadCarteraAsync(resetPage: true);
            await LoadDepositadosAsync(resetPage: true);
        }

        private async Task LoadCarteraAsync(bool resetPage)
        {
            try
            {
                if (resetPage)
                {
                    pagina_cartera = 1;
                    desde_cartera = 0;
                }

                using var ctx = new CentrexDbContext();

                var query = ctx.ChequeEntity
                    .AsNoTracking()
                    .Include(ch => ch.IdClienteNavigation)
                    .Include(ch => ch.IdProveedorNavigation)
                    .Include(ch => ch.IdBancoNavigation)
                    .Include(ch => ch.IdCuentaBancariaNavigation)
                    .Include(ch => ch.IdEstadochNavigation)
                    .Where(ch => ch.Activo && ch.IdEstadoch == ID_CH_CARTERA);

                if (chk_desdeSiempre_cartera.Checked)
                {
                    var fechaDesde = DateOnly.FromDateTime(dtp_desde_cartera.Value.Date);
                    query = query.Where(ch => ch.FechaCobro.HasValue && ch.FechaCobro.Value >= fechaDesde);
                }

                if (chk_hastaSiempre_cartera.Checked)
                {
                    var fechaHasta = DateOnly.FromDateTime(dtp_hasta_cartera.Value.Date);
                    query = query.Where(ch => ch.FechaCobro.HasValue && ch.FechaCobro.Value <= fechaHasta);
                }

                if (int.TryParse(txt_nCH_cartera.Text, out var numeroCheque))
                {
                    query = query.Where(ch => ch.NCheque == numeroCheque);
                }

                if (decimal.TryParse(txt_importeCH_cartera.Text, NumberStyles.Number, CultureInfo.CurrentCulture, out var importeCheque))
                {
                    query = query.Where(ch => ch.Importe == importeCheque);
                }

                nRegs_cartera = await query.CountAsync();

                var pageSize = itXPage > 0 ? itXPage : 50;
                tPaginas_cartera = nRegs_cartera == 0 ? 1 : (int)Math.Ceiling(nRegs_cartera / (double)pageSize);

                if (pagina_cartera < 1)
                    pagina_cartera = 1;
                if (pagina_cartera > tPaginas_cartera)
                    pagina_cartera = tPaginas_cartera;

                desde_cartera = (pagina_cartera - 1) * pageSize;
                if (desde_cartera < 0)
                    desde_cartera = 0;

                var pagedQuery = query
                    .OrderBy(ch => ch.IdCheque)
                    .Skip(desde_cartera)
                    .Take(pageSize)
                    .Select(ch => new
                    {
                        ID = ch.IdCheque,
                        FechaIngreso = ch.FechaIngreso,
                        FechaEmision = ch.FechaEmision,
                        RecibidoDe = ch.IdClienteNavigation != null ? ch.IdClienteNavigation.RazonSocial : string.Empty,
                        EntregadoA = ch.IdProveedorNavigation != null ? ch.IdProveedorNavigation.RazonSocial : string.Empty,
                        BancoEmisor = ch.IdBancoNavigation.Nombre,
                        DepositadoEn = ch.IdCuentaBancariaNavigation != null ? ch.IdCuentaBancariaNavigation.Nombre : string.Empty,
                        NumeroCheque = ch.NCheque,
                        SegundoNumeroCheque = ch.NCheque2,
                        Monto = ch.Importe,
                        Estado = ch.IdEstadochNavigation.Estado,
                        FechaCobro = ch.FechaCobro,
                        FechaSalida = ch.FechaSalida,
                        FechaDeposito = ch.FechaDeposito
                    });

                var result = new DataGridQueryResult
                {
                    Query = pagedQuery,
                    ColumnasOcultar = new List<string> { "ID" },
                    EsMaterializada = false
                };

                await LoadDataGridDynamic.LoadDataGridWithEntityAsync(dg_view_chCartera, result);

                if (dg_view_chCartera.Columns.Contains("FechaIngreso"))
                    dg_view_chCartera.Columns["FechaIngreso"].HeaderText = "F. Ingreso";
                if (dg_view_chCartera.Columns.Contains("FechaEmision"))
                    dg_view_chCartera.Columns["FechaEmision"].HeaderText = "F. Emisión";
                if (dg_view_chCartera.Columns.Contains("RecibidoDe"))
                    dg_view_chCartera.Columns["RecibidoDe"].HeaderText = "Recibido de";
                if (dg_view_chCartera.Columns.Contains("EntregadoA"))
                    dg_view_chCartera.Columns["EntregadoA"].HeaderText = "Entregado a";
                if (dg_view_chCartera.Columns.Contains("BancoEmisor"))
                    dg_view_chCartera.Columns["BancoEmisor"].HeaderText = "Banco emisor";
                if (dg_view_chCartera.Columns.Contains("NumeroCheque"))
                    dg_view_chCartera.Columns["NumeroCheque"].HeaderText = "Nº cheque";
                if (dg_view_chCartera.Columns.Contains("SegundoNumeroCheque"))
                    dg_view_chCartera.Columns["SegundoNumeroCheque"].HeaderText = "2º nº cheque";
                if (dg_view_chCartera.Columns.Contains("Monto"))
                {
                    dg_view_chCartera.Columns["Monto"].HeaderText = "$$";
                    dg_view_chCartera.Columns["Monto"].DefaultCellStyle.Format = "N2";
                }
                if (dg_view_chCartera.Columns.Contains("Estado"))
                    dg_view_chCartera.Columns["Estado"].HeaderText = "Estado";
                if (dg_view_chCartera.Columns.Contains("FechaCobro"))
                    dg_view_chCartera.Columns["FechaCobro"].HeaderText = "Fecha de cobro";
                if (dg_view_chCartera.Columns.Contains("FechaSalida"))
                    dg_view_chCartera.Columns["FechaSalida"].HeaderText = "Fecha de salida";
                if (dg_view_chCartera.Columns.Contains("FechaDeposito"))
                    dg_view_chCartera.Columns["FechaDeposito"].HeaderText = "Fecha de depósito";

                txt_nPage_cartera.Text = nRegs_cartera == 0 ? "0 / 0" : $"{pagina_cartera} / {tPaginas_cartera}";
                dg_view_chCartera.ClearSelection();
            }
            catch (Exception ex)
            {
                Interaction.MsgBox($"Error al cargar cheques en cartera: {ex.Message}", MsgBoxStyle.Critical, "Centrex");
            }
        }

        private async Task LoadDepositadosAsync(bool resetPage)
        {
            try
            {
                if (resetPage)
                {
                    pagina_depositado = 1;
                    desde_depositado = 0;
                }

                using var ctx = new CentrexDbContext();

                var query = ctx.ChequeEntity
                    .AsNoTracking()
                    .Include(ch => ch.IdClienteNavigation)
                    .Include(ch => ch.IdProveedorNavigation)
                    .Include(ch => ch.IdBancoNavigation)
                    .Include(ch => ch.IdCuentaBancariaNavigation)
                    .Include(ch => ch.IdEstadochNavigation)
                    .Where(ch => ch.Activo && ch.IdEstadoch == ID_CH_DEPOSITADO);

                if (chk_desdeSiempre_depositado.Checked)
                {
                    var fechaDesde = DateOnly.FromDateTime(dtp_desde_depositado.Value.Date);
                    query = query.Where(ch => ch.FechaCobro.HasValue && ch.FechaCobro.Value >= fechaDesde);
                }

                if (chk_hastaSiempre_depositado.Checked)
                {
                    var fechaHasta = DateOnly.FromDateTime(dtp_hasta_depositado.Value.Date);
                    query = query.Where(ch => ch.FechaCobro.HasValue && ch.FechaCobro.Value <= fechaHasta);
                }

                if (cmb_cuentaBancaria.Enabled && cmb_cuentaBancaria.SelectedValue is int idCuenta)
                {
                    query = query.Where(ch => ch.IdCuentaBancaria == idCuenta);
                }

                if (int.TryParse(txt_nCH_depositado.Text, out var numeroCheque))
                {
                    query = query.Where(ch => ch.NCheque == numeroCheque);
                }

                if (decimal.TryParse(txt_importeCH_depositado.Text, NumberStyles.Number, CultureInfo.CurrentCulture, out var importeCheque))
                {
                    query = query.Where(ch => ch.Importe == importeCheque);
                }

                nRegs_depositado = await query.CountAsync();

                var pageSize = itXPage > 0 ? itXPage : 50;
                tPaginas_depositado = nRegs_depositado == 0 ? 1 : (int)Math.Ceiling(nRegs_depositado / (double)pageSize);

                if (pagina_depositado < 1)
                    pagina_depositado = 1;
                if (pagina_depositado > tPaginas_depositado)
                    pagina_depositado = tPaginas_depositado;

                desde_depositado = (pagina_depositado - 1) * pageSize;
                if (desde_depositado < 0)
                    desde_depositado = 0;

                var pagedQuery = query
                    .OrderBy(ch => ch.IdCheque)
                    .Skip(desde_depositado)
                    .Take(pageSize)
                    .Select(ch => new
                    {
                        ID = ch.IdCheque,
                        FechaIngreso = ch.FechaIngreso,
                        FechaEmision = ch.FechaEmision,
                        RecibidoDe = ch.IdClienteNavigation != null ? ch.IdClienteNavigation.RazonSocial : string.Empty,
                        EntregadoA = ch.IdProveedorNavigation != null ? ch.IdProveedorNavigation.RazonSocial : string.Empty,
                        BancoEmisor = ch.IdBancoNavigation.Nombre,
                        DepositadoEn = ch.IdCuentaBancariaNavigation != null ? ch.IdCuentaBancariaNavigation.Nombre : string.Empty,
                        NumeroCheque = ch.NCheque,
                        SegundoNumeroCheque = ch.NCheque2,
                        Monto = ch.Importe,
                        Estado = ch.IdEstadochNavigation.Estado,
                        FechaCobro = ch.FechaCobro,
                        FechaSalida = ch.FechaSalida,
                        FechaDeposito = ch.FechaDeposito
                    });

                var result = new DataGridQueryResult
                {
                    Query = pagedQuery,
                    ColumnasOcultar = new List<string> { "ID" },
                    EsMaterializada = false
                };

                await LoadDataGridDynamic.LoadDataGridWithEntityAsync(dg_view_chDepositados, result);

                if (dg_view_chDepositados.Columns.Contains("FechaIngreso"))
                    dg_view_chDepositados.Columns["FechaIngreso"].HeaderText = "F. Ingreso";
                if (dg_view_chDepositados.Columns.Contains("FechaEmision"))
                    dg_view_chDepositados.Columns["FechaEmision"].HeaderText = "F. Emisión";
                if (dg_view_chDepositados.Columns.Contains("RecibidoDe"))
                    dg_view_chDepositados.Columns["RecibidoDe"].HeaderText = "Recibido de";
                if (dg_view_chDepositados.Columns.Contains("EntregadoA"))
                    dg_view_chDepositados.Columns["EntregadoA"].HeaderText = "Entregado a";
                if (dg_view_chDepositados.Columns.Contains("BancoEmisor"))
                    dg_view_chDepositados.Columns["BancoEmisor"].HeaderText = "Banco emisor";
                if (dg_view_chDepositados.Columns.Contains("NumeroCheque"))
                    dg_view_chDepositados.Columns["NumeroCheque"].HeaderText = "Nº cheque";
                if (dg_view_chDepositados.Columns.Contains("SegundoNumeroCheque"))
                    dg_view_chDepositados.Columns["SegundoNumeroCheque"].HeaderText = "2º nº cheque";
                if (dg_view_chDepositados.Columns.Contains("Monto"))
                {
                    dg_view_chDepositados.Columns["Monto"].HeaderText = "$$";
                    dg_view_chDepositados.Columns["Monto"].DefaultCellStyle.Format = "N2";
                }
                if (dg_view_chDepositados.Columns.Contains("Estado"))
                    dg_view_chDepositados.Columns["Estado"].HeaderText = "Estado";
                if (dg_view_chDepositados.Columns.Contains("FechaCobro"))
                    dg_view_chDepositados.Columns["FechaCobro"].HeaderText = "Fecha de cobro";
                if (dg_view_chDepositados.Columns.Contains("FechaSalida"))
                    dg_view_chDepositados.Columns["FechaSalida"].HeaderText = "Fecha de salida";
                if (dg_view_chDepositados.Columns.Contains("FechaDeposito"))
                    dg_view_chDepositados.Columns["FechaDeposito"].HeaderText = "Fecha de depósito";

                txt_nPage_depositado.Text = nRegs_depositado == 0 ? "0 / 0" : $"{pagina_depositado} / {tPaginas_depositado}";
                dg_view_chDepositados.ClearSelection();
            }
            catch (Exception ex)
            {
                Interaction.MsgBox($"Error al cargar cheques depositados: {ex.Message}", MsgBoxStyle.Critical, "Centrex");
            }
        }

        private async void cmd_next_Click_cartera(object sender, EventArgs e)
        {
            if (pagina_cartera >= tPaginas_cartera)
                return;
            pagina_cartera += 1;
            await LoadCarteraAsync(resetPage: false);
        }

        private async void cmd_prev_Click_cartera(object sender, EventArgs e)
        {
            if (pagina_cartera == 1)
                return;
            pagina_cartera -= 1;
            await LoadCarteraAsync(resetPage: false);
        }

        private async void cmd_first_Click_cartera(object sender, EventArgs e)
        {
            pagina_cartera = 1;
            await LoadCarteraAsync(resetPage: true);
        }

        private async void cmd_last_Click_cartera(object sender, EventArgs e)
        {
            pagina_cartera = tPaginas_cartera;
            await LoadCarteraAsync(resetPage: false);
        }

        private async void cmd_go_Click_cartera(object sender, EventArgs e)
        {
            var partes = txt_nPage_cartera.Text.Split('/');
            if (partes.Length > 0 && int.TryParse(partes[0].Trim(), out var paginaSolicitada))
            {
                pagina_cartera = paginaSolicitada;
            }
            else if (int.TryParse(txt_nPage_cartera.Text.Trim(), out var paginaLibre))
            {
                pagina_cartera = paginaLibre;
            }
            else
            {
                pagina_cartera = 1;
            }

            if (pagina_cartera > tPaginas_cartera)
                pagina_cartera = tPaginas_cartera;
            if (pagina_cartera < 1)
                pagina_cartera = 1;
            await LoadCarteraAsync(resetPage: false);
        }

        private void txt_nPage_KeyDown_cartera(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmd_go_Click_cartera(null, null);
            }
        }

        private void txt_nPage_Click_cartera(object sender, EventArgs e)
        {
            txt_nPage_cartera.Text = "";
        }
        private async void cmd_next_Click_depositado(object sender, EventArgs e)
        {
            if (pagina_depositado >= tPaginas_depositado)
                return;
            pagina_depositado += 1;
            await LoadDepositadosAsync(resetPage: false);
        }

        private async void cmd_prev_Click_depositado(object sender, EventArgs e)
        {
            if (pagina_depositado == 1)
                return;
            pagina_depositado -= 1;
            await LoadDepositadosAsync(resetPage: false);
        }

        private async void cmd_first_Click_depositado(object sender, EventArgs e)
        {
            pagina_depositado = 1;
            await LoadDepositadosAsync(resetPage: true);
        }

        private async void cmd_last_Click_depositado(object sender, EventArgs e)
        {
            pagina_depositado = tPaginas_depositado;
            await LoadDepositadosAsync(resetPage: false);
        }

        private async void cmd_go_Click_depositado(object sender, EventArgs e)
        {
            var partes = txt_nPage_depositado.Text.Split('/');
            if (partes.Length > 0 && int.TryParse(partes[0].Trim(), out var paginaSolicitada))
            {
                pagina_depositado = paginaSolicitada;
            }
            else if (int.TryParse(txt_nPage_depositado.Text.Trim(), out var paginaLibre))
            {
                pagina_depositado = paginaLibre;
            }
            else
            {
                pagina_depositado = 1;
            }

            if (pagina_depositado > tPaginas_depositado)
                pagina_depositado = tPaginas_depositado;
            if (pagina_depositado < 1)
                pagina_depositado = 1;
            await LoadDepositadosAsync(resetPage: false);
        }

        private void txt_nPage_KeyDown_depositado(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmd_go_Click_depositado(null, null);
            }
        }

        private void txt_nPage_Click_depositado(object sender, EventArgs e)
        {
            txt_nPage_depositado.Text = "";
        }

        private async void txt_nCH_cartera_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                await LoadCarteraAsync(resetPage: true);
            }
        }

        private async void txt_importeCH_cartera_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                await LoadCarteraAsync(resetPage: true);
            }
        }

        private async void cmd_depositar_Click(object sender, EventArgs e)
        {
            int c;
            int sel = 0;
            bool hay_error = false;
            var ch = new ChequeEntity();

            var loopTo = dg_view_chCartera.Rows.Count - 1;
            for (c = 0; c <= loopTo; c++)
            {
                if (dg_view_chCartera.Rows[c].Selected)
                {
                    sel += 1;
                }
            }

            if (dg_view_chCartera.Rows.Count - 1 == 0)
            {
                Interaction.MsgBox("No hay cheques que pueda depositar", (MsgBoxStyle)((int)Constants.vbExclamation + (int)Constants.vbOKOnly), "Centrex");
                return;
            }
            else if (sel == 0)
            {
                Interaction.MsgBox("No ha seleccionado ningún cheque para depositar", (MsgBoxStyle)((int)Constants.vbExclamation + (int)Constants.vbOKOnly), "Centrex");
                return;
            }
            else if (cmb_banco.SelectedValue is null)
            {
                Interaction.MsgBox("No hay seleccionado un banco en el cual depositar el/los cheque(s)", (MsgBoxStyle)((int)Constants.vbExclamation + (int)Constants.vbOKOnly), "Centrex");
                return;
            }
            else if (cmb_cuentaBancaria.SelectedValue is null)
            {
                Interaction.MsgBox("No hay seleccionada una cuenta bancaria en la cual depositar el/los cheque(s)", (MsgBoxStyle)((int)Constants.vbExclamation + (int)Constants.vbOKOnly), "Centrex");
                return;
            }

            if (Interaction.MsgBox("¿Confirma depositar el/los cheque(s) seleccionados en la cuenta: " + cmb_cuentaBancaria.Text + " perteneciente al banco: " + cmb_banco.Text + "?", (MsgBoxStyle)((int)Constants.vbYesNo + (int)Constants.vbQuestion), "Centrex") == Constants.vbNo)
            {
                return;
            }

            var loopTo1 = dg_view_chCartera.Rows.Count - 1;
            for (c = 0; c <= loopTo1; c++)
            {
                if (!dg_view_chCartera.Rows[c].Selected)
                    continue;

                var idValor = dg_view_chCartera.Rows[c].Cells["ID"].Value;
                if (idValor is null)
                    continue;

                ch.IdCheque = Convert.ToInt32(idValor);
                ch.FechaDeposito = ConversorFechas.GetFecha(generales.Hoy(), ch.FechaDeposito);
                ch.IdCuentaBancaria = Convert.ToInt32(cmb_cuentaBancaria.SelectedValue);
                var nChequeValor = dg_view_chCartera.Rows[c].Cells["NumeroCheque"].Value;
                if (nChequeValor != null && int.TryParse(nChequeValor.ToString(), out var numCheque))
                    ch.NCheque = numCheque;
                else
                    ch.NCheque = 0;


                if (!cheques.Depositar_cheque(ch))
                {
                    Interaction.MsgBox("Hubo un problema al depositar el cheque con número: " + ch.NCheque + " en la cuenta bancaria: " + cmb_cuentaBancaria.Text + " perteneciente al banco: " + cmb_banco.Text, (MsgBoxStyle)((int)Constants.vbCritical + (int)Constants.vbOKOnly), "Centrex");
                    hay_error = true;
                }
            }


            if (!hay_error)
            {
                Interaction.MsgBox("Se han depositado correctamente los cheques.", (MsgBoxStyle)((int)Constants.vbInformation + (int)Constants.vbOKOnly), "Centrex");
            }
            else
            {
                Interaction.MsgBox("Verifique, no todos los cheques se depositaron correctamente, puede ser que no puedan ser depositados.", (MsgBoxStyle)((int)Constants.vbInformation + (int)Constants.vbOKOnly), "Centrex");
            }

            await LoadCarteraAsync(resetPage: true);
            await LoadDepositadosAsync(resetPage: true);
        }

        private async void cmb_banco_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cmb_banco.SelectedValue is null)
            {
                cmb_cuentaBancaria.DataSource = null;
                cmb_cuentaBancaria.Items.Clear();
                cmb_cuentaBancaria.Enabled = false;
                await LoadDepositadosAsync(resetPage: true);
                return;
            }

            var filtros = new Dictionary<string, object>
            {
                ["IdBanco"] = Convert.ToInt32(cmb_banco.SelectedValue)
            };
            var orden = new List<Tuple<string, bool>> { Tuple.Create("Nombre", true) };
            generales.Cargar_Combo(
                ref cmb_cuentaBancaria,
                entidad: "CuentaBancariaEntity",
                displaymember: "Nombre",
                valuemember: "IdCuentaBancaria",
                predet: -1,
                soloActivos: true,
                filtros: filtros,
                orden: orden);

            cmb_cuentaBancaria.SelectedIndex = -1;
            cmb_cuentaBancaria.Enabled = true;

            await LoadDepositadosAsync(resetPage: true);
        }

        private async void chk_desdeSiempre_cartera_CheckedChanged(object sender, EventArgs e)
        {
            dtp_desde_cartera.Enabled = chk_desdeSiempre_cartera.Checked;
            await LoadCarteraAsync(resetPage: true);
        }

        private async void chk_hastaSiempre_cartera_CheckedChanged(object sender, EventArgs e)
        {
            dtp_hasta_cartera.Enabled = chk_hastaSiempre_cartera.Checked;
            await LoadCarteraAsync(resetPage: true);
        }

        private async void chk_desdeSiempre_depositado_CheckedChanged(object sender, EventArgs e)
        {
            dtp_desde_depositado.Enabled = chk_desdeSiempre_depositado.Checked;
            await LoadDepositadosAsync(resetPage: true);
        }

        private async void chk_hastaSiempre_depositado_CheckedChanged(object sender, EventArgs e)
        {
            dtp_hasta_depositado.Enabled = chk_hastaSiempre_depositado.Checked;
            await LoadDepositadosAsync(resetPage: true);
        }

        private async void txt_nCH_depositado_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                await LoadDepositadosAsync(resetPage: true);
            }
        }

        private async void txt_importeCH_depositado_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                await LoadDepositadosAsync(resetPage: true);
            }
        }

        private void frm_depositarCH_FormClosed(object sender, FormClosedEventArgs e)
        {
            closeandupdate(this);
        }

        private async void cmb_cuentaBancaria_Leave(object sender, EventArgs e)
        {
            await LoadDepositadosAsync(resetPage: true);
        }

        private async void chk_desdeSiempre_depositado_Leave(object sender, EventArgs e)
        {
            await LoadDepositadosAsync(resetPage: true);
        }

        private async void dtp_desde_depositado_Leave(object sender, EventArgs e)
        {
            await LoadDepositadosAsync(resetPage: true);
        }

        private async void chk_hastaSiempre_depositado_Leave(object sender, EventArgs e)
        {
            await LoadDepositadosAsync(resetPage: true);
        }

        private async void dtp_hasta_depositado_Leave(object sender, EventArgs e)
        {
            await LoadDepositadosAsync(resetPage: true);
        }

        private async void txt_nCH_depositado_Leave(object sender, EventArgs e)
        {
            await LoadDepositadosAsync(resetPage: true);
        }

        private async void txt_importeCH_depositado_Leave(object sender, EventArgs e)
        {
            await LoadDepositadosAsync(resetPage: true);
        }

        private async void dtp_desde_cartera_Leave(object sender, EventArgs e)
        {
            await LoadCarteraAsync(resetPage: true);
        }

        private async void dtp_hasta_cartera_Leave(object sender, EventArgs e)
        {
            await LoadCarteraAsync(resetPage: true);
        }

        private async void txt_nCH_cartera_Leave(object sender, EventArgs e)
        {
            await LoadCarteraAsync(resetPage: true);
        }

        private async void txt_importeCH_cartera_Leave(object sender, EventArgs e)
        {
            await LoadCarteraAsync(resetPage: true);
        }

        private async void cmb_cuentaBancaria_SelectionChangeCommitted(object sender, EventArgs e)
        {
            await LoadDepositadosAsync(resetPage: true);
        }

        private async void cmd_anularDeposito_Click(object sender, EventArgs e)
        {
            int c;
            int sel = 0;
            bool hay_error = false;
            var ch = new ChequeEntity();

            var loopTo = dg_view_chDepositados.Rows.Count - 1;
            for (c = 0; c <= loopTo; c++)
            {
                if (dg_view_chDepositados.Rows[c].Selected)
                {
                    sel += 1;
                }
            }

            if (dg_view_chDepositados.Rows.Count - 1 == 0)
            {
                Interaction.MsgBox("No hay cheques que pueda anular el deposito.", (MsgBoxStyle)((int)Constants.vbExclamation + (int)Constants.vbOKOnly), "Centrex");
                return;
            }
            else if (sel == 0)
            {
                Interaction.MsgBox("No ha seleccionado ningún cheque para anular el deposito.", (MsgBoxStyle)((int)Constants.vbExclamation + (int)Constants.vbOKOnly), "Centrex");
                return;
            }

            if (Interaction.MsgBox("¿Confirma anular el deposito de el/los cheque(s) seleccionados?", (MsgBoxStyle)((int)Constants.vbYesNo + (int)Constants.vbQuestion), "Centrex") == Constants.vbNo)
            {
                return;
            }

            var loopTo1 = dg_view_chDepositados.Rows.Count - 1;
            for (c = 0; c <= loopTo1; c++)
            {
                if (!dg_view_chDepositados.Rows[c].Selected)
                    continue;

                var idValor = dg_view_chDepositados.Rows[c].Cells["ID"].Value;
                if (idValor is null)
                    continue;

                ch.IdCheque = Convert.ToInt32(idValor);
                var nChequeValor = dg_view_chCartera.Rows[c].Cells["NumeroCheque"].Value;
                if (nChequeValor != null && int.TryParse(nChequeValor.ToString(), out var numCheque))
                    ch.NCheque = numCheque;
                else
                    ch.NCheque = 0;

                if (!cheques.Anular_Deposito_Cheque(ch.IdCheque))
                {
                    Interaction.MsgBox("Hubo un problema al depositar el cheque con número: " + ch.NCheque + " en la cuenta bancaria: " + cmb_cuentaBancaria.Text + " perteneciente al banco: " + cmb_banco.Text, (MsgBoxStyle)((int)Constants.vbCritical + (int)Constants.vbOKOnly), "Centrex");
                    hay_error = true;
                }
            }


            if (!hay_error)
            {
                Interaction.MsgBox("Se ha(n) anulado el/los deposito(s) correctamente.", (MsgBoxStyle)((int)Constants.vbInformation + (int)Constants.vbOKOnly), "Centrex");
            }
            else
            {
                Interaction.MsgBox("Verifique, no se pudo anular el deposito de todos los cheques, posiblemente el deposito no pueda ser anulado.", (MsgBoxStyle)((int)Constants.vbInformation + (int)Constants.vbOKOnly), "Centrex");
            }

            await LoadCarteraAsync(resetPage: true);
            await LoadDepositadosAsync(resetPage: true);
        }

        private void cmd_filtrarCH_cartera_Click(object sender, EventArgs e)
        {

        }
    }
}
