using Microsoft.AspNetCore.Mvc;
using ProductsAPI.Modules.Products.Dtos.Responses;
using ProductsAPI.Modules.Products.Services;

namespace ProductsAPI.Modules.Products;

[ApiController]
[Route("api/product")]
public sealed class ProductController(
    IProductsService productsService
) : ControllerBase
{
    [HttpGet]
    [Route("{id:guid}")]
    public async Task<ActionResult<GetProductResponse?>> GetProduct([FromRoute] Guid id)
    {
        var product = await productsService.GetProduct(id);
        return product == null
            ? NotFound()
            : Ok(GetProductResponse.CreateFrom(product));
    }
}