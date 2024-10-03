using apihealthcareconnect.Interfaces;
using apihealthcareconnect.Models;
using apihealthcareconnect.ViewModel;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace apihealthcareconnect.Controllers
{
    [ApiController]
    [Route("api/doctors")]
    public class DoctorsController : ControllerBase
    {
        private readonly IDoctorsRepository _doctorsRepository;
        private readonly IUsersRepository _usersRepository;
        private readonly IMapper _mapper;

        public DoctorsController(IDoctorsRepository doctorsRepository, IUsersRepository usersRepository, IMapper mapper)
        {
            _doctorsRepository = doctorsRepository ?? throw new ArgumentNullException(nameof(doctorsRepository));
            _usersRepository = usersRepository ?? throw new ArgumentNullException(nameof(usersRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }



        [HttpGet]
        [ProducesResponseType(typeof(List<UsersViewModel>), 200)]
        public IActionResult GetUserTypes()
        {
            var users = _usersRepository.GetAll().ToList();
            var mappedUsers = _mapper.Map<List<UsersViewModel>>(users);
            return Ok(mappedUsers);
        }
    }
}
