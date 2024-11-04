using apihealthcareconnect.Models;

namespace apihealthcareconnect.Interfaces
{
    public interface IAllergiesRepository
    {
        Task<List<Allergies>> GetAll();

        Task<List<Allergies>> GetAllergiesByUserId(int userId);

        Task<Allergies> Add(Allergies allergies);

        Task<Allergies> Update(Allergies allergies);

        Task<List<Allergies>> AddMultiple(List<Allergies> allergies);

        Task<List<Allergies>> UpdateMultiple(List<Allergies> allergies);
    }
}
