using apihealthcareconnect.Models;

namespace apihealthcareconnect.Interfaces
{
    public interface IPacientRepository
    {
        Task<Pacients> Add(Pacients pacients);

        Task<Pacients> Update(Pacients pacients);
    }
}
