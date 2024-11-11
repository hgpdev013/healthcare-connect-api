namespace apihealthcareconnect.ViewModel.Reponses.Appointments
{
    public class AppointmentsSpecialtyTypeResponseViewModel
    {
        public int id { get; set; }
        public string specialtyName { get; set; }
        public string interval { get; set; }
        public bool isActive { get; set; }

        public AppointmentsSpecialtyTypeResponseViewModel(int id, string specialtyName, string interval, bool isActive)
        {
            this.id = id;
            this.specialtyName = specialtyName;
            this.interval = interval;
            this.isActive = isActive;
        }
    }
}
