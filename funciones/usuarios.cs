using System;
using System.Linq;
using System.Windows.Forms;

namespace Centrex.Funciones
{
    public static class usuarios
    {

        // ************************************ FUNCIONES DE USUARIOS **********************
        public static UsuarioEntity info_usuario(int id_usuario)
        {
            
            try
            {
                using (CentrexDbContext context = new CentrexDbContext())
                {
                    var usuarioEntity = context.UsuarioEntity.FirstOrDefault(u => u.IdUsuario == id_usuario);

                    if (usuarioEntity is not null)
                    {
                        return usuarioEntity;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener el usuario:" + ex.Message, "Centrex", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        public static UsuarioEntity info_usuario(string Usuario, bool exacto)
        {
                        try
            {
                using (CentrexDbContext context = new CentrexDbContext())
                {
                    UsuarioEntity usuarioEntity = null;

                    if (exacto)
                    {
                        usuarioEntity = context.UsuarioEntity.FirstOrDefault(u => u.Usuario == Usuario);
                    }
                    else
                    {
                        usuarioEntity = context.UsuarioEntity.FirstOrDefault(u => u.Usuario.Contains(Usuario));
                    }

                    if (usuarioEntity is not null)
                    {
                        return usuarioEntity;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener el usuario:" + ex.Message, "Centrex", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

        }

        public static bool addUsuario(UsuarioEntity u)
        {
            try
            {
                using (CentrexDbContext context = new CentrexDbContext())
                {
                    var usuarioEntity = new UsuarioEntity()
                    {
                        Usuario = u.Usuario,
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

        public static bool updateUsuario(UsuarioEntity u, bool borra = false)
        {
            try
            {
                using (CentrexDbContext context = new CentrexDbContext())
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
                            usuarioEntity.Usuario = u.Usuario;
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

        public static bool borrarUsuario(UsuarioEntity u)
        {
            try
            {
                using (CentrexDbContext context = new CentrexDbContext())
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

        public static bool add_usuario_perfil(UsuarioPerfilEntity up)
        {
            try
            {
                using (CentrexDbContext context = new CentrexDbContext())
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

        public static bool add_permiso_perfil(PermisoPerfilEntity pp)
        {
            try
            {
                using (CentrexDbContext context = new CentrexDbContext())
                {
                    var perfilPermisoEntity = new PermisoPerfilEntity()
                    {
                        IdPermiso = pp.IdPermiso,
                        IdPefil = pp.IdPefil
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

        public static UsuarioEntity info_login(string Usuario, string password)
        {
            var tmp = new UsuarioEntity();
            var e = new EncriptarType();

            try
            {
                using (CentrexDbContext context = new CentrexDbContext())
                {
                    string passwordEncriptado = e.Encriptar(password);
                    var usuarioEntity = context.UsuarioEntity.FirstOrDefault(u => u.Usuario == Usuario && u.Password == passwordEncriptado);

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
                tmp.Usuario = "error";
                Interaction.MsgBox("Error en login de usuario: " + ex.Message, MsgBoxStyle.Critical, "Centrex");
            }

            return tmp;
        }

        /// <summary>
        /// Obtiene información de un Usuario por nombre de Usuario y estado activo
        /// </summary>
        /// <summary>
        /// Obtiene información de un Usuario por nombre de Usuario
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
                Interaction.MsgBox("Error al obtener usuario: " + ex.Message, MsgBoxStyle.Critical, "Centrex");
                return null;
            }
        }
        // ************************************ FUNCIONES DE USUARIOS **********************
    }
}

