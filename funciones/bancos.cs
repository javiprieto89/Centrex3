using System;
using System.Linq;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Centrex.Models;

namespace Centrex
{
    static class bancos
    {
        // ************************************ FUNCIONES DE BANCOS ***************************

        public static BancoEntity InfoBanco(string id_banco)
        {
            try
            {
                using (var context = new CentrexDbContext())
                {
                    if (string.IsNullOrEmpty(id_banco))
                        return context.BancoEntity.FirstOrDefault();

                    int id = Conversions.ToInteger(id_banco);
                    return context.BancoEntity.FirstOrDefault(b => b.IdBanco == id);
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
