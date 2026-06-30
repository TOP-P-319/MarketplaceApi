namespace PurchasesAPI.Purchases;

public sealed record GetPurchaseResponse
{
    public required Guid BuyerId { get; init; }
    public required Guid SellerId { get; init; }
    public required string ProductName { get; init; }
    public required string PricePaid { get; init; }
}