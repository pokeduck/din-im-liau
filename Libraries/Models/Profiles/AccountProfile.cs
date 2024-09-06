using AutoMapper;
using Models.DataModels;
using Models.DTOs;

namespace Models.Profiles;



public class AccountProfile : Profile
{
    public AccountProfile()
    {
        CreateMap<Account, AccountDTO>()
        .ForMember(dest => dest.Uid, src => src.MapFrom(o => o.Id))
        .ForMember(dest => dest.Nickname, src => src.MapFrom(o => o.NickName))
        .ForMember(dest => dest.IsEmailVerified, src => src.MapFrom(o => o.EmailValidStatus == Common.Enums.EmailVerificationStatus.valid));
        
    }
}
