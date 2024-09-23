using System.Dynamic;
using System.Security.Claims;
using System.Text.Json;
using System.Text.Json.Serialization;
using Common.Extensions;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;

// using System.IdentityModel.Tokens.Jwt;
// using System.Security.Claims;
// using System.Security.Cryptography;
// using Common.Extensions;
// using Microsoft.Extensions.Options;
// using Microsoft.IdentityModel.Tokens;


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
    public static (string? Guid, int? Uid) DecodeTokenGuid(string token)
    {
        var jwt = new JsonWebTokenHandler().ReadJsonWebToken(token);
        jwt.TryGetPayloadValue<string>("jti", out var guid);
        jwt.TryGetPayloadValue<string>("uid", out var uid);
        if (int.TryParse(uid, out var parsedUid))
        {
            return (guid, parsedUid);
        }
        return (guid, null);

    }

    public static string GenerateToken(int uid, string guidString, long expSpanSeconds, string key, string issuer, string? role = null)
    {


        if (string.IsNullOrWhiteSpace(key) || string.IsNullOrWhiteSpace(issuer))
            throw new ArgumentException("Jwt settings is empty.");

        var now = DateTime.UtcNow;
        var expTime = now.AddSeconds(expSpanSeconds);

        var localGuidString = guidString ?? Guid.NewGuid().ToString();

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Jti,localGuidString),
            new(JwtRegisteredClaimNames.Exp, expTime.ToUnixTimeSecondsString()),
            new("uid",uid.ToString()),
            new(JwtRegisteredClaimNames.Iat, now.ToUnixTimeSecondsString())
        };
        if (!string.IsNullOrEmpty(role))
        {
            claims.Add(new("role", role));
        }

        var newUserClaimsIdentity = new ClaimsIdentity(claims);

        var securityKey = new SymmetricSecurityKey(key.Bytes());

        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = issuer,
            Subject = newUserClaimsIdentity,
            Expires = expTime,
            SigningCredentials = signingCredentials
        };




        var tokenHandler = new JsonWebTokenHandler();
        var securityToken = tokenHandler.CreateToken(tokenDescriptor);
        // var serializeToken = tokenHandler.EncryptToken(securityToken);

        return securityToken;
    }

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
