using apihealthcareconnect.Interfaces;
using apihealthcareconnect.Models;
using apihealthcareconnect.Repositories;
using apihealthcareconnect.ResponseMappings;
using apihealthcareconnect.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace apihealthcareconnect.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/files")]
    public class FilesController : ControllerBase
    {
        private readonly IExamsRepository _examsRepository;
        private readonly IPrescriptionsRepository _prescriptionsRepository;
        private readonly IAppointmentsRepository _appointmentsRepository;
        private readonly IUsersRepository _usersRepository;
        private readonly ExamResponseMapping _examResponseMapping;
        private readonly PrescriptionResponseMapping _prescriptionResponseMapping;

        public FilesController(IExamsRepository examsRepository,
            IPrescriptionsRepository prescriptionsRepository,
            IAppointmentsRepository appointmentsRepository,
            IUsersRepository usersRepository,
            ExamResponseMapping examResponseMapping,
            PrescriptionResponseMapping prescriptionResponseMapping)
        {
            _examsRepository = examsRepository ?? throw new ArgumentNullException();
            _prescriptionsRepository = prescriptionsRepository ?? throw new ArgumentNullException();
            _appointmentsRepository = appointmentsRepository ?? throw new ArgumentNullException();
            _usersRepository = usersRepository ?? throw new ArgumentNullException();
            _examResponseMapping = examResponseMapping ?? throw new ArgumentNullException();
            _prescriptionResponseMapping = prescriptionResponseMapping ?? throw new ArgumentNullException();
        }

        [HttpGet("documents-exams/{userId}")]
        public async Task<IActionResult> getDocumentsByUserId(int userId)
        {

            var pacientByUserId = await _usersRepository.GetById(userId);

            if (pacientByUserId?.pacientData == null)
            {
                return NotFound("O paciente solicitado não existe");
            }

            var exams = await _examsRepository.GetAll(pacientByUserId?.pacientData.cd_pacient);

            if (exams == null)
            {
                return NotFound("Exame não encontrado.");
            }

            var mappedExams = exams.Select(e => _examResponseMapping.mapExamsAppointments(e)).ToList();

            return Ok(mappedExams);
        }

        [HttpPost("documents-exam/{userId}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> PostFile(int userId, IFormFile file)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var pacientByUserId = await _usersRepository.GetById(userId);

            if (pacientByUserId?.pacientData == null)
            {
                return NotFound("O paciente solicitado não existe");
            }

            try
            {
                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    byte[] fileData = memoryStream.ToArray();

                        var examToCreate = new Exams(
                            null,
                            DateTime.Now.ToBrazilTime(),
                            file.FileName,
                            Path.GetExtension(file.FileName),
                            fileData,
                            null,
                            pacientByUserId.pacientData.cd_pacient!.Value
                        );

                        var createdExam = await _examsRepository.Add(examToCreate);

                        var createdExamMapped = _examResponseMapping.mapExamsGeneral(createdExam);

                        return Ok(createdExamMapped);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao cadastrar documento: {ex.Message}");
            }
        }

        [HttpGet("download/{id}")]
        public async Task<IActionResult> DownloadFile(int id, string examOrPrescription)
        {
            if (string.IsNullOrEmpty(examOrPrescription))
            {
                return BadRequest("O tipo do arquivo é obrigatório (exam ou prescription).");
            }

            try
            {
                if (examOrPrescription == "exam")
                {
                    var exam = await _examsRepository.GetById(id);
                    if (exam == null)
                    {
                        return NotFound("Exame não encontrado.");
                    }

                    return File(exam.exam, "application/octet-stream", exam.nm_exam_file);
                }

                if (examOrPrescription == "prescription")
                {
                    var prescription = await _prescriptionsRepository.GetById(id);
                    if (prescription == null)
                    {
                        return NotFound("Prescrição não encontrada.");
                    }

                    return File(prescription.prescription, "application/octet-stream", prescription.nm_prescription_file);
                }

                return BadRequest("Tipo inválido.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao baixar o arquivo: {ex.Message}");
            }
        }

        [HttpPost("{appointmentId}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> PostFile(int appointmentId, string examOrPrescription, IFormFile file)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (string.IsNullOrEmpty(examOrPrescription))
            {
                return BadRequest("O tipo do arquivo é obrigatório (exam ou prescription).");
            }

            var appointmentToAttach = await _appointmentsRepository.GetById(appointmentId);

            if (appointmentToAttach == null)
            {
                return NotFound("Consulta não existe no sistema");
            }

            try
            {
                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    byte[] fileData = memoryStream.ToArray();

                    if (examOrPrescription == "exam")
                    {
                        var examToCreate = new Exams(
                            null,
                            DateTime.Now.ToBrazilTime(),
                            file.FileName,
                            Path.GetExtension(file.FileName),
                            fileData,
                            appointmentId,
                            appointmentToAttach.cd_pacient
                        );

                        var createdExam = await _examsRepository.Add(examToCreate);

                        var createdExamMapped = _examResponseMapping.mapExamsGeneral(createdExam);

                        return Ok(createdExamMapped);
                    }

                    if (examOrPrescription == "prescription")
                    {
                        var prescriptionToCreate = new Prescriptions(
                            null,
                            DateTime.Now.ToBrazilTime(),
                            file.FileName,
                            Path.GetExtension(file.FileName),
                            fileData,
                            appointmentId,
                            appointmentToAttach.cd_pacient
                        );

                        var createdPrescription = await _prescriptionsRepository.Add(prescriptionToCreate);

                        var createdPrescriptionMapped = _prescriptionResponseMapping.mapPrescriptionsAppointments(createdPrescription);

                        return Ok(createdPrescriptionMapped);
                    }

                    return BadRequest("Tipo inválido");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao cadastrar documento: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFile(int id, string examOrPrescription)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (string.IsNullOrEmpty(examOrPrescription))
            {
                return BadRequest("O tipo do arquivo é obrigatório (exam ou prescription).");
            }

            try
            {
                if (examOrPrescription == "exam")
                {
                    var examToDelete = await _examsRepository.GetById(id);

                    if (examToDelete == null)
                    {
                        return NotFound("Exame não existe");
                    }

                    await _examsRepository.Delete(examToDelete);
                    return Ok(id);
                }

                if (examOrPrescription == "prescription")
                {
                    var prescriptionToDelete = await _prescriptionsRepository.GetById(id);

                    if (prescriptionToDelete == null)
                    {
                        return NotFound("Prescrição não existe");
                    }

                    await _prescriptionsRepository.Delete(prescriptionToDelete);
                    return Ok(id);
                }

                return BadRequest("Tipo inválido");

            }

            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao deletar documento: {ex.Message}");
            }
        }

        [HttpPut("rename/{id}")]
        public async Task<IActionResult> RenameFile(int id, string examOrPrescription, [FromBody] string newFileName)
        {
            if (string.IsNullOrEmpty(examOrPrescription))
            {
                return BadRequest("O tipo do arquivo é obrigatório (exam ou prescription).");
            }

            if (string.IsNullOrWhiteSpace(newFileName))
            {
                return BadRequest("O novo nome do arquivo é obrigatório.");
            }

            try
            {
                if (examOrPrescription == "exam")
                {
                    var exam = await _examsRepository.GetById(id);
                    if (exam == null)
                    {
                        return NotFound("Exame não encontrado.");
                    }

                    exam.nm_exam_file = newFileName;
                    await _examsRepository.Update(exam);

                    return Ok("Nome do exame atualizado com sucesso.");
                }

                if (examOrPrescription == "prescription")
                {
                    var prescription = await _prescriptionsRepository.GetById(id);
                    if (prescription == null)
                    {
                        return NotFound("Prescrição não encontrada.");
                    }

                    prescription.nm_prescription_file = newFileName;
                    await _prescriptionsRepository.Update(prescription);

                    return Ok("Nome da prescrição atualizado com sucesso.");
                }

                return BadRequest("Tipo inválido.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao renomear o arquivo: {ex.Message}");
            }
        }
    }
}
