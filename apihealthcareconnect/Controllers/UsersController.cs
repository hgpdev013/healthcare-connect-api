using apihealthcareconnect.Interfaces;
using apihealthcareconnect.Models;
using apihealthcareconnect.ViewModel.Requests;
using Microsoft.AspNetCore.Mvc;

namespace apihealthcareconnect.Controllers
{
    [ApiController]
    [Route("api/generic-users")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersRepository _usersRepository;

        public UsersController(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository ?? throw new ArgumentNullException();
        }

        [HttpGet("only-general-employees")]
        [ProducesResponseType(typeof(List<Users>), 200)]
        public async Task<IActionResult> GetUsers(bool? showAllUserTypes)
        {
            var users = await _usersRepository.GetAllExceptMedicAndPatient(showAllUserTypes ?? false);
            return Ok(users);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Users), 200)]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _usersRepository.GetById(id);
            return Ok(user);
        }


        [HttpGet("by-user-type/{userTypeId}")]
        [ProducesResponseType(typeof(List<Users>), 200)]
        public async Task<IActionResult> GetUsersByUserType(int userTypeId)
        {
            var users = await _usersRepository.GetByUserTypeId(userTypeId);
            return Ok(users);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Users), 201)]
        public async Task<IActionResult> PostUsers(UsersViewModel UserParams)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userToCreate = new Users(UserParams.id,
                UserParams.cpf,
                UserParams.documentNumber,
                UserParams.name,
                UserParams.dateOfBirth,
                UserParams.email,
                UserParams.cellphone,
                UserParams.email,
                UserParams.userTypeId,
                UserParams.streetName,
                UserParams.streetNumber,
                UserParams.complement,
                UserParams.state,
                UserParams.cep,
                UserParams.city,
                UserParams.gender,
                UserParams.neighborhood,
                UserParams.isActive);

            var user = await _usersRepository.Add(userToCreate);

            if (user == null)
            {
                return BadRequest("Erro ao cadastrar usuário");
            }

            return Ok(user);
        }

        [HttpPut]
        [ProducesResponseType(typeof(Users), 204)]
        public async Task<IActionResult> PutUsers(UsersViewModel UserParams)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userToBeEdited = await _usersRepository.GetById(UserParams.id!.Value);

            if (userToBeEdited == null)
            {
                return NotFound("Usuário não encontrado");
            }

            userToBeEdited.cd_user = UserParams.id;
            userToBeEdited.cd_cpf = UserParams.cpf;
            userToBeEdited.cd_identification = UserParams.documentNumber;
            userToBeEdited.nm_user = UserParams.name;
            userToBeEdited.dt_birth = UserParams.dateOfBirth;
            userToBeEdited.ds_email = UserParams.email;
            userToBeEdited.ds_cellphone = UserParams.cellphone;
            userToBeEdited.ds_login = UserParams.email;
            userToBeEdited.cd_user_type = UserParams.userTypeId;
            userToBeEdited.nm_street = UserParams.streetName;
            userToBeEdited.cd_street_number = UserParams.streetNumber;
            userToBeEdited.ds_complement = UserParams.complement;
            userToBeEdited.nm_state = UserParams.state;
            userToBeEdited.cd_cep = UserParams.cep;
            userToBeEdited.nm_city = UserParams.city;
            userToBeEdited.ds_gender = UserParams.gender;
            userToBeEdited.ds_neighborhood = UserParams.neighborhood;
            userToBeEdited.is_active = UserParams.isActive;

            var editedUser = await _usersRepository.Update(userToBeEdited);

            return Ok(editedUser);
        }
    }
}
