using apihealthcareconnect.Models;

namespace apihealthcareconnect.ViewModel.Requests
{
    public class UsersDoctorsRequestViewModel
    {
        public int? id { get; set; }

        public string cpf { get; set; }

        public string documentNumber { get; set; }

        public string name { get; set; }

        public DateTime dateOfBirth { get; set; }

        public string email { get; set; }

        public string cellphone { get; set; }

        public string? streetName { get; set; }

        public int? streetNumber { get; set; }

        public string? complement { get; set; }

        public string? neighborhood { get; set; }

        public string? state { get; set; }

        public string? cep { get; set; }

        public string? city { get; set; }

        public string gender { get; set; }

        public bool isActive { get; set; }

        public DoctorsViewModel doctorData { get; set; }
    }
}
