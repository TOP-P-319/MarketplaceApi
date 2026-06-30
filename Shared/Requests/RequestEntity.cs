using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Shared.Infrastructure;

namespace Shared.Requests;

[Table("requests")]
public sealed class RequestEntity : Entity<RequestEntity>
{
    [Column("type", TypeName = "text")]
    [Required]
    public RequestTypes Type { get; set; }
    
    [Column("status",  TypeName = "text")]
    [Required]
    public RequestStatuses Status { get; set; }

    [Column("payload", TypeName = "jsonb")]
    [Required]
    public Dictionary<string, string?> Payload { get; set; } = [];
}