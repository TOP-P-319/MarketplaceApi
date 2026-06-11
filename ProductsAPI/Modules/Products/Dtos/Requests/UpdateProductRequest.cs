using System.ComponentModel.DataAnnotations;
using ProductsAPI.Core.Constants;
using ProductsAPI.Modules.Products.Domain.Models;

namespace ProductsAPI.Modules.Products.Dtos.Requests;

public sealed record UpdateProductRequest
{
    [Required]
    [MaxLength(Limits.Product.Name.MaxLength)]
    public required string Name { get; init; }

    public ProductModel ConvertToProductModel(Guid id) => new() 
    {
        Id = id,
        Name = Name
    };
}