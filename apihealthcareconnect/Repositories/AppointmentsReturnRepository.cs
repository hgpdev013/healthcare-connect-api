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

        public async Task<List<AppointmentsReturn>> GetAll(int? appointmentId, DateTime? date, int? doctorId, bool onlyActiveAppointment)
        {
            var appointmentsListQuery = _context.AppointmentsReturn.AsQueryable();

            if (appointmentId.HasValue)
            {
                appointmentsListQuery = appointmentsListQuery.Where(a => a.cd_appointment == appointmentId);
            }

            if (date.HasValue)
            {
                appointmentsListQuery = appointmentsListQuery.Where(a => a.dt_return == date);
            }

            if (doctorId.HasValue)
            {
                appointmentsListQuery = appointmentsListQuery.Where(a => a.cd_doctor == doctorId);
            }
            if (onlyActiveAppointment)
            {
                appointmentsListQuery = appointmentsListQuery.Where(a => a.is_active);
            }

            appointmentsListQuery = appointmentsListQuery
                .Include(i => i.doctorData).ThenInclude(i => i.specialtyType)
                .Include(i => i.doctorData).ThenInclude(i => i.Users)
                .Include(i => i.appointment).ThenInclude(i => i.pacientData).ThenInclude(i => i.Users)
                .Include(i => i.appointment).ThenInclude(i => i.pacientData).ThenInclude(i => i.Allergies)
                .Include(i => i.appointment).ThenInclude(i => i.exams)
                .Include(i => i.appointment).ThenInclude(i => i.prescriptions);

            return await appointmentsListQuery.ToListAsync();
        }

        public async Task<AppointmentsReturn> GetById(int id)
        {
            return await _context.AppointmentsReturn
               .Include(i => i.doctorData).ThenInclude(i => i.specialtyType)
               .Include(i => i.doctorData).ThenInclude(i => i.Users)
               .Include(i => i.appointment).ThenInclude(i => i.pacientData).ThenInclude(i => i.Users)
               .Include(i => i.appointment).ThenInclude(i => i.pacientData).ThenInclude(i => i.Allergies)
               .Include(i => i.appointment).ThenInclude(i => i.exams)
               .Include(i => i.appointment).ThenInclude(i => i.prescriptions)
               .FirstOrDefaultAsync(x => x.cd_appointment_return == id);
        }

        public async Task<List<AppointmentsReturn>> GetUnavailableTimes(int doctorId, DateTime date)
        {
            return await _context.AppointmentsReturn
                .Include(i => i.doctorData).ThenInclude(i => i.Users)
                .Include(i => i.doctorData).ThenInclude(i => i.specialtyType)
                .Include(i => i.appointment).ThenInclude(i => i.pacientData).ThenInclude(i => i.Users)
                .Include(i => i.appointment).ThenInclude(i => i.pacientData).ThenInclude(i => i.Allergies)
                .Include(i => i.appointment).ThenInclude(i => i.exams)
                .Include(i => i.appointment).ThenInclude(i => i.prescriptions)
                .Where(x => x.dt_return >= date.Date && x.dt_return < date.Date.AddDays(1))
                .Where(x => x.cd_doctor == doctorId)
                .Where(x => x.is_active)
                .ToListAsync();

        }

        public async Task<AppointmentsReturn> Add(AppointmentsReturn appointmentsReturn)
        {
            var createdAppointmentReturn = await _context.AddAsync(appointmentsReturn);
            await _context.SaveChangesAsync();

            return await _context.AppointmentsReturn
                .Include(i => i.doctorData).ThenInclude(i => i.specialtyType)
                .Include(i => i.doctorData).ThenInclude(i => i.Users)
                .Include(i => i.appointment).ThenInclude(i => i.pacientData).ThenInclude(i => i.Users)
                .Include(i => i.appointment).ThenInclude(i => i.pacientData).ThenInclude(i => i.Allergies)
                .Include(i => i.appointment).ThenInclude(i => i.exams)
                .Include(i => i.appointment).ThenInclude(i => i.prescriptions)
                .FirstOrDefaultAsync(u => u.cd_appointment_return == createdAppointmentReturn.Entity.cd_appointment_return);
        }

        public async Task<AppointmentsReturn> Update(AppointmentsReturn appointmentsReturn)
        {
            var updatedAppointmentReturn = _context.Update(appointmentsReturn);
            await _context.SaveChangesAsync();

            return await _context.AppointmentsReturn
                .Include(i => i.doctorData).ThenInclude(i => i.specialtyType)
                .Include(i => i.doctorData).ThenInclude(i => i.Users)
                .Include(i => i.appointment).ThenInclude(i => i.pacientData).ThenInclude(i => i.Users)
                .Include(i => i.appointment).ThenInclude(i => i.pacientData).ThenInclude(i => i.Allergies)
                .Include(i => i.appointment).ThenInclude(i => i.exams)
                .Include(i => i.appointment).ThenInclude(i => i.prescriptions)
                .FirstOrDefaultAsync(u => u.cd_appointment_return == updatedAppointmentReturn.Entity.cd_appointment_return);
        }
    }
}
