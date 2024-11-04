using apihealthcareconnect.Infraestrutura;
using apihealthcareconnect.Interfaces;
using apihealthcareconnect.Models;
using Microsoft.EntityFrameworkCore;

namespace apihealthcareconnect.Repositories
{
    public class PacientRepository : IPacientRepository
    {
        private readonly ConnectionContext _context = new ConnectionContext();
        public async Task<List<Users>> GetAll()
        {
            return await _context.Users.Where(x => x.cd_user_type == 2)
                .Include(i => i.userType).ThenInclude(i => i.permissions)
                .Include(i => i.pacientData).ThenInclude(i => i.Allergies)
                .ToListAsync();
        }

        public async Task<Users> GetById(int id)
        {
            return await _context.Users.Where(x => x.cd_user_type == 2)
                .Include(i => i.userType).ThenInclude(i => i.permissions)
                .Include(i => i.pacientData).ThenInclude(i => i.Allergies)
                .FirstOrDefaultAsync(x => x.cd_user == id);
        }

        public async Task<Pacients> Add(Pacients pacients)
        {
            var pacient = await _context.AddAsync(pacients);
            await _context.SaveChangesAsync();
            return pacient.Entity;
        }

        public async Task<Pacients> Update(Pacients pacients)
        {
            var updatedPacient = _context.Update(pacients);
            await _context.SaveChangesAsync();
            return updatedPacient.Entity;
        }
    }
}
