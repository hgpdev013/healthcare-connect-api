namespace apihealthcareconnect.ViewModel.Requests
{
    public class UserTypeViewModel
    {
        public int? id { get; set; }

        public string name { get; set; }

        public bool isActive { get; set; }

        public UserTypePermissionsViewModel permissions { get; set; }

        public UserTypeViewModel(int? id, string name, bool isActive, UserTypePermissionsViewModel permissions)
        {
            this.id = id;
            this.name = name;
            this.isActive = isActive;
            this.permissions = permissions;
        }
    }
}
