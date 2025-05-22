using System.Text.Json;
using System.Text.Json.Serialization;

namespace VibrantIo.PosApi.JsonConverters;

internal class SnakeCaseLowerJsonStringEnumConverter<TEnum>()
    : JsonStringEnumConverter<TEnum>(JsonNamingPolicy.SnakeCaseLower)
    where TEnum : struct, Enum;
