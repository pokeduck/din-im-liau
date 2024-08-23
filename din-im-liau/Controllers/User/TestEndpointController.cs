using Microsoft.AspNetCore.Mvc;

namespace din_im_liau.Controllers.User;

[Route("test")]
[Produces("application/json")]

[ApiController]
public class TestEndpointController : ControllerBase
{

    public TestEndpointController()
    {
        Console.WriteLine("init test endpoint");
    }

    [HttpGet("test2")]
    public async Task<IActionResult> Index([FromQuery] TestRequest param)
    {
        Console.WriteLine(param);
        return Ok(new { result = "ok" });
    }

    [HttpPost("post1")]
    public async Task<IActionResult> Post1()
    {
        return Ok(new { result = "post1" });
    }

    [HttpPost("post2")]
    public async Task<IActionResult> Post2()
    {
        return Ok(new { result = "post2" });
    }

    [HttpGet("get2")]
    public async Task<IActionResult> Get2()
    {
        return Ok(new { result = "get2" });
    }

    [HttpGet("get3")]
    public async Task<IActionResult> Get3([FromQuery] TestRequest req)
    {
        return Ok(req);
    }

    [HttpPost("post3")]
    public async Task<IActionResult> Post3([FromForm] TestRequest req)
    {
        return Ok(req);
    }
}

