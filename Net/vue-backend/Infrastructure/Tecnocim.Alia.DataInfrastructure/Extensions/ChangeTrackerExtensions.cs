using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using Tecnocim.Alia.Domain;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Tecnocim.Alia.DataInfrastructure.Extensions
{
    public static class ChangeTrackerExtensions
    {
        public static void SetAuditProperties(this ChangeTracker changeTracker)
        {
            changeTracker.DetectChanges();
            IEnumerable<EntityEntry> entities =
                changeTracker
                    .Entries()
                    .Where(t => t.Entity is AuditableEntity && t.State == EntityState.Deleted);

            if (entities.Any())
            {
                foreach (EntityEntry entry in entities)
                {
                    AuditableEntity entity = (AuditableEntity)entry.Entity;
                    entity.Deleted = DateTime.UtcNow;
                    entry.State = EntityState.Modified;

                    foreach (var navigationEntry in entry.Navigations.Where(n => !((IReadOnlyNavigation)n.Metadata).IsOnDependent))
                    {
                        navigationEntry.Load();
                        if (navigationEntry is CollectionEntry collectionEntry)
                        {
                            foreach (var dependentEntry in collectionEntry.CurrentValue)
                            {
                                HandleDependent(dependentEntry as AuditableEntity);
                            }
                        }
                        else
                        {
                            var dependentEntry = navigationEntry.CurrentValue;
                            if (dependentEntry != null)
                            {
                                HandleDependent(dependentEntry as AuditableEntity);
                            }
                        }
                    }
                }
            }
        }

        private static void HandleDependent(AuditableEntity entry)
        {
            if(entry == null)
            {
                return;
            }

            entry.Deleted = DateTime.UtcNow;
        }
    }
}
