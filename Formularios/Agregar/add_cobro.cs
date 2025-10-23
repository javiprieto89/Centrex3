using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using static Centrex.generales;

namespace Centrex
{

    public partial class add_cobro : Form
    {

        // ==============================
        // === VARIABLES INTERNAS =======
        // ==============================
        private CobroEntity c;
        private CcClienteEntity cc;
        private string selColName = "Seleccionado";
        private double Total = 0d;
        private double Efectivo = 0d;
        private double transferenciaBancaria = 0d;
        private double totalRetenciones = 0d;
        private double TotalCh = 0d;
        private int[] chSel;
        private bool noCambiar = false;

        public add_cobro()
        {
            InitializeComponent();
        }

        // ==============================
        // === EVENTOS DE FORMULARIO ====
        // ==============================

        private void add_cobro_Load(object sender, EventArgs e)
        {
            try
            {
                // Limpieza de temporales EF
                using (var ctx = new CentrexDbContext())
                {
                    ctx.Database.ExecuteSqlCommand("CobrosRetenciones", new { IdCobro = (int?)null });
                    ctx.Database.ExecuteSqlCommand("Transferencias", new { IdCobro = (int?)null });
                    ctx.Database.ExecuteSqlCommand("Cheques", new { IdCobro = (int?)null, IdEstadoCH = 1 });
                }

                // Cargar combo clientes
                using (var ctx = new CentrexDbContext())
                {
                    var clientes = ctx.ClienteEntity.Where(x => x.Activo == true).OrderBy(x => x.RazonSocial).ToList();

                    cmb_cliente.DataSource = clientes;
                    cmb_cliente.DisplayMember = "RazonSocial";
                    cmb_cliente.ValueMember = "IdCliente";
                }

                cmb_cliente.Text = "Seleccione un cliente";
                cmb_cc.Enabled = false;
                resetForm();
                lbl_fechaCarga.Text = DateTime.Now.ToShortDateString();
            }
            catch (Exception ex)
            {
                Interaction.MsgBox("Error al inicializar el formulario: " + ex.Message, Constants.vbCritical, "Centrex");
            }
        }

        private void add_cobro_FormClosed(object sender, FormClosedEventArgs e)
        {
            closeandupdate(this);
        }

        // ==============================
        // === SELECCIÓN DE CLIENTE =====
        // ==============================

        private void cmb_cliente_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                int clienteId = Conversions.ToInteger(cmb_cliente.SelectedValue);
                using (var ctx = new CentrexDbContext())
                {
                    var cuentas = ctx.CcClienteEntity.Where(x => x.activo == true & x.IdCliente == clienteId).OrderBy(x => x.Nombre).ToList();

                    cmb_cc.DataSource = cuentas;
                    cmb_cc.DisplayMember = "Nombre";
                    cmb_cc.ValueMember = "IdCC";
                }

                cmb_cc.Text = "Seleccione una cuenta corriente...";
                cmb_cc.Enabled = true;
                ActiveControl = cmb_cc;

                chk_cobro_no_oficial.Enabled = true;
                chk_efectivo.Enabled = true;
                chk_transferencia.Enabled = true;
                chk_cheque.Enabled = true;
                txt_notas.Enabled = true;
                chk_retenciones.Enabled = true;
                dg_view_nFC_importes.Enabled = true;

