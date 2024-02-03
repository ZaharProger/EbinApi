using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EbinApi.Models.Db
{
    [Table("User_apps")]
    public class UserApp: BaseModel
    {
        public long UserId { get; set; }

        public User? User { get; set; }

        public long AppId { get; set; }

        public App? App { get; set; }

        [Required, Column("app_version", TypeName = "varchar(20)")]
        public string AppVersion { get; set;}
    }
}