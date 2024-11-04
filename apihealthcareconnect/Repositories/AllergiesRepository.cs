using apihealthcareconnect.Infraestrutura;
using apihealthcareconnect.Interfaces;
using apihealthcareconnect.Models;
using Microsoft.EntityFrameworkCore;

namespace apihealthcareconnect.Repositories
{
    public class AllergiesRepository : IAllergiesRepository
    {
        private readonly ConnectionContext _context = new ConnectionContext();

        public async Task<List<Allergies>> GetAll()
        {
            return await _context.Allergies.ToListAsync();
        }

        public async Task<List<Allergies>> GetAllergiesByUserId(int pacientId)
        {
            return await _context.Allergies.Where(x => x.cd_pacient == pacientId).ToListAsync();
        }

        public async Task<Allergies> Add(Allergies allergies)
        {
            var createdAllergy = await _context.Allergies.AddAsync(allergies);
            await _context.SaveChangesAsync();

            return createdAllergy.Entity;
        }

        public async Task<List<Allergies>> AddMultiple(List<Allergies> allergies)
        {
            await _context.Allergies.AddRangeAsync(allergies);
            await _context.SaveChangesAsync();

            return allergies;
        }

        public async Task<Allergies> Update(Allergies allergies)
        {
            var updatedAllergy = _context.Update(allergies);
            await _context.SaveChangesAsync();

            return updatedAllergy.Entity;
        }

        public async Task<List<Allergies>> UpdateMultiple(List<Allergies> allergies)
        {
            _context.Allergies.UpdateRange(allergies);
            await _context.SaveChangesAsync();

            return allergies;
        }
    }
}
