
using ProductsAPI.Core.Infrastructure.Db.Entities;
using ProductsAPI.Core.Infrastructure.Domain.Models;

namespace ProductsAPI.Core.Infrastructure.Db.Mappers;

public interface IMapper<TModel, TEntity>
    where TModel : ModelBase
    where TEntity : EntityBase
{
    TModel MapToModel(TEntity entity);
    TEntity MapToEntity(TModel model);
}