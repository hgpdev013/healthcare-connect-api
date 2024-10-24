using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace apihealthcareconnect.Models
{
    [Table("allergies")]
    public class Allergies
    {
        [Key]
        [JsonPropertyName("id")]
        public int? cd_allergy { get; set; }

        [JsonPropertyName("allergy")]
        public string nm_allergy { get; set; }

        [JsonIgnore]
        [ForeignKey("cd_pacient")]
        public int cd_pacient { get; set; }


        public Allergies(int? cd_allergy, string nm_allergy, int cd_pacient)
        {
            this.cd_allergy = cd_allergy ?? null;
            this.nm_allergy = nm_allergy;
            this.cd_pacient = cd_pacient;
        }
    }
}
