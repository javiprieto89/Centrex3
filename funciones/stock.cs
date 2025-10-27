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
                    var rs = ctx.RegistroStockEntity.AsNoTracking().FirstOrDefault(r => r.IdRegistro == id_rs);
                    if (rs is not null)
                    {
                        tmp.IdRegistro = rs.IdRegistro;
                        tmp.IdIngreso = rs.IdIngreso;
                        tmp.Fecha = Conversions.ToString(rs.Fecha.Value);
                        tmp.FechaIngreso = rs.FechaIngreso;
                        tmp.Factura = rs.Factura;
                        tmp.IdProveedor = rs.IdProveedor;
                        tmp.IdItem = rs.IdItem;
                        tmp.Cantidad = (decimal)rs.Cantidad;
                        tmp.Costo = rs.Costo;
                        tmp.PrecioLista = rs.PrecioLista;
                        tmp.Factor = rs.Factor;
                        tmp.CantidadAnterior = rs.CantidadAnterior;
                        tmp.CostoAnterior = rs.CostoAnterior;
                        tmp.PrecioListaAnterior = rs.PrecioListaAnterior;
                        tmp.FactorAnterior = rs.FactorAnterior;
                        tmp.Nota = rs.Nota;
                        tmp.Activo = rs.Activo;
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
                    var rs = ctx.TmpRegistroStockEntity.AsNoTracking().FirstOrDefault(r => r.IdRegistroTmp == id_rs);
                    if (rs is not null)
                    {
                        tmp.IdRegistro = rs.IdRegistrotmp;
                        tmp.IdIngreso = rs.IdIngreso;
                        tmp.Fecha = Conversions.ToString(rs.Fecha.Value);
                        tmp.FechaIngreso = rs.FechaIngreso;
                        tmp.Factura = rs.Factura;
                        tmp.IdProveedor = rs.IdProveedor;
                        tmp.IdItem = rs.IdItem;
                        tmp.Cantidad = (decimal)rs.Cantidad;
                        tmp.Costo = rs.Costo;
                        tmp.PrecioLista = rs.PrecioLista;
                        tmp.Factor = (decimal)rs.Factor;
                        tmp.CantidadAnterior = rs.CantidadAnterior;
                        tmp.CostoAnterior = rs.CostoAnterior;
                        tmp.PrecioListaAnterior = rs.PrecioListaAnterior;
                        tmp.FactorAnterior = rs.FactorAnterior;
                        tmp.Nota = rs.Nota;
                        tmp.Activo = rs.Activo;
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
                        Fecha = Conversions.ToDate(rs.fecha),
                        Factura = rs.factura,
                        IdProveedor = rs.IdProveedor,
                        IdItem = rs.id_item,
                        Cantidad = (int)Math.Round(rs.cantidad),
                        Costo = rs.costo,
                        PrecioLista = rs.precio_lista,
                        Factor = rs.factor,
                        CantidadAnterior = rs.cantidad_anterior,
                        CostoAnterior = rs.costo_anterior,
                        PrecioListaAnterior = rs.precio_lista_anterior,
                        FactorAnterior = rs.factor_anterior,
                        Nota = rs.nota,
                        Activo = true
                    };
                    ctx.TmpRegistroStockEntity.Add(entity);
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
                    var entity = ctx.TmpRegistroStockEntity.FirstOrDefault(r => r.IdRegistroTmp == rs.id_registro);
                    if (entity is not null)
                    {
                        entity.Fecha = Conversions.ToDate(rs.fecha);
                        entity.Factura = rs.factura;
                        entity.IdProveedor = rs.IdProveedor;
                        entity.IdItem = rs.id_item;
                        entity.Cantidad = (int)Math.Round(rs.cantidad);
                        entity.Costo = rs.costo;
                        entity.PrecioLista = rs.precio_lista;
                        entity.Factor = rs.factor;
                        entity.Nota = rs.nota;
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
                    var tmpList = ctx.TmpRegistroStockEntity.ToList();
                    foreach (var tmp in tmpList)
                    {
                        var entity = new RegistroStockEntity()
                        {
                            IdIngreso = tmp.IdIngreso,
                            Fecha = tmp.Fecha,
                            Factura = tmp.Factura,
                            IdProveedor = tmp.IdProveedor,
                            IdItem = tmp.IdItem,
                            Cantidad = tmp.Cantidad,
                            Costo = tmp.Costo,
                            PrecioLista = tmp.PrecioLista,
                            Factor = (decimal)tmp.Factor,
                            CantidadAnterior = tmp.CantidadAnterior,
                            CostoAnterior = tmp.CostoAnterior,
                            PrecioListaAnterior = tmp.PrecioListaAnterior,
                            FactorAnterior = tmp.FactorAnterior,
                            Nota = tmp.Nota,
                            Activo = true
                        };
                        ctx.RegistroStockEntity.Add(entity);
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
                    var entity = ctx.TmpRegistroStockEntity.FirstOrDefault(r => r.IdRegistroTmp == rs.id_registro);
                    if (entity is not null)
                    {
                        ctx.TmpRegistroStockEntity.Remove(entity);
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
                    var registros = ctx.RegistroStockEntity.Where(r => r.FechaIngreso != DbFunctions.TruncateTime(DateTime.Now)).ToList();
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
