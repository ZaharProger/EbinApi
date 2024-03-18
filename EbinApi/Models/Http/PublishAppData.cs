using System.Text.Json.Serialization;

namespace EbinApi.Models.Http
{
    public class PublishAppData
    {
        public string Name { get; set; }
        public string Status { get; set; }
        public string Developer { get; set; }
        public string? Description { get; set; }
        public string? MinIos { get; set; }
        public string? MinAndroid { get; set; }

        [JsonPropertyName("iconFile")]
        public IFormFile? IconFile { get; set; }

        [JsonPropertyName("imageFiles")]
        public List<IFormFile> ImagesFiles { get; set; } = [];
        public string Version { get; set;}

        [JsonPropertyName("apkFile")]
        public IFormFile? ApkFile { get; set; }
        public string? TestFlight { get; set; }
        public string Companies { get; set; } = "";
    }
}