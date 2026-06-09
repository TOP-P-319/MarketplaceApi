namespace ProductsAPI.Core.Infrastructure.Db.Exceptions;

public abstract class EntityNotFoundException<TEntity>(string cause)
    : KeyNotFoundException(
        $"Entity of type={nameof(TEntity)} was not found in the database in cause: {cause}."
    );
    
public sealed class EntityNotFoundByIdException<TEntity>(Guid id)
    : EntityNotFoundException<TEntity>($"Id={id} was not found");