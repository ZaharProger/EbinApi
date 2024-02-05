using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EbinApi.Models.Db
{
    [Table("Roles")]
    public class Role: BaseModel
    {
        [Required, Column("name", TypeName = "varchar(30)")]
        public string Name { get; set; }
    }
}