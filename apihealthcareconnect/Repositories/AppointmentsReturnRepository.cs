using apihealthcareconnect.Infraestrutura;
using apihealthcareconnect.Interfaces;
using apihealthcareconnect.Models;
using Microsoft.EntityFrameworkCore;

namespace apihealthcareconnect.Repositories
{
    public class AppointmentsReturnRepository : IAppointmentsReturnRepository
    {
        private readonly ConnectionContext _context;

        public AppointmentsReturnRepository(ConnectionContext context)
        {
            _context = context;
        }

        public Task<List<AppointmentsReturn>> GetAll(int? appointmentId)
        {
            throw new NotImplementedException();
        }

        public Task<AppointmentsReturn> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<AppointmentsReturn>> GetUnavailableTimes(int doctorId, DateTime date)
        {
            return await _context.AppointmentsReturn
                .Include(i => i.doctorData).ThenInclude(i => i.Users)
                .Include(i => i.doctorData).ThenInclude(i => i.specialtyType)
                 .Where(x => x.dt_return >= date.Date && x.dt_return < date.Date.AddDays(1))
                .Where(x => x.cd_doctor == doctorId)
                .ToListAsync();

        }

        public Task<AppointmentsReturn> Add(AppointmentsReturn appointmentsReturn)
        {
            throw new NotImplementedException();
        }

        public Task<AppointmentsReturn> Update(AppointmentsReturn appointmentsReturn)
        {
            throw new NotImplementedException();
        }
    }
}
