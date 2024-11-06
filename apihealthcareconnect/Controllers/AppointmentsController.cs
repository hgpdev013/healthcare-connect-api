using apihealthcareconnect.Interfaces;
using apihealthcareconnect.Models;
using apihealthcareconnect.ViewModel.Requests;
using Microsoft.AspNetCore.Mvc;

namespace apihealthcareconnect.Controllers
{
    [ApiController]
    [Route("api/appointments")]
    public class AppointmentsController : ControllerBase
    {
        private readonly IAppointmentsRepository _appointmentsRepository;
        private readonly IUsersRepository _usersRepository;

        public AppointmentsController(IAppointmentsRepository appointmentsRepository, IUsersRepository usersRepository)
        {
            _appointmentsRepository = appointmentsRepository ?? throw new ArgumentNullException();
            _usersRepository = usersRepository ?? throw new ArgumentNullException();
        }


        [HttpGet]
        [ProducesResponseType(typeof(List<Appointments>), 200)]
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

            return Ok(appointments);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Appointments), 200)]
        public async Task<IActionResult> GetAppointmentById(int id)
        {
            var appointment = await _appointmentsRepository.GetById(id);

            if (appointment == null)
            {
                return NotFound("Consulta não consta no sistema");
            }
            
            return Ok(appointment);
        }
    }
}
