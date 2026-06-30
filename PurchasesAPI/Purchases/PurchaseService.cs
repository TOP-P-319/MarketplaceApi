using Shared.Infrastructure;
using Shared.Products;
using Shared.Purchases;
using Shared.Users;

namespace PurchasesAPI.Purchases;

public sealed class PurchaseService(
    TransactionBuilder transactionBuilder,
    ProductsRepo productsRepo,
    UsersRepo usersRepo,
    PurchasesRepo purchasesRepo)
{
    public async Task<CreatePurchaseResponse> CreatePurchaseAsync(CreatePurchaseRequest request, Guid buyerId)
    {
        await using var transaction = await transactionBuilder.BeginTransactionAsync();
        try
        {
            var product = await productsRepo.FindByIdAndLockAsync(request.ProductId);
            if (product == null) throw new KeyNotFoundException("Product not found");
            if (product.Amount == 0) throw new InvalidOperationException("Already sold");

            var buyer = await usersRepo.FindByIdAndLockAsync(buyerId);
            if (buyer == null) throw new KeyNotFoundException("Buyer not found");
            if (buyer.Balance < product.Price) throw new InvalidOperationException("Not enough balance");

            var seller = await usersRepo.FindByIdAndLockAsync(product.SellerId);
            if (seller == null) throw new KeyNotFoundException("Seller not found");
            if (seller.Id == buyer.Id)  throw new InvalidOperationException("Can't buy from self");
            
            buyer = buyer.WithDecreasedBalance(product.Price);
            seller = seller.WithIncreasedBalance(product.Price);
            product = product.WithDecreasedAmount();

            var purchase = new PurchaseModel
            {
                BuyerId = buyer.Id,
                SellerId = seller.Id,
                ProductName = product.Name,
                PricePaid = product.Price,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };

            await productsRepo.UpdateAsync(product);
            await usersRepo.UpdateAsync(seller);
            await usersRepo.UpdateAsync(buyer);
            await purchasesRepo.AddAsync(purchase);

            await transaction.CommitAsync();

            return purchase.ConvertToCreatePurchaseResponse();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task<GetPurchaseResponse?> GetPurchaseAsync(Guid id)
    {
        var purchase = await purchasesRepo.FindByIdAsync(id);
        return purchase?.ConvertToGetPurchaseResponse();
    }
}