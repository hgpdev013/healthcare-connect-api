using apihealthcareconnect.Models;

namespace apihealthcareconnect.Interfaces
{
    public interface ISpecialtyTypeRepository
    {
        Task<List<SpecialtyType>> GetAll();

        Task<SpecialtyType> GetById(int id);

        Task<SpecialtyType> Add(SpecialtyType specialtyType);

        Task<SpecialtyType> Update(SpecialtyType specialtyType);
    }
}
