using System.Reflection;
using Newtonsoft.Json;
using JsonException = System.Text.Json.JsonException;

namespace N90.Domain.Extensions;

public static class JsonExtensions
{
    public static IList<Newtonsoft.Json.JsonConverter> GetJsonConverters(this Type type)
    {
        var attribute = type.GetCustomAttribute<Newtonsoft.Json.JsonConverterAttribute>();
        return attribute is null ? [] : [(Newtonsoft.Json.JsonConverter)Activator.CreateInstance(attribute.ConverterType)!];
    }

    public static string GetValueByKey(this JsonReader reader, string keyToMatch)
    {
        // var keyValue = Encoding.UTF8.GetBytes(keyToMatch).AsSpan();

        while (reader.Read())
        {
            if (reader.TokenType != JsonToken.PropertyName ||
                !reader.Value!.ToString()!.Equals(keyToMatch, StringComparison.OrdinalIgnoreCase)) continue;

            reader.Read();
            return (string)reader.Value!;
        }


        throw new JsonException($"Could not find property {keyToMatch} in JSON.");
    }
}