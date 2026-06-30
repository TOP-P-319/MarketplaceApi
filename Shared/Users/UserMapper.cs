using Shared.Infrastructure;

namespace Shared.Users;

public sealed class UserMapper : Mapper<UserModel, UserEntity>
{
    public override UserEntity MapToEntity(UserModel model)
    {
        var entity = base.MapToEntity(model);
        entity.Name = model.Name;
        entity.PhoneNumber = model.PhoneNumber;
        entity.PasswordHash = model.PasswordHash;
        entity.Balance = model.Balance;
        entity.Role = model.Role;
        return entity;
    }

    public override UserModel MapToModel(UserEntity entity) =>
        base.MapToModel(entity) with
        {
            Balance = entity.Balance,
            Name = entity.Name,
            PhoneNumber = entity.PhoneNumber,
            PasswordHash = entity.PasswordHash,
            Role = entity.Role
        };
}