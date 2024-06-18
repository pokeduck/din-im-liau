
using Amazon.S3.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Models.DataModels;
using Models.Repositories;

namespace Services;

public class AccountService : BaseService<Account>
{

    public AccountService(IHttpContextAccessor contextAccessor) : base(contextAccessor)
    {
        Console.WriteLine("init");
    }

    public async Task<Account?> Get(string googleId)
    {
        var account = await Repository.ReadFirst(x => x.GoogleOpenId == googleId);
        return account;
    }

    public async Task<Account?> GetByAccountId(int accountId)
    {
        var account = await Repository.ReadFirstById(accountId);
        return account;
    }

    public async Task<Account> Create(string googleId, string nickName, string email, string thumbnailUrl)
    {
        var newAccount = new Account { GoogleOpenId = googleId, NickName = nickName, Email = email, ThumbnailUrl = thumbnailUrl, PermissionId = 2 };
        await Repository.Create(newAccount);
        return newAccount;
    }
}
