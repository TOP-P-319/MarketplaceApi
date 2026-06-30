using ProductsAPI.Products.Requests;
using ProductsAPI.Products.Responses;
using Shared.Products;

namespace ProductsAPI.Products;

public sealed class ProductsService(ProductsRepo productsRepo)
{
    public async Task<GetProductResponse?> GetProductAsync(Guid id)
    {
        var product = await productsRepo.FindByIdAsync(id);
        return product?.ConvertToGetProductResponse();
    }

    public async Task<IEnumerable<GetProductPreviewResponse>> GetAllProductPreviewsAsync()
    {
        var products = await productsRepo.FindAllAsync();
        return products.Select(ProductConverter.ConvertToGetProductPreviewResponse);
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