namespace apihealthcareconnect.ViewModel.Reponses.Login
{
    public class TokenResponseViewModel
    {
        public string token { get; set; }

        public int expiresIn { get; set; }

        public TokenResponseViewModel(string token, int expiresIn)
        {
            this.token = token;
            this.expiresIn = expiresIn;
        }
    }
}
