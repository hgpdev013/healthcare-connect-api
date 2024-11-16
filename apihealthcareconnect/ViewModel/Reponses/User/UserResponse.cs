using apihealthcareconnect.ViewModel.Requests;

namespace apihealthcareconnect.ViewModel.Reponses.User
{
    public class UserResponse
    {
        public int id { get; set; }

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

        public string? stateName { get; set; }

        public string? cep { get; set; }

        public string? city { get; set; }

        public string? gender { get; set; }

        public bool isActive { get; set; }


        public byte[]? photo { get; set; }

        public DoctorDataResponse? doctorData { get; set; }

        public PacientDataResponse? pacientData { get; set; }

        public UserTypeViewModel? userType { get; set; }

        public UserResponse(int id,
            string cpf,
            string documentNumber,
            string name,
            DateTime dateOfBirth,
            string email,
            string cellphone,
            string? streetName,
            int? streetNumber,
            string? complement,
            string? neighborhood,
            string? stateName,
            string? cep,
            string? city,
            string? gender,
            bool isActive,
            byte[]? photo,
            DoctorDataResponse? doctorData,
            PacientDataResponse? pacientData,
            UserTypeViewModel? userType)
        {
            this.id = id;
            this.cpf = cpf;
            this.documentNumber = documentNumber;
            this.name = name;
            this.dateOfBirth = dateOfBirth;
            this.email = email;
            this.cellphone = cellphone;
            this.streetName = streetName;
            this.streetNumber = streetNumber;
            this.complement = complement;
            this.neighborhood = neighborhood;
            this.stateName = stateName;
            this.cep = cep;
            this.city = city;
            this.gender = gender;
            this.isActive = isActive;
            this.photo = photo;
            this.doctorData = doctorData;
            this.pacientData = pacientData;
            this.userType = userType;
        }
    }
}
