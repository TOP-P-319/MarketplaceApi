using ProductsAPI.Reviews.Requests;
using ProductsAPI.Reviews.Responses;
using Shared.Products;
using Shared.Requests;
using Shared.Reviews;
using Shared.Users;

namespace ProductsAPI.Reviews;

public sealed class ReviewsService(
    ReviewsRepo reviewsRepo,
    ReviewCreateRequestsRepo reviewCreateRequestsRepo,
    UsersRepo usersRepo,
    ProductsRepo productsRepo)
{
    public async Task<IEnumerable<GetReviewResponse>> GetProductReviewsAsync(Guid productId)
    {
        var reviews = await reviewsRepo.FindAllByProductAsync(productId);

        var result = new List<GetReviewResponse>();
        foreach (var review in reviews)
        {
            var author = await usersRepo.FindByIdAsync(review.AuthorId);
            result.Add(review.ConvertToGetReviewResponse(author?.Name ?? "—"));
        }

        return result;
    }

    public async Task SubmitReviewAsync(CreateReviewRequest request, Guid authorId)
    {
        var product = await productsRepo.FindByIdAsync(request.ProductId)
                      ?? throw new KeyNotFoundException("Product not found");

        var reviewRequest = new ReviewCreateRequestModel
        {
            AuthorId = authorId,
            ProductId = product.Id,
            Rating = request.Rating,
            Text = request.Text,
            Status = RequestStatuses.Pending,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
        };

        await reviewCreateRequestsRepo.AddAsync(reviewRequest);
    }

    public async Task<IEnumerable<GetMyReviewResponse>> GetMyReviewsAsync(Guid authorId)
    {
        var result = new List<GetMyReviewResponse>();

        foreach (var review in await reviewsRepo.FindAllByAuthorAsync(authorId))
        {
            var product = await productsRepo.FindByIdAsync(review.ProductId);
            result.Add(review.ConvertToGetMyReviewResponse(product?.Name ?? "—"));
        }

        var pending = (await reviewCreateRequestsRepo.FindAllByStatusAsync(RequestStatuses.Pending))
            .Where(request => request.AuthorId == authorId);
        foreach (var request in pending)
        {
            var product = await productsRepo.FindByIdAsync(request.ProductId);
            result.Add(request.ConvertToGetMyReviewResponse(product?.Name ?? "—"));
        }

        return result;
    }
}
