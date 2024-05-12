namespace EbinApi.Models.Enums
{
    public enum AppAccesses
    {
        [StringValue("Общий")]
        OPEN,

        [StringValue("Закрытый")]
        CLOSE,
        [StringValue("Частичный")]
        PARTIAL
    }
}