namespace apihealthcareconnect.ViewModel.Reponses.Appointments
{
    public class AppointmentsPacientResponseViewModel
    {
        public int id { get; set; }

        public string name { get; set; }

        public string email { get; set; }

        public string cellphone { get; set; }

        public List<AppointmentsAllergyResponseViewModel> allergies { get; set; }

        public AppointmentsPacientResponseViewModel(int id, string name, string email, string cellphone, List<AppointmentsAllergyResponseViewModel> allergies)
        {
            this.id = id;
            this.name = name;
            this.email = email;
            this.cellphone = cellphone;
            this.allergies = allergies;
        }
    }
}
