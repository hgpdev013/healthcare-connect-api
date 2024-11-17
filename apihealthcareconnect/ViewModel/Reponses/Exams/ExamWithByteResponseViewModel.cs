namespace apihealthcareconnect.ViewModel.Reponses.Exams
{
    public class ExamWithoutByteResponseViewModel
    {
        public int id { get; set; }
        public string fileName { get; set; }
        public string fileExtension { get; set; }
        public DateTime uploadDate { get; set; }

        public ExamWithoutByteResponseViewModel(int id,
            string fileName,
            string fileExtension,
            DateTime uploadDate)
        {
            this.id = id;
            this.fileName = fileName;
            this.fileExtension = fileExtension;
            this.uploadDate = uploadDate;
        }
    }
}

