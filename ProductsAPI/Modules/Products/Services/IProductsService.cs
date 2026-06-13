using ProductsAPI.Modules.Products.Dtos.Requests;
using ProductsAPI.Modules.Products.Dtos.Responses;

namespace ProductsAPI.Modules.Products.Services;

public interface IProductsService
{
    Task<GetProductResponse?> GetProductAsync(Guid id);
    Task<IEnumerable<GetProductResponse>> GetAllProductsAsync();
    Task RemoveProductAsync(Guid id);
    Task<CreateProductResponse> AddProductAsync(CreateProductRequest request);
    Task<UpdateProductResponse> UpdateProductAsync(Guid id, UpdateProductRequest request);
}