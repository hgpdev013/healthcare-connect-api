namespace apihealthcareconnect.ViewModel.Reponses.Appointments
{
    public class AppointmentsUnavailableTimesResponseViewModel
    {
        public int doctorId { get; set; }

        public string doctorName { get; set; }

        AppointmentsSpecialtyTypeResponseViewModel specialtyType { get; set; }

        public List<string> unavailableTimes { get; set; }

        public AppointmentsUnavailableTimesResponseViewModel(int doctorId,
            string doctorName,
            AppointmentsSpecialtyTypeResponseViewModel specialtyType,
            List<string> unavailableTimes)
        {
            this.doctorId = doctorId;
            this.doctorName = doctorName;
            this.specialtyType = specialtyType;
            this.unavailableTimes = unavailableTimes;
        }
    }
}
