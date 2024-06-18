

using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services;

namespace din_im_liau.Page;

[Authorize]
[ValidateAntiForgeryToken]
public class BasePageModel : PageModel
{
    private const string IsLoggedInKey = "IsLoggedIn";
    private const string NickNameKey = "NickName";
    private const string EmailKey = "Email";
    protected readonly AccountService _accountService;
    protected readonly HttpContext _httpContext;
    public BasePageModel(IHttpContextAccessor httpContextAccessor)
    {
        _httpContext = httpContextAccessor.HttpContext!;
        _accountService = _httpContext.RequestServices.GetRequiredService<AccountService>();
    }

    public override async Task OnPageHandlerExecutionAsync(PageHandlerExecutingContext context, PageHandlerExecutionDelegate next)
    {
        if (User.Identity?.IsAuthenticated ?? false)
        {
            ViewData[IsLoggedInKey] = true;
            ViewData[NickNameKey] = "NickName!!!";
            ViewData[EmailKey] = "Email@email.com";
        }
        else
        {
            ViewData[IsLoggedInKey] = false;
            ViewData[NickNameKey] = null;
            ViewData[EmailKey] = null;
        }
        await base.OnPageHandlerExecutionAsync(context, next);
    }


    protected async Task SignIn(string googleUserId, string accountId)
    {

        var claims = new List<Claim> {
             new Claim("googleId", googleUserId) ,
             new Claim("accountId", accountId),
             };

        var identity = new ClaimsIdentity(
            claims,
            CookieAuthenticationDefaults.AuthenticationScheme
        );
        var principal = new ClaimsPrincipal(identity);
        var authProperties = new AuthenticationProperties
        {
            IsPersistent = true,
            ExpiresUtc = DateTime.UtcNow.AddMinutes(5)
        };

        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            principal,
            authProperties
        );
    }

    protected async Task SignOut()
    {
        await _httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    }
}
