using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace apihealthcareconnect.Models
{
    [Table("doctors")]
    public class Doctors
    {
        [Key]
        [JsonIgnore]
        public int cd_doctor {  get; set; }
        [JsonPropertyName("crm")]
        public string? cd_crm { get; set; }
        [JsonIgnore]
        [JsonPropertyName("userId")]
        public int? cd_user { get; set; }
        [JsonIgnore]
        public int? cd_specialty_type { get; set; }
        [ForeignKey("cd_specialty_type")]
        public SpecialtyType specialtyType { get; set; } = null!;
        [JsonIgnore]
        [ForeignKey("cd_user")]
        public Users Users { get; set; }

        public Doctors(string? cd_crm, int? cd_user, int? cd_specialty_type)
        {
            this.cd_crm = cd_crm;
            this.cd_user = cd_user;
            this.cd_specialty_type = cd_specialty_type;
        }
    }
}