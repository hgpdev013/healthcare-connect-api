using apihealthcareconnect.Infraestrutura;
using apihealthcareconnect.Interfaces;
using apihealthcareconnect.Models;
using Microsoft.EntityFrameworkCore;

namespace apihealthcareconnect.Repositories
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly ConnectionContext _context = new ConnectionContext();
        public async Task<List<Users>> GetAll()
        {
            return await _context.Users.Where(x => x.cd_user_type == 1).Include(i => i.doctorData).ThenInclude(i => i.specialtyType).ToListAsync();
        }

        public async Task<Users> GetById(int id)
        {
            return await _context.Users.Where(x => x.cd_user_type == 1).Include(i => i.doctorData).ThenInclude(i => i.specialtyType).FirstOrDefaultAsync(x => x.cd_user == id);
        }

        public async Task<Doctors> Add(Doctors doctors)
        {
            var doctor = await _context.AddAsync(doctors);
            await _context.SaveChangesAsync();
            return doctor.Entity;
        }

        public async Task<Doctors> Update(Doctors doctors)
        {
            var updatedDoctor = _context.Update(doctors);
            await _context.SaveChangesAsync();
            return updatedDoctor.Entity;
        }
    }
}
