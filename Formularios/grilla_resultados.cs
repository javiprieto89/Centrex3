using System;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace Centrex
{
    public partial class grilla_resultados
    {
        public grilla_resultados()
        {
            InitializeComponent();
        }

        private void grilla_resultados_Load(object sender, EventArgs e)
        {
            var argcombo = cmb_consultas;
            generales.Cargar_Combo(ref argcombo, "SELECT id_consulta, nombre FROM consultas_personalizadas ORDER BY nombre ASC", VariablesGlobales.basedb, "nombre", Conversions.ToInteger("id_consulta"));
            cmb_consultas = argcombo;
            cmb_consultas.SelectedValue = 0;
            cmb_consultas.Text = "Elija una consulta...";
        }

        private void cmd_ejecutar_Click(object sender, EventArgs e)
        {
            var c = new consultaP();
            if (Conversions.ToBoolean(Operators.ConditionalCompareObjectEqual(cmb_consultas.SelectedValue, 0, false)))
            {
                Interaction.MsgBox("Debe elegir una consulta para ejecutar", (MsgBoxStyle)((int)Constants.vbExclamation + (int)Constants.vbOKOnly), "Centrex");
                return;
            }

            c = consultas.info_consulta(Conversions.ToInteger(cmb_consultas.SelectedValue));

            var argdataGrid = dg_view_resultados;
            int argnRegs = 0;
            int argtPaginas = 0;
            TextBox argtxtnPage = null;
            generales.cargar_datagrid(ref argdataGrid, c.consulta, VariablesGlobales.basedb, 0, ref argnRegs, ref argtPaginas, 1, ref argtxtnPage, "", "");
            dg_view_resultados = argdataGrid;
        }
    }
}
