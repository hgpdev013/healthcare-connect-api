using apihealthcareconnect.Infraestrutura;
using apihealthcareconnect.Interfaces;
using apihealthcareconnect.Models;
using Microsoft.EntityFrameworkCore;

namespace apihealthcareconnect.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly ConnectionContext _context = new ConnectionContext();

        public async Task<Users> Add(Users users)
        {
            var userCreated = await _context.AddAsync(users);
            await _context.SaveChangesAsync();
            return userCreated.Entity;
        }

        public async Task<Users> Update(Users users)
        {
            var updatedUser = _context.Update(users);
            await _context.SaveChangesAsync();
            return updatedUser.Entity;
        }

        public async Task<List<Users>> GetAll()
        {
            return await _context.Users.OrderBy(x => x.nm_user).ToListAsync();
        }

        public async Task<Users> GetById(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.cd_user == id);
        }
    }
}
