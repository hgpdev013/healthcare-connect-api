namespace apihealthcareconnect.ViewModel.Reponses.Appointments
{
    public class AppointmentReturnResponseViewModel
    {
        public int id { get; set; }
        
        public int fatherAppointmentId { get; set; }

        public DateTime appointmentDate { get; set; }

        public string? observation { get; set; }

        public bool isActive { get; set; }

        public string status { get; set; }

        public AppointmentsDoctorResponseViewModel doctorData { get; set; }

        public AppointmentsPacientResponseViewModel pacientData { get; set; }

        public AppointmentReturnResponseViewModel(int id,
            int fatherAppointmentId,
            DateTime appointmentDate,
            string? observation,
            bool isActive,
            string status,
            AppointmentsDoctorResponseViewModel doctorData,
            AppointmentsPacientResponseViewModel pacientData)
        {
            this.id = id;
            this.fatherAppointmentId = fatherAppointmentId;
            this.appointmentDate = appointmentDate;
            this.observation = observation;
            this.isActive = isActive;
            this.status = status;
            this.doctorData = doctorData;
            this.pacientData = pacientData;
        }
    }
}
