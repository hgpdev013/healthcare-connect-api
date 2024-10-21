using apihealthcareconnect.Models;

namespace apihealthcareconnect.Interfaces
{
    public interface IDoctorRepository
    {
        Task<List<Users>> GetAll();

        Task<Users> GetById(int id);

        Task<Doctors> Add(Doctors doctors);

        Task<Doctors> Update(Doctors doctors);
    }
}
