using NuGet.Protocol.Plugins;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace apihealthcareconnect.Models
{
    [Table("user_type_permissions")]
    public class UserTypePermissions
    {
        [Key]
        [JsonPropertyName("id")]
        public int? cd_user_type_permission { get; set; }

        [JsonPropertyName("listOfDoctors")]
        public bool sg_doctors_list { get; set; }

        [JsonPropertyName("listOfPatients")]
        public bool sg_pacients_list { get; set; }

        [JsonPropertyName("listOfEmployees")]
        public bool sg_employees_list { get; set; }

        [JsonPropertyName("canEditInfoPatient")]
        public bool sg_patients_edit { get; set; }

        [JsonPropertyName("canEditAllergiesPatient")]
        public bool sg_patients_allergy_edit { get; set; }

        [JsonPropertyName("makeAppointment")]
        public bool sg_appointment_create { get; set; }

        [JsonPropertyName("canEditObsAppointment")]
        public bool sg_edit_appointmente_obs { get; set; }

        [JsonPropertyName("canTakeExams")]
        public bool sg_take_exams { get; set; }

        [JsonPropertyName("canTakePrescription")]
        public bool sg_take_prescriptions { get; set; }

        [JsonIgnore]
        public int cd_user_type { get; set; }

        [ForeignKey("cd_user_type")]
        [JsonIgnore]
        public UserType? UserType { get; set; }


        public UserTypePermissions(int? cd_user_type_permission, bool sg_doctors_list, bool sg_pacients_list, bool sg_employees_list,
            bool sg_patients_edit, bool sg_patients_allergy_edit, bool sg_appointment_create, bool sg_edit_appointmente_obs, bool sg_take_exams, bool sg_take_prescriptions)
        {
            this.cd_user_type_permission = cd_user_type_permission ?? null;
            this.sg_doctors_list = sg_doctors_list;
            this.sg_pacients_list = sg_pacients_list;
            this.sg_employees_list = sg_employees_list;
            this.sg_patients_edit = sg_patients_edit;
            this.sg_patients_allergy_edit = sg_patients_allergy_edit;
            this.sg_appointment_create = sg_appointment_create;
            this.sg_edit_appointmente_obs = sg_edit_appointmente_obs;
            this.sg_take_exams = sg_take_exams;
            this.sg_take_prescriptions = sg_take_prescriptions;
        }
    }
}
