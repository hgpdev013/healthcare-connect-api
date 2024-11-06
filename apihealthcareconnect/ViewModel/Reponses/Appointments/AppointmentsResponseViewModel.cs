using Humanizer.DateTimeHumanizeStrategy;

namespace apihealthcareconnect.ViewModel.Reponses.Appointments
{
    public class AppointmentsResponseViewModel
    {
        public int id { get; set; }

        public DateTime appointmentDate { get; set; }

        public string? observation { get; set; }

        public bool isActive { get; set; }

        public AppointmentsDoctorResponseViewModel doctorData { get; set; }

        public AppointmentsPacientResponseViewModel pacientData { get; set; }

        public List<AppointmentReturnResponseViewModel> appointmentsReturn { get; set; }

        public AppointmentsResponseViewModel(int id,
            DateTime appointmentDate,
            string? observation,
            bool isActive,
            AppointmentsDoctorResponseViewModel doctorData,
            AppointmentsPacientResponseViewModel pacientData,
            List<AppointmentReturnResponseViewModel> appointmentsReturn
            )
        {
            this.id = id;
            this.appointmentDate = appointmentDate;
            this.observation = observation;
            this.isActive = isActive;
            this.doctorData = doctorData;
            this.pacientData = pacientData;
            this.appointmentsReturn = appointmentsReturn;
        }
    }
}
