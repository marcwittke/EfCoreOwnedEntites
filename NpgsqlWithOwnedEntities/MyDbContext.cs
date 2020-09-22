using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NpgsqlWithOwnedEntities.Lib;
using NpgsqlWithOwnedEntities.StockItems;
using NpgsqlWithOwnedEntities.Stores;

namespace NpgsqlWithOwnedEntities
{
    public class MyDbContext : DbContext
    {
        public DbSet<Store> Stores { get; set; }
        public DbSet<StockItem> StockItems { get; set; }
        public DbSet<ItemTransaction> ItemTransactions { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlite("Data Source=:memory:;Version=3;New=True;");
            optionsBuilder.UseNpgsql("Host=localhost;Username=anicors;Password=P0stgr3s;Database=testdb;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // RegisterEntityIdAsNeverGenerated
            foreach (var mutableEntityType in modelBuilder.Model
                .GetEntityTypes()
                .Where(mt => typeof(Entity).GetTypeInfo().IsAssignableFrom(mt.ClrType.GetTypeInfo())))
            {
                modelBuilder.Entity(mutableEntityType.ClrType).Property(nameof(Entity.Id)).ValueGeneratedNever();
            }

            // RegisterRowVersionProperty
            foreach (var mutableEntityType in modelBuilder.Model
                .GetEntityTypes()
                .Where(mt => typeof(Entity).GetTypeInfo().IsAssignableFrom(mt.ClrType.GetTypeInfo())))
            {
                modelBuilder.Entity(mutableEntityType.ClrType).UseXminAsConcurrencyToken();
            }

            modelBuilder.HasDefaultSchema("inventory");

            // ApplyAggregateMappings
            modelBuilder.Entity<Store>(sb =>
            {
                sb.Property(s => s.Name).IsRequired().HasMaxLength(100);
                sb.Property(s => s.Site).HasMaxLength(100);
                sb.Property(s => s.Building).IsRequired().HasMaxLength(100);
                sb.Property(s => s.Room).HasMaxLength(100);
                sb.Property(s => s.DefaultLocation).HasMaxLength(100);

                sb.OwnsOne(itm => itm.CoolingTemperature, ownedNavBuilder =>
                {
                    ownedNavBuilder.Property<uint>("xmin").HasColumnType("xid").HasColumnName("xmin")
                        .ValueGeneratedOnAddOrUpdate().IsConcurrencyToken();
                    ownedNavBuilder.Property(t => t.Unit)
                        .HasConversion(new EnumToStringConverter<TemperatureRangeUnit>()).HasMaxLength(3);
                });
            });

            modelBuilder.Entity<StockItem>(sib =>
            {
                sib.Property(si => si.Barcode).IsRequired().HasMaxLength(100);
                sib.Property(si => si.Compartment).HasMaxLength(100);
                sib.Property(si => si.Department).HasMaxLength(100);
                sib.Ignore(si => si.StoreName);
                sib.Property(si => si.InventoryBaseUnit).HasMaxLength(50);
                sib.Property(si => si.InventoryDisplayUnit).HasMaxLength(50);
                sib.Property(si => si.MinimumInventoryBaseUnit).HasMaxLength(50);
                sib.Property(si => si.MinimumInventoryDisplayUnit).HasMaxLength(50);
                sib.Property(si => si.Status)
                    .HasConversion(new EnumToStringConverter<StockItemStatus>())
                    .HasMaxLength(15)
                    .IsRequired();

                sib.OwnsOne(si => si.CatalogItem, cb => cb.MapCatalogItemInfoAsOwned());
                sib.OwnsOne(si => si.Product, pb => pb.MapProductAsOwned());
                sib.OwnsOne(si => si.Substance, sb => sb.MapSubstanceAsOwned());

                sib.HasOne<Store>().WithMany().OnDelete(DeleteBehavior.Restrict);

                sib.OwnsOne(si => si.ResponsibleOwner, eb =>
                {
                    //eb.Property<uint>("xmin").HasColumnType("xid").HasColumnName("xmin").ValueGeneratedOnAddOrUpdate().IsConcurrencyToken();
                    eb.Property(e => e.Department).HasMaxLength(400);
                    eb.Property(e => e.EmployeeId).HasMaxLength(400);
                    eb.Property(e => e.Name).HasMaxLength(400);
                    eb.Property(e => e.PhoneNumber).HasMaxLength(200);
                });

                sib.OwnsOne(si => si.Reservation, rb =>
                {
                    rb.Property<uint>("xmin").HasColumnType("xid").HasColumnName("xmin").ValueGeneratedOnAddOrUpdate()
                        .IsConcurrencyToken();
                    rb.Property(e => e.Department).HasMaxLength(400);
                    rb.Property(e => e.EmployeeId).HasMaxLength(400);
                    rb.Property(e => e.Name).HasMaxLength(400);
                    rb.Property(e => e.PhoneNumber).HasMaxLength(200);
                });

                sib.OwnsOne(si => si.TransferredFor, eb =>
                {
                    eb.Property<uint>("xmin").HasColumnType("xid").HasColumnName("xmin").ValueGeneratedOnAddOrUpdate()
                        .IsConcurrencyToken();
                    eb.Property(e => e.Department).HasMaxLength(400);
                    eb.Property(e => e.EmployeeId).HasMaxLength(400);
                    eb.Property(e => e.Name).HasMaxLength(400);
                    eb.Property(e => e.PhoneNumber).HasMaxLength(200);
                });
            });

            modelBuilder.Entity<ItemTransaction>(itb => { itb.Ignore(it => it.Actor); });


            modelBuilder.Entity<CheckingIn>(cib => { cib.HasBaseType<ItemTransaction>(); });

            modelBuilder.Entity<CheckingOut>(cob =>
            {
                cob.HasBaseType<ItemTransaction>();
                cob.Ignore(co => co.Recipient);
            });


            // ApplySnakeCaseMapping
            foreach (IMutableEntityType entity in modelBuilder.Model.GetEntityTypes())
            {
                if (entity.BaseType == null && !entity.IsOwned())
                {
                    entity.SetTableName(entity.GetTableName().ToSnakeCase());
                }

                foreach (IMutableProperty property in entity.GetProperties())
                {
                    property.SetColumnName(property.GetColumnName().ToSnakeCase());
                }

                foreach (IMutableKey key in entity.GetKeys())
                {
                    key.SetName(key.GetName().ToSnakeCase());
                }

                foreach (IMutableForeignKey key in entity.GetForeignKeys())
                {
                    key.SetConstraintName(key.GetConstraintName().ToSnakeCase());
                }

                foreach (IMutableIndex index in entity.GetIndexes())
                {
                    index.SetName(index.GetName().ToSnakeCase());
                }
            }
        }
    }

