using System;
using System.Collections.Generic;

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
            var argcombo = cmb_Comprobante;
            generales.Cargar_Combo(
      ref argcombo,
      entidad: "ComprobanteEntity",
                displaymember: "Comprobante",
           valuemember: "IdComprobante",
  predet: -1,
    soloActivos: true,
                filtros: new Dictionary<string, object> { ["EsElectronica"] = true },
            orden: new List<Tuple<string, bool>> { Tuple.Create("Comprobante", true) });
            cmb_Comprobante = argcombo;
            cmb_Comprobante.Text = "Seleccione un comprobante...";
        }

        private void cmd_consultar_Click(object sender, EventArgs e)
        {
            ComprobanteEntity c;

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

            c = comprobantes.info_comprobante(Conversions.ToInteger(cmb_Comprobante.SelectedValue));

            // Usar la función de factura_electronica directamente
            factura_electronica.Consultar_Comprobante(Conversions.ToInteger(txt_puntoVenta.Text), c.IdTipoComprobante, (int)Conversion.Int(txt_numeroComprobante.Text));
        }

        private void cmb_Comprobante_SelectionChangeCommitted(object sender, EventArgs e)
        {
            var comprobante = comprobantes.info_comprobante(Conversions.ToInteger(cmb_Comprobante.SelectedValue));
            txt_puntoVenta.Text = comprobante.PuntoVenta.ToString();
        }
    }
}
