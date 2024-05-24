using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Helper;

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

public class GoogleModel : PageModel
{
    public GoogleModel() { }

    public async Task<IActionResult> OnGet([FromQuery] GoogleOAuthResponse response)
    {
        Console.WriteLine(response.ToString());
        var client = new HttpClient();
        client.BaseAddress = new Uri("https://oauth2.googleapis.com");

        var formData = new Dictionary<string, string>();
        formData.Add("client_id", "697611657905-ide2j8m6bav95l0kcn45kn1lra6c7v3l.apps.googleusercontent.com");
        formData.Add("client_secret", "");
        formData.Add("code", response.code);
        formData.Add("grant_type", "authorization_code");
        formData.Add("redirect_uri", "https://localhost:8888/login/google");

        using var authCodeResponse = await client.PostAsync("token", new FormUrlEncodedContent(formData));
        var responseBody = await authCodeResponse.Content.ReadAsStringAsync();

        var oauth2GoogleTokenResponse = JsonSerializer.Deserialize<Oauth2GoogleTokenResponse>(responseBody);



        var model = JwtHelper.contertToGooglePayload(oauth2GoogleTokenResponse?.IdToken ?? "");

        Console.WriteLine(model.Email);

        //RedirectToPage("GoogleVerifyToken");
        return Page();

    }

    public async Task<IActionResult> OnPost([FromBody] object payload)
    {
        Console.WriteLine(payload);
        return Page();

    }
}