    public static class ModelBuilderExtensions
    {
        public static void MapSubstanceAsOwned<T>(this OwnedNavigationBuilder<T, Substance> ownedSubstanceBuilder) where T : class
        {
            ownedSubstanceBuilder.Property<uint>("xmin").HasColumnType("xid").HasColumnName("xmin").ValueGeneratedOnAddOrUpdate().IsConcurrencyToken();
            ownedSubstanceBuilder.Property(s => s.CasRegistryNumber).HasMaxLength(100);
            ownedSubstanceBuilder.Property(s => s.DensityBaseUnit).HasMaxLength(50);
            ownedSubstanceBuilder.Property(s => s.DensityDisplayUnit).HasMaxLength(50);
            ownedSubstanceBuilder.Property(s => s.Formula).HasMaxLength(100);
            ownedSubstanceBuilder.Property(s => s.IupacName).HasMaxLength(500);
        }

        public static void MapProductAsOwned<T>(this OwnedNavigationBuilder<T, Product> ownedProductBuilder) where T : class
        {
            ownedProductBuilder.Property<uint>("xmin").HasColumnType("xid").HasColumnName("xmin").ValueGeneratedOnAddOrUpdate().IsConcurrencyToken();
            ownedProductBuilder.Property(p => p.Name).HasMaxLength(2000);
            ownedProductBuilder.Property(p => p.Producer).HasMaxLength(100);
            ownedProductBuilder.Property(p => p.ProducerItemNumber).HasMaxLength(100);
            ownedProductBuilder.Property(p => p.Unspsc).HasMaxLength(10);
            ownedProductBuilder.Property(p => p.Url).HasMaxLength(2000);
        }

        public static void MapCatalogItemInfoAsOwned<T>(this OwnedNavigationBuilder<T, CatalogItemInfo> ownedCatalogItemInfoBuilder) where T : class
        {
            ownedCatalogItemInfoBuilder.Property<uint>("xmin").HasColumnType("xid").HasColumnName("xmin").ValueGeneratedOnAddOrUpdate().IsConcurrencyToken();
            ownedCatalogItemInfoBuilder.Property(p => p.Batch).HasMaxLength(100);
            ownedCatalogItemInfoBuilder.Property(p => p.CatalogName).HasMaxLength(100);
            ownedCatalogItemInfoBuilder.Property(p => p.LeadTime).HasMaxLength(100);
            ownedCatalogItemInfoBuilder.Property(p => p.PackageAmountBaseUnit).HasMaxLength(50);
            ownedCatalogItemInfoBuilder.Property(p => p.PackageAmountDisplayUnit).HasMaxLength(50);
            ownedCatalogItemInfoBuilder.Property(p => p.PackageDescription).HasMaxLength(100);
            ownedCatalogItemInfoBuilder.Property(p => p.PriceCurrency).HasMaxLength(5);
            ownedCatalogItemInfoBuilder.Property(p => p.Url).HasMaxLength(2000);

            ownedCatalogItemInfoBuilder.OwnsOne(p => p.Supplier, ownedSupplierInfoBuilder =>
            {
                ownedSupplierInfoBuilder.Property<uint>("xmin").HasColumnType("xid").HasColumnName("xmin").ValueGeneratedOnAddOrUpdate().IsConcurrencyToken();
                ownedSupplierInfoBuilder.Property(s => s.City).HasMaxLength(200);
                ownedSupplierInfoBuilder.Property(s => s.Country).HasMaxLength(200);
                ownedSupplierInfoBuilder.Property(s => s.Email).HasMaxLength(200);
                ownedSupplierInfoBuilder.Property(s => s.Name).HasMaxLength(100);
                ownedSupplierInfoBuilder.Property(s => s.Phone).HasMaxLength(100);
                ownedSupplierInfoBuilder.Property(s => s.Url).HasMaxLength(2000);
            });
        }
    }
}