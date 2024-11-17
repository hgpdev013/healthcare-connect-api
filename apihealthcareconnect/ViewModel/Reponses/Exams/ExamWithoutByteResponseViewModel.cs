namespace apihealthcareconnect.ViewModel.Reponses.Exams
{
    public class ExamWithByteResponseViewModel
    {
        public int id { get; set; }
        public string fileName { get; set; }
        public string fileExtension { get; set; }
        public DateTime uploadDate { get; set; }
        public byte[] exam { get; set; }

        public ExamWithByteResponseViewModel(int id,
            string fileName,
            string fileExtension,
            DateTime uploadDate,
            byte[] exam)
        {
            this.id = id;
            this.fileName = fileName;
            this.fileExtension = fileExtension;
            this.uploadDate = uploadDate;
            this.exam = exam;
        }
    }
}

