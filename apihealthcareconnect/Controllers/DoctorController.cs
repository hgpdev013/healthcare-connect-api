using apihealthcareconnect.Interfaces;
using apihealthcareconnect.Models;
using apihealthcareconnect.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace apihealthcareconnect.Controllers
{
    [ApiController]
    [Route("api/doctors")]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly IUsersRepository _usersRepository;

        public DoctorController(IDoctorRepository doctorRepository, IUsersRepository usersRepository)
        {
            _doctorRepository = doctorRepository ?? throw new ArgumentNullException();
            _usersRepository = usersRepository ?? throw new ArgumentNullException();
        }


        [HttpGet]
        [ProducesResponseType(typeof(List<Users>), 200)]
        public async Task<IActionResult> GetDoctors()
        {
            var doctors = await _doctorRepository.GetAll();
            return Ok(doctors);
        }

        [HttpPost]
        public async Task<IActionResult> PostDoctors(UsersDoctorsRequestViewModel UserDoctorsParams)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userToCreate = new Users(UserDoctorsParams.id,
                UserDoctorsParams.cpf,
                UserDoctorsParams.documentNumber,
                UserDoctorsParams.name,
                UserDoctorsParams.dateOfBirth,
                UserDoctorsParams.email,
                UserDoctorsParams.cellphone,
                UserDoctorsParams.email,
                UserDoctorsParams.userTypeId,
                UserDoctorsParams.streetName,
                UserDoctorsParams.streetNumber,
                UserDoctorsParams.complement,
                UserDoctorsParams.state,
                UserDoctorsParams.cep,
                UserDoctorsParams.city,
                UserDoctorsParams.gender,
                UserDoctorsParams.isActive,
                null);

            var user = await _usersRepository.Add(userToCreate);

            if(user == null)
            {
                return BadRequest("Erro ao cadastrar usuário");
            }

            var doctorToCreate = new Doctors(UserDoctorsParams.doctorData.crm,
                user.cd_user,
                UserDoctorsParams.doctorData.specialtyTypeId);

            var doctor = await _doctorRepository.Add(doctorToCreate);

            if (doctor == null)
            {
                return BadRequest("Erro ao relacionar usuário como médico");
            }

            return Ok(new { user, doctorData = doctor });
        }
    }
}
