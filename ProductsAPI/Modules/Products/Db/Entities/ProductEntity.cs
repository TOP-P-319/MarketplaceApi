using ProductsAPI.Core.Infrastructure.Db.Entities;

namespace ProductsAPI.Modules.Products.Db.Entities;

public sealed class ProductEntity : EntityBase<ProductEntity>
{
    public string Name { get; set; } = string.Empty;
    public string? PreviewUrl { get; set; }

    public override void Update(ProductEntity other)
    {
        base.Update(other);
        Name = other.Name;
    }
}