namespace apihealthcareconnect.ViewModel.Requests.Appointments
{
    public class AppointmentRequestViewModel
    {
        public int? id { get; set; }
        public DateTime date { get; set; }
        public string observation { get; set; }
        public bool isActive { get; set; }
        public int doctorId { get; set; }
        public int pacientId { get; set; }
    }
}
