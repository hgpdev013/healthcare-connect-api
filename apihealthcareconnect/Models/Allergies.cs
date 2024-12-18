﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace apihealthcareconnect.Models
{
    [Table("allergies")]
    public class Allergies
    {
        [Key]
        public int? cd_allergy { get; set; }

        public string nm_allergy { get; set; }

        public int cd_pacient { get; set; }

        [ForeignKey("cd_pacient")]
        public Pacients? pacient { get; set; }

        public Allergies(int? cd_allergy, string nm_allergy, int cd_pacient)
        {
            this.cd_allergy = cd_allergy ?? null;
            this.nm_allergy = nm_allergy;
            this.cd_pacient = cd_pacient;
        }
    }
}
