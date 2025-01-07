using System.Text.Json.Serialization;

namespace VibrantIo.PosApi;

internal class PagedList<T>
{
    [JsonPropertyName("has_more")]
    public bool HasMore { get; set; }

    [JsonPropertyName("total")]
    public int Total { get; set; }

    [JsonPropertyName("data")]
    public required List<T> Data { get; set; }
}
