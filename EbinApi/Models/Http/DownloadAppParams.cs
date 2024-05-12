using System.ComponentModel.DataAnnotations;

namespace EbinApi.Models.Http
{
    public class DownloadAppParams
    {
        [Required]
        public long AppId { get; set; }
        public string Version { get; set; } = "";
    }
}