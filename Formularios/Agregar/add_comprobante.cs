using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Centrex.Models;

namespace Centrex
{
    public partial class add_comprobante
    {
   public add_comprobante()
 {
            InitializeComponent();
        }

        private void cmd_ok_Click(object sender, EventArgs e)
        {
    if (string.IsNullOrEmpty(txt_comprobante.Text))
   {
        Interaction.MsgBox("El campo 'Comprobante' es obligatorio y está vacio", (MsgBoxStyle)((int)Constants.vbOKOnly + (int)Constants.vbExclamation), "Centrex");
   return;
     }
     else if (cmb_tipoComprobante.SelectedValue is null)
            {
                Interaction.MsgBox("Debe seleccionar un tipo de comprobante", (MsgBoxStyle)((int)Constants.vbOKOnly + (int)Constants.vbExclamation), "Centrex");
                return;
            }
      else if (chk_esMipyme.Checked && cmb_modoMiPyme.SelectedValue is null)
         {
       Interaction.MsgBox("Debe seleccionar un modo MiPyME", (MsgBoxStyle)((int)Constants.vbOKOnly + (int)Constants.vbExclamation), "Centrex");
        return;
            }
  else if (chk_esMipyme.Checked && cmb_comprobanteRelacionado.Text == "Seleccione un comprobante asociado...")
     {
Interaction.MsgBox("El campo 'Comprobant asociado' es obligatorio al ser un comprobante MiPyME y está vacio", (MsgBoxStyle)((int)Constants.vbOKOnly + (int)Constants.vbExclamation), "Centrex");
       return;
      }

            var c = new ComprobanteEntity();

     c.Comprobante = txt_comprobante.Text;
            c.IdTipoComprobante = Convert.ToInt32(cmb_tipoComprobante.SelectedValue);
            c.NumeroComprobante = (int)Math.Round(nup_numero.Value);
            c.PuntoVenta = (int)Math.Round(nup_puntoVenta.Value);
            c.EsFiscal = rb_fiscal.Checked;
  c.EsElectronica = rb_electronico.Checked;
       c.EsManual = rb_manual.Checked;
            c.EsPresupuesto = rb_presupuesto.Checked;
            c.Activo = chk_activo.Checked;
            c.Testing = chk_testing.Checked;
            c.MaxItems = (int?)Math.Round(nup_maxItems.Value);
            c.Contabilizar = chk_contabilizar.Checked;
            c.MueveStock = chk_mueveStock.Checked;
    // .comprobanteRelacionado = cmb_comprobanteRelacionado.SelectedValue
   c.EsMiPyMe = chk_esMipyme.Checked;
    c.CbuEmisor = txt_CBU_emisor.Text;
   c.AliasCbuEmisor = txt_alias_CBU_emisor.Text;
       c.IdModoMiPyme = Convert.ToInt32(cmb_modoMiPyme.SelectedValue);
   c.AnulaMiPyMe = string.IsNullOrEmpty(txt_comprobanteAnulacion.Text)
  ? null
    : txt_comprobanteAnulacion.Text.Substring(0, 1);

            if (VariablesGlobales.edicion == true)
         {
            c.IdComprobante = VariablesGlobales.edita_comprobante.IdComprobante;
     if (comprobantes.updatecomprobante(c) == false)
    {
       Interaction.MsgBox("Hubo un problema al actualizar el comprobante, consulte con su programdor", Constants.vbExclamation);
           closeandupdate(this);
       }
         }
            else
            {
     comprobantes.addcomprobante(c);
      }

  if (chk_secuencia.Checked == true)
    {
          txt_comprobante.Text = "";
 if (VariablesGlobales.id_tipoComprobante_default > 0)
        cmb_tipoComprobante.SelectedValue = VariablesGlobales.id_tipoComprobante_default;
      else
      cmb_tipoComprobante.SelectedIndex = -1;
            nup_numero.Value = 1m;
    nup_puntoVenta.Value = 1m;
    rb_electronico.Checked = true;
                chk_activo.Checked = true;
    chk_testing.Checked = false;
        nup_maxItems.Value = 20m;
           // chk_esMipyme.Checked = False
                // cmb_modoMiPyme.SelectedValue = 1
    cmb_comprobanteRelacionado.Text = "Seleccione un comprobante asociado...";
            }
            else
       {
  closeandupdate(this);
            }
        }

        private void cmd_exit_Click(object sender, EventArgs e)
    {
            closeandupdate(this);
        }

        private void add_comprobante_FormClosed(object sender, FormClosedEventArgs e)
        {
            closeandupdate(this);
      }

