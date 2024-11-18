using apihealthcareconnect.Interfaces;
using apihealthcareconnect.Models;
using apihealthcareconnect.ViewModel.Reponses.Appointments;
using apihealthcareconnect.ResponseMappings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using apihealthcareconnect.ViewModel.Requests.Appointments;

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
        public async Task<IActionResult> GetAppointments(int? pacientUserId, int? doctorUserId)
        {

            throw new NotImplementedException();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAppointmentById(int id)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public async Task<IActionResult> PostAppointment(AppointmentRequestViewModel AppointmentParams)
        {
            throw new NotImplementedException();
        }

        [HttpPut]
        public async Task<IActionResult> PutAppointment(AppointmentPutRequestViewModel AppointmentParams)
        {
            throw new NotImplementedException();
        }
    }
}
