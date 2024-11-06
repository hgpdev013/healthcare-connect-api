using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace apihealthcareconnect.Models
{
    [Table("appointments")]
    public class Appointments
    {
        [Key]
        [JsonPropertyName("id")]
        public int? cd_appointment { get; set; }

        [JsonPropertyName("appointmentDate")]
        public DateTime dt_appointment { get; set; }

        [JsonPropertyName("ds_observation")]
        public string? observation { get; set; }

        [JsonPropertyName("isActive")]
        public bool is_active { get; set; } = false;

        [JsonIgnore]
        public int cd_pacient { get; set; }

        [JsonIgnore]
        public int cd_doctor { get; set; }

        [ForeignKey("cd_pacient")]
        public Pacients pacientData { get; set; }

        [ForeignKey("cd_doctor")]
        public Doctors doctorData { get; set; }

        public List<AppointmentsReturn> appointmentsReturn { get; set; }

        public Appointments(int? cd_appointment, DateTime dt_appointment, string? observation, bool is_active, int cd_pacient, int cd_doctor)
        {
            this.cd_appointment = cd_appointment ?? null;
            this.dt_appointment = dt_appointment;
            this.observation = observation;
            this.is_active = is_active;
            this.cd_pacient = cd_pacient;
            this.cd_doctor = cd_doctor;
        }
    }
}
