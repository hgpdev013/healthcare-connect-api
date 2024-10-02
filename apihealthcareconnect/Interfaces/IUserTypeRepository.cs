using apihealthcareconnect.Models;

namespace apihealthcareconnect.Interfaces
{
    public interface IUserTypeRepository
    {
        void Add(UserType userType);

        List<UserType> GetAll();

        UserType GetById(int id);

        void Update(UserType userType);
    }
}
