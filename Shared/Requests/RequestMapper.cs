using System.Collections.Frozen;
using System.Numerics;
using System.Text.Json;
using Shared.Infrastructure;
using Shared.Utils;

namespace Shared.Requests;

public static class RequestPayloadFields
{
    public const string Name = "Name";
    public const string ProductId = "ProductId";
    public const string PhoneNumber = "PhoneNumber";
    public const string PasswordHash = "PasswordHash";
    public const string SellerId = "SellerId";
    public const string Description = "Description";
    public const string ImageUrls = "ImageUrls";
    public const string Price = "Price";
    public const string Amount = "Amount";
    public const string Features = "Features";
    public const string AuthorId = "AuthorId";
    public const string Rating = "Rating";
    public const string Text = "Text";
}

public abstract class RequestMapper<TModel> : Mapper<TModel, RequestEntity>
    where TModel : RequestModel, new()
{
    public override RequestEntity MapToEntity(TModel model)
    {
        var entity = base.MapToEntity(model);
        entity.Type = model.Type;
        entity.Status = model.Status;
        return entity;
    }

    public override TModel MapToModel(RequestEntity entity) =>
        base.MapToModel(entity) with
        {
            Type = entity.Type,
            Status = entity.Status,
        };
}

public sealed class SellerRegisterRequestMapper : RequestMapper<SellerRegisterRequestModel>
{
    public override RequestEntity MapToEntity(SellerRegisterRequestModel model)
    {
        var entity = base.MapToEntity(model);
        entity.Payload =
            new Dictionary<string, string?>
            {
                [RequestPayloadFields.Name] = model.Name,
                [RequestPayloadFields.PhoneNumber] = model.PhoneNumber,
                [RequestPayloadFields.PasswordHash] = model.PasswordHash,
            };

        return entity;
    }

    public override SellerRegisterRequestModel MapToModel(RequestEntity entity)
    {
        return base.MapToModel(entity) with
        {
            Name = entity.Payload[RequestPayloadFields.Name]!,
            PhoneNumber = entity.Payload[RequestPayloadFields.PhoneNumber]!,
            PasswordHash = entity.Payload[RequestPayloadFields.PasswordHash]!,
        };
    }
}

public sealed class ProductCreateRequestMapper : RequestMapper<ProductCreateRequestModel>
{
    public override RequestEntity MapToEntity(ProductCreateRequestModel model)
    {
        var entity = base.MapToEntity(model);
        entity.Payload =
            new Dictionary<string, string?>
            {
                [RequestPayloadFields.SellerId] = model.SellerId.ToString(),
                [RequestPayloadFields.Name] = model.Name,
                [RequestPayloadFields.Description] = model.Description,
                [RequestPayloadFields.ImageUrls] =
                    JsonSerializer.Serialize(model.ImageUrls.Select(url => url.ToString())),
                [RequestPayloadFields.Price] = model.Price.ToString(),
                [RequestPayloadFields.Amount] = model.Amount.ToString(),
                [RequestPayloadFields.Features] = JsonSerializer.Serialize(model.Features),
            };

        return entity;
    }

    public override ProductCreateRequestModel MapToModel(RequestEntity entity)
    {
        return base.MapToModel(entity) with
        {
            SellerId = Guid.Parse(entity.Payload[RequestPayloadFields.SellerId]!),
            Name = entity.Payload[RequestPayloadFields.Name]!,
            Description = entity.Payload[RequestPayloadFields.Description],
            ImageUrls = JsonSerializer.Deserialize<string[]>(entity.Payload[RequestPayloadFields.ImageUrls]!)!
                .Select(str => str.ToUri()!).ToArray(),
            Price = BigInteger.Parse(entity.Payload[RequestPayloadFields.Price]!),
            Amount = int.Parse(entity.Payload[RequestPayloadFields.Amount]!),
            Features =
            JsonSerializer.Deserialize<Dictionary<string, string>>(entity.Payload[RequestPayloadFields.Features]!)!
                .ToFrozenDictionary()
        };
    }
}

public sealed class ProductUpdateRequestMapper : RequestMapper<ProductUpdateRequestModel>
{
    public override RequestEntity MapToEntity(ProductUpdateRequestModel model)
    {
        var entity = base.MapToEntity(model);
        entity.Payload =
            new Dictionary<string, string?>
            {
                [RequestPayloadFields.ProductId] = model.ProductId.ToString(),
                [RequestPayloadFields.SellerId] = model.SellerId.ToString(),
                [RequestPayloadFields.Name] = model.Name,
                [RequestPayloadFields.Description] = model.Description,
                [RequestPayloadFields.ImageUrls] =
                    JsonSerializer.Serialize(model.ImageUrls.Select(url => url.ToString())),
                [RequestPayloadFields.Features] = JsonSerializer.Serialize(model.Features),
            };

        return entity;
    }

    public override ProductUpdateRequestModel MapToModel(RequestEntity entity)
    {
        return base.MapToModel(entity) with
        {
            ProductId = Guid.Parse(entity.Payload[RequestPayloadFields.ProductId]!),
            SellerId = Guid.Parse(entity.Payload[RequestPayloadFields.SellerId]!),
            Name = entity.Payload[RequestPayloadFields.Name]!,
            Description = entity.Payload[RequestPayloadFields.Description],
            ImageUrls = JsonSerializer.Deserialize<string[]>(entity.Payload[RequestPayloadFields.ImageUrls]!)!
                .Select(str => str.ToUri()!).ToArray(),
            Features =
            JsonSerializer.Deserialize<Dictionary<string, string>>(entity.Payload[RequestPayloadFields.Features]!)!
                .ToFrozenDictionary()
        };
    }
}

public sealed class ReviewCreateRequestMapper : RequestMapper<ReviewCreateRequestModel>
{
    public override RequestEntity MapToEntity(ReviewCreateRequestModel model)
    {
        var entity = base.MapToEntity(model);
        entity.Payload =
            new Dictionary<string, string?>
            {
                [RequestPayloadFields.AuthorId] = model.AuthorId.ToString(),
                [RequestPayloadFields.ProductId] = model.ProductId.ToString(),
                [RequestPayloadFields.Rating] = model.Rating.ToString(),
                [RequestPayloadFields.Text] = model.Text,
            };

        return entity;
    }

    public override ReviewCreateRequestModel MapToModel(RequestEntity entity)
    {
        return base.MapToModel(entity) with
        {
            AuthorId = Guid.Parse(entity.Payload[RequestPayloadFields.AuthorId]!),
            ProductId = Guid.Parse(entity.Payload[RequestPayloadFields.ProductId]!),
            Rating = int.Parse(entity.Payload[RequestPayloadFields.Rating]!),
            Text = entity.Payload[RequestPayloadFields.Text]!,
        };
    }
}