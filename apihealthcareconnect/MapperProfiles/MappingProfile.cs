using apihealthcareconnect.Models;
using apihealthcareconnect.ViewModel;
using AutoMapper;
namespace apihealthcareconnect.MapperProfiles;

public class SpecialtyTypeProfile : Profile
{
    public SpecialtyTypeProfile()
    {
        CreateMap<SpecialtyType, SpecialtyTypeViewModel>()
       .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.cd_specialty_type))
       .ForMember(dest => dest.description, opt => opt.MapFrom(src => src.ds_specialty_type))
       .ForMember(dest => dest.intervalBetweenAppointments, opt => opt.MapFrom(src => src.dt_interval_between_appointments));
    }
}
