namespace apihealthcareconnect.ViewModel.Requests.Appointments
{
    public class AppointmentPutRequestViewModel
    {
            public int id { get; set; }
            public DateTime date { get; set; }
            public string observation { get; set; }
            public bool isActive { get; set; }
    }

}
