using ProductsAPI.Core.Infrastructure.Db.Mappers;
using ProductsAPI.Modules.Products.Db.Entities;
using ProductsAPI.Modules.Products.Domain.Models;

namespace ProductsAPI.Modules.Products.Db.Mappers;

public sealed class ProductMapper : MapperBase<ProductModel, ProductEntity>
{
    public override ProductEntity MapToEntity(ProductModel model)
    {
        var entity = base.MapToEntity(model);
        entity.Name = model.Name;
        entity.PreviewUrl = model.PreviewUrl?.ToString();
        return entity;
    }

    public override ProductModel MapToModel(ProductEntity entity) =>
        base.MapToModel(entity) with
        {
            Name = entity.Name,
            PreviewUrl = Uri.TryCreate(entity.PreviewUrl, UriKind.Absolute, out var previewUrl)
                ? previewUrl
                : null
        };
}