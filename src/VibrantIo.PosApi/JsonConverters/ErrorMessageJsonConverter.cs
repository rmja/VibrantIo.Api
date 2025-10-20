using System.Text.Json;
using System.Text.Json.Serialization;

namespace VibrantIo.PosApi.JsonConverters;

internal class ErrorMessageJsonConverter : JsonConverter<string[]>
{
    public override string[] Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options
    )
    {
        if (reader.TokenType == JsonTokenType.StartArray)
        {
            var messages = new List<string>();
            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndArray)
                {
                    break;
                }
                if (reader.TokenType == JsonTokenType.String)
                {
                    messages.Add(reader.GetString()!);
                }
            }
            return messages.ToArray();
        }
        else if (reader.TokenType == JsonTokenType.String)
        {
            return [reader.GetString()!];
        }
        throw new JsonException("Unexpected token type for error message");
    }

    public override void Write(Utf8JsonWriter writer, string[] value, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }
}
