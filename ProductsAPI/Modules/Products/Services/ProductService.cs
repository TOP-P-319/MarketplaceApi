using ProductsAPI.Modules.Products.Domain.Models;

namespace ProductsAPI.Modules.Products.Services;

public sealed class ProductService : IProductService
{
    public async Task<ProductModel> GetProduct(int id)
    {
        return new ProductModel
        {
            Id = id,
            Name = "Какой-то продукт"
        };
    }
}