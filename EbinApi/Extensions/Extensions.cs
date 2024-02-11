using EbinApi.Models.Enums;

namespace EbinApi.Extensions
{
    public static class Extensions
    {
        public static string? GetStringValue(this Enum enumValue)
        {
            var type = enumValue.GetType();
            var fieldInfo = type.GetField(enumValue.ToString());

            var attributes = fieldInfo?.GetCustomAttributes(
                typeof(StringValue),
                false
            ) as StringValue[];

            return attributes?.Length > 0 ? attributes[0].Value : null;
        }

        public static string FormatSize(this long bytes)
        {
            const long OneKB = 1024;
            const long OneMB = OneKB * OneKB;
            const long OneGB = OneMB * OneKB;
            const long OneTB = OneGB * OneKB;

            return bytes switch
            {
                < OneKB => $"{bytes:0.00} B",
                (>= OneKB) and(< OneMB) => $"{bytes / OneKB:0.00} KB",
                (>= OneMB) and(< OneGB) => $"{bytes / OneMB:0.00} MB",
                (>= OneGB) and(< OneTB) => $"{bytes / OneMB:0.00} GB",
                >= OneTB => $"{bytes / OneTB:0.00} TB"
            };
        }
    }
}