namespace apihealthcareconnect.Models
{
    public class ExamType
    {
        public int Id { get; set; }
        public string? Type { get; set; }

        //Relacionamento de 1 para N com Exams
        public ICollection<Exams> Exams { get; set; }
    }
}