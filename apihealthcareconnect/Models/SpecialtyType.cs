using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace apihealthcareconnect.Models
{
    [Table("specialty_type")]
    public class SpecialtyType
    {
        [Key]
        public int? cd_specialty_type { get; set; }
        public string ds_specialty_type { get; set; }
        public string? dt_interval_between_appointments { get; set; }

        public SpecialtyType(int? cd_specialty_type, string ds_specialty_type, string? dt_interval_between_appointments) {
            this.cd_specialty_type = cd_specialty_type ?? null;
            this.ds_specialty_type = ds_specialty_type;
            this.dt_interval_between_appointments = dt_interval_between_appointments ?? null;
        }
    }
}
