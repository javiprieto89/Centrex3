using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace Centrex
{
    public partial class frm_ultimo_comprobante
    {
        public frm_ultimo_comprobante()
        {
            InitializeComponent();
        }
        private void frm_ultimo_comprobante_Load(object sender, EventArgs e)
        {
            var filtros = new Dictionary<string, object>
            {
                ["EsElectronica"] = true
            };
            var orden = new List<Tuple<string, bool>> { Tuple.Create("Comprobante", true) };
            generales.Cargar_Combo(
                ref cmb_comprobante,
                entidad: "ComprobanteEntity",
                displaymember: "Comprobante",
                valuemember: "IdComprobante",
                predet: -1,
                soloActivos: true,
                filtros: filtros,
                orden: orden);
            cmb_comprobante.SelectedIndex = -1;
        }

        private void cmb_comprobante_KeyPress(object sender, KeyPressEventArgs e)
        {
            // e.KeyChar = ""
        }

        private void cmd_ok_Click(object sender, EventArgs e)
        {
            if (cmb_comprobante.SelectedValue is null)
            {
                Interaction.MsgBox("Debe seleccionar un comprobante para ejecutar la consulta", (MsgBoxStyle)((int)Constants.vbExclamation + (int)Constants.vbOKOnly), "Centrex");
                return;
            }

            var c = new ComprobanteEntity();
            int ultimoComprobante;
            c = comprobantes.info_comprobante(Conversions.ToInteger(cmb_comprobante.SelectedValue));

            // NO usar Try-Catch aquí para permitir que las excepciones se propaguen
            // y se muestre el formulario de errores detallados desde WSFEv1 o WSAA
            ultimoComprobante = factura_electronica.ConsultaUltimoComprobante(c.PuntoVenta, c.IdTipoComprobante, c.NumeroComprobante, c.Testing);

            Interaction.MsgBox("El último número de comprobante para el documento: " + c.Comprobante + " con el punto de venta: " + c.PuntoVenta.ToString() + " es: " + ultimoComprobante.ToString(), (MsgBoxStyle)((int)Constants.vbInformation + (int)Constants.vbOKOnly), "Centrex");
        }

        private void frm_ultimo_comprobante_FormClosed(object sender, FormClosedEventArgs e)
        {
            closeandupdate(this);
        }
    }
}
