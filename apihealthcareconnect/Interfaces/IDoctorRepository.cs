using apihealthcareconnect.Models;

namespace apihealthcareconnect.Interfaces
{
    public interface IDoctorRepository
    {
        Task<Doctors> Add(Doctors doctors);

        Task<Doctors> Update(Doctors doctors);
    }
}
