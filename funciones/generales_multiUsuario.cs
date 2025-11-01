using System;
using System.Data;
using System.Linq;
namespace Centrex.Funciones
{
    public static class generales_multiUsuario
    {
        // ************************************* FUNCIONES MULTI-USUARIO CON ENTITY FRAMEWORK *****************************

        /// <summary>
        // ************************************* FUNCIONES MULTI-USUARIO CON ENTITY FRAMEWORK *****************************

        /// <summary>
        /// Borra tabla de pedidos temporales según usuario - Migrado a EF
        /// </summary>
        public static byte borrar_tabla_pedidos_temporales(int idUsuario)
        {
            try
            {
                using (var context = new CentrexDbContext())
                {

                    var itemsTemp = context.TmpPedidoItemEntity.Where(t => t.IdUsuario == idUsuario).ToList();
                    if (itemsTemp.Count == 0)
                    {
                        return Conversions.ToByte(true);
                    }

                    context.TmpPedidoItemEntity.RemoveRange(itemsTemp);
                    context.SaveChanges();
                    return Conversions.ToByte(true);
                }
            }
            catch (Exception ex)
            {
                Interaction.MsgBox(ex.Message);
                return Conversions.ToByte(false);
            }
        }

        //// NOTA: Función duplicada en produccion.vb, esta versión está comentada para evitar ambigüedad
        //public static void borrarTmpProduccionGeneral(int id_usuario)
        //{
        //    Borrar_tabla_segun_usuario("tmpproduccion_asocItems", id_usuario);
        //    Borrar_tabla_segun_usuario("tmpproduccion_items", id_usuario);
        //}

        /// <summary>
        /// Borra tabla según usuario - Migrado a EF
        /// </summary>
        public static bool Borrar_tabla_segun_usuario(string tbl, int id_usuario)
        {
            try
            {
                using (var context = new CentrexDbContext())
                using (var dbTrans = context.Database.BeginTransaction())
                {
                    try
                    {
                        if (string.IsNullOrWhiteSpace(tbl) || tbl.Any(c => !char.IsLetterOrDigit(c) && c != '_'))
                            throw new ArgumentException("Nombre de tabla inválido: " + tbl);

                        string sqlDelete = $"DELETE FROM [{tbl}] WHERE id_usuario = {id_usuario};";
                        context.Database.ExecuteSqlRaw(sqlDelete);

                        string sqlReseed = $"DBCC CHECKIDENT ('[{tbl}]', RESEED, 0);";
                        context.Database.ExecuteSqlRaw(sqlReseed);

                        dbTrans.Commit();
                        return true;
                    }
                    catch (Exception innerEx)
                    {
                        dbTrans.Rollback();
                        Interaction.MsgBox(
                            $"Error al limpiar tabla '{tbl}': {innerEx.Message}",
                            MsgBoxStyle.Critical,
                            "Centrex"
                        );
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Interaction.MsgBox(
                    $"Error general en Borrar_tabla_segun_usuario: {ex.Message}",
                    MsgBoxStyle.Critical,
                    "Centrex"
                );
                return false;
            }
        }

        /// <summary>
        /// Borra registros temporales de producción para un usuario específico
        /// </summary>
        public static void borrarTmpProduccion(int id_usuario)
        {
            try
            {
                using (var context = new CentrexDbContext())
                {
                    var tmpItems = context.TmpProduccionItemEntity.Where(t => t.IdUsuario == id_usuario).ToList();
                    if (tmpItems.Count > 0)
                        context.TmpProduccionItemEntity.RemoveRange(tmpItems);

                    var tmpAsocItems = context.TmpProduccionAsocItemEntity.Where(t => t.IdUsuario == id_usuario).ToList();
                    if (tmpAsocItems.Count > 0)
                        context.TmpProduccionAsocItemEntity.RemoveRange(tmpAsocItems);

                    context.SaveChanges();
                }
            }
            catch
            {
                // Intencionalmente silencioso para mantener comportamiento legacy
            }
        }

        /// <summary>
        /// Genera ID único usando NEWID() de SQL Server
        /// </summary>
        public static Guid Generar_ID_Unico()
        {
            try
            {
                // 🔹 Generar un GUID nativo en formato estándar
                return Guid.NewGuid();
            }
            catch (Exception)
            {
                // 🔸 En caso de error (extremadamente raro), devolver un GUID vacío
                return Guid.Empty;
            }
        }
    }
}