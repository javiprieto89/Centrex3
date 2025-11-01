using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Centrex
{
    public partial class grilla_resultados : Form
    {
        public grilla_resultados()
        {
            InitializeComponent();
        }

        private static List<Tuple<string, bool>> OrdenAsc(string columna) =>
            new List<Tuple<string, bool>> { Tuple.Create(columna, true) };

        private void grilla_resultados_Load(object sender, EventArgs e)
        {
            try
            {
                var orden = OrdenAsc("Nombre");

                // Cargar combo usando la nueva función EF de "generales"
                generales.Cargar_Combo(
                    ref cmb_consultas,
                    entidad: "ConsultaPersonalizadaEntity",
                    displaymember: "Nombre",
                    valuemember: "IdConsulta",
                    predet: -1,
                    soloActivos: true,
                    filtros: null,
                    orden: orden);

                cmb_consultas.SelectedIndex = -1;
                cmb_consultas.Text = "Elija una consulta...";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar consultas: {ex.Message}", "Centrex", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void cmd_ejecutar_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmb_consultas.SelectedValue is null)
                {
                    MessageBox.Show("Debe elegir una consulta para ejecutar", "Centrex", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                int idConsulta = Convert.ToInt32(cmb_consultas.SelectedValue);

                using var ctx = new CentrexDbContext();
                var consulta = ctx.ConsultaPersonalizadaEntity.FirstOrDefault(x => x.IdConsulta == idConsulta);

                if (consulta is null)
                {
                    MessageBox.Show("No se encontró la consulta seleccionada.", "Centrex", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                // Obtener la "entidad" o alias de consulta
                string entidad = consulta.Consulta?.Trim() ?? "";
                if (string.IsNullOrEmpty(entidad))
                {
                    MessageBox.Show("La consulta seleccionada no tiene una entidad asociada.", "Centrex", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                // Construir el DataGridQueryResult con EF dinámico
                var result = DataGridQueryFactory.GetQueryForTable(ctx, entidad, historicoActivo: true);
                result.GridName = "grilla_resultados";

                // Cargar la grilla usando la nueva función centralizada
                await LoadDataGridDynamic.LoadDataGridWithEntityAsync(dg_view_resultados, result);

                // Mostrar total de registros
                lbl_registros.Text = $"Total: {dg_view_resultados.Rows.Count}";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al ejecutar la consulta: " + ex.Message, "Centrex", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
