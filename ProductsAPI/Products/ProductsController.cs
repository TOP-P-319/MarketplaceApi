using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductsAPI.Core.Constants;
using ProductsAPI.Products.Requests;
using ProductsAPI.Products.Responses;
using Shared.Products;
using Shared.Users;

namespace ProductsAPI.Products;

[ApiController]
[Route("api/products")]
public sealed class ProductsController(
    ProductsService productsService
) : ControllerBase
{
    [HttpPost(Name = Routes.Product.Create)]
    [Authorize(Roles = UserRole.Seller)]
    public async Task<ActionResult> CreateProductAsync([FromBody] CreateProductRequest request)
    {
        if (!Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var sellerId))
            return Unauthorized();

        await productsService.CreateProductCreateRequestAsync(request, sellerId);
        return NoContent();
    }

    [HttpGet("{id:guid}", Name = Routes.Product.Get)]
    public async Task<ActionResult<GetProductResponse>> GetProductAsync([FromRoute] Guid id)
    {
        var response = await productsService.GetProductAsync(id);
        if (response == null) return NotFound();
        return Ok(response);
    }

    [HttpPut("{id:guid}", Name = Routes.Product.Update)]
    [Authorize(Roles = UserRole.Seller)]
    public async Task<ActionResult<UpdateProductResponse>> UpdateProductAsync(
        [FromRoute] Guid id,
        [FromBody] UpdateProductRequest request
    )
    {
        if (!Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var sellerId))
            return Unauthorized();

        try
        {
            await productsService.CreateProductUpdateRequestAsync(id, request, sellerId);
            return NoContent();
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

    [HttpGet("previews", Name = Routes.Products.GetAllPreviews)]
    public async Task<ActionResult<IEnumerable<GetProductResponse>>> GetAllProductPreviewsAsync()
    {
        var response = await productsService.GetAllProductPreviewsAsync();
        return Ok(response);
    }

    [HttpGet("mine", Name = Routes.Products.GetMine)]
    [Authorize(Roles = UserRole.Seller)]
    public async Task<ActionResult<IEnumerable<GetMyProductResponse>>> GetMyProductsAsync()
    {
        if (!Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var sellerId))
            return Unauthorized();

        var response = await productsService.GetSellerProductsAsync(sellerId);
        return Ok(response);
    }

    [HttpPost("{id:guid}/publish", Name = Routes.Product.Publish)]
    [Authorize(Roles = UserRole.Seller)]
    public Task<ActionResult> PublishProductAsync([FromRoute] Guid id) =>
        SetStatusAsync(id, ProductStatuses.Published);

    [HttpPost("{id:guid}/hide", Name = Routes.Product.Hide)]
    [Authorize(Roles = UserRole.Seller)]
    public Task<ActionResult> HideProductAsync([FromRoute] Guid id) =>
        SetStatusAsync(id, ProductStatuses.Hidden);

    [HttpPatch("{id:guid}/price-amount", Name = Routes.Product.UpdatePriceAmount)]
    [Authorize(Roles = UserRole.Seller)]
    public async Task<ActionResult> UpdatePriceAndAmountAsync(
        [FromRoute] Guid id,
        [FromBody] UpdatePriceAmountRequest request)
    {
        if (!Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var sellerId))
            return Unauthorized();

        try
        {
            await productsService.UpdatePriceAndAmountAsync(id, sellerId, request);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (UnauthorizedAccessException)
        {
            return Forbid();
        }
    }

    private async Task<ActionResult> SetStatusAsync(Guid id, ProductStatuses status)
    {
        if (!Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var sellerId))
            return Unauthorized();

        try
        {
            await productsService.SetProductStatusAsync(id, sellerId, status);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (UnauthorizedAccessException)
        {
            return Forbid();
        }
    }
}