using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using Microsoft.VisualBasic;
using Centrex.Models;

namespace Centrex
{

    static class proveedores
    {
        // ************************************
        // FUNCIONES DE PROVEEDORES (Entity Framework)
        // ************************************

        // ===========================================
        // FUNCIÓN: info_proveedor
        // ===========================================
        public static ProveedorEntity info_proveedor(string id_proveedor)
        {
            var tmp = new ProveedorEntity();

            try
            {
                using (var context = new CentrexDbContext())
                {
                    int id;
                    int.TryParse(id_proveedor, out id);

                    var proveedorEntity = context.Proveedores.FirstOrDefault(p => p.IdProveedor == id);

                    if (proveedorEntity is not null)
                    {
                        tmp.IdProveedor = proveedorEntity.IdProveedor.ToString();
                        tmp.razon_social = proveedorEntity.razon_social;
                        tmp.taxNumber = proveedorEntity.taxNumber;
                        tmp.contacto = proveedorEntity.contacto;
                        tmp.telefono = proveedorEntity.telefono;
                        tmp.celular = proveedorEntity.celular;
                        tmp.email = proveedorEntity.email;
                        tmp.id_provincia_fiscal = proveedorEntity.IdProvinciaFiscal.HasValue ? proveedorEntity.IdProvinciaFiscal.Value : -1;
                        tmp.direccion_fiscal = proveedorEntity.direccion_fiscal;
                        tmp.localidad_fiscal = proveedorEntity.localidad_fiscal;
                        tmp.cp_fiscal = proveedorEntity.cp_fiscal;
                        tmp.id_provincia_entrega = proveedorEntity.IdProvinciaEntrega.HasValue ? proveedorEntity.IdProvinciaEntrega.Value : -1;
                        tmp.direccion_entrega = proveedorEntity.direccion_entrega;
                        tmp.localidad_entrega = proveedorEntity.localidad_entrega;
                        tmp.cp_entrega = proveedorEntity.cp_entrega;
                        tmp.notas = proveedorEntity.notas;
                        tmp.esInscripto = proveedorEntity.esInscripto;
                        tmp.vendedor = proveedorEntity.vendedor;
                        tmp.activo = proveedorEntity.activo;
                        tmp.id_tipoDocumento = proveedorEntity.IdTipoDocumento;
                        tmp.id_claseFiscal = proveedorEntity.IdClaseFiscal.HasValue ? proveedorEntity.IdClaseFiscal.Value : -1;
                    }
                    else
                    {
                        tmp.razon_social = "error";
                    }
                }
            }
            catch (Exception ex)
            {
                Interaction.MsgBox("Error en info_proveedor: " + ex.Message);
                tmp.razon_social = "error";
            }

            return tmp;
        }

