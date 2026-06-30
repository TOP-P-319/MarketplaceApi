using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Shared.Users;

namespace UsersAPI.Auth;

public sealed class AuthService(UsersRepo usersRepo, PasswordHasher<UserModel> hasher, IOptions<JwtSettings> jwtOptions)
{
    private readonly JwtSettings _jwtSettings = jwtOptions.Value;

    public async Task<List<Claim>> RegisterUser(RegisterRequest request, UserRoles role)
    {
        var user = new UserModel
        {
            Name = request.Name,
            PhoneNumber = request.PhoneNumber,
            Role = role,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
        };
        user = user.WithPasswordHash(hasher.HashPassword(user, request.Password));
        await usersRepo.AddAsync(user);
        return
        [
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Role, user.Role.ToString())
        ];
    }

    public async Task<List<Claim>?> ValidateCredentials(LoginRequest request)
    {
        var user = await usersRepo.FindByPhoneNumber(request.PhoneNumber);
        if (user == null) return null;

        var verification = hasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);
        if (verification == PasswordVerificationResult.Failed) return null;

        return
        [
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Role, user.Role.ToString())
        ];
    }

    public LoginResponse GenerateJwtToken(List<Claim> claims)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddHours(2),
            signingCredentials: credentials
        );

        return new LoginResponse { Token = new JwtSecurityTokenHandler().WriteToken(token) };
    }
}