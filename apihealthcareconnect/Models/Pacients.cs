using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace apihealthcareconnect.Models
{
    [Table("pacients")]
    public class Pacients
    {
        [Key]
        [JsonIgnore]
        public int? cd_pacient { get; set; }

        [JsonIgnore]
        public int cd_user { get; set; }

        [JsonIgnore]
        [ForeignKey("cd_user")]
        public Users Users { get; set; }

        public List<Allergies> Allergies { get; set; }

        public Pacients(int? cd_pacient, int cd_user)
        {
            this.cd_pacient = cd_pacient;
            this.cd_user = cd_user;
        }
    }
}