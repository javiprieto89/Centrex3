using System;
using System.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace Centrex
{
    static class clientes
    {
        // ************************************ FUNCIONES DE CLIENTES ***************************
        public static cliente info_cliente(string id_cliente)
  {
var tmp = new cliente();

      try
          {
    using (CentrexDbContext context = new CentrexDbContext())
                {
  var clienteEntity = context.ClienteEntity.Include(c => c.IdProvinciaFiscalNavigation).Include(c => c.IdProvinciaEntregaNavigation).FirstOrDefault(c => c.IdCliente == Conversions.ToInteger(id_cliente));

     if (clienteEntity is not null)
{
   tmp.IdCliente = clienteEntity.IdCliente;
   tmp.RazonSocial = clienteEntity.RazonSocial;
  tmp.NombreFantasia = clienteEntity.NombreFantasia;
   tmp.TaxNumber = clienteEntity.TaxNumber;
   tmp.Contacto = clienteEntity.Contacto;
        tmp.Telefono = clienteEntity.Telefono;
      tmp.Celular = clienteEntity.Celular;
       tmp.Email = clienteEntity.Email;
  tmp.IdProvinciaFiscal = clienteEntity.IdProvinciaFiscal;
     tmp.DireccionFiscal = clienteEntity.DireccionFiscal;
         tmp.LocalidadFiscal = clienteEntity.LocalidadFiscal;
           tmp.CpFiscal = clienteEntity.CpFiscal;
         tmp.IdProvinciaEntrega = clienteEntity.IdProvinciaEntrega;
            tmp.DireccionEntrega = clienteEntity.DireccionEntrega;
     tmp.LocalidadEntrega = clienteEntity.LocalidadEntrega;
 tmp.CpEntrega = clienteEntity.CpEntrega;
             tmp.Notas = clienteEntity.Notas;
      tmp.EsInscripto = clienteEntity.EsInscripto;
       tmp.Activo = clienteEntity.Activo;
    tmp.IdTipoDocumento = clienteEntity.IdTipoDocumento;
      tmp.IdClaseFiscal = clienteEntity.IdClaseFiscal;
      tmp.IdPaisFiscal = clienteEntity.IdPaisFiscal;
        tmp.IdPaisEntrega = clienteEntity.IdPaisEntrega;
       }
      else
        {
       tmp.RazonSocial = "error";
       }
            }
         }
         catch (Exception ex)
  {
        Interaction.MsgBox(ex.Message.ToString());
         tmp.RazonSocial = "error";
            }

  return tmp;
}

        public static bool addcliente(cliente cl)
        {
 try
  {
        using (CentrexDbContext context = new CentrexDbContext())
    {
          var clienteEntity = new ClienteEntity();

         clienteEntity.RazonSocial = cl.RazonSocial;
          clienteEntity.NombreFantasia = cl.NombreFantasia;
          clienteEntity.TaxNumber = cl.TaxNumber;
    clienteEntity.Contacto = cl.Contacto;
            clienteEntity.Telefono = cl.Telefono;
         clienteEntity.Celular = cl.Celular;
     clienteEntity.Email = cl.Email;
   clienteEntity.IdProvinciaFiscal = cl.IdProvinciaFiscal;
    clienteEntity.DireccionFiscal = cl.DireccionFiscal;
         clienteEntity.LocalidadFiscal = cl.LocalidadFiscal;
  clienteEntity.CpFiscal = cl.CpFiscal;
               clienteEntity.IdProvinciaEntrega = cl.IdProvinciaEntrega;
      clienteEntity.DireccionEntrega = cl.DireccionEntrega;
         clienteEntity.LocalidadEntrega = cl.LocalidadEntrega;
       clienteEntity.CpEntrega = cl.CpEntrega;
           clienteEntity.Notas = cl.Notas;
      clienteEntity.EsInscripto = cl.EsInscripto;
  clienteEntity.Activo = cl.Activo;
          clienteEntity.IdTipoDocumento = cl.IdTipoDocumento;
     clienteEntity.IdClaseFiscal = cl.IdClaseFiscal;
          clienteEntity.IdPaisFiscal = cl.IdPaisFiscal;
         clienteEntity.IdPaisEntrega = cl.IdPaisEntrega;

   context.ClienteEntity.Add(clienteEntity);
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

     public static bool updatecliente(cliente cl, bool borra = false)
        {
   try
   {
    using (CentrexDbContext context = new CentrexDbContext())
          {
        var clienteEntity = context.ClienteEntity.FirstOrDefault(c => c.IdCliente == cl.IdCliente);

         if (clienteEntity is not null)
       {
    if (borra == true)
  {
 clienteEntity.Activo = false;
     }
      else
         {
           clienteEntity.RazonSocial = cl.RazonSocial;
             clienteEntity.NombreFantasia = cl.NombreFantasia;
     clienteEntity.TaxNumber = cl.TaxNumber;
          clienteEntity.Contacto = cl.Contacto;
   clienteEntity.Telefono = cl.Telefono;
          clienteEntity.Celular = cl.Celular;
 clienteEntity.Email = cl.Email;
              clienteEntity.IdProvinciaFiscal = cl.IdProvinciaFiscal;
       clienteEntity.DireccionFiscal = cl.DireccionFiscal;
  clienteEntity.LocalidadFiscal = cl.LocalidadFiscal;
           clienteEntity.CpFiscal = cl.CpFiscal;
          clienteEntity.IdProvinciaEntrega = cl.IdProvinciaEntrega;
    clienteEntity.DireccionEntrega = cl.DireccionEntrega;
               clienteEntity.LocalidadEntrega = cl.LocalidadEntrega;
        clienteEntity.CpEntrega = cl.CpEntrega;
       clienteEntity.Notas = cl.Notas;
      clienteEntity.EsInscripto = cl.EsInscripto;
    clienteEntity.Activo = cl.Activo;
             clienteEntity.IdTipoDocumento = cl.IdTipoDocumento;
clienteEntity.IdClaseFiscal = cl.IdClaseFiscal;
     clienteEntity.IdPaisFiscal = cl.IdPaisFiscal;
    clienteEntity.IdPaisEntrega = cl.IdPaisEntrega;
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

        public static bool borrarcliente(cliente cl)
  {
 try
            {
            using (CentrexDbContext context = new CentrexDbContext())
       {
  var clienteEntity = context.ClienteEntity.FirstOrDefault(c => c.IdCliente == cl.IdCliente);

             if (clienteEntity is not null)
        {
 context.ClienteEntity.Remove(clienteEntity);
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

   public static bool estaClienteDefault(int id_clientePedidoDefault)
        {
      try
            {
          using (CentrexDbContext context = new CentrexDbContext())
           {
    var clienteEntity = context.ClienteEntity.Where(c => c.Activo == true && c.IdCliente == id_clientePedidoDefault).OrderBy(c => c.RazonSocial).FirstOrDefault();

       return clienteEntity is not null;
              }
            }
          catch (Exception ex)
        {
             Interaction.MsgBox(ex.Message.ToString());
                return false;
   }
        }

        public static int existecliente(string taxNumber)
        {
   try
            {
    using (CentrexDbContext context = new CentrexDbContext())
   {
 var clienteEntity = context.ClienteEntity.FirstOrDefault(c => c.TaxNumber == Strings.Trim(taxNumber.ToString()));

         if (clienteEntity is not null)
   {
        return clienteEntity.IdCliente;
     }
          else
      {
  return -1;
      }
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
