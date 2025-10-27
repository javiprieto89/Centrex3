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
                tmp.Comprobante = "error";
                return tmp;
            }

            try
            {
                using (var context = new CentrexDbContext())
                {
                    var comprobanteEntity = context.ComprobanteEntity
     .Include(c => c.IdTipoComprobanteNavigation)
      .FirstOrDefault(c => c.IdComprobante == Conversions.ToInteger(id_comprobante));

        if (comprobanteEntity is not null)
  {
      tmp.IdComprobante = comprobanteEntity.IdComprobante;
               tmp.Comprobante = comprobanteEntity.Comprobante;
    tmp.IdTipoComprobante = comprobanteEntity.IdTipoComprobante;
                tmp.NumeroComprobante = comprobanteEntity.NumeroComprobante;
 tmp.PuntoVenta = comprobanteEntity.PuntoVenta;
               tmp.EsFiscal = comprobanteEntity.EsFiscal ?? false;
             tmp.EsElectronica = comprobanteEntity.EsElectronica ?? false;
                 tmp.EsManual = comprobanteEntity.EsManual ?? false;
        tmp.EsPresupuesto = comprobanteEntity.EsPresupuesto ?? false;
    tmp.Activo = comprobanteEntity.Activo;
   tmp.Testing = comprobanteEntity.Testing;
       tmp.MaxItems = comprobanteEntity.MaxItems ?? 0;
          tmp.ComprobanteRelacionado = comprobanteEntity.ComprobanteRelacionado ?? 0;
       tmp.EsMiPyME = comprobanteEntity.EsMiPyME;
            tmp.CbuEmisor = comprobanteEntity.CbuEmisor;
   tmp.AliasCbuEmisor = comprobanteEntity.AliasCbuEmisor;
    tmp.AnulaMiPyME = comprobanteEntity.AnulaMiPyME;
   tmp.Contabilizar = comprobanteEntity.Contabilizar;
       tmp.MueveStock = comprobanteEntity.MueveStock;
   tmp.IdModoMiPyme = comprobanteEntity.IdModoMiPyme;
     tmp.Prefijo = comprobanteEntity.IdTipoComprobanteNavigation?.NombreAbreviado ?? "";
               }
                    else
       {
      tmp.Comprobante = "error";
     }
       }
          }
  catch (Exception ex)
     {
       Interaction.MsgBox(ex.Message.ToString());
         tmp.Comprobante = "error";
            }

     return tmp;
        }

   // =============================================
        // OBTENER COMPROBANTE POR PUNTO DE VENTA Y TIPO
        // =============================================
        public static comprobante info_comprobante_porPtoYTipo(int puntoVenta, int tipoComprobante)
        {
 try
         {
    using (var context = new CentrexDbContext())
         {
        var comprobanteEntity = context.ComprobanteEntity
        .Include(c => c.IdTipoComprobanteNavigation)
           .FirstOrDefault(c => c.PuntoVenta == puntoVenta && c.IdTipoComprobante == tipoComprobante && c.Activo);

        if (comprobanteEntity is not null)
                  {
  var tmp = new comprobante
 {
  IdComprobante = comprobanteEntity.IdComprobante,
           Comprobante = comprobanteEntity.Comprobante,
           IdTipoComprobante = comprobanteEntity.IdTipoComprobante,
          NumeroComprobante = comprobanteEntity.NumeroComprobante,
    PuntoVenta = comprobanteEntity.PuntoVenta,
       EsFiscal = comprobanteEntity.EsFiscal ?? false,
        EsElectronica = comprobanteEntity.EsElectronica ?? false,
     EsManual = comprobanteEntity.EsManual ?? false,
    EsPresupuesto = comprobanteEntity.EsPresupuesto ?? false,
        Activo = comprobanteEntity.Activo,
     Testing = comprobanteEntity.Testing,
            MaxItems = comprobanteEntity.MaxItems ?? 0,
             ComprobanteRelacionado = comprobanteEntity.ComprobanteRelacionado ?? 0,
      EsMiPyME = comprobanteEntity.EsMiPyME,
    CbuEmisor = comprobanteEntity.CbuEmisor,
                AliasCbuEmisor = comprobanteEntity.AliasCbuEmisor,
    AnulaMiPyME = comprobanteEntity.AnulaMiPyME,
            Contabilizar = comprobanteEntity.Contabilizar,
              MueveStock = comprobanteEntity.MueveStock,
  IdModoMiPyme = comprobanteEntity.IdModoMiPyme,
            Prefijo = comprobanteEntity.IdTipoComprobanteNavigation?.NombreAbreviado ?? ""
         };
     return tmp;
       }
       }
     }
            catch (Exception ex)
       {
      Interaction.MsgBox($"Error en info_comprobante_porPtoYTipo: {ex.Message}");
            }

 return null;
        }

    // =============================================
        // CONSULTAR COMPROBANTE EN AFIP - ADAPTADO A EF CORE Y .NET 8.0
        // =============================================
        /// <summary>
 /// Consultar comprobante en AFIP - ADAPTADO A EF CORE Y .NET 8.0
        /// </summary>
        public static void Consultar_Comprobante(int pVenta, int tipo_comprobante, string nComprobante)
        {
   // Delegar a la implementación en factura_electronica
       factura_electronica.Consultar_Comprobante(pVenta, tipo_comprobante, nComprobante);
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
                Comprobante = c.Comprobante,
          IdTipoComprobante = c.IdTipoComprobante,
       NumeroComprobante = c.NumeroComprobante,
              PuntoVenta = c.PuntoVenta,
             EsFiscal = c.EsFiscal,
                EsElectronica = c.EsElectronica,
       EsManual = c.EsManual,
      EsPresupuesto = c.EsPresupuesto,
  Activo = c.Activo,
    Testing = c.Testing,
     MaxItems = c.MaxItems,
      ComprobanteRelacionado = c.ComprobanteRelacionado,
 EsMiPyME = c.EsMiPyME,
           CbuEmisor = c.CbuEmisor,
    AliasCbuEmisor = c.AliasCbuEmisor,
     AnulaMiPyME = c.AnulaMiPyME,
      Contabilizar = c.Contabilizar,
             MueveStock = c.MueveStock,
          IdModoMiPyme = c.IdModoMiPyme
      };

        context.ComprobanteEntity.Add(comprobanteEntity);
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
           var comprobanteEntity = context.ComprobanteEntity.FirstOrDefault(comp => comp.IdComprobante == c.IdComprobante);

           if (comprobanteEntity is null)
       return false;

       if (borra)
    {
        comprobanteEntity.Activo = false;
 }
          else
      {
         comprobanteEntity.Comprobante = c.Comprobante;
        comprobanteEntity.IdTipoComprobante = c.IdTipoComprobante;
      comprobanteEntity.NumeroComprobante = c.NumeroComprobante;
      comprobanteEntity.PuntoVenta = c.PuntoVenta;
          comprobanteEntity.EsFiscal = c.EsFiscal;
      comprobanteEntity.EsElectronica = c.EsElectronica;
    comprobanteEntity.EsManual = c.EsManual;
          comprobanteEntity.EsPresupuesto = c.EsPresupuesto;
    comprobanteEntity.Activo = c.Activo;
           comprobanteEntity.Testing = c.Testing;
          comprobanteEntity.MaxItems = c.MaxItems;
       comprobanteEntity.ComprobanteRelacionado = c.ComprobanteRelacionado;
        comprobanteEntity.EsMiPyME = c.EsMiPyME;
           comprobanteEntity.CbuEmisor = c.CbuEmisor;
            comprobanteEntity.AliasCbuEmisor = c.AliasCbuEmisor;
 comprobanteEntity.AnulaMiPyME = c.AnulaMiPyME;
        comprobanteEntity.Contabilizar = c.Contabilizar;
           comprobanteEntity.MueveStock = c.MueveStock;
      comprobanteEntity.IdModoMiPyme = c.IdModoMiPyme;
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
         var comprobantesValidos = new int[] { 1, 2, 3, 4, 5, 34, 39, 60, 63, 0, 99, 199 };
         
         var comprobantes = context.ComprobanteEntity
  .Include(c => c.IdTipoComprobanteNavigation)
       .Where(c => c.Activo == true && 
  (comprobantesValidos.Contains(c.IdTipoComprobante) || c.IdComprobante == id_comprobanteDefault))
        .ToList();

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
      var tipoComprobante = context.TipoComprobanteEntity
   .FirstOrDefault(tc => tc.IdTipoComprobante == Conversions.ToInteger(id_tipoComprobante));
   
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
