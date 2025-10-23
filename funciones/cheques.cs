using System;
using System.Linq;
using System.Xml.Linq;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Centrex.Models;

namespace Centrex
{

    static class cheques
    {
        // ************************************ FUNCIONES DE CHEQUES ***************************
        public static ChequeEntity info_cheque(string id_cheque)
        {
            try
            {
                using (var context = new CentrexDbContext())
                {
                    return context.Cheques.FirstOrDefault(c => c.IdCheque == Conversions.ToInteger(id_cheque));
                }
            }
            catch (Exception ex)
            {
                Interaction.MsgBox(ex.Message.ToString());
                return null;
            }
        }

        public static bool addch(cheque ch)
        {
            try
            {
                using (var context = new CentrexDbContext())
                {
                    var chequeEntity = new ChequeEntity()
                    {
                        FechaIngreso = DateTime.Now,
                        FechaEmision = !string.IsNullOrEmpty(ch.fecha_emision) ? Conversions.ToDate(ch.fecha_emision) : DateTime.Now,
                        IdCliente = ch.id_cliente != 0 ? ch.id_cliente : default,
                        IdProveedor = ch.IdProveedor != 0 ? ch.IdProveedor : default,
                        IdBanco = ch.id_banco,
                        nCheque = Conversions.ToInteger(ch.nCheque),
                        nCheque2 = Conversions.ToInteger(ch.nCheque2),
                        importe = (decimal)ch.importe,
                        IdEstadoCh = ch.id_estadoch,
                        FechaCobro = !string.IsNullOrEmpty(ch.fecha_cobro) ? Conversions.ToDate(ch.fecha_cobro) : default,
                        FechaSalida = !string.IsNullOrEmpty(ch.fecha_salida) ? Conversions.ToDate(ch.fecha_salida) : default,
                        FechaDeposito = !string.IsNullOrEmpty(ch.fecha_deposito) ? Conversions.ToDate(ch.fecha_deposito) : default,
                        recibido = ch.recibido,
                        Emitido = ch.Emitido,
                        IdCuentaBancaria = ch.id_cuentaBancaria != 0 ? ch.id_cuentaBancaria : default,
                        eCheck = ch.eCheck,
                        activo = true
                    };

                    context.Cheques.Add(chequeEntity);
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

        public static bool updatech(cheque ch, bool borra = false)
        {
            try
            {
                using (var context = new CentrexDbContext())
                {
                    var chequeEntity = context.Cheques.FirstOrDefault(c => c.IdCheque == ch.id_cheque);

                    if (chequeEntity is not null)
                    {
                        if (borra == true)
                        {
                            chequeEntity.activo = false;
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(ch.fecha_emision))
                            {
                                chequeEntity.FechaEmision = Conversions.ToDate(ch.fecha_emision);
                            }
                            chequeEntity.IdBanco = ch.id_banco;
                            chequeEntity.nCheque = Conversions.ToInteger(ch.nCheque);
                            chequeEntity.nCheque2 = Conversions.ToInteger(ch.nCheque2);
                            chequeEntity.importe = (decimal)ch.importe;
                            chequeEntity.IdEstadoCh = ch.id_estadoch;
                            if (!string.IsNullOrEmpty(ch.fecha_cobro))
                            {
                                chequeEntity.FechaCobro = Conversions.ToDate(ch.fecha_cobro);
                            }
                            if (!string.IsNullOrEmpty(ch.fecha_salida))
                            {
                                chequeEntity.FechaSalida = Conversions.ToDate(ch.fecha_salida);
                            }
                            if (!string.IsNullOrEmpty(ch.fecha_deposito))
                            {
                                chequeEntity.FechaDeposito = Conversions.ToDate(ch.fecha_deposito);
                            }
                            chequeEntity.recibido = ch.recibido;
                            chequeEntity.Emitido = ch.Emitido;
                            chequeEntity.eCheck = ch.eCheck;
                            if (ch.id_cliente != 0)
                            {
                                chequeEntity.IdCliente = ch.id_cliente;
                            }
                            else if (ch.IdProveedor != 0)
                            {
                                chequeEntity.IdProveedor = ch.IdProveedor;
                            }
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

        public static bool borrarch(cheque ch)
        {
            try
            {
                using (var context = new CentrexDbContext())
                {
                    var chequeEntity = context.Cheques.FirstOrDefault(c => c.IdCheque == ch.id_cheque);

                    if (chequeEntity is not null)
                    {
                        context.Cheques.Remove(chequeEntity);
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

        public static bool borrarch(int id_cheque)
        {
            try
            {
                using (var context = new CentrexDbContext())
                {
                    var chequeEntity = context.Cheques.FirstOrDefault(c => c.IdCheque == id_cheque);

                    if (chequeEntity is not null)
                    {
                        context.Cheques.Remove(chequeEntity);
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

        public static bool Depositar_cheque(cheque ch)
        {
            try
            {
                using (var context = new CentrexDbContext())
                {
                    var chequeEntity = context.Cheques.FirstOrDefault(c => c.IdCheque == ch.id_cheque);

                    if (chequeEntity is not null)
                    {
                        chequeEntity.IdEstadoCh = VariablesGlobales.ID_CH_DEPOSITADO;
                        chequeEntity.FechaDeposito = !string.IsNullOrEmpty(ch.fecha_deposito) ? Conversions.ToDate(ch.fecha_deposito) : DateTime.Now;
                        chequeEntity.IdCuentaBancaria = ch.id_cuentaBancaria;

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

        public static bool Anular_Deposito_Cheque(int id_cheque)
        {
            try
            {
                using (var context = new CentrexDbContext())
                {
                    var chequeEntity = context.Cheques.FirstOrDefault(c => c.IdCheque == id_cheque);

                    if (chequeEntity is not null)
                    {
                        chequeEntity.IdEstadoCh = VariablesGlobales.ID_CH_CARTERA;
                        chequeEntity.FechaDeposito = (object)null;
                        chequeEntity.IdCuentaBancaria = (object)null;

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

        public static bool Existe_N_Cheque(string nCheque)
        {
            try
            {
                using (var context = new CentrexDbContext())
                {
                    int count = context.Cheques.Count(c => c.nCheque.ToString() == Strings.Trim(nCheque));
                    return count > 0;
                }
            }
            catch (Exception ex)
            {
                Interaction.MsgBox(ex.Message.ToString());
                return false;
            }
        }

        public static int Ultimo_N2_Cheque()
        {
            try
            {
                using (var context = new CentrexDbContext())
                {
                    if (context.Cheques.Any())
                    {
                        return context.Cheques.Max(c => c.nCheque2);
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Interaction.MsgBox(ex.Message.ToString());
                return 0;
            }
        }

        // ************************************ FUNCIONES DE CHEQUES ***************************
    }
}
