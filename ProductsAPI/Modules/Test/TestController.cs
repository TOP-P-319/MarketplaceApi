using Microsoft.AspNetCore.Mvc;
using ProductsAPI.Modules.Test.Dtos.Requests;
using ProductsAPI.Modules.Test.Dtos.Responses;

namespace ProductsAPI.Modules.Test;

[ApiController]
[Route("api/test")]
public class TestController : ControllerBase
{
    [HttpGet]
    [Route("hello")]
    public async Task<ActionResult<string>> HelloWorldAsync()
    {
        return Ok($"Hello World From {GetType().Name}!");
    }

    [HttpPost]
    [Route("do/something")]
    public async Task<ActionResult<TestDataResponse>> DoSomethingAsync([FromBody] TestDataRequest test)
    {
        if (test.Id <= 0) return BadRequest("Невалидное значение ID");
        return Ok(new TestDataResponse()
        {
            Id = test.Id,
            Name = test.Name
        });
    }
}