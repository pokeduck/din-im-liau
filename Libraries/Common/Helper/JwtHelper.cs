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
        var jwt = handler.ReadJsonWebToken(token);

        var model = new GoogleJwtPayloadModel
        {
            Iss = jwt.GetPayloadValue<string>("iss"),
            Azp = jwt.GetPayloadValue<string>("azp"),
            Aud = jwt.GetPayloadValue<string>("azp"),
            Sub = jwt.GetPayloadValue<string>("sub"),
            Hd = jwt.TryGetPayloadValue<string>("hd") ?? jwt.GetPayloadValue<string>("hd"),
            Email = jwt.GetPayloadValue<string>("email"),
            EmailVerified = jwt.GetPayloadValue<bool>("email_verified"),
            AtHash = jwt.GetPayloadValue<string>("at_hash"),
            GivenName = jwt.GetPayloadValue<string>("given_name"),
            FamilyName = jwt.GetPayloadValue<string>("family_name"),
            Iat = jwt.GetPayloadValue<long>("lat"),
            Exp = jwt.GetPayloadValue<long>("exp"),
        };
        return model;
    }
}
