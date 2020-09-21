using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace NpgsqlWithOwnedEntities
{
public class MyDbContext : DbContext
{
    public DbSet<SimpleEntity> SimpleEntities { get; set; }
    public DbSet<ExtendedEntity> ExtendedEntities { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=:memory:;Version=3;New=True;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // RegisterEntityIdAsNeverGenerated
        // foreach (var mutableEntityType in modelBuilder.Model
        //     .GetEntityTypes()
        //     .Where(mt => typeof(Entity).GetTypeInfo().IsAssignableFrom(IntrospectionExtensions.GetTypeInfo(mt.ClrType))))
        // {
        //     modelBuilder.Entity(mutableEntityType.ClrType).Property(nameof(Entity.Id)).ValueGeneratedNever();
        // }
        
        // RegisterRowVersionProperty
        // foreach (var mutableEntityType in modelBuilder.Model
        //     .GetEntityTypes()
        //     .Where(mt => typeof(Entity).GetTypeInfo().IsAssignableFrom(mt.ClrType.GetTypeInfo())))
        // {
        //     modelBuilder.Entity(mutableEntityType.ClrType).UseXminAsConcurrencyToken();
        // }
        
        // modelBuilder.HasDefaultSchema("test33");
        
        // ApplyAggregateMappings
modelBuilder.Entity<SimpleEntity>(baseEntityBuilder =>
{
    baseEntityBuilder.Property(simpleEntity => simpleEntity.Name).HasMaxLength(100);
    baseEntityBuilder.OwnsOne(simpleEntity => simpleEntity.OwnedOne, ownedOneBuilder =>
    {
        ownedOneBuilder.Property(ownedOne => ownedOne.Name1).HasMaxLength(120);
    });
});
        
        modelBuilder.Entity<ExtendedEntity>(extendedEntityBuilder =>
        {
            extendedEntityBuilder.HasBaseType<SimpleEntity>();
            extendedEntityBuilder.Property(extendedEntity => extendedEntity.ExtendedName).HasMaxLength(1000).IsRequired();
            // extendedEntityBuilder.OwnsOne(extendedEntity => extendedEntity.OwnedTwo, ownedTwoBuilder =>
            // {
            //     ownedTwoBuilder.Property(ownedTwo => ownedTwo.Name2).HasMaxLength(300);
            // });
        });
        
        // ApplySnakeCaseMapping
        // foreach (IMutableEntityType entity in modelBuilder.Model.GetEntityTypes())
        // {
        //     if (entity.BaseType == null && !entity.IsOwned())
        //     {
        //         entity.SetTableName(entity.GetTableName().ToSnakeCase());
        //     }
        //
        //     foreach (IMutableProperty property in entity.GetProperties())
        //     {
        //         property.SetColumnName(property.GetColumnName().ToSnakeCase());
        //     }
        //
        //     foreach (IMutableKey key in entity.GetKeys())
        //     {
        //         key.SetName(key.GetName().ToSnakeCase());
        //     }
        //
        //     foreach (IMutableForeignKey key in entity.GetForeignKeys())
        //     {
        //         key.SetConstraintName(key.GetConstraintName().ToSnakeCase());
        //     }
        //
        //     foreach (IMutableIndex index in entity.GetIndexes())
        //     {
        //         index.SetName(index.GetName().ToSnakeCase());
        //     }
        // }
        
        
    }
    
    
}
}