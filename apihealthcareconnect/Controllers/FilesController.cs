using apihealthcareconnect.Interfaces;
using apihealthcareconnect.Models;
using apihealthcareconnect.ResponseMappings;
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
        private readonly ExamResponseMapping _examResponseMapping;
        private readonly PrescriptionResponseMapping _prescriptionResponseMapping;

        public FilesController(IExamsRepository examsRepository,
            IPrescriptionsRepository prescriptionsRepository,
            IAppointmentsRepository appointmentsRepository,
            ExamResponseMapping examResponseMapping,
            PrescriptionResponseMapping prescriptionResponseMapping)
        {
            _examsRepository = examsRepository ?? throw new ArgumentNullException();
            _prescriptionsRepository = prescriptionsRepository ?? throw new ArgumentNullException();
            _appointmentsRepository = appointmentsRepository ?? throw new ArgumentNullException();
            _examResponseMapping = examResponseMapping ?? throw new ArgumentNullException();
            _prescriptionResponseMapping = prescriptionResponseMapping ?? throw new ArgumentNullException();
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

            try { 
                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    byte[] fileData = memoryStream.ToArray();

                    if (examOrPrescription == "exam")
                    {
                        var examToCreate = new Exams(
                            null,
                            DateTime.Now,
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
                            DateTime.Now,
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

                    if(examToDelete == null)
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
                        return NotFound("Exame não existe");
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
    }
}
