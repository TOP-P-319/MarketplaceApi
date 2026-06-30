using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;
using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure;

namespace Shared.Users;

[Table("users")]
[Index(nameof(PhoneNumber), IsUnique = true)]
public sealed class UserEntity : Entity<UserEntity>
{
    [Column("name")] [Required] public string Name { get; set; } = string.Empty;

    [Column("phone_number")]
    [RegularExpression(@"^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$")]
    [Required]
    public string PhoneNumber { get; set; } = string.Empty;

    [Column("password_hash")] [Required] public string PasswordHash { get; set; } = string.Empty;

    [Column("balance")] [Required] public BigInteger Balance { get; set; }

    [Column("role", TypeName = "text")]
    [Required]
    public UserRoles Role { get; set; }

    [Column("status", TypeName = "text")]
    [Required]
    public UserStatuses Status { get; set; }

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