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
                    var usuarioEntity = context.Usuarios.FirstOrDefault(u => u.IdUsuario == id_usuario);

                    if (usuarioEntity is not null)
                    {
                        tmp.IdUsuario = usuarioEntity.IdUsuario;
                        tmp.usuario = usuarioEntity.usuario;
                        tmp.password = usuarioEntity.password;
                        tmp.nombre = usuarioEntity.nombre;
                        tmp.activo = usuarioEntity.activo;
                    }
                    else
                    {
                        tmp.usuario = "error";
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
                        usuarioEntity = context.Usuarios.FirstOrDefault(u => u.usuario == usuario);
                    }
                    else
                    {
                        usuarioEntity = context.Usuarios.FirstOrDefault(u => u.usuario.Contains(usuario));
                    }

                    if (usuarioEntity is not null)
                    {
                        tmp.IdUsuario = usuarioEntity.IdUsuario;
                        tmp.usuario = usuarioEntity.usuario;
                        tmp.password = usuarioEntity.password;
                        tmp.nombre = usuarioEntity.nombre;
                        tmp.activo = usuarioEntity.activo;
                    }
                    else
                    {
                        tmp.usuario = "error";
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
                        usuario = u.usuarioField,
                        password = u.password,
                        nombre = u.nombre,
                        activo = u.activo
                    };

                    context.Usuarios.Add(usuarioEntity);
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
                    var usuarioEntity = context.Usuarios.FirstOrDefault(ue => ue.IdUsuario == u.id_usuario);

                    if (usuarioEntity is not null)
                    {
                        if (borra == true)
                        {
                            usuarioEntity.activo = false;
                        }
                        else
                        {
                            usuarioEntity.usuario = u.usuarioField;
                            usuarioEntity.password = u.password;
                            usuarioEntity.nombre = u.nombre;
                            usuarioEntity.activo = u.activo;
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
                    var usuarioEntity = context.Usuarios.FirstOrDefault(ue => ue.IdUsuario == u.id_usuario);

                    if (usuarioEntity is not null)
                    {
                        context.Usuarios.Remove(usuarioEntity);
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
                        IdUsuario = up.id_usuario,
                        IdPerfil = up.id_perfil
                    };

                    context.UsuariosPerfiles.Add(usuarioPerfilEntity);
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
                    var perfilPermisoEntity = new PerfilPermisoEntity()
                    {
                        IdPermiso = pp.id_permiso,
                        IdPerfil = pp.id_perfil
                    };

                    context.PerfilesPermisos.Add(perfilPermisoEntity);
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
                    var usuarioEntity = context.Usuarios.FirstOrDefault(u => u.usuario == usuario && u.password == passwordEncriptado);

                    if (usuarioEntity is not null)
                    {
                        tmp.IdUsuario = usuarioEntity.IdUsuario;
                        tmp.usuario = usuarioEntity.usuario;
                        tmp.password = usuarioEntity.password;
                        tmp.nombre = usuarioEntity.nombre;
                        tmp.activo = usuarioEntity.activo;
                    }
                    else
                    {
                        tmp.usuario = "error";
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
                    return context.Usuarios.FirstOrDefault(u => u.NombreUsuario == nombreUsuario);
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
