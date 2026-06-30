using System.Collections.Frozen;
using System.Numerics;
using Shared.Infrastructure;

namespace Shared.Requests;

public abstract record RequestModel() : Model
{
    public RequestTypes Type { get; init; } = RequestTypes.Unknown;
    public RequestStatuses Status { get; init; } = RequestStatuses.Unknown;

    protected RequestModel(RequestTypes type) : this()
    {
        Type = type;
    }
}

public sealed record SellerRegisterRequestModel() : RequestModel(RequestTypes.SellerRegister)
{
    public string Name { get; init; } = string.Empty;
    public string PhoneNumber { get; init; } = string.Empty;
    public string PasswordHash { get; init; } = string.Empty;
}

public sealed record ProductCreateRequestModel() : RequestModel(RequestTypes.ProductCreate)
{
    public Guid SellerId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? Description { get; init; }
    public Uri[] ImageUrls { get; init; } = [];
    public BigInteger Price { get; init; }
    public int Amount { get; init; }
    public FrozenDictionary<string, string> Features { get; init; } = FrozenDictionary<string, string>.Empty;
}

public sealed record ProductUpdateRequestModel() : RequestModel(RequestTypes.ProductUpdate)
{
    public Guid ProductId { get; init; }
    public Guid SellerId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? Description { get; init; }
    public Uri[] ImageUrls { get; init; } = [];
    public FrozenDictionary<string, string> Features { get; init; } = FrozenDictionary<string, string>.Empty;
}

public sealed record ReviewCreateRequestModel() : RequestModel(RequestTypes.ReviewCreate)
{
    public Guid AuthorId { get; init; }
    public Guid ProductId { get; init; }
    public int Rating { get; init; }
    public string Text { get; init; } = string.Empty;
}