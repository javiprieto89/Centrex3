using System;
using System.Linq;
using System.Windows.Forms;

namespace Centrex.Funciones
{

    static class condiciones_compra
    {

        // ************************************ FUNCIONES DE CONDICIONES DE COMPRA **********************
        public static CondicionCompraEntity info_condicion_compra(int id_condicion_compra)
        {
            try
            {
                using (CentrexDbContext context = new CentrexDbContext())
                {

                    var condicionCompraEntity = context.CondicionCompraEntity.FirstOrDefault(c => c.IdCondicionCompra == id_condicion_compra);                    

                    if (condicionCompraEntity is not null)
                    {
                        return condicionCompraEntity;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener condición de compra: " + ex.Message, "Centrex", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        public static bool addCondicion_compra(CondicionCompraEntity Condicion)
        {
            try
            {
                using (CentrexDbContext context = new CentrexDbContext())
                {
                    var condicionEntity = new CondicionCompraEntity()
                    {
                        Condicion = Condicion.Condicion,
                        Vencimiento = Condicion.Vencimiento,
                        Recargo = (decimal)Condicion.Recargo,
                        Activo = Condicion.Activo
                    };

                    context.CondicionCompraEntity.Add(condicionEntity);
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

        public static bool updateCondicion_compra(CondicionCompraEntity Condicion, bool borra = false)
        {
            try
            {
                using (CentrexDbContext context = new CentrexDbContext())
                {
                    var condicionEntity = context.CondicionCompraEntity.FirstOrDefault(c => c.IdCondicionCompra == Condicion.IdCondicionCompra);

                    if (condicionEntity is not null)
                    {
                        if (borra == true)
                        {
                            condicionEntity.Activo = false;
                        }
                        else
                        {
                            condicionEntity.Condicion = Condicion.Condicion;
                            condicionEntity.Vencimiento = Condicion.Vencimiento;
                            condicionEntity.Recargo = (decimal)Condicion.Recargo;
                            condicionEntity.Activo = Condicion.Activo;
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

        public static bool borrarCondicion_compra(CondicionCompraEntity Condicion)
        {
            try
            {
                using (CentrexDbContext context = new CentrexDbContext())
                {
                    var condicionEntity = context.CondicionCompraEntity.FirstOrDefault(c => c.IdCondicionCompra == Condicion.IdCondicionCompra);

                    if (condicionEntity is not null)
                    {
                        context.CondicionCompraEntity.Remove(condicionEntity);
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
        // ************************************ FUNCIONES DE CONDICIONES COMPRA **********************
    }
}

