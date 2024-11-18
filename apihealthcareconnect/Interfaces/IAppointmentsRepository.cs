using apihealthcareconnect.Models;

namespace apihealthcareconnect.Interfaces
{
    public interface IAppointmentsRepository
    {
        Task<List<Appointments>> GetAll(int? pacientId, int? doctorId, DateTime? date);

        Task<Appointments> GetById(int id);

        Task<List<Appointments>> GetUnavailableTimes(int doctorId, DateTime date);

        Task<Appointments> Add(Appointments appointments);

        Task<Appointments> Update(Appointments appointments);
    }
}
