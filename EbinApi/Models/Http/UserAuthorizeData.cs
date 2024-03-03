using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

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