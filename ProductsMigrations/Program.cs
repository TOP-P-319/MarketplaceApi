using Microsoft.EntityFrameworkCore;
using ProductsAPI.Core.Constants;
using ProductsAPI.Modules.App.Db;

namespace ProductsMigrations;

public static class Program
{
    public static async Task Main(string[] args)
    {
        var connection = Environment.GetEnvironmentVariable(Config.Envs.Db.Connection);
        if (string.IsNullOrWhiteSpace(connection)) throw new InvalidOperationException();
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseNpgsql(connection)
            .Options;
        await using var ctx = new AppDbContext(options);
        await ctx.Database.MigrateAsync();
    }
}