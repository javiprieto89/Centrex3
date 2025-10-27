using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
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
                // Limpieza de temporales usando las funciones existentes
                generales.Borrar_tabla_segun_usuario("tmpCobrosRetenciones", usuario_logueado.IdUsuario);
                generales.Borrar_tabla_segun_usuario("tmpTransferencias", usuario_logueado.IdUsuario);
                generales.Borrar_tabla_segun_usuario("tmpCheques", usuario_logueado.IdUsuario);

                // Cargar combo clientes usando la función existente
                generales.Cargar_Combo(
                    ref cmb_cliente,
                    entidad: "ClienteEntity",
                    displaymember: "RazonSocial",
                    valuemember: "IdCliente",
                    predet: -1,
                    soloActivos: true,
                    orden: new List<Tuple<string, bool>> { Tuple.Create("RazonSocial", true) });

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
                
                // Cargar cuentas corrientes usando la función existente con filtros
                var filtros = new Dictionary<string, object>
                {
                    ["IdCliente"] = clienteId
                };

                generales.Cargar_Combo(
                    ref cmb_cc,
                    entidad: "CcClienteEntity",
                    displaymember: "Nombre",
                    valuemember: "IdCc",
                    predet: -1,
                    soloActivos: true,
                    filtros: filtros,
                    orden: new List<Tuple<string, bool>> { Tuple.Create("Nombre", true) });

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
            lbl_importePago.Text = precio(Total.ToString());
        }

        private void chk_cheque_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = chk_cheque.Checked;
            if (chk)
            {
                Total += TotalCh;
                lbl_totalCh.Text = precio(TotalCh.ToString());
            }
            else
            {
                Total -= TotalCh;
                lbl_totalCh.Text = precio("0");
            }
            lbl_importePago.Text = precio(Total.ToString());
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
                lbl_totalTransferencia.Text = precio(transferenciaBancaria.ToString());
            }
            else
            {
                Total -= transferenciaBancaria;
                lbl_totalTransferencia.Text = precio("0");
            }
            lbl_importePago.Text = precio(Total.ToString());
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
                lbl_totalRetencion.Text = precio(totalRetenciones.ToString());
            }
            else
            {
                Total -= totalRetenciones;
                lbl_totalRetencion.Text = precio("0");
            }
            lbl_importePago.Text = precio(Total.ToString());
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
                cb.FechaCarga = DateOnly.FromDateTime(DateTime.Now);
                cb.FechaCobro = DateOnly.FromDateTime(dtp_fechaCobro.Value.Date);
                cb.IdCliente = Conversions.ToInteger(cmb_cliente.SelectedValue);
                cb.IdCc = Conversions.ToInteger(cmb_cc.SelectedValue);
                cb.Efectivo = (decimal)(chk_efectivo.Checked ? Conversion.Val(txt_efectivo.Text) : 0d);
                cb.HayCheque = chk_cheque.Checked;
                cb.HayTransferencia = chk_transferencia.Checked;
                cb.HayRetencion = chk_retenciones.Checked;
                cb.TotalCh = (decimal)(chk_cheque.Checked ? sumaCheques() : 0d);
                cb.TotalTransferencia = (decimal)(chk_transferencia.Checked ? sumaTransferencias() : 0d);
                cb.TotalRetencion = (decimal)(chk_retenciones.Checked ? sumaRetenciones() : 0d);
                cb.Total = (decimal)Total;
                cb.Notas = txt_notas.Text;
                cb.Firmante = txt_firmante.Text;

                cb.IdCobro = addcobro(cb);
                if (cb.IdCobro > 0)
                {
                    if (cb.HayCheque)
                        cobros.add_chequeCobrado(cb.IdCobro, obtenerChequesSeleccionados());
                    if (cb.HayTransferencia)
                        guardarTransferencias_EF(cb);
                    if (cb.HayRetencion)
                        guardarCobroRetencion_EF(cb);
                    cobros.guardar_cobros_facturas_importes(cb.IdCobro, dg_view_nFC_importes);
                    cc.Saldo = cc.Saldo - (decimal)Total;
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

        private void selCheques()
        {
            noCambiar = true;

            foreach (DataGridViewRow row in dg_viewCH.Rows)
            {
                if (searchArray(chSel, Conversions.ToInteger(row.Cells["ID"].Value)))
                {
                    row.Cells[selColName].Value = true;
                }
            }

            noCambiar = false;
        }

        private bool searchArray(int[] array, int value)
        {
            if (array == null) return false;
          return array.Contains(value);
        }

        private string precio(string str)
        {
            return "$ " + str;
        }

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
                ctx.CobroEntity.Add(cb);
                ctx.SaveChanges();
                return cb.IdCobro;
            }
        }

        private void guardarTransferencias_EF(CobroEntity cb)
        {
            using (var ctx = new CentrexDbContext())
            {
                var transferencias = ctx.TmpTransferenciaEntity.ToList();
                foreach (var t in transferencias)
                {
                    var transferencia = new TransferenciaEntity
                    {
                        IdCobro = cb.IdCobro,
                        IdCuentaBancaria = t.IdCuentaBancaria,
                        Fecha = t.Fecha,
                        Total = t.Total,
                        NComprobante = t.NComprobante,
                        Notas = t.Notas
                    };
                    ctx.TransferenciaEntity.Add(transferencia);
                }
                
                // Borrar temporales
                ctx.TmpTransferenciaEntity.RemoveRange(transferencias);
                ctx.SaveChanges();
            }
        }

        private void guardarCobroRetencion_EF(CobroEntity cb)
        {
            using (var ctx = new CentrexDbContext())
            {
                var retenciones = ctx.TmpCobroRetencionEntity.ToList();
 
                foreach (var r in retenciones)
                {
                    var retencion = new CobroRetencionEntity
                  {
                 IdCobro = cb.IdCobro,
                IdImpuesto = r.IdImpuesto,
              Total = r.Total,
                   Notas = r.Notas
                };
                    ctx.CobroRetencionEntity.Add(retencion);
                }
                
                // Borrar temporales
              ctx.TmpCobroRetencionEntity.RemoveRange(retenciones);
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
            // Usar las funciones temporales existentes para transferencias
            using (var ctx = new CentrexDbContext())
            {
      var transferencias = ctx.TmpTransferenciaEntity
        .Include(t => t.IdCuentaBancariaNavigation)
          .ThenInclude(cb => cb.IdBancoNavigation)
         .Select(t => new
    {
             ID = t.IdTmpTransferencia,
     Banco = t.IdCuentaBancariaNavigation.IdBancoNavigation.Nombre,
        CuentaBancaria = t.IdCuentaBancariaNavigation.IdCuentaBancaria,
        t.Fecha,
    t.Total,
          t.Notas
  }).ToList();
         
           dg_viewTransferencia.DataSource = transferencias;
         }
        }

        private void cargarDGRetenciones()
        {
            // Usar las funciones temporales existentes para retenciones
       using (var ctx = new CentrexDbContext())
     {
   var retenciones = ctx.TmpCobroRetencionEntity
     .Include(r => r.IdImpuestoNavigation)
  .Select(r => new
   {
     ID = r.IdTmpRetencion,
     Retencion = r.IdImpuestoNavigation.Nombre,
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
                // Limpieza inicial
                dg_viewCH.DataSource = null;
                lbl_importePago.Text = "0,00";

                using (var context = new CentrexDbContext())
                {
                    string txtsearch = txt_searchCH.Text.Trim();
 var query = context.ChequeEntity
  .Include(ch => ch.IdClienteNavigation)
        .Include(ch => ch.IdBancoNavigation)
  .Include(ch => ch.IdCuentaBancariaNavigation)
     .ThenInclude(cb => cb.IdBancoNavigation)
   .Include(ch => ch.IdEstadochNavigation)
    .Where(ch => ch.Activo == true && ch.IdCliente == idCliente);

     // Filtro de búsqueda
      if (!string.IsNullOrEmpty(txtsearch))
     {
  string s = txtsearch.ToLower();
       query = query.Where(ch => ch.IdCheque.ToString().Contains(s) ||
   ch.IdBancoNavigation.Nombre.ToLower().Contains(s) ||
     ch.NCheque.ToString().Contains(s) ||
     ch.Importe.ToString().Contains(s) ||
   ch.IdEstadochNavigation.Estado.ToLower().Contains(s));
    }

       // Proyección y resultado
   var resultado = query.OrderBy(ch => ch.NCheque)
       .Select(ch => new
    {
     ID = ch.IdCheque,
     Cliente = ch.IdClienteNavigation.RazonSocial,
  Banco = ch.IdBancoNavigation.Nombre,
   Numero = ch.NCheque,
       ch.Importe,
          Estado = ch.IdEstadochNavigation.Estado,
      Depositado = ch.IdCuentaBancaria == null ? "No" :
       $"Si, en: {ch.IdCuentaBancariaNavigation.IdBancoNavigation.Nombre} - {ch.IdCuentaBancariaNavigation.Nombre}",
     Activo = ch.Activo ? "Si" : "No"
         }).ToList();

     dg_viewCH.DataSource = resultado;
    dg_viewCH.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
     dg_viewCH.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

   // Calcular total
 decimal total = 0m;
     foreach (var row in resultado)
     total += ((dynamic)row).Importe;

 lbl_importePago.Text = precio(total.ToString());
     selCheques();
   }
     }
       catch (Exception ex)
 {
 Interaction.MsgBox("Error al actualizar la grilla de cheques: " + ex.Message, MsgBoxStyle.Critical, "Centrex");
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
            lbl_totalRetencion.Text = precio(totalRetenciones.ToString());
       lbl_totalTransferencia.Text = precio(transferenciaBancaria.ToString());
   lbl_totalCh.Text = precio(TotalCh.ToString());
      lbl_importePago.Text = precio(Total.ToString());
            txt_notas.Text = "";
            cargarDGTransferencias();
      cargarDGRetenciones();
    }

    }
}
