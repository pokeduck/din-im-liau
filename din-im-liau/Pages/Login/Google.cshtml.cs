using System.Text.Json;
using System.Text.Json.Serialization;
using din_im_liau.Page;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.ObjectPool;
using Services.Helper;
using Common.Extensions;

namespace din_im_liau.Pages.Login;

public class GoogleOAuthResponse
{
    public string state { get; set; }
    public string code { get; set; }
    public string hd { get; set; }

}

public partial class Oauth2GoogleTokenResponse
{
    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; }

    [JsonPropertyName("expires_in")]
    public long ExpiresIn { get; set; }

    [JsonPropertyName("refresh_token")]
    public string RefreshToken { get; set; }

    [JsonPropertyName("scope")]
    public Uri Scope { get; set; }

    [JsonPropertyName("token_type")]
    public string TokenType { get; set; }

    [JsonPropertyName("id_token")]
    public string IdToken { get; set; }
}

public class GoogleViewModel
{
    public string email { get; set; }
    public string fullName { get; set; }
    public string thumbnailUrl { get; set; }

    public string googleUserId { get; set; }
}
[AllowAnonymous]
public class GoogleModel : BasePageModel
{

    

    public GoogleViewModel GoogleViewModel { get; set; }

    private IConfiguration _config;
    public GoogleModel(IConfiguration config, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
    {
        _config = config;
    }
    public async Task<IActionResult> OnGet([FromQuery] GoogleOAuthResponse response)
    {
        if (!_httpContext.IsGoogleStateTokenValid(response.state))
        {
            return new RedirectToPageResult("/User/LoginError");
        }
        Console.WriteLine(response.ToString());
        var client = new HttpClient();
        client.BaseAddress = new Uri("https://oauth2.googleapis.com");

        var formData = new Dictionary<string, string>();
        formData.Add("client_id", _config["Google:ClientId"] ?? "");
        formData.Add("client_secret", _config["Google:ClientSecret"] ?? "");
        formData.Add("code", response.code);
        formData.Add("grant_type", "authorization_code");
        formData.Add("redirect_uri", "https://localhost:8888/login/google");

        using var authCodeResponse = await client.PostAsync("token", new FormUrlEncodedContent(formData));
        var responseBody = await authCodeResponse.Content.ReadAsStringAsync();

        var oauth2GoogleTokenResponse = JsonSerializer.Deserialize<Oauth2GoogleTokenResponse>(responseBody);



        var model = JwtHelper.contertToGooglePayload(oauth2GoogleTokenResponse?.IdToken ?? "");

        Console.WriteLine(model.Email);

        //RedirectToPage("GoogleVerifyToken");

        var vm = new GoogleViewModel
        {
            email = model.Email ?? "",
            fullName = model.GivenName ?? "" + model.FamilyName ?? "",
            googleUserId = model.Sub ?? "",
            thumbnailUrl = model.Picture ?? ""
        };
        await SignIn();
        GoogleViewModel = vm;

        var directPage = new RedirectToPageResult("/user/profile");
        var directPage2 = LocalRedirect("/user");
        return directPage2;
    }

    public async Task<IActionResult> OnPost([FromBody] object payload)
    {
        Console.WriteLine(payload);
        return Page();

    }
}
