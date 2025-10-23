using System;
using System.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Centrex.Models;

namespace Centrex
{

    static class cobros
    {
        // ************************************ FUNCIONES DE COBROS ***************************
        public static CobroEntity info_cobro(string id_cobro)
        {
            try
            {
                using (var context = new CentrexDbContext())
                {
                    return context.Cobros.FirstOrDefault(c => c.IdCobro == Conversions.ToInteger(id_cobro));
                }
            }
            catch (Exception ex)
            {
                Interaction.MsgBox(ex.Message.ToString());
                return null;
            }
        }

        public static int addcobro(cobro c)
        {
            try
            {
                using (var context = new CentrexDbContext())
                {
                    var cobroEntity = new CobroEntity()
                    {
                        IdCobroOficial = c.id_cobro_oficial,
                        IdCobroNoOficial = c.id_cobro_no_oficial,
                        FechaCarga = DateTime.Now,
                        FechaCobro = !string.IsNullOrEmpty(c.fecha_cobro) ? Conversions.ToDate(c.fecha_cobro) : DateTime.Now,
                        IdCliente = c.id_cliente,
                        IdCc = c.id_cc,
                        dineroEnCc = (decimal)c.dineroEnCc,
                        efectivo = (decimal)c.efectivo,
                        totalTransferencia = (decimal)c.totalTransferencia,
                        totalCh = (decimal)c.totalCh,
                        totalRetencion = (decimal)c.totalRetencion,
                        total = (decimal)c.total,
                        hayCheque = c.hayCheque,
                        hayTransferencia = c.hayTransferencia,
                        hayRetencion = c.hayRetencion,
                        activo = true,
                        IdAnulaCobro = c.id_anulaCobro != -1 ? c.id_anulaCobro : default,
                        notas = c.notas ?? "",
                        firmante = c.firmante ?? ""
                    };

                    context.Cobros.Add(cobroEntity);
                    context.SaveChanges();

                    return cobroEntity.IdCobro;
                }
            }
            catch (Exception ex)
            {
                Interaction.MsgBox(ex.Message);
                return -1;
            }
        }

