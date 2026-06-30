using System.ComponentModel.DataAnnotations;
using Shared.Constants;

namespace ProductsAPI.Reviews.Requests;

public sealed record CreateReviewRequest
{
    [Required]
    public required Guid ProductId { get; init; }

    [Range(1, 5)]
    public required int Rating { get; init; }

    [Required]
    [MaxLength(Limits.Review.Text.MaxLength)]
    public required string Text { get; init; }
}
