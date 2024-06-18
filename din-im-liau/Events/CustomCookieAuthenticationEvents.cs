using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Services;


namespace din_im_liau.Events;
public class CustomCookieAuthenticationEvents : CookieAuthenticationEvents
{
    private readonly AccountService _accountService;

    public CustomCookieAuthenticationEvents(AccountService accountService)
    {
        _accountService = accountService;
    }


    public override async Task ValidatePrincipal(CookieValidatePrincipalContext context)
    {
        var userPrincipal = context.Principal;

        // // Look for the LastChanged claim.
        var googleId = (from c in userPrincipal.Claims
                        where c.Type == "googleId"
                        select c.Value).FirstOrDefault();
        var accountId = (from c in userPrincipal.Claims
                         where c.Type == "accountId"
                         select c.Value).FirstOrDefault();
        if (string.IsNullOrEmpty(googleId))
        {
            context.RejectPrincipal();
            await context.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return;
        }


        var lastAccount = await _accountService.Get(googleId);
        if (lastAccount == null)
        {
            context.RejectPrincipal();
            await context.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return;
        }

        Console.WriteLine(lastAccount);
        // if (string.IsNullOrEmpty(lastChanged) ||
        //     !_userRepository.ValidateLastChanged(lastChanged))
        // {
        //     context.RejectPrincipal();

        //     await context.HttpContext.SignOutAsync(
        //         CookieAuthenticationDefaults.AuthenticationScheme);
        // }
        Console.WriteLine(userPrincipal.Claims);
        Console.WriteLine("cookie middleware!");
    }
}
