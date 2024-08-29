using System.Text.Json;
using System.Text.Json.Serialization;

namespace SummerShop.Application.Extensions;

public static class JsonExtensions
{
    private static readonly JsonSerializerOptions Options = CreateOptions();

    private static JsonSerializerOptions CreateOptions()
    {
        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            PropertyNameCaseInsensitive = true,
            WriteIndented = true,
            Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) }
        };

        return options;
    }

    public static string ToJson<T>(this T o)
    {
        return JsonSerializer.Serialize(o, Options);
    }

    public static T? FromJson<T>(this string s)
    {
        return JsonSerializer.Deserialize<T>(s, Options);
    }

    public static async Task<T?> FromJson<T>(this Stream stream)
    {
        return await JsonSerializer.DeserializeAsync<T>(stream, Options);
    }

    public static T? FromJsonBinary<T>(this BinaryData binaryData)
    {
        return binaryData.ToString().FromJson<T>();
    }

    public static BinaryData ToBinaryData<T>(this T o)
    {
        return BinaryData.FromObjectAsJson(o, Options);
    }
}