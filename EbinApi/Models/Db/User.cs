using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EbinApi.Models.Db
{
    [Table("Users")]
    public class User: BaseModel
    {
        [Required, Column("fullname", TypeName = "varchar(100)")]
        public string Fullname { get; set; }

        [Required, Column("status", TypeName = "varchar(70)")]
        public string Status { get; set; }

        [Required, Column("phone", TypeName = "varchar(10)")]
        public string Phone { get; set; }

        public long CompanyId { get; set; }

        public Company? Company { get; set; }

        public Account? Account { get; set; }

        public List<App> Apps { get; set; } = [];

        public List<UserApp> UserApps { get; set; } = [];

        public List<Review> Reviews { get; set; } = [];
    }
}