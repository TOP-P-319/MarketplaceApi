using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Users;

namespace UsersAPI.Users;

[ApiController]
[Route("api/user")]
[Authorize]
public sealed class UsersController(UsersRepo usersRepo) : ControllerBase
{
    [HttpGet("me")]
    public async Task<ActionResult<GetMeResponse>> GetMeAsync()
    {
        if (!Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var id))
            return Unauthorized();

        var user = await usersRepo.FindByIdAsync(id);
        if (user == null) return NotFound();

        return Ok(new GetMeResponse
        {
            Id = user.Id,
            Name = user.Name,
            PhoneNumber = user.PhoneNumber,
            Role = user.Role.ToString(),
            Balance = user.Balance.ToString(),
            Status = user.Status.ToString(),
        });
    }
}
