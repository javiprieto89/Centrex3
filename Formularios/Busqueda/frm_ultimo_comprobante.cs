using System;
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
            var argcombo = cmb_comprobante;
            generales.Cargar_Combo(ref argcombo, "SELECT id_comprobante, comprobante FROM comprobantes WHERE activo = '1' AND esElectronica = '1' ORDER BY comprobante ASC", VariablesGlobales.basedb, "comprobante", Conversions.ToInteger("id_comprobante"));
            cmb_comprobante = argcombo;
            cmb_comprobante.Text = "Seleccione un comprobante...";
        }

        private void cmb_comprobante_KeyPress(object sender, KeyPressEventArgs e)
        {
            // e.KeyChar = ""
        }

        private void cmd_ok_Click(object sender, EventArgs e)
        {
            if (cmb_comprobante.Text == "Seleccione un comprobante...")
            {
                Interaction.MsgBox("Debe seleccionar un comprobante para ejecutar la consulta", (MsgBoxStyle)((int)Constants.vbExclamation + (int)Constants.vbOKOnly), "Centrex");
                return;
            }

            var c = new comprobante();
            int ultimoComprobante;
            c = comprobantes.info_comprobante(Conversions.ToString(cmb_comprobante.SelectedValue));

            // NO usar Try-Catch aquí para permitir que las excepciones se propaguen
            // y se muestre el formulario de errores detallados desde WSFEv1 o WSAA
            ultimoComprobante = consultaUltimoComprobante(c.puntoVenta, c.id_tipoComprobante, c.numeroComprobante);

            Interaction.MsgBox("El último número de comprobante para el documento: " + c.comprobanteField + " con el punto de venta: " + c.puntoVenta.ToString() + " es: " + ultimoComprobante.ToString(), (MsgBoxStyle)((int)Constants.vbInformation + (int)Constants.vbOKOnly), "Centrex");
        }

        private void frm_ultimo_comprobante_FormClosed(object sender, FormClosedEventArgs e)
        {
            closeandupdate(this);
        }
    }
}
