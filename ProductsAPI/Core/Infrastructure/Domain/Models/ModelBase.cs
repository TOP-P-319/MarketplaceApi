namespace ProductsAPI.Core.Infrastructure.Domain.Models;

public abstract class ModelBase
{
    public int Id { get; init; }

    public override bool Equals(object? obj) =>
        obj is ModelBase model
        && model.Id == Id;

    public override int GetHashCode() => Id;
}