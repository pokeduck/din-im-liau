
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.AspNetCore.Mvc.Filters;
using Models.Exceptions;

namespace Models.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthorizeRoleAdminAttribute : AuthorizeAttribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var user = context.HttpContext.User;

        var role = user.FindFirstValue("account_level") ?? throw new ForbiddenException("Insufficient permissions to access this endpoint.");
        if (role != "admin")
        {
            throw new ForbiddenException("Insufficient permissions to access this endpoint.");
        }
    }
}


