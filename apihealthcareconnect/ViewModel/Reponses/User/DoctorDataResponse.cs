using apihealthcareconnect.Models;
using Microsoft.CodeAnalysis;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace apihealthcareconnect.ViewModel.Reponses.User
{
    public class DoctorDataResponse
    {
        public string crm { get; set; }

        public int userId { get; set; }

        public string? observation { get; set; }

        public SpecialtyTypeViewModel specialtyType { get; set; } = null!;

        public DoctorDataResponse(string crm, int userId, string? observation, SpecialtyTypeViewModel specialtyType)
        {
            this.crm = crm;
            this.userId = userId;
            this.observation = observation;
            this.specialtyType = specialtyType;
        }
    }
}
