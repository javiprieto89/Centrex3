using System;
using System.Linq;
using System.Xml.Linq;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Centrex.Models;

namespace Centrex.Funciones
{

    static class permisos
    {
        // ************************************ FUNCIONES DE PERMISOS ********************
        public static PermisoEntity info_permiso(int id_permiso)
        {
            try
            {
                using (CentrexDbContext context = new CentrexDbContext())
                {
                    if (id_permiso != 0 || id_permiso != -1)
                    {
                        return context.PermisoEntity.FirstOrDefault(c => c.IdPermiso == id_permiso);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                Interaction.MsgBox($"Error al obtener el permiso: {ex.Message}", MsgBoxStyle.Critical, "Centrex");
                return null;
            }
        }

        public static bool addpermiso(PermisoEntity p)
        {
            try
            {
                using (CentrexDbContext context = new CentrexDbContext())
                {
                   
                    context.PermisoEntity.Add(p);
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

        public static bool updatepermiso(PermisoEntity p, bool borra = false)
        {
            try
            {
                using (CentrexDbContext context = new CentrexDbContext())
                {
                    var permisoEntity = context.PermisoEntity.FirstOrDefault(pm => pm.IdPermiso == p.IdPermiso);

                    if (permisoEntity is not null)
                    {
                        if (borra == true)
                        {
                            // Si tiene campo activo en la entity
                            var activoProp = permisoEntity.GetType().GetProperty("Activo");
                            if (activoProp is not null)
                            {
                                activoProp.SetValue(permisoEntity, false);
                            }
                        }
                        else
                        {
                            permisoEntity.Nombre = p.Nombre;                            
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

        public static bool borrarpermiso(PermisoEntity p)
        {
            try
            {
                using (CentrexDbContext context = new CentrexDbContext())
                {
                    var permisoEntity = context.PermisoEntity.FirstOrDefault(pm => pm.IdPermiso == p.IdPermiso);

                    if (permisoEntity is not null)
                    {
                        context.PermisoEntity.Remove(permisoEntity);
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
        // ************************************ FUNCIONES DE PERMISOS ********************
    }
}
