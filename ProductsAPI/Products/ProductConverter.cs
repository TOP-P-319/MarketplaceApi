using ProductsAPI.Products.Requests;
using ProductsAPI.Products.Responses;
using Shared.Products;

namespace ProductsAPI.Products;

public static class ProductConverter
{
    extension(ProductModel product)
    {
        public GetProductResponse ConvertToGetProductResponse() =>
            new()
            {
                SellerId = product.SellerId,
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                ImageUrls = product.ImageUrls.Select(uri => uri.ToString()).ToArray(),
                Price = product.Price.ToString(),
                Amount = product.Amount,
                Features = product.Features,
                CreatedAt = product.CreatedAt,
                UpdatedAt = product.UpdatedAt,
            };

        public GetProductPreviewResponse ConvertToGetProductPreviewResponse() =>
            new()
            {
                SellerId = product.SellerId,
                Id = product.Id,
                Name = product.Name,
                PreviewUrl = product.ImageUrls.FirstOrDefault()?.ToString(),
                Price = product.Price.ToString(),
                Amount = product.Amount,
            };

        public CreateProductResponse ConvertToCreateProductResponse() =>
            new()
            {
                Id = product.Id,
                CreatedAt = product.CreatedAt,
            };

        public UpdateProductResponse ConvertToUpdateProductResponse() =>
            new()
            {
                UpdatedAt = product.UpdatedAt,
            };
    }

    public static ProductModel ConvertToProductModel(this CreateProductRequest request) => new()
    {
        Name = request.Name,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    };

    public static ProductModel ConvertToProductModel(this UpdateProductRequest request, Guid id) =>
        new ProductModel
        {
            Id = id
        }.WithUpdatedName(request.Name);
}