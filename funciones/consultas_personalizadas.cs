using System;
using System.Linq;
using System.Xml.Linq;
using Microsoft.VisualBasic;
using Centrex.Models;

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
                using (CentrexDbContext context = GetDbContext())
                {
                    var consultaEntity = context.ConsultasPersonalizadas.FirstOrDefault(c => c.IdConsulta == id_consulta);

                    if (consultaEntity is not null)
                    {
                        tmp.id_consulta = consultaEntity.IdConsulta.ToString();
                        tmp.nombre = consultaEntity.nombre;
                        tmp.consulta = consultaEntity.SqlTexto;
                        tmp.activo = consultaEntity.activo;
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
                using (CentrexDbContext context = GetDbContext())
                {
                    var consultaEntity = new ConsultaPersonalizadaEntity()
                    {
                        nombre = c.nombre,
                        SqlTexto = c.consulta,
                        activo = c.activo
                    };

                    context.ConsultasPersonalizadas.Add(consultaEntity);
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
                using (CentrexDbContext context = GetDbContext())
                {
                    var consultaEntity = context.ConsultasPersonalizadas.FirstOrDefault(ce => ce.IdConsulta == c.id_consulta);

                    if (consultaEntity is not null)
                    {
                        if (borra)
                        {
                            consultaEntity.activo = false;
                        }
                        else
                        {
                            consultaEntity.nombre = c.nombre;
                            consultaEntity.SqlTexto = c.consulta;
                            consultaEntity.activo = c.activo;
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
                using (CentrexDbContext context = GetDbContext())
                {
                    var consultaEntity = context.ConsultasPersonalizadas.FirstOrDefault(ce => ce.IdConsulta == c.id_consulta);

                    if (consultaEntity is not null)
                    {
                        context.ConsultasPersonalizadas.Remove(consultaEntity);
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
