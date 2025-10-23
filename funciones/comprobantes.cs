using System;
using System.Data;
using System.Linq;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Centrex.Models;

namespace Centrex
{

    // ************************************
    // Módulo de funciones de comprobantes (Entity Framework)
    // Migrado desde SQL manual a LINQ/EF
    // ************************************
    static class comprobantes
    {

        // =============================================
        // OBTENER INFORMACIÓN DE UN COMPROBANTE
        // =============================================
        public static comprobante info_comprobante(string id_comprobante)
        {
            var tmp = new comprobante();

            if (string.IsNullOrEmpty(id_comprobante))
            {
                tmp.comprobanteField = "error";
                return tmp;
            }

            try
            {
                using (var context = new CentrexDbContext())
                {
                    var comprobanteEntity = context.Comprobantes.Include(c => c.TipoComprobante).FirstOrDefault(c => c.IdComprobante == Conversions.ToInteger(id_comprobante));

                    if (comprobanteEntity is not null)
                    {
                        tmp.id_comprobante = comprobanteEntity.IdComprobante.ToString();
                        tmp.comprobanteField = comprobanteEntity.comprobante;
                        tmp.id_tipoComprobante = comprobanteEntity.IdTipoComprobante.ToString();
                        tmp.numeroComprobante = comprobanteEntity.numeroComprobante;
                        tmp.puntoVenta = comprobanteEntity.puntoVenta;
                        tmp.esFiscal = comprobanteEntity.esFiscal.HasValue ? comprobanteEntity.esFiscal.Value : false;
                        tmp.esElectronica = comprobanteEntity.esElectronica.HasValue ? comprobanteEntity.esElectronica.Value : false;
                        tmp.esManual = comprobanteEntity.esManual.HasValue ? comprobanteEntity.esManual.Value : false;
                        tmp.esPresupuesto = comprobanteEntity.esPresupuesto.HasValue ? comprobanteEntity.esPresupuesto.Value : false;
                        tmp.activo = comprobanteEntity.activo;
                        tmp.testing = comprobanteEntity.testing;
                        tmp.maxItems = comprobanteEntity.maxItems.HasValue ? comprobanteEntity.maxItems.Value : 0;
                        tmp.comprobanteRelacionado = comprobanteEntity.comprobanteRelacionado.HasValue ? comprobanteEntity.comprobanteRelacionado.Value : 0;
                        tmp.esMiPyME = comprobanteEntity.esMiPyME;
                        tmp.CBU_emisor = comprobanteEntity.CBUEmisor;
                        tmp.alias_CBU_emisor = comprobanteEntity.AliasCBUEmisor;
                        tmp.anula_MiPyME = comprobanteEntity.AnulaMiPyME;
                        tmp.contabilizar = comprobanteEntity.contabilizar;
                        tmp.mueveStock = comprobanteEntity.mueveStock;
                        tmp.id_modoMiPyme = comprobanteEntity.IdModoMiPyme;
                        tmp.prefijo = comprobanteEntity.TipoComprobante is not null ? comprobanteEntity.TipoComprobante.nombreAbreviado : "";
                    }
                    else
                    {
                        tmp.comprobanteField = "error";
                    }
                }
            }
            catch (Exception ex)
            {
                Interaction.MsgBox(ex.Message.ToString());
                tmp.comprobanteField = "error";
            }

            return tmp;
        }

