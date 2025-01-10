namespace VibrantIo.PosApi.PaymentIntents;

public record PaymentIntent
{
    public int Amount { get; set; }
    public required string Description { get; set; }
    public Dictionary<string, string> Metadata { get; set; } = [];
    public PaymentIntentStatus Status { get; set; }
    public string CancelationReason { get; set; } = "";
}
