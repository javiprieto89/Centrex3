using System;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace Centrex
{
    public partial class info_fc
    {
        public info_fc()
        {
            InitializeComponent();
        }
        private void info_fc_Load(object sender, EventArgs e)
        {
            // cargar_combo(cmb_tipoComprobante, "SELECT id_tipoComprobante, comprobante_AFIP FROM tipos_comprobantes ORDER BY comprobante_AFIP ASC", basedb, "comprobante_AFIP", "id_tipoComprobante")
            var argcombo = cmb_Comprobante;
            generales.Cargar_Combo(ref argcombo, "SELECT id_comprobante, comprobante FROM comprobantes WHERE esElectronica = 1 AND activo = 1 ORDER BY comprobante ASC", VariablesGlobales.basedb, "comprobante", Conversions.ToInteger("id_comprobante"));
            cmb_Comprobante = argcombo;
            cmb_Comprobante.Text = "Seleccione un comprobante...";
        }

        private void cmd_consultar_Click(object sender, EventArgs e)
        {
            comprobante c;

            if (cmb_Comprobante.Text == "Seleccione un comprobante...")
            {
                Interaction.MsgBox("Debe seleccionar un comprobante sobre el cual desesa realizar la consulta.", (MsgBoxStyle)((int)Constants.vbInformation + (int)Constants.vbOKOnly), "Centrex");
                return;
            }
            else if (string.IsNullOrEmpty(txt_numeroComprobante.Text))
            {
                Interaction.MsgBox("Debe ingresar un número de comprobante sobre el cual desesa realizar la consulta.", (MsgBoxStyle)((int)Constants.vbInformation + (int)Constants.vbOKOnly), "Centrex");
                return;
            }

            c = comprobantes.info_comprobante(Conversions.ToString(cmb_Comprobante.SelectedValue));


            Consultar_Comprobante(txt_puntoVenta.Text, c.id_tipoComprobante, txt_numeroComprobante.Text);
        }

        private void cmb_Comprobante_SelectionChangeCommitted(object sender, EventArgs e)
        {
            txt_puntoVenta.Text = comprobantes.info_comprobante(Conversions.ToString(cmb_Comprobante.SelectedValue)).puntoVenta.ToString();
        }
    }
}