        private void add_comprobante_Load(object sender, EventArgs e)
        {
            // Cargo todos los tipos de documentos
   generales.Cargar_Combo(
     ref cmb_tipoComprobante,
    entidad: "TipoComprobanteEntity",
                displaymember: "ComprobanteAfip",
   valuemember: "IdTipoComprobante",
         predet: -1,
          soloActivos: true,
             filtros: null,
       orden: OrdenAsc("ComprobanteAfip"));
            if (VariablesGlobales.id_tipoComprobante_default > 0)
  {
          cmb_tipoComprobante.SelectedValue = VariablesGlobales.id_tipoComprobante_default;
  }
 else
       {
  cmb_tipoComprobante.SelectedIndex = -1;
     cmb_tipoComprobante.Text = "Seleccione un tipo...";
            }

      // Cargo todos los comprobantes asociados
 // cargar_combo(cmb_comprobanteRelacionado, "SELECT id_comprobante, comprobante FROM comprobantes ORDER BY comprobante ASC", basedb, "comprobante", "id_comprobante")
    // cmb_comprobanteRelacionado.Text = "Seleccione un comprobante asociado..."
            // cmb_comprobanteRelacionado.Enabled = False
            // cmb_comprobanteAsociado.SelectedValue = id_tipoComprobante_default

         // Cargo todos los modos de MiPyme
       generales.Cargar_Combo(
 ref cmb_modoMiPyme,
                entidad: "SysModoMiPymeEntity",
              displaymember: "Modo",
    valuemember: "IdModoMiPyme",
         predet: -1,
    soloActivos: false,
       filtros: null,
         orden: OrdenAsc("Modo"));
    if (cmb_modoMiPyme.Items.Count > 0)
            {
              cmb_modoMiPyme.SelectedValue = 1; // 1=No es MiPyME
         }

       rb_electronico.Checked = true;
 chk_activo.Checked = true;

       if (VariablesGlobales.edicion == true | VariablesGlobales.borrado == true)
    {
   chk_secuencia.Enabled = false;
    {
    var withBlock = VariablesGlobales.edita_comprobante;
              txt_comprobante.Text = withBlock.Comprobante;
        cmb_tipoComprobante.SelectedValue = withBlock.IdTipoComprobante;
           cmb_tipoComprobante.Enabled = false;
         nup_numero.Value = withBlock.NumeroComprobante;
        nup_puntoVenta.Value = withBlock.PuntoVenta;
          rb_fiscal.Checked = withBlock.EsFiscal ?? false;
      rb_electronico.Checked = withBlock.EsElectronica ?? false;
  rb_manual.Checked = withBlock.EsManual ?? false;
     rb_presupuesto.Checked = withBlock.EsPresupuesto ?? false;
           chk_activo.Checked = withBlock.Activo;
    chk_testing.Checked = withBlock.Testing;
        nup_maxItems.Value = withBlock.MaxItems ?? 0;
        chk_contabilizar.Checked = withBlock.Contabilizar;
       chk_mueveStock.Checked = withBlock.MueveStock;
               chk_esMipyme.Checked = withBlock.EsMiPyMe;
   // cmb_comprobanteRelacionado.SelectedValue = .comprobanteRelacionado
               txt_CBU_emisor.Text = withBlock.CbuEmisor;
  txt_alias_CBU_emisor.Text = withBlock.AliasCbuEmisor;
   cmb_modoMiPyme.SelectedValue = withBlock.IdModoMiPyme;
         txt_comprobanteAnulacion.Text = withBlock.AnulaMiPyMe;
             }
  }

         if (VariablesGlobales.borrado == true)
  {
           txt_comprobante.Enabled = false;
   cmb_tipoComprobante.Enabled = false;
         nup_numero.Enabled = false;
    nup_puntoVenta.Enabled = false;
          rb_fiscal.Enabled = false;
      rb_electronico.Enabled = false;
     rb_manual.Enabled = false;
     rb_presupuesto.Enabled = false;
       chk_activo.Enabled = false;
      chk_testing.Enabled = false;
    chk_secuencia.Enabled = false;
       cmd_ok.Visible = false;
     cmd_exit.Visible = false;
        chk_contabilizar.Enabled = false;
                chk_mueveStock.Enabled = false;
           chk_esMipyme.Enabled = false;
       // cmb_comprobanteRelacionado.Enabled = False
         txt_CBU_emisor.Enabled = false;
     txt_alias_CBU_emisor.Enabled = false;
             txt_comprobanteAnulacion.Enabled = false;
         cmb_modoMiPyme.Enabled = false;
   Show();
     if (Interaction.MsgBox("¿Está seguro que desea borrar este comprobante?", (MsgBoxStyle)((int)Constants.vbYesNo + (int)Constants.vbQuestion)) == MsgBoxResult.Yes)
   {
         if (comprobantes.borrarcomprobante(VariablesGlobales.edita_comprobante) == false)
           {
          if (Interaction.MsgBox("Ocurrió un error al realizar el borrado del comprobante, ¿desea intectar desactivarlo para que no aparezca en la búsqueda?", (MsgBoxStyle)((int)MsgBoxStyle.Question + (int)MsgBoxStyle.YesNo)) == Constants.vbYes)
          {
       // Realizo un borrado lógico
         if (comprobantes.updatecomprobante(VariablesGlobales.edita_comprobante, true) == true)
  {
     Interaction.MsgBox("Se ha podido realizar un borrado lógico, pero el comprobante no se borró definitivamente." + "\r" + "Esto posiblemente se deba a que el comprobante, tiene operaciones realizadas y por lo tanto no podrá borrarse", Constants.vbInformation);
     }
       else
      {
          Interaction.MsgBox("No se ha podido borrar el comprobante, consulte con el programador");
              }
  }
}
   }
           closeandupdate(this);
       }
        }

        private void cmb_tipoComprobante_KeyPress(object sender, KeyPressEventArgs e)
        {
      // e.KeyChar = ""
        }

     private void chk_esMipyme_CheckedChanged(object sender, EventArgs e)
        {
        bool chk = chk_esMipyme.Checked;

       // cmb_comprobanteRelacionado.Enabled = chk
      txt_CBU_emisor.Enabled = chk;
            txt_alias_CBU_emisor.Enabled = chk;
            txt_comprobanteAnulacion.Enabled = chk;
            cmb_modoMiPyme.Enabled = chk;
        }

    private static List<Tuple<string, bool>> OrdenAsc(string columna) =>
            new List<Tuple<string, bool>> { Tuple.Create(columna, true) };
    }
}
