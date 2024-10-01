using System.ComponentModel.DataAnnotations.Schema;

namespace apihealthcareconnect.Models
{
    public class Exams
    {
        public int Id { get; set; }
        public string CnpjConsignor { get; set; }
        public string ExamName { get; set; }
        public DateTime ExamDate { get; set; }
        public byte[] Exam { get; set; }

        //Relacionamento de N para 1 com Appointments
        [ForeignKey("AppointmentId")]
        public int AppointmentId { get; set; }
        public Appointments Appointments { get; set; }

        //Relacionamento de N para 1 com Pacients
        [ForeignKey("PacientId")]
        public int PacientId { get; set; }
        public Pacients Pacients{ get; set; }

        //Relacionamento de N para 1 com ExamType
        [ForeignKey("ExamTypeId")]
        public int ExamTypeId { get; set; }
        public ExamType ExamType { get; set; }
    } 
}
