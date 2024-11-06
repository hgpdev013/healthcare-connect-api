using apihealthcareconnect.Models;

namespace apihealthcareconnect.ViewModel.Requests
{
    public class DoctorsViewModel
    {
        public string crm { get; set; }

        public int specialtyTypeId { get; set; }

        public string? observation { get; set; }
    }
}
