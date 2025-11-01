using System;
using System.Linq;

namespace Centrex.Funciones
{

    static class marcasitems
    {

        // ************************************ FUNCIONES DE MARCAS DE ITEMS **********************
        public static MarcaItemEntity info_marcai(object IdMarca)
        {
            var tmp = new MarcaItemEntity();
            try
            {
                using (CentrexDbContext context = new CentrexDbContext())
                {
                    var marcaEntity = context.MarcaItemEntity.FirstOrDefault(m => m.IdMarca == Conversions.ToInteger(IdMarca));

                    if (marcaEntity is not null)
                    {
                        tmp.IdMarca = marcaEntity.IdMarca;
                        tmp.Marca = marcaEntity.Marca;
                        tmp.Activo = marcaEntity.Activo;
                    }
                    else
                    {
                        tmp.Marca = "error";
                    }
                }
                return tmp;
            }
            catch (Exception ex)
            {
                Interaction.MsgBox(ex.Message.ToString());
                tmp.Marca = "error";
                return tmp;
            }
        }

        public static bool addmarcai(MarcaEntity marcai)
        {
            try
            {
                using (CentrexDbContext context = new CentrexDbContext())
                {
                    var marcaEntity = new MarcaEntity()
                    {
                        Marca = marcai.Marca,
                        Activo = marcai.Activo
                    };

                    context.MarcaItemEntity.Add(marcaEntity);
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

        public static bool updatemarcai(MarcaEntity marcai, bool borra = false)
        {
            try
            {
                using (CentrexDbContext context = new CentrexDbContext())
                {
                    var marcaEntity = context.MarcaItemEntity.FirstOrDefault(m => m.IdMarca == (int)marcai.IdMarca);

                    if (marcaEntity is not null)
                    {
                        if (borra == true)
                        {
                            marcaEntity.Activo = false;
                        }
                        else
                        {
                            marcaEntity.Marca = marcai.Marca;
                            marcaEntity.Activo = marcai.Activo;
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

        public static bool borrarmarcai(MarcaEntity marcai)
        {
            try
            {
                using (CentrexDbContext context = new CentrexDbContext())
                {
                    var marcaEntity = context.MarcaItemEntity.FirstOrDefault(m => m.IdMarca == (int)marcai.IdMarca);

                    if (marcaEntity is not null)
                    {
                        context.MarcaItemEntity.Remove(marcaEntity);
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

        // ************************************ FUNCIONES DE MARCAS DE ITEMS **********************
    }
}
