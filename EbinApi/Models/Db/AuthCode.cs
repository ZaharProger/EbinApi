using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EbinApi.Models.Db
{
    [Table("Auth_codes")]
    public class AuthCode: BaseModel
    {
        [Required, Column("phone", TypeName = "varchar(20)")]
        public string Phone { get; set; }

        [Required, Column("code")]
        public string Code { get; set; }
    }
}