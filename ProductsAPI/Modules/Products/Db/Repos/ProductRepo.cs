using Microsoft.EntityFrameworkCore;
using ProductsAPI.Core.Infrastructure.Db.Repos;
using ProductsAPI.Core.Infrastructure.Domain.Mappers;
using ProductsAPI.Modules.Products.Db.Entities;
using ProductsAPI.Modules.Products.Domain.Models;

namespace ProductsAPI.Modules.Products.Db.Repos;

public sealed class ProductRepo(DbContext context, IMapper<ProductModel, ProductEntity> mapper, DbSet<ProductEntity> table) : RepoBase<ProductModel, ProductEntity>(context, mapper, table)
{
    
}