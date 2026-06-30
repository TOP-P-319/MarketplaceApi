using Microsoft.EntityFrameworkCore.Storage;

namespace Shared.Infrastructure;

public sealed class TransactionBuilder(AppDbContext ctx)
{
    public async Task<IDbContextTransaction> BeginTransactionAsync() => await ctx.Database.BeginTransactionAsync();
}