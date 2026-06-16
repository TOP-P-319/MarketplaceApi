using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProductsAPI.Core.Constants;
using ProductsAPI.Modules.App.Db;

namespace ProductsMigrations;

public class Program
{
    public static async Task Main(string[] args)
    {
        await Apply();
    }

    private static async Task Apply()
    {
        var logger = LoggerFactory.Create(builder => builder.AddConsole()).CreateLogger<Program>();
        
        logger.LogInformation("Migration started...");
        var connection = Environment.GetEnvironmentVariable(Config.Envs.Db.Connection);
        if (string.IsNullOrWhiteSpace(connection))
        {
            logger.LogError("Missing connection string {ConnectionString}.", Config.Envs.Db.Connection);
            return;
        }
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseNpgsql(connection)
            .Options;
        await using var ctx = new AppDbContext(options);
        await ctx.Database.MigrateAsync();
        logger.LogInformation("Migration succeeded.");
    }
}