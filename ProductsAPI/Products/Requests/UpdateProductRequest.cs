using System.ComponentModel.DataAnnotations;
using Shared.Constants;

namespace ProductsAPI.Products.Requests;

public sealed record UpdateProductRequest
{
    [Required]
    [MaxLength(Limits.Product.Name.MaxLength)]
    public required string Name { get; init; }
}