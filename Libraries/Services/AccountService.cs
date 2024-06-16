
using Microsoft.AspNetCore.Http;
using Models.DataModels;
using Models.Repositories;

namespace Services;

public class AccountService : BaseService<Account>
{
    public AccountService(IHttpContextAccessor contextAccessor) : base(contextAccessor)
    {
        Console.WriteLine("init");

    }
}
