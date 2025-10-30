using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace Centrex
{
    public partial class add_pago : Form
    {
        // ====== Estado interno ======
        private decimal total = 0;
        private decimal efectivo = 0;
        private decimal transferenciaBancaria = 0;
        private decimal totalCh = 0;
        private int[] chSel = null;
        private bool noCambiar = false;

        private const string selColName = "Seleccionado";

        private readonly CentrexDbContext ctx = new CentrexDbContext();

        public add_pago()
        {
            InitializeComponent();
        }

        // ====== Load ======
        private void add_pago_Load(object sender, EventArgs e)
        {
            try
            {
                // Proveedores (activos, orden por RazonSocial)
                generales.Cargar_Combo(
                    ref cmb_proveedor,
                    "ProveedorEntity",
                    "RazonSocial",
                    "IdProveedor",
                    predet: -1,
                    soloActivos: true
                );
                cmb_proveedor.Text = "Seleccione un proveedor...";
                cmb_cc.Enabled = false;

                ResetForm();

                // Fecha de hoy (label o DateTimePicker de tu UI)
                lbl_fecha.Text = generales.Hoy();

                // Grillas
                ActualizarGridCheques();
                CargarDGTransferencias();
            }
            catch (Exception ex)
            {
                Interaction.MsgBox($"Error en carga de formulario: {ex.Message}", MsgBoxStyle.Critical, "Centrex");
            }
        }

        private void add_pago_FormClosed(object sender, FormClosedEventArgs e)
        {
            generales.closeandupdate(this);
        }

        // ====== Proveedor seleccionado -> cargar CC proveedor ======
        private void cmb_proveedor_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                if (cmb_proveedor.SelectedValue is null) return;
                int idProv = Conversions.ToInteger(cmb_proveedor.SelectedValue);

                // Cuentas corrientes del proveedor
                generales.Cargar_Combo(
                    ref cmb_cc,
                    "CcProveedorEntity",
                    "Nombre",
                    "IdCc",
                    predet: -1,
                    soloActivos: true,
                    filtros: new System.Collections.Generic.Dictionary<string, object>
                    {
                        { "IdProveedor", idProv }
                    }
                );
                cmb_cc.Enabled = true;

                // Habilitar UI relacionada
                chk_efectivo.Enabled = true;
                chk_transferencia.Enabled = true;
                chk_cheque.Enabled = true;
                dg_view_nFC_importes.Enabled = true;
                txt_notas.Enabled = true;

                // Refrescar grillas y totales
                ActualizarGridCheques();
                ResetForm(); // reinicia totales al cambiar de proveedor
            }
            catch (Exception ex)
            {
                Interaction.MsgBox($"Error al seleccionar proveedor: {ex.Message}", MsgBoxStyle.Critical, "Centrex");
            }
        }

        private void cmb_cc_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                if (cmb_cc.SelectedValue is null)
                    return;

                // Obtener info de la cuenta corriente del proveedor
                var cc = info_ccProveedor(Conversions.ToInteger(cmb_cc.SelectedValue));

                // Actualizar texto y color del saldo
                lbl_dineroCuenta.Text = "$ " + cc.Saldo.ToString("N2");

                if (cc.Saldo < 0)
                    lbl_dineroCuenta.ForeColor = Color.Red;
                else
                    lbl_dineroCuenta.ForeColor = Color.Green;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al seleccionar la cuenta corriente: {ex.Message}",
                    "Centrex", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void cmb_proveedor_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Evitar escritura libre si querés forzar selección
            // e.Handled = true;
        }

        private void cmb_cc_KeyPress(object sender, KeyPressEventArgs e)
        {
            // e.Handled = true;
        }

        // ====== Cambios en check de EFECTIVO ======
        private void chk_efectivo_CheckedChanged(object sender, EventArgs e)
        {
            if (decimal.TryParse(txt_efectivo.Text, NumberStyles.Number, CultureInfo.CurrentCulture, out var val))
            {
                if (val != 0)
                    efectivo = val;
            }

            if (chk_efectivo.Checked)
                total += efectivo;
            else
                total -= efectivo;

            txt_efectivo.Enabled = chk_efectivo.Checked;
            lbl_importePago.Text = total.ToString("N2");
        }

        private void txt_efectivo_Leave(object sender, EventArgs e)
        {
            total -= efectivo;
            if (!decimal.TryParse(txt_efectivo.Text, NumberStyles.Number, CultureInfo.CurrentCulture, out efectivo))
                efectivo = 0;
            total += efectivo;
            lbl_importePago.Text = generales.Precio((decimal)total);
        }

        // ====== cheque ======
        private void chk_cheque_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = chk_cheque.Checked;

            if (chk)
            {
                total += totalCh;
                lbl_totalCh.Text = generales.Precio((decimal)totalCh);
                lbl_importePago.Text = generales.Precio((decimal)total);
            }
            else
            {
                total -= totalCh;
                lbl_totalCh.Text = generales.Precio(0);
                lbl_importePago.Text = generales.Precio((decimal)total);
            }

            dg_viewCH.Enabled = chk;
            cmd_addCheques.Enabled = chk;
            cmd_verCheques.Enabled = chk;
            txt_search.Enabled = chk;
            lbl_borrarbusqueda.Enabled = chk;
        }

        // ====== transferencia ======
        private void chk_transferencia_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_transferencia.Checked)
            {
                total += transferenciaBancaria;
                lbl_totalTransferencia.Text = generales.Precio((decimal)transferenciaBancaria);
            }
            else
            {
                total -= transferenciaBancaria;
                lbl_totalTransferencia.Text = generales.Precio(0);
            }

            lbl_importePago.Text = generales.Precio((decimal)total);

            txt_searchTransferencia.Enabled = chk_transferencia.Checked;
            dg_viewTransferencia.Enabled = chk_transferencia.Checked;
            cmd_addTransferencia.Enabled = chk_transferencia.Checked;
        }

        private void lbl_borrarbusqueda_DoubleClick(object sender, EventArgs e)
        {
            txt_search.Text = "";
            ActualizarGridCheques();
        }

        private void txt_search_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                ActualizarGridCheques();
                e.Handled = true;
            }
        }

        // ==========================================================
        // GRILLA: Cheques en cartera (IdEstadoch = ID_CH_CARTERA, Activo=1)
        // ==========================================================
        private void ActualizarGridCheques()
        {
            try
            {
                using var ctx = new CentrexDbContext();

                // ==========================================================
                // 1️⃣ Construir la query base (equivalente al SQL original)
                // ==========================================================
                var query = ctx.ChequeEntity
                    .Include(ch => ch.IdClienteNavigation)
                    .Include(ch => ch.IdProveedorNavigation)
                    .Include(ch => ch.IdBancoNavigation)
                    .Include(ch => ch.IdCuentaBancariaNavigation)
                        .ThenInclude(cb => cb.IdBancoNavigation)
                    .Include(ch => ch.IdEstadochNavigation)
                    .Where(ch => ch.Activo == true && ch.IdEstadoch == VariablesGlobales.ID_CH_CARTERA);

                // ==========================================================
                // 2️⃣ Filtro textual si hay búsqueda
                // ==========================================================
                var raw = txt_search.Text?.Trim();
                if (!string.IsNullOrWhiteSpace(raw))
                {
                    var parts = raw.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                    foreach (var p in parts)
                    {
                        var pat = $"%{p}%";

                        query = query.Where(ch =>
                            // Cliente
                            (ch.IdClienteNavigation != null &&
                             EF.Functions.Like(ch.IdClienteNavigation.RazonSocial ?? "", pat)) ||

                            // Proveedor
                            (ch.IdProveedorNavigation != null &&
                             EF.Functions.Like(ch.IdProveedorNavigation.RazonSocial ?? "", pat)) ||

                            // Banco directo
                            EF.Functions.Like(ch.IdBancoNavigation.Nombre ?? "", pat) ||

                            // Banco asociado a cuenta bancaria
                            (ch.IdCuentaBancariaNavigation != null &&
                             ch.IdCuentaBancariaNavigation.IdBancoNavigation != null &&
                             EF.Functions.Like(ch.IdCuentaBancariaNavigation.IdBancoNavigation.Nombre ?? "", pat)) ||

                            // Estado del cheque
                            EF.Functions.Like(ch.IdEstadochNavigation.Estado ?? "", pat) ||

                            // Número de cheque
                            //(ch.NCheque.ToString() != null && EF.Functions.Like(ch.NCheque.ToString(), pat)) ||
                            EF.Functions.Like("" + ch.NCheque, pat) ||

                            // Importe (decimal) convertido a texto para búsqueda textual
                            EF.Functions.Like("" + ch.Importe, pat)
                        );
                    }
                }

                // ==========================================================
                // 3️⃣ Empaquetar en DataGridQueryResult
                // ==========================================================
                var result = new DataGridQueryResult
                {
                    Query = query.AsNoTracking(),
                    GridName = "cheques",   // nombre interno de la configuración de grilla
                    EsMaterializada = false // se deja a LoadDataGridDynamic para el ToList()
                };

                // ==========================================================
                // 4️⃣ Cargar grilla con la función centralizada
                // ==========================================================
                LoadDataGridDynamic.LoadDataGridWithEntityAsync(dg_viewCH, result).Wait();

                // ==========================================================
                // 5️⃣ Re-aplicar selección previa y recalcular totales
                // ==========================================================
                SelCheques();
                lbl_importePago.Text = generales.Precio((decimal)total);
            }
            catch (Exception ex)
            {
                Interaction.MsgBox($"Error al actualizar la grilla de cheques: {ex.Message}", MsgBoxStyle.Critical, "Centrex");
            }
        }

        private void dg_viewCH_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dg_viewCH.IsCurrentCellDirty)
            {
                dg_viewCH.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void dg_viewCH_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (noCambiar)
                    return;

                // 🔹 Reinicio parcial de totales
                total -= totalCh;
                totalCh = 0;

                // 🔹 Recalcular cheques seleccionados
                CheckCheques();

                if (chSel is not null && chSel.Length > 0)
                {
                    using (var ctx = new CentrexDbContext())
                    {
                        foreach (var idc in chSel)
                        {
                            var ch = ctx.ChequeEntity.AsNoTracking()
                                .FirstOrDefault(x => x.IdCheque == idc);

                            if (ch != null)
                                totalCh += ch.Importe;
                        }
                    }
                }

                // 🔹 Recalcular total general
                total += totalCh;

                // 🔹 Mostrar totales formateados
                lbl_totalCh.Text = generales.Precio((decimal)totalCh);
                lbl_importePago.Text = generales.Precio((decimal)total);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al recalcular totales de cheques: {ex.Message}",
                    "Centrex", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void dg_viewCH_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dg_viewCH.CurrentRow is null) return;

            string seleccionado = dg_viewCH.CurrentRow.Cells["ID"].Value?.ToString();
            if (string.IsNullOrEmpty(seleccionado)) return;

            VariablesGlobales.edita_cheque = cccheque.info_cheque(seleccionado);
            if (!VariablesGlobales.borrado) VariablesGlobales.edicion = true;

            var frm = new add_cheque();
            frm.ShowDialog();

            ActualizarGridCheques();
            VariablesGlobales.edicion = false;
        }

        // ====== Helpers selección cheques ======
        private void CheckCheques()
        {
            // Recorre la grilla y actualiza chSel
            foreach (DataGridViewRow row in dg_viewCH.Rows)
            {
                if (row.Cells[selColName] is DataGridViewCheckBoxCell)
                {
                    bool seleccionado = Convert.ToBoolean(row.Cells[selColName].Value ?? false);
                    int id = Conversions.ToInteger(row.Cells["ID"].Value);

                    if (seleccionado)
                        chSel = Centrex.Funciones.generales.addValArray(ref chSel, id);
                    else if (chSel is not null)
                        chSel = Centrex.Funciones.generales.delValArray(ref chSel, id);
                }
            }
        }

        private void SelCheques()
        {
            if (chSel is null || dg_viewCH.Rows.Count == 0) return;

            noCambiar = true;
            foreach (DataGridViewRow row in dg_viewCH.Rows)
            {
                int id = Conversions.ToInteger(row.Cells["ID"].Value);
                if (Centrex.Funciones.generales.searchArray(chSel, id))
                {
                    row.Cells[selColName].Value = true;
                }
            }
            noCambiar = false;
        }

        // ==========================================================
        // TRANSFERENCIAS (tmptransferencias)
        // ==========================================================
        private void CargarDGTransferencias()
        {
            try
            {
                var transf = ctx.TmpTransferenciaEntity
                    .Include(t => t.IdCuentaBancariaNavigation)
                        .ThenInclude(cb => cb.IdBancoNavigation)
                    .AsNoTracking()
                    .Select(t => new
                    {
                        ID = t.IdTmpTransferencia,
                        Banco = t.IdCuentaBancariaNavigation != null && t.IdCuentaBancariaNavigation.IdBancoNavigation != null
                                ? t.IdCuentaBancariaNavigation.IdBancoNavigation.Nombre
                                : "(Banco)",
                        CuentaBancaria = t.IdCuentaBancariaNavigation != null ? t.IdCuentaBancariaNavigation.Nombre : "(Cuenta)",
                        Fecha = t.Fecha, // DateOnly/DateTime según tu entidad
                        Total = t.Total,
                        Notas = t.Notas
                    })
                    .ToList();

                dg_viewTransferencia.AutoGenerateColumns = true;
                dg_viewTransferencia.DataSource = transf;

                if (dg_viewTransferencia.Columns.Contains("Total"))
                {
                    dg_viewTransferencia.Columns["Total"].DefaultCellStyle.Format = "N2";
                }
            }
            catch (Exception ex)
            {
                Interaction.MsgBox($"Error al cargar transferencias: {ex.Message}", MsgBoxStyle.Critical, "Centrex");
            }
        }

        private decimal SumaTransferencias()
        {
            decimal suma = 0;
            foreach (DataGridViewRow row in dg_viewTransferencia.Rows)
            {
                if (row.Cells["Total"]?.Value is null) continue;
                if (decimal.TryParse(row.Cells["Total"].Value.ToString(), NumberStyles.Number, CultureInfo.CurrentCulture, out var v))
                    suma += v;
            }
            return suma;
        }

        private void ActualizaTransferencias()
        {
            CargarDGTransferencias();

            total -= transferenciaBancaria;
            transferenciaBancaria = SumaTransferencias();
            total += transferenciaBancaria;

            lbl_totalTransferencia.Text = generales.Precio((decimal)transferenciaBancaria);
            lbl_importePago.Text = generales.Precio((decimal)total);
        }

        private void dg_viewTransferencia_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                // 🔹 Si no hay fila seleccionada, no hace nada
                if (dg_viewTransferencia.CurrentRow == null)
                    return;

                // 🔹 Obtener ID seleccionado
                string seleccionado = dg_viewTransferencia.CurrentRow.Cells["ID"].Value?.ToString();
                if (string.IsNullOrEmpty(seleccionado))
                    return;

                // 🔹 Buscar transferencia temporal por ID
                //VariablesGlobales.edita_transferencia = cctransferencia.InfoTmpTransferencia(seleccionado);

                if (VariablesGlobales.edita_transferencia == null || VariablesGlobales.edita_transferencia.IdTransferencia == -1)
                {
                    MessageBox.Show("Ocurrió un problema al editar la transferencia.", "Centrex",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                // 🔹 Marcar modo edición
                if (!VariablesGlobales.borrado)
                    VariablesGlobales.edicion = true;

                // 🔹 Abrir formulario de edición
                using (var frm = new add_transferencia())
                {
                    frm.ShowDialog();
                }

                // 🔹 Refrescar lista de transferencias
                ActualizaTransferencias();

                // 🔹 Restaurar estado global
                VariablesGlobales.edicion = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al abrir la transferencia: {ex.Message}", "Centrex",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void cmd_addTransferencia_Click(object sender, EventArgs e)
        {
            var frm = new add_transferencia();
            frm.ShowDialog();
            ActualizaTransferencias();
        }

        // ==========================================================
        // BOTONES / ACCIONES
        // ==========================================================
        private void pic_searchProveedor_Click(object sender, EventArgs e)
        {
            string tmp = VariablesGlobales.tabla;
            VariablesGlobales.tabla = "proveedores";

            Enabled = false;
            var s = new search();
            s.ShowDialog();
            Enabled = true;

            VariablesGlobales.tabla = tmp;

            if (VariablesGlobales.id == 0) VariablesGlobales.id = VariablesGlobales.id_cliente_pedido_default;
            generales.updateform(VariablesGlobales.id.ToString(), ref cmb_proveedor);
        }

        private void pic_searchCCProveedor_Click(object sender, EventArgs e)
        {
            if (cmb_proveedor.SelectedValue is null || cmb_proveedor.SelectedIndex == -1) return;

            string tmp = VariablesGlobales.tabla;
            var tmpProv = VariablesGlobales.edita_proveedor;

            VariablesGlobales.edita_proveedor = info_proveedor(Conversions.ToInteger(cmb_proveedor.SelectedValue));
            VariablesGlobales.tabla = "cc_proveedores";

            Enabled = false;
            var s = new search();
            s.ShowDialog();
            Enabled = true;

            VariablesGlobales.tabla = tmp;
            VariablesGlobales.edita_proveedor = tmpProv;

            if (VariablesGlobales.id == 0) VariablesGlobales.id = VariablesGlobales.id_cliente_pedido_default;
            generales.updateform(VariablesGlobales.id.ToString(), ref cmb_cc);
        }

        private void cmd_verCheques_Click(object sender, EventArgs e)
        {
            if (cmb_proveedor.SelectedValue is null) return;
            var frm = new frmCheques(Conversions.ToInteger(cmb_proveedor.SelectedValue));
            frm.ShowDialog();
        }

        private void cmd_addCheques_Click(object sender, EventArgs e)
        {
            if (cmb_proveedor.SelectedValue is null) return;
            var addCheque = new add_cheque(-1, Conversions.ToInteger(cmb_proveedor.SelectedValue));
            addCheque.ShowDialog();
            ActualizarGridCheques();
        }

        private void cmd_ok_Click(object sender, EventArgs e)
        {
            try
            {
                // === Validaciones básicas ===
                if (cmb_proveedor.SelectedIndex == -1 || string.IsNullOrWhiteSpace(cmb_proveedor.Text))
                {
                    MessageBox.Show("El campo 'Proveedor' es obligatorio.", "Centrex",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                if (!chk_efectivo.Checked && !chk_transferencia.Checked && !chk_cheque.Checked)
                {
                    MessageBox.Show("Debe elegir algún medio de pago.", "Centrex",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                // === Recalcular totales ===
                total -= transferenciaBancaria;
                transferenciaBancaria = SumaTransferencias();
                total += transferenciaBancaria;

                total -= totalCh;
                totalCh = 0;

                CheckCheques();

                if (chSel != null && chSel.Length > 0)
                {
                    using (var ctx = new CentrexDbContext())
                    {
                        foreach (var idc in chSel)
                        {
                            var ch = ctx.ChequeEntity.AsNoTracking().FirstOrDefault(x => x.IdCheque == idc);
                            if (ch != null)
                                totalCh += ch.Importe;
                        }
                    }
                }

                total += totalCh;

                // === Mostrar totales actualizados ===
                lbl_totalCh.Text = generales.Precio((decimal)totalCh);
                lbl_totalTransferencia.Text = generales.Precio((decimal)transferenciaBancaria);
                lbl_importePago.Text = generales.Precio((decimal)total);

                // === Guardado pendiente ===
                MessageBox.Show("Validación OK. Integra aquí el guardado de Pago, Cheques y Transferencias (EF o tus funciones existentes).",
                    "Centrex", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al procesar el pago: {ex.Message}", "Centrex",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void cmd_exit_Click(object sender, EventArgs e)
        {
            generales.closeandupdate(this);
        }

        private void dg_view_nFC_importes_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dg_view_nFC_importes.CurrentCell?.ColumnIndex == 2 && e.Control is TextBox tb)
            {
                tb.KeyPress -= dg_view_nFC_importes_textbox_keyPress;
                tb.KeyPress += dg_view_nFC_importes_textbox_keyPress;
            }
        }

        private void dg_view_nFC_importes_textbox_keyPress(object sender, KeyPressEventArgs e)
        {
            // Sólo números + un punto decimal
            var txt = sender as TextBox;
            if (txt == null) return;

            var ch = e.KeyChar;
            if (!char.IsControl(ch) && !char.IsDigit(ch) && ch != '.')
            {
                e.Handled = true;
                return;
            }

            if (ch == '.' && txt.Text.Contains("."))
            {
                e.Handled = true;
                return;
            }
        }

        // ====== Reset formulario ======
        private void ResetForm()
        {
            total = 0;
            efectivo = 0;
            transferenciaBancaria = 0;
            totalCh = 0;
            chSel = null;
            noCambiar = false;

            chk_efectivo.Checked = false;
            chk_transferencia.Checked = false;
            chk_cheque.Checked = false;

            txt_efectivo.Text = "0";
            lbl_totalTransferencia.Text = generales.Precio(0);
            txt_search.Text = "";
            lbl_totalCh.Text = generales.Precio(0);
            lbl_importePago.Text = generales.Precio(0);

            CargarDGTransferencias();
        }

        // ===================================================================
        // EVENTOS FALTANTES DEL DESIGNER
        // ===================================================================

        private void cheques_Click(object sender, EventArgs e)
        {
            // Al hacer clic en la pestaña de cheques, actualizamos su listado
            try
            {
                //actualizarDataGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al actualizar cheques: {ex.Message}", "Centrex", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    }
}
