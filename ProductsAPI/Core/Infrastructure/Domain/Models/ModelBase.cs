namespace ProductsAPI.Core.Infrastructure.Domain.Models;

public abstract class ModelBase
{
    public Guid Id { get; init; }
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; init; } = DateTime.UtcNow;

    public override bool Equals(object? obj) =>
        obj is ModelBase model
        && model.Id == Id;

    public override int GetHashCode() => Id.GetHashCode();
}