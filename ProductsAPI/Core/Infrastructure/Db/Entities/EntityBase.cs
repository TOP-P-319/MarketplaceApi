namespace ProductsAPI.Core.Infrastructure.Db.Entities;

public abstract class EntityBase
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

public abstract class EntityBase<TSelf> : EntityBase where TSelf : EntityBase<TSelf>
{
    public abstract void Update(TSelf other);
}