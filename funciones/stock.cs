using System;
using System.Data;
using System.Linq;
using System.Xml.Linq;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Centrex.Models;

namespace Centrex.Funciones
{

    static class stock
    {
        // ************************************ FUNCIONES DE STOCK ***************************

        public static RegistroStockEntity InfoRegistroStock(int id_rs)
        {
            var tmp = new RegistroStockEntity();
            try
            {
                using (var ctx = new CentrexDbContext())
                {
                    var rs = ctx.RegistroStockEntity.AsNoTracking().FirstOrDefault(r => r.IdRegistro == id_rs);
                    if (rs is not null)
                    {
                        tmp.IdRegistro = rs.IdRegistro;
                        tmp.IdIngreso = rs.IdIngreso;
                        tmp.Fecha = rs.Fecha;
                        tmp.FechaIngreso = rs.FechaIngreso;
                        tmp.Factura = rs.Factura;
                        tmp.IdProveedor = rs.IdProveedor;
                        tmp.IdItem = rs.IdItem;
                        tmp.Cantidad = rs.Cantidad;
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

        public static RegistroStockEntity InfoRegistroStockTmp(int id_rs)
        {
            var tmp = new RegistroStockEntity();
            try
            {
                using (var ctx = new CentrexDbContext())
                {
                    var rs = ctx.TmpRegistroStockEntity.AsNoTracking().FirstOrDefault(r => r.IdRegistrotmp == id_rs);
                    if (rs is not null)
                    {
                        tmp.IdRegistro = rs.IdRegistrotmp;
                        tmp.IdIngreso = rs.IdIngreso;
                        tmp.Fecha = rs.Fecha.Value;
                        tmp.FechaIngreso = rs.FechaIngreso;
                        tmp.Factura = rs.Factura;
                        tmp.IdProveedor = rs.IdProveedor;
                        tmp.IdItem = rs.IdItem;
                        tmp.Cantidad = rs.Cantidad;
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

        public static bool AddStockTmp(RegistroStockEntity rs)
        {
            try
            {
                using (var ctx = new CentrexDbContext())
                {
                    var entity = new TmpRegistroStockEntity()
                    {
                        Fecha = rs.Fecha,
                        Factura = rs.Factura,
                        IdProveedor = rs.IdProveedor,
                        IdItem = rs.IdItem,
                        Cantidad = rs.Cantidad,
                        Costo = rs.Costo,
                        PrecioLista = rs.PrecioLista,
                        Factor = rs.Factor,
                        CantidadAnterior = rs.CantidadAnterior,
                        CostoAnterior = rs.CostoAnterior,
                        PrecioListaAnterior = rs.PrecioListaAnterior,
                        FactorAnterior = rs.FactorAnterior,
                        Nota = rs.Nota,
                        Activo = rs.Activo
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

        public static bool UpdateStockTmp(RegistroStockEntity rs)
        {
            try
            {
                using (var ctx = new CentrexDbContext())
                {
                    var entity = ctx.TmpRegistroStockEntity.FirstOrDefault(r => r.IdRegistrotmp == rs.IdRegistro);
                    if (entity is not null)
                    {
                        entity.Fecha = rs.Fecha;
                        entity.Factura = rs.Factura;
                        entity.IdProveedor = rs.IdProveedor;
                        entity.IdItem = rs.IdItem;
                        entity.Cantidad = rs.Cantidad;
                        entity.Costo = rs.Costo;
                        entity.PrecioLista = rs.PrecioLista;
                        entity.Factor = rs.Factor;
                        entity.Nota = rs.Nota;
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

        public static bool BorrarItemRegistroStockTmp(RegistroStockEntity rs)
        {
            try
            {
                using (var ctx = new CentrexDbContext())
                {
                    var entity = ctx.TmpRegistroStockEntity.FirstOrDefault(r => r.IdRegistrotmp == rs.IdRegistro);
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
                    var hoy = DateOnly.FromDateTime(DateTime.Now);
                    var registros = ctx.RegistroStockEntity
                        .Where(r => r.FechaIngreso != hoy)
                        .ToList();

                    foreach (var r in registros)
                        r.Activo = false;

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
