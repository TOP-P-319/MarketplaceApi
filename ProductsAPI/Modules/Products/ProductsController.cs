using Microsoft.AspNetCore.Mvc;
using ProductsAPI.Modules.Products.Dtos.Responses;
using ProductsAPI.Modules.Products.Services;

namespace ProductsAPI.Modules.Products;

[ApiController]
[Route("api/products")]
public sealed class ProductsController(
    IProductsService productsService
) : ControllerBase
{
    [HttpGet]
    [Route("all")]
    public async Task<ActionResult<IEnumerable<GetProductResponse>>> GetAllProductsAsync()
    {
        var products = await productsService.GetAllProductsAsync();
        return Ok(products.Select(GetProductResponse.CreateFrom));
    }
}