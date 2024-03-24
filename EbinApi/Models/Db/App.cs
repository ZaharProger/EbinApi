using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

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

        [JsonPropertyName("min_ios")]
        public string? MinIos { get; set; }

        [JsonPropertyName("min_android")]
        public string? MinAndroid { get; set; }

        [Column("icon", TypeName = "varchar(100)")]
        public string? Icon { get; set; }

        [Column("images", TypeName = "varchar(5000)")]
        public string? Images { get; set; }

        public virtual List<Update> Updates { get; set; } = [];

        public virtual List<User> Users { get; set; } = [];

        public virtual List<UserApp> UserApps { get; set; } = [];

        public virtual List<Review> Reviews { get; set; } = [];

        public virtual List<Company> Companies { get; set; } = [];

        [NotMapped]
        public string? Size { get; set; }

        [NotMapped]
        public bool IsInstalled { get; set; }

        [NotMapped]
        public string Access { get; set; }

        [NotMapped]
        public int Downloads { get; set; }

        [NotMapped]
        public Update? LastUpdate { get; set; }

        [NotMapped]
        public float Rating { get; set; }
    }
}