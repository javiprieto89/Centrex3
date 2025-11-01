using System;
using System.Linq;

namespace Centrex.Funciones
{
    static class bancos
    {
        // ************************************ FUNCIONES DE BANCOS ***************************

        public static BancoEntity info_banco(int id_banco)
        {
            try
            {
                using (var context = new CentrexDbContext())
                {
                    var bancoEntity = context.BancoEntity.FirstOrDefault(b => b.IdBanco == id_banco);
                    if (bancoEntity is not null)
                    {
                        return bancoEntity;
                    }
                    else
                    {
                        return null;
                    }                   
                }
            }
            catch (Exception ex)
            {
                Interaction.MsgBox(ex.Message);                
                return null;
            }
        }

        public static bool AddBanco(BancoEntity b)
        {
            try
            {
                using (var context = new CentrexDbContext())
                {
                    var bancoEntity = new BancoEntity
                    {
                        Nombre = b.Nombre,
                        IdPais = b.IdPais,
                        NBanco = b.NBanco,
                        Activo = b.Activo
                    };

                    context.BancoEntity.Add(bancoEntity);
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

        public static bool UpdateBanco(BancoEntity b, bool borra = false)
        {
            try
            {
                using (var context = new CentrexDbContext())
                {
                    var bancoEntity = context.BancoEntity.FirstOrDefault(bn => bn.IdBanco == b.IdBanco);

                    if (bancoEntity is null)
                        return false;

                    if (borra)
                    {
                        bancoEntity.Activo = false;
                    }
                    else
                    {
                        bancoEntity.Nombre = b.Nombre;
                        bancoEntity.IdPais = b.IdPais;
                        bancoEntity.NBanco = b.NBanco;
                        bancoEntity.Activo = b.Activo;
                    }

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

        public static bool BorrarBanco(int idBanco)
        {
            try
            {
                using (var context = new CentrexDbContext())
                {
                    var bancoEntity = context.BancoEntity.FirstOrDefault(bn => bn.IdBanco == idBanco);
                    if (bancoEntity is null)
                        return false;

                    context.BancoEntity.Remove(bancoEntity);
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
    }
}
