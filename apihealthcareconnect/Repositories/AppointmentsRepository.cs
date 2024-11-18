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

        public async Task<List<Appointments>> GetAll(int? pacientId, int? doctorId, DateTime? date)
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

            if (date.HasValue)
            {
                appointmentsListQuery = appointmentsListQuery.Where(a => a.dt_appointment == date);
            }

            appointmentsListQuery = appointmentsListQuery
                .Include(i => i.prescriptions)
                .Include(i => i.exams)
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
                .Include(i => i.prescriptions)
                .Include(i => i.exams)
                .Include(i => i.appointmentsReturn)
                .Include(i => i.pacientData).ThenInclude(i => i.Allergies)
                .Include(i => i.pacientData).ThenInclude(i => i.Users)
                .Include(i => i.doctorData).ThenInclude(i => i.specialtyType)
                .Include(i => i.doctorData).ThenInclude(i => i.Users)
                .FirstOrDefaultAsync(x => x.cd_appointment == id);
        }

        public async Task<List<Appointments>> GetUnavailableTimes(int doctorId, DateTime date)
        {
            return await _context.Appointments
                .Include(i => i.appointmentsReturn)
                .Include(i => i.doctorData).ThenInclude(i => i.Users)
                .Include(i => i.doctorData).ThenInclude(i => i.specialtyType)
                 .Where(x => x.dt_appointment >= date.Date && x.dt_appointment < date.Date.AddDays(1))
                .Where(x => x.cd_doctor == doctorId)
                .ToListAsync();

        }

        public async Task<Appointments> Add(Appointments appointments)
        {
            var createdAppointment = await _context.AddAsync(appointments);
            await _context.SaveChangesAsync();

            return await _context.Appointments
                .Include(i => i.prescriptions)
                .Include(i => i.exams)
                .Include(i => i.appointmentsReturn)
                .Include(i => i.pacientData).ThenInclude(i => i.Allergies)
                .Include(i => i.pacientData).ThenInclude(i => i.Users)
                .Include(i => i.doctorData).ThenInclude(i => i.specialtyType)
                .Include(i => i.doctorData).ThenInclude(i => i.Users)
                .FirstOrDefaultAsync(u => u.cd_appointment == createdAppointment.Entity.cd_appointment);
        }


        public async Task<Appointments> Update(Appointments appointments)
        {
            var updatedAppointment = _context.Update(appointments);
            await _context.SaveChangesAsync();

            return await _context.Appointments
                .Include(i => i.prescriptions)
                .Include(i => i.exams)
                .Include(i => i.appointmentsReturn)
                .Include(i => i.pacientData).ThenInclude(i => i.Allergies)
                .Include(i => i.pacientData).ThenInclude(i => i.Users)
                .Include(i => i.doctorData).ThenInclude(i => i.specialtyType)
                .Include(i => i.doctorData).ThenInclude(i => i.Users)
                .FirstOrDefaultAsync(u => u.cd_appointment == updatedAppointment.Entity.cd_appointment);
        }
    }
}
