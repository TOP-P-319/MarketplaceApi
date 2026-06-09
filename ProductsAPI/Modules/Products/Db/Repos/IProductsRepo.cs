using ProductsAPI.Core.Infrastructure.Db.Repos;
using ProductsAPI.Modules.Products.Domain.Models;

namespace ProductsAPI.Modules.Products.Db.Repos;

public interface IProductsRepo : IRepoBase<ProductModel>;