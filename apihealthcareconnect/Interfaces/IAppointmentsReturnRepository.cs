using apihealthcareconnect.Models;

namespace apihealthcareconnect.Interfaces
{
    public interface IAppointmentsReturnRepository
    {
        Task<List<AppointmentsReturn>> GetAll(int? appointmentId);

        Task<AppointmentsReturn> GetById(int id);

        Task<AppointmentsReturn> Add(AppointmentsReturn appointmentsReturn);

        Task<AppointmentsReturn> Update(AppointmentsReturn appointmentsReturn);
    }
}
