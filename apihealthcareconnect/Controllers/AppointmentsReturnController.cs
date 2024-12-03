using apihealthcareconnect.Interfaces;
using apihealthcareconnect.Models;
using apihealthcareconnect.ViewModel.Reponses.Appointments;
using apihealthcareconnect.ResponseMappings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using apihealthcareconnect.ViewModel.Requests.Appointments;
using apihealthcareconnect.ViewModel.Requests.Appointments.Return;
using apihealthcareconnect.Services;

namespace apihealthcareconnect.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/appointments-return")]
    public class AppointmentsReturnController : ControllerBase
    {
        private readonly IAppointmentsRepository _appointmentsRepository;
        private readonly IUsersRepository _usersRepository;
        private readonly IAppointmentsReturnRepository _appointmentsReturnRepository;
        private readonly AppointmentResponseMapping _appointmentResponseMapping;

        public AppointmentsReturnController(IAppointmentsRepository appointmentsRepository,
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
        [ProducesResponseType(typeof(List<AppointmentReturnResponseViewModel>), 200)]
        public async Task<IActionResult> GetAppointmentsReturnByAppointment(int appointmentId, DateTime? date)
        {
            var appointmentReferenced = await _appointmentsRepository.GetById(appointmentId);

            if (appointmentReferenced == null)
            {
                return NotFound("A consulta para ver os retornos não existe");
            }

            var appointmentsReturnByAppointment = await _appointmentsReturnRepository.GetAll(appointmentId, date, null);

            var appointmentsReturnFormatted = appointmentsReturnByAppointment.Select(x => _appointmentResponseMapping.mapAppointmentReturn(x)).ToList();

            return Ok(appointmentsReturnFormatted);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(AppointmentReturnResponseViewModel), 200)]
        public async Task<IActionResult> GetAppointmentReturnById(int id)
        {
            var appointmentById = await _appointmentsReturnRepository.GetById(id);

            if (appointmentById == null)
            {
                return NotFound("O retorno de consulta procurado não existe");
            }

            var appointmentReturnFormatted = _appointmentResponseMapping.mapAppointmentReturn(appointmentById);

            return Ok(appointmentReturnFormatted);
        }

        [HttpPost]
        [ProducesResponseType(typeof(AppointmentReturnResponseViewModel), 200)]
        public async Task<IActionResult> PostAppointment(AppointmentReturnRequestViewModel AppointmentReturnParams)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var appointmentReferenced = await _appointmentsRepository.GetById(AppointmentReturnParams.appointmentId);

            if (appointmentReferenced == null)
            {
                return NotFound("A consulta para vincular o retorno não existe");
            }

            var doctorScheduled = await _usersRepository.GetById(AppointmentReturnParams.doctorId);

            if (doctorScheduled == null)
            {
                return NotFound("Médico não existe");
            }

            if (doctorScheduled.cd_user_type != 1)
            {
                return BadRequest("Usuário não é médico");
            }

            var appointmentsOnSameDate = await _appointmentsRepository.GetAll(null, doctorScheduled.doctorData.cd_doctor, AppointmentReturnParams.date);
            var returnsOnSameDate = await _appointmentsReturnRepository.GetAll(null, AppointmentReturnParams.date, doctorScheduled.doctorData.cd_doctor);

            if (appointmentsOnSameDate.Count > 0 || returnsOnSameDate.Count > 0)
            {
                return BadRequest("Já existe uma consulta no mesmo horário");
            }

            var appointmentReturnToCreate = new AppointmentsReturn(
                null,
                AppointmentReturnParams.date,
                AppointmentReturnParams.observation,
                AppointmentReturnParams.isActive,
                doctorScheduled.doctorData.cd_doctor,
                appointmentReferenced.cd_appointment!.Value

            );

            var createdAppointmentReturn = await _appointmentsReturnRepository.Add(appointmentReturnToCreate);

            if (createdAppointmentReturn == null)
            {
                return BadRequest("Erro ao cadastrar retorno de consulta");
            }

            var createdAppointmentReturnFormatted = _appointmentResponseMapping.mapAppointmentReturn(createdAppointmentReturn);

            return Ok(createdAppointmentReturnFormatted);
        }

        [HttpPut]
        [ProducesResponseType(typeof(AppointmentReturnResponseViewModel), 200)]
        public async Task<IActionResult> PutAppointment(AppointmentReturnPutRequestViewModel AppointmentParams)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (AppointmentParams.id == null)
            {
                return NotFound("Retorno de consulta inválido");
            }

            var appointmentReturnToBeEdited = await _appointmentsReturnRepository.GetById(AppointmentParams.id);

            if (appointmentReturnToBeEdited == null)
            {
                return NotFound("Retorno de consulta desejado não existe");
            }

            if (AppointmentParams.date != appointmentReturnToBeEdited.dt_return && DateTime.Now.ToBrazilTime().AddDays(-1) >= appointmentReturnToBeEdited.dt_return)
            {
                return Forbid("A data da consulta não pode ser alterada após a data antiga ter passado.");
            }

            var appointmentsOnSameDate = await _appointmentsRepository.GetAll(null, appointmentReturnToBeEdited.cd_doctor, AppointmentParams.date);
            var returnsOnSameDate = await _appointmentsReturnRepository.GetAll(null, AppointmentParams.date, appointmentReturnToBeEdited.cd_doctor);

            var sameReturn = returnsOnSameDate.Find(a => a.cd_appointment_return == AppointmentParams.id);

            if (sameReturn == null)
            {
                if (appointmentsOnSameDate.Count > 0 || returnsOnSameDate.Count > 0)
                {
                    return BadRequest("Já existe uma consulta no mesmo horário com o médico escolhido");
                }
            }

            appointmentReturnToBeEdited.dt_return = AppointmentParams.date;
            appointmentReturnToBeEdited.is_active = AppointmentParams.isActive;
            appointmentReturnToBeEdited.ds_observation = AppointmentParams.observation;

            var updatedAppointmentReturn = await _appointmentsReturnRepository.Update(appointmentReturnToBeEdited);

            if (updatedAppointmentReturn == null)
            {
                return BadRequest("Erro ao editar retorno de consulta");
            }

            var updatedAppointmentReturnFormatted = _appointmentResponseMapping.mapAppointmentReturn(updatedAppointmentReturn);

            return Ok(updatedAppointmentReturnFormatted);
        }
    }
}
