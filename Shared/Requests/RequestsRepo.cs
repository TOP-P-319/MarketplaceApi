using Shared.Infrastructure;

namespace Shared.Requests;

public abstract class RequestsRepo<TModel>(AppDbContext ctx, RequestMapper<TModel> mapper)
    : Repo<TModel, RequestEntity>(ctx, ctx.Requests, mapper) where TModel : RequestModel, new()
{
    protected abstract RequestTypes Type { get; }

    public async Task<IEnumerable<TModel>> FindAllByStatusAsync(RequestStatuses status)
    {
        var type = Type;
        return await FindAllByAsync(e => e.Type == type && e.Status == status);
    }
}

public sealed class ProductCreateRequestsRepo(AppDbContext ctx, ProductCreateRequestMapper mapper)
    : RequestsRepo<ProductCreateRequestModel>(ctx, mapper)
{
    protected override RequestTypes Type => RequestTypes.ProductCreate;
}

public sealed class ProductUpdateRequestsRepo(AppDbContext ctx, ProductUpdateRequestMapper mapper)
    : RequestsRepo<ProductUpdateRequestModel>(ctx, mapper)
{
    protected override RequestTypes Type => RequestTypes.ProductUpdate;
}

public sealed class SellerRegisterRequestsRepo(AppDbContext ctx, SellerRegisterRequestMapper mapper)
    : RequestsRepo<SellerRegisterRequestModel>(ctx, mapper)
{
    protected override RequestTypes Type => RequestTypes.SellerRegister;
}

public sealed class ReviewCreateRequestsRepo(AppDbContext ctx, ReviewCreateRequestMapper mapper)
    : RequestsRepo<ReviewCreateRequestModel>(ctx, mapper)
{
    protected override RequestTypes Type => RequestTypes.ReviewCreate;
}
