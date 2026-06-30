using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PurchasesAPI.Constants;
using Shared.Users;

namespace PurchasesAPI.Purchases;

[ApiController]
[Authorize]
[Route("api/purchase")]
public sealed class PurchaseController(PurchaseService purchaseService) : ControllerBase
{
    [HttpPost(Name = Routes.Purchase.Create)]
    [Authorize(Roles = UserRole.Buyer)]
    public async Task<ActionResult<CreatePurchaseResponse?>> CreatePurchaseAsync(
        [FromBody] CreatePurchaseRequest request)
    {
        if (!Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var buyerId))
            return Unauthorized();

        try
        {
            var response = await purchaseService.CreatePurchaseAsync(request, buyerId);
            return CreatedAtRoute(Routes.Purchase.Get, new { id = response.PurchaseId }, response);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(ex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id:guid}", Name = Routes.Purchase.Get)]
    public async Task<ActionResult<GetPurchaseResponse?>> GetPurchaseAsync([FromRoute] Guid id)
    {
        var response = await purchaseService.GetPurchaseAsync(id);
        if (response == null) return NotFound();
        return Ok(response);
    }

    [HttpGet("mine", Name = Routes.Purchase.GetMine)]
    [Authorize(Roles = UserRole.Buyer)]
    public async Task<ActionResult<IEnumerable<GetPurchaseHistoryResponse>>> GetMyPurchasesAsync()
    {
        if (!Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var buyerId))
            return Unauthorized();

        var response = await purchaseService.GetBuyerHistoryAsync(buyerId);
        return Ok(response);
    }

    [HttpGet("sales", Name = Routes.Purchase.GetSales)]
    [Authorize(Roles = UserRole.Seller)]
    public async Task<ActionResult<IEnumerable<GetPurchaseHistoryResponse>>> GetMySalesAsync()
    {
        if (!Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var sellerId))
            return Unauthorized();

        var response = await purchaseService.GetSellerSalesAsync(sellerId);
        return Ok(response);
    }
}