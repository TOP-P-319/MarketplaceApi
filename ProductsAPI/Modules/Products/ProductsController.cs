using Microsoft.AspNetCore.Mvc;
using ProductsAPI.Core.Constants;
using ProductsAPI.Modules.Products.Dtos.Responses;
using ProductsAPI.Modules.Products.Services;

namespace ProductsAPI.Modules.Products;

[ApiController]
[Route("api/products")]
public sealed class ProductsController(
    IProductsService productsService
) : ControllerBase
{
    [HttpGet("all", Name = Routes.Products.GetAll)]
    public async Task<ActionResult<IEnumerable<GetProductResponse>>> GetAllProductsAsync()
    {
        var response = await productsService.GetAllProductsAsync();
        return Ok(response);
    }
}