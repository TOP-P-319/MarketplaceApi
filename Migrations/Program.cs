using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Migrations;

public class Program
{
    public static async Task Main(string[] args)
    {
        var logger = LoggerFactory.Create(builder => builder.AddConsole()).CreateLogger<Program>();
        logger.LogInformation("Migration started...");
        await using var ctx = new AppDbContextFactory().CreateDbContext(args);
        await ctx.Database.MigrateAsync();
        logger.LogInformation("Migration succeeded.");
        await Seeder.SeedAsync(ctx);
        logger.LogInformation("Seeding succeeded.");
    }
}