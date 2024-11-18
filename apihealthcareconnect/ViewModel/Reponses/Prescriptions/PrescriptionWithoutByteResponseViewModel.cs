namespace apihealthcareconnect.ViewModel.Reponses.Prescriptions
{
    public class PrescriptionWithoutByteResponseViewModel
    {
        public int id { get; set; }
        public string fileName { get; set; }
        public string fileExtension { get; set; }
        public DateTime uploadDate { get; set; }

        public PrescriptionWithoutByteResponseViewModel(int id,
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

