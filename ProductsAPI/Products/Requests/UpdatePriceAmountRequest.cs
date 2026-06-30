using System.ComponentModel.DataAnnotations;

namespace ProductsAPI.Products.Requests;

public sealed record UpdatePriceAmountRequest
{
    [Required]
    public required string Price { get; init; }

    [Range(0, int.MaxValue)]
    public required int Amount { get; init; }
}
