using apihealthcareconnect.Models;
using apihealthcareconnect.ViewModel;
using System.Numerics;

namespace apihealthcareconnect.ResponseMappings
{
    public class UserResponseMapping
    {
        private readonly ExamResponseMapping _examResponseMapping;
        private readonly PrescriptionResponseMapping _prescriptionResponseMapping;

        public UserResponseMapping(ExamResponseMapping examResponseMapping, PrescriptionResponseMapping prescriptionResponseMapping)
        {
            _examResponseMapping = examResponseMapping;
            _prescriptionResponseMapping = prescriptionResponseMapping;
        }

        public ViewModel.Requests.UserTypeViewModel MapUserType(UserType userType)
        {
            var userTypeFormatted = new ViewModel.Requests.UserTypeViewModel(
                userType.cd_user_type,
                userType.ds_user_type,
                userType.is_active,
                new ViewModel.Requests.UserTypePermissionsViewModel(
                    userType.permissions.cd_user_type_permission,
                    userType.permissions.sg_doctors_list,
                    userType.permissions.sg_pacients_list,
                    userType.permissions.sg_employees_list,
                    userType.permissions.sg_patients_edit,
                    userType.permissions.sg_patients_allergy_edit,
                    userType.permissions.sg_appointment_create,
                    userType.permissions.sg_edit_appointmente_obs,
                    userType.permissions.sg_take_exams,
                    userType.permissions.sg_take_prescriptions
                )
            );
            return userTypeFormatted;
        }

        public SpecialtyTypeViewModel MapSpecialtyType(SpecialtyType specialtyType)
        {
            var specialtyTypeFormatted = new SpecialtyTypeViewModel(
                specialtyType.cd_specialty_type!.Value,
                specialtyType.ds_specialty_type,
                specialtyType.dt_interval_between_appointments,
                specialtyType.is_active
            );

            return specialtyTypeFormatted;
        }

        public ViewModel.Reponses.User.DoctorDataResponse MapDoctorData(Doctors doctor)
        {
            var doctorData = new ViewModel.Reponses.User.DoctorDataResponse(
                doctor.cd_crm,
                doctor.cd_user!.Value,
                doctor.ds_observation,
                MapSpecialtyType(doctor.specialtyType)
            );

            return doctorData;
        }

        public AllergiesViewModel MapAllergies(Allergies allergies) {
            var allergiesMapped = new AllergiesViewModel(
                allergies.cd_allergy,
                allergies.nm_allergy
            );

                return allergiesMapped;

        }

        public ViewModel.Reponses.User.PacientDataResponse MapPacientData(Pacients pacient)
        {
            var pacientData = new ViewModel.Reponses.User.PacientDataResponse(pacient.Users.cd_user!.Value,
                pacient.Allergies.Select(a => MapAllergies(a)).ToList(),
                pacient.exams.Select(e => _examResponseMapping.mapExamsAppointments(e)).ToList(),
                pacient.prescriptions.Select(p => _prescriptionResponseMapping.mapPrescriptionsAppointments(p)).ToList()
            );

            return pacientData;
        }

        public ViewModel.Reponses.User.UserResponse MapGenericUser(bool isList, Users user)
        {
            var response = new ViewModel.Reponses.User.UserResponse(
                user.cd_user!.Value,
                user.cd_cpf,
                user.cd_identification,
                user.nm_user,
                user.dt_birth,
                user.ds_email,
                user.ds_cellphone,
                user.nm_street,
                user.cd_street_number,
                user.ds_complement,
                user.ds_neighborhood,
                user.nm_state,
                user.cd_cep,
                user.nm_city,
                user.ds_gender,
                user.is_active,
                isList ? null : user.user_photo,
                //user.user_photo,
                MapUserType(user.userType),
                user.cd_user_type == 1 ? MapDoctorData(user.doctorData) : null,
                user.cd_user_type == 2 ? MapPacientData(user.pacientData) : null
            );

            return response;
        }
    }
}
