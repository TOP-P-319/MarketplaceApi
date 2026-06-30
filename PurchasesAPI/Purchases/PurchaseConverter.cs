using Shared.Purchases;

namespace PurchasesAPI.Purchases;

public static class PurchaseConverter
{
    public static CreatePurchaseResponse ConvertToCreatePurchaseResponse(this PurchaseModel purchase) =>
        new()
        {
            PurchaseId = purchase.Id,
        };

    public static GetPurchaseResponse ConvertToGetPurchaseResponse(this PurchaseModel purchase) =>
        new()
        {
            BuyerId = purchase.BuyerId,
            SellerId = purchase.SellerId,
            ProductName = purchase.ProductName,
            PricePaid = purchase.PricePaid.ToString()
        };
}