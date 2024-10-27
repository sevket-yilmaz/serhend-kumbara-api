using System.Text.Encodings.Web;
using System.Text.Unicode;

namespace SerhendKumbara.Core.Constants;

public static class JsonConstants
{
    public static JsonSerializerOptions SerializerOptions => new()
    {
        ReferenceHandler = ReferenceHandler.IgnoreCycles,
        WriteIndented = true,
        PropertyNameCaseInsensitive = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals,
        Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
    };
}