using ProductsAPI.Modules.Products.Domain.Models;
using ProductsAPI.Modules.Products.Dtos.Requests;
using ProductsAPI.Modules.Products.Dtos.Responses;

namespace ProductsAPI.Modules.Products.Domain.Converters;

public static class ProductConverter
{
    extension(ProductModel product)
    {
        public GetProductResponse ConvertToGetProductResponse() =>
            new()
            {
                Id = product.Id,
                Name = product.Name,
                PreviewUrl = product.PreviewUrl?.ToString(),
                CreatedAt = product.CreatedAt,
                UpdatedAt = product.UpdatedAt,
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
        PreviewUrl = Uri.TryCreate(request.PreviewUrl, UriKind.Absolute, out var previewUrl) ? previewUrl : null,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    };

    public static ProductModel ConvertToProductModel(this UpdateProductRequest request, Guid id) =>
        new ProductModel
            {
                Id = id
            }.WithUpdatedName(request.Name)
            .WithUpdatedPreviewUrl(Uri.TryCreate(request.PreviewUrl, UriKind.Absolute, out var previewUrl)
                ? previewUrl
                : null);
}