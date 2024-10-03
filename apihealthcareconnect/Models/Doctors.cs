using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace apihealthcareconnect.Models
{
    [Table("doctors")]
    public class Doctors
    {
        [Key]
        public int? cd_doctor {  get; set; }
        public string? cd_crm { get; set; }
        public string? ds_observation { get; set; }
        public int? cd_user { get; set; }
        public int? cd_specialty_type { get; set; }
        [ForeignKey("SpecialtyType")]
        public SpecialtyType specialtyType { get; set; }

        public ICollection<Users>? Users { get; set; }

        public Doctors(int? cd_doctor, string? cd_crm, string? ds_observation, int? cd_user, int? cd_specialty_type)
        {
            this.cd_doctor = cd_doctor;
            this.cd_crm = cd_crm;
            this.ds_observation = ds_observation;
            this.cd_user = cd_user;
            this.cd_specialty_type = cd_specialty_type;
        }
    }
}