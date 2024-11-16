using apihealthcareconnect.ViewModel.Requests;

namespace apihealthcareconnect.ViewModel.Reponses.Login
{
    public class LoginResponseViewModel
    {
        public int id {  get; set; }

        public string name { get; set; }

        public string email { get; set; }

        public string cellphone {  get; set; }

        public byte[]? photo { get; set; }

        public UserTypeViewModel userType { get; set; }

        public TokenResponseViewModel token { get; set; }


        public LoginResponseViewModel(int id, string name, string email, string cellphone, byte[]? photo, UserTypeViewModel userType, TokenResponseViewModel token)
        {
            this.id = id;
            this.name = name;
            this.email = email;
            this.cellphone = cellphone;
            this.photo = photo;
            this.userType = userType;
            this.token = token;
        }
    }
}
