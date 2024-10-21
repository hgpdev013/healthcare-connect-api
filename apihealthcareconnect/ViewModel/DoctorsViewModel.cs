using apihealthcareconnect.Models;

namespace apihealthcareconnect.ViewModel
{
    public class DoctorsViewModel
    {
        public int? doctorId { get; set; }
        public string crm { get; set; }
        public string? observation { get; set; }
        public int userId { get; set; }
        public int specialtyTypeId { get; set; }
    }
}
