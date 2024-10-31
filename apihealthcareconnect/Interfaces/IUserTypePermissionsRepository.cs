using apihealthcareconnect.Models;

namespace apihealthcareconnect.Interfaces
{
    public interface IUserTypePermissionsRepository
    {
        Task<UserTypePermissions> AddUserTypePermissions(UserTypePermissions userTypePermissions);

        Task<UserTypePermissions> UpdateUserTypePermissions(UserTypePermissions userTypePermissions);
    }
}
