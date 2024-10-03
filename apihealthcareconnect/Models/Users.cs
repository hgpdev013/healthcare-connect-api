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
        public string? nm_street { get; set; }
        public int? cd_street_number { get; set; }
        public string? ds_complement { get; set; }
        public string? ds_neighborhood { get; set; }
        public string? nm_state { get; set; }
        public string? cd_cep { get; set; }
        public string? ds_password { get; set; }
        [ForeignKey("UserType")]
        public int cd_user_type { get; set; }
        public ICollection<UserType>? userType { get; set; }
        [ForeignKey("Doctors")]
        public ICollection<Doctors>? doctors { get; set; }




        public Users(string cd_cpf, string cd_identification, string nm_user, DateTime dt_birth, string ds_email, string ds_cellphone, string ds_login, int cd_user_type, string? nm_street,
            int? cd_street_number, string? ds_complement, string? nm_state, string? cd_cep, string? ds_password)
        {
            this.cd_cpf = cd_cpf;
            this.cd_identification = cd_identification;
            this.nm_user = nm_user;
            this.dt_birth = dt_birth;
            this.ds_email = ds_email;
            this.ds_cellphone = ds_cellphone;
            this.ds_login = ds_login;
            this.cd_user_type = cd_user_type;
            this.nm_street = nm_street;
            this.cd_street_number = cd_street_number;
            this.ds_complement = ds_complement;
            this.nm_state = nm_state;
            this.cd_cep = cd_cep;
            this.ds_password = ds_password;

        }
    }
}
