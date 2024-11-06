using apihealthcareconnect.Models;
using apihealthcareconnect.ViewModel.Reponses.Appointments;

namespace apihealthcareconnect.ResponseMappings
{
    public class AppointmentResponseMapping
    {
        public AppointmentsResponseViewModel mapAppointmentResponse(Appointments appointment)
        {
            var mappedAppointment = new AppointmentsResponseViewModel(
                    appointment.cd_appointment!.Value,
                    appointment.dt_appointment,
                    appointment.ds_observation,
                    appointment.is_active,
                    new AppointmentsDoctorResponseViewModel(
                        appointment.doctorData.cd_user!.Value,
                        appointment.doctorData.Users.nm_user,
                        appointment.doctorData.cd_crm,
                        new AppointmentsSpecialtyTypeResponseViewModel(
                            appointment.doctorData.specialtyType.cd_specialty_type!.Value,
                            appointment.doctorData.specialtyType.ds_specialty_type
                        )
                    ),
                    new AppointmentsPacientResponseViewModel(
                        appointment.pacientData.cd_user,
                        appointment.pacientData.Users.nm_user,
                        appointment.pacientData.Users.ds_email,
                        appointment.pacientData.Users.ds_cellphone,
                        appointment.pacientData.Allergies.Select(x => new AppointmentsAllergyResponseViewModel(x.cd_allergy!.Value, x.nm_allergy)).ToList()
                    ),
                    appointment.appointmentsReturn.Select(ar => new AppointmentReturnResponseViewModel(
                        ar.cd_appointment_return!.Value,
                        ar.dt_return,
                        ar.ds_observation,
                        ar.is_active,
                        new AppointmentsDoctorResponseViewModel(
                            ar.doctorData.cd_user!.Value,
                            ar.doctorData.Users.nm_user,
                            ar.doctorData.cd_crm,
                            new AppointmentsSpecialtyTypeResponseViewModel(
                                ar.doctorData.specialtyType.cd_specialty_type!.Value,
                                ar.doctorData.specialtyType.ds_specialty_type
                            )
                        )
                    )).ToList()
                );

            return mappedAppointment;
        }
    }
}
