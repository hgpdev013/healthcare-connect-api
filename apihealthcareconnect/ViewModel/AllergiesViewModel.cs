namespace apihealthcareconnect.ViewModel
{
    public class AllergiesViewModel
    {
        public int? id { get; set; }
        public string allergy { get; set; }

        public AllergiesViewModel(int? id, string allergy)
        {
            this.id = id;
            this.allergy = allergy;
        }
    }
}
