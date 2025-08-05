using System.Text.Json.Serialization;

namespace VibrantIo.PosApi.PaymentIntents;

public record PaymentIntentInit
{
    public required int Amount { get; set; }
    public required string Description { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Currency { get; set; }
    public Dictionary<string, string> Metadata { get; set; } = [];
}
