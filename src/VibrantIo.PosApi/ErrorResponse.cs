using System.Text.Json.Serialization;
using VibrantIo.PosApi.JsonConverters;

namespace VibrantIo.PosApi;

internal class ErrorResponse
{
    public int Status
    {
        get => StatusCode;
        set => StatusCode = value;
    }
    public int StatusCode { get; set; }
    public string? Error { get; set; }

    [JsonConverter(typeof(ErrorMessageJsonConverter))]
    public string[] Message { get; set; } = [];
}
