using ProductsAPI.Core.Infrastructure.Db.Entities;
using ProductsAPI.Core.Infrastructure.Domain.Models;

namespace ProductsAPI.Core.Infrastructure.Domain.Mappers;

public abstract class MapperBase<TModel, TEntity> : IMapper<TModel, TEntity>
    where TModel : ModelBase, new()
    where TEntity : EntityBase<TEntity>, new()
{
    public virtual TEntity MapToEntity(TModel model) =>
        new()
        {
            Id = model.Id,
            CreatedAt = model.CreatedAt,
            UpdatedAt = model.UpdatedAt,
        };

    public virtual TModel MapFrom(TEntity entity) =>
        new()
        {
            Id = entity.Id,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt,
        };
}