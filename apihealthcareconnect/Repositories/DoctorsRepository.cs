using apihealthcareconnect.Infraestrutura;
using apihealthcareconnect.Interfaces;
using apihealthcareconnect.Models;

namespace apihealthcareconnect.Repositories
{
    public class DoctorsRepository : IDoctorsRepository
    {
        private readonly ConnectionContext _context = new ConnectionContext();

        public void Add(Doctors doctor)
        {
            _context.Add(doctor);
            _context.SaveChanges();
        }

        public List<Doctors> GetAll()
        {
            return _context.Doctors.ToList();
        }

        public void Update(Doctors doctor)
        {
            _context.Update(doctor);
            _context.SaveChanges();
        }
    }
}
