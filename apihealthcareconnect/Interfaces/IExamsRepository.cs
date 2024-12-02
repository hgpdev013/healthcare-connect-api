using apihealthcareconnect.Models;

namespace apihealthcareconnect.Interfaces
{
    public interface IExamsRepository
    {
        Task<List<Exams>> GetAll(int? userId);

        Task<Exams> GetById(int id);

        Task<Exams> Add(Exams exam);

        Task<Exams> Update(Exams exam);

        Task Delete(Exams exam);
    }
}
