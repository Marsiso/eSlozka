using eSlozka.Domain.Models.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace eSlozka.Data;

public class AuditingSaveChangeInterceptor : SaveChangesInterceptor
{
    private static void OnAfterSavedChanges(DataContext context)
    {
    }

    private static void OnBeforeSavedChanges(DataContext context)
    {
        context.ChangeTracker.DetectChanges();

        var dateTime = DateTime.UtcNow;

        foreach (var entityEntry in context.ChangeTracker.Entries<ChangeTrackingEntity>())
            switch (entityEntry.State)
            {
                case EntityState.Added:
                    entityEntry.Entity.IsActive = true;
                    entityEntry.Entity.DateCreated = entityEntry.Entity.DateUpdated = dateTime;
                    continue;

                case EntityState.Modified:
                    entityEntry.Entity.DateUpdated = dateTime;
                    continue;

                case EntityState.Deleted:
                    throw new InvalidOperationException();

                default:
                    continue;
            }
    }

    public override int SavedChanges(SaveChangesCompletedEventData eventData, int result)
    {
        if (eventData.Context is not DataContext context) return base.SavedChanges(eventData, result);

        OnAfterSavedChanges(context);

        return base.SavedChanges(eventData, result);
    }

    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        if (eventData.Context is not DataContext context) return base.SavingChanges(eventData, result);

        OnBeforeSavedChanges(context);

        return base.SavingChanges(eventData, result);
    }
}
