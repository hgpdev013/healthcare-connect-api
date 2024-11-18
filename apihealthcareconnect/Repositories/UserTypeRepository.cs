using apihealthcareconnect.Infraestrutura;
using apihealthcareconnect.Interfaces;
using apihealthcareconnect.Models;
using Microsoft.EntityFrameworkCore;

namespace apihealthcareconnect.Repositories
{
    public class UserTypeRepository : IUserTypeRepository
    {
        private readonly ConnectionContext _context;

        public UserTypeRepository(ConnectionContext context)
        {
            _context = context;
        }
        public async Task<List<UserType>> GetAll()
        {
            return await _context.UserTypes
                .Include(i => i.permissions)
                .OrderBy(s => s.ds_user_type)
                .ToListAsync();
        }

        public async Task<UserType> GetById(int id)
        {
            return await _context.UserTypes
                .Include(i => i.permissions)
                .FirstOrDefaultAsync(x => x.cd_user_type == id);
        }

        public async Task<UserType> Add(UserType userType)
        {
            var createdUserType = await _context.AddAsync(userType);
            await _context.SaveChangesAsync();

            return await _context.UserTypes
                .Include(i => i.permissions)
                .FirstOrDefaultAsync(x => x.cd_user_type == createdUserType.Entity.cd_user_type);
        }

        public async Task<UserType> Update(UserType userType)
        {
            var updatedUserType = _context.Update(userType);
            await _context.SaveChangesAsync();

            return await _context.UserTypes
                .Include(i => i.permissions)
                .FirstOrDefaultAsync(x => x.cd_user_type == updatedUserType.Entity.cd_user_type);
        }
    }
}
