using System;
using System.Linq;
using Microsoft.VisualBasic;
using Centrex.Models;

namespace Centrex.Funciones
{
    static class consultas
    {
        // ************************************
        // FUNCIONES DE CONSULTAS PERSONALIZADAS
        // ************************************

        public static ConsultaPersonalizadaEntity info_consulta(int id_consulta)
        {
            var tmp = new ConsultaPersonalizadaEntity();
            try
            {
                using (var context = new CentrexDbContext())
                {
                    var consultaEntity = context.ConsultaPersonalizadaEntity.FirstOrDefault(c => c.IdConsulta == id_consulta);

                    if (consultaEntity is not null)
                    {
                        tmp.IdConsulta = consultaEntity.IdConsulta;
                        tmp.Nombre = consultaEntity.Nombre;
                        tmp.Consulta = consultaEntity.Consulta;
                        tmp.Activo = consultaEntity.Activo;
                    }
                    else
                    {
                        tmp.Nombre = "error";
                    }
                }
            }
            catch (Exception ex)
            {
                Interaction.MsgBox("Error en info_consulta: " + ex.Message);
                tmp.Nombre = "error";
            }
            return tmp;
        }

        public static bool addConsulta(ConsultaPersonalizadaEntity c)
        {
            try
            {
                using (var context = new CentrexDbContext())
                {
                    var consultaEntity = new ConsultaPersonalizadaEntity()
                    {
                        Nombre = c.Nombre,
                        Consulta = c.Consulta,
                        Activo = c.Activo
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

        public static bool updateConsulta(ConsultaPersonalizadaEntity c, bool borra = false)
        {
            try
            {
                using (var context = new CentrexDbContext())
                {
                    var consultaEntity = context.ConsultaPersonalizadaEntity.FirstOrDefault(ce => ce.IdConsulta == c.IdConsulta);

                    if (consultaEntity is not null)
                    {
                        if (borra)
                        {
                            consultaEntity.Activo = false;
                        }
                        else
                        {
                            consultaEntity.Nombre = c.Nombre;
                            consultaEntity.Consulta = c.Consulta;
                            consultaEntity.Activo = c.Activo;
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

        public static bool borrarConsulta(ConsultaPersonalizadaEntity c)
        {
            try
            {
                using (var context = new CentrexDbContext())
                {
                    var consultaEntity = context.ConsultaPersonalizadaEntity.FirstOrDefault(ce => ce.IdConsulta == c.IdConsulta);

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
