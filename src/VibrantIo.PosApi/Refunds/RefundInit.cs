using System.Text.Json.Serialization;

namespace VibrantIo.PosApi.Refunds;

public record RefundInit
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? Amount { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Currency { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? PaymentIntentId { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? ChargeId { get; set; }
    public required string Description { get; set; }
    public RefundReason Reason { get; set; } = RefundReason.RequestedByCustomer;
    public Dictionary<string, string> Metadata { get; set; } = [];
}
