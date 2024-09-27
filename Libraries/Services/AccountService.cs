
using Common.Helper;
using Microsoft.AspNetCore.Http;
using Models.DataModels;
using Models.DTOs;
using Models.Exceptions;
using Models.Repositories;
using Models.Requests;

namespace Services;

public class AccountService : BaseService<Account>
{
    private readonly IGenericRepository<EmailVerificationToken> _emailVerifyTokenRepository;

    public AccountService(IHttpContextAccessor contextAccessor, IGenericRepository<EmailVerificationToken> emailVerifyTokenRepository) : base(contextAccessor)
    {
        _emailVerifyTokenRepository = emailVerifyTokenRepository;

        Console.WriteLine("init");
    }

    public async Task<Account?> Get(string googleId)
    {
        var account = await Repository.ReadFirst(x => x.GoogleOpenId == googleId);

        return account;
    }

    public async Task<Account?> GetAccountById(int accountId)
    {
        var account = await Repository.ReadFirstById(accountId);
        return account;
    }

    public async Task<AccountDTO?> GetAccountDTOById(int accountId)
    {
        var account = await Repository.ReadFirstById(accountId);
        var resultDTO = Mapper.Map<AccountDTO>(account);
        return resultDTO;
    }



    public async Task<AccountDTO> Create(string email, string nickname, string password)
    {
        var lastAccount = await Repository.ReadFirst(x => x.Email == email);
        if (lastAccount != null)
        {
            throw new BadRequestException($"The Email [{email}] already exists.", Common.Enums.ResultErrorCode.ResourceAlreadyExist);
        }

        var newSalt = SaltHelper.GenerateN();
        var hashPwd = HashHelper.Argon2Id(password, newSalt);
        var newAcc = new Account { Email = email, Nickname = nickname, ThumbnailUrl = null, GoogleOpenId = "", EmailValidStatus = Common.Enums.EmailVerificationStatus.invalid, Salt = newSalt, HashPassword = hashPwd, PermissionId = 2 };
        await Repository.Create(newAcc);
        var newAccWrited = await Repository.ReadFirst(x => x.Email == email);
        if (newAccWrited == null)
        {
            throw new BadRequestException("internal error.");
        }
        var accountDTO = Mapper.Map<AccountDTO>(newAccWrited);
        //var isEmailValid = (newAccWrited.EmailValidStatus == Common.Enums.EmailVerificationStatus.invalid) ? false : true;

        return accountDTO;
    }

    public async Task<Account> Create(string googleId, string nickName, string email, string thumbnailUrl)
    {
        var lastAccount = await Repository.ReadFirst(x => x.Email == email);
        if (lastAccount != null)
        {
            throw new BadRequestException($"The Email [{email}] already exists.", Common.Enums.ResultErrorCode.ResourceAlreadyExist);
        }

        var newAccount = new Account { GoogleOpenId = googleId, Nickname = nickName, Email = email, ThumbnailUrl = thumbnailUrl, PermissionId = 2, EmailValidStatus = Common.Enums.EmailVerificationStatus.valid };
        await Repository.Create(newAccount);
        return newAccount;
    }

    public async Task UpdateNickName(int id, string nickName)
    {
        var account = await Repository.ReadFirstById(id);
        if (account == null)
        {
            return;
        }
        account.Nickname = nickName;

        await Repository.Update(account);
    }

    public async Task<AccountDTO> UpdateAccount(int uid, UpdateProfileRequest profile)
    {
        var lastAccount = await Repository.ReadFirstById(uid) ?? throw new NotFoundException("User not found.");
        if (profile.Email != null && profile.Email != lastAccount.Email)
        {
            var lastSameEmailAccount = await Repository.ReadFirst(x => x.Email == profile.Email);
            if (lastSameEmailAccount != null)
            {
                throw new BadRequestException("Email Alread used.");
            }
            lastAccount.Email = profile.Email;
            lastAccount.EmailValidStatus = Common.Enums.EmailVerificationStatus.invalid;
        }

        if (profile.Name != null)
        {
            lastAccount.Nickname = profile.Name;
        }

        await Repository.Update(lastAccount);

        return Mapper.Map<AccountDTO>(lastAccount);

    }
}
