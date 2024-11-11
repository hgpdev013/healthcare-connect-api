using apihealthcareconnect.Interfaces;
using apihealthcareconnect.Models;
using apihealthcareconnect.ViewModel.Reponses.Appointments;
using apihealthcareconnect.ResponseMappings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace apihealthcareconnect.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/appointments")]
    public class AppointmentsController : ControllerBase
    {
        private readonly IAppointmentsRepository _appointmentsRepository;
        private readonly IUsersRepository _usersRepository;
        private readonly IAppointmentsReturnRepository _appointmentsReturnRepository;
        private readonly AppointmentResponseMapping _appointmentResponseMapping;

        public AppointmentsController(IAppointmentsRepository appointmentsRepository,
            IAppointmentsReturnRepository appointmentsReturnRepository,
            IUsersRepository usersRepository,
            AppointmentResponseMapping appointmentResponseMapping)
        {
            _appointmentsRepository = appointmentsRepository ?? throw new ArgumentNullException();
            _appointmentsReturnRepository = appointmentsReturnRepository ?? throw new ArgumentNullException();
            _usersRepository = usersRepository ?? throw new ArgumentNullException();
            _appointmentResponseMapping = appointmentResponseMapping ?? throw new ArgumentNullException();
        }


        [HttpGet]
        [ProducesResponseType(typeof(List<AppointmentsResponseViewModel>), 200)]
        public async Task<IActionResult> GetAppointments(int? pacientUserId, int? doctorUserId)
        {

            var pacientByUserId = pacientUserId.HasValue
                ? await _usersRepository.GetById(pacientUserId.Value)
                : null;

            if (pacientUserId.HasValue && pacientByUserId?.pacientData == null)
            {
                return NotFound("O paciente solicitado não existe");
            }

            var doctorByUserId = doctorUserId.HasValue
                ? await _usersRepository.GetById(doctorUserId.Value)
                : null;

            if (doctorUserId.HasValue && doctorByUserId?.doctorData == null)
            {
                return NotFound("O médico solicitado não existe");
            }

            var appointments = await _appointmentsRepository.GetAll(
                pacientByUserId?.pacientData?.cd_pacient,
                doctorByUserId?.doctorData?.cd_doctor
            );

            var response = appointments
                .Select(a => _appointmentResponseMapping.mapAppointmentResponse(a))
                .ToList();

            return Ok(response);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(AppointmentsResponseViewModel), 200)]
        public async Task<IActionResult> GetAppointmentById(int id)
        {
            var appointment = await _appointmentsRepository.GetById(id);

            if (appointment == null)
            {
                return NotFound("Consulta não consta no sistema");
            }

            var appointmentFormatted = _appointmentResponseMapping.mapAppointmentResponse(appointment);

            return Ok(appointmentFormatted);
        }

        [HttpGet("unavailable-times")]
        [ProducesResponseType(typeof(List<AppointmentsUnavailableTimesResponseViewModel>), 200)]
        public async Task<IActionResult> GetUnavailableTimes(int doctorUserId, DateTime date)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var doctorByUserId = await _usersRepository.GetById(doctorUserId);
;

            if (doctorByUserId?.doctorData == null)
            {
                return NotFound("O médico solicitado não existe");
            }

            var appointmentsBasedOnDateAndDoctor = await _appointmentsRepository.GetUnavailableTimes(
                doctorByUserId.doctorData.cd_doctor,
                date
            );

            var returnsBasedOnDateAndDoctor = await _appointmentsReturnRepository.GetUnavailableTimes(
                doctorByUserId.doctorData.cd_doctor,
                date
            );

            var listedAppointmentsUnavailableTimes = appointmentsBasedOnDateAndDoctor
                .Select(a => TimeOnly.FromDateTime(a.dt_appointment)).ToList();

            var listedReturnsUnavailableTimes = returnsBasedOnDateAndDoctor
                .Select(a => TimeOnly.FromDateTime(a.dt_return)).ToList();

            var listedUnavailableTimes = listedReturnsUnavailableTimes.Concat(listedAppointmentsUnavailableTimes).ToList();

            var response = _appointmentResponseMapping.mapUnavailableTimes(
                appointmentsBasedOnDateAndDoctor[0],
                listedUnavailableTimes
            );

            return Ok(response);
        }
    }
}
