using System;
using System.Linq;

namespace Centrex.Funciones
{

    static class cuentas_bancarias
    {
        // ************************************ FUNCIONES DE CUENTAS BANCARIAS (VERSIÓN EF) ***************************

        // ----------------------------------------------------------
        // Obtener información de una cuenta bancaria por ID
        // ----------------------------------------------------------
        public static CuentaBancariaEntity info_cuentaBancaria(int IdCuentaBancaria)
        {
            var tmp = new CuentaBancariaEntity();
            try
            {
                using (CentrexDbContext context = new CentrexDbContext())
                {                    
                    var entidad = context.CuentaBancariaEntity.AsNoTracking().FirstOrDefault(c => c.IdCuentaBancaria == IdCuentaBancaria);

                    if (entidad is not null)
                    {
                        tmp.Nombre = "error";
                        return tmp;
                    }
                    else
                    {

                        tmp.Nombre = "error";
                        return tmp;
                    }
                }
            }
            catch (Exception ex)
            {
                Interaction.MsgBox(ex.Message.ToString());
                tmp.Nombre = "error";
                return tmp;
            }
        }

        // ----------------------------------------------------------
        // Agregar una nueva cuenta bancaria
        // ----------------------------------------------------------
        public static bool addcuentaBancaria(CuentaBancariaEntity cb)
        {
            try
            {
                using (CentrexDbContext context = new CentrexDbContext())
                {
                    var bancoEntity = new CuentaBancariaEntity();

                    bancoEntity.Nombre = cb.Nombre;
                    bancoEntity.IdMoneda = cb.IdMoneda;
                    bancoEntity.Saldo = cb.Saldo;
                    bancoEntity.Activo = cb.Activo;

                    context.CuentaBancariaEntity.Add(bancoEntity);
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

        // ----------------------------------------------------------
        // Actualizar o desactivar una cuenta bancaria
        // ----------------------------------------------------------
        public static bool updatecuentaBancaria(CuentaBancariaEntity cb, bool borra = false)
        {
            try
            {
                int id = cb.IdCuentaBancaria;
                using (CentrexDbContext ctx = new CentrexDbContext())
                {
                    var entidad = ctx.CuentaBancariaEntity.FirstOrDefault(c => c.IdCuentaBancaria == id);

                    if (entidad is null)
                    {
                        Interaction.MsgBox("No se encontró la cuenta bancaria especificada.", Constants.vbExclamation);
                        return false;
                    }

                    if (borra)
                    {
                        entidad.Activo = false;
                    }
                    else
                    {
                        entidad.IdBanco = cb.IdBanco;
                        entidad.Nombre = cb.Nombre;
                        entidad.IdMoneda = cb.IdMoneda;
                        entidad.Saldo = cb.Saldo;
                        entidad.Activo = cb.Activo;
                    }

                    ctx.Entry(entidad).State = EntityState.Modified;
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

        // ----------------------------------------------------------
        // Borrar completamente una cuenta bancaria
        // ----------------------------------------------------------
        public static bool borrarcuenta_Bancaria(CuentaBancariaEntity cb)
        {
            try
            {
                using (CentrexDbContext ctx = new CentrexDbContext())
                {
                    int id = cb.IdCuentaBancaria;
                    var entidad = ctx.CuentaBancariaEntity.FirstOrDefault(c => c.IdCuentaBancaria == id);

                    if (entidad is null)
                    {
                        Interaction.MsgBox("No se encontró la cuenta bancaria a eliminar.", Constants.vbExclamation);
                        return false;
                    }

                    ctx.CuentaBancariaEntity.Remove(entidad);
                    ctx.SaveChanges();

                    return true;
                }
            }
            catch (Exception ex)
            {
                Interaction.MsgBox(ex.Message.ToString());
                return false;
            }
        }
        // ************************************ FIN DE FUNCIONES DE CUENTAS BANCARIAS ***************************
    }
}
