using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using ProductsAPI.Core.Constants;
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
            entity.ToTable("products");
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id)
                .HasColumnName("id")
                .HasDefaultValueSql("gen_random_uuid()")
                .ValueGeneratedOnAdd();
            
            entity.Property(e => e.Name)
                .HasColumnName("name")
                .HasMaxLength(Limits.Product.Name.MaxLength)
                .IsRequired();
        });
    }
}