using ProductsAPI.Modules.Products.Domain.Models;

namespace ProductsAPI.Modules.Products.Services;

public interface IProductsService
{
    Task<ProductModel?> GetProductAsync(Guid id);
    Task<IEnumerable<ProductModel>> GetAllProductsAsync();
    Task AddProductAsync(ProductModel product);
}