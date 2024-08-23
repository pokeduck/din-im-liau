using din_im_liau.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace din_im_liau.Controllers;

// [Route("auth")]
// [Produces("application/json")]
// [ApiController]
public class AuthController : BaseController
{
    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest login)
    {
        return Ok(login);
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh(string refreshToken)
    {
        return Ok(refreshToken);
    }


    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        return Ok();
    }

    [AllowAnonymous]
    // [Consumes("application/x-www-form-urlencoded")]
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] AuthRegisterRequest register)
    {
        throw new NotImplementedException("Exception Test 01");
        //return new BadRequestResult();
        //return Ok(register);
    }

}

