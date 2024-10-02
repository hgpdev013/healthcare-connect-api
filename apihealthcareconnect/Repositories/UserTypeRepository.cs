using apihealthcareconnect.Infraestrutura;
using apihealthcareconnect.Interfaces;
using apihealthcareconnect.Models;

namespace apihealthcareconnect.Repositories
{
    public class UserTypeRepository : IUserTypeRepository
    {
        private readonly ConnectionContext _context = new ConnectionContext();

        public void Add(UserType userType)
        {
            _context.Add(userType);
            _context.SaveChanges();
        }

        public void Update(UserType userType)
        {
            _context.Update(userType);
            _context.SaveChanges();
        }

        public List<UserType> GetAll()
        {
            return _context.UserTypes.ToList();
        }

        public UserType GetById(int id)
        {
            return _context.UserTypes.FirstOrDefault(x => x.cd_user_type == id);
        }
    }
}
