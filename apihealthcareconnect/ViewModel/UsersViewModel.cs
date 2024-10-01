namespace apihealthcareconnect.ViewModel
{
    public class UsersViewModel
    {
        public string cd_cpf { get; set; }
        public string cd_identification { get; set; }
        public DateTime dt_birth {  get; set; }
        public string ds_cellphone { get; set; }
        public string nm_user { get; set; }
        public string ds_email { get; set; }
        public string? ds_password { get; set; }
        public string ds_login { get; set; }
        public int cd_user_type { get; set; }
    }
}
