using apihealthcareconnect.Interfaces;
using apihealthcareconnect.Models;
using apihealthcareconnect.ResponseMappings;
using apihealthcareconnect.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace apihealthcareconnect.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/specialties")]
    public class SpecialtyTypeController : ControllerBase
    {
        private readonly ISpecialtyTypeRepository _specialtyTypeRepository;
        private UserResponseMapping _userResponseMapping;

        public SpecialtyTypeController(ISpecialtyTypeRepository specialtyTypeRepository, UserResponseMapping userResponseMapping)
        {
            _specialtyTypeRepository = specialtyTypeRepository ?? throw new ArgumentNullException();
            _userResponseMapping = userResponseMapping ?? throw new ArgumentNullException();
        }


        [HttpGet]
        [ProducesResponseType(typeof(List<SpecialtyTypeViewModel>), 200)]
        public async Task<IActionResult> GetSpecialties()
        {
            var specialties = await _specialtyTypeRepository.GetAll();

            var specialtiesFormatted = specialties.Select(s => _userResponseMapping.MapSpecialtyType(s)).ToList();

            return Ok(specialtiesFormatted);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(SpecialtyTypeViewModel), 200)]
        public async Task<IActionResult> GetSpecialtyById(int id)
        {
            var specialtyById = await _specialtyTypeRepository.GetById(id);
            if (specialtyById == null)
            {
                return NotFound("Especialidade não encontrada");
            }

            var specialtyFormatted = _userResponseMapping.MapSpecialtyType(specialtyById);

            return Ok(specialtyFormatted);
        }

        [HttpPost]
        [ProducesResponseType(typeof(SpecialtyTypeViewModel), 201)]
        public async Task<IActionResult> PostSpecialties(SpecialtyTypeViewModel specialtyTypeParams)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(specialtyTypeParams.GetType()
                .GetProperties()
                .Where(p => p.PropertyType == typeof(string))
                .Any(p => string.IsNullOrEmpty((string)p.GetValue(specialtyTypeParams))))
            {
                return BadRequest("Todos os campos devem ser preenchidos.");
            }

            var specialty = new SpecialtyType(null,
                specialtyTypeParams.description,
                specialtyTypeParams.intervalBetweenAppointments,
                specialtyTypeParams.isActive);

            var createdSpecialty = await _specialtyTypeRepository.Add(specialty);

            var createdSpecialtyFormatted = _userResponseMapping.MapSpecialtyType(createdSpecialty);

            return Ok(createdSpecialtyFormatted);
        }

        [HttpPut]
        [ProducesResponseType(typeof(SpecialtyTypeViewModel), 200)]
        public async Task<IActionResult> PutSpecialties(SpecialtyTypeViewModel specialtyTypeParams)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var specialtyToUpdate = await _specialtyTypeRepository.GetById(specialtyTypeParams.id);

            if (specialtyToUpdate == null)
            {
                return NotFound("Especialidade não encontrada");
            }

            specialtyToUpdate.cd_specialty_type = specialtyTypeParams.id;
            specialtyToUpdate.ds_specialty_type = specialtyTypeParams.description;
            specialtyToUpdate.dt_interval_between_appointments = specialtyTypeParams.intervalBetweenAppointments;
            specialtyToUpdate.is_active = specialtyTypeParams.isActive;

            var updatedSpecialty = await _specialtyTypeRepository.Update(specialtyToUpdate);

            var updatedSpecialtyFormatted = _userResponseMapping.MapSpecialtyType(updatedSpecialty);

            return Ok(updatedSpecialtyFormatted);
        }
    }
}
