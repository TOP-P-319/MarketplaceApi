using Microsoft.EntityFrameworkCore;
using ProductsAPI.Core;
using ProductsAPI.Core.Utils.Db;
using ProductsAPI.Modules.Products.Db.Entities;

namespace ProductsAPI.Modules.Shared.Db;

public sealed class ProductsDbContext(DbContextOptions<ProductsDbContext> options) : DbContext(options)
{
    public DbSet<ProductEntity> Products => Set<ProductEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<ProductEntity>(entity =>
        {
            EntityTypeBuilderEx.ToTable(entity, "products");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.Name)
                .HasColumnName("name")
                .HasMaxLength(Limits.Product.Name.MaxLength)
                .IsRequired();
        });
    }
}