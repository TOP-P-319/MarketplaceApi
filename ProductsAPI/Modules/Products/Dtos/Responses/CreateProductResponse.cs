namespace ProductsAPI.Modules.Products.Dtos.Responses;

public sealed record CreateProductResponse
{
    public required Guid Id { get; init; }
    public string? PreviewUrl { get; init; }
    public required DateTime CreatedAt { get; init; }
}