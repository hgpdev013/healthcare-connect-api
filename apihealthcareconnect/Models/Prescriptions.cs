using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace apihealthcareconnect.Models
{
    [Table("prescriptions")]
    public class Prescriptions
    {
        [Key]
        public int cd_prescription { get; set; }

        public DateTime dt_prescription { get; set; }

        public string nm_prescription_file { get; set; }

        public string nm_prescription_extension { get; set; }

        public byte[] prescription {  get; set; }

        [JsonIgnore]
        public int cd_appointment {  get; set; }

        [JsonIgnore]
        public int cd_pacient { get; set; }

        [JsonIgnore]
        [ForeignKey("cd_appointment")]
        public Appointments appointments { get; set; }

        [JsonIgnore]
        [ForeignKey("cd_pacient")]
        public Pacients pacientData { get; set; }

        public Prescriptions(int cd_prescription,
            DateTime dt_prescription,
            string nm_prescription_file,
            string nm_prescription_extension,
            byte[] prescription,
            int cd_appointment,
            int cd_pacient)
        {
            this.cd_prescription = cd_prescription;
            this.dt_prescription = dt_prescription;
            this.nm_prescription_file = nm_prescription_file;
            this.nm_prescription_extension = nm_prescription_extension;
            this.prescription = prescription;
            this.cd_appointment = cd_appointment;
            this.cd_pacient = cd_pacient;
        }
    }
}