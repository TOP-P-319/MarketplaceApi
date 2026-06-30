using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;
using Shared.Constants;
using Shared.Infrastructure;
using Shared.Users;

namespace Shared.Purchases;

[Table("purchases")]
public sealed class PurchaseEntity : Entity<PurchaseEntity>
{
    [Column("buyer_id")] [Required] public Guid BuyerId { get; set; }
    [ForeignKey(nameof(BuyerId))] public UserEntity? Buyer { get; set; }
    [Column("seller_id")] [Required] public Guid SellerId { get; set; }
    [ForeignKey(nameof(SellerId))] public UserEntity? Seller { get; set; }

    [Column("product_name")]
    [StringLength(Limits.Product.Name.MaxLength)]
    [Required]
    public string ProductName { get; set; } = string.Empty;

    [Column("price_paid")] [Required] public BigInteger PricePaid { get; set; }
}