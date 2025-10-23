using Centrex.Helpers;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Centrex
{
    public partial class frm_exportaSiap
    {
        public frm_exportaSiap()
        {
            InitializeComponent();
        }
        private void frm_exportaSiap_Load(object sender, EventArgs e)
        {
            // Cargo todas las consultas
            var argcombo = cmb_consultas;
            generales.Cargar_Combo(ref argcombo, "SELECT id_consultaSiap, nombre FROM consultas_SIAP ORDER BY nombre ASC", VariablesGlobales.basedb, "nombre", Conversions.ToInteger("id_consultaSiap"));
            cmb_consultas = argcombo;
            cmb_consultas.SelectedValue = 0;
            cmb_consultas.Text = "Elija una consulta...";

            pExportXLS.Enabled = false;
            pExportTXT.Enabled = false;
        }

        private bool checkFilters()
        {
            bool ok = true;

            if (Conversions.ToBoolean(Operators.ConditionalCompareObjectEqual(cmb_consultas.SelectedValue, 0, false)))
            {
                Interaction.MsgBox("Debe elegir una consulta para ejecutar", (MsgBoxStyle)((int)Constants.vbExclamation + (int)Constants.vbOKOnly), "Centrex");
                ok = false;
            }

            if (dtp_desde.Value.Date > dtp_hasta.Value.Date)
            {
                Interaction.MsgBox("La fecha 'Desde' no puede ser superior a 'Hasta'", Constants.vbExclamation, "Centrex");
                ok = false;
            }

            return ok;
        }

        private string cabecera(DateTime fecha_desde, DateTime fecha_hasta, int id_comprobante = -1, int id_item = -1, int id_cliente = -1)
        {
            string cabeceraStr;

            cabeceraStr = "DECLARE @desde AS DATE " + "DECLARE @hasta AS DATE ";

            if (chk_desde.Checked)
            {
                cabeceraStr += " SET @desde = '" + fecha_desde.ToString("yyyy/MM/dd") + "' ";
            }
            else
            {
                cabeceraStr += " SET @desde = NULL ";
            }

            if (chk_hasta.Checked)
            {
                cabeceraStr += " SET @hasta = '" + fecha_hasta.ToString("yyyy/MM/dd") + "' ";
            }
            else
            {
                cabeceraStr += " SET @hasta = NULL ";
            }

            if (!chk_desde.Checked & !chk_hasta.Checked)
            {
                cabeceraStr += " SET @desde = NULL SET @hasta = NULL ";
            }

            return cabeceraStr;
        }

        private void pExportTXT_Click(object sender, EventArgs e)
        {
            // Validar filtros
            if (!checkFilters())
                return;

            try
            {
                // 🔹 Obtener definición de la consulta (de tu tabla consultasSIAP)
                var c = consultasSIAP.info_consultaSIAP(Convert.ToInt32(cmb_consultas.SelectedValue));

                // 🔹 Determinar filtros de fecha
                DateTime? desde = chk_desde.Checked ? dtp_desde.Value.Date : null;
                DateTime? hasta = chk_hasta.Checked ? dtp_hasta.Value.Date : null;

                // 🔹 Construir objeto de filtro dinámico
                var filtro = ConsultasDinamicas.CrearCabecera(
                    entidad: c.NombreEntidad,               // Por ejemplo: "ComprobanteEntity"
                    campoFecha: c.CampoFecha ?? "Fecha",    // Campo fecha principal a usar
                    fecha_desde: desde,
                    fecha_hasta: hasta,
                    filtrosExtras: new Dictionary<string, object>
                    {
                // Si tu consulta tiene parámetros adicionales, los agregás acá
                { "IdCliente", c.IdCliente },
                { "IdItem", c.IdItem },
                { "IdComprobante", c.IdComprobante }
                    }
                );

                // 🔹 Ejecutar consulta dinámica con EF Core (sin SQL)
                string resultado = generales.ejecutarSQLconResultado(filtro);

                // 🔹 Seleccionar ruta destino
                using (SaveFileDialog save = new SaveFileDialog())
                {
                    save.AddExtension = true;
                    save.CheckFileExists = false;
                    save.CheckPathExists = true;
                    save.Filter = "Text Files|*.txt";
                    save.DefaultExt = "txt";
                    save.InitialDirectory = @"C:\";

                    if (save.ShowDialog() != DialogResult.OK || string.IsNullOrEmpty(save.FileName))
                    {
                        Interaction.MsgBox("Exportación cancelada.", Constants.vbInformation, "Centrex");
                        return;
                    }

                    // 🔹 Guardar resultado en archivo
                    generales.guardarArchivoTexto(save.FileName, resultado);

                    Interaction.MsgBox("Archivo exportado a: " + save.FileName,
                        (MsgBoxStyle)(Constants.vbInformation + Constants.vbOKOnly), "Centrex");

                    closeandupdate(this);
                }
            }
            catch (Exception ex)
            {
                Interaction.MsgBox("Error durante la exportación: " + ex.Message, Constants.vbCritical, "Centrex");
            }
        }

        private void cmb_consultas_SelectionChangeCommitted(object sender, EventArgs e)
        {
            consultaSIAP c;

            c = consultasSIAP.info_consultaSIAP(Conversions.ToInteger(cmb_consultas.SelectedValue));

            if (c.excel)
            {
                pExportXLS.Enabled = true;
                pExportTXT.Enabled = false;

                pExportXLS.Image = My.Resources.Resources.iconoExcel;
                pExportTXT.Image = My.Resources.Resources.iconotxtByN;
            }
            else if (c.txt)
            {
                pExportXLS.Enabled = false;
                pExportTXT.Enabled = true;

                pExportXLS.Image = My.Resources.Resources.iconoExcelByN;
                pExportTXT.Image = My.Resources.Resources.iconotxt;
            }
        }

        private void pExportXLS_Click(object sender, EventArgs e)
        {
            // MsgBox("Hola")
        }

        private void cmd_cerrar_Click(object sender, EventArgs e)
        {
            closeandupdate(this);
        }

        private void chk_desde_CheckedChanged(object sender, EventArgs e)
        {
            dtp_desde.Enabled = chk_desde.Checked;
        }

        private void chk_hasta_CheckedChanged(object sender, EventArgs e)
        {
            dtp_hasta.Enabled = chk_hasta.Checked;
        }
    }
}
