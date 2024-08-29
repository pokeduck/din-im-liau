

using Microsoft.AspNetCore.Mvc;

namespace din_im_liau.Controllers;

[Route("api/v{version:apiVersion}/auth/[controller]")]
public class GoogleController : BaseController
{
    [HttpGet("callback")]
    public async Task<IActionResult> CallBack()
    {
        return Ok();
    }
}
