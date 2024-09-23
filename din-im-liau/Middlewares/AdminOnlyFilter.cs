using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Filters;
using Services;
using Services.Extensions;

namespace din_im_liau.Middlewares;
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AdminOnlyFilter : Attribute, IAuthorizationFilter
{

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var user = context.HttpContext.User;

        var role = user.FindFirstValue("role") ?? throw new UnauthorizedAccessException("Insufficient permissions to access this endpoint.");
        if (role != "admin")
        {
            throw new UnauthorizedAccessException("Insufficient permissions to access this endpoint.");
        }

    }
}
