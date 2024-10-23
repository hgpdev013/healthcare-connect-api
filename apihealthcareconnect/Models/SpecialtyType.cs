using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace apihealthcareconnect.Models
{
    [Table("specialty_type")]
    public class SpecialtyType
    {
        [Key]
        [JsonPropertyName("id")]
        public int? cd_specialty_type { get; set; }

        [JsonPropertyName("specialtyName")]
        public string ds_specialty_type { get; set; }

        [JsonPropertyName("intervalBetweenAppointments")]
        public string dt_interval_between_appointments { get; set; }

        [JsonPropertyName("isActive")]
        public bool is_active { get; set; }

        public SpecialtyType(int? cd_specialty_type, string ds_specialty_type, string dt_interval_between_appointments, bool is_active) {
            this.cd_specialty_type = cd_specialty_type ?? null;
            this.ds_specialty_type = ds_specialty_type;
            this.dt_interval_between_appointments = dt_interval_between_appointments;
            this.is_active = is_active;
        }
    }
}
