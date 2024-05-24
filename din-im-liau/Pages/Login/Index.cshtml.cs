using Azure;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Web;
namespace din_im_liau.Pages.Login;
public class IndexModel : PageModel
{
    public async Task<IActionResult> OnGet()
    {
        Console.WriteLine("get");
        return Page();
    }

    public async Task<IActionResult> OnGetGoogleSignIn()
    {
        var uriBuilder = new UriBuilder("https", "accounts.google.com");
        uriBuilder.Path = "/o/oauth2/v2/auth";
        var parameters = new Dictionary<string, string>();
        parameters.Add("state", "eyJjc3JmVG9rZW4iOiI1YmEwY2E5NzRjNTJkNzA3ZTYzY2Y3ODkzY2ExODQ3NjIzZTA5NmNmMmQxZDg4NGJlNzEyMzVkMjMzOGI0OWY1IiwiYW5vbnltb3VzSWQiOiJhMjZlYTEyZS0zYzhlLTQyN2EtYTBkNy1hZjcyMTBhYTg2MDkiLCJxdWVyeSI6Ij9hcHBsaWNhdGlvbj1iaXRidWNrZXQmY29udGludWU9aHR0cHMlM0ElMkYlMkZiaXRidWNrZXQub3JnJTJGYWNjb3VudCUyRnNpZ25pbiUyRiUzRm5leHQlM0QlMjUyRiUyNnJlZGlyZWN0Q291bnQlM0QxJmxvZ2luVHlwZT1nb29nbGVCdXR0b24mcHJvbXB0PXNlbGVjdF9hY2NvdW50JnNvdXJjZT1sb2dpblNjcmVlbiIsInNvdXJjZSI6ImxvZ2luU2NyZWVuIiwibG9naW5UeXBlIjoiZ29vZ2xlQnV0dG9uIn0%3D");
        parameters.Add("access_type", "offline");
        parameters.Add("scope", "openid email profile");
        parameters.Add("response_type", "code");
        parameters.Add("redirect_uri", "https://localhost:8888/login/google");
        parameters.Add("client_id", "697611657905-ide2j8m6bav95l0kcn45kn1lra6c7v3l.apps.googleusercontent.com");
        parameters.Add("prompt", "select_account");
        var queryBuilder = new QueryBuilder(parameters);
        QueryString queryString = queryBuilder.ToQueryString();
        uriBuilder.Query = queryString.Value;
        var result = uriBuilder.Uri.AbsoluteUri;


        Console.WriteLine("get sign in");
        return Redirect(result);
    }


    public async Task<IActionResult> OnPost()
    {
        Console.WriteLine("post");
        return Page();
    }

    public async Task<IActionResult> OnPostGoogleSignIn()
    {
        Console.WriteLine("post google sign in");
        return Page();
    }
}
