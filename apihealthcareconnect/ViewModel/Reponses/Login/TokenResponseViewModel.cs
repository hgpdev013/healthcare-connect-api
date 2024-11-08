namespace apihealthcareconnect.ViewModel.Reponses.Login
{
    public class TokenResponseViewModel
    {
        public string token { get; set; }

        public int expiresInSeconds { get; set; }

        public DateTime expireDate { get; set; }

        public TokenResponseViewModel(string token, int expiresInSeconds, DateTime expireDate)
        {
            this.token = token;
            this.expiresInSeconds = expiresInSeconds;
            this.expireDate = expireDate;
        }
    }
}
