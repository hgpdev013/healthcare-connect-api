using apihealthcareconnect.Interfaces;
using apihealthcareconnect.ViewModel.Reponses.Login;
using apihealthcareconnect.ViewModel.Requests;
using apihealthcareconnect.ViewModel.Requests.Login;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace apihealthcareconnect.Controllers
{
    [ApiController]
    [Route("api/login")]
    public class LoginController : ControllerBase
    {
        private readonly IUsersRepository _usersRepository;
        private TokenGen _tokenGen;

        public LoginController(IUsersRepository usersRepository, TokenGen tokenGen)
        {
            _usersRepository = usersRepository ?? throw new ArgumentNullException();
            _tokenGen = tokenGen ?? throw new ArgumentNullException();
        }

        [HttpPost]
        [ProducesResponseType(typeof(LoginResponseViewModel), 200)]
        public async Task<IActionResult> PostLogin([FromHeader(Name = "typeOfApplication")] string typeOfApplication, LoginRequestViewModel LoginParams)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (string.IsNullOrEmpty(typeOfApplication) || !new HashSet<string> { "web", "mobile" }.Contains(typeOfApplication))
            {
                return BadRequest("typeOfApplication inválido.");
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


            if (typeOfApplication == "web" && userToLogin?.userType?.cd_user_type == 2)
            {
                return Unauthorized("Você não possui permissão para acessar esse sistema.");
            }

            if (typeOfApplication == "mobile" && userToLogin?.userType?.cd_user_type != 2)
            {
                return Unauthorized("Você não possui permissão para acessar esse sistema.");
            }

            var token = _tokenGen.GenerateJwtToken(userToLogin.cd_user.Value, userToLogin.ds_email, userToLogin.userType.ds_user_type);

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
                new TokenResponseViewModel(token, 3600, DateTime.Now.AddMinutes(60))
            );

            return Ok(response);

        }
        [Authorize]
        [HttpPut("reset-password")]
        public async Task<IActionResult> PutPassword(ResetPasswordRequestViewModel resetPasswordParams)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var authorizationHeader = Request.Headers["Authorization"].ToString();
            var token = string.Empty;

            if (authorizationHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            {
                token = authorizationHeader.Substring("Bearer ".Length).Trim();
            }

            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized("Não autorizado.");
            }

            var emailFromToken = _tokenGen.GetDataFromJwtToken(token, "email");

            if (emailFromToken == null)
            {
                return BadRequest("Email para redefinir senha não encontrado");
            }

            var userToResetPassword = await _usersRepository.GetByEmail(emailFromToken);

            if (userToResetPassword == null)
            {
                return BadRequest("E-mail ou senha inválidos");
            }

            if (userToResetPassword.ds_email != emailFromToken)
            {
                return BadRequest("E-mail inválido.");
            }

            userToResetPassword.ds_password = BCrypt.Net.BCrypt.HashPassword(resetPasswordParams.newPassword);

            var userEdited = await _usersRepository.Update(userToResetPassword);

            if (userEdited == null)
            {
                return BadRequest("Erro ao alterar senha");
            }

            return Ok("Senha alterada com sucesso");

        }
    }
}
