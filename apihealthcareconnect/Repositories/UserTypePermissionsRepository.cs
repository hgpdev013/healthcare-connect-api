using apihealthcareconnect.Infraestrutura;
using apihealthcareconnect.Interfaces;
using apihealthcareconnect.Models;

namespace apihealthcareconnect.Repositories
{
    public class UserTypePermissionsRepository : IUserTypePermissionsRepository
    {
        private readonly ConnectionContext _context = new ConnectionContext();

        public async Task<UserTypePermissions> AddUserTypePermissions(UserTypePermissions userTypePermissions)
        {
            var createdPermissions = await _context.AddAsync(userTypePermissions);
            await _context.SaveChangesAsync();
            return createdPermissions.Entity;
        }

        public async Task<UserTypePermissions> UpdateUserTypePermissions(UserTypePermissions userTypePermissions)
        {
            var updatedPermissions = _context.Update(userTypePermissions);
            await _context.SaveChangesAsync();
            return updatedPermissions.Entity;
        }
    }
}
