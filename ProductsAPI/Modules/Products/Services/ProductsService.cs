using ProductsAPI.Modules.Products.Db.Repos;
using ProductsAPI.Modules.Products.Domain.Models;

namespace ProductsAPI.Modules.Products.Services;

public sealed class ProductsService(IProductsRepo productsRepo) : IProductsService
{
    public async Task<ProductModel?> GetProductAsync(Guid id) => await productsRepo.FindByIdAsync(id);
    public async Task<IEnumerable<ProductModel>> GetAllProductsAsync() => await productsRepo.FindAllAsync();
    public async Task RemoveProductAsync(Guid id)=> await productsRepo.DeleteByIdAsync(id);
    public async Task AddProductAsync(ProductModel product) =>  await productsRepo.AddAsync(product);
    public async Task UpdateProductAsync(ProductModel product) =>  await productsRepo.UpdateAsync(product);
}