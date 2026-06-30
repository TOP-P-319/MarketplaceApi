using System.ComponentModel.DataAnnotations;
using Shared.Constants;

namespace ProductsAPI.Products.Requests;

public sealed record UpdateProductRequest
{
    [Required]
    [MaxLength(Limits.Product.Name.MaxLength)]
    public required string Name { get; init; }

    [MaxLength(Limits.Product.Description.MaxLength)]
    public string? Description { get; init; }

    public string[] ImageUrls { get; init; } = [];

    public Dictionary<string, string> Features { get; init; } = new();
}
