using Shared.Infrastructure;

namespace Shared.Reviews;

public sealed class ReviewsRepo(AppDbContext ctx, ReviewMapper mapper)
    : Repo<ReviewModel, ReviewEntity>(ctx, ctx.Reviews, mapper)
{
    public async Task<IEnumerable<ReviewModel>> FindAllByProductAsync(Guid productId) =>
        await FindAllByAsync(e => e.ProductId == productId);

    public async Task<IEnumerable<ReviewModel>> FindAllByAuthorAsync(Guid authorId) =>
        await FindAllByAsync(e => e.AuthorId == authorId);
}
