using Microsoft.EntityFrameworkCore;
using ProductsAPI.Core.Infrastructure.Db.Mappers;
using ProductsAPI.Modules.Products.Db.Entities;
using ProductsAPI.Modules.Products.Db.Mappers;
using ProductsAPI.Modules.Products.Db.Repos;
using ProductsAPI.Modules.Products.Domain.Models;
using ProductsAPI.Modules.Products.Services;
using ProductsAPI.Modules.Shared.Db;

namespace ProductsAPI;

public static class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddDbContext<ProductsDbContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("ProductsDB")));

        // Add services to the container.
        builder.Services.AddAuthorization();

        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options => options.SupportNonNullableReferenceTypes());

        #region Services

        builder.Services.AddScoped<IProductsService, ProductsService>();

        #endregion

        #region Repos

        builder.Services.AddScoped<IProductRepo, ProductRepo>();

        #endregion

        #region Mappers

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