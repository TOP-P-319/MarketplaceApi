using Microsoft.AspNetCore.Mvc;
using ProductsAPI.Modules.Products.Dtos.Responses;
using ProductsAPI.Modules.Products.Services;

namespace ProductsAPI.Modules.Products;

[ApiController]
[Route("api/product")]
public sealed class ProductController(
    IProductService productService
) : ControllerBase
{
    [HttpGet]
    [Route("{id:int}")]
    public async Task<ActionResult<GetProductResponse>> GetProduct([FromRoute] int id)
    {
        var product = await productService.GetProduct(id);
        return Ok(GetProductResponse.CreateFrom(product));
    }
}