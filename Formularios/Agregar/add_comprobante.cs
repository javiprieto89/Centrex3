using System;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

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
            else if (chk_esMipyme.Checked & cmb_comprobanteRelacionado.Text == "Seleccione un comprobante asociado...")
            {
                Interaction.MsgBox("El campo 'Comprobant asociado' es obligatorio al ser un comprobante MiPyME y está vacio", (MsgBoxStyle)((int)Constants.vbOKOnly + (int)Constants.vbExclamation), "Centrex");
                return;
            }

            var c = new comprobante();

            c.comprobanteField = txt_comprobante.Text;
            c.id_tipoComprobante = Conversions.ToInteger(cmb_tipoComprobante.SelectedValue);
            c.numeroComprobante = (int)Math.Round(nup_numero.Value);
            c.puntoVenta = (int)Math.Round(nup_puntoVenta.Value);
            c.esFiscal = rb_fiscal.Checked;
            c.esElectronica = rb_electronico.Checked;
            c.esManual = rb_manual.Checked;
            c.esPresupuesto = rb_presupuesto.Checked;
            c.activo = chk_activo.Checked;
            c.testing = chk_testing.Checked;
            c.maxItems = (int)Math.Round(nup_maxItems.Value);
            c.contabilizar = chk_contabilizar.Checked;
            c.mueveStock = chk_mueveStock.Checked;
            // .comprobanteRelacionado = cmb_comprobanteRelacionado.SelectedValue
            c.esMiPyME = chk_esMipyme.Checked;
            c.CBU_emisor = txt_CBU_emisor.Text;
            c.alias_CBU_emisor = txt_alias_CBU_emisor.Text;
            c.id_modoMiPyme = Conversions.ToInteger(cmb_modoMiPyme.SelectedValue);
            c.anula_MiPyME = Conversions.ToChar(txt_comprobanteAnulacion.Text);

            if (VariablesGlobales.edicion == true)
            {
                c.id_comprobante = VariablesGlobales.edita_comprobante.id_comprobante;
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
                cmb_tipoComprobante.SelectedValue = VariablesGlobales.id_tipoComprobante_default;
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
            var argcombo = cmb_tipoComprobante;
            generales.Cargar_Combo(ref argcombo, "SELECT id_tipoComprobante, comprobante_AFIP FROM tipos_comprobantes ORDER BY comprobante_AFIP ASC", VariablesGlobales.basedb, "comprobante_AFIP", Conversions.ToInteger("id_tipoComprobante"));
            cmb_tipoComprobante = argcombo;
            cmb_tipoComprobante.SelectedValue = VariablesGlobales.id_tipoComprobante_default;

            // Cargo todos los comprobantes asociados
            // cargar_combo(cmb_comprobanteRelacionado, "SELECT id_comprobante, comprobante FROM comprobantes ORDER BY comprobante ASC", basedb, "comprobante", "id_comprobante")
            // cmb_comprobanteRelacionado.Text = "Seleccione un comprobante asociado..."
            // cmb_comprobanteRelacionado.Enabled = False
            // cmb_comprobanteAsociado.SelectedValue = id_tipoComprobante_default

            // Cargo todos los modos de MiPyme
            var argcombo1 = cmb_modoMiPyme;
            generales.Cargar_Combo(ref argcombo1, "SELECT id_modoMiPyme, modo FROM sys_modoMiPyme ORDER BY id_modoMiPyme ASC", VariablesGlobales.basedb, "modo", Conversions.ToInteger("id_modoMiPyme"));
            cmb_modoMiPyme = argcombo1;
            cmb_modoMiPyme.SelectedValue = 1; // 1=No es MiPyme

            rb_electronico.Checked = true;
            chk_activo.Checked = true;

            if (VariablesGlobales.edicion == true | VariablesGlobales.borrado == true)
            {
                chk_secuencia.Enabled = false;
                {
                    var withBlock = VariablesGlobales.edita_comprobante;
                    txt_comprobante.Text = withBlock.comprobante;
                    cmb_tipoComprobante.SelectedValue = withBlock.id_tipoComprobante;
                    cmb_tipoComprobante.Enabled = false;
                    nup_numero.Value = withBlock.numeroComprobante;
                    nup_puntoVenta.Value = withBlock.puntoVenta;
                    rb_fiscal.Checked = withBlock.esFiscal;
                    rb_electronico.Checked = withBlock.esElectronica;
                    rb_manual.Checked = withBlock.esManual;
                    rb_presupuesto.Checked = withBlock.esPresupuesto;
                    chk_activo.Checked = withBlock.activo;
                    chk_testing.Checked = withBlock.testing;
                    nup_maxItems.Value = withBlock.maxItems;
                    chk_contabilizar.Checked = withBlock.contabilizar;
                    chk_mueveStock.Checked = withBlock.mueveStock;
                    chk_esMipyme.Checked = withBlock.esMiPyME;
                    // cmb_comprobanteRelacionado.SelectedValue = .comprobanteRelacionado
                    txt_CBU_emisor.Text = withBlock.CBU_emisor;
                    txt_alias_CBU_emisor.Text = withBlock.alias_CBU_emisor;
                    cmb_modoMiPyme.SelectedValue = withBlock.id_modoMiPyme;
                    txt_comprobanteAnulacion.Text = withBlock.anula_MiPyME;
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
                    if (borrarcomprobante(VariablesGlobales.edita_comprobante) == false)
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
    }
}

