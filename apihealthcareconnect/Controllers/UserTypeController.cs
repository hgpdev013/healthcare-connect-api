using apihealthcareconnect.Interfaces;
using apihealthcareconnect.Models;
using apihealthcareconnect.ResponseMappings;
using apihealthcareconnect.ViewModel.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace apihealthcareconnect.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/user-types")]
    public class UserTypeController : ControllerBase
    {
        private readonly IUserTypeRepository _userTypeRepository;
        private readonly IUserTypePermissionsRepository _userTypePermissionsRepository;
        private readonly UserResponseMapping _userResponseMapping;

        public UserTypeController(IUserTypeRepository userTypeRepository,
            IUserTypePermissionsRepository userTypePermissionsRepository,
            UserResponseMapping userResponseMapping)
        {
            _userTypeRepository = userTypeRepository ?? throw new ArgumentNullException();
            _userTypePermissionsRepository = userTypePermissionsRepository ?? throw new ArgumentNullException();
            _userResponseMapping = userResponseMapping ?? throw new ArgumentNullException();
        }


        [HttpGet]
        [ProducesResponseType(typeof(List<UserTypeViewModel>), 200)]
        public async Task<IActionResult> GetUserTypes()
        {
            var userTypes = await _userTypeRepository.GetAll();

            var userTypesFormatted = userTypes.Select(u => _userResponseMapping.MapUserType(u));

            return Ok(userTypesFormatted);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(UserTypeViewModel), 200)]
        public async Task<IActionResult> GetSpecialtyById(int id)
        {
            var userTypeById = await _userTypeRepository.GetById(id);

            if (userTypeById == null)
            {
                return NotFound("Tipo de usuário não encontrado");
            }

            var userFormatted = _userResponseMapping.MapUserType(userTypeById);

            return Ok(userFormatted);
        }

        [HttpPost]
        [ProducesResponseType(typeof(UserTypeViewModel), 201)]
        public async Task<IActionResult> PostUserType(UserTypeViewModel userTypeParams)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userTypeToCreate = new UserType(null, userTypeParams.name, userTypeParams.isActive);
            var createdUserType = await _userTypeRepository.Add(userTypeToCreate);

            if (createdUserType == null)
            {
                return BadRequest("Erro ao cadastrar tipo de usuário");
            }

            var permissionsToCreate = new UserTypePermissions(null,
                userTypeParams.permissions.listOfDoctors,
                userTypeParams.permissions.listOfPatients,
                userTypeParams.permissions.listOfEmployees,
                userTypeParams.permissions.canEditInfoPatient,
                userTypeParams.permissions.canEditAllergiesPatient,
                userTypeParams.permissions.makeAppointment,
                userTypeParams.permissions.canEditObsAppointment,
                userTypeParams.permissions.canTakeExams,
                userTypeParams.permissions.canTakePrescription,
                createdUserType.cd_user_type!.Value);

            var createdPermissions = await _userTypePermissionsRepository.AddUserTypePermissions(permissionsToCreate);

            if (createdPermissions == null)
            {
                return BadRequest("Erro ao vincular permissões ao tipo de usuário");
            }
            createdUserType.permissions =
                new UserTypePermissions(createdPermissions.cd_user_type_permission,
                    createdPermissions.sg_doctors_list,
                    createdPermissions.sg_pacients_list,
                    createdPermissions.sg_employees_list,
                    createdPermissions.sg_patients_edit,
                    createdPermissions.sg_patients_allergy_edit,
                    createdPermissions.sg_appointment_create,
                    createdPermissions.sg_edit_appointmente_obs,
                    createdPermissions.sg_take_exams,
                    createdPermissions.sg_take_prescriptions,
                    createdPermissions.cd_user_type
                );

            var createdUserTypeFormatted = _userResponseMapping.MapUserType(createdUserType);

            return Ok(createdUserTypeFormatted);
        }

        [HttpPut]
        [ProducesResponseType(typeof(UserTypeViewModel), 200)]
        public async Task<IActionResult> PutUserType(UserTypeViewModel userTypeViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (userTypeViewModel.id == null)
            {
                return BadRequest("O Id deve ser enviado para atualizar os dados");
            }

            var userTypeToUpdate = await _userTypeRepository.GetById(userTypeViewModel.id!.Value);
            if (userTypeToUpdate == null)
            {
                return NotFound("Tipo de usuário a ser atualizado não existe na base de dados.");
            }

            userTypeToUpdate.cd_user_type = userTypeViewModel.id;
            userTypeToUpdate.ds_user_type = userTypeViewModel.name;
            userTypeToUpdate.is_active = userTypeViewModel.isActive;
            
            var updatedUserType = await _userTypeRepository.Update(userTypeToUpdate);

            updatedUserType.permissions.cd_user_type = userTypeViewModel.id!.Value;
            updatedUserType.permissions.sg_doctors_list = userTypeViewModel.permissions.listOfDoctors;
            updatedUserType.permissions.sg_pacients_list = userTypeViewModel.permissions.listOfPatients;
            updatedUserType.permissions.sg_employees_list = userTypeViewModel.permissions.listOfEmployees;
            updatedUserType.permissions.sg_patients_edit = userTypeViewModel.permissions.canEditInfoPatient;
            updatedUserType.permissions.sg_patients_allergy_edit = userTypeViewModel.permissions.canEditAllergiesPatient;
            updatedUserType.permissions.sg_appointment_create = userTypeViewModel.permissions.makeAppointment;
            updatedUserType.permissions.sg_edit_appointmente_obs = userTypeViewModel.permissions.canEditObsAppointment;
            updatedUserType.permissions.sg_take_exams = userTypeViewModel.permissions.canTakeExams;
            updatedUserType.permissions.sg_take_prescriptions = userTypeViewModel.permissions.canTakePrescription;

            var updatedPermissions = await _userTypePermissionsRepository.UpdateUserTypePermissions(updatedUserType.permissions);

            if(updatedPermissions == null)
            {
                return BadRequest("Não foi possível atualizar as permissões do tipo de usuário");
            }

            var updatedUserTypeFormatted = _userResponseMapping.MapUserType(updatedUserType);

            return Ok(updatedUserTypeFormatted);
        }
    }
}
