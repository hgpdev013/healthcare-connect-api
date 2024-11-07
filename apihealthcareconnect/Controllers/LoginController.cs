using apihealthcareconnect.Interfaces;
using apihealthcareconnect.ViewModel.Reponses.Login;
using apihealthcareconnect.ViewModel.Requests;
using apihealthcareconnect.ViewModel.Requests.Login;
using Microsoft.AspNetCore.Mvc;

namespace apihealthcareconnect.Controllers
{
    [ApiController]
    [Route("api/login")]
    public class LoginController : ControllerBase
    {
        private readonly IUsersRepository _usersRepository;

        public LoginController(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository ?? throw new ArgumentNullException();
        }

        [HttpPost]
        [ProducesResponseType(typeof(LoginResponseViewModel), 200)]
        public async Task<IActionResult> PostLogin(LoginRequestViewModel LoginParams)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userToLogin = await _usersRepository.GetByEmail(LoginParams.email);

            if (userToLogin == null)
            {
                return BadRequest("E-mail ou senha inválidos");
            }

            if (userToLogin.ds_password == null)
            {
                return BadRequest("Primeiro acesso. Necessário redefinir sua senha para ter o acesso liberado");
            }

            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(LoginParams.password, userToLogin.ds_password);

            if (userToLogin.ds_email != LoginParams.email || !isPasswordValid)
            {
                return BadRequest("E-mail ou senha inválidos.");
            }

            var response = new LoginResponseViewModel(
                userToLogin.cd_user!.Value,
                userToLogin.nm_user,
                userToLogin.ds_email,
                new UserTypeViewModel(
                    userToLogin.userType.cd_user_type,
                    userToLogin.userType.ds_user_type,
                    userToLogin.userType.is_active,
                    new UserTypePermissionsViewModel(
                        userToLogin.userType.permissions.cd_user_type_permission,
                        userToLogin.userType.permissions.sg_doctors_list,
                        userToLogin.userType.permissions.sg_pacients_list,
                        userToLogin.userType.permissions.sg_employees_list,
                        userToLogin.userType.permissions.sg_patients_edit,
                        userToLogin.userType.permissions.sg_patients_allergy_edit,
                        userToLogin.userType.permissions.sg_appointment_create,
                        userToLogin.userType.permissions.sg_edit_appointmente_obs,
                        userToLogin.userType.permissions.sg_take_exams,
                        userToLogin.userType.permissions.sg_take_prescriptions
                    )
                ),
                new TokenResponseViewModel("", 0)
            );

            return Ok(response);

        }

        [HttpPut("reset-password")]
        public async Task<IActionResult> PutPassword(LoginRequestViewModel LoginParams)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userToResetPassword = await _usersRepository.GetByEmail(LoginParams.email);

            if (userToResetPassword == null)
            {
                return BadRequest("E-mail ou senha inválidos");
            }

            if (userToResetPassword.ds_email != LoginParams.email)
            {
                return BadRequest("E-mail inválido.");
            }

            userToResetPassword.ds_password = BCrypt.Net.BCrypt.HashPassword(LoginParams.password);

            var userEdited = await _usersRepository.Update(userToResetPassword);

            if (userEdited == null)
            {
                return BadRequest("Erro ao alterar senha");
            }

            return Ok("Senha alterada com sucesso");

        }
    }
}
