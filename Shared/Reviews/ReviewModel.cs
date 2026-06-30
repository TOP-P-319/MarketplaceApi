using Shared.Infrastructure;

namespace Shared.Reviews;

public sealed record ReviewModel : Model
{
    public Guid AuthorId { get; init; }
    public Guid ProductId { get; init; }
    public int Rating { get; init; }
    public string Text { get; init; } = string.Empty;
}
