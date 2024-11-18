using apihealthcareconnect.Models;

namespace apihealthcareconnect.Interfaces
{
    public interface IDoctorRepository
    {
        Task<Users> Add(Doctors doctors);

        Task<Users> Update(Doctors doctors);
    }
}
