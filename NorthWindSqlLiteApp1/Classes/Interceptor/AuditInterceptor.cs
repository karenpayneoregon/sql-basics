using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using NorthWindSqlLiteApp1.Models.Interceptor;
using Serilog;
using System.Data.Common;
using NorthWindSqlLiteApp1.Classes.Extensions;

namespace NorthWindSqlLiteApp1.Classes.Interceptor;
/// <summary>
/// Provides an implementation of <see cref="SaveChangesInterceptor"/> to audit and log changes
/// made to the entities tracked by the <see cref="DbContext"/> during save operations.
/// </summary>
/// <remarks>
/// This class intercepts save operations to inspect and log changes to the tracked entities.
/// It supports both synchronous and asynchronous save operations and ensures that changes
/// are logged in a consistent manner. The changes are analyzed and serialized for logging purposes.
/// </remarks>
public class AuditInterceptor : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = new())
    {
        Inspect(eventData);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        Inspect(eventData);
        return base.SavingChanges(eventData, result);
    }

    public override int SavedChanges(SaveChangesCompletedEventData eventData, int result)
    {
        Inspect(eventData);
        return base.SavedChanges(eventData, result);
    }

    /// <summary>
    /// Inspects the changes in the <see cref="DbContext"/> tracked entities during a save operation.
    /// </summary>
    /// <param name="eventData">
    /// The <see cref="DbContextEventData"/> containing information about the current save operation,
    /// including the <see cref="DbContext"/> and its tracked entities.
    /// </param>
    /// <remarks>
    /// This method analyzes the state of tracked entities and logs the changes, including added, deleted,
    /// and modified entities. The changes are serialized to JSON for logging purposes.
    /// </remarks>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown when an unexpected <see cref="EntityState"/> is encountered.
    /// </exception>
    private static void Inspect(DbContextEventData eventData)
    {
        var changesList = new List<CompareModel>();

        foreach (EntityEntry entry in eventData.Context!.ChangeTracker.Entries())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    changesList.Add(new CompareModel()
                    {
                        OriginalValue = null,
                        NewValue = entry.CurrentValues.ToObject(),
                        EntityState = nameof(EntityState.Added)
                    });
                    
                    break;
                
                case EntityState.Deleted:
                    changesList.Add(new CompareModel()
                    {
                        OriginalValue = entry.OriginalValues.ToObject(),
                        NewValue = null,
                        EntityState = nameof(EntityState.Deleted)
                    });
                    
                    break;
                
                case EntityState.Modified:
                    changesList.Add(new CompareModel()
                    {
                        OriginalValue = entry.OriginalValues.ToObject(),
                        NewValue = entry.CurrentValues.ToObject(),
                        EntityState = nameof(EntityState.Modified)
                    });
                    
                    break;
                
                case EntityState.Detached:
                case EntityState.Unchanged:
                    break;
                
                default:
                    throw new ArgumentOutOfRangeException();
            }

            if (changesList.Count > 0)
            {
                Log.Information($"\nchange list:{changesList.ToJson()}");
            }
        }
    }
}

public class ConnectionInterceptor : DbConnectionInterceptor
{
    public override DbConnection ConnectionCreated(ConnectionCreatedEventData eventData, DbConnection result)
    {

        return base.ConnectionCreated(eventData, result);
    }
}