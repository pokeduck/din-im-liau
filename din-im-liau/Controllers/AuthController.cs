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
    public async Task<IActionResult> Refresh([FromBody] AuthRefreshRequest token)
    {
        return Ok(new { refreshToken = token });
    }


    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        return Ok(Account);
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

    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword()
    {
        return Ok();
    }

    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword()
    {
        return Ok();
    }

    [HttpGet("email-verification")]
    public async Task<IActionResult> EmailVerification([FromQuery] string token)
    {
        return Ok(new { token = token });
    }




}

