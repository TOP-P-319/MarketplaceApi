using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;
using Shared.Constants;
using Shared.Infrastructure;
using Shared.Users;

namespace Shared.Products;

[Table("products")]
public sealed class ProductEntity : Entity<ProductEntity>
{
    [Column("seller_id")] [Required] public Guid SellerId { get; set; }
    [ForeignKey(nameof(SellerId))] public UserEntity? Seller { get; set; }

    [Column("name")]
    [MaxLength(Limits.Product.Name.MaxLength)]
    [Required]
    public string Name { get; set; } = string.Empty;

    [Column("description")]
    [MaxLength(Limits.Product.Description.MaxLength)]
    public string? Description { get; set; }

    [Column("image_urls")] [Required] public string[] ImageUrls { get; set; } = [];

    [Column("features", TypeName = "jsonb")]
    [Required]
    public Dictionary<string, string> Features { get; set; } = [];

    [Column("price")] [Required] public BigInteger Price { get; set; }
    [Column("amount")] [Required] public int Amount { get; set; }

    [Column("status", TypeName = "text")]
    [Required]
    public ProductStatuses Status { get; set; }

    public override void Update(ProductEntity other)
    {
        base.Update(other);
        SellerId = other.SellerId;
        Name = other.Name;
        Description = other.Description;
        ImageUrls = other.ImageUrls;
        Features = other.Features;
        Price = other.Price;
        Amount = other.Amount;
        Status = other.Status;
    }
}