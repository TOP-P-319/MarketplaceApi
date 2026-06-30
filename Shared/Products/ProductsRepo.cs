using Shared.Infrastructure;

namespace Shared.Products;

public sealed class ProductsRepo(
    AppDbContext ctx,
    ProductMapper mapper
) : Repo<ProductModel, ProductEntity>(ctx, ctx.Products, mapper)
{
    public async Task<IEnumerable<ProductModel>> FindAllPublishedAsync() =>
        await FindAllByAsync(e => e.Status == ProductStatuses.Published);

    public async Task<IEnumerable<ProductModel>> FindAllBySellerAsync(Guid sellerId) =>
        await FindAllByAsync(e => e.SellerId == sellerId);
}