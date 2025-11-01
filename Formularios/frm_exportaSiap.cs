using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Centrex
{
    public partial class frm_exportaSiap : Form
    {
        public frm_exportaSiap()
        {
            InitializeComponent();
        }

        private void frm_exportaSiap_Load(object sender, EventArgs e)
        {
            try
            {
                using var ctx = new CentrexDbContext();

                // Cargar las consultas personalizadas activas
                var consultas = ctx.ConsultaPersonalizadaEntity
                    .Where(c => c.Activo == true)
                    .OrderBy(c => c.Nombre)
                    .Select(c => new { c.IdConsulta, c.Nombre })
                    .ToList();

                cmb_consultas.DataSource = consultas;
                cmb_consultas.DisplayMember = "Nombre";
                cmb_consultas.ValueMember = "IdConsulta";
                cmb_consultas.SelectedIndex = -1;

                pExportXLS.Enabled = false;
                pExportTXT.Enabled = false;
            }
            catch (Exception ex)
            {
                Interaction.MsgBox($"Error al cargar consultas SIAP: {ex.Message}", MsgBoxStyle.Critical, "Centrex");
            }
        }

        private bool checkFilters()
        {
            bool ok = true;

            if (cmb_consultas.SelectedValue is null)
            {
                Interaction.MsgBox("Debe elegir una consulta para ejecutar",
                    (MsgBoxStyle)((int)MsgBoxStyle.Exclamation | (int)MsgBoxStyle.OkOnly), "Centrex");
                ok = false;
            }

            if (dtp_desde.Value.Date > dtp_hasta.Value.Date)
            {
                Interaction.MsgBox("La fecha 'Desde' no puede ser superior a 'Hasta'",
                    MsgBoxStyle.Exclamation, "Centrex");
                ok = false;
            }

            return ok;
        }

        private void cmb_consultas_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                if (cmb_consultas.SelectedValue is null) return;

                // Por ahora ambas opciones se habilitan
                pExportXLS.Enabled = true;
                pExportTXT.Enabled = true;
            }
            catch (Exception ex)
            {
                Interaction.MsgBox("Error al seleccionar la consulta: " + ex.Message, MsgBoxStyle.Critical, "Centrex");
            }
        }

        private void pExportTXT_Click(object sender, EventArgs e)
        {
            if (!checkFilters()) return;

            try
            {
                int idConsulta = Conversions.ToInteger(cmb_consultas.SelectedValue);

                using var ctx = new CentrexDbContext();
                var consulta = ctx.ConsultaPersonalizadaEntity.FirstOrDefault(c => c.IdConsulta == idConsulta);

                if (consulta == null)
                {
                    Interaction.MsgBox("No se encontró la consulta seleccionada.", MsgBoxStyle.Exclamation, "Centrex");
                    return;
                }

                DateTime? desde = chk_desde.Checked ? dtp_desde.Value.Date : (DateTime?)null;
                DateTime? hasta = chk_hasta.Checked ? dtp_hasta.Value.Date : (DateTime?)null;

                string resultado = generales.ejecutarSQLconResultado(consulta.Consulta, desde, hasta);

                using SaveFileDialog save = new SaveFileDialog
                {
                    AddExtension = true,
                    Filter = "Text Files|*.txt",
                    DefaultExt = "txt",
                    InitialDirectory = @"C:\"
                };

                if (save.ShowDialog() != DialogResult.OK) return;

                generales.guardarArchivoTexto(save.FileName, resultado);

                Interaction.MsgBox($"Archivo exportado a: {save.FileName}",
                    MsgBoxStyle.Information, "Centrex");
            }
            catch (Exception ex)
            {
                Interaction.MsgBox("Error al exportar TXT: " + ex.Message, MsgBoxStyle.Critical, "Centrex");
            }
        }

        private void pExportXLS_Click(object sender, EventArgs e)
        {
            if (!checkFilters()) return;

            try
            {
                int idConsulta = Conversions.ToInteger(cmb_consultas.SelectedValue);

                using var ctx = new CentrexDbContext();
                var consulta = ctx.ConsultaPersonalizadaEntity.FirstOrDefault(c => c.IdConsulta == idConsulta);

                if (consulta == null)
                {
                    Interaction.MsgBox("No se encontró la consulta seleccionada.", MsgBoxStyle.Exclamation, "Centrex");
                    return;
                }

                DateTime? desde = chk_desde.Checked ? dtp_desde.Value.Date : (DateTime?)null;
                DateTime? hasta = chk_hasta.Checked ? dtp_hasta.Value.Date : (DateTime?)null;

                // Obtener los resultados (máx. 500 registros)
                string entidad = consulta.Consulta;
                var texto = generales.ejecutarSQLconResultado(entidad, desde, hasta);

                // Convertir a DataGridView temporal para exportar (reutiliza tu función)
                DataGridView dgtemp = new DataGridView();
                dgtemp.Columns.Add("Resultado", "Resultado");
                dgtemp.Rows.Add(texto.Split('\n').Select(line => new object[] { line }).ToArray());

                using SaveFileDialog save = new SaveFileDialog
                {
                    AddExtension = true,
                    Filter = "Excel Files|*.xlsx",
                    DefaultExt = "xlsx",
                    InitialDirectory = @"C:\"
                };

                if (save.ShowDialog() != DialogResult.OK) return;

                generales.exportarExcel(dgtemp, save.FileName);

                Interaction.MsgBox($"Archivo exportado a Excel: {save.FileName}",
                    MsgBoxStyle.Information, "Centrex");
            }
            catch (Exception ex)
            {
                Interaction.MsgBox("Error al exportar Excel: " + ex.Message, MsgBoxStyle.Critical, "Centrex");
            }
        }

        private void cmd_cerrar_Click(object sender, EventArgs e)
        {
            generales.closeandupdate(this);
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
