using Azure;
using din_im_liau.Page;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Web;
using Common.Extensions;
using Common.Helper;
namespace din_im_liau.Pages.User;

[AllowAnonymous]
[IgnoreAntiforgeryToken(Order = 1001)]
public class LoginModel : BasePageModel
{
    private readonly IConfiguration _config;
    public LoginModel(IConfiguration config, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
    {
        _config = config;
    }
    public async Task<IActionResult> OnGet()
    {
        _httpContext.DeleteGoogleState();
        Console.WriteLine("get");
        return Page();
    }

    public async Task<IActionResult> OnGetGoogleSignIn()
    {
        var uriBuilder = new UriBuilder("https", "accounts.google.com");
        uriBuilder.Path = "/o/oauth2/v2/auth";
        var parameters = new Dictionary<string, string>();
        var oauthState = RandomHelper.Random(32) ?? "state_token";
        _httpContext.SetGoogleStateResponseCookie(oauthState);

        parameters.Add("state", oauthState);
        var clientId = _config["Google:ClientId"] ?? "";
        parameters.Add("access_type", "offline");
        parameters.Add("scope", "openid email profile");
        parameters.Add("response_type", "code");
        parameters.Add("redirect_uri", "https://localhost:8888/login/google");
        parameters.Add("client_id", clientId);
        parameters.Add("prompt", "select_account");
        var queryBuilder = new QueryBuilder(parameters);
        QueryString queryString = queryBuilder.ToQueryString();
        uriBuilder.Query = queryString.Value;
        var result = uriBuilder.Uri.AbsoluteUri;


        Console.WriteLine("get sign in");
        return Redirect(result);
    }


    public async Task<IActionResult> OnPostAsync()
    {

        Console.WriteLine("post");
        return new JsonResult(new { Success = true, });
    }
    public async Task<IActionResult> OnPostGoogleSignIn()
    {
        Console.WriteLine("post google sign in");
        return Page();
    }
}
