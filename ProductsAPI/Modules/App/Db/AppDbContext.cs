using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using ProductsAPI.Core.Constants;
using ProductsAPI.Core.Utils.Db;
using ProductsAPI.Modules.Products.Db.Entities;

namespace ProductsAPI.Modules.App.Db;

public sealed class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<ProductEntity> Products => Set<ProductEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<ProductEntity>(entity =>
        {
            entity.ToTableWithDefaultProperties("products");

            entity.Property(e => e.Name)
                .HasColumnName("name")
                .HasMaxLength(Limits.Product.Name.MaxLength)
                .IsRequired();
        });
    }
}