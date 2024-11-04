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
            var doctors = await _usersRepository.GetByUserTypeId(1);
            return Ok(doctors);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Users), 200)]
        public async Task<IActionResult> GetDoctorById(int id)
        {
            var doctor = await _usersRepository.GetById(id);

            if(doctor.cd_user_type != 1)
            {
                return BadRequest("Usuário não é médico");
            }

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
                //UserDoctorsParams.userTypeId,
                1,
                UserDoctorsParams.streetName,
                UserDoctorsParams.streetNumber,
                UserDoctorsParams.complement,
                UserDoctorsParams.state,
                UserDoctorsParams.cep,
                UserDoctorsParams.city,
                UserDoctorsParams.gender,
                UserDoctorsParams.neighborhood,
                UserDoctorsParams.isActive);

            var createdUser = await _usersRepository.Add(userToCreate);

            if (createdUser == null)
            {
                return BadRequest("Erro ao cadastrar usuário");
            }

            var doctorToCreate = new Doctors(UserDoctorsParams.doctorData.crm,
                createdUser.cd_user,
                UserDoctorsParams.doctorData.specialtyTypeId,
                UserDoctorsParams.doctorData.observation);

            var createdDoctor = await _doctorRepository.Add(doctorToCreate);

            if (createdDoctor == null)
            {
                return BadRequest("Erro ao relacionar usuário como médico");
            }

            createdUser.doctorData = createdDoctor;

            return Ok(createdUser);
        }

        [HttpPut]
        public async Task<IActionResult> PutDoctors(UsersDoctorsRequestViewModel UserDoctorsParams)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userToBeEdited = await _usersRepository.GetById(UserDoctorsParams.id!.Value);

            if (userToBeEdited == null)
            {
                return NotFound("Usuário não encontrado");
            }

            if(userToBeEdited.cd_user_type != 1)
            {
                return BadRequest("Usuário não é médico");
            }

            userToBeEdited.cd_user = UserDoctorsParams.id;
            userToBeEdited.cd_cpf = UserDoctorsParams.cpf;
            userToBeEdited.cd_identification = UserDoctorsParams.documentNumber;
            userToBeEdited.nm_user = UserDoctorsParams.name;
            userToBeEdited.dt_birth = UserDoctorsParams.dateOfBirth;
            userToBeEdited.ds_email = UserDoctorsParams.email;
            userToBeEdited.ds_cellphone = UserDoctorsParams.cellphone;
            userToBeEdited.ds_login = UserDoctorsParams.email;
            //userToBeEdited.cd_user_type = UserDoctorsParams.userTypeId;
            userToBeEdited.cd_user_type = 1;
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

            editedUser.doctorData.cd_crm = UserDoctorsParams.doctorData.crm;
            editedUser.doctorData.cd_specialty_type = UserDoctorsParams.doctorData.specialtyTypeId;
            editedUser.doctorData.cd_user = UserDoctorsParams.id;
            editedUser.doctorData.ds_observation = UserDoctorsParams.doctorData.observation;

            var editedDoctor = await _doctorRepository.Update(editedUser.doctorData);

            return Ok(editedUser);
        }
    }
}
