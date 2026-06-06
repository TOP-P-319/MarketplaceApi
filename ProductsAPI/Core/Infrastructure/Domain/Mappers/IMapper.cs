using ProductsAPI.Core.Infrastructure.Db.Entities;
using ProductsAPI.Core.Infrastructure.Domain.Models;

namespace ProductsAPI.Core.Infrastructure.Domain.Mappers;

public interface IMapper<TModel, TEntity>
    where TModel : ModelBase, new()
    where TEntity : EntityBase<TEntity>, new()
{
    TEntity MapToEntity(TModel model);
    TModel MapFrom(TEntity entity);
}