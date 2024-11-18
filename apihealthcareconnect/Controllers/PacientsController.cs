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
    [Route("api/pacients")]
    public class PacientsController : ControllerBase
    {
        private readonly IPacientRepository _pacientRepository;
        private readonly IUsersRepository _usersRepository;
        private readonly IAllergiesRepository _allergiesRepository;
        private readonly UserResponseMapping _userResponseMapping;

        public PacientsController(IPacientRepository pacientRepository,
            IUsersRepository usersRepository,
            IAllergiesRepository allergiesRepository,
            UserResponseMapping userResponseMapping)
        {
            _pacientRepository = pacientRepository ?? throw new ArgumentNullException();
            _usersRepository = usersRepository ?? throw new ArgumentNullException();
            _allergiesRepository = allergiesRepository ?? throw new ArgumentNullException();
            _userResponseMapping = userResponseMapping ?? throw new ArgumentNullException();
        }


        [HttpGet]
        [ProducesResponseType(typeof(List<UserResponse>), 200)]
        public async Task<IActionResult> GetPacients()
        {
            var pacients = await _usersRepository.GetByUserTypeId(2);

            var pacientsFormatted = pacients.Select(p => _userResponseMapping.MapGenericUser(true, p)).ToList();

            return Ok(pacientsFormatted);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(UserResponse), 200)]
        public async Task<IActionResult> GetPacientById(int id)
        {
            var pacient = await _usersRepository.GetById(id);

            if(pacient.cd_user_type != 2)
            {
                return BadRequest("Usuário não é do tipo paciente.");
            }

            var pacientFormmated = _userResponseMapping.MapGenericUser(false, pacient);

            return Ok(pacientFormmated);

        }

        [HttpPost]
        [ProducesResponseType(typeof(UserResponse), 201)]
        public async Task<IActionResult> PostPacient(UsersPacientsRequestViewModel UserPacientsParams)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userToCreate = new Users(null,
                UserPacientsParams.cpf,
                UserPacientsParams.documentNumber,
                UserPacientsParams.name,
                UserPacientsParams.dateOfBirth,
                UserPacientsParams.email,
                UserPacientsParams.cellphone,
                UserPacientsParams.email,
                //UserPacientsParams.userTypeId,
                2,
                UserPacientsParams.streetName,
                UserPacientsParams.streetNumber,
                UserPacientsParams.complement,
                UserPacientsParams.state,
                UserPacientsParams.cep,
                UserPacientsParams.city,
                UserPacientsParams.gender,
                UserPacientsParams.neighborhood,
                UserPacientsParams.isActive,
                null);

            var createdUser = await _usersRepository.Add(userToCreate);

            if (createdUser == null)
            {
                return BadRequest("Erro ao cadastrar usuário");
            }

            var pacientToCreate = new Pacients(null, createdUser.cd_user!.Value);

            var createdPacient = await _pacientRepository.Add(pacientToCreate);

            if (createdPacient == null)
            {
                return BadRequest("Erro ao relacionar usuário como paciente");
            }

            var allergiesToCreate = UserPacientsParams.pacientData.Allergies
                                    .Select(vm => new Allergies(vm.id, vm.allergy, createdUser.pacientData!.cd_pacient!.Value))
                                    .ToList();

            var createdMultipleAllergies = await _allergiesRepository.AddMultiple(allergiesToCreate);

            if(createdMultipleAllergies == null)
            {
                return BadRequest("Erro ao relacionar alergias ao paciente");
            }


            createdUser.pacientData.cd_user = createdUser.cd_user!.Value;
            createdUser.pacientData.cd_pacient = createdPacient.cd_pacient;
            createdUser.pacientData.Allergies = createdMultipleAllergies;

            var createdPacientFormmated = _userResponseMapping.MapGenericUser(false, createdUser);

            return Ok(createdPacientFormmated);
        }

        [HttpPut]
        [ProducesResponseType(typeof(UserResponse), 200)]
        public async Task<IActionResult> PutPacient(UsersPacientsRequestViewModel UserPacientsParams)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userToBeEdited = await _usersRepository.GetById(UserPacientsParams.id!.Value);

            if (userToBeEdited == null)
            {
                return NotFound("Usuário não encontrado");
            }

            if (userToBeEdited.cd_user_type != 2)
            {
                return BadRequest("Usuário não é do tipo paciente");
            }

            userToBeEdited.cd_user = UserPacientsParams.id;
            userToBeEdited.cd_cpf = UserPacientsParams.cpf;
            userToBeEdited.cd_identification = UserPacientsParams.documentNumber;
            userToBeEdited.nm_user = UserPacientsParams.name;
            userToBeEdited.dt_birth = UserPacientsParams.dateOfBirth;
            userToBeEdited.ds_email = UserPacientsParams.email;
            userToBeEdited.ds_cellphone = UserPacientsParams.cellphone;
            userToBeEdited.ds_login = UserPacientsParams.email;
            //userToBeEdited.cd_user_type = UserPacientsParams.userTypeId;
            userToBeEdited.cd_user_type = 2;
            userToBeEdited.nm_street = UserPacientsParams.streetName;
            userToBeEdited.cd_street_number = UserPacientsParams.streetNumber;
            userToBeEdited.ds_complement = UserPacientsParams.complement;
            userToBeEdited.nm_state = UserPacientsParams.state;
            userToBeEdited.cd_cep = UserPacientsParams.cep;
            userToBeEdited.nm_city = UserPacientsParams.city;
            userToBeEdited.ds_gender = UserPacientsParams.gender;
            userToBeEdited.ds_neighborhood = UserPacientsParams.neighborhood;
            userToBeEdited.is_active = UserPacientsParams.isActive;

            var editedUser = await _usersRepository.Update(userToBeEdited);

            var pacientToBeEdited = await _pacientRepository.GetByUserId(editedUser.cd_user!.Value);

            //ALLERGY -----------------------

            var allergiesToBeEdited = await _allergiesRepository.GetAllergiesByUserId(editedUser.pacientData!.cd_pacient!.Value);

            var existingAllergiesToUpdate = allergiesToBeEdited
                .Select(x => x).Where(x => UserPacientsParams.pacientData.Allergies.Select(x => x.id).Contains(x.cd_allergy));

            var nonExistingAllergiesToCreate = UserPacientsParams.pacientData.Allergies.Select(x => x)
                .Where(x => !allergiesToBeEdited.Any(a => a.cd_allergy == x.id))
                .Select(x => new Allergies(
                    null,
                    x.allergy,
                    editedUser.pacientData!.cd_pacient!.Value
                ))
                .ToList();

            var createdAllergies = await _allergiesRepository.AddMultiple(nonExistingAllergiesToCreate);

            foreach (var allergy in existingAllergiesToUpdate)
            {
                var allergyToUpdate = UserPacientsParams.pacientData.Allergies.FirstOrDefault(a => a.id == allergy.cd_allergy);

               if (allergyToUpdate != null)
                {
                    allergy.nm_allergy = allergyToUpdate.allergy;
                }
            }

            var editedAllergies = await _allergiesRepository.UpdateMultiple(existingAllergiesToUpdate.ToList());

            editedUser.pacientData.Allergies = editedAllergies.Concat(createdAllergies).ToList() ;


            //ALLERGY END-----------------------

            var editedUserFormmated = _userResponseMapping.MapGenericUser(false, editedUser);

            return Ok(editedUserFormmated);
        }
    }
}
