using System;
using System.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace Centrex
{

    public partial class add_transferencia
    {
        private Form formViejo;
        private bool esCobro;
        private bool esPago;

        public add_transferencia()
        {
            InitializeComponent();
        }

        public add_transferencia(bool _esCobro, bool _esPago)
        {
            InitializeComponent();
            esCobro = _esCobro;
            esPago = _esPago;
        }

        private void add_transferencia_Load(object sender, EventArgs e)
        {
            using (var context = new CentrexDbContext())
            {
                // Cargar bancos
                var bancos = context.Bancos.OrderBy(b => b.nombre).ToList();
                cmb_banco.DataSource = bancos;
                cmb_banco.DisplayMember = "Nombre";
                cmb_banco.ValueMember = "IdBanco";
            }

            if (!VariablesGlobales.edicion & !VariablesGlobales.borrado)
            {
                cmb_banco.SelectedItem = null;
                cmb_banco.Text = "Seleccione un banco...";
                cmb_cuentaBancaria.Text = "Seleccione un banco...";
                cmb_cuentaBancaria.Enabled = false;
            }
            else
            {
                dtp_fecha.Value = DateTime.Parse(Conversions.ToString(VariablesGlobales.edita_transferencia.fecha.Value));

                using (var context = new CentrexDbContext())
                {
                    var cb = context.CuentasBancarias.Include(c => c.Banco).FirstOrDefault(c => c.IdCuentaBancaria == VariablesGlobales.edita_transferencia.IdCuentaBancaria);

                    if (cb is not null)
                    {
                        cmb_banco.SelectedValue = cb.Banco.IdBanco;

                        var cuentas = context.CuentasBancarias.Where(c => c.IdBanco == cb.Banco.IdBanco).OrderBy(c => c.Nombre).ToList();


                        cmb_cuentaBancaria.DataSource = cuentas;
                        cmb_cuentaBancaria.DisplayMember = "Nombre";
                        cmb_cuentaBancaria.ValueMember = "IdCuentaBancaria";
                        cmb_cuentaBancaria.SelectedValue = VariablesGlobales.edita_transferencia.IdCuentaBancaria;
                    }
                }

                txt_importe.Text = VariablesGlobales.edita_transferencia.total.ToString();
                txt_nComprobante.Text = VariablesGlobales.edita_transferencia.nComprobante;
                txt_notas.Text = VariablesGlobales.edita_transferencia.notas;
            }

            if (VariablesGlobales.borrado)
            {
                dtp_fecha.Enabled = false;
                cmb_banco.Enabled = false;
                cmb_cuentaBancaria.Enabled = false;
                txt_importe.Enabled = false;
                txt_nComprobante.Enabled = false;
                txt_notas.Enabled = false;
                cmd_ok.Enabled = false;
                cmd_exit.Enabled = false;
                Show();
                if (Interaction.MsgBox("¿Está seguro que desea borrar esta transferencia?", (MsgBoxStyle)((int)Constants.vbYesNo + (int)Constants.vbQuestion), "Centrex") == MsgBoxResult.Yes)
                {
                    if (BorrarTmpTransferencia(VariablesGlobales.edita_transferencia.IdTransferencia) == false)
                    {
                        Interaction.MsgBox("No se ha podido borrar la transferencia.");
                    }
                }
                closeandupdate(this);
            }

            formViejo = this;
        }

        private void txt_importe_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = VariablesGlobales.valida(e.KeyChar, 5);
        }

        private void cmd_exit_Click(object sender, EventArgs e)
        {
            closeandupdate(this);
        }

        private void cmd_ok_Click(object sender, EventArgs e)
        {
            var t = new TransferenciaEntity();

            if (cmb_cuentaBancaria.Text.Contains("Seleccione"))
            {
                Interaction.MsgBox("Debe seleccionar una cuenta bancaria", (MsgBoxStyle)((int)Constants.vbExclamation + (int)Constants.vbOKOnly), "Centrex");
                return;
            }
            else if (string.IsNullOrEmpty(txt_importe.Text))
            {
                Interaction.MsgBox("Debe ingresar un importe", (MsgBoxStyle)((int)Constants.vbExclamation + (int)Constants.vbOKOnly), "Centrex");
                return;
            }
            else if (string.IsNullOrEmpty(txt_nComprobante.Text))
            {
                Interaction.MsgBox("Debe ingresar un número de comprobante", (MsgBoxStyle)((int)Constants.vbExclamation + (int)Constants.vbOKOnly), "Centrex");
                return;
            }

            t.fecha = dtp_fecha.Value.Date;
            t.IdCuentaBancaria = cmb_cuentaBancaria.SelectedValue;
            t.total = decimal.Parse(txt_importe.Text);
            t.nComprobante = txt_nComprobante.Text;
            t.notas = txt_notas.Text;

            if (VariablesGlobales.edicion)
            {
                t.IdTransferencia = VariablesGlobales.edita_transferencia.IdTransferencia;
                if (!transferencias.UpdateTmpTransferencia(VariablesGlobales.ConvertToTmpTransferencia(t)))
                {
                    Interaction.MsgBox("Ocurrió un error al actualizar la transferencia.", (MsgBoxStyle)((int)Constants.vbExclamation + (int)Constants.vbOKOnly), "Centrex");
                }
            }
            else
            {
                t.IdTransferencia = transferencias.AddTmpTransferencia(VariablesGlobales.ConvertToTmpTransferencia(t));
                if (t.IdTransferencia == 0)
                {
                    Interaction.MsgBox("Ocurrió un error al agregar la transferencia", (MsgBoxStyle)((int)Constants.vbExclamation + (int)Constants.vbOKOnly), "Centrex");
                }
            }

            closeandupdate(this);
        }

        private void cmb_banco_SelectionChangeCommitted(object sender, EventArgs e)
        {
            int seleccionado = Conversions.ToInteger(cmb_banco.SelectedValue);

            using (var context = new CentrexDbContext())
            {
                var cuentas = context.CuentasBancarias.Where(c => c.IdBanco == seleccionado).OrderBy(c => c.Nombre).ToList();


                cmb_cuentaBancaria.DataSource = cuentas;
                cmb_cuentaBancaria.DisplayMember = "Nombre";
                cmb_cuentaBancaria.ValueMember = "IdCuentaBancaria";
            }

            cmb_cuentaBancaria.SelectedItem = null;
            cmb_cuentaBancaria.Text = "Seleccione una cuenta bancaria...";
            cmb_cuentaBancaria.Enabled = true;
        }

        private void psearch_banco_Click(object sender, EventArgs e)
        {
            string tmp = VariablesGlobales.tabla;
            VariablesGlobales.tabla = "bancos";
            Enabled = false;
            My.MyProject.Forms.search.ShowDialog();
            VariablesGlobales.tabla = tmp;

            cmb_banco.SelectedValue = VariablesGlobales.id;
            VariablesGlobales.id = 0;
            cmb_banco_SelectionChangeCommitted(null, null);
        }

        private void psearch_cuentaBancaria_Click(object sender, EventArgs e)
        {
            if (cmb_banco.Text.Contains("Seleccione"))
                return;

            string tmp = VariablesGlobales.tabla;
            VariablesGlobales.tabla = "cuentas_bancarias";
            Enabled = false;
            var frm = new search(Conversions.ToInteger(cmb_banco.SelectedValue));
            frm.ShowDialog();
            VariablesGlobales.tabla = tmp;
            Enabled = true;

            cmb_cuentaBancaria.SelectedValue = VariablesGlobales.id;
            VariablesGlobales.id = 0;
        }

        private void add_transferencia_FormClosed(object sender, FormClosedEventArgs e)
        {
            formViejo = null;
            closeandupdate(this);
        }

        private void cmb_banco_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void cmb_cuentaBancaria_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
    }
}
