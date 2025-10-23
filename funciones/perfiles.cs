using System;
using System.Linq;
using System.Xml.Linq;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Centrex.Models;

namespace Centrex
{

    static class perfiles
    {
        // ************************************ FUNCIONES DE PERFILES ********************
        public static perfil info_perfil(string id_perfil)
        {
            var tmp = new perfil();

            try
            {
                using (CentrexDbContext context = GetDbContext())
                {
                    var perfilEntity = context.Perfiles.FirstOrDefault(p => p.IdPerfil == Conversions.ToInteger(id_perfil));

                    if (perfilEntity is not null)
                    {
                        tmp.id_perfil = perfilEntity.IdPerfil.ToString();
                        tmp.nombre = perfilEntity.nombre;
                        tmp.activo = perfilEntity.activo;
                    }
                    else
                    {
                        tmp.nombre = "error";
                    }
                }
                return tmp;
            }
            catch (Exception ex)
            {
                Interaction.MsgBox(ex.Message.ToString());
                tmp.nombre = "error";
                return tmp;
            }
        }

        public static bool addperfil(perfil p)
        {
            try
            {
                using (CentrexDbContext context = GetDbContext())
                {
                    var perfilEntity = new PerfilEntity()
                    {
                        nombre = p.nombre,
                        activo = p.activo
                    };

                    context.Perfiles.Add(perfilEntity);
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

        public static bool updateperfil(perfil p, bool borra = false)
        {
            try
            {
                using (CentrexDbContext context = GetDbContext())
                {
                    var perfilEntity = context.Perfiles.FirstOrDefault(pf => pf.IdPerfil == p.id_perfil);

                    if (perfilEntity is not null)
                    {
                        if (borra == true)
                        {
                            perfilEntity.activo = false;
                        }
                        else
                        {
                            perfilEntity.nombre = p.nombre;
                            perfilEntity.activo = p.activo;
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

        public static bool borrarperfil(perfil p)
        {
            try
            {
                using (CentrexDbContext context = GetDbContext())
                {
                    var perfilEntity = context.Perfiles.FirstOrDefault(pf => pf.IdPerfil == p.id_perfil);

                    if (perfilEntity is not null)
                    {
                        context.Perfiles.Remove(perfilEntity);
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
        // ************************************ FUNCIONES DE PERFILES ********************
    }
}
