using apihealthcareconnect.Models;

namespace apihealthcareconnect.Interfaces
{
    public interface IDoctorsRepository
    {
        void Add(Doctors doctors);
        List<Doctors> GetAll();
        Doctors GetById(int id);
        void Update(Doctors doctors);
    }
}
