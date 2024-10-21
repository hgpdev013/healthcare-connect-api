using apihealthcareconnect.Models;

namespace apihealthcareconnect.Interfaces
{
    public interface IUserTypeRepository
    {
        Task<UserType> Add(UserType userType);

        Task<List<UserType>> GetAll();

        Task<UserType> GetById(int id);

        Task<UserType> Update(UserType userType);
    }
}
