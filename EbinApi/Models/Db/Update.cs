using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EbinApi.Models.Db
{
    [Table("Updates")]
    public class Update: BaseModel
    {
        public long AppId { get; set; }

        public virtual App? App { get; set; }

        [Required, Column("version", TypeName = "varchar(20)")]
        public string Version { get; set;}

        [Required, Column("date")]
        public long Date { get; set; }

        [Column("description", TypeName = "varchar(5000)")]
        public string? Description { get; set; }

        [Column("file_path", TypeName = "varchar(100)")]
        public string? FilePath { get; set; }

        [Column("testflight", TypeName = "varchar(150)")]
        public string? TestFlight { get; set; }
    }
}