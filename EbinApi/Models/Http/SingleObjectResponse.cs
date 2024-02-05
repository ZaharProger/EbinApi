using EbinApi.Models.Db;

namespace EbinApi.Models.Http
{
    public class SingleObjectResponse<T>: BaseResponse where T: BaseModel
    {
        public T Object { get; set; }
    }
}