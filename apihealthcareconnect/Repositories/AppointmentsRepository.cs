using apihealthcareconnect.Infraestrutura;
using apihealthcareconnect.Interfaces;
using apihealthcareconnect.Models;
using Microsoft.EntityFrameworkCore;

namespace apihealthcareconnect.Repositories
{
    public class AppointmentsRepository : IAppointmentsRepository
    {
        private readonly ConnectionContext _context;

        public AppointmentsRepository(ConnectionContext context)
        {
            _context = context;
        }

        public async Task<List<Appointments>> GetAll(int? pacientId, int? doctorId)
        {
            var appointmentsListQuery = _context.Appointments.AsQueryable();

            if (pacientId.HasValue)
            {
                appointmentsListQuery = appointmentsListQuery.Where(a => a.cd_pacient == pacientId);
            }

            if (doctorId.HasValue)
            {
                appointmentsListQuery = appointmentsListQuery.Where(a => a.cd_doctor == doctorId);
            }

            appointmentsListQuery = appointmentsListQuery
                .Include(i => i.appointmentsReturn)
                .Include(i => i.pacientData).ThenInclude(i => i.Allergies)
                .Include(i => i.pacientData).ThenInclude(i => i.Users)
                .Include(i => i.doctorData).ThenInclude(i => i.specialtyType)
                .Include(i => i.doctorData).ThenInclude(i => i.Users);

            return await appointmentsListQuery.ToListAsync();
        }

        public async Task<Appointments> GetById(int id)
        {
            return await _context.Appointments
                .Include(i => i.appointmentsReturn)
                .Include(i => i.pacientData).ThenInclude(i => i.Allergies)
                .Include(i => i.pacientData).ThenInclude(i => i.Users)
                .Include(i => i.doctorData).ThenInclude(i => i.specialtyType)
                .Include(i => i.doctorData).ThenInclude(i => i.Users)
                .FirstOrDefaultAsync(x => x.cd_appointment == id);
        }

        public async Task<Appointments> Add(Appointments appointments)
        {
            throw new NotImplementedException();
        }


        public async Task<Appointments> Update(Appointments appointments)
        {
            throw new NotImplementedException();
        }
    }
}
