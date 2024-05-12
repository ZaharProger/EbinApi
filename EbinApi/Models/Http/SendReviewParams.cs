namespace EbinApi.Models.Http
{
    public class SendReviewParams
    {
        public byte Rating { get; set; }
        public string? Description { get; set; }
        public long AppId { get; set; }
    }
}