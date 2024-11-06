namespace apihealthcareconnect.ViewModel.Reponses.Appointments
{
    public class AppointmentsDoctorResponseViewModel
    {
        public int id { get; set; }

        public string name { get; set; }

        public string crm { get; set; }

        public AppointmentsSpecialtyTypeResponseViewModel specialtyType { get; set; }

        public AppointmentsDoctorResponseViewModel(int id, string name, string crm, AppointmentsSpecialtyTypeResponseViewModel specialtyType)
        {
            this.id = id;
            this.name = name;
            this.crm = crm;
            this.specialtyType = specialtyType;
        }
    }
}
