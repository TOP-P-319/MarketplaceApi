using ProductsAPI.Modules.Products.Db.Repos;
using ProductsAPI.Modules.Products.Domain.Models;

namespace ProductsAPI.Modules.Products.Services;

public sealed class ProductsService(IProductRepo productRepo) : IProductsService
{
    public async Task<ProductModel?> GetProduct(Guid id) => await productRepo.FindByIdAsync(id);
    public async Task<IEnumerable<ProductModel>> GetAllProducts() => await productRepo.FindAllAsync();
}