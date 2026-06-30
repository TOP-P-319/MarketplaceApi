using dotenv.net;
using ProductsAPI.Products;
using ProductsAPI.Reviews;
using Shared.Products;
using Shared.Requests;
using Shared.Reviews;
using Shared.Users;
using Shared.Utils;

namespace ProductsAPI;

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

        builder.Services.AddScoped<ProductsService>();
        builder.Services.AddScoped<ProductsRepo>();
        builder.Services.AddScoped<ProductCreateRequestsRepo>();
        builder.Services.AddScoped<ProductUpdateRequestsRepo>();
        builder.Services.AddSingleton<ProductMapper>();
        builder.Services.AddSingleton<ProductCreateRequestMapper>();
        builder.Services.AddSingleton<ProductUpdateRequestMapper>();

        builder.Services.AddScoped<ReviewsService>();
        builder.Services.AddScoped<ReviewsRepo>();
        builder.Services.AddScoped<ReviewCreateRequestsRepo>();
        builder.Services.AddSingleton<ReviewMapper>();
        builder.Services.AddSingleton<ReviewCreateRequestMapper>();
        builder.Services.AddScoped<UsersRepo>();
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