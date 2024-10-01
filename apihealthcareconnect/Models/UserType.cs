namespace apihealthcareconnect.Models
{
    public class UserType
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public ICollection<Users> Users { get; set; }
    }
}
