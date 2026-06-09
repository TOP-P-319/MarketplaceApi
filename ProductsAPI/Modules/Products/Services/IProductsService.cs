using ProductsAPI.Modules.Products.Domain.Models;

namespace ProductsAPI.Modules.Products.Services;

public interface IProductsService
{
    Task<ProductModel?> GetProduct(Guid id);
}