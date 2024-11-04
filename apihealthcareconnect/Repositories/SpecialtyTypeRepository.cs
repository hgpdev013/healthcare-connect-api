using apihealthcareconnect.Infraestrutura;
using apihealthcareconnect.Interfaces;
using apihealthcareconnect.Models;
using Microsoft.EntityFrameworkCore;

namespace apihealthcareconnect.Repositories
{
    public class SpecialtyTypeRepository : ISpecialtyTypeRepository
    {
        private readonly ConnectionContext _context;

        public SpecialtyTypeRepository(ConnectionContext context)
        {
            _context = context;
        }

        public async Task<List<SpecialtyType>> GetAll()
        {
            return await _context.SpecialtyType.OrderBy(s => s.ds_specialty_type).ToListAsync();
        }

        public async Task<SpecialtyType> GetById(int id)
        {
            return await _context.SpecialtyType.FirstOrDefaultAsync(x => x.cd_specialty_type == id);
        }

        public async Task<SpecialtyType> Add(SpecialtyType specialtyType)
        {
            var specialtyCreated = await _context.AddAsync(specialtyType);
            await _context.SaveChangesAsync();
            return specialtyCreated.Entity;
        }
        public async Task<SpecialtyType> Update(SpecialtyType specialtyType)
        {
            var specialtyUpdated = _context.Update(specialtyType);
            await _context.SaveChangesAsync();
            return specialtyUpdated.Entity;
        }
    }
}
