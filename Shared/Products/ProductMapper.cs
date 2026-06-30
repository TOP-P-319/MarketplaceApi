using System.Collections.Frozen;
using Shared.Infrastructure;
using Shared.Utils;

namespace Shared.Products;

public sealed class ProductMapper : Mapper<ProductModel, ProductEntity>
{
    public override ProductEntity MapToEntity(ProductModel model)
    {
        var entity = base.MapToEntity(model);
        entity.Name = model.Name;
        entity.Description = model.Description;
        entity.ImageUrls = model.ImageUrls.Select(url => url.ToString()).ToArray();
        entity.Features = model.Features.ToDictionary();
        entity.Amount = model.Amount;
        entity.SellerId = model.SellerId;
        entity.Price = model.Price;
        return entity;
    }

    public override ProductModel MapToModel(ProductEntity entity) =>
        base.MapToModel(entity) with
        {
            Name = entity.Name,
            Description = entity.Description,
            ImageUrls = entity.ImageUrls.Select(str => str.ToUri()!).ToArray(),
            Features = entity.Features.ToFrozenDictionary(),
            Amount = entity.Amount,
            SellerId = entity.SellerId,
            Price = entity.Price
        };
}