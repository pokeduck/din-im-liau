

using Common.Enums;
using Common.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Models.DataModels;
using Models.Settings;
using Services;

namespace Services;

public class RefreshTokenService : BaseService<RefreshToken>
{

    private readonly JwtSetting _jwtSetting;
    public RefreshTokenService(IHttpContextAccessor contextAccessor, IOptions<JwtSetting> jwtSetting) : base(contextAccessor)
    {
        _jwtSetting = jwtSetting.Value;
        Console.WriteLine("init");

    }

    public async Task<RefreshToken?> GetTokenByGuidString(string guid)
    {
        Guid convertedGuid;
        if (Guid.TryParse(guid, out convertedGuid))
        {
            return null;
        }

        var lastToken = await Repository.ReadFirst(x => x.Token == convertedGuid);
        if (lastToken == null)
            return null;

        if (lastToken.Status != TokenStatus.alive)
        {
            return null;
        }

        var appendedExpTime = lastToken.CreateTime + _jwtSetting.RefreshTokenExpireSeconds;
        var nowSecond = DateTime.UtcNow.ToUnixTimeSeconds();
        if (lastToken.CreateTime >= nowSecond)
        {
            lastToken.Status = TokenStatus.revoked;
            await Repository.Update(lastToken);
            return null;
        }

        return lastToken;

    }

    public async Task Create(Guid guid, int uid)
    {
        await Repository.Create(new RefreshToken { Token = guid, AccountId = uid });
    }
}
