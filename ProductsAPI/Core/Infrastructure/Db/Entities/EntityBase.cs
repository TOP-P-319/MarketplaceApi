namespace ProductsAPI.Core.Infrastructure.Db.Entities;

public abstract class EntityBase
{
    public Guid Id { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
}

public abstract class EntityBase<TSelf> : EntityBase
    where TSelf : EntityBase<TSelf>
{
    public abstract void Update(TSelf entity);
}