using System.Text.Json;

namespace YtDownloader.Metadata;

public static class JsonFields
{
    public static string Text(this JsonElement element, string name) =>
        element.TryGetProperty(name, out JsonElement value) && value.ValueKind == JsonValueKind.String
            ? value.GetString() ?? string.Empty
            : string.Empty;

    public static double Number(this JsonElement element, string name) =>
        element.TryGetProperty(name, out JsonElement value) && value.ValueKind == JsonValueKind.Number
            ? value.GetDouble()
            : 0;

    public static IEnumerable<JsonElement> Items(this JsonElement element, string name) =>
        element.TryGetProperty(name, out JsonElement value) && value.ValueKind == JsonValueKind.Array
            ? value.EnumerateArray().ToList()
            : new List<JsonElement>();

    public static IEnumerable<string> Keys(this JsonElement element, string name) =>
        element.TryGetProperty(name, out JsonElement value) && value.ValueKind == JsonValueKind.Object
            ? value.EnumerateObject().Select(property => property.Name).ToList()
            : new List<string>();
}
