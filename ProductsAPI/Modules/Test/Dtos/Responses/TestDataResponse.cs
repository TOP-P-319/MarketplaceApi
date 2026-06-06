using System.ComponentModel.DataAnnotations;

namespace ProductsAPI.Modules.Test.Dtos.Responses;

public class TestDataResponse
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; } =  string.Empty;
}