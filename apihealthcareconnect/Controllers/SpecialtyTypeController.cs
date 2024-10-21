using apihealthcareconnect.Interfaces;
using apihealthcareconnect.Models;
using apihealthcareconnect.ViewModel;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace apihealthcareconnect.Controllers
{
    [ApiController]
    [Route("api/specialties")]
    public class SpecialtyTypeController : ControllerBase
    {
        private readonly ISpecialtyTypeRepository _specialtyTypeRepository;
        private readonly IMapper _mapper;

        public SpecialtyTypeController(ISpecialtyTypeRepository specialtyTypeRepository, IMapper mapper)
        {
            _specialtyTypeRepository = specialtyTypeRepository ?? throw new ArgumentNullException();
            _mapper = mapper;
        }

        
        [HttpGet]
        [ProducesResponseType(typeof(List<SpecialtyTypeViewModel>), 200)]
        public IActionResult GetSpecialties()
        {
            var specialties = _specialtyTypeRepository.GetAll().OrderBy(s => s.ds_specialty_type).ToList();
            var specialtiesViewModel = _mapper.Map<List<SpecialtyTypeViewModel>>(specialties);
            return Ok(specialtiesViewModel);
        }

        [HttpPost]
        [ProducesResponseType(typeof(SpecialtyTypeViewModel), 201)]
        public IActionResult PostSpecialties(SpecialtyTypeViewModel specialtyTypeViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var specialty = new SpecialtyType(null,
                specialtyTypeViewModel.description,
                specialtyTypeViewModel.intervalBetweenAppointments,
                specialtyTypeViewModel.isActive);
            _specialtyTypeRepository.Add(specialty);
            specialtyTypeViewModel = _mapper.Map<SpecialtyTypeViewModel>(specialty);
            return Ok(specialtyTypeViewModel);
        }

        [HttpPut]
        [ProducesResponseType(typeof(SpecialtyTypeViewModel), 200)]
        public IActionResult PutSpecialties(SpecialtyTypeViewModel specialtyTypeViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedSpecialty = new SpecialtyType(specialtyTypeViewModel.id,
                specialtyTypeViewModel.description,
                specialtyTypeViewModel.intervalBetweenAppointments,
                specialtyTypeViewModel.isActive);
            _specialtyTypeRepository.Update(updatedSpecialty);
            specialtyTypeViewModel = _mapper.Map<SpecialtyTypeViewModel>(updatedSpecialty);
            return Ok(specialtyTypeViewModel);
        }
    }
}
