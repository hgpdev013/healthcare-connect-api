using apihealthcareconnect.Infraestrutura;
using apihealthcareconnect.Interfaces;
using apihealthcareconnect.Models;
using Microsoft.EntityFrameworkCore;

namespace apihealthcareconnect.Repositories
{
    public class AppointmentsReturnRepository : IAppointmentsReturnRepository
    {
        private readonly ConnectionContext _context;

        public Task<List<AppointmentsReturn>> GetAll(int? appointmentId)
        {
            throw new NotImplementedException();
        }

        public Task<AppointmentsReturn> GetById(int id)
        {
            throw new NotImplementedException();
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
