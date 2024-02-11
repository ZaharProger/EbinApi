namespace EbinApi.Models.Enums
{
    [AttributeUsage(AttributeTargets.Field)]
    public class StringValue(string value) : Attribute
    {
        public string Value { get; protected set; } = value;
    }
}