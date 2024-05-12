namespace EbinApi.Models.Http
{
    public class EditUpdateData
    {
        public long Id { get; set; }
        public long AppId { get; set; }
        public string Version { get; set;}
        public string? Description { get; set; }
    }
}