namespace PurchasesAPI.Purchases;

public sealed record GetPurchaseHistoryResponse
{
    public required Guid Id { get; init; }
    public required Guid ProductId { get; init; }
    public required string ProductName { get; init; }
    public required string PricePaid { get; init; }
    public required DateTime CreatedAt { get; init; }
}
