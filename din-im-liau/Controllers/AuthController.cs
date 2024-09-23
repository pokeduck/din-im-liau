using Models.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Models.Attributes;
using Models.Responses;
using Models.DTOs;
using Microsoft.Extensions.Options;
using Services.Extensions;
using Models.Exceptions;
using System.Security.Claims;

namespace din_im_liau.Controllers;

// [Route("auth")]
// [Produces("application/json")]
// [ApiController]
[SwaggerTag("驗證")]
public class AuthController(AuthService authService, AccountService accountService, JwtService jwtService, RefreshTokenService refreshTokenService) : BaseController
{

    private readonly AuthService _authService = authService;
    private readonly AccountService _accountService = accountService;
    private readonly JwtService _jwtService = jwtService;

    private readonly RefreshTokenService _refreshTokenService = refreshTokenService;

    [AllowAnonymous]
    [HttpPost("tset-account")]
    public async Task<IActionResult> TestList()
    {

        Response200.Data = await _authService.GetAccounts();
        return Ok(Response200);
    }

    /// <summary>
    /// 登入
    /// </summary>
    /// <param name="login">登入資料</param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest login)
    {
        var lastAccount = await _authService.SignIn(login.Account, login.Password);
        Response200.Data = lastAccount;

        return Ok(Response200);
    }


    /// <summary>
    /// 利用AccessToken登入
    /// </summary>
    /// <returns></returns>
    [HttpPost("login-with-token")]
    public IActionResult LoginWithToken()
    {
        var account = HttpContext.GetAccount();
        Response200.Data = account;
        return Ok(Response200);
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
        var jti = HttpContext.User.FindFirstValue("jti") ?? throw new NotFoundException("Access Token not found.");
        await _authService.RevokeAccessToken(jti);
        return Ok(Response200);
    }


    /// <summary>
    /// 註冊會員
    /// </summary>
    /// <param name="request">會員資料</param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpPost("register")]
    [SwaggerSuccessResponse(typeof(GenericResponse<AccountDTO>))]
    public async Task<IActionResult> Register([FromBody] AuthRegisterRequest request)
    {
        var username = request.Username;
        var email = request.Email;
        var password = request.Password;
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
        {
            return BadRequest();
        }
        var result = await _authService.Create(username, email, password);
        Response200.Data = result;
        return Ok(Response200);
    }



    /// <summary>
    /// 忘記密碼，發送密碼到Email
    /// </summary>
    /// <returns></returns>
    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword()
    {
        return Ok();
    }


    /// <summary>
    /// 重設密碼依據Email Token
    /// </summary>
    /// <returns></returns> 
    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword()
    {
        return Ok();
    }

    /// <summary>
    /// 修改密碼
    /// </summary>
    /// <returns></returns>
    [HttpPost("modify-password")]
    public async Task<IActionResult> ModifyPassword()
    {
        return Ok();
    }


    /// <summary>
    /// 驗證email token
    /// </summary>
    /// <param name="token"> email token </param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpGet("verify-email")]
    public async Task<IActionResult> EmailVerification([FromQuery] string token)
    {
        Response200.Data = await _authService.VerifyEmail(token);
        return Ok(Response200);
    }

    /// <summary>
    /// 發送驗證email
    /// </summary>
    /// <returns></returns>
    [HttpPost("resend-verify-email")]
    public async Task<IActionResult> SendVerifyEmail()
    {
        Response200.Data = await _authService.CreateEmailVerifyToken(Account.Id);
        return Ok(Response200);
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

