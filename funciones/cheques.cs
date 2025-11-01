using System;
using System.Linq;
using System.Windows.Forms;

namespace Centrex.Funciones
{

    static class cheques
    {
        // ************************************ FUNCIONES DE CHEQUES ***************************
        public static ChequeEntity info_cheque(int idCheque)
        {
            try
            {
                using (var context = new CentrexDbContext())
                {
                    return context.ChequeEntity.FirstOrDefault(c => c.IdCheque == idCheque);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener el cheque: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }


        public static bool addch(ChequeEntity ch)
        {
            try
            {
                using (var context = new CentrexDbContext())
                {
                    var chequeEntity = new ChequeEntity()
                    {
                        FechaEmision = ch.FechaEmision,
                        IdCliente = ch.IdCliente != 0 ? ch.IdCliente : default,
                        IdProveedor = ch.IdProveedor != 0 ? ch.IdProveedor : default,
                        IdBanco = ch.IdBanco,
                        NCheque = ch.NCheque,
                        NCheque2 = ch.NCheque2,
                        Importe = ch.Importe,
                        IdEstadoch = ch.IdEstadoch,
                        FechaCobro = ch.FechaCobro,
                        FechaSalida = ch.FechaSalida,
                        FechaDeposito = ch.FechaDeposito,
                        Recibido = ch.Recibido,
                        Emitido = ch.Emitido,
                        IdCuentaBancaria = ch.IdCuentaBancaria != 0 ? ch.IdCuentaBancaria : default,
                        ECheck = ch.ECheck,
                        Activo = ch.Activo
                    };

                    context.ChequeEntity.Add(chequeEntity);
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static bool updatech(ChequeEntity ch, bool borra = false)
        {
            try
            {
                using (var context = new CentrexDbContext())
                {
                    var chequeEntity = context.ChequeEntity.FirstOrDefault(c => c.IdCheque == ch.IdCheque);

                    if (chequeEntity is not null)
                    {
                        if (borra == true)
                        {
                            chequeEntity.Activo = false;
                        }
                        else
                        {
                            chequeEntity.FechaEmision = ch.FechaEmision;
                            chequeEntity.IdBanco = ch.IdBanco;
                            chequeEntity.NCheque = ch.NCheque2;
                            chequeEntity.NCheque2 = ch.NCheque2;
                            chequeEntity.Importe = ch.Importe;
                            chequeEntity.IdEstadoch = ch.IdEstadoch;
                            chequeEntity.FechaCobro = ch.FechaCobro;
                            chequeEntity.FechaSalida = ch.FechaSalida;
                            chequeEntity.FechaDeposito = ch.FechaDeposito;
                            chequeEntity.Recibido = ch.Recibido;
                            chequeEntity.Emitido = ch.Emitido;
                            chequeEntity.ECheck = ch.ECheck;
                            if (ch.IdCliente != 0)
                            {
                                chequeEntity.IdCliente = ch.IdCliente;
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
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static bool borrarch(ChequeEntity ch)
        {
            try
            {
                using (var context = new CentrexDbContext())
                {
                    var chequeEntity = context.ChequeEntity.FirstOrDefault(c => c.IdCheque == ch.IdCheque);

                    if (chequeEntity is not null)
                    {
                        context.ChequeEntity.Remove(chequeEntity);
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
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static bool borrarch(int IdCheque)
        {
            try
            {
                using (var context = new CentrexDbContext())
                {
                    var chequeEntity = context.ChequeEntity.FirstOrDefault(c => c.IdCheque == IdCheque);

                    if (chequeEntity is not null)
                    {
                        context.ChequeEntity.Remove(chequeEntity);
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
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static bool Depositar_cheque(ChequeEntity ch)
        {
            try
            {
                using (var context = new CentrexDbContext())
                {
                    var chequeEntity = context.ChequeEntity.FirstOrDefault(c => c.IdCheque == ch.IdCheque);

                    if (chequeEntity is not null)
                    {
                        chequeEntity.IdEstadoch = ID_CH_DEPOSITADO;
                        chequeEntity.FechaDeposito = ch.FechaDeposito;
                        chequeEntity.IdCuentaBancaria = ch.IdCuentaBancaria;

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
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static bool Anular_Deposito_Cheque(int IdCheque)
        {
            try
            {
                using (var context = new CentrexDbContext())
                {
                    var chequeEntity = context.ChequeEntity.FirstOrDefault(c => c.IdCheque == IdCheque);

                    if (chequeEntity is not null)
                    {
                        chequeEntity.IdEstadoch = ID_CH_CARTERA;
                        chequeEntity.FechaDeposito = null;
                        chequeEntity.IdCuentaBancaria = null;

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
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static bool Existe_N_Cheque(string nCheque)
        {
            try
            {
                using (var context = new CentrexDbContext())
                {
                    string trimmedNCheque = nCheque.Trim();
                    int count = context.ChequeEntity.Count(c => c.NCheque.ToString() == trimmedNCheque);
                    return count > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static int Ultimo_N2_Cheque()
        {
            try
            {
                using (var context = new CentrexDbContext())
                {
                    if (context.ChequeEntity.Any())
                    {
                        return context.ChequeEntity.Max(c => c.NCheque2);
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
        }

        // ************************************ FUNCIONES DE CHEQUES ***************************
    }
}
