namespace apihealthcareconnect.ViewModel
{
    public class SpecialtyTypeViewModel
    {
        public int id { get; set; }

        public string description { get; set; }

        public string intervalBetweenAppointments { get; set; }

        public bool isActive { get; set; }

        public SpecialtyTypeViewModel(int id, string description, string intervalBetweenAppointments, bool isActive)
        {
            this.id = id;
            this.description = description;
            this.intervalBetweenAppointments = intervalBetweenAppointments;
            this.isActive = isActive;
        }
    }
}
