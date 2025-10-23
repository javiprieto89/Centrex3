using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using Centrex.Models;

namespace Centrex
{

    public static class combos_ef
    {
        // Función genérica base (mantiene tu firma original)

        // Versión moderna (para listas genéricas, LINQ, EF, etc.)
        public static DataTable ToDataTable<T>(IEnumerable<T> data)
        {
            var dt = new DataTable();
            if (data is null)
                return dt;

            PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var prop in props)
            {
                var propType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                dt.Columns.Add(prop.Name, propType);
            }

            foreach (var item in data)
            {
                var row = dt.NewRow();
                foreach (var prop in props)
                    row[prop.Name] = prop.GetValue(item, null) ?? DBNull.Value;
                dt.Rows.Add(row);
            }

            return dt;
        }

        // Versión de compatibilidad (para KeyValuePair, usada en combos)
        public static DataTable ToDataTable<TKey, TValue>(IEnumerable<KeyValuePair<TKey, TValue>> data, string valueMember = "Key", string displayMember = "Value")
        {

            var dt = new DataTable();
            dt.Columns.Add(valueMember, typeof(TKey));
            dt.Columns.Add(displayMember, typeof(TValue));

            foreach (var kv in data)
                dt.Rows.Add(kv.Key, kv.Value);

            return dt;
        }


        public static DataTable ComboTiposItems()
        {
            using (var ctx = new CentrexDbContext())
            {
                var list = ctx.TiposItems.Where(t => t.activo).OrderBy(t => t.tipo).Select(t => new KeyValuePair<int, string>(t.IdTipo, t.tipo)).ToList();
                return ToDataTable(list, "id_tipo", "tipo");
            }
        }

        public static DataTable ComboMarcas()
        {
            using (var ctx = new CentrexDbContext())
            {
                var list = ctx.Marcas.OrderBy(m => m.marca).Select(m => new KeyValuePair<int, string>(m.IdMarca, m.marca)).ToList();
                return ToDataTable(list, "id_marca", "marca");
            }
        }

        public static DataTable ComboProveedores()
        {
            using (var ctx = new CentrexDbContext())
            {
                var list = ctx.Proveedores.Where(p => p.activo).OrderBy(p => p.razon_social).Select(p => new KeyValuePair<int, string>(p.IdProveedor, p.razon_social)).ToList();
                return ToDataTable(list, "id_proveedor", "razon_social");
            }
        }

        public static DataTable ComboImpuestosIVAActivos()
        {
            using (var ctx = new CentrexDbContext())
            {
                var list = ctx.Impuestos.Where(i => i.activo & i.nombre.ToLower().Contains("iva")).OrderByDescending(i => i.nombre).Select(i => new KeyValuePair<int, string>(i.IdImpuesto, i.nombre)).ToList();

                return ToDataTable(list, "id_impuesto", "nombre");
            }
        }
    }

    // -------------------- EXTENSION DE COMBOS PARA OTROS FORMULARIOS --------------------
    public static class combos_ef_extra
    {
        public static DataTable ComboItems()
        {
            using (var ctx = new CentrexDbContext())
            {
                var list = ctx.Items.OrderBy(i => i.item).Select(i => new KeyValuePair<int, string>(i.IdItem, i.descript)).ToList();
                var dt = new DataTable();
                dt.Columns.Add("id_item", typeof(int));
                dt.Columns.Add("descript", typeof(string));
                foreach (var kv in list)
                    dt.Rows.Add(((dynamic)kv).Key, ((dynamic)kv).Value);
                return dt;
            }
        }

        public static DataTable ComboClientesActivos()
        {
            using (var ctx = new CentrexDbContext())
            {
                var list = ctx.Clientes.Where(c => c.activo).OrderBy(c => c.RazonSocial).Select(c => new { Id = c.IdCliente, Nombre = c.RazonSocial }).ToList();
                var dt = new DataTable();
                dt.Columns.Add("id_cliente", typeof(int));
                dt.Columns.Add("razon_social", typeof(string));
                foreach (var r in list)
                    dt.Rows.Add(((dynamic)r).Id, ((dynamic)r).Nombre);
                return dt;
            }
        }

        public static DataTable ComboComprobantesActivos()
        {
            using (var ctx = new CentrexDbContext())
            {
                var list = ctx.Comprobantes.OrderBy(c => c.comprobante).Select(c => new { Id = c.IdComprobante, Nombre = c.comprobante }).ToList();
                var dt = new DataTable();
                dt.Columns.Add("id_comprobante", typeof(int));
                dt.Columns.Add("comprobante", typeof(string));
                foreach (var r in list)
                    dt.Rows.Add(((dynamic)r).Id, ((dynamic)r).Nombre);
                return dt;
            }
        }

        public static DataTable ComboCcCliente(int idCliente)
        {
            using (var ctx = new CentrexDbContext())
            {
                var list = ctx.CcClientes.Where(cc => cc.IdCliente == idCliente).OrderBy(cc => cc.IdCc).Select(cc => new { Id = cc.IdCc, Nombre = cc.IdCc }).ToList();

                var dt = new DataTable();
                dt.Columns.Add("id_cc", typeof(int));
                dt.Columns.Add("nombre", typeof(string));
                foreach (var r in list)
                    dt.Rows.Add(((dynamic)r).Id, ((dynamic)r).Nombre.ToString());
                return dt;
            }
        }
    }

    public static class combos_alias
    {
        // Alias de categorías para mantener coherencia con la UI
        public static DataTable ComboCategoriasItems()
        {
            // Usa TiposItems como categorías por ahora
            return combos_ef.ComboTiposItems();
        }
    }
}
