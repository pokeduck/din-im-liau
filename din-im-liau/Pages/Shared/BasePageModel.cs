

using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Models.DataModels;
using NUglify.Helpers;
using NUglify.JavaScript.Syntax;
using Services;

namespace din_im_liau.Page;

[Authorize]
[ValidateAntiForgeryToken]
public class BasePageModel : PageModel
{
    private const string IsLoggedInKey = "IsLoggedIn";
    private const string NickNameKey = "NickName";
    private const string EmailKey = "Email";

    protected Account? Account { get; set; }
    protected readonly AccountService _accountService;
    protected readonly HttpContext _httpContext;

    protected int Id => int.Parse(User.FindFirstValue("accountId"));

    protected void IgnoreFieldValidation(string key) => ModelState.Where(x => x.Key.Contains(key)).ForEach(x => ModelState.Remove(x.Key));

    protected bool IsValidField(string key) => ModelState.GetFieldValidationState(key) == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Valid;
    public BasePageModel(IHttpContextAccessor httpContextAccessor)
    {
        _httpContext = httpContextAccessor.HttpContext!;
        _accountService = _httpContext.RequestServices.GetRequiredService<AccountService>();
    }

    public override async Task OnPageHandlerExecutionAsync(PageHandlerExecutingContext context, PageHandlerExecutionDelegate next)
    {
        if (User.Identity?.IsAuthenticated ?? false)
        {
            var account = await _accountService.GetAccountById(Id);
            Account = account;
            if (account != null)
            {

                ViewData[IsLoggedInKey] = true;
                ViewData[NickNameKey] = account.Nickname;
                ViewData[EmailKey] = account.Email;
            }
            else
            {
                ViewData[IsLoggedInKey] = false;
                ViewData[NickNameKey] = null;
                ViewData[EmailKey] = null;
            }
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

    protected RedirectToPageResult RedirectToIndexPage()
    {
        return RedirectToPage("index");
    }


}
