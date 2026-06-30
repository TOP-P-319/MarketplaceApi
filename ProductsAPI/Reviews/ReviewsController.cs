using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductsAPI.Core.Constants;
using ProductsAPI.Reviews.Requests;
using ProductsAPI.Reviews.Responses;
using Shared.Users;

namespace ProductsAPI.Reviews;

[ApiController]
[Route("api/reviews")]
public sealed class ReviewsController(ReviewsService reviewsService) : ControllerBase
{
    [HttpGet(Name = Routes.Reviews.GetByProduct)]
    public async Task<ActionResult<IEnumerable<GetReviewResponse>>> GetProductReviewsAsync([FromQuery] Guid productId)
    {
        var response = await reviewsService.GetProductReviewsAsync(productId);
        return Ok(response);
    }

    [HttpGet("mine", Name = Routes.Reviews.GetMine)]
    [Authorize(Roles = UserRole.Buyer)]
    public async Task<ActionResult<IEnumerable<GetMyReviewResponse>>> GetMyReviewsAsync()
    {
        if (!Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var authorId))
            return Unauthorized();

        var response = await reviewsService.GetMyReviewsAsync(authorId);
        return Ok(response);
    }

    [HttpPost(Name = Routes.Reviews.Create)]
    [Authorize(Roles = UserRole.Buyer)]
    public async Task<ActionResult> CreateReviewAsync([FromBody] CreateReviewRequest request)
    {
        if (!Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var authorId))
            return Unauthorized();

        try
        {
            await reviewsService.SubmitReviewAsync(request, authorId);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }
}
