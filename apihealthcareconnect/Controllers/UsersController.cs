using apihealthcareconnect.Interfaces;
using apihealthcareconnect.Models;
using apihealthcareconnect.ResponseMappings;
using apihealthcareconnect.ViewModel.Reponses.User;
using apihealthcareconnect.ViewModel.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace apihealthcareconnect.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/generic-users")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersRepository _usersRepository;
        private readonly UserResponseMapping _userResponseMapping;

        public UsersController(IUsersRepository usersRepository, UserResponseMapping userResponseMapping)
        {
            _usersRepository = usersRepository ?? throw new ArgumentNullException();
            _userResponseMapping = userResponseMapping ?? throw new ArgumentNullException();
        }

        [HttpGet("only-general-employees")]
        [ProducesResponseType(typeof(List<UserResponse>), 200)]
        public async Task<IActionResult> GetUsers(bool? showAllUserTypes)
        {
            var users = await _usersRepository.GetAllExceptMedicAndPatient(showAllUserTypes ?? false);

            var usersFormatted = users.Select(u => _userResponseMapping.MapGenericUser(
                    true,
                    u
                )
            ).ToList();

            return Ok(usersFormatted);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(UserResponse), 200)]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _usersRepository.GetById(id);

            var userFormatted = _userResponseMapping.MapGenericUser(false, user);

            return Ok(userFormatted);
        }


        [HttpGet("by-user-type/{userTypeId}")]
        [ProducesResponseType(typeof(List<UserResponse>), 200)]
        public async Task<IActionResult> GetUsersByUserType(int userTypeId)
        {
            var users = await _usersRepository.GetByUserTypeId(userTypeId);

            var usersFormatted = users.Select(u => _userResponseMapping.MapGenericUser(
                    true,
                    u
                )
            ).ToList();

            return Ok(usersFormatted);
        }

        [HttpPost]
        [ProducesResponseType(typeof(UserResponse), 201)]
        public async Task<IActionResult> PostUsers(UsersViewModel UserParams)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userToCreate = new Users(null,
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
                UserParams.isActive,
                null);

            var user = await _usersRepository.Add(userToCreate);

            if (user == null)
            {
                return BadRequest("Erro ao cadastrar usuário");
            }

            var userFormatted = _userResponseMapping.MapGenericUser(false, user);

            return Ok(userFormatted);
        }

        [HttpPut]
        [ProducesResponseType(typeof(UserResponse), 204)]
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

            var userFormmated = _userResponseMapping.MapGenericUser(false,editedUser);

            return Ok(userFormmated);
        }

        [HttpPatch("{id}/user-photo")]
        public async Task<IActionResult> PatchUserPhoto(int id, IFormFile photo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userToBeEdited = await _usersRepository.GetById(id);
            if (userToBeEdited == null)
            {
                return NotFound("Usuário não encontrado");
            }
            if (photo == null || photo.Length == 0)
            {
                return BadRequest("Nenhuma foto enviada.");
            }

            try
            {
                using (var memoryStream = new MemoryStream())
                {
                    await photo.CopyToAsync(memoryStream);
                    byte[] photoData = memoryStream.ToArray();

                    userToBeEdited.user_photo = photoData;

                    await _usersRepository.Update(userToBeEdited);

                    return Ok(photoData);

                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao atualizar a foto do usuário: {ex.Message}");
            }
        }

    }
}
