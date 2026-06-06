using ProductsAPI.Core.Infrastructure.Domain.Models;

namespace ProductsAPI.Modules.Products.Domain.Models;

public sealed class ProductModel : ModelBase
{
    public string Name { get; init; } = string.Empty;

    public override bool Equals(object? obj) =>
        obj is ProductModel model
        && base.Equals(model);

    public override int GetHashCode() => base.GetHashCode();
}