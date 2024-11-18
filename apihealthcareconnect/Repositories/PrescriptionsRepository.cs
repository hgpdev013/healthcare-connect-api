using apihealthcareconnect.Infraestrutura;
using apihealthcareconnect.Interfaces;
using apihealthcareconnect.Models;
using Microsoft.EntityFrameworkCore;

namespace apihealthcareconnect.Repositories
{
    public class PrescriptionsRepository : IPrescriptionsRepository
    {
        private readonly ConnectionContext _context;

        public PrescriptionsRepository(ConnectionContext context)
        {
            _context = context;
        }

        public async Task<List<Prescriptions>> GetAll()
        {
            return await _context.Prescriptions.OrderBy(s => s.nm_prescription_file).ToListAsync();
        }

        public async Task<Prescriptions> GetById(int id)
        {
            return await _context.Prescriptions.FirstOrDefaultAsync(x => x.cd_prescription == id);
        }

        public async Task<Prescriptions> Add(Prescriptions prescription)
        {
            var prescriptionCreated = await _context.AddAsync(prescription);
            await _context.SaveChangesAsync();
            return prescriptionCreated.Entity;
        }
        public async Task<Prescriptions> Update(Prescriptions prescription)
        {
            var prescriptionUpdated = _context.Update(prescription);
            await _context.SaveChangesAsync();
            return prescriptionUpdated.Entity;
        }

        public async Task Delete(int id)
        {
            _context.Remove(id);
            await _context.SaveChangesAsync();
        }
    }
}
