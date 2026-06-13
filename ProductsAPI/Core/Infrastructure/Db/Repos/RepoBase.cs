using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ProductsAPI.Core.Infrastructure.Db.Entities;
using ProductsAPI.Core.Infrastructure.Db.Mappers;
using ProductsAPI.Core.Infrastructure.Domain.Models;

namespace ProductsAPI.Core.Infrastructure.Db.Repos;

public abstract class RepoBase<TModel, TEntity>(
    DbContext ctx,
    DbSet<TEntity> set,
    IMapper<TModel, TEntity> mapper
) : IRepoBase<TModel>
    where TModel : ModelBase
    where TEntity : EntityBase<TEntity>
{
    public async Task<TModel?> FindByIdAsync(Guid id)
    {
        var entity = await set.FindAsync(id);
        return entity == null ? null : mapper.MapToModel(entity);
    }

    public async Task<IEnumerable<TModel>> FindAllAsync() =>
        await set.AsNoTracking()
            .Select(entity => mapper.MapToModel(entity))
            .ToArrayAsync();

    public async Task AddAsync(TModel model)
    {
        var entity = mapper.MapToEntity(model);
        await set.AddAsync(entity);
        await ctx.SaveChangesAsync();
    }

    public async Task UpdateAsync(TModel model)
    {
        var entity = await set.FindAsync(model.Id) ?? throw new KeyNotFoundException();
        entity.Update(mapper.MapToEntity(model));
        await ctx.SaveChangesAsync();
    }

    public async Task DeleteByIdAsync(Guid id)
    {
        var entity = await set.FindAsync(id) ?? throw new KeyNotFoundException();
        set.Remove(entity);
        await ctx.SaveChangesAsync();
    }

    protected async Task<TModel?> FindByAsync(Expression<Func<TEntity, bool>> predicate)
    {
        var entity = await set.AsNoTracking().FirstOrDefaultAsync(predicate);
        return entity == null ? null : mapper.MapToModel(entity);
    }
    
    protected async Task<IEnumerable<TModel>> FindAllByAsync(Expression<Func<TEntity, bool>> predicate) =>
        await set.AsNoTracking()
            .Where(predicate)
            .Select(entity => mapper.MapToModel(entity))
            .ToArrayAsync();
}