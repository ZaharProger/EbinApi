using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EbinApi.Models.Db
{
    [Table("Accounts")]
    public class Account: BaseModel
    {
        public bool DarkTheme { get; set; } = false;
        public bool PushInstall { get; set; } = true;
        public bool PushUpdate { get; set; } = true;
        public long UserId { get; set; }
        public virtual User? User { get; set; }
    }
}