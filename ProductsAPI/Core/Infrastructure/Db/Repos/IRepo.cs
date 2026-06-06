using ProductsAPI.Core.Infrastructure.Domain.Models;

namespace ProductsAPI.Core.Infrastructure.Db.Repos;

public interface IRepo<TModel> where TModel : ModelBase, new()
{
    Task<IEnumerable<TModel>> FindAllAsync();
    Task<TModel?> FindByIdAsync(Guid id);

    Task AddAsync(TModel model);
    Task UpdateAsync(TModel model);

    Task RemoveByIdAsync(Guid id);
}