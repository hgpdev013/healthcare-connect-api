using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace apihealthcareconnect.Models
{
    [Table("user_type")]
    public class UserType
    {
        [Key]
        public int? cd_user_type { get; set; }
        public string ds_user_type { get; set; }
        public ICollection<Users>? Users { get; set; } = new List<Users>();

        public UserType(int? cd_user_type, string ds_user_type)
        {
            this.cd_user_type = cd_user_type;
            this.ds_user_type = ds_user_type;
        }
    }
}
