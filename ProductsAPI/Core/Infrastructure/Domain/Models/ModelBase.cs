namespace ProductsAPI.Core.Infrastructure.Domain.Models;

public abstract record ModelBase
{
    public Guid Id { get; init; } = Guid.CreateVersion7();
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; init; } = DateTime.UtcNow;

    protected TSelf Touch<TSelf>() where TSelf : ModelBase =>
        (TSelf)this with
        {
            UpdatedAt = DateTime.UtcNow
        };
}