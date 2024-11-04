using apihealthcareconnect.Models;

namespace apihealthcareconnect.Interfaces
{
    public interface IPacientRepository
    {
        Task<List<Users>> GetAll();

        Task<Users> GetById(int id);

        Task<Pacients> Add(Pacients pacients);

        Task<Pacients> Update(Pacients pacients);
    }
}
