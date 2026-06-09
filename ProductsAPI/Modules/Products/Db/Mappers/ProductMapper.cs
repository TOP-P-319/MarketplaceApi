using ProductsAPI.Core.Infrastructure.Db.Mappers;
using ProductsAPI.Modules.Products.Db.Entities;
using ProductsAPI.Modules.Products.Domain.Models;

namespace ProductsAPI.Modules.Products.Db.Mappers;

public sealed class ProductMapper : MapperBase<ProductModel, ProductEntity>
{
    public override ProductEntity Map(ProductModel model) =>
        new()
        {
            Name = model.Name,
        };

    public override ProductModel Map(ProductEntity entity)
    {
        var model = base.Map(entity);
        model.Name = entity.Name;
        return model;
    }
}