using ProductsAPI.Core.Infrastructure.Db.Mappers;
using ProductsAPI.Core.Infrastructure.Db.Repos;
using ProductsAPI.Modules.App.Db;
using ProductsAPI.Modules.Products.Db.Entities;
using ProductsAPI.Modules.Products.Domain.Models;

namespace ProductsAPI.Modules.Products.Db.Repos;

public sealed class ProductsRepo(
    AppDbContext ctx,
    IMapper<ProductModel, ProductEntity> mapper
) : RepoBase<ProductModel, ProductEntity>(ctx, ctx.Products, mapper), IProductsRepo;