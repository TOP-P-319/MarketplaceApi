using ProductsAPI.Core.Infrastructure.Domain.Mappers;
using ProductsAPI.Modules.Products.Db.Entities;
using ProductsAPI.Modules.Products.Domain.Models;

namespace ProductsAPI.Modules.Products.Domain.Mappers;

public sealed class ProductMapper : MapperBase<ProductModel, ProductEntity>
{
    public override ProductEntity MapToEntity(ProductModel model)
    {
        var entity = base.MapToEntity(model);
        entity.Name = model.Name;
        return entity;
    }

    public override ProductModel MapFrom(ProductEntity entity)
    {
        var model = base.MapFrom(entity);
        model.Name = entity.Name;
        return model;
    }
}