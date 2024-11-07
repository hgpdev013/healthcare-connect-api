namespace apihealthcareconnect.ViewModel.Requests
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

        public UserTypePermissionsViewModel(int? id,
            bool listOfDoctors,
            bool listOfPatients,
            bool listOfEmployees,
            bool canEditInfoPatient,
            bool canEditAllergiesPatient,
            bool makeAppointment,
            bool canEditObsAppointment,
            bool canTakeExams,
            bool canTakePrescription)
        {
            this.id = id;
            this.listOfDoctors = listOfDoctors;
            this.listOfPatients = listOfPatients;
            this.listOfEmployees = listOfEmployees;
            this.canEditInfoPatient = canEditInfoPatient;
            this.canEditAllergiesPatient = canEditAllergiesPatient;
            this.makeAppointment = makeAppointment;
            this.canEditObsAppointment = canEditObsAppointment;
            this.canTakeExams = canTakeExams;
            this.canTakePrescription = canTakePrescription;
        }
    }
}
