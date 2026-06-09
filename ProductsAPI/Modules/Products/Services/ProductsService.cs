using ProductsAPI.Modules.Products.Db.Repos;
using ProductsAPI.Modules.Products.Domain.Models;

namespace ProductsAPI.Modules.Products.Services;

public sealed class ProductsService(IProductsRepo productsRepo) : IProductsService
{
    public async Task<ProductModel?> GetProduct(Guid id) => await productsRepo.FindByIdAsync(id);
}