namespace apihealthcareconnect.ViewModel.Requests
{
    public class UserTypeViewModel
    {
        public int? id { get; set; }

        public string name { get; set; }

        public bool isActive { get; set; }

        public UserTypePermissionsViewModel permissions { get; set; }
    }
}
