using Microsoft.AspNetCore.Mvc;
using ProductsAPI.Modules.Products.Dtos.Requests;
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
    public async Task<ActionResult<GetProductResponse?>> GetProductAsync([FromRoute] Guid id)
    {
        var product = await productsService.GetProductAsync(id);
        return product == null
            ? NotFound()
            : Ok(GetProductResponse.CreateFrom(product));
    }

    [HttpPost]
    public async Task<ActionResult> AddProductAsync([FromBody] AddProductRequest request)
    {
        var product = request.ToProductModel();
        await productsService.AddProductAsync(product);
        return Ok();
    }
}