using apihealthcareconnect.Models;

namespace apihealthcareconnect.Interfaces
{
    public interface IAppointmentsRepository
    {
        Task<List<Appointments>> GetAll(int? pacientId, int? doctorId);

        Task<Appointments> GetById(int id);

        Task<Appointments> Add(Appointments appointments);

        Task<Appointments> Update(Appointments appointments);
    }
}
