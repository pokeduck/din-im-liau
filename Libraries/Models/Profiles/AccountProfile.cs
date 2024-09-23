using AutoMapper;
using AutoMapper.Execution;
using Common.Enums;
using Models.DataModels;
using Models.DTOs;

namespace Models.Profiles;



public class AccountProfile : Profile
{
    public AccountProfile()
    {
        CreateMap<Account, AccountDTO>()
        .ForMember(dest => dest.Uid, src => src.MapFrom(o => o.Id))
        .ForMember(dest => dest.Nickname, src => src.MapFrom(o => o.Nickname))
        .ForMember(dest => dest.Email, src => src.MapFrom(o => o.Email))
        .ForMember(dest => dest.IsEmailVerified, src => src.MapFrom(o => o.EmailValidStatus == Common.Enums.EmailVerificationStatus.valid))
        .ForMember(dest => dest.AccountStatus, src => src.MapFrom(o => (int)o.AccountStatus))
        .ForMember(dest => dest.AccountStatusText, src => src.MapFrom<AccountStatusResolver>());
        ;

    }
}

public class AccountStatusResolver : IValueResolver<Account, AccountDTO, string?>
{

    public string? Resolve(Account source, AccountDTO destination, string destMember, ResolutionContext context)
    {
        var result = source.AccountStatus switch
        {
            Common.Enums.AccountStatus.Verified => "已驗證",
            Common.Enums.AccountStatus.Unverified => "未驗證",
            Common.Enums.AccountStatus.Deleted => "已刪除",
            Common.Enums.AccountStatus.Disabled => "已停用",
        };
        return result;
    }
}
