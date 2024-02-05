using EbinApi.Models.Db;

namespace EbinApi.Models.Http
{
    public class CollectionResponse<T>: BaseResponse where T: BaseModel
    {
        public List<T> Objects { get; set; }
    }
}