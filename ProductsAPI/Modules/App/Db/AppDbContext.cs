using Microsoft.EntityFrameworkCore;
using ProductsAPI.Modules.Products.Db.Entities;

namespace ProductsAPI.Modules.App.Db;

public sealed class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<ProductEntity> Products => Set<ProductEntity>();
}