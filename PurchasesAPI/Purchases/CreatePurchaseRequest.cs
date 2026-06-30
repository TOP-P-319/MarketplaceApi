namespace PurchasesAPI.Purchases;

public sealed record CreatePurchaseRequest
{
    public required Guid ProductId { get; init; }
}