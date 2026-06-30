using System.Collections.Frozen;
using System.Numerics;
using ProductsAPI.Products.Requests;
using ProductsAPI.Products.Responses;
using Shared.Products;
using Shared.Requests;
using Shared.Utils;

namespace ProductsAPI.Products;

public static class ProductConverter
{
    extension(ProductModel product)
    {
        public GetProductResponse ConvertToGetProductResponse(string sellerName, double rating, int reviewsCount) =>
            new()
            {
                SellerId = product.SellerId,
                SellerName = sellerName,
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                ImageUrls = product.ImageUrls.Select(uri => uri.ToString()).ToArray(),
                Price = product.Price.ToString(),
                Amount = product.Amount,
                Features = product.Features,
                Rating = rating,
                ReviewsCount = reviewsCount,
                CreatedAt = product.CreatedAt,
                UpdatedAt = product.UpdatedAt,
            };

        public GetProductPreviewResponse ConvertToGetProductPreviewResponse(double rating, int reviewsCount) =>
            new()
            {
                SellerId = product.SellerId,
                Id = product.Id,
                Name = product.Name,
                PreviewUrl = product.ImageUrls.FirstOrDefault()?.ToString(),
                Price = product.Price.ToString(),
                Amount = product.Amount,
                Rating = rating,
                ReviewsCount = reviewsCount,
            };

        public GetMyProductResponse ConvertToGetMyProductResponse() =>
            new()
            {
                Id = product.Id,
                Name = product.Name,
                PreviewUrl = product.ImageUrls.FirstOrDefault()?.ToString(),
                Price = product.Price.ToString(),
                Amount = product.Amount,
                Status = product.Status.ToString(),
                Editable = true,
            };

        public ProductCreateRequestModel ConvertToProductCreateRequest() =>
            new()
            {
                SellerId = product.SellerId,
                Name = product.Name,
                Description = product.Description,
                ImageUrls = product.ImageUrls,
                Price = product.Price,
                Amount = product.Amount,
                Features = product.Features,
                Status = RequestStatuses.Pending,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };

        public ProductUpdateRequestModel ConvertToProductUpdateRequest() =>
            new()
            {
                ProductId = product.Id,
                SellerId = product.SellerId,
                Name = product.Name,
                Description = product.Description,
                ImageUrls = product.ImageUrls,
                Features = product.Features,
                Status = RequestStatuses.Pending,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };
    }

    extension(ProductCreateRequestModel request)
    {
        public GetMyProductResponse ConvertToGetMyProductResponse() =>
            new()
            {
                Id = request.Id,
                Name = request.Name,
                PreviewUrl = request.ImageUrls.FirstOrDefault()?.ToString(),
                Price = request.Price.ToString(),
                Amount = request.Amount,
                Status = "OnModeration",
                Editable = false,
            };
    }

    public static ProductModel ConvertToProductModel(this CreateProductRequest request, Guid sellerId) => new()
    {
        SellerId = sellerId,
        Name = request.Name,
        Description = request.Description,
        ImageUrls = request.ToUris(),
        Price = BigInteger.Parse(request.Price),
        Amount = request.Amount,
        Features = request.Features.ToFrozenDictionary(),
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow,
    };

    public static ProductModel ConvertToProductModel(this UpdateProductRequest request, Guid id, Guid sellerId) => new()
    {
        Id = id,
        SellerId = sellerId,
        Name = request.Name,
        Description = request.Description,
        ImageUrls = request.ImageUrls.Select(url => url.ToUri()).Where(uri => uri is not null).Select(uri => uri!).ToArray(),
        Features = request.Features.ToFrozenDictionary(),
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow,
    };

    private static Uri[] ToUris(this CreateProductRequest request) =>
        request.ImageUrls.Select(url => url.ToUri()).Where(uri => uri is not null).Select(uri => uri!).ToArray();
}