using System;
using System.Data;
using System.Linq;
using System.Xml.Linq;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Centrex.Models;

namespace Centrex
{

    static class stock
    {
        // ************************************ FUNCIONES DE STOCK ***************************

        public static registro_stock InfoRegistroStock(int id_rs)
        {
            var tmp = new registro_stock();
            try
            {
                using (var ctx = new CentrexDbContext())
                {
                    var rs = ctx.RegistrosStock.AsNoTracking().FirstOrDefault(r => r.IdRegistro == id_rs);
                    if (rs is not null)
                    {
                        tmp.id_registro = rs.IdRegistro;
                        tmp.id_ingreso = rs.IdIngreso;
                        tmp.fecha = Conversions.ToString(rs.fecha.Value);
                        tmp.fecha_ingreso = rs.FechaIngreso;
                        tmp.factura = rs.factura;
                        tmp.IdProveedor = rs.IdProveedor;
                        tmp.id_item = rs.IdItem;
                        tmp.cantidad = (decimal)rs.cantidad;
                        tmp.costo = rs.costo;
                        tmp.precio_lista = rs.PrecioLista;
                        tmp.factor = rs.factor;
                        tmp.cantidad_anterior = rs.CantidadAnterior;
                        tmp.costo_anterior = rs.CostoAnterior;
                        tmp.precio_lista_anterior = rs.PrecioListaAnterior;
                        tmp.factor_anterior = rs.FactorAnterior;
                        tmp.nota = rs.nota;
                        tmp.activo = rs.activo;
                    }
                }
            }
            catch (Exception ex)
            {
                Interaction.MsgBox(ex.Message);
            }
            return tmp;
        }

        public static registro_stock InfoRegistroStockTmp(int id_rs)
        {
            var tmp = new registro_stock();
            try
            {
                using (var ctx = new CentrexDbContext())
                {
                    var rs = ctx.TmpRegistroStock.AsNoTracking().FirstOrDefault(r => r.IdRegistroTmp == id_rs);
                    if (rs is not null)
                    {
                        tmp.id_registro = rs.IdRegistroTmp;
                        tmp.id_ingreso = rs.IdIngreso;
                        tmp.fecha = Conversions.ToString(rs.fecha.Value);
                        tmp.fecha_ingreso = rs.FechaIngreso;
                        tmp.factura = rs.factura;
                        tmp.IdProveedor = rs.IdProveedor;
                        tmp.id_item = rs.IdItem;
                        tmp.cantidad = (decimal)rs.cantidad;
                        tmp.costo = rs.costo;
                        tmp.precio_lista = rs.PrecioLista;
                        tmp.factor = (decimal)rs.factor;
                        tmp.cantidad_anterior = rs.CantidadAnterior;
                        tmp.costo_anterior = rs.CostoAnterior;
                        tmp.precio_lista_anterior = rs.PrecioListaAnterior;
                        tmp.factor_anterior = rs.FactorAnterior;
                        tmp.nota = rs.nota;
                        tmp.activo = rs.activo;
                    }
                }
            }
            catch (Exception ex)
            {
                Interaction.MsgBox(ex.Message);
            }
            return tmp;
        }

        public static bool AddStockTmp(registro_stock rs)
        {
            try
            {
                using (var ctx = new CentrexDbContext())
                {
                    var entity = new TmpRegistroStockEntity()
                    {
                        fecha = Conversions.ToDate(rs.fecha),
                        factura = rs.factura,
                        IdProveedor = rs.IdProveedor,
                        IdItem = rs.id_item,
                        cantidad = (int)Math.Round(rs.cantidad),
                        costo = rs.costo,
                        PrecioLista = rs.precio_lista,
                        factor = rs.factor,
                        CantidadAnterior = rs.cantidad_anterior,
                        CostoAnterior = rs.costo_anterior,
                        PrecioListaAnterior = rs.precio_lista_anterior,
                        FactorAnterior = rs.factor_anterior,
                        nota = rs.nota,
                        activo = true
                    };
                    ctx.TmpRegistroStock.Add(entity);
                    ctx.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Interaction.MsgBox(ex.Message);
                return false;
            }
        }

        public static bool UpdateStockTmp(registro_stock rs)
        {
            try
            {
                using (var ctx = new CentrexDbContext())
                {
                    var entity = ctx.TmpRegistroStock.FirstOrDefault(r => r.IdRegistroTmp == rs.id_registro);
                    if (entity is not null)
                    {
                        entity.fecha = Conversions.ToDate(rs.fecha);
                        entity.factura = rs.factura;
                        entity.IdProveedor = rs.IdProveedor;
                        entity.IdItem = rs.id_item;
                        entity.cantidad = (int)Math.Round(rs.cantidad);
                        entity.costo = rs.costo;
                        entity.PrecioLista = rs.precio_lista;
                        entity.factor = rs.factor;
                        entity.nota = rs.nota;
                        ctx.SaveChanges();
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
                Interaction.MsgBox(ex.Message);
                return false;
            }
        }

        public static bool AddStock()
        {
            try
            {
                using (var ctx = new CentrexDbContext())
                {
                    var tmpList = ctx.TmpRegistroStock.ToList();
                    foreach (var tmp in tmpList)
                    {
                        var entity = new RegistroStockEntity()
                        {
                            IdIngreso = tmp.IdIngreso,
                            fecha = tmp.fecha,
                            factura = tmp.factura,
                            IdProveedor = tmp.IdProveedor,
                            IdItem = tmp.IdItem,
                            cantidad = tmp.cantidad,
                            costo = tmp.costo,
                            PrecioLista = tmp.PrecioLista,
                            factor = (decimal)tmp.factor,
                            CantidadAnterior = tmp.CantidadAnterior,
                            CostoAnterior = tmp.CostoAnterior,
                            PrecioListaAnterior = tmp.PrecioListaAnterior,
                            FactorAnterior = tmp.FactorAnterior,
                            nota = tmp.nota,
                            activo = true
                        };
                        ctx.RegistrosStock.Add(entity);
                    }
                    ctx.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Interaction.MsgBox(ex.Message);
                return false;
            }
        }

        public static bool BorrarItemRegistroStockTmp(registro_stock rs)
        {
            try
            {
                using (var ctx = new CentrexDbContext())
                {
                    var entity = ctx.TmpRegistroStock.FirstOrDefault(r => r.IdRegistroTmp == rs.id_registro);
                    if (entity is not null)
                    {
                        ctx.TmpRegistroStock.Remove(entity);
                        ctx.SaveChanges();
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
                Interaction.MsgBox(ex.Message);
                return false;
            }
        }

        public static void ArchivarIngresoStock()
        {
            try
            {
                using (var ctx = new CentrexDbContext())
                {
                    var registros = ctx.RegistrosStock.Where(r => r.FechaIngreso != DbFunctions.TruncateTime(DateTime.Now)).ToList();
                    foreach (var r in registros)
                        ((dynamic)r).Activo = false;
                    ctx.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Interaction.MsgBox(ex.Message);
            }
        }

        // ************************************ FUNCIONES DE STOCK ***************************
    }
}
