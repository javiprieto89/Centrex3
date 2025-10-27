using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;

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
                using (var context = GetDbContext())
                {
                    int id;
                    int.TryParse(id_proveedor, out id);

                    var proveedorEntity = context.ProveedorEntity
                        .Include(p => p.IdProvinciaFiscalNavigation)
                        .Include(p => p.IdProvinciaEntregaNavigation)
                        .Include(p => p.IdPaisFiscalNavigation)
                        .Include(p => p.IdPaisEntregaNavigation)
                        .FirstOrDefault(p => p.IdProveedor == id);

                    if (proveedorEntity is not null)
                    {
                        tmp.IdProveedor = proveedorEntity.IdProveedor;
                        tmp.RazonSocial = proveedorEntity.RazonSocial;
                        tmp.TaxNumber = proveedorEntity.TaxNumber;
                        tmp.Contacto = proveedorEntity.Contacto;
                        tmp.Telefono = proveedorEntity.Telefono;
                        tmp.Celular = proveedorEntity.Celular;
                        tmp.Email = proveedorEntity.Email;
                        tmp.IdProvinciaFiscal = proveedorEntity.IdProvinciaFiscal.HasValue ? proveedorEntity.IdProvinciaFiscal.Value : -1;
                        tmp.DireccionFiscal = proveedorEntity.DireccionFiscal;
                        tmp.LocalidadFiscal = proveedorEntity.LocalidadFiscal;
                        tmp.CpFiscal = proveedorEntity.CpFiscal;
                        tmp.IdProvinciaEntrega = proveedorEntity.IdProvinciaEntrega.HasValue ? proveedorEntity.IdProvinciaEntrega.Value : -1;
                        tmp.DireccionEntrega = proveedorEntity.DireccionEntrega;
                        tmp.LocalidadEntrega = proveedorEntity.LocalidadEntrega;
                        tmp.CpEntrega = proveedorEntity.CpEntrega;
                        tmp.Notas = proveedorEntity.Notas;
                        tmp.EsInscripto = proveedorEntity.EsInscripto;
                        tmp.Vendedor = proveedorEntity.Vendedor;
                        tmp.Activo = proveedorEntity.Activo;
                        tmp.IdTipoDocumento = proveedorEntity.IdTipoDocumento;
                        tmp.IdClaseFiscal = proveedorEntity.IdClaseFiscal.HasValue ? proveedorEntity.IdClaseFiscal.Value : -1;
                        tmp.IdPaisFiscal = proveedorEntity.IdPaisFiscal;
                        tmp.IdPaisEntrega = proveedorEntity.IdPaisEntrega;

                    }
                    else
                    {
                        tmp.RazonSocial = "error";
                    }
                }
            }
            catch (Exception ex)
            {
                Interaction.MsgBox("Error en info_proveedor: " + ex.Message);
                tmp.RazonSocial = "error";
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
                using (var context = GetDbContext())
                {
                    var proveedorEntity = new ProveedorEntity()
                    {
                        RazonSocial = pr.RazonSocial,
                        TaxNumber = pr.TaxNumber,
                        Contacto = pr.Contacto,
                        Telefono = pr.Telefono,
                        Celular = pr.Celular,
                        Email = pr.Email,
                        IdProvinciaFiscal = pr.IdProvinciaFiscal,
                        DireccionFiscal = pr.DireccionFiscal,
                        LocalidadFiscal = pr.LocalidadFiscal,
                        CpFiscal = pr.CpFiscal,
                        IdProvinciaEntrega = pr.IdProvinciaEntrega,
                        DireccionEntrega = pr.DireccionEntrega,
                        LocalidadEntrega = pr.LocalidadEntrega,
                        CpEntrega = pr.CpEntrega,
                        Notas = pr.Notas,
                        EsInscripto = pr.EsInscripto,
                        Vendedor = pr.Vendedor,
                        Activo = pr.Activo,
                        IdTipoDocumento = pr.IdTipoDocumento,
                        IdClaseFiscal = pr.IdClaseFiscal,
                        IdPaisFiscal = pr.IdPaisFiscal,
                        IdPaisEntrega = pr.IdPaisEntrega
                    };

                    context.ProveedorEntity.Add(proveedorEntity);
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
                using (var context = GetDbContext())
                {
                    var proveedorEntity = context.ProveedorEntity.FirstOrDefault(p => p.IdProveedor == pr.IdProveedor);

                    if (proveedorEntity is not null)
                    {
                        if (borra)
                        {
                            proveedorEntity.Activo = false;
                        }
                        else
                        {
                            proveedorEntity.RazonSocial = pr.RazonSocial;
                            proveedorEntity.TaxNumber = pr.TaxNumber;
                            proveedorEntity.Contacto = pr.Contacto;
                            proveedorEntity.Telefono = pr.Telefono;
                            proveedorEntity.Celular = pr.Celular;
                            proveedorEntity.Email = pr.Email;
                            proveedorEntity.IdProvinciaFiscal = pr.IdProvinciaFiscal;
                            proveedorEntity.DireccionFiscal = pr.DireccionFiscal;
                            proveedorEntity.LocalidadFiscal = pr.LocalidadFiscal;
                            proveedorEntity.CpFiscal = pr.CpFiscal;
                            proveedorEntity.IdProvinciaEntrega = pr.IdProvinciaEntrega;
                            proveedorEntity.DireccionEntrega = pr.DireccionEntrega;
                            proveedorEntity.LocalidadEntrega = pr.LocalidadEntrega;
                            proveedorEntity.CpEntrega = pr.CpEntrega;
                            proveedorEntity.Notas = pr.Notas;
                            proveedorEntity.EsInscripto = pr.EsInscripto;
                            proveedorEntity.Vendedor = pr.Vendedor;
                            proveedorEntity.Activo = pr.Activo;
                            proveedorEntity.IdTipoDocumento = pr.IdTipoDocumento;
                            proveedorEntity.IdClaseFiscal = pr.IdClaseFiscal;
                            proveedorEntity.IdPaisFiscal = pr.IdPaisFiscal;
                            proveedorEntity.IdPaisEntrega = pr.IdPaisEntrega;
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
                using (var context = GetDbContext())
                {
                    var proveedorEntity = context.ProveedorEntity.FirstOrDefault(p => p.IdProveedor == pr.IdProveedor);

                    if (proveedorEntity is not null)
                    {
                        context.ProveedorEntity.Remove(proveedorEntity);
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
            try
            {
                using (var context = GetDbContext())
                {
                    // Usar el Stored Procedure del contexto EF Core
                    var results = context.Procedures.SP_consulta_CC_ProveedorAsync(
                        id_proveedor, 
                        id_Cc, 
                        fecha_desde.ToString("yyyy-MM-dd"), 
                        fecha_hasta.ToString("yyyy-MM-dd")
                    ).Result;

                    if (results != null && results.Count > 0)
                    {
                        // Crear DataGridQueryResult para usar el sistema completo
                        var queryResult = new DataGridQueryResult
                        {
                            Query = results.AsQueryable(),
                            EsMaterializada = true,
                            DataMaterializada = results,
                            ColumnasOcultar = new List<string> { "ID" }
                        };

                        // Usar el sistema LoadDataGridDynamic de forma síncrona
                        LoadDataGridDynamic.LoadDataGridWithEntityAsync(dataGrid, queryResult).Wait();

                        // Configurar paginación
                        if (!traerTodo)
                        {
                            nRegs = results.Count;
                            tPaginas = (int)Math.Round(Math.Ceiling(nRegs / (double)VariablesGlobales.itXPage));
                            txtnPage.Text = pagina + " / " + tPaginas;
                        }
                    }
                    else
                    {
                        // Limpiar grid si no hay datos
                        dataGrid.DataSource = null;
                    }
                }
            }
            catch (Exception ex)
            {
                Interaction.MsgBox("Error en consultaCcProveedor: " + ex.Message);
            }
        }

        // ===========================================
        // FUNCIÓN: ConvertToDataTable (helper) - ELIMINADA
        // Ya existe en LoadDataGridDynamic.cs
        // ===========================================


    }
}
