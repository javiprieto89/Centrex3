using System;
using System.Linq;
using System.Xml.Linq;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Centrex.Models;

namespace Centrex.Funciones
{

    static class perfiles
    {
        // ************************************ FUNCIONES DE PERFILES ********************
        public static PerfilEntity info_perfil(int id_perfil)
        {
            try
            {
                using (CentrexDbContext context = new CentrexDbContext())
                {
                    if (id_perfil != 0 || id_perfil != -1)
                    {
                        return context.PerfilEntity.FirstOrDefault(c => c.IdPerfil == id_perfil);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                Interaction.MsgBox($"Error al obtener el perfil: {ex.Message}", MsgBoxStyle.Critical, "Centrex");
                return null;
            }
        }

        public static bool addperfil(PerfilEntity p)
        {
            try
            {
                using (CentrexDbContext context = new CentrexDbContext())
                {
                    var perfilEntity = new PerfilEntity()
                    {
                        Nombre = p.Nombre,
                        Activo = p.Activo
                    };

                    context.PerfilEntity.Add(perfilEntity);
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

        public static bool updateperfil(PerfilEntity p, bool borra = false)
        {
            try
            {
                using (CentrexDbContext context = new CentrexDbContext())
                {
                    var perfilEntity = context.PerfilEntity.FirstOrDefault(pf => pf.IdPerfil == p.IdPerfil);

                    if (perfilEntity is not null)
                    {
                        if (borra == true)
                        {
                            perfilEntity.Activo = false;
                        }
                        else
                        {
                            perfilEntity.Nombre = p.Nombre;
                            perfilEntity.Activo = p.Activo;
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

        public static bool borrarperfil(int id_perfil)
        {
            try
            {
                using (CentrexDbContext context = new CentrexDbContext())
                {
                    var perfilEntity = context.PerfilEntity.FirstOrDefault(pf => pf.IdPerfil == id_perfil);

                    if (perfilEntity is not null)
                    {
                        context.PerfilEntity.Remove(perfilEntity);
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
