using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace apihealthcareconnect.Models
{
    [Table("users")]
    public class Users
    {
        [Key]
        public int cd_user { get; set; }
        public string cd_cpf { get; set; }
        public string cd_identification { get; set; }
        public string nm_user { get; set; }
        public DateTime dt_birth { get; set; }
        public string ds_email { get; set; }
        public string ds_cellphone { get; set; }
        public string ds_login { get; set; }
        public string? ds_password { get; set; }
        public int cd_user_type { get; set; }


        public Users(string cd_cpf, string cd_identification, string nm_user, DateTime dt_birth, string ds_email, string ds_cellphone, string ds_login, string? ds_password, int cd_user_type)
        {
            this.cd_cpf = cd_cpf;
            this.cd_identification = cd_identification;
            this.nm_user = nm_user;
            this.dt_birth = dt_birth;
            this.ds_email = ds_email;
            this.ds_cellphone = ds_cellphone;
            this.ds_login = ds_login;
            this.ds_password = ds_password;
            this.cd_user_type = cd_user_type;
        }
    }
}
