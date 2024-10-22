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

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Users), 200)]
        public async Task<IActionResult> GetDoctorById(int id)
        {
            var doctor = await _doctorRepository.GetById(id);
            return Ok(doctor);
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
                UserDoctorsParams.neighborhood,
                UserDoctorsParams.isActive,
                null);

            var user = await _usersRepository.Add(userToCreate);

            if(user == null)
            {
                return BadRequest("Erro ao cadastrar usuário");
            }

            var doctorToCreate = new Doctors(UserDoctorsParams.doctorData.crm,
                user.cd_user,
                UserDoctorsParams.doctorData.specialtyTypeId,
                UserDoctorsParams.doctorData.observation);

            var doctor = await _doctorRepository.Add(doctorToCreate);

            if (doctor == null)
            {
                return BadRequest("Erro ao relacionar usuário como médico");
            }

            return Ok(new { user, doctorData = doctor });
        }

        [HttpPut]
        public async Task<IActionResult> PutDoctors(UsersDoctorsRequestViewModel UserDoctorsParams)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userToBeEdited = await _doctorRepository.GetById(UserDoctorsParams.id!.Value);

            if (userToBeEdited == null)
            {
                return NotFound("Usuário não encontrado");
            }

            userToBeEdited.cd_user = UserDoctorsParams.id;
            userToBeEdited.cd_cpf = UserDoctorsParams.cpf;
            userToBeEdited.cd_identification = UserDoctorsParams.documentNumber;
            userToBeEdited.nm_user = UserDoctorsParams.name;
            userToBeEdited.dt_birth = UserDoctorsParams.dateOfBirth;
            userToBeEdited.ds_email = UserDoctorsParams.email;
            userToBeEdited.ds_cellphone = UserDoctorsParams.cellphone;
            userToBeEdited.ds_login = UserDoctorsParams.email;
            userToBeEdited.cd_user_type = UserDoctorsParams.userTypeId;
            userToBeEdited.nm_street = UserDoctorsParams.streetName;
            userToBeEdited.cd_street_number = UserDoctorsParams.streetNumber;
            userToBeEdited.ds_complement = UserDoctorsParams.complement;
            userToBeEdited.nm_state = UserDoctorsParams.state;
            userToBeEdited.cd_cep = UserDoctorsParams.cep;
            userToBeEdited.nm_city = UserDoctorsParams.city;
            userToBeEdited.ds_gender = UserDoctorsParams.gender;
            userToBeEdited.ds_neighborhood = UserDoctorsParams.neighborhood;
            userToBeEdited.is_active = UserDoctorsParams.isActive;

            var editedUser = await _usersRepository.Update(userToBeEdited);

            userToBeEdited.doctorData.cd_crm = UserDoctorsParams.doctorData.crm;
            userToBeEdited.doctorData.cd_specialty_type = UserDoctorsParams.doctorData.specialtyTypeId;
            userToBeEdited.doctorData.cd_user = UserDoctorsParams.id;
            userToBeEdited.doctorData.ds_observation = UserDoctorsParams.doctorData.observation;

            var editedDoctor = await _doctorRepository.Update(userToBeEdited.doctorData);

            return Ok(new { user = editedUser, doctorData = editedDoctor });
        }
    }
}
