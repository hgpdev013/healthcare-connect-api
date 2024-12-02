using apihealthcareconnect.Models;
using apihealthcareconnect.Services;
using apihealthcareconnect.ViewModel.Reponses.Appointments;

namespace apihealthcareconnect.ResponseMappings
{
    public class AppointmentResponseMapping
    {
        private ExamResponseMapping _examResponseMapping;
        private PrescriptionResponseMapping _prescriptionResponseMapping;

        public AppointmentResponseMapping(ExamResponseMapping examResponseMapping, PrescriptionResponseMapping prescriptionResponseMapping)
        {
            _examResponseMapping = examResponseMapping;
            _prescriptionResponseMapping = prescriptionResponseMapping;
        }

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
                        appointment.doctorData.Users.ds_gender,
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
                        appointment.pacientData.Users.nm_street,
                        appointment.pacientData.Users.cd_street_number,
                        appointment.pacientData.Users.ds_complement,
                        appointment.pacientData.Users.ds_neighborhood,
                        appointment.pacientData.Users.nm_state,
                        appointment.pacientData.Users.cd_cep,
                        appointment.pacientData.Users.nm_city,
                        appointment.pacientData.Allergies.Select(x => new AppointmentsAllergyResponseViewModel(x.cd_allergy!.Value, x.nm_allergy)).ToList()
                    ),
                    appointment.appointmentsReturn.Select(ar => mapAppointmentReturn(ar)).ToList(),
                    appointment.exams.Select(e => _examResponseMapping.mapExamsAppointments(e)).ToList(),
                    appointment.prescriptions.Select(p => _prescriptionResponseMapping.mapPrescriptionsAppointments(p)).ToList()
                );

            return mappedAppointment;
        }

        public AppointmentsUnavailableTimesResponseViewModel mapUnavailableTimes(Users doctor, List<TimeOnly> unavailableTimes)
        {
            var mappedUnavailableTimes = unavailableTimes
            .Select(time => time.ToString("HH:mm"))
            .ToList();

            var mappedAppointment = new AppointmentsUnavailableTimesResponseViewModel(
                doctor.cd_user!.Value,
                doctor.nm_user,
                new AppointmentsSpecialtyTypeResponseViewModel(
                    doctor.doctorData.specialtyType.cd_specialty_type!.Value,
                    doctor.doctorData.specialtyType.ds_specialty_type,
                    doctor.doctorData.specialtyType.dt_interval_between_appointments,
                    doctor.doctorData.specialtyType.is_active
                ),
                mappedUnavailableTimes
                );

            return mappedAppointment;
        }

        public AppointmentReturnResponseViewModel mapAppointmentReturn(AppointmentsReturn appointmentReturn)
        {
            var appointmentReturnMapped = new AppointmentReturnResponseViewModel(
                appointmentReturn.cd_appointment_return!.Value,
                appointmentReturn.cd_appointment,
                appointmentReturn.dt_return,
                appointmentReturn.ds_observation,
                appointmentReturn.is_active,
                GenerateAppointmentStatus(appointmentReturn.is_active, appointmentReturn.dt_return),
                new AppointmentsDoctorResponseViewModel(
                    appointmentReturn.doctorData.cd_user!.Value,
                    appointmentReturn.doctorData.Users.nm_user,
                    appointmentReturn.doctorData.cd_crm,
                    appointmentReturn.doctorData.Users.ds_gender,
                    new AppointmentsSpecialtyTypeResponseViewModel(
                        appointmentReturn.doctorData.specialtyType.cd_specialty_type!.Value,
                        appointmentReturn.doctorData.specialtyType.ds_specialty_type,
                        appointmentReturn.doctorData.specialtyType.dt_interval_between_appointments,
                        appointmentReturn.doctorData.specialtyType.is_active
                    )
                ),
                new AppointmentsPacientResponseViewModel(
                        appointmentReturn.appointment.pacientData.cd_user,
                        appointmentReturn.appointment.pacientData.Users.nm_user,
                        appointmentReturn.appointment.pacientData.Users.ds_email,
                        appointmentReturn.appointment.pacientData.Users.ds_cellphone,
                        appointmentReturn.appointment.pacientData.Users.dt_birth,
                        appointmentReturn.appointment.pacientData.Users.cd_cpf,
                        appointmentReturn.appointment.pacientData.Users.cd_identification,
                        appointmentReturn.appointment.pacientData.Users.is_active,
                        appointmentReturn.appointment.pacientData.Users.ds_gender,
                        appointmentReturn.appointment.pacientData.Users.nm_street,
                        appointmentReturn.appointment.pacientData.Users.cd_street_number,
                        appointmentReturn.appointment.pacientData.Users.ds_complement,
                        appointmentReturn.appointment.pacientData.Users.ds_neighborhood,
                        appointmentReturn.appointment.pacientData.Users.nm_state,
                        appointmentReturn.appointment.pacientData.Users.cd_cep,
                        appointmentReturn.appointment.pacientData.Users.nm_city,
                        appointmentReturn.appointment.pacientData.Allergies.Select(x => new AppointmentsAllergyResponseViewModel(x.cd_allergy!.Value, x.nm_allergy)).ToList()
                    )
            );

            return appointmentReturnMapped;
        }

        private string GenerateAppointmentStatus(bool isActive, DateTime appointmentDate)
        {
            if (!isActive)
            {
                return "Cancelada";
            }
            else
            {
                return DateTime.Now.ToBrazilTime() > appointmentDate ? "Concluída" : "Agendada";
            }
        }
    }
}
