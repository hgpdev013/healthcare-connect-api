using apihealthcareconnect.Infraestrutura;
using apihealthcareconnect.Interfaces;
using apihealthcareconnect.Models;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace apihealthcareconnect.Repositories
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly ConnectionContext _context;

        public DoctorRepository(ConnectionContext context)
        {
            _context = context;
        }

        public async Task<Users> Add(Doctors doctors)
        {
            var doctor = await _context.AddAsync(doctors);
            await _context.SaveChangesAsync();

            return await _context.Users
                .Include(u => u.userType).ThenInclude(ut => ut.permissions)
                .Include(u => u.doctorData).ThenInclude(d => d.specialtyType)
                .Include(u => u.pacientData).ThenInclude(p => p.Allergies)
                .FirstOrDefaultAsync(u => u.cd_user == doctor.Entity.cd_user!.Value);

        }

        public async Task<Users> Update(Doctors doctors)
        {
            var updatedDoctor = _context.Update(doctors);
            await _context.SaveChangesAsync();

            return await _context.Users
                .Include(u => u.userType).ThenInclude(ut => ut.permissions)
                .Include(u => u.doctorData).ThenInclude(d => d.specialtyType)
                .Include(u => u.pacientData).ThenInclude(p => p.Allergies)
                .FirstOrDefaultAsync(u => u.cd_user == updatedDoctor.Entity.cd_user!.Value);

        }
    }
}
