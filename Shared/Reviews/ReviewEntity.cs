using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Shared.Infrastructure;

namespace Shared.Reviews;

[Table("reviews")]
public sealed class ReviewEntity : Entity<ReviewEntity>
{
    [Column("author_id")] [Required] public Guid AuthorId { get; set; }
    [Column("product_id")] [Required] public Guid ProductId { get; set; }
    [Column("rating")] [Required] public int Rating { get; set; }
    [Column("text")] [Required] public string Text { get; set; } = string.Empty;

    public override void Update(ReviewEntity other)
    {
        base.Update(other);
        AuthorId = other.AuthorId;
        ProductId = other.ProductId;
        Rating = other.Rating;
        Text = other.Text;
    }
}
