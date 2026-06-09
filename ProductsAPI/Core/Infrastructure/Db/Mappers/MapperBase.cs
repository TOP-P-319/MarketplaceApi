using ProductsAPI.Core.Infrastructure.Db.Entities;
using ProductsAPI.Core.Infrastructure.Domain.Models;

namespace ProductsAPI.Core.Infrastructure.Db.Mappers;

public abstract class MapperBase<TModel, TEntity> : IMapper<TModel, TEntity>
    where TModel : ModelBase, new()
    where TEntity : EntityBase
{
    public virtual TModel Map(TEntity entity) =>
        new()
        {
            Id = entity.Id,
        };

    public abstract TEntity Map(TModel model);
}