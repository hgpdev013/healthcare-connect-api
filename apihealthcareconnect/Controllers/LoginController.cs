using apihealthcareconnect.Interfaces;
using apihealthcareconnect.Services;
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
        private TokenService _tokenService;
        private EmailService _emailService;

        public LoginController(IUsersRepository usersRepository, TokenService tokenService, EmailService emailService)
        {
            _usersRepository = usersRepository ?? throw new ArgumentNullException();
            _tokenService = tokenService ?? throw new ArgumentNullException();
            _emailService = emailService ?? throw new ArgumentNullException();
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

            var token = _tokenService.GenerateJwtToken(userToLogin.cd_user.Value, userToLogin.ds_email, userToLogin.userType.ds_user_type, DateTime.Now.ToBrazilTime().AddHours(9));

            var response = new LoginResponseViewModel(
                userToLogin.cd_user!.Value,
                userToLogin.nm_user,
                userToLogin.ds_email,
                userToLogin.ds_cellphone,
                userToLogin.user_photo,
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
                new TokenResponseViewModel(token, 3600 * 9, DateTime.Now.ToBrazilTime().AddHours(9))
            );

            return Ok(response);

        }

        [HttpPost("send-mail-to-reset")]
        public async Task<IActionResult> PostSendMailToResetPassword(EmailToResetPasswordRequestViewModel request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _usersRepository.GetByEmail(request.email);

            var token = _tokenService.GenerateJwtToken(user.cd_user.Value, user.ds_email, user.userType.ds_user_type, DateTime.Now.ToBrazilTime().AddMinutes(5));

            if (user == null)
            {
                return NotFound("E-mail inválido.");
            }

            var sendMailRequest = new SendEmailViewModel(user.ds_email,
                "Redefinição de senha - Healthcare Connect",
                "<!DOCTYPE html>" +
                "<html lang=\"pt-BR\">" +
                "<head>" +
                    "<meta charset=\"UTF-8\">" +
                    "<meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">" +
                    "<title>Redefinição de Senha - Healthcare Connect</title>" +
                    "<style>" +
                        "body {" +
                            "font-family: Arial, sans-serif;" +
                            "background-color: #f7f7f7;" +
                            "margin: 20px;" +
                            "padding: 0;" +
                            "color: #333333;" +
                            "border-radius: 8px" +
                        "}" +
                        ".container {" +
                            "width: 100%;" +
                            "max-width: 600px;" +
                            "margin: 0 auto;" +
                            "background-color: #ffffff;" +
                            "padding: 20px;" +
                            "border-radius: 8px;" +
                            "box-shadow: 0px 0px 10px rgba(0, 0, 0, 0.1);" +
                        "}" +
                        ".header {" +
                            "text-align: center;" +
                            "padding: 10px 0;" +
                        "}" +
                        ".header h1 {" +
                            "color: #307ac1;" +
                            "margin: 0;" +
                        "}" +
                        ".content {" +
                            "padding: 20px;" +
                            "line-height: 1.6;" +
                        "}" +
                        ".button {" +
                            "display: inline-block;" +
                            "padding: 10px 20px;" +
                            "color: #ffffff;" +
                            "background-color: #307ac1;" +
                            "text-decoration: none;" +
                            "border-radius: 5px;" +
                            "margin-top: 20px;" +
                            "text-align: center;" +
                        "}" +
                        ".footer {" +
                            "text-align: center;" +
                            "font-size: 12px;" +
                            "color: #777777;" +
                            "padding: 10px 0;" +
                            "border-top: 1px solid #eeeeee;" +
                            "margin-top: 20px;" +
                        "}" +
                    "</style>" +
                "</head>" +
                "<body>" +
                    "<div class=\"container\">" +
                        "<div class=\"header\">" +
                            "<h1>Redefinição de Senha</h1>" +
                        "</div>" +
                        "<div class=\"content\">" +
                            "<p>Olá,</p>" +
                            "<p>Recebemos uma solicitação para redefinir sua senha no <strong>Healthcare Connect</strong>. Para redefinir sua senha, clique no botão abaixo:</p>" +
                            $"<p><a href=\"https://www.healthcareconnect.com.br/login/esqueceuSenha?token={token}\" class=\"button\">Redefinir Senha</a></p>" +
                            "<p>Se você não solicitou essa alteração, desconsidere este e-mail. Sua senha permanecerá a mesma e nenhuma ação adicional será necessária.</p>" +
                            "<p>Atenciosamente,<br>Equipe Healthcare Connect</p>" +
                        "</div>" +
                        "<div class=\"footer\">" +
                            "<p>Healthcare Connect &copy; 2024</p>" +
                            "<p>Este é um e-mail automático. Por favor, não responda.</p>" +
                        "</div>" +
                    "</div>" +
                "</body>" +
                "</html>");

            await _emailService.sendEmail(sendMailRequest);

            return Ok("Email enviado com sucesso");
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

            var emailFromToken = _tokenService.GetDataFromJwtToken(token, "email");

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
