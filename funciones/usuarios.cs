using System;
using System.Linq;
using System.Xml.Linq;
using Microsoft.VisualBasic;
using Centrex.Models;

namespace Centrex
{
    static class usuarios
    {

        // ************************************ FUNCIONES DE USUARIOS **********************
        public static UsuarioEntity info_usuario(int id_usuario)
        {
            var tmp = new UsuarioEntity();

            try
            {
                using (CentrexDbContext context = GetDbContext())
                {
                    var usuarioEntity = context.UsuarioEntity.FirstOrDefault(u => u.IdUsuario == id_usuario);

                    if (usuarioEntity is not null)
                    {
                        tmp.IdUsuario = usuarioEntity.IdUsuario;
                        tmp.Usuario = usuarioEntity.Usuario;
                        tmp.Password = usuarioEntity.Password;
                        tmp.Nombre = usuarioEntity.Nombre;
                        tmp.Activo = usuarioEntity.Activo;
                    }
                    else
                    {
                        tmp.Usuario = "error";
                    }
                }
            }
            catch (Exception ex)
            {
                tmp.usuario = "error";
            }

            return tmp;
        }

        public static UsuarioEntity info_usuario(string usuario, bool exacto)
        {
            var tmp = new UsuarioEntity();

            try
            {
                using (CentrexDbContext context = GetDbContext())
                {
                    UsuarioEntity usuarioEntity = null;

                    if (exacto)
                    {
                        usuarioEntity = context.UsuarioEntity.FirstOrDefault(u => u.Usuario == usuario);
                    }
                    else
                    {
                        usuarioEntity = context.UsuarioEntity.FirstOrDefault(u => u.Usuario.Contains(usuario));
                    }

                    if (usuarioEntity is not null)
                    {
                        tmp.IdUsuario = usuarioEntity.IdUsuario;
                        tmp.Usuario = usuarioEntity.Usuario;
                        tmp.Password = usuarioEntity.Password;
                        tmp.Nombre = usuarioEntity.Nombre;
                        tmp.Activo = usuarioEntity.Activo;
                    }
                    else
                    {
                        tmp.Usuario = "error";
                    }
                }
            }
            catch (Exception ex)
            {
                tmp.usuario = "error";
            }

            return tmp;
        }

        public static bool addUsuario(usuario u)
        {
            try
            {
                using (CentrexDbContext context = GetDbContext())
                {
                    var usuarioEntity = new UsuarioEntity()
                    {
                        Usuario = u.UsuarioField,
                        Password = u.Password,
                        Nombre = u.Nombre,
                        Activo = u.Activo
                    };

                    context.UsuarioEntity.Add(usuarioEntity);
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

        public static bool updateUsuario(usuario u, bool borra = false)
        {
            try
            {
                using (CentrexDbContext context = GetDbContext())
                {
                    var usuarioEntity = context.UsuarioEntity.FirstOrDefault(ue => ue.IdUsuario == u.IdUsuario);

                    if (usuarioEntity is not null)
                    {
                        if (borra == true)
                        {
                            usuarioEntity.Activo = false;
                        }
                        else
                        {
                            usuarioEntity.Usuario = u.UsuarioField;
                            usuarioEntity.Password = u.Password;
                            usuarioEntity.Nombre = u.Nombre;
                            usuarioEntity.Activo = u.Activo;
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

        public static bool borrarUsuario(usuario u)
        {
            try
            {
                using (CentrexDbContext context = GetDbContext())
                {
                    var usuarioEntity = context.UsuarioEntity.FirstOrDefault(ue => ue.IdUsuario == u.IdUsuario);

                    if (usuarioEntity is not null)
                    {
                        context.UsuarioEntity.Remove(usuarioEntity);
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

        public static bool add_usuario_perfil(usuario_perfil up)
        {
            try
            {
                using (CentrexDbContext context = GetDbContext())
                {
                    var usuarioPerfilEntity = new UsuarioPerfilEntity()
                    {
                        IdUsuario = up.IdUsuario,
                        IdPerfil = up.IdPerfil
                    };

                    context.UsuarioPerfilEntity.Add(usuarioPerfilEntity);
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

        public static bool add_permiso_perfil(perfil_permiso pp)
        {
            try
            {
                using (CentrexDbContext context = GetDbContext())
                {
                    var perfilPermisoEntity = new PermisoPerfilEntity()
                    {
                        IdPermiso = pp.IdPermiso,
                        IdPefil = pp.IdPerfil
                    };

                    context.PermisoPerfilEntity.Add(perfilPermisoEntity);
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

        public static UsuarioEntity info_login(string usuario, string password)
        {
            var tmp = new UsuarioEntity();
            var e = new EncriptarType();

            try
            {
                using (CentrexDbContext context = GetDbContext())
                {
                    string passwordEncriptado = e.Encriptar(password);
                    var usuarioEntity = context.UsuarioEntity.FirstOrDefault(u => u.Usuario == usuario && u.Password == passwordEncriptado);

                    if (usuarioEntity is not null)
                    {
                        tmp.IdUsuario = usuarioEntity.IdUsuario;
                        tmp.Usuario = usuarioEntity.Usuario;
                        tmp.Password = usuarioEntity.Password;
                        tmp.Nombre = usuarioEntity.Nombre;
                        tmp.Activo = usuarioEntity.Activo;
                    }
                    else
                    {
                        tmp.Usuario = "error";
                    }
                }
            }
            catch (Exception ex)
            {
                tmp.usuario = "error";
            }

            return tmp;
        }

        /// <summary>
    /// Obtiene información de un usuario por nombre de usuario y estado activo
    /// </summary>
        /// <summary>
    /// Obtiene información de un usuario por nombre de usuario
    /// </summary>
        public static UsuarioEntity info_usuario(string nombreUsuario)
        {
            try
            {
                using (var context = new CentrexDbContext())
                {
                    return context.UsuarioEntity.FirstOrDefault(u => u.Nombre == nombreUsuario);
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        // ************************************ FUNCIONES DE USUARIOS **********************
    }
}
