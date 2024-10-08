


using System.Diagnostics.CodeAnalysis;
using Amazon.S3.Model;
using AutoMapper;
using Common.Enums;
using Common.Extensions;
using Common.Helper;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
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

    private readonly IGenericRepository<EmailVerificationToken> _emailVerifyTokenRepository;


    private readonly JwtSetting _jwtSetting;

    public AuthService(
        IMapper mapper,
        IOptions<JwtSetting> jwtSetting,
        IGenericRepository<AccessToken> accessTokenRepository,
        IGenericRepository<RefreshToken> refreshTokenRepository,
        IGenericRepository<EmailVerificationToken> emailVerifyTokenRepository,
        IHttpContextAccessor contextAccessor
        ) : base(contextAccessor)
    {

        _jwtSetting = jwtSetting.Value;
        _accessTokenRepository = accessTokenRepository;
        _refreshTokenRepository = refreshTokenRepository;
        _emailVerifyTokenRepository = emailVerifyTokenRepository;
        // var httpContext = contextAccessor.HttpContext!;
        // _accessTokenRepository = httpContext.RequestServices.GetService<IGenericRepository<AccessToken>>()!;
        // _refreshTokenRepository = httpContext.RequestServices.GetService<IGenericRepository<RefreshToken>>()!;

        Console.WriteLine("init");
    }
    public async Task<RegisterDTO> SignIn(string email, string password)
    {
        var lastAccount = await Repository.ReadFirst(x => x.Email == email) ?? throw new NotFoundException("帳號或密碼錯誤");

        var lastSalt = lastAccount.Salt;

        var hashedInputPassword = HashHelper.Argon2Id(password, lastSalt ?? "");

        if (hashedInputPassword != lastAccount.HashPassword)
            throw new NotFoundException("帳號或密碼錯誤");

        var accountDTO = Mapper.Map<AccountDTO>(lastAccount);

        var tokens = await CreateAccessTokenAndWriteToDB(lastAccount);

        var result = new RegisterDTO
        {
            Account = accountDTO,
            AccessToken = tokens.accessToken,
            RefreshToken = tokens.refreshToken,
        };

        return result;
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


        var assessTokenGuid = Guid.NewGuid();
        var refreshTokenGuid = Guid.NewGuid();

        var dateNow = DateTime.UtcNow;
        var accessTokenExpiredSecond = dateNow.AddSeconds(_jwtSetting.AccessTokenExpireSeconds).ToUnixTimeSeconds();
        var refreshTokenExpiredSecond = dateNow.AddSeconds(_jwtSetting.RefreshTokenExpireSeconds).ToUnixTimeSeconds();



        var accessToken = CreateAccessToken(newAccount.Id, refreshTokenGuid.ToString(), "role");

        var refreshToken = CreateRefreshToken(newAccount.Id, refreshTokenGuid.ToString());

        await _accessTokenRepository.Create(new AccessToken { Account = newAccount, Token = assessTokenGuid, ExpireTime = accessTokenExpiredSecond, Status = Common.Enums.TokenStatus.Alive }, false);


        await _refreshTokenRepository.Create(new RefreshToken { Account = newAccount, Token = refreshTokenGuid, ExpireTime = accessTokenExpiredSecond, Status = Common.Enums.TokenStatus.Alive }, true);

        var accountDTO = Mapper.Map<AccountDTO>(newAccount);

        var registerDTO = new RegisterDTO
        {
            Account = accountDTO,
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };

        return registerDTO;

    }

    private async Task<(string accessToken, string refreshToken)> CreateAccessTokenAndWriteToDB(Account account)
    {

        var assessTokenGuid = Guid.NewGuid();
        var refreshTokenGuid = Guid.NewGuid();

        var dateNow = DateTime.UtcNow;
        var accessTokenExpiredSecond = dateNow.AddSeconds(_jwtSetting.AccessTokenExpireSeconds).ToUnixTimeSeconds();
        var refreshTokenExpiredSecond = dateNow.AddSeconds(_jwtSetting.RefreshTokenExpireSeconds).ToUnixTimeSeconds();

        var role = account.PermissionId == 1 ? "admin" : "user";

        var accessToken = CreateAccessToken(account.Id, assessTokenGuid.ToString(), role);

        var refreshToken = CreateRefreshToken(account.Id, refreshTokenGuid.ToString());

        await _accessTokenRepository.Create(new AccessToken { AccountId = account.Id, Token = assessTokenGuid, ExpireTime = accessTokenExpiredSecond, Status = Common.Enums.TokenStatus.Alive }, false);


        await _refreshTokenRepository.Create(new RefreshToken { AccountId = account.Id, Token = refreshTokenGuid, ExpireTime = refreshTokenExpiredSecond, Status = Common.Enums.TokenStatus.Alive }, true);


        return (accessToken, refreshToken);
    }

    private string CreateAccessToken(int id, string guid, string role)
    {
        return JwtHelper.GenerateToken(id, guid, _jwtSetting.AccessTokenExpireSeconds, _jwtSetting.Key, _jwtSetting.Issuer, role);
    }

    private string CreateRefreshToken(int id, string guid) => JwtHelper.GenerateToken(id, guid, _jwtSetting.RefreshTokenExpireSeconds, _jwtSetting.Key, _jwtSetting.Issuer);


    public async Task RevokeAccessToken(string accessTokenGuid)
    {
        var parseResult = Guid.TryParse(accessTokenGuid, out var convertedGuid);
        if (!parseResult)
            throw new NotFoundException("Access Token not found.");
        var lastToken = await _accessTokenRepository.ReadFirst(x => x.Token.ToString() == accessTokenGuid);

        if (lastToken == null)
        {
            return;
        }

        lastToken.Status = Common.Enums.TokenStatus.Revoked;

        await _accessTokenRepository.Update(lastToken);

    }

    public async Task<bool> IsValidAccessToken(string accessTokenGuid)
    {
        var parseResult = Guid.TryParse(accessTokenGuid, out var convertedGuid);
        if (!parseResult)
            throw new NotFoundException("Access Token not found.");
        var lastToken = await _accessTokenRepository.ReadFirst(x => x.Token.ToString() == accessTokenGuid);

        if (lastToken == null)
        {
            return false;
        }

        var nowTime = DateTime.UtcNow.ToUnixTimeSeconds();

        if (lastToken.Status == Common.Enums.TokenStatus.Revoked || lastToken.ExpireTime <= nowTime)
        {
            return false;
        }

        return true;

    }

    public async Task<List<AccountDTO>> GetAccounts()
    {
        var accounts = await Repository.ReadList(x => x.Id == 41);
        var accountDTOs = new List<AccountDTO>();
        foreach (var a in accounts)
        {
            a.EmailValidStatus = EmailVerificationStatus.valid;
            var accountDTO = Mapper.Map<AccountDTO>(a);
            accountDTOs.Add(accountDTO);
        }
        return accountDTOs;
    }

    public async Task<SendEmailDTO> CreateEmailVerifyToken(int uid)
    {
        var tokens = await _emailVerifyTokenRepository.ReadList(x => x.AccountId == uid && x.Status == TokenStatus.Alive);
        foreach (var token in tokens)
        {
            token.Status = TokenStatus.Revoked;
        }
        await _emailVerifyTokenRepository.UpdateRange(tokens);
        var emailTokenGuid = Guid.NewGuid();
        var addTimeString = DateTime.UtcNow.AddSeconds(_jwtSetting.EmailTokenExpireSeconds).ToUnixTimeSeconds();
        var emailToken = JwtHelper.GenerateToken(uid, emailTokenGuid.ToString(), _jwtSetting.EmailTokenExpireSeconds, _jwtSetting.Key, _jwtSetting.Issuer);
        await _emailVerifyTokenRepository.Create(new EmailVerificationToken { Token = emailTokenGuid, AccountId = uid, ExpireTime = addTimeString, Status = TokenStatus.Alive });

        return new SendEmailDTO { token = emailToken };
    }


    public async Task<object> VerifyEmail(string token)
    {

        var jwt = JwtHelper.DecodeTokenGuid(token);
        var uid = jwt.Uid;
        if (string.IsNullOrEmpty(jwt.Guid) || uid == null)
        {
            throw new NotFoundException("token exipred!");
        }

        var parseResult = Guid.TryParse(jwt.Guid, out var convertedGuid);
        if (parseResult == false)
        {
            throw new NotFoundException("token exipred!");
        }

        var lastToken = await _emailVerifyTokenRepository.ReadFirst(x => x.Token == convertedGuid) ?? throw new NotFoundException("token exipred!");

        var now = DateTime.UtcNow.ToUnixTimeSeconds();
        if (lastToken.Status == TokenStatus.Revoked || lastToken.ExpireTime <= now)
        {
            throw new NotFoundException("Your token has exipred! Please resend it.");
        }

        var lastAccount = await Repository.ReadFirstById(uid ?? 0) ?? throw new NotFoundException("token exipred!");

        if (lastAccount.AccountStatus == (AccountStatus.Disabled | AccountStatus.Deleted))
        {
            throw new NotFoundException("Your token has exipred! Please resend it.");
        }

        lastAccount.AccountStatus = AccountStatus.Verified;
        lastAccount.EmailValidStatus = EmailVerificationStatus.valid;
        await Repository.Update(lastAccount);

        return new { result = "ok", message = "Your email has already been verified." };
    }
}
