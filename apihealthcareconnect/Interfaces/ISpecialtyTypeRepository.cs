using apihealthcareconnect.Models;

namespace apihealthcareconnect.Interfaces
{
    public interface ISpecialtyTypeRepository
    {
        List<SpecialtyType> GetAll();

        void Add(SpecialtyType specialtyType);

        void Update(SpecialtyType specialtyType);
    }
}
