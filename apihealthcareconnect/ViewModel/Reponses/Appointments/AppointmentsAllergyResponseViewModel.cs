namespace apihealthcareconnect.ViewModel.Reponses.Appointments
{
    public class AppointmentsAllergyResponseViewModel
    {
        public int id { get; set; }
        public string allergy { get; set; }

        public AppointmentsAllergyResponseViewModel(int id, string allergy)
        {
            this.id = id;
            this.allergy = allergy;
        }
    }
}
