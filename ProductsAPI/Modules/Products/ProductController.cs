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
    [Route("{id:guid}")]
    public async Task<ActionResult<GetProductResponse>> GetProduct([FromRoute] Guid id)
    {
        var product = await productService.GetProduct(id);
        return Ok(GetProductResponse.CreateFrom(product));
    }
}