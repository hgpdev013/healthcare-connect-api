namespace apihealthcareconnect.ViewModel
{
    public class UserTypePermissionsViewModel
    {
        public int? id { get; set; }
        public bool listOfDoctors { get; set; } = false;

        public bool listOfPatients { get; set; } = false;

        public bool listOfEmployees { get; set; } = false;

        public bool canEditInfoPatient { get; set; } = false;

        public bool canEditAllergiesPatient { get; set; } = false;

        public bool makeAppointment { get; set; } = false;

        public bool canEditObsAppointment { get; set; } = false;

        public bool canTakeExams { get; set; } = false;

        public bool canTakePrescription { get; set; } = false;
    }
}
