using apihealthcareconnect.Models;

namespace apihealthcareconnect.Interfaces
{
    public interface IAppointmentsReturnRepository
    {
        Task<List<AppointmentsReturn>> GetAll(int? appointmentId, DateTime? date);

        Task<AppointmentsReturn> GetById(int id);

        Task<List<AppointmentsReturn>> GetUnavailableTimes(int doctorId, DateTime date);

        Task<AppointmentsReturn> Add(AppointmentsReturn appointmentsReturn);

        Task<AppointmentsReturn> Update(AppointmentsReturn appointmentsReturn);

    }
}
