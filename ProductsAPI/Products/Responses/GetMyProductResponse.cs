namespace ProductsAPI.Products.Responses;

public sealed record GetMyProductResponse
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public string? PreviewUrl { get; init; }
    public required string Price { get; init; }
    public required int Amount { get; init; }

    /// <summary>Published | Hidden | OnModeration</summary>
    public required string Status { get; init; }

    /// <summary>True for real products (inline edit / hide / publish), false for pending requests.</summary>
    public required bool Editable { get; init; }
}
