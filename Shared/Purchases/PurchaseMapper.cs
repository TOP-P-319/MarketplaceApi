using Shared.Infrastructure;

namespace Shared.Purchases;

public sealed class PurchaseMapper : Mapper<PurchaseModel, PurchaseEntity>
{
    public override PurchaseEntity MapToEntity(PurchaseModel model)
    {
        var entity = base.MapToEntity(model);
        entity.BuyerId = model.BuyerId;
        entity.SellerId = model.SellerId;
        entity.ProductName = model.ProductName;
        entity.PricePaid = model.PricePaid;
        return entity;
    }

    public override PurchaseModel MapToModel(PurchaseEntity entity) =>
        base.MapToModel(entity) with
        {
            BuyerId = entity.BuyerId,
            SellerId = entity.SellerId,
            ProductName = entity.ProductName,
            PricePaid = entity.PricePaid
        };
}