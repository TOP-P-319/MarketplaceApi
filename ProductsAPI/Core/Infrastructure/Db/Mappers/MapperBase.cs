using ProductsAPI.Core.Infrastructure.Db.Entities;
using ProductsAPI.Core.Infrastructure.Domain.Models;

namespace ProductsAPI.Core.Infrastructure.Db.Mappers;

public abstract class MapperBase<TModel, TEntity> : IMapper<TModel, TEntity>
    where TModel : ModelBase, new()
    where TEntity : EntityBase, new()
{
    public virtual TEntity MapToEntity(TModel model) =>
        new()
        {
            Id = model.Id,
            CreatedAt = model.CreatedAt,
            UpdatedAt = model.UpdatedAt,
        };

    public virtual TModel MapToModel(TEntity entity) =>
        new()
        {
            Id = entity.Id,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt,
        };
}