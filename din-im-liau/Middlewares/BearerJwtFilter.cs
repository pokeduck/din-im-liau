using System.Security.Claims;
using din_im_liau.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Models.DataModels;
using Services;

namespace din_im_liau.Middlewares;

public class BearerJwtFilter(AccountService accountService) : ActionFilterAttribute
{

    private readonly AccountService _accountService = accountService;
    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (context.HasActionAttribute<AllowAnonymousAttribute>())
        {
            await next();
            return;
        }

        var user = context.HttpContext.User;
        var uId = user.FindFirstValue("uid") ?? throw new UnauthorizedAccessException("Your token has expired!");



        var mockupAccount = new Account();
        var convertResult = int.TryParse(uId, out var intUid);
        if (!convertResult)
        {
            throw new UnauthorizedAccessException("Your token is invalid!");
        }

        var lastAccount = await _accountService.GetByAccountId(intUid) ?? throw new UnauthorizedAccessException("Your token uid is Invalid");



        context.HttpContext.Items.Add("Account", lastAccount);

        Console.WriteLine(user);

        await next();
    }
}
