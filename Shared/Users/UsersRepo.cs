using Shared.Infrastructure;

namespace Shared.Users;

public sealed class UsersRepo(AppDbContext ctx, UserMapper mapper) : Repo<UserModel, UserEntity>(ctx, ctx.Users, mapper)
{
    public async Task<UserModel?> FindByPhoneNumber(string phoneNumber) =>
        await FindByAsync(e => e.PhoneNumber == phoneNumber);
}