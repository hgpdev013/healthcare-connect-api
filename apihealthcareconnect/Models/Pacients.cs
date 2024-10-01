using System.ComponentModel.DataAnnotations.Schema;

namespace apihealthcareconnect.Models
{
    public class Pacients
    {
        public int Id { get; set; }

        //Relacionamento de N para 1 com Users
        [ForeignKey("UserId")]
        public int UserId{ get; set; }
        public Users Users { get; set; }

        //Relacionamento de 1 para N com Appointments
        public ICollection<Appointments> Appointments { get; set; }

        //Relacionamento de 1 para N com Allergies
        public ICollection<Allergies> Allergies { get; set; }

        //Relacionamento de 1 para N com Exams
        public ICollection<Exams> Exams { get; set; }
    }
}
