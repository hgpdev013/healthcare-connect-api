﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace apihealthcareconnect.Models
{
    [Table("pacients")]
    public class Pacients
    {
        [Key]
        public int? cd_pacient { get; set; }

        public int cd_user { get; set; }

        [ForeignKey("cd_user")]
        public Users Users { get; set; }

        public List<Allergies> Allergies { get; set; } = new List<Allergies>();

        public List<Exams> exams { get; set; } = new List<Exams>();

        public List<Prescriptions> prescriptions { get; set; } = new List<Prescriptions>();

        public Pacients(int? cd_pacient, int cd_user)
        {
            this.cd_pacient = cd_pacient;
            this.cd_user = cd_user;
        }
    }
}