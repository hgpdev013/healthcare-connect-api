using apihealthcareconnect.Infraestrutura;
using apihealthcareconnect.Interfaces;
using apihealthcareconnect.Models;

namespace apihealthcareconnect.Repositories
{
    public class SpecialtyTypeRepository : ISpecialtyTypeRepository
    {
        private readonly ConnectionContext _context = new ConnectionContext();
        public List<SpecialtyType> GetAll()
        {
            return _context.SpecialtyType.ToList();
        }

        public void Add(SpecialtyType specialtyType)
        {
            _context.Add(specialtyType);
            _context.SaveChanges();
        }
        public void Update(SpecialtyType specialtyType)
        {
            _context.Update(specialtyType);
            _context.SaveChanges();
        }
    }
}
