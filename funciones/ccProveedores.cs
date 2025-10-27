using System;
using System.Data;
using System.Linq;
using System.Xml.Linq;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Centrex.Models;

namespace Centrex
{

    static class ccProveedores
    {

        // ************************************ FUNCIONES DE CUENTAS CORRIENTES DE PROVEEDORES **********************
        public static ccProveedor info_ccProveedor(int id_cc)
        {
            var tmp = new ccProveedor();

            try
            {
                using (CentrexDbContext context = GetDbContext())
                {
                    var ccEntity = context.CcProveedorEntity.FirstOrDefault(cc => cc.IdCc == id_cc);

                    if (ccEntity is not null)
                    {
                        tmp.id_cc = ccEntity.IdCc.ToString();
                        tmp.IdProveedor = ccEntity.IdProveedor.ToString();
                        tmp.id_moneda = ccEntity.IdMoneda.ToString();
                        tmp.nombre = ccEntity.Nombre;
                        tmp.saldo = Conversions.ToDouble(ccEntity.Saldo.ToString());
                        tmp.activo = ccEntity.Activo;
                    }
                    else
                    {
                        tmp.nombre = "error";
                    }
                }

                return tmp;
            }
            catch (Exception ex)
            {
                Interaction.MsgBox(ex.Message.ToString());
                tmp.nombre = "error";
                return tmp;
            }
        }

        public static bool addCCProveedor(ccProveedor cc)
        {
            try
            {
                using (CentrexDbContext context = GetDbContext())
                {
                    var ccEntity = new CcProveedorEntity()
                    {
                        IdProveedor = cc.IdProveedor,
                        IdMoneda = cc.id_moneda,
                        Nombre = cc.nombre,
                        Saldo = (decimal)cc.saldo,
                        Activo = cc.activo
                    };

                    context.CcProveedorEntity.Add(ccEntity);
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Interaction.MsgBox(ex.Message);
                return false;
            }
        }

        public static bool updateCCProveedor(ccProveedor cc, bool borra = false)
        {
            try
            {
                using (CentrexDbContext context = GetDbContext())
                {
                    var ccEntity = context.CcProveedorEntity.FirstOrDefault(c => c.IdCc == cc.id_cc);

                    if (ccEntity is not null)
                    {
                        if (borra == true)
                        {
                            ccEntity.activo = false;
                        }
                        else
                        {
                            ccEntity.IdProveedor = cc.IdProveedor;
                            ccEntity.IdMoneda = cc.id_moneda;
                            ccEntity.nombre = cc.nombre;
                            ccEntity.saldo = (decimal)cc.saldo;
                            ccEntity.activo = cc.activo;
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
                Interaction.MsgBox(ex.Message);
                return false;
            }
        }

        public static bool borrarccProveedor(ccProveedor cc)
        {
            try
            {
                using (CentrexDbContext context = GetDbContext())
                {
                    var ccEntity = context.CcProveedorEntity.FirstOrDefault(c => c.IdCc == cc.id_cc);

                    if (ccEntity is not null)
                    {
                        context.CcProveedorEntity.Remove(ccEntity);
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
                Interaction.MsgBox(ex.Message.ToString());
                return false;
            }
        }


        public static string consultaTotalCcProveedor(int id_proveedor, DateTime fecha_desde, DateTime fecha_hasta)
        {
            try
            {
                using (CentrexDbContext context = GetDbContext())
                {
                    var importeTotal = (from trans in context.Transacciones
                                        join tipoComp in context.TiposComprobantes on trans.IdTipoComprobante equals tipoComp.IdTipoComprobante
                                        where trans.fecha >= fecha_desde && trans.fecha <= fecha_hasta && trans.IdProveedor == id_proveedor && tipoComp.contabilizar == true
                                        select trans.total).Distinct().Sum();

                    // Manejo de valores nulos
                    if (importeTotal is null)
                    {
                        return "0";
                    }
                    else
                    {
                        return importeTotal.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                Interaction.MsgBox(ex.Message.ToString());
                return "";
            }
        }

        // ************************************ FUNCIONES DE CUENTAS CORRIENTES DE PROVEEDORES **********************
    }
}
