using dotenv.net;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Shared.Constants;
using Shared.Infrastructure;
using Shared.Utils;

namespace Migrations;

public sealed class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        if (!EnvironmentEx.IsRunningInContainer) DotEnv.Load();
        return new AppDbContext(new DbContextOptionsBuilder<AppDbContext>()
            .UseNpgsql(Environment.GetEnvironmentVariable(Config.Envs.Db.Connection), builder => builder.MigrationsAssembly("Migrations"))
            .Options);
    }
}