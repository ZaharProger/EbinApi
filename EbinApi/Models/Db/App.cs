using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EbinApi.Models.Db
{
    [Table("Apps")]
    public class App: BaseModel
    {
        [Required, Column("name", TypeName = "varchar(100)")]
        public string Name { get; set; }

        [Required, Column("status", TypeName = "varchar(70)")]
        public string Status { get; set; }

        [Required, Column("developer", TypeName = "varchar(50)")]
        public string Developer { get; set; }

        [Column("description", TypeName = "varchar(500)")]
        public string? Description { get; set; }

        public byte? MinIos { get; set; }

        public float? MinAndroid { get; set; }

        [Column("icon", TypeName = "varchar(100)")]
        public string? Icon { get; set; }

        [Column("images", TypeName = "varchar(500)")]
        public string? Images { get; set; }

        public List<Update> Updates { get; set; } = [];

        public List<User> Users { get; set; } = [];

        public List<UserApp> UserApps { get; set; } = [];

        public List<Review> Reviews { get; set; } = [];

        public List<Company> Companies { get; set; } = [];
    }
}