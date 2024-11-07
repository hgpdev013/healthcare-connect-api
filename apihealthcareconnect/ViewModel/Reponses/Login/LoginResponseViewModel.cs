using apihealthcareconnect.ViewModel.Requests;

namespace apihealthcareconnect.ViewModel.Reponses.Login
{
    public class LoginResponseViewModel
    {
        public int id {  get; set; }

        public string name { get; set; }

        public string email { get; set; }

        public UserTypeViewModel userType { get; set; }

        public TokenResponseViewModel token { get; set; }


        public LoginResponseViewModel(int id, string name, string email, UserTypeViewModel userType, TokenResponseViewModel token)
        {
            this.id = id;
            this.name = name;
            this.email = email;
            this.userType = userType;
            this.token = token;
        }
    }
}
