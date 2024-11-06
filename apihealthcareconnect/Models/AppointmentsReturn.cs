using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace apihealthcareconnect.Models
{
    [Table("appointments_return")]
    public class AppointmentsReturn
    {

        [Key]
        [JsonPropertyName("id")]
        public int? cd_appointment_return { get; set; }

        [JsonPropertyName("returnDate")]
        public DateTime dt_return { get; set; }

        [JsonPropertyName("ds_observation")]
        public string? observation { get; set; }

        [JsonPropertyName("isActive")]
        public bool is_active { get; set; } = false;

        [JsonIgnore]
        public int cd_appointment { get; set; }

        [JsonIgnore]
        public int cd_doctor { get; set; }

        [ForeignKey("cd_doctor")]
        public Doctors doctorData { get; set; }

        [JsonIgnore]
        [ForeignKey("cd_appointment")]
        public Appointments appointment {  get; set; }

        public AppointmentsReturn(int? cd_appointment_return, DateTime dt_return, string? observation, bool is_active, int cd_doctor, int cd_appointment)
        {
            this.cd_appointment_return = cd_appointment_return ?? null;
            this.dt_return = dt_return;
            this.observation = observation;
            this.is_active = is_active;
            this.cd_doctor = cd_doctor;
            this.cd_appointment = cd_appointment;
        }
    }
}
