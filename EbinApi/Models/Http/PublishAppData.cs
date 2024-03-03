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
        public IFormFile? IconFile { get; set; }
        public List<IFormFile> ImagesFiles { get; set; } = [];
        public string Version { get; set;}
        public IFormFile? ApkFile { get; set; }
        public string? TestFlight { get; set; }
        public string Companies { get; set; } = "";
    }
}