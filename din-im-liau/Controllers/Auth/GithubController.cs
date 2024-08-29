

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace din_im_liau.Controllers;

[Route("api/v{version:apiVersion}/auth/[controller]")]
public class GithubController : BaseController
{
    [HttpGet("callback")]
    [AllowAnonymous]
    public async Task<IActionResult> CallBack()
    {
        return Ok();
    }
}
