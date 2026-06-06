using Microsoft.EntityFrameworkCore;
using ProductsAPI.Core.Infrastructure.Db.Repos;
using ProductsAPI.Core.Infrastructure.Domain.Mappers;
using ProductsAPI.Modules.Products.Db.Entities;
using ProductsAPI.Modules.Products.Domain.Models;
using ProductsAPI.Modules.Shared.Db;

namespace ProductsAPI.Modules.Products.Db.Repos;

public sealed class ProductRepo(
    ProductsDbContext context,
    IMapper<ProductModel, ProductEntity> mapper
) : RepoBase<ProductModel, ProductEntity>(context, mapper, context.Products),
    IProductRepo;
