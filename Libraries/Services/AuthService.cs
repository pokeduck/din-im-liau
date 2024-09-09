


using Amazon.S3.Model;
using AutoMapper;
using Common.Extensions;
using Common.Helper;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Validations;
using Models.DataModels;
using Models.DTOs;
using Models.Exceptions;
using Models.Repositories;
using Models.Settings;
using Models.ViewModels;
using Services.Helper;

namespace Services;

public class AuthService : BaseService<Account>
{

    private readonly IGenericRepository<AccessToken> _accessTokenRepository;
    private readonly IGenericRepository<RefreshToken> _refreshTokenRepository;

    private readonly JwtSetting _jwtSetting;

    public AuthService(
        IMapper mapper,
        IOptions<JwtSetting> jwtSetting,
        IGenericRepository<AccessToken> accessTokenRepository,
        IGenericRepository<RefreshToken> refreshTokenRepository,
        IHttpContextAccessor contextAccessor
        ) : base(contextAccessor)
    {

        _jwtSetting = jwtSetting.Value;
        _accessTokenRepository = accessTokenRepository;
        _refreshTokenRepository = refreshTokenRepository;
        // var httpContext = contextAccessor.HttpContext!;
        // _accessTokenRepository = httpContext.RequestServices.GetService<IGenericRepository<AccessToken>>()!;
        // _refreshTokenRepository = httpContext.RequestServices.GetService<IGenericRepository<RefreshToken>>()!;

        Console.WriteLine("init");
    }

    public async Task<RegisterDTO> Create(string nickname, string email, string password)
    {
        //1.check email exist
        //2.hashed password
        //3.create account
        //4.create & write refresh and access token
        //5.return dto

        var lastAccount = await Repository.ReadFirst(x => x.Email == email);
        if (lastAccount != null)
            throw new BadRequestException($"The Email [{email}] already exists.", Common.Enums.ResultErrorCode.ResourceAlreadyExist);

        var salt = SaltHelper.GenerateN();
        var hashedPassword = HashHelper.Argon2Id(password, salt);

        var assessTokenGuid = Guid.NewGuid();
        var refreshTokenGuid = Guid.NewGuid();

        var newAccount = new Account
        {
            Nickname = nickname,
            Email = email,
            EmailValidStatus = Common.Enums.EmailVerificationStatus.invalid,
            Salt = salt,
            HashPassword = hashedPassword,
            PermissionId = 2,
            AccountStatus = Common.Enums.AccountStatus.Unverified,
        };

        await Repository.Create(newAccount, true);

        var dateNow = DateTime.UtcNow;
        var accessTokenExpiredSecond = dateNow.AddSeconds(_jwtSetting.AccessTokenExpireSeconds).ToUnixTimeSeconds();
        var refreshTokenExpiredSecond = dateNow.AddSeconds(_jwtSetting.RefreshTokenExpireSeconds).ToUnixTimeSeconds();

        var accessToken = JwtHelper.GenerateToken(newAccount.Id, assessTokenGuid.ToString(), _jwtSetting.AccessTokenExpireSeconds, _jwtSetting.Key, _jwtSetting.Issuer);

        var refreshToken = JwtHelper.GenerateToken(newAccount.Id, refreshTokenGuid.ToString(), _jwtSetting.RefreshTokenExpireSeconds, _jwtSetting.Key, _jwtSetting.Issuer);

        await _accessTokenRepository.Create(new AccessToken { Account = newAccount, Token = assessTokenGuid, ExpireTime = accessTokenExpiredSecond }, false);


        await _refreshTokenRepository.Create(new RefreshToken { Account = newAccount, Token = refreshTokenGuid, ExpireTime = accessTokenExpiredSecond }, true);

        var accountDTO = Mapper.Map<AccountDTO>(newAccount);

        var registerDTO = new RegisterDTO
        {
            Account = accountDTO,
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };

        return registerDTO;

    }

}
