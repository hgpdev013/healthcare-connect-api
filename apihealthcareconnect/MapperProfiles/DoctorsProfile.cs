using apihealthcareconnect.Models;
using apihealthcareconnect.ViewModel;
using AutoMapper;

namespace apihealthcareconnect.MapperProfiles
{
    public class DoctorsProfile : Profile
    {
        public DoctorsProfile()
        {
            CreateMap<Doctors, DoctorsViewModel>()
            .ForMember(dest => dest.userId, opt => opt.MapFrom(src => src.cd_user))
            .ForMember(dest => dest.doctorId, opt => opt.MapFrom(src => src.cd_doctor))
            .ForMember(dest => dest.specialtyTypeId, opt => opt.MapFrom(src => src.cd_specialty_type))
            .ForMember(dest => dest.crm, opt => opt.MapFrom(src => src.cd_crm))
            .ForMember(dest => dest.observation, opt => opt.MapFrom(src => src.ds_observation));
        }
    }
}
