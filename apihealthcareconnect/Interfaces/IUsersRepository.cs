using apihealthcareconnect.Models;

namespace apihealthcareconnect.Interfaces
{
    public interface IUsersRepository
    {
        void Add(Users users);

        List<Users> GetAll();

        Users GetById(int id);

        void Update(Users users);
    }
}
