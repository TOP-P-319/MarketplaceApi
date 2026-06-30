using Microsoft.EntityFrameworkCore;
using Shared.Products;
using Shared.Purchases;
using Shared.Requests;
using Shared.Reviews;
using Shared.Users;

namespace Shared.Infrastructure;

public sealed class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<ProductEntity> Products => Set<ProductEntity>();
    public DbSet<UserEntity> Users => Set<UserEntity>();
    public DbSet<PurchaseEntity> Purchases => Set<PurchaseEntity>();
    public DbSet<RequestEntity> Requests => Set<RequestEntity>();
    public DbSet<ReviewEntity> Reviews => Set<ReviewEntity>();
}