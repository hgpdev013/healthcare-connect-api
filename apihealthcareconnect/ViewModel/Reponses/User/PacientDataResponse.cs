using apihealthcareconnect.ViewModel.Reponses.Exams;
using apihealthcareconnect.ViewModel.Reponses.Prescriptions;

namespace apihealthcareconnect.ViewModel.Reponses.User
{
    public class PacientDataResponse
    {
        public int userId { get; set; }

        public List<AllergiesViewModel> allergies { get; set; }

        public List<ExamWithoutByteResponseViewModel> exams { get; set; }

        public List<PrescriptionWithoutByteResponseViewModel> prescriptions { get; set; }

        public PacientDataResponse(int userId,
            List<AllergiesViewModel> allergies,
            List<ExamWithoutByteResponseViewModel> exams,
            List<PrescriptionWithoutByteResponseViewModel> prescriptions)
        {
            this.userId = userId;
            this.allergies = allergies;
            this.exams = exams;
            this.prescriptions = prescriptions;
        }
    }
}