        // =============================================
        // AGREGAR COMPROBANTE
        // =============================================
        public static bool addcomprobante(comprobante c)
        {
            try
            {
                using (var context = new CentrexDbContext())
                {
                    var comprobanteEntity = new ComprobanteEntity()
                    {
                        comprobante = c.comprobanteField,
                        IdTipoComprobante = c.id_tipoComprobante,
                        numeroComprobante = c.numeroComprobante,
                        puntoVenta = c.puntoVenta,
                        esFiscal = c.esFiscal,
                        esElectronica = c.esElectronica,
                        esManual = c.esManual,
                        esPresupuesto = c.esPresupuesto,
                        activo = c.activo,
                        testing = c.testing,
                        maxItems = c.maxItems,
                        comprobanteRelacionado = c.comprobanteRelacionado,
                        esMiPyME = c.esMiPyME,
                        CBUEmisor = c.CBU_emisor,
                        AliasCBUEmisor = c.alias_CBU_emisor,
                        AnulaMiPyME = c.anula_MiPyME,
                        contabilizar = c.contabilizar,
                        mueveStock = c.mueveStock,
                        IdModoMiPyme = c.id_modoMiPyme
                    };

                    context.Comprobantes.Add(comprobanteEntity);
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

        // =============================================
        // ACTUALIZAR COMPROBANTE
        // =============================================
        public static bool updatecomprobante(comprobante c, bool borra = false)
        {
            try
            {
                using (var context = new CentrexDbContext())
                {
                    var comprobanteEntity = context.Comprobantes.FirstOrDefault(comp => comp.IdComprobante == c.id_comprobante);

                    if (comprobanteEntity is null)
                        return false;

                    if (borra)
                    {
                        comprobanteEntity.activo = false;
                    }
                    else
                    {
                        comprobanteEntity.comprobante = c.comprobanteField;
                        comprobanteEntity.IdTipoComprobante = c.id_tipoComprobante;
                        comprobanteEntity.numeroComprobante = c.numeroComprobante;
                        comprobanteEntity.puntoVenta = c.puntoVenta;
                        comprobanteEntity.esFiscal = c.esFiscal;
                        comprobanteEntity.esElectronica = c.esElectronica;
                        comprobanteEntity.esManual = c.esManual;
                        comprobanteEntity.esPresupuesto = c.esPresupuesto;
                        comprobanteEntity.activo = c.activo;
                        comprobanteEntity.testing = c.testing;
                        comprobanteEntity.maxItems = c.maxItems;
                        comprobanteEntity.comprobanteRelacionado = c.comprobanteRelacionado;
                        comprobanteEntity.esMiPyME = c.esMiPyME;
                        comprobanteEntity.CBUEmisor = c.CBU_emisor;
                        comprobanteEntity.AliasCBUEmisor = c.alias_CBU_emisor;
                        comprobanteEntity.AnulaMiPyME = c.anula_MiPyME;
                        comprobanteEntity.contabilizar = c.contabilizar;
                        comprobanteEntity.mueveStock = c.mueveStock;
                        comprobanteEntity.IdModoMiPyme = c.id_modoMiPyme;
                    }

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

        // =============================================
        // VERIFICAR COMPROBANTE DEFAULT
        // =============================================
        public static bool estaComprobanteDefault(string condicion, int id_comprobanteDefault)
        {
            try
            {
                using (var context = new CentrexDbContext())
                {
                    var comprobantes = context.Comprobantes.Include(c => c.TipoComprobante).Where(c => c.activo == true && (c.IdTipoComprobante == 1 || c.IdTipoComprobante == 2 || c.IdTipoComprobante == 3 || c.IdTipoComprobante == 4 || c.IdTipoComprobante == 5 || c.IdTipoComprobante == 34 || c.IdTipoComprobante == 39 || c.IdTipoComprobante == 60 || c.IdTipoComprobante == 63 || c.IdTipoComprobante == 0 || c.IdTipoComprobante == 99 || c.IdTipoComprobante == 199 || c.IdComprobante == id_comprobanteDefault)).ToList();


                    return comprobantes.Any();
                }
            }
            catch (Exception ex)
            {
                Interaction.MsgBox(ex.Message);
                return false;
            }
        }

        // =============================================
        // OBTENER ID DE COMPROBANTE DE ANULACIÓN
        // =============================================
        public static int info_comprobante_anulacion(string id_tipoComprobante)
        {
            try
            {
                using (var context = new CentrexDbContext())
                {
                    var tipoComprobante = context.TiposComprobantes.FirstOrDefault(tc => tc.IdTipoComprobante == Conversions.ToInteger(id_tipoComprobante));
                    if (tipoComprobante is not null && tipoComprobante.IdAnulaTipoComprobante.HasValue)
                    {
                        return tipoComprobante.IdAnulaTipoComprobante.Value;
                    }
                    return -1;
                }
            }
            catch (Exception ex)
            {
                Interaction.MsgBox(ex.Message.ToString());
                return -1;
            }
        }

    }
}
