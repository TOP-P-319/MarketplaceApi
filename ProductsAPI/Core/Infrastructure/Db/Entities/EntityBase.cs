namespace ProductsAPI.Core.Infrastructure.Db.Entities;

public abstract class EntityBase
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; } =  DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } =  DateTime.UtcNow;
}

public abstract class EntityBase<TSelf> : EntityBase
    where TSelf : EntityBase<TSelf>
{
    public abstract void Update(TSelf entity);
}