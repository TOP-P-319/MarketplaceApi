using Microsoft.AspNetCore.Mvc;
using ProductsAPI.Core.Constants;
using ProductsAPI.Products.Responses;

namespace ProductsAPI.Products;

[ApiController]
[Route("api/products")]
public sealed class ProductsController(
    ProductsService productsService
) : ControllerBase
{
    [HttpGet("all/preview", Name = Routes.Products.GetAllPreviews)]
    public async Task<ActionResult<IEnumerable<GetProductResponse>>> GetAllProductPreviewsAsync()
    {
        var response = await productsService.GetAllProductPreviewsAsync();
        return Ok(response);
    }
}