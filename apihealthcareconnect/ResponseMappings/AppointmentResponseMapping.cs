using apihealthcareconnect.Models;
using apihealthcareconnect.ViewModel;
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
                    GenerateAppointmentStatus(appointment.is_active, appointment.dt_appointment),
                    new AppointmentsDoctorResponseViewModel(
                        appointment.doctorData.cd_user!.Value,
                        appointment.doctorData.Users.nm_user,
                        appointment.doctorData.cd_crm,
                        new AppointmentsSpecialtyTypeResponseViewModel(
                            appointment.doctorData.specialtyType.cd_specialty_type!.Value,
                            appointment.doctorData.specialtyType.ds_specialty_type,
                            appointment.doctorData.specialtyType.dt_interval_between_appointments,
                            appointment.doctorData.specialtyType.is_active
                        )
                    ),
                    new AppointmentsPacientResponseViewModel(
                        appointment.pacientData.cd_user,
                        appointment.pacientData.Users.nm_user,
                        appointment.pacientData.Users.ds_email,
                        appointment.pacientData.Users.ds_cellphone,
                        appointment.pacientData.Users.dt_birth,
                        appointment.pacientData.Users.cd_cpf,
                        appointment.pacientData.Users.cd_identification,
                        appointment.pacientData.Users.is_active,
                        appointment.pacientData.Users.ds_gender,
                        appointment.pacientData.Allergies.Select(x => new AppointmentsAllergyResponseViewModel(x.cd_allergy!.Value, x.nm_allergy)).ToList()
                    ),
                    appointment.appointmentsReturn.Select(ar => new AppointmentReturnResponseViewModel(
                        ar.cd_appointment_return!.Value,
                        ar.dt_return,
                        ar.ds_observation,
                        ar.is_active,
                        GenerateAppointmentStatus(ar.is_active, ar.dt_return),
                        new AppointmentsDoctorResponseViewModel(
                            ar.doctorData.cd_user!.Value,
                            ar.doctorData.Users.nm_user,
                            ar.doctorData.cd_crm,
                            new AppointmentsSpecialtyTypeResponseViewModel(
                                ar.doctorData.specialtyType.cd_specialty_type!.Value,
                                ar.doctorData.specialtyType.ds_specialty_type,
                                ar.doctorData.specialtyType.dt_interval_between_appointments,
                                ar.doctorData.specialtyType.is_active
                            )
                        )
                    )).ToList()
                );

            return mappedAppointment;
        }

        public AppointmentsUnavailableTimesResponseViewModel mapUnavailableTimes(Appointments appointment, List<TimeOnly> unavailableTimes)
        {
            var mappedUnavailableTimes = unavailableTimes
            .Select(time => time.ToString("HH:mm"))
            .ToList();

            var mappedAppointment = new AppointmentsUnavailableTimesResponseViewModel(
                appointment.doctorData.Users.cd_user!.Value,
                appointment.doctorData.Users.nm_user,
                new AppointmentsSpecialtyTypeResponseViewModel(
                    appointment.doctorData.specialtyType.cd_specialty_type!.Value,
                    appointment.doctorData.specialtyType.ds_specialty_type,
                    appointment.doctorData.specialtyType.dt_interval_between_appointments,
                    appointment.doctorData.specialtyType.is_active
                ),
                mappedUnavailableTimes
                );

            return mappedAppointment;
        }

        private string GenerateAppointmentStatus(bool isActive, DateTime appointmentDate)
        {
            if (!isActive)
            {
                return "Cancelada";
            }
            else
            {
                return DateTime.Now > appointmentDate ? "Concluída" : "Agendada";
            }
        }
    }
}
