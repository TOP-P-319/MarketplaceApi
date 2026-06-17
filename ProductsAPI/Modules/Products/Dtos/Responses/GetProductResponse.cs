namespace ProductsAPI.Modules.Products.Dtos.Responses;

public sealed record GetProductResponse
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public string? PreviewUrl { get; init; }
    public required DateTime CreatedAt { get; init; }
    public required DateTime UpdatedAt { get; init; }
}