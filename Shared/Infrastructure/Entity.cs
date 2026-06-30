using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.Infrastructure;

public abstract class Entity
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Column("created_at")]
    [Required]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at")]
    [Required]
    public DateTime UpdatedAt { get; set; }
}

public abstract class Entity<TSelf> : Entity where TSelf : Entity<TSelf>
{
    public virtual void Update(TSelf other)
    {
        UpdatedAt = other.UpdatedAt;
    }
}