namespace SerhendKumbara.Core.Helpers;

public static class TypeHelper
{
    public static T DeserializeFromString<T>(this string data) => JsonSerializer.Deserialize<T>(data, JsonConstants.SerializerOptions)!;

    public static string SerializeToString<T>(this T data) => JsonSerializer.Serialize(data, JsonConstants.SerializerOptions);

    //public static decimal ConvertToDecimal(this string value) => decimal.Parse(value.Replace('.', ','), CultureInfo.InvariantCulture);
}