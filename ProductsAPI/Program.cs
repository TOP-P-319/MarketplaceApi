using Microsoft.EntityFrameworkCore;
using ProductsAPI.Core.Infrastructure.Db.Mappers;
using ProductsAPI.Modules.App.Db;
using ProductsAPI.Modules.Products.Db.Entities;
using ProductsAPI.Modules.Products.Db.Mappers;
using ProductsAPI.Modules.Products.Db.Repos;
using ProductsAPI.Modules.Products.Domain.Models;
using ProductsAPI.Modules.Products.Services;

namespace ProductsAPI;

public static class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        #region Services

        builder.Services.AddControllers();
        builder.Services.AddAuthorization();
        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("ProductsDB"))
        );

        #endregion

        #region API Docs

        builder.Services.AddOpenApi();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        #endregion

        #region Services DI

        builder.Services.AddScoped<IProductsService, ProductsService>();

        #endregion

        #region Repositories DI

        builder.Services.AddScoped<IProductsRepo, ProductsRepo>();

        #endregion

        #region Mappers DI

        builder.Services.AddSingleton<IMapper<ProductModel, ProductEntity>, ProductMapper>();

        #endregion

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        await app.RunAsync();
    }
}