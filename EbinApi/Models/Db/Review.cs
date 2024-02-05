using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EbinApi.Models.Db
{
    [Table("Reviews")]
    public class Review: BaseModel
    {
        public long UserId { get; set; }

        public virtual User? User { get; set; }

        public long AppId { get; set; }

        public virtual App? App { get; set; }

        [Required, Column("date")]
        public long Date { get; set; }

        [Required, Column("rating")]
        public byte Rating { get; set; }

        [Column("description", TypeName = "varchar(500)")]
        public string? Description { get; set; }
        
        public bool IsViewed { get; set; }
    }
}