namespace apihealthcareconnect.ViewModel.Requests.Login
{
    public class SendEmailViewModel
    {
        public string to {  get; set; }
        public string subject { get; set; }
        public string body { get; set; }

        public SendEmailViewModel(string to, string subject, string body)
        {
            this.to = to;
            this.subject = subject;
            this.body = body;
        }
    }
}
