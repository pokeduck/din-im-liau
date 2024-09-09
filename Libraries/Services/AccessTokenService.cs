using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Models.DataModels;
using Models.Settings;

namespace Services;

public class AccessTokenService : BaseService<AccessToken>
{
    public JwtSetting _jwtSetting;
    public JwtService _jwtService;
    public AccessTokenService(JwtService jwtService, IOptions<JwtSetting> jwtSetting, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
    {
        _jwtSetting = jwtSetting.Value;
        _jwtService = jwtService;
    }

    public async void Create(Account account, Guid guid, long expTime)
    {
        var accessToken = new AccessToken
        {
            Token = guid,
            AccountId = account.Id,
            Status = Common.Enums.TokenStatus.Alive,
            ExpireTime = expTime,
        };
        await Repository.Create(accessToken);
    }

}
