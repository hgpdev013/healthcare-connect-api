using System.ComponentModel.DataAnnotations.Schema;

namespace apihealthcareconnect.Models
{
    public class Appointments
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }

        //Relacionamento de N para 1 com Pacients
        [ForeignKey("PacientId")]
        public int PacientId { get; set; }
        public Pacients Pacients { get; set; }

        //Relacionamento de N para 1 com Doctors
        [ForeignKey("DoctorId")]
        public int DoctorId { get; set; }
        //public Doctors Doctors { get; set; }

        //Relacionamento de 1 para N com Exams
        public ICollection<Exams> Exams { get; set; }

        //Relacionamento de 1 para N com AppointmentsReturn
        public ICollection<AppointmentsReturn> AppointmentsReturns { get; set; }
    }
}
