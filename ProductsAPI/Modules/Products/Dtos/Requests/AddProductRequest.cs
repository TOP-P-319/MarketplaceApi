using System.ComponentModel.DataAnnotations;
using ProductsAPI.Core;
using ProductsAPI.Modules.Products.Domain.Models;

namespace ProductsAPI.Modules.Products.Dtos.Requests;

public sealed record AddProductRequest
{
    [Required]
    [StringLength(Limits.Product.Name.MaxLength)]
    public required string Name { get; init; }
    
    public ProductModel ToProductModel() =>
        new()
        {
            Name = Name
        };
}