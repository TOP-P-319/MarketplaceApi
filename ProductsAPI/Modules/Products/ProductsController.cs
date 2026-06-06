using Microsoft.AspNetCore.Mvc;
using ProductsAPI.Modules.Products.Dtos.Responses;
using ProductsAPI.Modules.Products.Services;

namespace ProductsAPI.Modules.Products;

[ApiController]
[Route("api/products")]
public sealed class ProductsController(IProductsService productsService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetProductResponse>>> GetAllProducts()
    {
        var products = await productsService.GetAllProducts();
        return Ok(products.Select(GetProductResponse.CreateFrom));
    }
}