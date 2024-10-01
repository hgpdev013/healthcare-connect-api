using System.ComponentModel.DataAnnotations.Schema;

namespace apihealthcareconnect.Models
{
    public class AppointmentsReturn
    {
        public int Id { get; set; }
        public DateTime DateReturn { get; set; }

        //Relacionamento de N para 1 com Appointments
        [ForeignKey("AppointmentId")]
        public int AppointmentId { get; set; }
        public Appointments Appointments { get; set; }

    }
}
