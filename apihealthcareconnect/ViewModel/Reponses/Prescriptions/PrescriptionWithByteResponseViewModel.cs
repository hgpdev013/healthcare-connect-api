namespace apihealthcareconnect.ViewModel.Reponses.Prescriptions
{
    public class PrescriptionWithByteResponseViewModel
    {
        public int id { get; set; }
        public string fileName { get; set; }
        public string fileExtension { get; set; }
        public DateTime uploadDate { get; set; }
        public byte[] prescription { get; set; }

        public PrescriptionWithByteResponseViewModel(int id,
            string fileName,
            string fileExtension,
            DateTime uploadDate,
            byte[] prescription)
        {
            this.id = id;
            this.fileName = fileName;
            this.fileExtension = fileExtension;
            this.uploadDate = uploadDate;
            this.prescription = prescription;
        }
    }
}

