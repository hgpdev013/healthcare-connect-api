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

        public async Task<List<Allergies>> GetAllergiesByUserId(int userId)
        {
            throw new NotImplementedException();
        }

        public async Task<Allergies> Add(Allergies allergies)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Allergies>> AddMultiple(List<Allergies> allergies)
        {
            throw new NotImplementedException();
        }

        public async Task<Allergies> Update(Allergies allergies)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Allergies>> UpdateMultiple(List<Allergies> allergies)
        {
            throw new NotImplementedException();
        }
    }
}
