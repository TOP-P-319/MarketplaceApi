using ProductsAPI.Modules.Products.Db.Repos;
using ProductsAPI.Modules.Products.Domain.Converters;
using ProductsAPI.Modules.Products.Dtos.Requests;
using ProductsAPI.Modules.Products.Dtos.Responses;

namespace ProductsAPI.Modules.Products.Services;

public sealed class ProductsService(IProductsRepo productsRepo) : IProductsService
{
    public async Task<GetProductResponse?> GetProductAsync(Guid id)
    {
        var product = await productsRepo.FindByIdAsync(id);
        return product?.ConvertToGetProductResponse();
    }

    public async Task<IEnumerable<GetProductResponse>> GetAllProductsAsync()
    {
        var products = await productsRepo.FindAllAsync();
        return products.Select(ProductConverter.ConvertToGetProductResponse);
    }

    public async Task RemoveProductAsync(Guid id) => await productsRepo.DeleteByIdAsync(id);

    public async Task<CreateProductResponse> AddProductAsync(CreateProductRequest request)
    {
        var product = request.ConvertToProductModel();
        await productsRepo.AddAsync(product);
        return product.ConvertToCreateProductResponse();
    }

    public async Task<UpdateProductResponse> UpdateProductAsync(Guid id, UpdateProductRequest request)
    {
        var product = request.ConvertToProductModel(id);
        await productsRepo.UpdateAsync(product);
        return product.ConvertToUpdateProductResponse();
    }
}