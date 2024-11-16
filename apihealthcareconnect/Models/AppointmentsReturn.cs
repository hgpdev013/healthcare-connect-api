using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace apihealthcareconnect.Models
{
    [Table("appointments_return")]
    public class AppointmentsReturn
    {

        [Key]
        public int? cd_appointment_return { get; set; }

        public DateTime dt_return { get; set; }

        public string? ds_observation { get; set; }

        public bool is_active { get; set; } = false;

        public int cd_appointment { get; set; }

        public int cd_doctor { get; set; }

        [ForeignKey("cd_doctor")]
        public Doctors doctorData { get; set; }

        [JsonIgnore]
        [ForeignKey("cd_appointment")]
        public Appointments appointment {  get; set; }

        public AppointmentsReturn(int? cd_appointment_return, DateTime dt_return, string? ds_observation, bool is_active, int cd_doctor, int cd_appointment)
        {
            this.cd_appointment_return = cd_appointment_return ?? null;
            this.dt_return = dt_return;
            this.ds_observation = ds_observation;
            this.is_active = is_active;
            this.cd_doctor = cd_doctor;
            this.cd_appointment = cd_appointment;
        }
    }
}
