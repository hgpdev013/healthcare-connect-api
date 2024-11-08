using apihealthcareconnect.Interfaces;
using apihealthcareconnect.Models;
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

        public SpecialtyTypeController(ISpecialtyTypeRepository specialtyTypeRepository)
        {
            _specialtyTypeRepository = specialtyTypeRepository ?? throw new ArgumentNullException();
        }


        [HttpGet]
        [ProducesResponseType(typeof(List<SpecialtyType>), 200)]
        public async Task<IActionResult> GetSpecialties()
        {
            var specialties = await _specialtyTypeRepository.GetAll();
            return Ok(specialties);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(SpecialtyType), 200)]
        public async Task<IActionResult> GetSpecialtyById(int id)
        {
            var specialtyById = await _specialtyTypeRepository.GetById(id);
            if (specialtyById == null)
            {
                return NotFound("Especialidade não encontrada");
            }
            return Ok(specialtyById);
        }

        [HttpPost]
        [ProducesResponseType(typeof(SpecialtyType), 201)]
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
            return Ok(createdSpecialty);
        }

        [HttpPut]
        [ProducesResponseType(typeof(SpecialtyType), 200)]
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
            return Ok(updatedSpecialty);
        }
    }
}
