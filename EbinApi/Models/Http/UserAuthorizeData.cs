using System.ComponentModel.DataAnnotations;

namespace EbinApi.Models.Http
{
    public class UserAuthorizeData
    {
        [Required]
        public string Phone { get; set; }

        [Required]
        public string Code { get; set; }
    }
}