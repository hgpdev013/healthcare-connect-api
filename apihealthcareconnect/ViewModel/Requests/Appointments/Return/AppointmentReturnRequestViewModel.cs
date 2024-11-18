namespace apihealthcareconnect.ViewModel.Requests.Appointments.Return
{
    public class AppointmentReturnRequestViewModel
    {
        public DateTime date { get; set; }
        public string? observation { get; set; }
        public bool isActive { get; set; }
        public int doctorId { get; set; }
        public int appointmentId { get; set; }
    }
}
