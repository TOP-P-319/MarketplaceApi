using ProductsAPI.Modules.Products.Domain.Models;

namespace ProductsAPI.Modules.Products.Services;

public interface IProductsService
{
    Task<ProductModel?> GetProductAsync(Guid id);
    Task<IEnumerable<ProductModel>> GetAllProductsAsync();
    Task RemoveProductAsync(Guid id);
    Task AddProductAsync(ProductModel product);
    Task UpdateProductAsync(ProductModel product);
}