using System.ComponentModel.DataAnnotations;

namespace ProductsAPI.Modules.Test.Dtos.Requests;

public class TestDataRequest
{
    [Required]
    public int Id { get; set; }
    [Required] public string Name { get; set; } = string.Empty;
}