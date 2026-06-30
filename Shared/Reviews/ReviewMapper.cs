using Shared.Infrastructure;

namespace Shared.Reviews;

public sealed class ReviewMapper : Mapper<ReviewModel, ReviewEntity>
{
    public override ReviewEntity MapToEntity(ReviewModel model)
    {
        var entity = base.MapToEntity(model);
        entity.AuthorId = model.AuthorId;
        entity.ProductId = model.ProductId;
        entity.Rating = model.Rating;
        entity.Text = model.Text;
        return entity;
    }

    public override ReviewModel MapToModel(ReviewEntity entity) =>
        base.MapToModel(entity) with
        {
            AuthorId = entity.AuthorId,
            ProductId = entity.ProductId,
            Rating = entity.Rating,
            Text = entity.Text,
        };
}
