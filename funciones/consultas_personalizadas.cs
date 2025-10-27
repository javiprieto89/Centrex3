using System;
using System.Linq;
using Microsoft.VisualBasic;

namespace Centrex
{
    static class consultas
    {
        // ************************************
        // FUNCIONES DE CONSULTAS PERSONALIZADAS
        // ************************************

        public static consultaP info_consulta(int id_consulta)
 {
       var tmp = new consultaP();
            try
            {
  using (CentrexDbContext context = new CentrexDbContext())
    {
        var consultaEntity = context.ConsultaPersonalizadaEntity.FirstOrDefault(c => c.IdConsulta == id_consulta);

           if (consultaEntity is not null)
  {
              tmp.id_consulta = consultaEntity.IdConsulta.ToString();
   tmp.nombre = consultaEntity.Nombre;
     tmp.consulta = consultaEntity.Consulta;
           tmp.activo = consultaEntity.Activo;
              }
   else
 {
           tmp.nombre = "error";
           }
       }
        }
            catch (Exception ex)
 {
                Interaction.MsgBox("Error en info_consulta: " + ex.Message);
    tmp.nombre = "error";
            }
 return tmp;
        }

        public static bool addConsulta(consultaP c)
        {
      try
            {
                using (CentrexDbContext context = new CentrexDbContext())
            {
      var consultaEntity = new ConsultaPersonalizadaEntity()
{
    Nombre = c.nombre,
           Consulta = c.consulta,
      Activo = c.activo
             };

    context.ConsultaPersonalizadaEntity.Add(consultaEntity);
         context.SaveChanges();
    return true;
        }
   }
      catch (Exception ex)
            {
Interaction.MsgBox("Error en addConsulta: " + ex.Message);
  return false;
         }
        }

        public static bool updateConsulta(consultaP c, bool borra = false)
        {
      try
    {
       using (CentrexDbContext context = new CentrexDbContext())
         {
               var consultaEntity = context.ConsultaPersonalizadaEntity.FirstOrDefault(ce => ce.IdConsulta == Conversions.ToInteger(c.id_consulta));

         if (consultaEntity is not null)
           {
        if (borra)
     {
      consultaEntity.Activo = false;
     }
 else
                 {
       consultaEntity.Nombre = c.nombre;
        consultaEntity.Consulta = c.consulta;
             consultaEntity.Activo = c.activo;
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
   Interaction.MsgBox("Error en updateConsulta: " + ex.Message);
        return false;
     }
        }

        public static bool borrarConsulta(consultaP c)
    {
            try
     {
      using (CentrexDbContext context = new CentrexDbContext())
           {
          var consultaEntity = context.ConsultaPersonalizadaEntity.FirstOrDefault(ce => ce.IdConsulta == Conversions.ToInteger(c.id_consulta));

         if (consultaEntity is not null)
           {
    context.ConsultaPersonalizadaEntity.Remove(consultaEntity);
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
    Interaction.MsgBox("Error en borrarConsulta: " + ex.Message);
         return false;
            }
        }
    }
}
