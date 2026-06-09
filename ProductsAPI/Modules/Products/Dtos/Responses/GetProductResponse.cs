using System.ComponentModel.DataAnnotations;
using ProductsAPI.Modules.Products.Domain.Models;

namespace ProductsAPI.Modules.Products.Dtos.Responses;

public sealed record GetProductResponse
{
    [Required] public required Guid Id { get; init; }
    [Required] public required string Name { get; init; }

    public static GetProductResponse CreateFrom(ProductModel product) => new()
    {
        Id = product.Id,
        Name = product.Name
    };
}