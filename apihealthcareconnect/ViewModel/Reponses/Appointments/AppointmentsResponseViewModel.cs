﻿namespace apihealthcareconnect.ViewModel.Reponses.Appointments
{
    public class AppointmentsResponseViewModel
    {
        public int id { get; set; }

        public DateTime appointmentDate { get; set; }

        public string? observation { get; set; }

        public bool isActive { get; set; }

        public string status { get; set; }

        public AppointmentsDoctorResponseViewModel doctorData { get; set; }

        public AppointmentsPacientResponseViewModel pacientData { get; set; }

        public List<Exams.ExamWithoutByteResponseViewModel>? exams { get; set; } = new List<Exams.ExamWithoutByteResponseViewModel>();

        public List<AppointmentReturnResponseViewModel>? appointmentsReturn { get; set; } = new List<AppointmentReturnResponseViewModel>();

        public AppointmentsResponseViewModel(int id,
            DateTime appointmentDate,
            string? observation,
            bool isActive,
            string status,
            AppointmentsDoctorResponseViewModel doctorData,
            AppointmentsPacientResponseViewModel pacientData,
            List<AppointmentReturnResponseViewModel> appointmentsReturn,
            List<Exams.ExamWithoutByteResponseViewModel> exams
            )
        {
            this.id = id;
            this.appointmentDate = appointmentDate;
            this.observation = observation;
            this.isActive = isActive;
            this.status = status;
            this.doctorData = doctorData;
            this.pacientData = pacientData;
            this.exams = exams;
            this.appointmentsReturn = appointmentsReturn;
        }
    }
}
