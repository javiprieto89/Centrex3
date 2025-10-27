using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace Centrex
{
    public partial class grilla_resultados
    {
        public grilla_resultados()
        {
            InitializeComponent();
        }

        private static List<Tuple<string, bool>> OrdenAsc(string columna) =>
            new List<Tuple<string, bool>> { Tuple.Create(columna, true) };

        private void grilla_resultados_Load(object sender, EventArgs e)
        {
            var orden = OrdenAsc("Nombre");
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

        private void cmd_ejecutar_Click(object sender, EventArgs e)
        {
            var c = new consultaP();
            if (cmb_consultas.SelectedValue is null)
            {
                Interaction.MsgBox("Debe elegir una consulta para ejecutar", (MsgBoxStyle)((int)Constants.vbExclamation + (int)Constants.vbOKOnly), "Centrex");
                return;
            }

            c = consultas.info_consulta(Convert.ToInt32(cmb_consultas.SelectedValue));

            var argdataGrid = dg_view_resultados;
            int argnRegs = 0;
            int argtPaginas = 0;
            TextBox argtxtnPage = null;
            generales.cargar_datagrid(ref argdataGrid, c.consulta, VariablesGlobales.basedb, 0, ref argnRegs, ref argtPaginas, 1, ref argtxtnPage, "", "");
            dg_view_resultados = argdataGrid;
        }
    }
}
