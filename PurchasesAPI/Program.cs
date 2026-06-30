using dotenv.net;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using PurchasesAPI.Purchases;
using Shared.Constants;
using Shared.Infrastructure;
using Shared.Products;
using Shared.Purchases;
using Shared.Users;
using Shared.Utils;

namespace PurchasesAPI;

public static class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        if (!EnvironmentEx.IsRunningInContainer)
        {
            DotEnv.Load();
            builder.Configuration.AddEnvironmentVariables();
        }

        builder.Services.AddControllers();
        builder.Services.AddJwtAuthentication();
        builder.Services.AddAuthorization();
        builder.Services.AddNpgsqlWithDynamicJson();

        builder.Services.AddOpenApi();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerWithSecurityGen();

        builder.Services.AddScoped<PurchaseService>();
        
        builder.Services.AddScoped<TransactionBuilder>();
        builder.Services.AddScoped<ProductsRepo>();
        builder.Services.AddScoped<PurchasesRepo>();
        builder.Services.AddScoped<UsersRepo>();
        
        
        builder.Services.AddSingleton<ProductMapper>();
        builder.Services.AddSingleton<PurchaseMapper>();
        builder.Services.AddSingleton<UserMapper>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        await app.RunAsync();
    }
}