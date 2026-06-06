using System.ComponentModel.DataAnnotations;
using ProductsAPI.Modules.Products.Domain.Models;

namespace ProductsAPI.Modules.Products.Dtos.Responses;

public sealed class GetProductResponse
{
    [Required] public int Id { get; private init; }
    [Required] public string Name { get; private init; } = string.Empty;

    public static GetProductResponse CreateFrom(ProductModel product) => new()
    {
        Id = product.Id,
        Name = product.Name
    };
}