namespace ProductsAPI.Products.Responses;

public sealed record GetProductPreviewResponse
{
    public required Guid SellerId { get; init; }
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public string? PreviewUrl { get; init; }
    public required string Price { get; init; }
    public required int Amount { get; init; }
    public required double Rating { get; init; }
    public required int ReviewsCount { get; init; }
}