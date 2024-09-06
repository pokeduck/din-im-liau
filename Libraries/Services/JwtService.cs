using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Common.Extensions;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Models.DataModels;
using Models.Settings;

namespace Services;

public class JwtService(IOptions<JwtSetting> jwtSetting)
{
    private readonly JwtSetting _jwtSetting = jwtSetting.Value;

    public string Generate(int uid)
    {
        var key = _jwtSetting.Key;
        var issuer = _jwtSetting.Issuer;
        var expSpanSeconds = _jwtSetting.AccessTokenExpireSeconds;

        if (string.IsNullOrWhiteSpace(key) || string.IsNullOrWhiteSpace(issuer))
            throw new ArgumentException("Jwt settings is empty.");

        var now = DateTime.UtcNow;
        var expTime = now.AddSeconds(expSpanSeconds);

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.Exp, expTime.ToUnixTimeSecondsString()),
            new("uid",uid.ToString()),
            new(JwtRegisteredClaimNames.Iat, now.ToUnixTimeSecondsString())
        };

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

        var tokenHandler = new JwtSecurityTokenHandler();
        var securityToken = tokenHandler.CreateToken(tokenDescriptor);
        var serializeToken = tokenHandler.WriteToken(securityToken);

        return serializeToken;
    }

    public string GenerateRefreshToken(string guid, int uid)
    {
        var key = _jwtSetting.Key;
        var issuer = _jwtSetting.Issuer;
        var expSpanSeconds = _jwtSetting.RefreshTokenExpireSeconds;

        if (string.IsNullOrWhiteSpace(key) || string.IsNullOrWhiteSpace(issuer))
            throw new ArgumentException("Jwt settings is empty.");

        var now = DateTime.UtcNow;
        var expTime = now.AddSeconds(expSpanSeconds);

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Jti,guid),
            new(JwtRegisteredClaimNames.Exp, expTime.ToUnixTimeSecondsString()),
            new("uid",uid.ToString()),
            new(JwtRegisteredClaimNames.Iat, now.ToUnixTimeSecondsString())
        };

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

        var tokenHandler = new JwtSecurityTokenHandler();
        var securityToken = tokenHandler.CreateToken(tokenDescriptor);
        var serializeToken = tokenHandler.WriteToken(securityToken);

        return serializeToken;
    }

}
