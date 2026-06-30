using ProductsAPI.Reviews.Responses;
using Shared.Requests;
using Shared.Reviews;

namespace ProductsAPI.Reviews;

public static class ReviewConverter
{
    extension(ReviewModel review)
    {
        public GetReviewResponse ConvertToGetReviewResponse(string authorName) =>
            new()
            {
                Id = review.Id,
                AuthorName = authorName,
                Rating = review.Rating,
                Text = review.Text,
                CreatedAt = review.CreatedAt,
            };

        public GetMyReviewResponse ConvertToGetMyReviewResponse(string productName) =>
            new()
            {
                Id = review.Id,
                ProductName = productName,
                Rating = review.Rating,
                Text = review.Text,
                CreatedAt = review.CreatedAt,
                Status = "Published",
            };
    }

    extension(ReviewCreateRequestModel request)
    {
        public GetMyReviewResponse ConvertToGetMyReviewResponse(string productName) =>
            new()
            {
                Id = request.Id,
                ProductName = productName,
                Rating = request.Rating,
                Text = request.Text,
                CreatedAt = request.CreatedAt,
                Status = "OnModeration",
            };
    }
}
