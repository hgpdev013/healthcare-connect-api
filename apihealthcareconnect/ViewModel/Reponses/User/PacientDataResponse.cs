using apihealthcareconnect.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace apihealthcareconnect.ViewModel.Reponses.User
{
    public class PacientDataResponse
    {
        public int userId { get; set; }

        public List<AllergiesViewModel> allergies { get; set; }

        public PacientDataResponse(int userId, List<AllergiesViewModel> allergies)
        {
            this.userId = userId;
            this.allergies = allergies;
        }
    }
}
