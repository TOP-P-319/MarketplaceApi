using ProductsAPI.Core.Infrastructure.Domain.Models;

namespace ProductsAPI.Modules.Products.Domain.Models;

public sealed record ProductModel : ModelBase
{
    public string Name { get; init; } = string.Empty;

    public ProductModel WithUpdatedName(string name) =>
        Touch<ProductModel>() with
        {
            Name = name
        };
}