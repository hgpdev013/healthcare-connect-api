using apihealthcareconnect.Models;

namespace apihealthcareconnect.Interfaces
{
    public interface IPrescriptionsRepository
    {
        Task<List<Prescriptions>> GetAll();

        Task<Prescriptions> GetById(int id);

        Task<Prescriptions> Add(Prescriptions prescription);

        Task<Prescriptions> Update(Prescriptions prescription);

        Task Delete(Prescriptions prescription);
    }
}
