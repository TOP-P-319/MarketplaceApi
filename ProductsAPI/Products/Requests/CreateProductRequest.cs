using System.ComponentModel.DataAnnotations;
using Shared.Constants;

namespace ProductsAPI.Products.Requests;

public sealed record CreateProductRequest
{
    [Required]
    [MaxLength(Limits.Product.Name.MaxLength)]
    public required string Name { get; init; }

    [MaxLength(Limits.Product.Description.MaxLength)]
    public string? Description { get; init; }

    public string[] ImageUrls { get; init; } = [];

    [Required]
    public required string Price { get; init; }

    [Range(0, int.MaxValue)]
    public required int Amount { get; init; }

    public Dictionary<string, string> Features { get; init; } = new();
}
