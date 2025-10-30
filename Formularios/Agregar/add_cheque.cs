using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Centrex
{
    public partial class add_cheque : Form
    {
        private int id_Cliente = -1;
        private int id_Proveedor = -1;
        private bool chRecibido = true;
        private bool chEmitido = false;

        public add_cheque()
        {
            InitializeComponent();
        }

        public add_cheque(int idCliente, int _idProveedor)
        {
            InitializeComponent();
            id_Cliente = idCliente;
            id_Proveedor = _idProveedor;
            chRecibido = true;
        }

        public add_cheque(bool _chRecibido, bool _chEmitido)
        {
            InitializeComponent();
            chRecibido = _chRecibido;
            chEmitido = _chEmitido;

            if (chRecibido)
            {
                rb_recibido.Checked = true;
                rb_emitido.Enabled = false;
                cmb_proveedor.Enabled = false;
            }
            else
            {
                rb_emitido.Checked = true;
                rb_recibido.Enabled = false;
                cmb_cliente.Enabled = false;
            }
        }

        private async void Add_cheque_Load(object sender, EventArgs e)
        {
            try
            {
                using var ctx = new CentrexDbContext();

                // ===============================
                // CLIENTES
                // ===============================
                await CargarComboAsync(
                    cmb_cliente,
                    ctx.ClienteEntity
                        .Where(c => c.Activo)
                        .OrderBy(c => c.RazonSocial),
                    "RazonSocial",
                    "IdCliente"
                );
                cmb_cliente.Text = "Seleccione un cliente...";

                // ===============================
                // PROVEEDORES
                // ===============================
                await CargarComboAsync(
                    cmb_proveedor,
                    ctx.ProveedorEntity
                        .Where(p => p.Activo)
                        .OrderBy(p => p.RazonSocial),
                    "RazonSocial",
                    "IdProveedor"
                );
                cmb_proveedor.Text = "Seleccione un proveedor...";

                // ===============================
                // BANCOS
                // ===============================
                await CargarComboAsync(
                    cmb_banco,
                    ctx.BancoEntity
                        .Where(b => b.Activo)
                        .OrderBy(b => b.Nombre),
                    "Nombre",
                    "IdBanco"
                );
                cmb_banco.Text = "Seleccione un banco...";

                // ===============================
                // ESTADOS DE CHEQUE
                // ===============================
                await CargarComboAsync(
                    cmb_estadoch,
                    ctx.SysEstadoChequeEntity
                        .OrderBy(ech => ech.Estado),
                    "Estado",
                    "IdEstadoCh"
                );
                cmb_estadoch.Text = "Seleccione un estado...";
                cmb_estadoch.SelectedValue = 1; // 1 = Cheque en cartera

                // ===============================
                // CUENTAS BANCARIAS
                // ===============================
                await CargarComboAsync(
                    cmb_cuentaBancaria,
                    ctx.CuentaBancariaEntity
                        .Include(cb => cb.IdBancoNavigation.Nombre)
                        .Where(cb => cb.Activo)
                        .OrderBy(cb => cb.IdBancoNavigation.Nombre)
                        .Select(cb => new
                        {
                            cb.IdCuentaBancaria,
                            Nombre = cb.IdBancoNavigation.Nombre + " - " + cb.Nombre
                        }),
                    "Nombre",
                    "IdCuentaBancaria"
                );
                cmb_cuentaBancaria.Text = "Seleccione una cuenta bancaria...";

                // ===============================
                // CONFIGURACIÓN INICIAL
                // ===============================
                chk_depositado.Checked = false;

                // =======================================
                // CONFIGURACION INICIAL SEGUN ESTADO
                // =======================================

                if (id_Cliente != -1)
                {
                    cmb_cliente.SelectedValue = id_Cliente;
                    cmb_cliente.Enabled = false;
                    cmb_proveedor.Enabled = false;
                    pic_searchCliente.Enabled = false;
                    pic_searchProveedor.Enabled = false;
                    rb_recibido.CheckedChanged -= rb_recibido_CheckedChanged;
                    rb_recibido.Enabled = false;
                    rb_emitido.Enabled = false;
                    rb_recibido.Checked = true;
                    rb_recibido.CheckedChanged += rb_recibido_CheckedChanged;
                    return;
                }

                if (id_Proveedor != -1)
                {
                    cmb_proveedor.SelectedValue = id_Proveedor;
                    cmb_proveedor.Enabled = false;
                    pic_searchProveedor.Enabled = false;
                    rb_recibido.Enabled = false;
                    rb_emitido.Enabled = false;
                    return;
                }

                if (edicion || borrado)
                {
                    var ch = edita_cheque;

                    dtp_fIngreso.Value = DateTime.Parse(ch.FechaIngreso.ToString());
                    dtp_fEmision.Value = DateTime.Parse(ch.FechaEmision.ToString());

                    if (ch.IdCliente != default)
                    {
                        rb_recibido.Checked = true;
                        rb_emitido.Checked = false;
                        cmb_cliente.SelectedValue = ch.IdCliente;
                        cmb_cliente.Text = ch.IdClienteNavigation?.RazonSocial ?? "";
                    }
                    else if (ch.IdProveedor != default)
                    {
                        rb_recibido.Checked = false;
                        rb_emitido.Checked = true;
                        cmb_proveedor.SelectedValue = ch.IdProveedor;
                        cmb_proveedor.Text = ch.IdProveedorNavigation?.RazonSocial ?? "";
                    }

                    cmb_banco.SelectedValue = ch.IdBanco;
                    txt_nCheque.Text = ch.NCheque.ToString();
                    txt_nCheque2.Text = ch.NCheque.ToString();
                    txt_importe.Text = ch.Importe.ToString();
                    cmb_estadoch.SelectedValue = Conversions.ToInteger(ch.IdEstadoch);
                    chk_eCheck.Checked = Conversions.ToBoolean(ch.ECheck);

                    if (ch.FechaCobro != default)
                    {
                        chk_fCobro.Checked = true;
                        dtp_fCobro.Value = DateTime.Parse(ch.FechaCobro.ToString());                        
                    }

                    if (ch.FechaSalida != default)
                    {
                        chk_fSalida.Checked = true;
                        dtp_fSalida.Value = DateTime.Parse(ch.FechaSalida.ToString());
                    }

                    if (ch.FechaDeposito != default)
                    {
                        chk_fDeposito.Checked = true;
                        dtp_fDeposito.Value = DateTime.Parse(ch.FechaDeposito.ToString());
                    }

                    if (ch.IdCuentaBancaria != default)
                    {
                        chk_depositado.Checked = true;
                        cmb_cuentaBancaria.SelectedValue = ch.IdCuentaBancaria;
                    }

                    if (ch.IdEstadoch != ID_CH_CARTERA)
                    {
                        deshabilitarModiciaciones();
                    }

                    chk_secuencia.Checked = false;
                    chk_secuencia.Enabled = false;
                }
                else if (chRecibido)
                {
                    using (var ctxx = new CentrexDbContext())
                    {
                        var ultimo = await ctxx.ChequeEntity.MaxAsync(c => (int?)c.NCheque2) ?? 0;
                        txt_nCheque2.Text = (ultimo + 1).ToString();
                    }
                }
                else
                {
                    txt_nCheque2.Text = "0";
                }

                // =======================================
                // BORRADO LOGICO
                // =======================================

                if (borrado)
                {
                    deshabilitarModiciaciones();
                    Show();
                    var confirm = MessageBox.Show(
                        "¿Está seguro que desea borrar este cheque?",
                        "Centrex",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (confirm == DialogResult.Yes)
                    {
                        var success = await borrarChequeAsync(edita_cheque.IdCheque);
                        if (!success)
                        {
                            var alt = MessageBox.Show(
                                "Ocurrió un error al borrar el cheque.\n¿Desea desactivarlo para que no aparezca en la búsqueda?",
                                "Centrex",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question);
                            if (alt == DialogResult.Yes)
                            {
                                await desactivarChequeAsync(edita_cheque.IdCheque);
                            }
                        }
                    }
                    closeandupdate(this);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar formulario: {ex.Message}", "Centrex", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Ch_fSalida_CheckedChanged(object sender, EventArgs e)
        {
            dtp_fSalida.Enabled = chk_fSalida.Checked;
        }

        private void Ch_fDeposito_CheckedChanged(object sender, EventArgs e)
        {
            dtp_fDeposito.Enabled = chk_fDeposito.Checked;
        }

        private void rb_recibido_CheckedChanged(object sender, EventArgs e)
        {
            if (id_Proveedor != -1)
            {
                cmb_cliente.Enabled = rb_recibido.Checked;
                return;
            }
            cmb_cliente.Enabled = rb_recibido.Checked;
            cmb_proveedor.Enabled = rb_emitido.Checked;
        }

        private void rb_emitido_CheckedChanged(object sender, EventArgs e)
        {
            if (id_Proveedor != -1)
            {
                cmb_cliente.Enabled = rb_recibido.Checked;
                return;
            }
            cmb_cliente.Enabled = rb_recibido.Checked;
            cmb_proveedor.Enabled = rb_emitido.Checked;
        }

        private void chk_depositado_CheckedChanged(object sender, EventArgs e)
        {
            cmb_cuentaBancaria.Enabled = chk_depositado.Checked;
        }

        private void txt_importe_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
                e.Handled = true;
        }

        private void txt_nCheque_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
        }

        private void cmb_cliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void cmb_proveedor_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void cmb_banco_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void cmb_estadoch_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void cmb_cuentaBancaria_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private async void cmd_ok_Click(object sender, EventArgs e)
        {
            try
            {
                using (var ctx = new CentrexDbContext())
                {
                    // =======================
                    // VALIDACIONES
                    // =======================
                    if (rb_recibido.Checked && cmb_cliente.Text == "Seleccione un cliente...")
                    {
                        MessageBox.Show("Debe seleccionar un cliente.", "Centrex", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    if (rb_emitido.Checked && cmb_proveedor.Text == "Seleccione un proveedor...")
                    {
                        MessageBox.Show("Debe seleccionar un proveedor.", "Centrex", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    if (cmb_banco.Text == "Seleccione un banco...")
                    {
                        MessageBox.Show("Debe seleccionar un banco.", "Centrex", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    if (string.IsNullOrEmpty(txt_nCheque.Text))
                    {
                        MessageBox.Show("Debe ingresar un número de cheque.", "Centrex", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    if (string.IsNullOrEmpty(txt_importe.Text))
                    {
                        MessageBox.Show("Debe ingresar un importe.", "Centrex", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    if (cmb_estadoch.Text == "Seleccione un estado...")
                    {
                        MessageBox.Show("Debe seleccionar el estado del cheque.", "Centrex", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    if (chk_depositado.Checked && cmb_cuentaBancaria.Text == "Seleccione una cuenta bancaria...")
                    {
                        MessageBox.Show("Debe seleccionar la cuenta bancaria en la que se depositó el cheque.", "Centrex", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    // =======================
                    // CHEQUE NUEVO / EDICION
                    // =======================
                    ChequeEntity cheque;

                    if (edicion)
                    {
                        cheque = await ctx.ChequeEntity.FirstOrDefaultAsync(c => c.IdCheque == edita_cheque.IdCheque);
                        if (cheque == null)
                        {
                            MessageBox.Show("No se encontró el cheque para editar.", "Centrex", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                    else
                    {
                        cheque = new ChequeEntity();
                        ctx.ChequeEntity.Add(cheque);
                    }

                    // =======================
                    // MAPEO DE CAMPOS
                    // =======================
                    cheque.FechaEmision = DateOnly.FromDateTime(dtp_fEmision.Value.Date);

                    if (rb_recibido.Checked || chRecibido)
                    {
                        cheque.IdCliente = Convert.ToInt32(cmb_cliente.SelectedValue);
                        cheque.IdProveedor = null;
                        cheque.Recibido = true;
                        cheque.Emitido = false;
                    }
                    else if (rb_emitido.Checked)
                    {
                        cheque.IdProveedor = Convert.ToInt32(cmb_proveedor.SelectedValue);
                        cheque.IdCliente = null;
                        cheque.Emitido = true;
                        cheque.Recibido = false;
                    }

                    cheque.IdBanco = Convert.ToInt32(cmb_banco.SelectedValue);
                    cheque.NCheque = Convert.ToInt32(txt_nCheque.Text);
                    cheque.NCheque2 = Convert.ToInt32(txt_nCheque2.Text);
                    cheque.Importe = Convert.ToDecimal(txt_importe.Text);
                    cheque.ECheck = chk_eCheck.Checked;
                    cheque.IdEstadoch = Convert.ToInt32(cmb_estadoch.SelectedValue);

                    if (chk_fCobro.Checked)
                        cheque.FechaCobro = DateOnly.FromDateTime(dtp_fCobro.Value.Date);
                    else
                        cheque.FechaCobro = null;

                    if (chk_fSalida.Checked)
                        cheque.FechaSalida = DateOnly.FromDateTime(dtp_fSalida.Value.Date);
                    else
                        cheque.FechaSalida = null;

                    if (chk_fDeposito.Checked)
                        cheque.FechaDeposito = DateOnly.FromDateTime(dtp_fDeposito.Value.Date);
                    else
                        cheque.FechaDeposito = null;

                    if (chk_depositado.Checked)
                        cheque.IdCuentaBancaria = Convert.ToInt32(cmb_cuentaBancaria.SelectedValue);
                    else
                        cheque.IdCuentaBancaria = null;

                    // =======================
                    // VALIDACION DUPLICADO
                    // =======================
                    bool existe = await ctx.ChequeEntity.AnyAsync(c => c.NCheque == cheque.NCheque && c.IdCheque != cheque.IdCheque);
                    if (existe)
                    {
                        MessageBox.Show($"Ya existe un cheque con el número {cheque.NCheque}.", "Centrex", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    // =======================
                    // GUARDAR CAMBIOS
                    // =======================
                    await ctx.SaveChangesAsync();

                    MessageBox.Show("Cheque guardado correctamente.", "Centrex", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    if (chk_secuencia.Checked)
                    {
                        dtp_fEmision.Value = DateTime.Now;
                        txt_nCheque.Clear();
                        txt_nCheque2.Clear();
                        txt_importe.Clear();
                        chk_eCheck.Checked = false;
                        dtp_fCobro.Value = DateTime.Now;
                        dtp_fSalida.Value = DateTime.Now;
                        dtp_fDeposito.Value = DateTime.Now;
                        chk_fCobro.Checked = false;
                        chk_fSalida.Checked = false;
                        chk_fDeposito.Checked = false;
                        chk_depositado.Checked = false;
                    }
                    else
                    {
                        closeandupdate(this);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar el cheque: {ex.Message}", "Centrex", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // =====================================================
        // FUNCIONES DE BUSQUEDA (CLIENTE / PROVEEDOR / BANCO)
        // =====================================================

        private void pic_searchCliente_Click(object sender, EventArgs e)
        {
            string tmp = tabla;
            tabla = "clientes";
            Enabled = false;
            My.MyProject.Forms.search.ShowDialog();
            tabla = tmp;

            var idSeleccionado = id;
            if (idSeleccionado > 0)
                cmb_cliente.SelectedValue = idSeleccionado;
        }

        private void pic_searchProveedor_Click(object sender, EventArgs e)
        {
            string tmp = tabla;
            tabla = "proveedores";
            Enabled = false;
            My.MyProject.Forms.search.ShowDialog();
            tabla = tmp;

            var idSeleccionado = id;
            if (idSeleccionado > 0)
                cmb_proveedor.SelectedValue = idSeleccionado;
        }

        private void pic_searchBanco_Click(object sender, EventArgs e)
        {
            string tmp = tabla;
            tabla = "bancos";
            Enabled = false;
            My.MyProject.Forms.search.ShowDialog();
            tabla = tmp;

            var idSeleccionado = id;
            if (idSeleccionado > 0)
                cmb_banco.SelectedValue = idSeleccionado;
        }

        // =====================================================
        // FUNCION DE CIERRE
        // =====================================================

        private void cmd_exit_Click(object sender, EventArgs e)
        {
            closeandupdate(this);
        }

        // =====================================================
        // FUNCIONES AUXILIARES ASINCRONAS DE BORRADO / DESACTIVACION
        // =====================================================

        private async Task<bool> borrarChequeAsync(int idCheque)
        {
            try
            {
                using (var ctx = new CentrexDbContext())
                {
                    var cheque = await ctx.ChequeEntity.FindAsync(idCheque);
                    if (cheque != null)
                    {
                        ctx.ChequeEntity.Remove(cheque);
                        await ctx.SaveChangesAsync();
                        return true;
                    }
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        private async Task desactivarChequeAsync(int idCheque)
        {
            try
            {
                using (var ctx = new CentrexDbContext())
                {
                    var cheque = await ctx.ChequeEntity.FindAsync(idCheque);
                    if (cheque != null)
                    {
                        cheque.Activo = false;
                        await ctx.SaveChangesAsync();
                        MessageBox.Show("Se realizó un borrado lógico del cheque.", "Centrex", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al desactivar cheque: {ex.Message}", "Centrex", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // =====================================================
        // BLOQUEO DE CAMPOS EN MODO EDICION / BORRADO
        // =====================================================

        private void deshabilitarModiciaciones()
        {
            dtp_fEmision.Enabled = false;
            rb_recibido.Enabled = false;
            cmb_cliente.Enabled = false;
            pic_searchCliente.Enabled = false;
            rb_emitido.Enabled = false;
            cmb_proveedor.Enabled = false;
            pic_searchProveedor.Enabled = false;
            cmb_banco.Enabled = false;
            pic_searchBanco.Enabled = false;
            txt_nCheque.Enabled = false;
            txt_nCheque2.Enabled = false;
            txt_importe.Enabled = false;
            chk_eCheck.Enabled = false;
            cmb_estadoch.Enabled = false;
            chk_fCobro.Enabled = false;
            dtp_fCobro.Enabled = false;
            chk_fSalida.Enabled = false;
            dtp_fSalida.Enabled = false;
            chk_depositado.Enabled = false;
            dtp_fDeposito.Enabled = false;
            chk_depositado.Enabled = false;
            cmb_cuentaBancaria.Enabled = false;
            cmd_ok.Visible = false;
            cmd_ok.Enabled = false;
            cmd_exit.Visible = true;
            cmd_exit.Enabled = true;
        }
    }
}