                actualizarDataGrid(clienteId);
                resetForm();
            }
            catch (Exception ex)
            {
                Interaction.MsgBox("Error al cargar cuentas del cliente: " + ex.Message, Constants.vbCritical, "Centrex");
            }
        }

        private void cmb_cc_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                using (var ctx = new CentrexDbContext())
                {
                    cc = ctx.CcClienteEntity.FirstOrDefault(x => x.IdCc == Conversions.ToInteger(cmb_cc.SelectedValue));
                }
                lbl_saldo.Text = "$ " + cc.Saldo.ToString();
                lbl_importePago.Text = Strings.FormatCurrency(Total);
                lbl_saldo.ForeColor = cc.Saldo < 0m ? Color.Red : Color.Green;
            }
            catch (Exception ex)
            {
                Interaction.MsgBox("Error al cargar saldo de cuenta corriente: " + ex.Message, Constants.vbCritical, "Centrex");
            }
        }

        // ==============================
        // === CHECKBOXES DE PAGO =======
        // ==============================

        private void chk_efectivo_CheckedChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txt_efectivo.Text))
                Efectivo = Conversion.Val(txt_efectivo.Text);
            if (chk_efectivo.Checked)
            {
                Total += Efectivo;
            }
            else
            {
                Total -= Efectivo;
            }
            txt_efectivo.Enabled = chk_efectivo.Checked;
            lbl_importePago.Text = Strings.FormatCurrency(Total);
        }

        private void chk_cheque_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = chk_cheque.Checked;
            if (chk)
            {
                Total += TotalCh;
                lbl_totalCh.Text = Strings.FormatCurrency(TotalCh);
            }
            else
            {
                Total -= TotalCh;
                lbl_totalCh.Text = Strings.FormatCurrency(0);
            }
            lbl_importePago.Text = Strings.FormatCurrency(Total);
            dg_viewCH.Enabled = chk;
            cmd_addCheques.Enabled = chk;
            cmd_verCheques.Enabled = chk;
            txt_searchCH.Enabled = chk;
            lbl_borrarbusquedaCH.Enabled = chk;
        }

        private void chk_transferencia_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = chk_transferencia.Checked;
            if (chk)
            {
                Total += transferenciaBancaria;
                lbl_totalTransferencia.Text = Strings.FormatCurrency(transferenciaBancaria);
            }
            else
            {
                Total -= transferenciaBancaria;
                lbl_totalTransferencia.Text = Strings.FormatCurrency(0);
            }
            lbl_importePago.Text = Strings.FormatCurrency(Total);
            txt_searchTransferencia.Enabled = chk;
            dg_viewTransferencia.Enabled = chk;
            cmd_addTransferencia.Enabled = chk;
        }

        private void chk_retenciones_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = chk_retenciones.Checked;
            if (chk)
            {
                Total += totalRetenciones;
                lbl_totalRetencion.Text = Strings.FormatCurrency(totalRetenciones);
            }
            else
            {
                Total -= totalRetenciones;
                lbl_totalRetencion.Text = Strings.FormatCurrency(0);
            }
            lbl_importePago.Text = Strings.FormatCurrency(Total);
            txt_searchRetencion.Enabled = chk;
            dg_viewRetencion.Enabled = chk;
            cmd_addRetencion.Enabled = chk;
        }

        // ==============================
        // === BOTÓN GUARDAR COBRO =====
        // ==============================

        private void cmd_ok_Click(object sender, EventArgs e)
        {
            try
            {
                var cb = new CobroEntity();
                double sumaFC = 0d;

                if (cmb_cliente.Text == "Seleccione un cliente")
                {
                    Interaction.MsgBox("Debe seleccionar un cliente válido", Constants.vbExclamation, "Centrex");
                    return;
                }

                DialogResult confirm = (DialogResult)Interaction.MsgBox("Una vez dado de alta un cobro, no podrá modificarse. ¿Desea continuar?", (MsgBoxStyle)((int)Constants.vbYesNo + (int)Constants.vbQuestion), "Centrex");
                if ((int)confirm == (int)Constants.vbNo)
                    return;

                if (chk_cobro_no_oficial.Checked)
                {
                    cb.IdCobroOficial = -1;
                    cb.IdCobroNoOficial = cobros.Ultimo_cobro_no_oficial();
                }
                else
                {
                    cb.IdCobroOficial = cobros.Ultimo_cobro_oficial();
                    cb.IdCobroNoOficial = -1;
                }
                cb.FechaCarga = DateTime.Now;
                cb.FechaCobro = dtp_fechaCobro.Value.Date;
                cb.IdCliente = cmb_cliente.SelectedValue;
                cb.IdCc = cmb_cc.SelectedValue;
                cb.efectivo = (decimal)(chk_efectivo.Checked ? Conversion.Val(txt_efectivo.Text) : 0d);
                cb.hayCheque = chk_cheque.Checked;
                cb.hayTransferencia = chk_transferencia.Checked;
                cb.hayRetencion = chk_retenciones.Checked;
                cb.totalCh = (decimal)(chk_cheque.Checked ? sumaCheques() : 0d);
                cb.totalTransferencia = (decimal)(chk_transferencia.Checked ? sumaTransferencias() : 0d);
                cb.totalRetencion = (decimal)(chk_retenciones.Checked ? sumaRetenciones() : 0d);
                cb.total = (decimal)Total;
                cb.notas = txt_notas.Text;
                cb.firmante = txt_firmante.Text;

                cb.IdCobro = addcobro(cb);
                if (cb.IdCobro > 0)
                {
                    if (cb.hayCheque)
                        cobros.add_chequeCobrado(cb.IdCobro, obtenerChequesSeleccionados());
                    if (cb.hayTransferencia)
                        guardarTransferencias_EF(cb);
                    if (cb.hayRetencion)
                        guardarCobroRetencion_EF(cb);
                    cobros.guardar_cobros_facturas_importes(cb.IdCobro, dg_view_nFC_importes);
                    cc.saldo = (decimal)(cc.saldo - Total);
                    updateCCCliente_EF(cc);
                    Interaction.MsgBox("Cobro registrado correctamente", Constants.vbInformation, "Centrex");
                    closeandupdate(this);
                }
                else
                {
                    Interaction.MsgBox("Error al guardar el cobro", Constants.vbCritical, "Centrex");
                }
            }
            catch (Exception ex)
            {
                Interaction.MsgBox("Error al procesar cobro: " + ex.Message, Constants.vbCritical, "Centrex");
            }
        }

        // ==============================
        // === FUNCIONES DE APOYO =======
        // ==============================

        private int[] obtenerChequesSeleccionados()
        {
            var ids = new List<int>();
            foreach (DataGridViewRow row in dg_viewCH.Rows)
            {
                if (Convert.ToBoolean(row.Cells[selColName].Value))
                {
                    ids.Add(Convert.ToInt32(row.Cells["ID"].Value));
                }
            }
            return ids.ToArray();
        }

        private double sumaCheques()
        {
            double suma = 0d;
            foreach (DataGridViewRow row in dg_viewCH.Rows)
            {
                if (Convert.ToBoolean(row.Cells[selColName].Value))
                {
                    suma += Convert.ToDouble(row.Cells["Importe"].Value);
                }
            }
            return suma;
        }

        private double sumaTransferencias()
        {
            double suma = 0d;
            foreach (DataGridViewRow row in dg_viewTransferencia.Rows)
                suma += Convert.ToDouble(row.Cells["Total"].Value);
            return suma;
        }

        private double sumaRetenciones()
        {
            double suma = 0d;
            foreach (DataGridViewRow row in dg_viewRetencion.Rows)
                suma += Convert.ToDouble(row.Cells["Total"].Value);
            return suma;
        }

        // ==============================
        // === ENTITY FRAMEWORK (EF) ====
        // ==============================

        private int addcobro(CobroEntity cb)
        {
            using (var ctx = new CentrexDbContext())
            {
                ctx.Cobros.Add(cb);
                ctx.SaveChanges();
                return cb.IdCobro;
            }
        }

        private void guardarTransferencias_EF(CobroEntity cb)
        {
            using (var ctx = new CentrexDbContext())
            {
                var transferencias = ctx.Transferencias.Where(t => t.IdCobro is null).ToList();
                foreach (var t in transferencias)
                {
                    ((dynamic)t).IdCobro = cb.IdCobro;
                    ctx.Entry(t).State = EntityState.Modified;
                }
                ctx.SaveChanges();
            }
        }

        private void guardarCobroRetencion_EF(CobroEntity cb)
        {
            using (var ctx = new CentrexDbContext())
            {
                // Si IdCobro es Integer (no nullable):
                var retenciones = ctx.CobrosRetenciones.Where(r => r.IdCobro == 0).ToList();

                // Si IdCobro es Nullable(Of Integer), usar esta en cambio:
                // Dim retenciones = ctx.CobrosRetenciones.Where(Function(r) r.IdCobro Is Nothing).ToList()

                foreach (var r in retenciones)
                {
                    ((dynamic)r).IdCobro = cb.IdCobro;
                    ctx.Entry(r).State = EntityState.Modified;
                }
                ctx.SaveChanges();
            }
        }

        private void updateCCCliente_EF(CcClienteEntity cc)
        {
            using (var ctx = new CentrexDbContext())
            {
                ctx.Entry(cc).State = EntityState.Modified;
                ctx.SaveChanges();
            }
        }

        // ==============================
        // === REFRESCO DE GRILLAS ======
        // ==============================

        private void cargarDGTransferencias()
        {
            using (var ctx = new CentrexDbContext())
            {
                var transferencias = ctx.Transferencias.Where(t => t.IdCobro is null).Include(t => t.CuentaBancaria.Banco).Select(t => new
                {
                    ID = t.IdTransferencia,
                    Banco = t.CuentaBancaria.Banco.Nombre,
                    CuentaBancaria = t.CuentaBancaria.IdCuentaBancaria,
                    t.Fecha,
                    t.Total,
                    t.Notas
                }).ToList();
                dg_viewTransferencia.DataSource = transferencias;
            }
        }

        private void cargarDGRetenciones()
        {
            using (var ctx = new CentrexDbContext())
            {
                var retenciones = ctx.CobrosRetenciones.Where(r => r.IdCobro == 0).Select(r => new
                {
                    ID = r.IdRetencion,
                    Retencion = (from imp in ctx.Impuestos
                                 where imp.IdImpuesto == r.IdImpuesto
                                 select imp.nombre).FirstOrDefault(),
                    r.Total,
                    r.Notas
                }).ToList();

                dg_viewRetencion.DataSource = retenciones;
            }
        }

        private void actualizarDataGrid(int idCliente)
        {
            try
            {
                // Limpieza y preparación
                dg_viewCH.DataSource = null;
                lbl_importePago.Text = "0,00";

                using (var context = new CentrexDbContext())
                {
                    // ============================
                    // 1️⃣ Validar y obtener búsqueda
                    // ============================
                    string txtsearch = txt_searchCH.Text.Trim();
                    var query = context.Cheques.Include(ch => ch.Cliente).Include(ch => ch.Banco).Include(ch => ch.CuentaBancaria.Banco).Include(ch => ch.SysEstadoCheques).Where(ch => ch.activo == true && ch.IdCliente == idCliente);





                    // ============================
                    // 2️⃣ Filtro de texto (si aplica)
                    // ============================
                    if (!string.IsNullOrEmpty(txtsearch))
                    {
                        string s = txtsearch.ToLower();
                        query = query.Where(ch => ch.IdCheque.ToString().Contains(s) || ch.Banco.Nombre.ToLower().Contains(s) || ch.NCheque.ToString.Contains(s) || ch.Importe.ToString().Contains(s) || ch.SysEstadoCheques.Estado.ToLower().Contains(s));
                    }

                    // ============================
                    // 3️⃣ Ordenar por número de cheque
                    // ============================
                    query = query.OrderBy(ch => ch.NCheque);

                    // ============================
                    // 4️⃣ Proyección anónima para el DataGridView
                    // ============================
                    var resultado = query.Select(ch => new
                    {
                        ID = ch.IdCheque,
                        Cliente = ch.Cliente.RazonSocial,
                        Banco = ch.Banco.Nombre,
                        Numero = ch.NCheque,
                        ch.Importe,
                        ch.SysEstadoCheques.Estado,
                        Depositado = ch.IdCuentaBancaria is null ? "No" : Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject("Si, en: ", ch.CuentaBancaria.Banco.Nombre), " - "), ch.CuentaBancaria.Nombre),
                        Activo = Conversions.ToBoolean(ch.Activo) ? "Si" : "No"
                    }).ToList();

                    // ============================
                    // 5️⃣ Mostrar en DataGridView
                    // ============================
                    dg_viewCH.DataSource = resultado;
                    dg_viewCH.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    dg_viewCH.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                    // ============================
                    // 6️⃣ Calcular total de importes seleccionados
                    // ============================
                    decimal total = 0m;
                    foreach (var row in resultado)
                        total = Conversions.ToDecimal(total + ((dynamic)row).Importe);

                    lbl_importePago.Text = VariablesGlobales.precio(total);

                    // ============================
                    // 7️⃣ Selección y otros métodos
                    // ============================
                    VariablesGlobales.selCheques();

                }
            }

            catch (Exception ex)
            {
                Interaction.MsgBox("Error al actualizar la grilla de cheques: " + ex.Message, (MsgBoxStyle)((int)Constants.vbCritical + (int)Constants.vbOKOnly), "Centrex");
            }
        }



        private void resetForm()
        {
            Total = 0d;
            Efectivo = 0d;
            transferenciaBancaria = 0d;
            TotalCh = 0d;
            totalRetenciones = 0d;
            chSel = null;
            noCambiar = false;

            chk_cobro_no_oficial.Checked = false;
            chk_efectivo.Checked = false;
            chk_transferencia.Checked = false;
            chk_cheque.Checked = false;
            chk_retenciones.Checked = false;

            txt_efectivo.Text = Efectivo.ToString();
            lbl_totalRetencion.Text = Strings.FormatCurrency(totalRetenciones);
            lbl_totalTransferencia.Text = Strings.FormatCurrency(transferenciaBancaria);
            lbl_totalCh.Text = Strings.FormatCurrency(TotalCh);
            lbl_importePago.Text = Strings.FormatCurrency(Total);
            txt_notas.Text = "";
            cargarDGTransferencias();
            cargarDGRetenciones();
        }

    }
}
