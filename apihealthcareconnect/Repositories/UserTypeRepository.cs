using apihealthcareconnect.Infraestrutura;
using apihealthcareconnect.Interfaces;
using apihealthcareconnect.Models;
using Microsoft.EntityFrameworkCore;

namespace apihealthcareconnect.Repositories
{
    public class UserTypeRepository : IUserTypeRepository
    {
        private readonly ConnectionContext _context = new ConnectionContext();
        public async Task<List<UserType>> GetAll()
        {
            return await _context.UserTypes.OrderBy(s => s.ds_user_type).ToListAsync();
        }

        public async Task<UserType> GetById(int id)
        {
            return await _context.UserTypes.FirstOrDefaultAsync(x => x.cd_user_type == id);
        }

        public async Task<UserType> Add(UserType userType)
        {
            var createdUserType = await _context.AddAsync(userType);
            await _context.SaveChangesAsync();

            return createdUserType.Entity;
        }

        public async Task<UserType> Update(UserType userType)
        {
            var updatedUserType = _context.Update(userType);
            await _context.SaveChangesAsync();
            return updatedUserType.Entity;
        }
    }
}
