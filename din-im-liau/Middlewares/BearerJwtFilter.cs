using din_im_liau.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;

namespace din_im_liau.Middlewares;

public class BearerJwtFilter : ActionFilterAttribute
{
    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (context.HasActionAttribute<AllowAnonymousAttribute>())
        {
            await next();
            return;
        }

        var user = context.HttpContext.User;

        Console.WriteLine(user);
        if (true) {
            throw new UnauthorizedAccessException("Your token has expired!");
        } else {

        }
        
        await next();
    }
}
