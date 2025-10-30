using System;
using System.Data;
using System.Linq;

namespace Centrex.Funciones
{

    static class ccProveedores
    {

        // ************************************ FUNCIONES DE CUENTAS CORRIENTES DE PROVEEDORES **********************
        public static CcProveedorEntity info_ccProveedor(int  _ccP)
        {
            try
            {
                using (CentrexDbContext context = new CentrexDbContext())
                {
                    if (_ccP != 0 || _ccP != -1)
                    {
                        return context.CcProveedorEntity.FirstOrDefault(c => c.IdProveedor == _ccP);
                    }
                    else                     {
                        return null;
                    }
                }                
            }
            catch (Exception ex)
            {
                Interaction.MsgBox(ex.Message.ToString());                
                return null;
            }
        }

        public static bool addCCProveedor(CcProveedorEntity _ccP)
        {
            try
            {
                using (CentrexDbContext context = new CentrexDbContext())
                {
                    var tmp = new CcProveedorEntity()
                    {
                        IdProveedor = _ccP.IdProveedor,
                        IdMoneda = _ccP.IdMoneda,
                        Nombre = _ccP.Nombre,
                        Saldo = _ccP.Saldo,
                        Activo = _ccP.Activo
                    };

                    context.CcProveedorEntity.Add(tmp);
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

        public static bool updateCCProveedor(CcProveedorEntity _ccP, bool borra = false)
        {
            try
            {
                using (CentrexDbContext context = new CentrexDbContext())
                {
                    if (_ccP is not null)
                    {
                        if (borra == true)
                        {
                            _ccP.Activo = false;
                        }
                        else
                        {
                            _ccP.IdProveedor = _ccP.IdProveedor;
                            _ccP.IdMoneda = _ccP.IdMoneda;
                            _ccP.Nombre = _ccP.Nombre;
                            _ccP.Saldo = _ccP.Saldo;
                            _ccP.Activo = _ccP.Activo;
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

        public static bool borrarccProveedor(CcProveedorEntity _ccP)
        {
            try
            {
                using (CentrexDbContext context = new CentrexDbContext())
                {
                    if (_ccP is not null)
                    {
                        context.CcProveedorEntity.Remove(_ccP);
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


        public static string consultaTotalCcProveedor(int id_proveedor, DateOnly fecha_desde, DateOnly fecha_hasta)
        {
            try
            {
                using (CentrexDbContext context = new CentrexDbContext())
                {
                    var importeTotal = (from trans in context.TransaccionEntity
                                        join tipoComp in context.TipoComprobanteEntity on trans.IdTipoComprobante equals tipoComp.IdTipoComprobante
                                        where trans.Fecha >= fecha_desde && trans.Fecha <= fecha_hasta && trans.IdProveedor == id_proveedor && tipoComp.Contabilizar == true
                                        select trans.Total).Distinct().Sum();

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
