using System.Dynamic;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.IdentityModel.JsonWebTokens;
namespace Services.Helper;


public class GoogleJwtPayloadModel
{
    [JsonPropertyName("iss")]
    public string? Iss { get; set; }

    [JsonPropertyName("azp")]
    public string? Azp { get; set; }

    [JsonPropertyName("aud")]
    public string? Aud { get; set; }

    [JsonPropertyName("sub")]
    public string? Sub { get; set; }

    [JsonPropertyName("hd")]
    public string? Hd { get; set; }

    [JsonPropertyName("email")]
    public string? Email { get; set; }

    [JsonPropertyName("email_verified")]
    public bool? EmailVerified { get; set; }

    [JsonPropertyName("at_hash")]
    public string? AtHash { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("picture")]
    public string? Picture { get; set; }

    [JsonPropertyName("given_name")]
    public string GivenName { get; set; }

    [JsonPropertyName("family_name")]
    public string FamilyName { get; set; }

    [JsonPropertyName("iat")]
    public long Iat { get; set; }

    [JsonPropertyName("exp")]
    public long Exp { get; set; }
}

public class JwtHelper
{
    public static GoogleJwtPayloadModel contertToGooglePayload(string token)
    {
        var handler = new JsonWebTokenHandler();
        var model = new GoogleJwtPayloadModel();

        try
        {
            var jwt = handler.ReadJsonWebToken(token);
            if (jwt.TryGetPayloadValue<string>("iss", out var iss))
            {
                model.Iss = iss;
            }

            if (jwt.TryGetPayloadValue<string>("azp", out var azp))
            {
                model.Azp = azp;
            }

            if (jwt.TryGetPayloadValue<string>("aud", out var aud))
            {
                model.Aud = aud;
            }

            if (jwt.TryGetPayloadValue<string>("sub", out var sub))
            {
                model.Sub = sub;
            }

            if (jwt.TryGetPayloadValue<string>("hd", out var hd))
            {
                model.Hd = hd;
            }

            if (jwt.TryGetPayloadValue<string>("email", out var email))
            {
                model.Email = email;
            }

            if (jwt.TryGetPayloadValue<bool>("email_verified", out var email_verified))
            {
                model.EmailVerified = email_verified;
            }

            if (jwt.TryGetPayloadValue<string>("at_hash", out var asHash))
            {
                model.AtHash = asHash;
            }

            if (jwt.TryGetPayloadValue<string>("picture", out var picture))
            {
                model.Picture = picture;
            }
            if (jwt.TryGetPayloadValue<string>("given_name", out var givenName))
            {
                model.GivenName = givenName;
            }
            if (jwt.TryGetPayloadValue<string>("family_name", out var familyName))
            {
                model.FamilyName = familyName;
            }
            if (jwt.TryGetPayloadValue<long>("iat", out var iat))
            {
                model.Iat = iat;
            }
            if (jwt.TryGetPayloadValue<long>("exp", out var exp))
            {
                model.Exp = exp;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }

        return model;



    }
}
