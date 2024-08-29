using System.Security.Claims;
using din_im_liau.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Models.DataModels;

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
        var uId = user.FindFirstValue("accountId");
        if (uId == null)
            throw new UnauthorizedAccessException("Your token has expired!");

        var mockupAccount = new Account();
        var convertResult = int.TryParse(uId, out var intUid);
        if (!convertResult)
        {
            throw new UnauthorizedAccessException("Your token is invalid!");
        }


        mockupAccount.Id = intUid;


        context.HttpContext.Items.Add("Account", mockupAccount);

        Console.WriteLine(user);

        await next();
    }
}
