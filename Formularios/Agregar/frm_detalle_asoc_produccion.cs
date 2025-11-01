using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Centrex
{
    public partial class frm_detalle_asoc_produccion
    {
        private int id_item;
        private int id_produccion;
        private DataTable dt_cantidades_items_asociados = new DataTable();

        public frm_detalle_asoc_produccion()
        {

            // Esta llamada es exigida por el diseñador.
            InitializeComponent();

            // Agregue cualquier inicialización después de la llamada a InitializeComponent().

        }

        public frm_detalle_asoc_produccion(int _id_item, int _id_produccion = -1)
        {

            // Esta llamada es exigida por el diseñador.
            InitializeComponent();

            // Agregue cualquier inicialización después de la llamada a InitializeComponent().
            id_item = _id_item;
            id_produccion = _id_produccion;
        }
        private void frm_detalle_asoc_produccion_Load(object sender, EventArgs e)
        {
            var resultado = (id_produccion != -1)
                ? asocitems.Traer_Cantidades_Enviadas_Items_Asociados_Default_Produccion(id_item.ToString(), id_produccion)
                : asocitems.Traer_Cantidades_Enviadas_Items_Asociados_Default_Produccion(id_item.ToString());

            // 🔹 Usa tu ToDataTable<T> genérico con el tipo real del resultado
            var dataList = resultado?.DataMaterializada as IEnumerable<object>;
            DataTable dt = new DataTable();

            if (dataList != null && dataList.Any())
            {
                // Tomamos el tipo del primer elemento para inferir T
                var firstType = dataList.First().GetType();
                var genericMethod = typeof(generales).GetMethod("ToDataTable", new[] { typeof(IEnumerable<>).MakeGenericType(firstType) });
                if (genericMethod != null)
                    dt = (DataTable)genericMethod.Invoke(null, new object[] { dataList });
            }

            dg_view.DataSource = dt;

            if (id_produccion != -1)
            {
                dg_view.ReadOnly = true;
                cmd_ok.Enabled = false;
            }
        }


        private void cmd_ok_Click(object sender, EventArgs e)
        {
            foreach (DataRow row in dt_cantidades_items_asociados.Rows)
                producciones.addItemAsocProducciontmp(Conversions.ToInteger(row[4]), Conversions.ToInteger(row[5]), Conversions.ToInteger(row[6]), Conversions.ToInteger(row[3]));

            closeandupdate(this);
        }
    }
}
