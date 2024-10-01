using System.ComponentModel.DataAnnotations.Schema;

namespace apihealthcareconnect.Models
{
    public class Allergies
    {
        public int Id { get; set; }
        public string Allergy { get; set; }
        public string Observation { get; set; }

        //Relacionamento de N para 1 com Pacients
        [ForeignKey("PacientId")]
        public int PacientId { get; set; }
        public Pacients Pacients { get; set; }
    }
}
