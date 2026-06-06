using ProductsAPI.Modules.Products.Domain.Models;

namespace ProductsAPI.Modules.Products.Services;

public interface IProductService
{
    Task<ProductModel> GetProduct(int id);
}