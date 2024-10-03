using apihealthcareconnect.Infraestrutura;
using apihealthcareconnect.Interfaces;
using apihealthcareconnect.Models;

namespace apihealthcareconnect.Repositories
{
    public class DoctorsRepository : IDoctorsRepository
    {
        private readonly ConnectionContext _context = new ConnectionContext();
        public void Add(Doctors doctors)
        {
            _context.Doctors.Add(doctors);
        }

        public List<Doctors> GetAll()
        {
            return _context.Doctors.ToList();
        }

        public Doctors GetById(int id)
        {
            return _context.Doctors.FirstOrDefault(d => d.cd_user == id);
        }

        public void Update(Doctors doctors)
        {
            _context.Doctors.Update(doctors);
        }
    }
}
