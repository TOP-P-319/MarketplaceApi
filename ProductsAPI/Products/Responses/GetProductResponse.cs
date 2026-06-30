using System.Collections.Frozen;

namespace ProductsAPI.Products.Responses;

public sealed record GetProductResponse
{
    public required Guid SellerId { get; init; }
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public string? Description { get; init; }
    public required string[] ImageUrls { get; init; }
    public required string Price { get; init; }
    public required int Amount { get; init; }
    public required FrozenDictionary<string, string> Features { get; init; }
    public required DateTime CreatedAt { get; init; }
    public required DateTime UpdatedAt { get; init; }
}