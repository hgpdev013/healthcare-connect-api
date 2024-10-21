using apihealthcareconnect.Models;
using apihealthcareconnect.ViewModel;
using AutoMapper;

namespace apihealthcareconnect.MapperProfiles
{
    public class UserTypeProfile : Profile
    {
        public UserTypeProfile()
        {
            CreateMap<UserType, UserTypeViewModel>()
           .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.cd_user_type))
           .ForMember(dest => dest.name, opt => opt.MapFrom(src => src.ds_user_type))
           .ForMember(dest => dest.isActive, opt => opt.MapFrom(src => src.is_active));
        }
    }
}
