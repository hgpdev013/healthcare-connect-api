using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace apihealthcareconnect.Models
{
    [Table("appointments")]
    public class Appointments
    {
        [Key]
        public int? cd_appointment { get; set; }

        public DateTime dt_appointment { get; set; }

        public string? ds_observation { get; set; }

        public bool is_active { get; set; } = false;

        public int cd_pacient { get; set; }

        public int cd_doctor { get; set; }

        [ForeignKey("cd_pacient")]
        public Pacients pacientData { get; set; }

        [ForeignKey("cd_doctor")]
        public Doctors doctorData { get; set; }

        public List<AppointmentsReturn>? appointmentsReturn { get; set; } = new List<AppointmentsReturn>();

        public List<Exams>? exams {  get; set; } = new List<Exams>();

        public Appointments(int? cd_appointment, DateTime dt_appointment, string? ds_observation, bool is_active, int cd_pacient, int cd_doctor)
        {
            this.cd_appointment = cd_appointment ?? null;
            this.dt_appointment = dt_appointment;
            this.ds_observation = ds_observation;
            this.is_active = is_active;
            this.cd_pacient = cd_pacient;
            this.cd_doctor = cd_doctor;
        }
    }
}
