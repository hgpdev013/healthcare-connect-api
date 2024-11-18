using apihealthcareconnect.Infraestrutura;
using apihealthcareconnect.Interfaces;
using apihealthcareconnect.Models;
using Microsoft.EntityFrameworkCore;

namespace apihealthcareconnect.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly ConnectionContext _context;

        public UsersRepository(ConnectionContext context)
        {
            _context = context;
        }

        public async Task<Users> Add(Users users)
        {
            var userCreated = await _context.AddAsync(users);
            await _context.SaveChangesAsync();

            return await _context.Users
                .Include(u => u.userType).ThenInclude(ut => ut.permissions)
                .Include(u => u.doctorData).ThenInclude(d => d.specialtyType)
                .Include(u => u.pacientData).ThenInclude(p => p.Allergies)
                .FirstOrDefaultAsync(u => u.cd_user == userCreated.Entity.cd_user);
        }

        public async Task<Users> Update(Users users)
        {
            var updatedUser = _context.Update(users);
            await _context.SaveChangesAsync();

            return await _context.Users
                .Include(u => u.userType).ThenInclude(ut => ut.permissions)
                .Include(u => u.doctorData).ThenInclude(d => d.specialtyType)
                .Include(u => u.pacientData).ThenInclude(p => p.Allergies)
                .FirstOrDefaultAsync(u => u.cd_user == updatedUser.Entity.cd_user);
        }

        public async Task<List<Users>> GetAllExceptMedicAndPatient(bool showAllUserTypes)
        {
            return await _context.Users.OrderBy(x => x.nm_user)
                .Where(x => !showAllUserTypes ? !new[] { 1 , 2 }.Contains(x.cd_user_type) : true)
                .Include(i => i.userType).ThenInclude(i => i.permissions)
                .Include(i => i.pacientData).ThenInclude(i => i.Allergies)
                .Include(i => i.doctorData).ThenInclude(i => i.specialtyType)
                .ToListAsync();
        }

        public async Task<Users> GetById(int id)
        {
            return await _context.Users
                .Include(i => i.userType).ThenInclude(i => i.permissions)
                .Include(i => i.pacientData).ThenInclude(i => i.Allergies)
                .Include(i => i.doctorData).ThenInclude(i => i.specialtyType)
                .FirstOrDefaultAsync(x => x.cd_user == id);
        }

        public async Task<List<Users>> GetByUserTypeId(int userTypeId)
        {
            return await _context.Users.Where(x => x.cd_user_type == userTypeId)
                .Include(i => i.userType).ThenInclude(i => i.permissions)
                .Include(i => i.pacientData).ThenInclude(i => i.Allergies)
                .Include(i => i.doctorData).ThenInclude(i => i.specialtyType)
                .ToListAsync();
        }

        public async Task<Users> GetByEmail(string email)
        {
            return await _context.Users
                .Include(i => i.userType).ThenInclude(i => i.permissions)
                .FirstOrDefaultAsync(x => x.ds_email == email);
        }
    }
}
