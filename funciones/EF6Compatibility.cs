using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Linq;
using System.Threading.Tasks;

#region EF6 Compatibility Layer
// ============================================================
// EF6Compatibility.cs
// ------------------------------------------------------------
// Capa de compatibilidad para código migrado desde EF6 a EF Core
// Permite usar métodos antiguos como ExecuteSqlCommand()
// con TransactionalBehavior.EnsureTransaction
// ============================================================

public static class EF6Compatibility
{
    /// <summary>
    /// Enumeración ficticia para compatibilidad con EF6
    /// No tiene efecto real, pero permite compilar sin cambios.
    /// </summary>
    public enum TransactionalBehavior
    {
        EnsureTransaction,
        DoNotEnsureTransaction
    }

    /// <summary>
    /// Ejecuta una instrucción SQL dentro de una transacción
    /// emulando el comportamiento de EF6:
    /// context.Database.ExecuteSqlCommand(TransactionalBehavior.EnsureTransaction, sql)
    /// </summary>
    public static int ExecuteSqlCommand(this DatabaseFacade database, string entityName, object? parameters = null)
    {
        if (database == null)
            throw new ArgumentNullException(nameof(database));

        if (string.IsNullOrWhiteSpace(entityName))
            throw new ArgumentException("Debe especificarse el nombre de la entidad o tabla lógica.", nameof(entityName));

        var context = (DbContext)database.GetService(typeof(DbContext));
        int affected = 0;

        using var transaction = database.BeginTransaction();

        try
        {
            switch (entityName.Trim().ToUpperInvariant())
            {
                case "COBROSRETENCIONES":
                    {
                        var param = parameters as dynamic;
                        int? idCobro = param?.IdCobro;

                        var toDelete = context.Set<CobroRetencionEntity>()
                            .Where(c => c.IdCobro == idCobro)
                            .ToList();

                        context.RemoveRange(toDelete);
                        break;
                    }

                case "TRANSFERENCIAS":
                    {
                        var param = parameters as dynamic;
                        int? idCobro = param?.IdCobro;

                        var toDelete = context.Set<TransferenciaEntity>()
                            .Where(t => t.IdCobro == idCobro)
                            .ToList();

                        context.RemoveRange(toDelete);
                        break;
                    }

                case "CHEQUES":
                    {
                        var param = parameters as dynamic;
                        int? idCobro = param?.IdCobro;
                        int? idEstadoCH = param?.IdEstadoCH;

                        var chequeIds = context.Set<CobroChequeEntity>()
                            .Where(cc => cc.IdCobro == idCobro)
                            .Select(cc => cc.IdCheque)
                            .ToList();

                        if (chequeIds.Count > 0)
                        {
                            var toDelete = context.Set<ChequeEntity>()
                                .Where(ch => chequeIds.Contains(ch.IdCheque) && ch.IdEstadoch == idEstadoCH)
                                .ToList();

                            context.RemoveRange(toDelete);
                        }
                        break;
                    }

                default:
                    throw new NotSupportedException($"Operación no implementada para la entidad '{entityName}'.");
            }

            affected = context.SaveChanges();
            transaction.Commit();
        }
        catch
        {
            transaction.Rollback();
            throw;
        }

        return affected;
    }

    /// <summary>
    /// Versión asincrónica compatible con EF Core
    /// </summary>
    public static async Task<int> ExecuteSqlCommandAsync(this DatabaseFacade database, TransactionalBehavior behavior, string sql)
    {
        if (database == null)
            throw new ArgumentNullException(nameof(database));

        if (string.IsNullOrWhiteSpace(sql))
            throw new ArgumentException("El comando SQL no puede estar vacío.", nameof(sql));

        int result = 0;

        await using (var transaction = await database.BeginTransactionAsync())
        {
            try
            {
                result = await database.ExecuteSqlRawAsync(sql);
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        return result;
    }
}

#endregion

