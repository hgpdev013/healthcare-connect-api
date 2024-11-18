using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace apihealthcareconnect.Models
{
    [Table("exams")]
    public class Exams
    {
        [Key]
        public int? cd_exam { get; set; }

        public DateTime dt_exam { get; set; }

        public string nm_exam_file { get; set; }

        public string nm_file_extension { get; set; }

        public byte[] exam {  get; set; }

        [JsonIgnore]
        public int cd_appointment {  get; set; }

        [JsonIgnore]
        public int cd_pacient { get; set; }

        [JsonIgnore]
        [ForeignKey("cd_appointment")]
        public Appointments appointments { get; set; }

        [JsonIgnore]
        [ForeignKey("cd_pacient")]
        public Pacients pacientData { get; set; }

        public Exams(int? cd_exam, DateTime dt_exam, string nm_exam_file, string nm_file_extension, byte[] exam, int cd_appointment, int cd_pacient)
        {
            this.cd_exam = cd_exam;
            this.dt_exam = dt_exam;
            this.nm_exam_file = nm_exam_file;
            this.nm_file_extension = nm_file_extension;
            this.exam = exam;
            this.cd_appointment = cd_appointment;
            this.cd_pacient = cd_pacient;
        }
    }
}