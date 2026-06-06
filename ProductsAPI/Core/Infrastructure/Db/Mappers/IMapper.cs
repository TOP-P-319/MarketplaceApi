using ProductsAPI.Core.Infrastructure.Db.Entities;
using ProductsAPI.Core.Infrastructure.Domain.Models;

namespace ProductsAPI.Core.Infrastructure.Db.Mappers;

public interface IMapper<TModel, TEntity>
    where TModel : ModelBase
    where TEntity : EntityBase<TEntity>, new()
{
    TEntity MapToEntity(TModel model);
    TModel MapToModel(TEntity entity);
}