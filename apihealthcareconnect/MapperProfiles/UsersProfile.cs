using apihealthcareconnect.Models;
using apihealthcareconnect.ViewModel;
using AutoMapper;

namespace apihealthcareconnect.MapperProfiles
{
    public class UsersProfile : Profile
    {
        public UsersProfile()
        {
            CreateMap<Users, UsersViewModel>()
           .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.cd_user))
           .ForMember(dest => dest.name, opt => opt.MapFrom(src => src.nm_user))
           .ForMember(dest => dest.cpf, opt => opt.MapFrom(src => src.cd_cpf))
           .ForMember(dest => dest.documentNumber, opt => opt.MapFrom(src => src.cd_identification))
           .ForMember(dest => dest.dateOfBirth, opt => opt.MapFrom(src => src.dt_birth))
           .ForMember(dest => dest.email, opt => opt.MapFrom(src => src.ds_email))
           .ForMember(dest => dest.cellphoneNumber, opt => opt.MapFrom(src => src.ds_cellphone))
           .ForMember(dest => dest.login, opt => opt.MapFrom(src => src.ds_login))
           .ForMember(dest => dest.userTypeId, opt => opt.MapFrom(src => src.cd_user_type))
           .ForMember(dest => dest.streetName, opt => opt.MapFrom(src => src.nm_street))
           .ForMember(dest => dest.streetNumber, opt => opt.MapFrom(src => src.cd_street_number))
           .ForMember(dest => dest.complement, opt => opt.MapFrom(src => src.ds_complement))
           .ForMember(dest => dest.neighborhood, opt => opt.MapFrom(src => src.ds_neighborhood))
           .ForMember(dest => dest.state, opt => opt.MapFrom(src => src.nm_state))
           .ForMember(dest => dest.cep, opt => opt.MapFrom(src => src.cd_cep));
        }
    }
}
