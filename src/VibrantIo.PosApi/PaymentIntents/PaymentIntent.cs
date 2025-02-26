namespace VibrantIo.PosApi.PaymentIntents;

public record PaymentIntent
{
    /// <summary>
    /// Amount in cents
    /// </summary>
    public int Amount { get; set; }
    public required string Description { get; set; }
    /// <summary>
    /// Charge id associated with this payment intent
    /// </summary>
    public string? LatestCharge { get; set; }
    public Dictionary<string, string> Metadata { get; set; } = [];
    public PaymentIntentStatus Status { get; set; }
    /// <summary>
    /// Id of the terminal used to created this payment intent
    /// </summary>
    public required string TerminalId { get; set; }
    public string CancelationReason { get; set; } = "";
}
