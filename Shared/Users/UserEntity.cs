using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;
using Shared.Infrastructure;

namespace Shared.Users;

[Table("users")]
public sealed class UserEntity : Entity<UserEntity>
{
    [Column("name")] public string Name { get; set; } = string.Empty;

    [Column("phone_number")]
    [RegularExpression(@"^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$")]
    public string PhoneNumber { get; set; } = string.Empty;

    [Column("password_hash")] public string PasswordHash { get; set; } = string.Empty;

    [Column("balance")] public BigInteger Balance { get; set; }

    [Column("role", TypeName = "text")] public UserRoles Role { get; set; }
    [Column("status", TypeName = "text")] public UserStatus Status { get; set; }

    public override void Update(UserEntity other)
    {
        base.Update(other);
        Name = other.Name;
        PhoneNumber = other.PhoneNumber;
        PasswordHash = other.PasswordHash;
        Balance = other.Balance;
        Role = other.Role;
        Status = other.Status;
    }
}