namespace ProductsAPI.Reviews.Responses;

public sealed record GetMyReviewResponse
{
    public required Guid Id { get; init; }
    public required string ProductName { get; init; }
    public required int Rating { get; init; }
    public required string Text { get; init; }
    public required DateTime CreatedAt { get; init; }

    /// <summary>Published | OnModeration</summary>
    public required string Status { get; init; }
}
