using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Tecnocim.Alia.DataInfrastructure.Extensions;

internal static class PropertyTypeConfigurationExtension
{
    public static void SetAllProperties<TProperty>(this ModelBuilder modelBuilder, Func<PropertyBuilder<TProperty>, PropertyBuilder<TProperty>> propertyDefinition)
    {
        var properties = modelBuilder.Model.GetEntityTypes().SelectMany(t => t.GetProperties())
            .Where(p => p.ClrType == typeof(TProperty));

        foreach (var property in properties)
        {
            var propertyBuilder = modelBuilder.Entity(property.DeclaringEntityType.ClrType).Property<TProperty>(property.Name);
            propertyDefinition(propertyBuilder);
        }
    }
}
