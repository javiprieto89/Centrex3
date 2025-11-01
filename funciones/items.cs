using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Centrex.Funciones
{

    static class mitem
    {
        // ************************************ FUNCIONES DE ITEMS ***************************
        public static ItemEntity info_item(int id_item)
        {
            try
            {
                using (var context = new CentrexDbContext())
                {
                    return context.ItemEntity
    .Include(i => i.IdMarcaNavigation)
.Include(i => i.IdTipoNavigation)
        .Include(i => i.IdProveedorNavigation)
  .FirstOrDefault(i => i.IdItem == id_item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        public static ItemEntity info_itemDesc(string descript)
        {
            try
            {
                using (var context = new CentrexDbContext())
                {
                    return context.ItemEntity
   .Include(i => i.IdMarcaNavigation)
     .Include(i => i.IdTipoNavigation)
         .Include(i => i.IdProveedorNavigation)
   .FirstOrDefault(i => i.Descript == descript);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        public static bool info_itemtmp(string _idItem, int _idUsuario, Guid _idUnico)
        {
            try
            {
                using (var context = new CentrexDbContext())
                {
                    int idItemInt = int.Parse(_idItem);

                    bool exists = context.TmpPedidoItemEntity.Any(t => t.IdItem == idItemInt && t.IdUsuario == _idUsuario && t.IdUnico == _idUnico);

                    return exists;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static bool additem(ItemEntity it)
        {
            try
            {
                using (var context = new CentrexDbContext())
                {
                    context.ItemEntity.Add(it);
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static bool addItemIVA(ItemImpuestoEntity ii)
        {
            try
            {
                using (var context = new CentrexDbContext())
                {
                    context.ItemImpuestoEntity.Add(ii);
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static ItemEntity infoItem_lastItem()
        {
            try
            {
                using (var context = new CentrexDbContext())
                {
                    return context.ItemEntity
.Include(i => i.IdMarcaNavigation)
      .Include(i => i.IdTipoNavigation)
      .Include(i => i.IdProveedorNavigation)
         .OrderByDescending(i => i.IdItem)
   .FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        public static bool updateitem(ItemEntity it, bool borra = false)
        {
            try
            {
                using (var context = new CentrexDbContext())
                {
                    var itemEntity = context.ItemEntity.FirstOrDefault(i => i.IdItem == it.IdItem);

                    if (itemEntity is not null)
                    {
                        if (borra == true)
                        {
                            // Solo actualizar el campo activo
                            itemEntity.Activo = false;
                        }
                        else
                        {
                            // Actualizar todos los campos
                            context.Entry(itemEntity).CurrentValues.SetValues(it);
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
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static bool borraritem(ItemEntity it)
        {
            try
            {
                using (var context = new CentrexDbContext())
                {
                    var itemEntity = context.ItemEntity.FirstOrDefault(i => i.IdItem == it.IdItem);

                    if (itemEntity is not null)
                    {
                        context.ItemEntity.Remove(itemEntity);
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
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static int existeitem(string i)
        {
            try
            {
                using (var context = new CentrexDbContext())
                {
                    var itemEntity = context.ItemEntity.FirstOrDefault(item => item.Item.Contains(i));

                    if (itemEntity is not null)
                    {
                        return itemEntity.IdItem;
                    }
                    else
                    {
                        return -1;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }
        }

        public static bool updatePrecios_items(string id_item, string precio)
        {
            try
            {
                using (CentrexDbContext context = new CentrexDbContext())
                {
                    int idItemInt = int.Parse(id_item);
                    var itemEntity = context.ItemEntity.FirstOrDefault(i => i.IdItem == idItemInt);

                    if (itemEntity is not null)
                    {
                        itemEntity.PrecioLista = decimal.Parse(precio);
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
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        // ************************************ FUNCIONES DE ITEMS ***************************
    }
}
