using apihealthcareconnect.Interfaces;
using apihealthcareconnect.Models;
using apihealthcareconnect.Repositories;
using apihealthcareconnect.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace apihealthcareconnect.Controllers
{
    [ApiController]
    [Route("api/user-types")]
    public class UserTypeController : ControllerBase
    {
        private readonly IUserTypeRepository _userTypeRepository;

        public UserTypeController(IUserTypeRepository userTypeRepository)
        {
            _userTypeRepository = userTypeRepository ?? throw new ArgumentNullException();
        }


        [HttpGet]
        [ProducesResponseType(typeof(List<UserType>), 200)]
        public async Task<IActionResult> GetUserTypes()
        {
            var userTypes = await _userTypeRepository.GetAll();
            return Ok(userTypes);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(UserType), 200)]
        public async Task<IActionResult> GetSpecialtyById(int id)
        {
            var userTypeById = await _userTypeRepository.GetById(id);
            if (userTypeById == null)
            {
                return NotFound("Tipo de usuário não encontrado");
            }
            return Ok(userTypeById);
        }

        [HttpPost]
        [ProducesResponseType(typeof(UserType), 201)]
        public async Task<IActionResult> PostUserType(UserTypeViewModel userTypeViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userType = new UserType(null, userTypeViewModel.name, userTypeViewModel.isActive);
            var createdUserType = await _userTypeRepository.Add(userType);
            return Ok(createdUserType);
        }

        [HttpPut]
        [ProducesResponseType(typeof(UserTypeViewModel), 200)]
        public async Task<IActionResult> PutUserType(UserTypeViewModel userTypeViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userTypeToUpdate = await _userTypeRepository.GetById(userTypeViewModel.id);
            if (userTypeToUpdate == null)
            {
                return NotFound("Tipo de usuário a ser atualizado não existe na base de dados.");
            }

            userTypeToUpdate.cd_user_type = userTypeViewModel.id;
            userTypeToUpdate.ds_user_type = userTypeViewModel.name;
            userTypeToUpdate.is_active = userTypeViewModel.isActive;
            
            var updatedUserType = await _userTypeRepository.Update(userTypeToUpdate);
            return Ok(updatedUserType);
        }
    }
}