        public static bool borrarcobro(cobro c)
        {
            try
            {
                var ac = new cobro();
                var cc = new ccCliente();

                ac = c;
                ac.dineroEnCc = ac.dineroEnCc * -1;
                ac.efectivo = ac.efectivo * -1;
                ac.totalTransferencia = ac.totalTransferencia * -1;
                ac.totalCh = ac.totalCh * -1;
                ac.totalRetencion = ac.totalRetencion * -1;
                ac.total = ac.total * -1;
                ac.id_anulaCobro = c.id_cobro;
                if (ac.id_cobro_no_oficial == -1)
                {
                    ac.notas += Constants.vbCr + "Anula cobro: " + ac.id_cobro_oficial.ToString();
                }
                else
                {
                    ac.notas += Constants.vbCr + "Anula cobro: " + ac.id_cobro_no_oficial.ToString();
                }

                // Agrego un cobro exactamente al revez, referenciando al cobro original
                ac.id_cobro = addcobro(ac);

                // Borro todas las transferencias que pertenecen a ese cobro
                borrar_transferencias(c.id_cobro);

                // Borro todas las retenciones que pertenecen a ese cobro
                borrar_retenciones(c.id_cobro);

                if (ac.id_cobro > 0)
                {
                    // Borro los cheques asignados al cobro para liberarlos y actualizo el saldo
                    borrar_chequeCobrado(ac.id_cobro);
                    cc = ccClientes.info_ccCliente(c.id_cc);
                    cc.saldo -= c.total;
                    ccClientes.updateCCCliente(cc);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Interaction.MsgBox(ex.Message);
                return false;
            }
        }

        public static bool add_chequeCobrado(int id_cobro, int[] cheques)
        {
            try
            {
                using (var context = new CentrexDbContext())
                {
                    foreach (var idCheque in cheques)
                    {
                        // Buscar el cheque y actualizar estado
                        var ch = context.Cheques.FirstOrDefault(c => c.IdCheque == idCheque);
                        if (ch is not null)
                        {
                            ch.IdEstadoCh = VariablesGlobales.ID_CH_ENTREGADO;   // Constante definida en mconfig
                            ch.FechaSalida = DateTime.Now;
                            context.Entry(ch).State = EntityState.Modified;
                        }

                        // Crear la relación Cobro-Cheque
                        var cobroCheque = new CobroChequeEntity()
                        {
                            IdCobro = id_cobro,
                            idCheque = idCheque
                        };
                        context.CobrosCheques.Add(cobroCheque);
                    }

                    context.SaveChanges();
                    return true;
                }
            }

            catch (Exception ex)
            {
                Interaction.MsgBox("Error al vincular cheques con el cobro: " + ex.Message, MsgBoxStyle.Critical, "Centrex");
                return false;
            }
        }


        public static bool borrar_chequeCobrado(int id_cobro)
        {
            try
            {
                using (var context = new CentrexDbContext())
                {
                    var chequesCobrados = context.CobrosCheques.Where(cc => cc.IdCobro == id_cobro).ToList();

                    foreach (var cobroCheque in chequesCobrados)
                        context.CobrosCheques.Remove((CobrosChequeEntity)cobroCheque);

                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Interaction.MsgBox(ex.Message.ToString());
                return false;
            }
        }

        public static bool liberar_chequesCobrados(int id_cobro)
        {
            try
            {
                using (var context = new CentrexDbContext())
                {
                    // Obtener los IDs de cheques asociados al cobro
                    var idsChequesQuery = from cc in context.CobrosCheques
                                          where cc.IdCobro == id_cobro
                                          select cc.IdCheque;

                    var idsCheques = idsChequesQuery.ToList();

                    // Actualizar el estado de los cheques a CARTERA
                    foreach (var idCheque in idsCheques)
                    {
                        var cheque = context.Cheques.FirstOrDefault(ch => Operators.ConditionalCompareObjectEqual(ch.IdCheque, idCheque, false));
                        if (cheque is not null)
                        {
                            cheque.IdEstadoCh = VariablesGlobales.ID_CH_CARTERA;
                        }
                    }

                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Interaction.MsgBox(ex.Message.ToString());
                return false;
            }
        }

        public static bool borrar_transferencias(int id_cobro)
        {
            try
            {
                using (var context = new CentrexDbContext())
                {
                    var transferencias = context.Transferencias.Where(t => t.IdCobro == id_cobro).ToList();

                    foreach (var transferencia in transferencias)
                        context.Transferencias.Remove((TransferenciaEntity)transferencia);

                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Interaction.MsgBox(ex.Message.ToString());
                return false;
            }
        }

        public static bool borrar_retenciones(int id_cobro)
        {
            try
            {
                using (var context = new CentrexDbContext())
                {
                    var retenciones = context.CobrosRetenciones.Where(r => r.IdCobro == id_cobro).ToList();

                    foreach (var retencion in retenciones)
                        context.CobrosRetenciones.Remove((CobroRetencionEntity)retencion);

                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Interaction.MsgBox(ex.Message.ToString());
                return false;
            }
        }

        public static bool guardar_cobros_facturas_importes(int id_cobro, DataGridView dg_view)
        {
            try
            {
                using (var context = new CentrexDbContext())
                {
                    // Eliminar relaciones previas del mismo cobro
                    var previas = context.CobrosFacturasImportes.Where(f => f.IdCobro == id_cobro).ToList();
                    foreach (var f in previas)
                        context.CobrosFacturasImportes.Remove((CobroNFacturaImporteEntity)f);

                    // Agregar nuevas relaciones desde el DataGridView
                    foreach (DataGridViewRow row in dg_view.Rows)
                    {
                        if (row.IsNewRow)
                            continue;
                        if (row.Cells["ID"].Value is null)
                            continue;

                        string nFactura = Convert.ToString(row.Cells["ID"].Value);
                        decimal importe = Convert.ToDecimal(row.Cells["Importe"].Value);
                        var fecha = row.Cells["Fecha"] is not null && row.Cells["Fecha"].Value is not null ? Convert.ToDateTime(row.Cells["Fecha"].Value) : DateTime.Now;

                        var nuevaRelacion = new CobroNFacturaImporteEntity()
                        {
                            IdCobro = id_cobro,
                            nfactura = nFactura,
                            importe = importe,
                            fecha = fecha
                        };
                        context.CobrosFacturasImportes.Add(nuevaRelacion);
                    }

                    context.SaveChanges();
                    return true;
                }
            }

            catch (Exception ex)
            {
                Interaction.MsgBox("Error al guardar facturas e importes del cobro: " + ex.Message, MsgBoxStyle.Critical, "Centrex");
                return false;
            }
        }

        public static bool Cobro_Ya_Anulado(string id_cobro)
        {
            try
            {
                using (var context = new CentrexDbContext())
                {
                    int idCobroInt = Conversions.ToInteger(id_cobro);
                    bool existe = context.Cobros.Any(c => c.IdAnulaCobro.HasValue && c.IdAnulaCobro.Value == idCobroInt);
                    return existe;
                }
            }
            catch (Exception ex)
            {
                Interaction.MsgBox(ex.Message.ToString());
                return false;
            }
        }

        public static int Ultimo_cobro_oficial()
        {
            try
            {
                using (var context = new CentrexDbContext())
                {
                    if (context.Cobros.Any())
                    {
                        var maxIdOficial = context.Cobros.Max(c => c.IdCobroOficial);
                        if (maxIdOficial == -1)
                        {
                            return 1;
                        }
                        else
                        {
                            return maxIdOficial + 1;
                        }
                    }
                    else
                    {
                        return 1;
                    }
                }
            }
            catch (Exception ex)
            {
                Interaction.MsgBox(ex.Message.ToString());
                return -1;
            }
        }

        public static int Ultimo_cobro_no_oficial()
        {
            try
            {
                using (var context = new CentrexDbContext())
                {
                    if (context.Cobros.Any())
                    {
                        var maxIdNoOficial = context.Cobros.Max(c => c.IdCobroNoOficial);
                        if (maxIdNoOficial == -1)
                        {
                            return 1;
                        }
                        else
                        {
                            return maxIdNoOficial + 1;
                        }
                    }
                    else
                    {
                        return 1;
                    }
                }
            }
            catch (Exception ex)
            {
                Interaction.MsgBox(ex.Message.ToString());
                return -1;
            }
        }
        // ************************************ FUNCIONES DE COBROS ***************************
    }
}
