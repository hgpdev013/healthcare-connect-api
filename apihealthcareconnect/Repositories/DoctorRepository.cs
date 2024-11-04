using apihealthcareconnect.Infraestrutura;
using apihealthcareconnect.Interfaces;
using apihealthcareconnect.Models;
using Microsoft.EntityFrameworkCore;

namespace apihealthcareconnect.Repositories
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly ConnectionContext _context;

        public DoctorRepository(ConnectionContext context)
        {
            _context = context;
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
