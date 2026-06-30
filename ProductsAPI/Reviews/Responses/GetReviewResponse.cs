namespace ProductsAPI.Reviews.Responses;

public sealed record GetReviewResponse
{
    public required Guid Id { get; init; }
    public required string AuthorName { get; init; }
    public required int Rating { get; init; }
    public required string Text { get; init; }
    public required DateTime CreatedAt { get; init; }
}
