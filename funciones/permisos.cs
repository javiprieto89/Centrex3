using System;
using System.Linq;
using System.Xml.Linq;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Centrex.Models;

namespace Centrex
{

    static class permisos
    {
        // ************************************ FUNCIONES DE PERMISOS ********************
        public static permiso info_permiso(string id_permiso)
        {
            var tmp = new permiso();

            try
            {
                using (CentrexDbContext context = GetDbContext())
                {
                    var permisoEntity = context.Permisos.FirstOrDefault(p => p.IdPermiso == Conversions.ToInteger(id_permiso));

                    if (permisoEntity is not null)
                    {
                        tmp.id_permiso = permisoEntity.IdPermiso.ToString();
                        tmp.nombre = permisoEntity.nombre;
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

        public static bool addpermiso(permiso p)
        {
            try
            {
                using (CentrexDbContext context = GetDbContext())
                {
                    var permisoEntity = new PermisoEntity() { nombre = p.nombre };

                    context.Permisos.Add(permisoEntity);
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

        public static bool updatepermiso(permiso p, bool borra = false)
        {
            try
            {
                using (CentrexDbContext context = GetDbContext())
                {
                    var permisoEntity = context.Permisos.FirstOrDefault(pm => pm.IdPermiso == p.id_permiso);

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
                            permisoEntity.nombre = p.nombre;
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

        public static bool borrarpermiso(permiso p)
        {
            try
            {
                using (CentrexDbContext context = GetDbContext())
                {
                    var permisoEntity = context.Permisos.FirstOrDefault(pm => pm.IdPermiso == p.id_permiso);

                    if (permisoEntity is not null)
                    {
                        context.Permisos.Remove(permisoEntity);
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
