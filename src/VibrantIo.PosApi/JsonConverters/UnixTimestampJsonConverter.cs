﻿using System.Text.Json;
using System.Text.Json.Serialization;

namespace VibrantIo.PosApi.JsonConverters;

internal class UnixTimestampJsonConverter : JsonConverter<DateTime>
{
    public override DateTime Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options
    )
    {
        return DateTimeOffset.FromUnixTimeMilliseconds(reader.GetInt64()).UtcDateTime;
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        writer.WriteNumberValue(new DateTimeOffset(value).ToUnixTimeMilliseconds());
    }
}
