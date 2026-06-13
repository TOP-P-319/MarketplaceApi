using Microsoft.AspNetCore.Mvc;
using ProductsAPI.Core.Constants;
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
    [HttpPost(Name = Routes.Product.Create)]
    public async Task<ActionResult<CreateProductResponse>> CreateProductAsync([FromBody] CreateProductRequest request)
    {
        var response = await productsService.AddProductAsync(request);
        return CreatedAtRoute(Routes.Product.Get, new { id = response.Id }, response);
    }

    [HttpGet("{id:guid}", Name = Routes.Product.Get)]
    public async Task<ActionResult<GetProductResponse>> GetProductAsync([FromRoute] Guid id)
    {
        var response = await productsService.GetProductAsync(id);
        if (response == null) return NotFound();
        return Ok(response);
    }

    [HttpPut("{id:guid}", Name = Routes.Product.Update)]
    public async Task<ActionResult<UpdateProductResponse>> UpdateProductAsync([FromRoute] Guid id,
        [FromBody] UpdateProductRequest request)
    {
        try
        {
            var response = await productsService.UpdateProductAsync(id, request);
            return Ok(response);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpDelete("{id:guid}", Name = Routes.Product.Delete)]
    public async Task<ActionResult> DeleteProductAsync([FromRoute] Guid id)
    {
        try
        {
            await productsService.RemoveProductAsync(id);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}