        // ===========================================
        // FUNCIÓN: addproveedor
        // ===========================================
        public static bool addproveedor(ProveedorEntity pr)
        {
            try
            {
                using (var context = new CentrexDbContext())
                {
                    var proveedorEntity = new ProveedorEntity()
                    {
                        razon_social = pr.razon_social,
                        taxNumber = pr.taxNumber,
                        contacto = pr.contacto,
                        telefono = pr.telefono,
                        celular = pr.celular,
                        email = pr.email,
                        IdProvinciaFiscal = pr.id_provincia_fiscal,
                        direccion_fiscal = pr.direccion_fiscal,
                        localidad_fiscal = pr.localidad_fiscal,
                        cp_fiscal = pr.cp_fiscal,
                        IdProvinciaEntrega = pr.id_provincia_entrega,
                        direccion_entrega = pr.direccion_entrega,
                        localidad_entrega = pr.localidad_entrega,
                        cp_entrega = pr.cp_entrega,
                        notas = pr.notas,
                        esInscripto = pr.esInscripto,
                        vendedor = pr.vendedor,
                        activo = pr.activo,
                        IdTipoDocumento = pr.id_tipoDocumento,
                        IdClaseFiscal = pr.id_claseFiscal
                    };

                    context.Proveedores.Add(proveedorEntity);
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Interaction.MsgBox("Error en addproveedor: " + ex.Message);
                return false;
            }
        }

        // ===========================================
        // FUNCIÓN: updateProveedor
        // ===========================================
        public static bool updateProveedor(ProveedorEntity pr, bool borra = false)
        {
            try
            {
                using (var context = new CentrexDbContext())
                {
                    var proveedorEntity = context.Proveedores.FirstOrDefault(p => p.IdProveedor == pr.IdProveedor);

                    if (proveedorEntity is not null)
                    {
                        if (borra)
                        {
                            proveedorEntity.activo = false;
                        }
                        else
                        {
                            proveedorEntity.razon_social = pr.razon_social;
                            proveedorEntity.taxNumber = pr.taxNumber;
                            proveedorEntity.contacto = pr.contacto;
                            proveedorEntity.telefono = pr.telefono;
                            proveedorEntity.celular = pr.celular;
                            proveedorEntity.email = pr.email;
                            proveedorEntity.IdProvinciaFiscal = pr.id_provincia_fiscal;
                            proveedorEntity.direccion_fiscal = pr.direccion_fiscal;
                            proveedorEntity.localidad_fiscal = pr.localidad_fiscal;
                            proveedorEntity.cp_fiscal = pr.cp_fiscal;
                            proveedorEntity.IdProvinciaEntrega = pr.id_provincia_entrega;
                            proveedorEntity.direccion_entrega = pr.direccion_entrega;
                            proveedorEntity.localidad_entrega = pr.localidad_entrega;
                            proveedorEntity.cp_entrega = pr.cp_entrega;
                            proveedorEntity.notas = pr.notas;
                            proveedorEntity.esInscripto = pr.esInscripto;
                            proveedorEntity.vendedor = pr.vendedor;
                            proveedorEntity.activo = pr.activo;
                            proveedorEntity.IdTipoDocumento = pr.id_tipoDocumento;
                            proveedorEntity.IdClaseFiscal = pr.id_claseFiscal;
                        }

                        context.SaveChanges();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Interaction.MsgBox("Error en updateProveedor: " + ex.Message);
                return false;
            }
        }

        // ===========================================
        // FUNCIÓN: borrarproveedor
        // ===========================================
        public static bool borrarproveedor(ProveedorEntity pr)
        {
            try
            {
                using (var context = new CentrexDbContext())
                {
                    var proveedorEntity = context.Proveedores.FirstOrDefault(p => p.IdProveedor == pr.IdProveedor);

                    if (proveedorEntity is not null)
                    {
                        context.Proveedores.Remove(proveedorEntity);
                        context.SaveChanges();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Interaction.MsgBox("Error en borrarproveedor: " + ex.Message);
                return false;
            }
        }

        // ===========================================
        // FUNCIÓN: consultaCcProveedor
        // ===========================================
        public static void consultaCcProveedor(ref DataGridView dataGrid, int id_proveedor, int id_Cc, DateTime fecha_desde, DateTime fecha_hasta, int desde, ref int nRegs, ref int tPaginas, int pagina, ref TextBox txtnPage, bool traerTodo)
        {

            var datatable = new DataTable();
            DataGridViewColumn oldSortColumn = null;
            var oldSortDir = default(ListSortDirection);

            // Guardar el orden de las columnas
            oldSortColumn = dataGrid.SortedColumn;
            if (dataGrid.SortedColumn is not null)
            {
                oldSortDir = dataGrid.SortOrder == (int)System.Data.SqlClient.SortOrder.Ascending ? ListSortDirection.Ascending : ListSortDirection.Descending;
            }

            dataGrid.Columns.Clear();

            try
            {
                using (var context = new CentrexDbContext())
                {
                    // Parámetros del stored procedure
                    SqlParameter[] spParams = new[] { new SqlParameter("@id_proveedor", id_proveedor), new SqlParameter("@id_cc", id_Cc), new SqlParameter("@fecha_desde", fecha_desde), new SqlParameter("@fecha_hasta", fecha_hasta) };

                    // Ejecutar SP y obtener lista
                    var results = context.Database.SqlQuery<object>("EXEC SP_consulta_CC_Proveedor @id_proveedor, @id_cc, @fecha_desde, @fecha_hasta", spParams).ToList();

                    if (results.Count > 0)
                    {
                        datatable = ConvertToDataTable(results);

                        if (!traerTodo)
                        {
                            nRegs = datatable.Rows.Count;
                            tPaginas = (int)Math.Round(Math.Ceiling(nRegs / (double)VariablesGlobales.itXPage));
                            txtnPage.Text = pagina + " / " + tPaginas;

                            // Paginación manual
                            var pagedTable = datatable.Clone();
                            for (int i = desde, loopTo = Math.Min(desde + VariablesGlobales.itXPage - 1, nRegs - 1); i <= loopTo; i++)
                                pagedTable.ImportRow(datatable.Rows[i]);
                            datatable = pagedTable;
                        }
                    }
                }

                // Configurar el DataGrid
                dataGrid.DataSource = datatable;
                dataGrid.RowsDefaultCellStyle.BackColor = Color.White;
                dataGrid.AlternatingRowsDefaultCellStyle.BackColor = Color.AliceBlue;

                if (dataGrid.Rows.Count > 0)
                {
                    dataGrid.AutoSizeColumnsMode = (DataGridViewAutoSizeColumnsMode)DataGridViewAutoSizeColumnMode.AllCells;
                }
                else
                {
                    dataGrid.AutoSizeColumnsMode = (DataGridViewAutoSizeColumnsMode)DataGridViewAutoSizeColumnMode.None;
                }

                if (oldSortColumn is not null)
                {
                    dataGrid.Sort(dataGrid.Columns[oldSortColumn.Name], oldSortDir);
                }

                dataGrid.Refresh();
            }
            catch (Exception ex)
            {
                Interaction.MsgBox("Error en consultaCcProveedor: " + ex.Message);
            }
        }

        // ===========================================
        // FUNCIÓN: ConvertToDataTable (helper)
        // ===========================================
        private static DataTable ConvertToDataTable<T>(List<T> list)
        {
            var table = new DataTable();
            if (list is null || list.Count == 0)
                return table;

            System.Reflection.PropertyInfo[] properties = typeof(T).GetProperties();

            // Crear columnas según las propiedades del tipo T
            foreach (var prop in properties)
            {
                var columnType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                table.Columns.Add(prop.Name, columnType);
            }

            // Rellenar filas
            foreach (var item in list)
            {
                var row = table.NewRow();
                foreach (var prop in properties)
                    row[prop.Name] = prop.GetValue(item, null) ?? DBNull.Value;
                table.Rows.Add(row);
            }

            return table;
        }


    }
}
