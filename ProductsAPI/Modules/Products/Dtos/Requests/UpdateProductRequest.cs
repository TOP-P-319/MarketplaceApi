using System.ComponentModel.DataAnnotations;
using ProductsAPI.Core.Constants;

namespace ProductsAPI.Modules.Products.Dtos.Requests;

public sealed record UpdateProductRequest
{
    [Required]
    [MaxLength(Limits.Product.Name.MaxLength)]
    public required string Name { get; init; }
}