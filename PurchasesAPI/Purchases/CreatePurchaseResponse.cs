namespace PurchasesAPI.Purchases;

public sealed record CreatePurchaseResponse
{
    public required Guid PurchaseId { get; init; }
}