using apihealthcareconnect.Interfaces;
using apihealthcareconnect.Models;
using apihealthcareconnect.ViewModel.Reponses.Appointments;
using apihealthcareconnect.ResponseMappings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using apihealthcareconnect.ViewModel.Requests.Appointments;
using apihealthcareconnect.Services;

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
                doctorByUserId?.doctorData?.cd_doctor,
                null
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

            var listedAppointmentsUnavailableTimes = new List<TimeOnly>();

            listedAppointmentsUnavailableTimes = appointmentsBasedOnDateAndDoctor
                .Select(a => TimeOnly.FromDateTime(a.dt_appointment)).ToList();

            var listedReturnsUnavailableTimes = new List<TimeOnly>();

            listedReturnsUnavailableTimes = returnsBasedOnDateAndDoctor
                .Select(a => TimeOnly.FromDateTime(a.dt_return)).ToList();

            listedAppointmentsUnavailableTimes = listedReturnsUnavailableTimes.Concat(listedAppointmentsUnavailableTimes).ToList();

            var response = _appointmentResponseMapping.mapUnavailableTimes(
                doctorByUserId,
                listedAppointmentsUnavailableTimes
            );

            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(typeof(AppointmentsResponseViewModel), 200)]
        public async Task<IActionResult> PostAppointment(AppointmentRequestViewModel AppointmentParams)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var doctorScheduled = await _usersRepository.GetById(AppointmentParams.doctorId);

            if (doctorScheduled == null)
            {
                return NotFound("Médico não existe");
            }

            if (doctorScheduled.cd_user_type != 1)
            {
                return BadRequest("Usuário não é médico");
            }

            var pacientScheduled = await _usersRepository.GetById(AppointmentParams.pacientId);

            if (pacientScheduled == null)
            {
                return NotFound("Paciente não existe");
            }

            if (pacientScheduled.cd_user_type != 2)
            {
                return BadRequest("Usuário não é paciente");
            }

            var appointmentsOnSameDate = await _appointmentsRepository.GetAll(null, doctorScheduled.doctorData.cd_doctor, AppointmentParams.date);
            var returnsOnSameDate = await _appointmentsReturnRepository.GetAll(null, AppointmentParams.date, doctorScheduled.doctorData.cd_doctor);

            if (appointmentsOnSameDate.Count > 0 || returnsOnSameDate.Count > 0)
            {
                return BadRequest("Já existe uma consulta no mesmo horário");
            }

            var appointmentToCreate = new Appointments(
                null,
                AppointmentParams.date,
                AppointmentParams.observation,
                AppointmentParams.isActive,
                pacientScheduled.pacientData.cd_pacient!.Value,
                doctorScheduled.doctorData.cd_doctor
            );

            var createdAppointment = await _appointmentsRepository.Add(appointmentToCreate);

            if (createdAppointment == null)
            {
                return BadRequest("Erro ao cadastrar consulta");
            }

            var createdAppointmentFormatted = _appointmentResponseMapping.mapAppointmentResponse(createdAppointment);

            return Ok(createdAppointmentFormatted);
        }

        [HttpPut]
        [ProducesResponseType(typeof(AppointmentsResponseViewModel), 200)]
        public async Task<IActionResult> PutAppointment(AppointmentPutRequestViewModel AppointmentParams)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (AppointmentParams.id == null)
            {
                return NotFound("Consulta inválida");
            }

            var appointmentToBeEdited = await _appointmentsRepository.GetById(AppointmentParams.id);

            if (appointmentToBeEdited == null)
            {
                return NotFound("Consulta desejada não existe");
            }

            if (AppointmentParams.date != appointmentToBeEdited.dt_appointment && DateTime.Now.ToBrazilTime().AddDays(-1) >= appointmentToBeEdited.dt_appointment)
            {
                return Forbid("A data da consulta não pode ser alterada após a data antiga ter passado.");
            }

            var appointmentsOnSameDate = await _appointmentsRepository.GetAll(null, appointmentToBeEdited.cd_doctor, AppointmentParams.date);
            var returnsOnSameDate = await _appointmentsReturnRepository.GetAll(null, AppointmentParams.date, appointmentToBeEdited.cd_doctor);
            var sameAppointment = appointmentsOnSameDate.Find(a => a.cd_appointment == AppointmentParams.id);

            if (sameAppointment == null)
            {
                if (appointmentsOnSameDate.Count > 0 || returnsOnSameDate.Count > 0)
                {
                    return BadRequest("Já existe uma consulta no mesmo horário");
                }
            }

            appointmentToBeEdited.dt_appointment = AppointmentParams.date;
            appointmentToBeEdited.is_active = AppointmentParams.isActive;
            appointmentToBeEdited.ds_observation = AppointmentParams.observation;

            var updatedAppointment = await _appointmentsRepository.Update(appointmentToBeEdited);

            if (updatedAppointment == null)
            {
                return BadRequest("Erro ao editar consulta");
            }

            var updatedAppointmentFormatted = _appointmentResponseMapping.mapAppointmentResponse(updatedAppointment);

            return Ok(updatedAppointmentFormatted);
        }
    }
}
