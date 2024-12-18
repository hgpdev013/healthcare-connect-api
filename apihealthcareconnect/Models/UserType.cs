﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace apihealthcareconnect.Models
{
    [Table("user_type")]
    public class UserType
    {
        [Key]
        public int? cd_user_type { get; set; }

        public string ds_user_type { get; set; }

        public bool is_active { get; set; }

        public UserTypePermissions permissions { get; set; }

        public UserType(int? cd_user_type, string ds_user_type, bool is_active)
        {
            this.cd_user_type = cd_user_type;
            this.ds_user_type = ds_user_type;
            this.is_active = is_active;
        }
    }
}
