using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;


namespace din_im_liau.Events;
public class CustomCookieAuthenticationEvents : CookieAuthenticationEvents
{
    //private readonly IUserRepository _userRepository;

    // public CustomCookieAuthenticationEvents(IUserRepository userRepository)
    // {
    //     _userRepository = userRepository;
    // }

    public CustomCookieAuthenticationEvents() { }

    public override async Task ValidatePrincipal(CookieValidatePrincipalContext context)
    {
        var userPrincipal = context.Principal;

        // // Look for the LastChanged claim.
        var lastChanged = (from c in userPrincipal.Claims
                           where c.Type == "google"
                           select c.Value).FirstOrDefault();
        Console.WriteLine(lastChanged);
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
