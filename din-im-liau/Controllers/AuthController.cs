using din_im_liau.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace din_im_liau.Controllers;

// [Route("auth")]
// [Produces("application/json")]
// [ApiController]
[SwaggerTag("驗證")]
public class AuthController : BaseController
{

    /// <summary>
    /// 登入
    /// </summary>
    /// <param name="login">登入資料</param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest login)
    {
        return Ok(login);
    }

    /// <summary>
    /// 更新 access token 依據 refresh token
    /// </summary>
    /// <param name="token">token data</param>
    /// <returns></returns>
    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh([FromBody] AuthRefreshRequest token)
    {
        return Ok(new { refreshToken = token });
    }


    /// <summary>
    /// 登出
    /// </summary>
    /// <returns></returns>
    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        return Ok(Account);
    }


    /// <summary>
    /// 註冊會員
    /// </summary>
    /// <param name="register">會員資料</param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] AuthRegisterRequest register)
    {
        //throw new NotImplementedException("Exception Test 01");
        //return new BadRequestResult();
        return Ok(register);
    }



    /// <summary>
    /// 忘記密碼
    /// </summary>
    /// <returns></returns>
    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword()
    {
        return Ok();
    }


    /// <summary>
    /// 重設密碼
    /// </summary>
    /// <returns></returns> <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword()
    {
        return Ok();
    }


    /// <summary>
    /// 驗證email token
    /// </summary>
    /// <param name="token"> email token </param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpGet("email-verification")]
    public async Task<IActionResult> EmailVerification([FromQuery] string token)
    {
        return Ok(new { token = token });
    }

    /// <summary>
    ///  驗證 google 登入 token
    /// </summary>
    /// <param name="token">google token</param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpGet("google/callback")]
    public async Task<IActionResult> GoogleCallBack([FromQuery] string token)
    {
        return Ok(new { token = token });
    }

    /// <summary>
    ///  驗證 github 登入 token
    /// </summary>
    /// <param name="token">github token</param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpGet("github/callback")]
    public async Task<IActionResult> GithubCallBack([FromQuery] string token)
    {
        return Ok(new { token = token });
    }

}

