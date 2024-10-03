using apihealthcareconnect.Infraestrutura;
using apihealthcareconnect.Interfaces;
using apihealthcareconnect.Models;
using Microsoft.EntityFrameworkCore;

namespace apihealthcareconnect.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly ConnectionContext _context = new ConnectionContext();

        public void Add(Users users)
        {
            _context.Add(users);
            _context.SaveChanges();
        }

        public void Update(Users users)
        {
            _context.Update(users);
            _context.SaveChanges();
        }

        public List<Users> GetAll()
        {
            return _context.Users
            .ToList();
        }

        public Users GetById(int id)
        {
            return _context.Users.FirstOrDefault(x => x.cd_user == id);
        }

        public List<Users> GetByUserTypeId(int userTypeId)
        {
            return _context.Users.Where(u => u.cd_user_type == userTypeId).ToList();
        }
    }
}
