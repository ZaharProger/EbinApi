using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EbinApi.Models.Db 
{
    [Table("Companies")]
    public class Company: BaseModel
    {
        [Required, Column("name", TypeName = "varchar(100)")]
        public string Name { get; set; }
        
        public List<User> Users { get; set; } = [];

        public List<App> Apps { get; set; } = [];
    }
}