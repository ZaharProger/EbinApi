using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EbinApi.Models.Db
{
    [Table("Users")]
    public class User: BaseModel
    {
        [Required, Column("name", TypeName = "varchar(30)")]
        public string Name { get; set; }

        [Required, Column("last_name", TypeName = "varchar(30)")]
        public string LastName { get; set; }

        [Column("middle_name", TypeName = "varchar(50)")]
        public string? MiddleName { get; set; }

        [Required, Column("status", TypeName = "varchar(70)")]
        public string Status { get; set; }

        [Required, Column("phone", TypeName = "varchar(20)")]
        public string Phone { get; set; }

        public long RoleId { get; set; }

        public virtual Role? Role { get; set; }

        public long CompanyId { get; set; }

        public virtual Company? Company { get; set; }

        public virtual Account? Account { get; set; }

        public virtual List<App> Apps { get; set; } = [];

        public virtual List<UserApp> UserApps { get; set; } = [];

        public virtual List<Review> Reviews { get; set; } = [];
    }
}