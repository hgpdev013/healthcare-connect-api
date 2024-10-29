using apihealthcareconnect.Models;

namespace apihealthcareconnect.Interfaces
{
    public interface IUsersRepository
    {
        Task<Users> Add(Users users);
        Task<Users> Update(Users users);
        Task<List<Users>> GetAll();
        Task<Users> GetById(int id);
        Task<List<Users>> GetByUserTypeId(int userTypeid);
    }
}
