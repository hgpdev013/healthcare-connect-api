using apihealthcareconnect.Models;
using apihealthcareconnect.ViewModel.Reponses.Exams;

namespace apihealthcareconnect.ResponseMappings
{
    public class ExamResponseMapping
    {
        public ExamWithoutByteResponseViewModel mapExamsAppointments(Exams exam)
        {
            var response = new ExamWithoutByteResponseViewModel(
                exam.cd_exam!.Value,
                exam.nm_exam_file,
                exam.nm_file_extension,
                exam.dt_exam
            );

            return response;
        }

        public ExamWithByteResponseViewModel mapExamsGeneral(Exams exam)
        {
            var response = new ExamWithByteResponseViewModel(
                exam.cd_exam!.Value,
                exam.nm_exam_file,
                exam.nm_file_extension,
                exam.dt_exam,
                exam.exam
            );

            return response;
        }

    }
}
