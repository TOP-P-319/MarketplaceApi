using ProductsAPI.Core.Infrastructure.Domain.Models;

namespace ProductsAPI.Core.Infrastructure.Db.Repos;

public interface IRepoBase<TModel> where TModel : ModelBase
{
    Task<TModel?> FindByIdAsync(Guid id);
    Task<IEnumerable<TModel>> FindAllAsync();
    
    Task AddAsync(TModel model);
    Task UpdateAsync(TModel model);
    
    Task DeleteByIdAsync(Guid id);
}