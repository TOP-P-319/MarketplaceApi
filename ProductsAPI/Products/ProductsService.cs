using System.Numerics;
using ProductsAPI.Products.Requests;
using ProductsAPI.Products.Responses;
using Shared.Products;
using Shared.Requests;
using Shared.Reviews;
using Shared.Users;

namespace ProductsAPI.Products;

public sealed class ProductsService(
    ProductsRepo productsRepo,
    ProductCreateRequestsRepo productCreateRequestsRepo,
    ProductUpdateRequestsRepo productUpdateRequestsRepo,
    ReviewsRepo reviewsRepo,
    UsersRepo usersRepo)
{
    public async Task<GetProductResponse?> GetProductAsync(Guid id)
    {
        var product = await productsRepo.FindByIdAsync(id);
        if (product == null) return null;

        var seller = await usersRepo.FindByIdAsync(product.SellerId);
        var reviews = (await reviewsRepo.FindAllByProductAsync(id)).ToArray();
        var rating = reviews.Length == 0 ? 0 : reviews.Average(review => review.Rating);

        return product.ConvertToGetProductResponse(seller?.Name ?? "—", rating, reviews.Length);
    }

    public async Task<IEnumerable<GetProductPreviewResponse>> GetAllProductPreviewsAsync()
    {
        var products = await productsRepo.FindAllPublishedAsync();
        var stats = (await reviewsRepo.FindAllAsync())
            .GroupBy(review => review.ProductId)
            .ToDictionary(group => group.Key, group => (Rating: group.Average(r => r.Rating), Count: group.Count()));

        return products.Select(product =>
        {
            stats.TryGetValue(product.Id, out var stat);
            return product.ConvertToGetProductPreviewResponse(stat.Rating, stat.Count);
        });
    }

    public async Task RemoveProductAsync(Guid id) => await productsRepo.DeleteByIdAsync(id);

    public async Task CreateProductCreateRequestAsync(CreateProductRequest request, Guid sellerId)
    {
        var product = request.ConvertToProductModel(sellerId);
        var productCreateRequest = product.ConvertToProductCreateRequest();
        await productCreateRequestsRepo.AddAsync(productCreateRequest);
    }

    public async Task CreateProductUpdateRequestAsync(Guid id, UpdateProductRequest request, Guid sellerId)
    {
        var product = request.ConvertToProductModel(id, sellerId);
        var productUpdateRequest = product.ConvertToProductUpdateRequest();
        await productUpdateRequestsRepo.AddAsync(productUpdateRequest);
    }

    public async Task<IEnumerable<GetMyProductResponse>> GetSellerProductsAsync(Guid sellerId)
    {
        var products = (await productsRepo.FindAllBySellerAsync(sellerId))
            .Select(product => product.ConvertToGetMyProductResponse());

        var pending = (await productCreateRequestsRepo.FindAllByStatusAsync(RequestStatuses.Pending))
            .Where(request => request.SellerId == sellerId)
            .Select(request => request.ConvertToGetMyProductResponse());

        return products.Concat(pending);
    }

    public async Task SetProductStatusAsync(Guid id, Guid sellerId, ProductStatuses status)
    {
        var product = await productsRepo.FindByIdAsync(id) ?? throw new KeyNotFoundException();
        if (product.SellerId != sellerId) throw new UnauthorizedAccessException();
        await productsRepo.UpdateAsync(product.WithStatus(status));
    }

    public async Task UpdatePriceAndAmountAsync(Guid id, Guid sellerId, UpdatePriceAmountRequest request)
    {
        var product = await productsRepo.FindByIdAsync(id) ?? throw new KeyNotFoundException();
        if (product.SellerId != sellerId) throw new UnauthorizedAccessException();
        await productsRepo.UpdateAsync(product.WithPriceAndAmount(BigInteger.Parse(request.Price), request.Amount));
    }
}