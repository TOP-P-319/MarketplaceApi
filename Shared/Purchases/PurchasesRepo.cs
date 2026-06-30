using Shared.Infrastructure;

namespace Shared.Purchases;

public sealed class PurchasesRepo(AppDbContext ctx, PurchaseMapper mapper)
    : Repo<PurchaseModel, PurchaseEntity>(ctx, ctx.Purchases, mapper)
{
    public async Task<IEnumerable<PurchaseModel>> FindAllByBuyerAsync(Guid buyerId) =>
        await FindAllByAsync(e => e.BuyerId == buyerId);

    public async Task<IEnumerable<PurchaseModel>> FindAllBySellerAsync(Guid sellerId) =>
        await FindAllByAsync(e => e.SellerId == sellerId);
}