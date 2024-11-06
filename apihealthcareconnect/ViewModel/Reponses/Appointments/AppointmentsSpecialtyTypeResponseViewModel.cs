namespace apihealthcareconnect.ViewModel.Reponses.Appointments
{
    public class AppointmentsSpecialtyTypeResponseViewModel
    {
        public int id { get; set; }
        public string specialtyName { get; set; }

        public AppointmentsSpecialtyTypeResponseViewModel(int id, string specialtyName)
        {
            this.id = id;
            this.specialtyName = specialtyName;
        }
    }
}
