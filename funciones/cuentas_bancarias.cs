using System;
using System.Linq;
using System.Xml.Linq;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Centrex.Models;

namespace Centrex
{

    static class cuentas_bancarias
    {
        // ************************************ FUNCIONES DE CUENTAS BANCARIAS (VERSIÓN EF) ***************************
        private static CentrexDbContext ctx = new CentrexDbContext();

        // ----------------------------------------------------------
        // Obtener información de una cuenta bancaria por ID
        // ----------------------------------------------------------
        public static cuenta_bancaria info_cuentaBancaria(string id_cuentaBancaria)
        {
            var tmp = new cuenta_bancaria();
            try
            {
                int id = Conversions.ToInteger(id_cuentaBancaria);
                var entidad = ctx.CuentasBancarias.FirstOrDefault(c => c.IdCuentaBancaria == id);

                if (entidad is null)
                {
                    tmp.nombre = "error";
                    return tmp;
                }

                tmp.id_cuentaBancaria = entidad.IdCuentaBancaria;
                tmp.id_banco = entidad.IdBanco;
                tmp.nombre = entidad.nombre;
                tmp.id_moneda = entidad.IdMoneda;
                tmp.saldo = (double)entidad.saldo;
                tmp.activo = entidad.activo;

                return tmp;
            }
            catch (Exception ex)
            {
                Interaction.MsgBox(ex.Message.ToString());
                tmp.nombre = "error";
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
                var nueva = new CuentaBancariaEntity()
                {
                    IdBanco = cb.id_banco,
                    nombre = cb.nombre,
                    IdMoneda = cb.id_moneda,
                    saldo = cb.saldo,
                    activo = cb.activo
                };

                ctx.CuentasBancarias.Add(nueva);
                ctx.SaveChanges();

                return true;
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
                int id = cb.id_cuentaBancaria;
                var entidad = ctx.CuentasBancarias.FirstOrDefault(c => c.IdCuentaBancaria == id);

                if (entidad is null)
                {
                    Interaction.MsgBox("No se encontró la cuenta bancaria especificada.", Constants.vbExclamation);
                    return false;
                }

                if (borra)
                {
                    entidad.activo = false;
                }
                else
                {
                    entidad.IdBanco = cb.id_banco;
                    entidad.nombre = cb.nombre;
                    entidad.IdMoneda = cb.id_moneda;
                    entidad.saldo = cb.saldo;
                    entidad.activo = cb.activo;
                }

                ctx.Entry(entidad).State = EntityState.Modified;
                ctx.SaveChanges();

                return true;
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
        public static bool borrarcuenta_Bancaria(cuenta_bancaria cb)
        {
            try
            {
                int id = cb.id_cuentaBancaria;
                var entidad = ctx.CuentasBancarias.FirstOrDefault(c => c.IdCuentaBancaria == id);

                if (entidad is null)
                {
                    Interaction.MsgBox("No se encontró la cuenta bancaria a eliminar.", Constants.vbExclamation);
                    return false;
                }

                ctx.CuentasBancarias.Remove(entidad);
                ctx.SaveChanges();

                return true;
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
