using Shared.Requests;
using Shared.Users;

namespace UsersAPI.Auth;

public static class AuthConverter
{
    public static UserModel ConvertToUser(this RegisterRequest request, UserRoles role) => new()
    {
        Name = request.Name,
        PhoneNumber = request.PhoneNumber,
        Role = role,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow,
    };

    public static SellerRegisterRequestModel ConvertToSellerRegisterRequest(this UserModel request) => new()
    {
        Name = request.Name,
        PhoneNumber = request.PhoneNumber,
        PasswordHash = request.PasswordHash,
        Status = RequestStatuses.Pending,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow,
    };
}