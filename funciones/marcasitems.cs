using System;
using System.Linq;
using System.Xml.Linq;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Centrex.Models;

namespace Centrex
{

    static class marcasitems
    {

        // ************************************ FUNCIONES DE MARCAS DE ITEMS **********************
        public static marca_item info_marcai(object id_marca)
        {
            var tmp = new marca_item();
            try
            {
                using (CentrexDbContext context = GetDbContext())
                {
                    var marcaEntity = context.Marcas.FirstOrDefault(m => m.IdMarca == Conversions.ToInteger(id_marca));

                    if (marcaEntity is not null)
                    {
                        tmp.id_marca = marcaEntity.IdMarca.ToString();
                        tmp.marca = marcaEntity.marca;
                        tmp.activo = marcaEntity.activo;
                    }
                    else
                    {
                        tmp.marca = "error";
                    }
                }
                return tmp;
            }
            catch (Exception ex)
            {
                Interaction.MsgBox(ex.Message.ToString());
                tmp.marca = "error";
                return tmp;
            }
        }

        public static bool addmarcai(MarcaEntity marcai)
        {
            try
            {
                using (CentrexDbContext context = GetDbContext())
                {
                    var marcaEntity = new MarcaEntity()
                    {
                        Marca = marcai.Marca,
                        activo = marcai.Activo
                    };

                    context.Marcas.Add(marcaEntity);
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
                using (CentrexDbContext context = GetDbContext())
                {
                    var marcaEntity = context.Marcas.FirstOrDefault(m => m.IdMarca == (int)marcai.id_marca);

                    if (marcaEntity is not null)
                    {
                        if (borra == true)
                        {
                            marcaEntity.activo = false;
                        }
                        else
                        {
                            marcaEntity.marca = marcai.Marca;
                            marcaEntity.activo = marcai.Activo;
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
                using (CentrexDbContext context = GetDbContext())
                {
                    var marcaEntity = context.Marcas.FirstOrDefault(m => m.IdMarca == (int)marcai.IdMarca);

                    if (marcaEntity is not null)
                    {
                        context.Marcas.Remove(marcaEntity);
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
