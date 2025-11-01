using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Centrex.Funciones
{

    /// <summary>
    /// Módulo generales completamente migrado a Entity Framework
    /// Sin código SQL directo - Solo Entity Framework
    /// </summary>
    public static class generales
    {

        // ************************************* FUNCIONES GENERALES CON ENTITY FRAMEWORK PURO *****************************
        /// <summary>
        /// Ejecuta operaciones usando Entity Framework
        /// </summary>
        public static bool ejecutarSQL(string sqlstr)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(sqlstr))
                {
                    Interaction.MsgBox("La consulta SQL está vacía o es nula.", MsgBoxStyle.Exclamation, "Centrex");
                    return false;
                }

                using (var context = new CentrexDbContext())
                {
                    // 🔹 Normalizamos la consulta a mayúsculas solo para análisis
                    string sqlUpper = sqlstr.Trim().ToUpperInvariant();

                    // 🔹 Si es un SELECT COUNT, no ejecutamos nada destructivo
                    if (sqlUpper.StartsWith("SELECT COUNT"))
                    {
                        var result = context.Database.ExecuteSqlRaw(sqlstr);
                        return result >= 0;
                    }

                    // 🔹 Si es INSERT, UPDATE o DELETE, ejecutamos directamente
                    if (sqlUpper.StartsWith("INSERT") ||
                        sqlUpper.StartsWith("UPDATE") ||
                        sqlUpper.StartsWith("DELETE") ||
                        sqlUpper.StartsWith("TRUNCATE") ||
                        sqlUpper.StartsWith("ALTER") ||
                        sqlUpper.StartsWith("DROP") ||
                        sqlUpper.StartsWith("EXEC") ||
                        sqlUpper.StartsWith("BACKUP"))
                    {
                        context.Database.ExecuteSqlRaw(sqlstr);
                        return true;
                    }

                    // 🔹 Si es otro tipo de comando SQL (por ejemplo, DBCC)
                    context.Database.ExecuteSqlRaw(sqlstr);
                    return true;
                }
            }
            catch (Exception ex)
            {
                Interaction.MsgBox("Error ejecutando SQL: " + ex.Message, MsgBoxStyle.Critical, "Centrex");
                return false;
            }
        }


        /// <summary>
        /// Función de compatibilidad usando Entity Framework
        /// </summary>
        //public static int FnExecSQL(string sqlstr)
        //{
        //    try
        //    {
        //        using (var context = new CentrexDbContext())
        //        {
        //            // Para conteos, usar EF
        //            if (sqlstr.ToUpper().Contains("SELECT COUNT"))
        //            {
        //                if (sqlstr.Contains("clientes"))
        //                {
        //                    return context.ClienteEntity.Count();
        //                }
        //                else if (sqlstr.Contains("proveedores"))
        //                {
        //                    return context.ProveedorEntity.Count();
        //                }
        //                else if (sqlstr.Contains("items"))
        //                {
        //                    return context.ItemEntity.Count();
        //                }
        //                else if (sqlstr.Contains("pedidos"))
        //                {
        //                    return context.PedidoEntity.Count();
        //                }
        //                else if (sqlstr.Contains("cobros"))
        //                {
        //                    return context.CobroEntity.Count();
        //                }
        //            }
        //            return 0;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return 0;
        //    }
        //}



        /// <summary>
        /// Búsqueda usando Entity Framework puro
        /// </summary>
        //public static string sqlstrbuscar(string tbl, string txtsearch, bool historicoActivo)
        //{
        //    try
        //    {
        //        using (var context = new CentrexDbContext())
        //        {
        //            switch (tbl ?? "")
        //            {
        //                case var @case when @case == "clientes":
        //                    {
        //                        var clientes = context.ClienteEntity.Where(c => c.Activo == historicoActivo && (c.IdCliente.ToString().Contains(txtsearch) || c.RazonSocial.Contains(txtsearch) || c.NombreFantasia.Contains(txtsearch) || c.TaxNumber.Contains(txtsearch) || c.Contacto.Contains(txtsearch) || c.Telefono.Contains(txtsearch) || c.Celular.Contains(txtsearch) || c.Email.Contains(txtsearch) || c.DireccionFiscal.Contains(txtsearch) || c.LocalidadFiscal.Contains(txtsearch) || c.CpFiscal.Contains(txtsearch) || c.DireccionEntrega.Contains(txtsearch) || c.LocalidadEntrega.Contains(txtsearch) || c.CpEntrega.Contains(txtsearch))).OrderBy(c => c.RazonSocial).ToList();
        //                        return ConvertToDataGridString<ClienteEntity>(clientes, "clientes_search");
        //                    }

        //                case var case1 when case1 == "proveedores":
        //                    {
        //                        var proveedores = context.ProveedorEntity.Where(p => p.Activo == historicoActivo && (p.IdProveedor.ToString().Contains(txtsearch) || p.RazonSocial.Contains(txtsearch) || p.TaxNumber.Contains(txtsearch) || p.Contacto.Contains(txtsearch) || p.Telefono.Contains(txtsearch) || p.Celular.Contains(txtsearch) || p.Email.Contains(txtsearch) || p.DireccionFiscal.Contains(txtsearch) || p.LocalidadFiscal.Contains(txtsearch) || p.CpFiscal.Contains(txtsearch) || p.DireccionEntrega.Contains(txtsearch) || p.LocalidadEntrega.Contains(txtsearch) || p.CpEntrega.Contains(txtsearch))).OrderBy(p => p.RazonSocial).ToList();
        //                        return ConvertToDataGridString<ProveedorEntity>(proveedores, "proveedores_search");
        //                    }

        //                case var case2 when case2 == "items":
        //                case "itemsImpuestosItems":
        //                    {
        //                        var items = context.ItemImpuestoEntity.Where(i => i.Activo == historicoActivo && (i.IdItemNavigation.ToString().Contains(txtsearch) || i.IdItemNavigation.Item.Contains(txtsearch) || i.IdItemNavigation.Descript.Contains(txtsearch) || i.IdItemNavigation.Cantidad.ToString().Contains(txtsearch) || i.IdItemNavigation.Costo.ToString().Contains(txtsearch) || i.IdItemNavigation.PrecioLista.ToString().Contains(txtsearch) || i.IdItemNavigation.Factor.ToString().Contains(txtsearch))).OrderBy(i => i.IdItemNavigation.Item).ToList();
        //                        return ConvertToDataGridString<ItemImpuestoEntity>(items, "items_search");
        //                    }

        //                default:
        //                    {
        //                        return "ef_error_unknown_table";
        //                    }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return "ef_error_" + ex.Message;
        //    }
        //}

        /// <summary>
        /// Función simple para obtener fecha actual
        /// </summary>
        public static string Hoy()
        {
            return DateTime.Now.ToString("dd/MM/yyyy");
        }

        /// <summary>
        /// Borra tabla usando Entity Framework puro
        /// </summary>

        public static byte borrartbl(string tbl, bool reseed = false)
        {
            try
            {
                using (var context = new CentrexDbContext())
                {
                    var entityInfo = ResolveEntitySet(context, tbl);
                    if (entityInfo is null)
                    {
                        Interaction.MsgBox($"La entidad '{tbl}' no existe en el contexto.", Constants.vbExclamation, "Centrex");
                        return 0;
                    }

                    // Cargar y eliminar todos los registros
                    var registros = entityInfo.Query.ToList();
                    if (registros.Count > 0)
                    {
                        context.RemoveRange(registros);
                        context.SaveChanges();
                    }

                    // Reseed opcional (si la tabla tiene identidad)
                    if (reseed)
                    {
                        try
                        {
                            string tableName = entityInfo.TableName ?? tbl;
                            string reseedSql = $"DBCC CHECKIDENT ('[{tableName}]', RESEED, 0);";
                            context.Database.ExecuteSqlRaw(reseedSql);
                        }
                        catch
                        {
                            // Ignorar si la tabla no tiene columna IDENTITY
                        }
                    }

                    return 1;
                }
            }
            catch (Exception ex)
            {
                Interaction.MsgBox($"Error al vaciar la tabla '{tbl}': {ex.Message}", Constants.vbCritical, "Error en borrartbl");
                return 0;
            }
        }

        public static void activaitems(string tabla)
        {
            try
            {
                using var context = new CentrexDbContext();
                var entityInfo = ResolveEntitySet(context, tabla);
                if (entityInfo is null)
                {
                    Interaction.MsgBox($"La entidad '{tabla}' no existe en el contexto.", Constants.vbExclamation, "Centrex");
                    return;
                }

                bool cambios = false;
                foreach (var registro in entityInfo.Query)
                {
                    var prop = registro.GetType().GetProperty("Activo", BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                    if (prop != null && prop.CanWrite)
                    {
                        var valor = prop.GetValue(registro);
                        if (valor is bool activoActual && !activoActual)
                        {
                            prop.SetValue(registro, true);
                            cambios = true;
                        }
                    }
                }

                if (cambios)
                {
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Interaction.MsgBox("Error al activar ítems: " + ex.Message, MsgBoxStyle.Exclamation, "Centrex");
            }
        }


        /// <summary>
        /// Carga ComboBox usando Entity Framework
        /// </summary>
        /// 
        public static int Cargar_Combo(
                ref ComboBox combo,
                string entidad,
                string displaymember,
                string valuemember,
                int predet = 0,
                bool soloActivos = true,
                Dictionary<string, object>? filtros = null,
                List<Tuple<string, bool>>? orden = null)
        {
            try
            {
                using var context = new CentrexDbContext();

                var ctxType = context.GetType();
                var dbSetProp = ctxType.GetProperties()
                    .FirstOrDefault(p => p.Name.Equals(entidad, StringComparison.OrdinalIgnoreCase));

                if (dbSetProp is null)
                {
                    MessageBox.Show($"Entidad '{entidad}' no encontrada en el contexto.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return -1;
                }

                IQueryable query = (IQueryable)dbSetProp.GetValue(context, null)!;

                // === Filtro por "activo" ===
                if (soloActivos && query.ElementType.GetProperty("Activo", BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance) is not null)
                {
                    var param = Expression.Parameter(query.ElementType, "x");
                    var prop = Expression.Property(param, query.ElementType.GetProperty("Activo", BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance)!);
                    var value = Expression.Constant(true);
                    var lambda = Expression.Lambda(Expression.Equal(prop, value), param);
                    query = query.Provider.CreateQuery(Expression.Call(typeof(Queryable), "Where", new[] { query.ElementType }, query.Expression, lambda));
                }

                // === Filtros dinámicos ===
                if (filtros != null && filtros.Count > 0)
                {
                    foreach (var kvp in filtros)
                    {
                        var propInfo = query.ElementType.GetProperty(kvp.Key, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                        if (propInfo == null) continue;

                        var param = Expression.Parameter(query.ElementType, "x");
                        var left = Expression.Property(param, propInfo);
                        var right = Expression.Constant(kvp.Value, propInfo.PropertyType);
                        var equal = Expression.Equal(left, right);
                        var lambda = Expression.Lambda(equal, param);

                        query = query.Provider.CreateQuery(
                            Expression.Call(typeof(Queryable), "Where", new[] { query.ElementType }, query.Expression, lambda));
                    }
                }

                // === Orden múltiple ===
                if (orden != null && orden.Count > 0)
                {
                    bool firstOrder = true;
                    foreach (var campo in orden)
                    {
                        string colName = campo.Item1;
                        bool asc = campo.Item2;

                        var propInfo = query.ElementType.GetProperty(colName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                        if (propInfo == null) continue;

                        var param = Expression.Parameter(query.ElementType, "x");
                        var prop = Expression.Property(param, propInfo);
                        var lambda = Expression.Lambda(prop, param);

                        string methodName = firstOrder
                            ? (asc ? "OrderBy" : "OrderByDescending")
                            : (asc ? "ThenBy" : "ThenByDescending");

                        query = query.Provider.CreateQuery(
                            Expression.Call(typeof(Queryable), methodName,
                            new[] { query.ElementType, propInfo.PropertyType },
                            query.Expression, lambda));

                        firstOrder = false;
                    }
                }

                // === Cargar datos al DataTable ===
                var lista = query.Cast<object>().ToList();
                if (lista.Count == 0)
                {
                    combo.DataSource = null;
                    combo.Items.Clear();
                    return 0;
                }

                var dt = new DataTable();
                dt.Columns.Add(valuemember);
                dt.Columns.Add(displaymember);

                foreach (var item in lista)
                {
                    var val = item.GetType().GetProperty(valuemember, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance)?.GetValue(item, null);
                    var disp = item.GetType().GetProperty(displaymember, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance)?.GetValue(item, null);
                    dt.Rows.Add(val, disp);
                }

                combo.DataSource = dt;
                combo.DisplayMember = displaymember;
                combo.ValueMember = valuemember;
                combo.SelectedIndex = predet >= 0 && predet < dt.Rows.Count ? predet : -1;

                return dt.Rows.Count;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar combo: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }
        }

        public static async Task<int> CargarComboAsync<T>(
         ComboBox combo,
         IQueryable<T> query,
         string displayMember,
         string valueMember,
         bool soloActivos = true,
         Dictionary<string, object>? filtros = null,
         List<Tuple<string, bool>>? orden = null)
        {
            try
            {
                var tipo = typeof(T);

                // === Filtro automático por "Activo" ===
                if (soloActivos && tipo.GetProperty("Activo", BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance) is not null)
                {
                    var param = Expression.Parameter(tipo, "x");
                    var prop = Expression.Property(param, tipo.GetProperty("Activo", BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance)!);
                    var value = Expression.Constant(true);
                    var lambda = Expression.Lambda<Func<T, bool>>(Expression.Equal(prop, value), param);
                    query = query.Where(lambda);
                }

                // === Filtros dinámicos ===
                if (filtros != null)
                {
                    foreach (var kvp in filtros)
                    {
                        var propInfo = tipo.GetProperty(kvp.Key, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                        if (propInfo == null) continue;

                        var param = Expression.Parameter(tipo, "x");
                        var left = Expression.Property(param, propInfo);
                        var right = Expression.Constant(kvp.Value, propInfo.PropertyType);
                        var equal = Expression.Equal(left, right);
                        var lambda = Expression.Lambda<Func<T, bool>>(equal, param);
                        query = query.Where(lambda);
                    }
                }

                // === Orden múltiple ===
                if (orden != null && orden.Count > 0)
                {
                    bool firstOrder = true;
                    foreach (var campo in orden)
                    {
                        string colName = campo.Item1;
                        bool asc = campo.Item2;

                        var propInfo = tipo.GetProperty(colName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                        if (propInfo == null) continue;

                        var param = Expression.Parameter(tipo, "x");
                        var prop = Expression.Property(param, propInfo);
                        var lambda = Expression.Lambda(prop, param);

                        string methodName = firstOrder
                            ? (asc ? "OrderBy" : "OrderByDescending")
                            : (asc ? "ThenBy" : "ThenByDescending");

                        query = (IQueryable<T>)query.Provider.CreateQuery(
                            Expression.Call(typeof(Queryable), methodName,
                            new[] { tipo, propInfo.PropertyType },
                            query.Expression, lambda));

                        firstOrder = false;
                    }
                }

                // === Ejecutar consulta asíncrona ===
                var lista = await query.ToListAsync();

                if (lista.Count == 0)
                {
                    combo.DataSource = null;
                    combo.Items.Clear();
                    return 0;
                }

                // === Construir DataTable ===
                var dt = new DataTable();
                dt.Columns.Add(valueMember);
                dt.Columns.Add(displayMember);

                foreach (var item in lista)
                {
                    var val = tipo.GetProperty(valueMember, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance)?.GetValue(item, null);
                    var disp = tipo.GetProperty(displayMember, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance)?.GetValue(item, null);
                    dt.Rows.Add(val, disp);
                }

                combo.DataSource = dt;
                combo.DisplayMember = displayMember;
                combo.ValueMember = valueMember;
                combo.SelectedIndex = -1;

                return dt.Rows.Count;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar combo: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }
        }

        //public static int Cargar_Combo(ref ComboBox combo, string entidad, string displaymember, string valuemember, int predet = 0, bool soloActivos = true, Dictionary<string, object> filtros = null, List<Tuple<string, bool>> orden = null)
        //{
        //    try
        //    {
        //        using (var context = new CentrexDbContext())
        //        {
        //            // Buscar el DbSet correspondiente
        //            var ctxType = context.GetType();
        //            var dbSetProp = ctxType.GetProperties().FirstOrDefault(p => p.Name.Equals(entidad, StringComparison.OrdinalIgnoreCase));
        //            if (dbSetProp is null)
        //            {
        //                Interaction.MsgBox("Entidad '" + entidad + "' no encontrada en el contexto.", MsgBoxStyle.Exclamation);
        //                return -1;
        //            }

        //            // Base IQueryable
        //            IQueryable query = (IQueryable)dbSetProp.GetValue(context, null);

        //            // =======================
        //            // Filtro por "activo"
        //            // =======================
        //            if (soloActivos && query.ElementType.GetProperty("activo") is not null)
        //            {
        //                var @param = Expression.Parameter(query.ElementType, "x");
        //                var prop = Expression.Property(@param, "activo");
        //                var value = Expression.Constant(true);
        //                var lambda = Expression.Lambda(Expression.Equal(prop, value), @param);
        //                query = query.Provider.CreateQuery(Expression.Call(typeof(Queryable), "Where", new[] { query.ElementType }, query.Expression, lambda));
        //            }

        //            // =======================
        //            // Filtros dinámicos múltiples
        //            // =======================
        //            if (filtros is not null && filtros.Count > 0)
        //            {
        //                foreach (var kvp in filtros)
        //                {
        //                    var propInfo = query.ElementType.GetProperty(kvp.Key);
        //                    if (propInfo is not null)
        //                    {
        //                        var @param = Expression.Parameter(query.ElementType, "x");
        //                        var left = Expression.Property(@param, kvp.Key);
        //                        var right = Expression.Constant(kvp.Value, propInfo.PropertyType);
        //                        var equal = Expression.Equal(left, right);
        //                        var lambda = Expression.Lambda(equal, @param);
        //                        query = query.Provider.CreateQuery(Expression.Call(typeof(Queryable), "Where", new[] { query.ElementType }, query.Expression, lambda));
        //                    }
        //                }
        //            }

        //            // =======================
        //            // Orden dinámico múltiple
        //            // =======================
        //            if (orden is not null && orden.Count > 0)
        //            {
        //                bool firstOrder = true;
        //                foreach (var campo in orden)
        //                {
        //                    string colName = campo.Item1;
        //                    bool asc = campo.Item2;

        //                    var propInfo = query.ElementType.GetProperty(colName);
        //                    if (propInfo is null)
        //                        continue;

        //                    var @param = Expression.Parameter(query.ElementType, "x");
        //                    var prop = Expression.Property(@param, propInfo);
        //                    var lambda = Expression.Lambda(prop, @param);

        //                    string methodName;
        //                    if (firstOrder)
        //                    {
        //                        methodName = asc ? "OrderBy" : "OrderByDescending";
        //                        firstOrder = false;
        //                    }
        //                    else
        //                    {
        //                        methodName = asc ? "ThenBy" : "ThenByDescending";
        //                    }

        //                    query = query.Provider.CreateQuery(Expression.Call(typeof(Queryable), methodName, new[] { query.ElementType, propInfo.PropertyType }, query.Expression, lambda));
        //                }
        //            }

        //            // =======================
        //            // Llenar DataTable
        //            // =======================
        //            var lista = query.Cast<object>().ToList();
        //            var dt = new DataTable();
        //            dt.Columns.Add(valuemember);
        //            dt.Columns.Add(displaymember);

        //            foreach (var item in lista)
        //            {
        //                var val = item.GetType().GetProperty(valuemember)?.GetValue(item, null);
        //                var disp = item.GetType().GetProperty(displaymember)?.GetValue(item, null);
        //                dt.Rows.Add(val, disp);
        //            }

        //            // =======================
        //            // Vincular ComboBox
        //            // =======================
        //            combo.DataSource = dt;
        //            combo.DisplayMember = displaymember;
        //            combo.ValueMember = valuemember;
        //            combo.SelectedIndex = predet >= 0 && predet < dt.Rows.Count ? predet : -1;

        //            return 99;
        //        }
        //    }

        //    catch (Exception ex)
        //    {
        //        Interaction.MsgBox("Error al cargar combo: " + ex.Message, MsgBoxStyle.Critical);
        //        return -1;
        //    }
        //}


        /// <summary>
        /// Calcula total puro usando Entity Framework
        /// </summary>
        public static double calculoTotalPuro(DataGridView datagrid)
        {
            double total = 0d;
            string itemId;

            try
            {
                // Calcular precios normales
                for (int c = 0; c < datagrid.Rows.Count; c++)
                {
                    if (datagrid.Rows[c].Cells.Count > 0 && datagrid.Rows[c].Cells[0].Value is not null)
                    {
                        itemId = datagrid.Rows[c].Cells[0].Value.ToString();
                        if (!string.IsNullOrEmpty(itemId) && itemId.Contains("-"))
                        {
                            itemId = Strings.Right(itemId, itemId.Length - Strings.InStr(itemId, "-"));
                        }

                        if (!string.IsNullOrEmpty(itemId))
                        {
                            var item = mitem.info_item(Conversions.ToInteger(itemId));
                            if (!item.EsDescuento && !item.EsMarkup)
                            {
                                if (datagrid.Rows[c].Cells.Count > 4 && datagrid.Rows[c].Cells[4].Value is not null && datagrid.Rows[c].Cells[3].Value is not null)
                                {
                                    total += Conversions.ToDouble(datagrid.Rows[c].Cells[4].Value) * Conversions.ToDouble(datagrid.Rows[c].Cells[3].Value);
                                }
                            }
                        }
                    }
                }

                // Calcular descuentos
                for (int c = 0; c < datagrid.Rows.Count; c++)
                {
                    if (datagrid.Rows[c].Cells.Count > 0 && datagrid.Rows[c].Cells[0].Value is not null)
                    {
                        itemId = datagrid.Rows[c].Cells[0].Value.ToString();
                        if (!string.IsNullOrEmpty(itemId) && itemId.Contains("-"))
                        {
                            itemId = Strings.Right(itemId, itemId.Length - Strings.InStr(itemId, "-"));
                        }

                        if (!string.IsNullOrEmpty(itemId))
                        {
                            var item = mitem.info_item(Conversions.ToInteger(itemId));
                            if (item.EsDescuento)
                            {
                                if (datagrid.Rows[c].Cells.Count > 4 && datagrid.Rows[c].Cells[4].Value is not null)
                                {
                                    total -= Conversions.ToDouble(datagrid.Rows[c].Cells[4].Value);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                total = 0d;
                Interaction.MsgBox("Error al calcular totales: " + ex.Message, MsgBoxStyle.Critical, "Centrex");
            }

            return total;
        }
        public static void updateform([Optional, DefaultParameterValue("")] string seleccionado, [Optional] ref ComboBox cmb)
        {
            if (cmb is null)
                return;

            if (string.IsNullOrEmpty(seleccionado))
            {
                seleccionado = Conversions.ToString(cmb.SelectedValue);
            }
            else
            {
                cmb.SelectedValue = seleccionado;
            }
        }


        public static string fechaAFIP()
        {
            string anio = "";
            string mes = "";
            string dia = "";
            string fechaFinal;
            // Dim fechaArray() As String

            // If fecha = "" Then
            anio = DateTime.Now.Year.ToString();
            mes = DateTime.Now.Month.ToString();
            dia = DateTime.Now.Day.ToString();

            if (Conversions.ToDouble(mes) < 10d)
                mes = "0" + mes;
            if (Conversions.ToDouble(dia) < 10d)
                dia = "0" + dia;

            fechaFinal = anio + mes + dia;

            return fechaFinal;
        }

        public static string fechaAFIP_fecha(string fecha_afip)
        {
            string fecha;
            string anio;
            string mes;
            string dia;

            if (string.IsNullOrEmpty(fecha_afip))
            {
                return "";
            }

            anio = Strings.Left(fecha_afip, 4);
            mes = Strings.Mid(fecha_afip, 5, 2);
            dia = Strings.Right(fecha_afip, 2);
            fecha = anio + "/" + mes + "/" + dia;
            return fecha;
        }

        public static void ActivaItems(string tabla)
        {
            try
            {
                using (var context = new CentrexDbContext())
                {
                    switch (tabla.ToLower() ?? "")
                    {
                        case "items":
                            {
                                var inactivos = context.ItemEntity.Where(i => i.Activo == false).ToList();
                                foreach (var i in inactivos)
                                    i.Activo = true;
                                context.SaveChanges();
                                break;
                            }

                        case "clientes":
                            {
                                var inactivos = context.ClienteEntity.Where(c => c.Activo == false).ToList();
                                foreach (var c in inactivos)
                                    c.Activo = true;
                                context.SaveChanges();
                                break;
                            }

                        case "proveedores":
                            {
                                var inactivos = context.ProveedorEntity.Where(p => p.Activo == false).ToList();
                                foreach (var p in inactivos)
                                    p.Activo = true;
                                context.SaveChanges();
                                break;
                            }

                        case "tmppedidos_items":
                            {
                                var inactivos = context.TmpPedidoItemEntity.Where(t => t.Activo == false).ToList();
                                foreach (var t in inactivos)
                                    t.Activo = true;
                                context.SaveChanges();
                                break;
                            }

                        default:
                            {
                                Interaction.MsgBox("La tabla '" + tabla + "' no está contemplada en ActivarItems.", MsgBoxStyle.Exclamation);
                                break;
                            }
                    }
                }
            }

            catch (Exception ex)
            {
                Interaction.MsgBox("Error al activar registros en '" + tabla + "': " + ex.Message, MsgBoxStyle.Critical);
            }
        }

        /// <summary>
        /// Cierra el formulario actual y reactiva a su propietario.
        /// </summary>
        public static void closeandupdate(Form form)
        {
            if (form is null)
            {
                return;
            }

            try
            {
                if (form.Owner is not null)
                {
                    form.Owner.Enabled = true;
                    form.Owner.Activate();
                }
            }
            catch
            {
                // Ignoramos errores de UI para preservar compatibilidad legacy
            }

            form.Close();
        }

        /// <summary>
        /// Verifica si hay cambios pendientes en la base de datos
        /// </summary>
        public static bool haycambios()
        {
            try
            {
                using (var context = new CentrexDbContext())
                {
                    // Verificar si hay registros temporales pendientes
                    bool tmpPedidos = context.TmpPedidoItemEntity.Any(t => t.Activo == true);
                    bool tmpProduccion = context.TmpProduccionItemEntity.Any(t => t.Activo == true);
                    bool tmpStock = context.TmpRegistroStockEntity.Any(t => t.Activo == true);

                    return tmpPedidos || tmpProduccion || tmpStock;
                }
            }
            catch (Exception ex)
            {
                Interaction.MsgBox("Error al verificar cambios pendientes: " + ex.Message, MsgBoxStyle.Critical, "Centrex");
                return false;
            }
        }

        /// <summary>
        /// Cuenta registros en una tabla usando SQL directo
        /// </summary>
        public static int cantReg(string tbl)
        {
            try
            {
                using (var context = new CentrexDbContext())
                {
                    var entityInfo = ResolveEntitySet(context, tbl);
                    if (entityInfo is null)
                    {
                        Interaction.MsgBox($"La entidad '{tbl}' no existe en el contexto.", Constants.vbExclamation, "Centrex");
                        return 0;
                    }

                    // Contar registros usando EF nativo
                    return entityInfo.Query.Count();
                }
            }
            catch (Exception ex)
            {
                Interaction.MsgBox($"Error al contar registros en '{tbl}': {ex.Message}", Constants.vbCritical, "Centrex");
                return 0;
            }
        }

        private sealed class EntitySetInfo
        {
            public EntitySetInfo(IQueryable<object> query, string? tableName, string entityName)
            {
                Query = query;
                TableName = tableName;
                EntityName = entityName;
            }

            public IQueryable<object> Query { get; }

            public string? TableName { get; }

            public string EntityName { get; }
        }

        private static EntitySetInfo? ResolveEntitySet(CentrexDbContext context, string identifier)
        {
            // Intentar resolver por nombre de DbSet declarado en el contexto
            var dbSetProp = context.GetType()
                .GetProperties()
                .FirstOrDefault(p => p.Name.Equals(identifier, StringComparison.OrdinalIgnoreCase));

            if (dbSetProp is not null && dbSetProp.GetValue(context, null) is IQueryable rawQuery)
            {
                var entityClr = dbSetProp.PropertyType.GenericTypeArguments.FirstOrDefault();
                var entityType = entityClr is not null ? context.Model.FindEntityType(entityClr) : null;
                return new EntitySetInfo(rawQuery.Cast<object>(), entityType?.GetTableName(), dbSetProp.Name);
            }

            // Intentar resolver por nombre físico de tabla
            var entityByTable = context.Model.GetEntityTypes()
                .FirstOrDefault(t => string.Equals(t.GetTableName(), identifier, StringComparison.OrdinalIgnoreCase));

            if (entityByTable is not null)
            {
                var prop = context.GetType()
                    .GetProperties()
                    .FirstOrDefault(p =>
                        p.PropertyType.IsGenericType &&
                        p.PropertyType.GenericTypeArguments.Length == 1 &&
                        p.PropertyType.GenericTypeArguments[0] == entityByTable.ClrType);

                if (prop?.GetValue(context, null) is IQueryable setQuery)
                {
                    return new EntitySetInfo(setQuery.Cast<object>(), entityByTable.GetTableName(), prop.Name);
                }
            }

            return null;
        }

        // ============= FUNCIONES DE COMPATIBILIDAD LEGACY =============
        // Estas funciones han sido movidas a vb para evitar ambigüedad
        // y mantener compatibilidad con código existente

        // ================================================================
        // 1. LECTURA Y ESCRITURA DE ARCHIVOS DE TEXTO
        // ================================================================
        public static string leerArchivoTexto(string path)
        {
            try
            {
                if (!File.Exists(path))
                    return "";
                using (var reader = new StreamReader(path, Encoding.UTF8))
                {
                    return reader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                Interaction.MsgBox("Error leyendo archivo: " + ex.Message, Constants.vbCritical);
                return "";
            }
        }

        public static void guardarArchivoTexto(string path, string contenido)
        {
            try
            {
                using (var fs = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    byte[] data = Encoding.UTF8.GetBytes(contenido);
                    fs.Write(data, 0, data.Length);
                }
            }
            catch (Exception ex)
            {
                Interaction.MsgBox("Error guardando archivo: " + ex.Message, Constants.vbCritical);
            }
        }

        // ================================================================
        // 2. BACKUP DE BASE DE DATOS (mantiene compatibilidad)
        // ================================================================
        public static bool dbBackup(string strRuta, string strArchivo)
        {
            try
            {
                // Construir nombres y rutas de destino
                string archivoFinal = usuario_logueado.Nombre.Replace(" ", "_") + "_" + strArchivo;
                string rutaDestino1 = Path.Combine(strRuta, archivoFinal);
                string rutaDestino2 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"SQL\BKP", archivoFinal);

                // Crear carpeta local si no existe
                string dirLocal = Path.GetDirectoryName(rutaDestino2);
                if (!Directory.Exists(dirLocal))
                    Directory.CreateDirectory(dirLocal);

                using (var context = new CentrexDbContext())
                {
                    // Backup principal (ruta externa)
                    string sqlBackup1 = $"BACKUP DATABASE [{basedb}] TO DISK = N'{rutaDestino1}' " +
                                        "WITH NOFORMAT, NOINIT, NAME = N'" + basedb + "-Full Database Backup', " +
                                        "SKIP, NOREWIND, NOUNLOAD, STATS = 10";
                    context.Database.ExecuteSqlRaw(sqlBackup1);

                    // Backup secundario (ruta local)
                    string sqlBackup2 = $"BACKUP DATABASE [{basedb}] TO DISK = N'{rutaDestino2}' " +
                                        "WITH NOFORMAT, NOINIT, NAME = N'" + basedb + "-Local Backup', " +
                                        "SKIP, NOREWIND, NOUNLOAD, STATS = 10";
                    context.Database.ExecuteSqlRaw(sqlBackup2);
                }

                return true;
            }
            catch (Exception ex)
            {
                Interaction.MsgBox("Error al generar backup: " + ex.Message, MsgBoxStyle.Critical, "Centrex Backup");
                return false;
            }
        }


        // ================================================================
        // 3. EJECUTAR SQL CON RESULTADO - usando EF
        // ================================================================
        public static string ejecutarSQLconResultado(string entidad, DateTime? fecha_desde = null, DateTime? fecha_hasta = null)
        {
            try
            {
                using (var context = new CentrexDbContext())
                {
                    // 🔹 Buscar el DbSet correspondiente por nombre (ej: "Clientes", "Items", "Pedidos")
                    var dbSetProp = context.GetType()
                        .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                        .FirstOrDefault(p => p.Name.Equals(entidad, StringComparison.OrdinalIgnoreCase));

                    if (dbSetProp == null)
                    {
                        return $"Entidad '{entidad}' no encontrada en el contexto.";
                    }

                    // 🔹 Obtener los registros como IQueryable<object>
                    var dbSet = dbSetProp.GetValue(context) as IQueryable<object>;
                    if (dbSet == null)
                    {
                        return $"No se pudo acceder a los datos de '{entidad}'.";
                    }

                    // 🔹 Aplicar filtros de fecha si la entidad tiene una columna 'Fecha' o 'FechaCarga'
                    if (fecha_desde.HasValue || fecha_hasta.HasValue)
                    {
                        var tipoEntidad = dbSet.ElementType;
                        var fechaProp = tipoEntidad.GetProperties()
                            .FirstOrDefault(p =>
                                p.Name.Equals("Fecha", StringComparison.OrdinalIgnoreCase) ||
                                p.Name.Equals("FechaCarga", StringComparison.OrdinalIgnoreCase) ||
                                p.Name.Equals("FechaComprobante", StringComparison.OrdinalIgnoreCase));

                        if (fechaProp != null)
                        {
                            if (fecha_desde.HasValue)
                                dbSet = dbSet.Where(e => EF.Property<DateTime>(e, fechaProp.Name) >= fecha_desde.Value);

                            if (fecha_hasta.HasValue)
                                dbSet = dbSet.Where(e => EF.Property<DateTime>(e, fechaProp.Name) <= fecha_hasta.Value);
                        }
                    }

                    // 🔹 Ejecutar la consulta y cargar resultados
                    var registros = dbSet.Take(500).ToList(); // limita resultados para performance
                    if (registros.Count == 0)
                        return $"Sin resultados en '{entidad}'.";

                    // 🔹 Construir salida dinámica
                    StringBuilder sb = new StringBuilder();

                    foreach (var registro in registros)
                    {
                        var props = registro.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

                        foreach (var prop in props)
                        {
                            var valor = prop.GetValue(registro);
                            string texto = valor != null ? valor.ToString() : "(null)";
                            sb.AppendLine($"{prop.Name}: {texto}");
                        }

                        sb.AppendLine(new string('-', 50));
                    }

                    return sb.ToString();
                }
            }
            catch (Exception ex)
            {
                Interaction.MsgBox("Error al ejecutar consulta dinámica: " + ex.Message, Constants.vbCritical);
                return "ERROR";
            }
        }

        // ================================================================
        // 5. VALIDACIONES DE TECLADO
        // ================================================================
        public static bool esNumero(KeyPressEventArgs e, bool _negativosOk = false)
        {
            string permitidos = "0123456789,." + '\b';
            if (_negativosOk)
                permitidos += "-";
            return Strings.InStr(permitidos, Conversions.ToString(e.KeyChar)) > 0;
        }

        public static string isDecimalOk(KeyPressEventArgs e)
        {
            string valor = Conversions.ToString(e.KeyChar);
            if (Strings.InStr(".,", valor) > 0)
            {
                valor = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator == "," ? "," : ".";
            }
            return valor;
        }

        /// <summary>
        /// Valida una tecla presionada. Permite números, separadores decimales (., ,), signo negativo y backspace.
        /// Devuelve TRUE si la tecla debe bloquearse.
        /// </summary>
        public static bool valida(char tecla, bool negativosOk = true)
        {
            // Caracteres válidos siempre
            string permitidos = negativosOk
                ? "0123456789.,-\b"
                : "0123456789.,\b";

            // Si el carácter no está permitido → se bloquea
            return !permitidos.Contains(tecla);
        }

        /// <summary>
        /// Corrige el separador decimal o miles según la configuración.
        /// Devuelve el carácter que debe escribirse.
        /// </summary>
        public static char NormalizaDecimal(char tecla)
        {
            // Permite usar ',' o '.' indistintamente
            if (tecla == ',') tecla = '.';
            return tecla;
        }

        // ================================================================
        // 6. OBTIENE VALOR DE CONFIGURACIÓN "clave=valor"
        // ================================================================
        public static string obtieneValorConfig(string linea)
        {
            if (string.IsNullOrWhiteSpace(linea))
                return "";
            int idx = linea.IndexOf('=');
            if (idx < 0)
                return "";
            return linea.Substring(idx + 1).Trim();
        }


        public static int[] addValArray(ref int[] array, int @add)
        {
            int max = 0;
            int c = 0;
            bool found = false;

            try
            {
                max = Information.UBound(array);

                var loopTo = max;
                for (c = 0; c <= loopTo; c++)
                {
                    if (array[c] == @add)
                        found = true;
                }
            }
            catch (Exception ex)
            {
                max = -1;
                Interaction.MsgBox("Error al buscar elemento: " + ex.Message, MsgBoxStyle.Critical, "Centrex");
            }

            if (!found)
            {
                max += 1;
                Array.Resize(ref array, max + 1);
                array[max] = @add;
            }

            return array;
        }


        public static int[] delValArray(ref int[] array, int del)
        {
            var arrayB = new int[1];
            int c;
            int x = 0;
            int max = Information.UBound(array);

            var loopTo = max;
            for (c = 0; c <= loopTo; c++)
            {
                if (array[c] != del)
                {
                    Array.Resize(ref arrayB, Information.UBound(arrayB) + 1 + 1);
                    arrayB[x] = array[c];
                    x += 1;
                }
            }

            Array.Resize(ref arrayB, Information.UBound(arrayB));

            return arrayB;
        }


        public static bool searchArray(int[] array, int val)
        {
            int c = 0;
            int max = 0;
            bool found = false;

            try
            {
                max = Information.UBound(array);

                var loopTo = max;
                for (c = 0; c <= loopTo; c++)
                {
                    if (array[c] == val)
                    {
                        found = true;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Interaction.MsgBox("Error " + ex.Message, MsgBoxStyle.Critical, "Centrex");
            }

            return found;
        }

        public static string CalculoComprobanteLocal(string tipo, int puntoVenta, int numero)
        {
            return $"{tipo} Nº {puntoVenta.ToString().PadLeft(4, '0')}-{numero.ToString().PadLeft(8, '0')}";
        }

        /// <summary>
        /// Convierte cualquier valor (DateTimePicker, TextBox, Label, string, DateTime, DateOnly, etc.)
        /// a DateOnly de forma segura.
        /// Si no puede convertir, devuelve DateOnly.MinValue.
        /// </summary>
        public static DateOnly ToDateOnly(object valor)
        {
            try
            {
                if (valor == null) return DateOnly.MinValue;

                return valor switch
                {
                    DateOnly d => d,
                    DateTime dt => DateOnly.FromDateTime(dt.Date),
                    DateTimePicker dtp => DateOnly.FromDateTime(dtp.Value.Date),
                    TextBox txt when !string.IsNullOrWhiteSpace(txt.Text) => DateOnly.Parse(txt.Text),
                    Label lbl when !string.IsNullOrWhiteSpace(lbl.Text) => DateOnly.Parse(lbl.Text),
                    string s when !string.IsNullOrWhiteSpace(s) => DateOnly.Parse(s),
                    _ => DateOnly.MinValue
                };
            }
            catch
            {
                return DateOnly.MinValue;
            }
        }

        /// <summary>
        /// Convierte DateOnly, DateTime o cualquier valor de fecha a string ("dd/MM/yyyy").
        /// Si es nulo o inválido, devuelve cadena vacía.
        /// </summary>
        public static string ToDateString(object valor)
        {
            try
            {
                if (valor == null) return "";

                return valor switch
                {
                    DateOnly d => d.ToString("dd/MM/yyyy"),
                    DateTime dt => dt.ToString("dd/MM/yyyy"),
                    DateTimePicker dtp => dtp.Value.ToString("dd/MM/yyyy"),
                    TextBox txt when !string.IsNullOrWhiteSpace(txt.Text) => txt.Text,
                    Label lbl when !string.IsNullOrWhiteSpace(lbl.Text) => lbl.Text,
                    string s => s,
                    _ => ""
                };
            }
            catch
            {
                return "";
            }
        }


        // ======================================================
        // EXPORTAR A EXCEL
        // ======================================================
        public static void exportarExcel(DataGridView dgview, string rutaArchivo)
        {
            try
            {
                var oExcel = Interaction.CreateObject("Excel.Application");
                var oBook = ((dynamic)oExcel).Workbooks.Add;
                var oSheet = oBook.Worksheets(1);

                int rCount = dgview.Rows.Count;
                int cCount = dgview.Columns.Count;
                var dataArray = new object[rCount + 1, cCount];

                for (int j = 0, loopTo = cCount - 1; j <= loopTo; j++)
                    dataArray[0, j] = dgview.Columns[j].HeaderText;

                for (int i = 0, loopTo1 = rCount - 1; i <= loopTo1; i++)
                {
                    for (int j = 0, loopTo2 = cCount - 1; j <= loopTo2; j++)
                        dataArray[i + 1, j] = dgview.Rows[i].Cells[j].Value?.ToString();

                }
                oSheet.Range("A1").Resize(rCount + 1, cCount).Value = dataArray;
                ((dynamic)oExcel).DisplayAlerts = false;
                oBook.SaveAs(rutaArchivo);
                ((dynamic)oExcel).Quit();
            }
            catch (Exception ex)
            {
                Interaction.MsgBox(ex.Message.ToString());
            }
        }

        //public static string Precio(decimal valor)
        //{
        //    return $"$ {valor:N2}";
        //}

        // =====================================
        // SOBRECARGA DE PRECIO (acepta decimal)
        // =====================================
        public static string Precio(decimal valor)
        {
            try
            {
                return "$ " + valor.ToString("N2", CultureInfo.CurrentCulture);
            }
            catch
            {
                return "$ 0,00";
            }
        }
    }
}