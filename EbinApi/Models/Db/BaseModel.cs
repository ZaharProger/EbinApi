using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EbinApi.Models.Db
{
    [JsonDerivedType(typeof(User), typeDiscriminator: "user")]
    [JsonDerivedType(typeof(Company), typeDiscriminator: "company")]
    [JsonDerivedType(typeof(Account), typeDiscriminator: "account")]
    [JsonDerivedType(typeof(App), typeDiscriminator: "app")]
    [JsonDerivedType(typeof(Review), typeDiscriminator: "review")]
    [JsonDerivedType(typeof(Update), typeDiscriminator: "update")]
    [JsonDerivedType(typeof(UserApp), typeDiscriminator: "user_app")]
    public class BaseModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key, Column("id")]
        public long Id { get; set; }
    }
}