using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ProductsAPI.Core.Infrastructure.Db.Entities;
using ProductsAPI.Core.Infrastructure.Db.Exceptions;
using ProductsAPI.Core.Infrastructure.Domain.Mappers;
using ProductsAPI.Core.Infrastructure.Domain.Models;

namespace ProductsAPI.Core.Infrastructure.Db.Repos;

public abstract class RepoBase<TModel, TEntity>(
    DbContext context,
    IMapper<TModel, TEntity> mapper,
    DbSet<TEntity> table
) : IRepo<TModel>
    where TModel : ModelBase, new()
    where TEntity : EntityBase<TEntity>, new()
{
    public async Task<IEnumerable<TModel>> FindAllAsync() =>
        await table.AsNoTracking()
            .Select(entity => mapper.MapFrom(entity))
            .ToHashSetAsync();

    public async Task<TModel?> FindByIdAsync(Guid id)
    {
        var entity = await table.FindAsync(id);
        return entity == null ? null : mapper.MapFrom(entity);
    }

    public async Task AddAsync(TModel model)
    {
        var entity = mapper.MapToEntity(model);
        await table.AddAsync(entity);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(TModel model)
    {
        var entity = await table.FindAsync(model.Id) ??
                     throw new EntityNotFoundByIdException<TEntity>(model.Id);
        entity.Update(mapper.MapToEntity(model));
        await context.SaveChangesAsync();
    }

    public async Task RemoveByIdAsync(Guid id)
    {
        var entity = await table.FindAsync(id) ??
                     throw new EntityNotFoundByIdException<TEntity>(id);
        table.Remove(entity);
        await context.SaveChangesAsync();
    }
    
    protected async Task<TModel?> FindByAsync(Expression<Func<TEntity, bool>> predicate)
    {
        var entity = await table
            .AsNoTracking()
            .FirstOrDefaultAsync(predicate);
        return entity == null ? null : mapper.MapFrom(entity);
    }

    protected async Task<IEnumerable<TModel>> FindAllByAsync(Expression<Func<TEntity, bool>> predicate) =>
        await table
            .AsNoTracking()
            .Where(predicate)
            .Select(entity => mapper.MapFrom(entity))
            .ToHashSetAsync();

    protected async Task UpdateByIdAsync(Guid id, Action<TEntity> update)
    {
        var entity = await table.FindAsync(id) ??
                     throw new EntityNotFoundByIdException<TEntity>(id);
        update(entity);
        await context.SaveChangesAsync();
    }
}