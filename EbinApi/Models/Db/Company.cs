using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EbinApi.Models.Db 
{
    [Table("Companies")]
    public class Company: BaseModel
    {
        [Required, Column("name", TypeName = "varchar(100)")]
        public string Name { get; set; }
        
        public virtual List<User> Users { get; set; } = [];

        public virtual List<App> Apps { get; set; } = [];
    }
}