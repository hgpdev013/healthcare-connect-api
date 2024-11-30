namespace apihealthcareconnect.ViewModel.Reponses.Appointments
{
    public class AppointmentsPacientResponseViewModel
    {
        public int id { get; set; }

        public string name { get; set; }

        public string email { get; set; }

        public string cellphone { get; set; }

        public DateTime dateOfBirth { get; set; }

        public string cpf { get; set; }

        public string documentNumber { get; set; }

        public bool isActive { get; set; }

        public string gender { get; set; }
        public string? streetName { get; set; }
        public int? streetNumber { get; set; }

        public string? complement { get; set; }

        public string? neighborhood { get; set; }

        public string? stateName { get; set; }

        public string? cep { get; set; }

        public string? city { get; set; }

        public List<AppointmentsAllergyResponseViewModel> allergies { get; set; }

        public AppointmentsPacientResponseViewModel(int id,
            string name,
            string email,
            string cellphone,
            DateTime dateOfBirth,
            string cpf,
            string documentNumber,
            bool isActive,
            string gender,
            string? streetName,
            int? streetNumber,
            string? complement,
            string neighborhood,
            string stateName,
            string cep,
            string city,
            List<AppointmentsAllergyResponseViewModel> allergies)
        {
            this.id = id;
            this.name = name;
            this.email = email;
            this.cellphone = cellphone;
            this.dateOfBirth = dateOfBirth;
            this.cpf = cpf;
            this.documentNumber = documentNumber;
            this.isActive = isActive;
            this.gender = gender;
            this.streetName = streetName;
            this.streetNumber = streetNumber;
            this.complement = complement;
            this.neighborhood = neighborhood;
            this.stateName = stateName;
            this.cep = cep;
            this.city = city;
            this.allergies = allergies;
        }
    }
}
