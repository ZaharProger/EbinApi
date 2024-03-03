namespace EbinApi.Models.Http
{
    public class UpdateData
    {
        public long AppId { get; set; }
        public string Version { get; set;}
        public IFormFile? ApkFile { get; set; }
        public string? TestFlight { get; set; }
        public string? Description { get; set; }
    }
}