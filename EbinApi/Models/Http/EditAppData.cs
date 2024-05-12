namespace EbinApi.Models.Http
{
    public class EditAppData: PublishAppData
    {
        public string Id { get; set; }
        public List<EditUpdateData> Updates { get; set; }
    }
}