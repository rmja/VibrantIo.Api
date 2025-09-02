namespace VibrantIo.PosApi.Charges;

public record Charge : IPaginateableObject
{
    public required string Id { get; set; }
    public required string AccountId { get; set; }

    /// <summary>
    /// Amount in cents.
    /// </summary>
    public int Amount { get; set; }
    public string? BalanceTransactionId { get; set; }
    public string? AuthBalanceTransactionId { get; set; }
    
    /// <summary>
    /// Currency this charge was created with, e.g. DKK.
    /// </summary>
    public required string Currency { get; set; }
    
    /// <summary>
    /// ID of the PaymentIntent associated with this charge, if one exists.
    /// </summary>
    public string? PaymentIntent { get; set; }
    
    /// <summary>
    /// Currently only card payments is supported so this is always card
    /// </summary>
    public required string PaymentMethod { get; set; }
    /// <summary>
    /// This is the transaction number that appears on receipts for this charge. This attribute will be null until a receipt has been sent.
    /// </summary>
    public string? ReceiptNumber { get; set; }
    public required string TerminalId { get; set; }
    public PaymentStatus Status { get; set; }

    /// <summary>
    /// Whether the charge has been fully refunded. If the charge is only partially refunded, this attribute will still be false.
    /// </summary>
    public bool Refunded { get; set; }
    public string? FailureCode { get; set; }
    public string? FailureMessage { get; set; }
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
    public Dictionary<string, string> Metadata { get; set; } = [];
}
