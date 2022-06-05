using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ApplicationFramework.Infrastructure.Persistence;

public static class EntityEntryExtensions
{
    public static bool IsAddedOrModified(this EntityEntry entry) =>
        entry.State == EntityState.Modified ||
        entry.State == EntityState.Added ||
        entry.References.Any(r => r.TargetEntry != null &&
                                  r.TargetEntry.Metadata.IsOwned() && IsAddedOrModified(r.TargetEntry));
}