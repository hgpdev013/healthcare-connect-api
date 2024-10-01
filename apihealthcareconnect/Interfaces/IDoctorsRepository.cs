using apihealthcareconnect.Models;

namespace apihealthcareconnect.Interfaces
{
    public interface IDoctorsRepository
    {
        List<Doctors> GetAll();
        void Add(Doctors doctor);

        void Update(Doctors doctor);
    }
}
