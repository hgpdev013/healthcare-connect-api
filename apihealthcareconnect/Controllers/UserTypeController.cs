using apihealthcareconnect.Interfaces;
using apihealthcareconnect.Models;
using apihealthcareconnect.ViewModel;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace apihealthcareconnect.Controllers
{
    [ApiController]
    [Route("api/user-types")]
    public class UserTypeController : ControllerBase
    {
        private readonly IUserTypeRepository _userTypeRepository;
        private readonly IMapper _mapper;

        public UserTypeController(IUserTypeRepository userTypeRepository, IMapper mapper)
        {
            _userTypeRepository = userTypeRepository ?? throw new ArgumentNullException();
            _mapper = mapper;
        }


        [HttpGet]
        [ProducesResponseType(typeof(List<UserTypeViewModel>), 200)]
        public IActionResult GetUserTypes()
        {
            var userTypes = _userTypeRepository.GetAll().OrderBy(s => s.ds_user_type).ToList();
            var userTypesViewModel = _mapper.Map<List<UserTypeViewModel>>(userTypes);
            return Ok(userTypesViewModel);
        }

        [HttpPost]
        [ProducesResponseType(typeof(UserTypeViewModel), 201)]
        public IActionResult PostSpecialties(UserTypeViewModel userTypeViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userType = new UserType(null, userTypeViewModel.name);
            _userTypeRepository.Add(userType);
            var userTypeFormatted = _mapper.Map<UserTypeViewModel>(userType);
            return Ok(userTypeFormatted);
        }

        [HttpPut]
        [ProducesResponseType(typeof(UserTypeViewModel), 200)]
        public IActionResult PutSpecialties(UserTypeViewModel userTypeViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedUserType = new UserType(userTypeViewModel.id, userTypeViewModel.name);
            _userTypeRepository.Update(updatedUserType);
            var userTypeFormatted = _mapper.Map<UserTypeViewModel>(updatedUserType);
            return Ok(userTypeFormatted);
        }
    }
}
