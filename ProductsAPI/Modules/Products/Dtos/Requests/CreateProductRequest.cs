using System.ComponentModel.DataAnnotations;
using ProductsAPI.Core.Constants;
using ProductsAPI.Modules.Products.Domain.Models;

namespace ProductsAPI.Modules.Products.Dtos.Requests;

public sealed record CreateProductRequest
{
    [Required]
    [MaxLength(Limits.Product.Name.MaxLength)]
    public required string Name { get; init; }

    public ProductModel ConvertToProductModel() => new() 
    {
        Name = Name
    };
}